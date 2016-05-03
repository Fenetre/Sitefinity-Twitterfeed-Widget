using System;
using System.Web.UI.WebControls;
using System.Linq;
using System.Resources;
using System.Web.UI;
using Telerik.Sitefinity.Web;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using System.ServiceModel.Syndication;
using System.ComponentModel;
using LinqToTwitter;
using Telerik.Sitefinity.Web.UI;

namespace Fenetre.Sitefinity.TwitterFeed
{
    /// <summary>
    /// This class
    /// </summary>
	[Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(TwitterFeedDesigner))]
	public partial class TwitterFeed : SimpleView
	{

		private ResourceManager resourceManager = new ResourceManager(typeof(TwitterFeed));


		string test = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null).GetSection("system.web.webPages.razor").ToString();
		

		/// <summary>
		/// Gets the resource manager.
		/// </summary>
		protected ResourceManager ResourceManager { get { return resourceManager; } }

        /// <summary>
        /// Gets the layout template path.
        /// </summary>
		public override string LayoutTemplatePath
		{
			get
			{
				return String.Empty;
			}
		}

		/// <summary>
		/// Gets the layout template name.
		/// </summary>
		protected override string LayoutTemplateName
		{
			get
			{
				return TwitterFeed.layoutTemplateName;
			}
		}

		#region Controls

        /// <summary>
        /// The placeholder of the TwitterFeed in the Sitefinity page designer.
        /// </summary>
		protected PlaceHolder phrTwitterFeed
		{
			get
			{
				return this.Container.GetControl<PlaceHolder>("phrTwitterFeed", true);
			}
		}

        /// <summary>
        /// The header of the TwitterFeed in the Sitefinity page designer.
        /// </summary>
		protected Literal ltlHeader
		{
			get
			{
				return this.Container.GetControl<Literal>("ltlHeader", true);
			}
		}

        /// <summary>
        /// Repeater that fills the list of the feed.
        /// </summary>
		protected Repeater rprTwitterFeedList
		{
			get
			{
				return this.Container.GetControl<Repeater>("rprTwitterFeedList", true);
			}
		}

        /// <summary>
        /// Placeholder for the "Read more" link.
        /// </summary>
		protected PlaceHolder phrReadMore
		{
			get
			{
				return this.Container.GetControl<PlaceHolder>("phrReadMore", true);
			}
		}

        /// <summary>
        /// Page the <see cref="phrReadMore"/> placeholder links to.
        /// </summary>
		protected HyperLink hlLinkToTwitterSearch
		{
			get
			{
				return this.Container.GetControl<HyperLink>("hlLinkToTwitterSearch", true);
			}
		}
		#endregion

		#region Public properties
        /// <summary>
        /// The header of the feed on the page.
        /// </summary>
		public string HeaderTitle { get; set; }

        /// <summary>
        /// The Twitter searchtype.
        /// </summary>
		public TwitterSearchType SearchType { get; set; }

        /// <summary>
        /// The string used in the Twitter search.
        /// </summary>
		public string SearchString { get; set; }
		
        /// <summary>
        /// States whether a read more link should be shown.
        /// </summary>
		[DefaultValue(true)]
		public bool ShowReadMore { get; set; }

        /// <summary>
        /// Contains the read more text.
        /// </summary>
		public string ReadMoreText { get; set; }
		
        /// <summary>
        /// Amount of messages to be shown.
        /// </summary>
		[DefaultValue(10)]
		public int MessageLimit { get; set; }
	
        /// <summary>
        /// The base url for the Twitter search.
        /// </summary>
		[DefaultValue("https://twitter.com/search")]
		public string SearchBaseUrl { get; set; }

        /// <summary>
        /// The base url for the Twitter page.
        /// </summary>
		[DefaultValue("https://twitter.com/")]
		public string BaseUrl { get; set; }
		
        /// <summary>
        /// Secret token used to access Twitter.
        /// </summary>
		public string TwitterUserAccessTokenSecret { get; set; }

        /// <summary>
        /// Key used to access Twitter.
        /// </summary>
		public string TwitterConsumerKey { get; set; }

        /// <summary>
        /// Secret used to access Twitter.
        /// </summary>
		public string TwitterConsumerSecret { get; set; }

        /// <summary>
        /// Token used to access Twitter.
        /// </summary>
		public string TwitterUserAccessToken { get; set; }

		#endregion

        #region Methods
        /// <summary>
        /// Base constructor using default values.
        /// </summary>
        public TwitterFeed()
		{
			this.MessageLimit = 10;
			this.ShowReadMore = true;
			this.SearchBaseUrl = "https://twitter.com/search";
			this.BaseUrl = "https://twitter.com/";
		}

        /// <summary>
        /// Handles init event.
        /// </summary>
        /// <param name="e">Event arguments</param>
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
		}

        /// <summary>
        /// Initializes the Twitter feed controls.
        /// </summary>
        /// <param name="container"></param>
		protected override void InitializeControls(GenericContainer container)
		{
			ltlHeader.Text = "Fenêtre Twitter feed";

			if (Page.IsPreviewMode() || !Page.IsDesignMode())
			{
				rprTwitterFeedList.ItemDataBound += new RepeaterItemEventHandler(rprTwitterFeedList_ItemDataBound);

				if(!string.IsNullOrWhiteSpace(HeaderTitle))
				{
					ltlHeader.Text = HeaderTitle;
				}
				else
				{
					ltlHeader.Text = resourceManager.GetString("Header");
				}

				List<TwitterFeedItem> flatDataList = null;
				string refreshUrl = String.Empty;

				try
				{
					SingleUserAuthorizer auth = new SingleUserAuthorizer()
					{
						Credentials = new InMemoryCredentials()
						{
							AccessToken = TwitterUserAccessTokenSecret, 
							ConsumerKey =  TwitterConsumerKey,
							ConsumerSecret = TwitterConsumerSecret,
							OAuthToken = TwitterUserAccessToken
						}
					};

					var twitterContext = new TwitterContext(auth);

					// Search api
					if (!String.IsNullOrWhiteSpace(SearchString))
					{
						var results = new List<Status>();

						string query = "";
						Search srch;
						IQueryable<Search> twitterQuery;
						IQueryable<Status> twitterStatusQuery;

						switch (SearchType)
						{
							case TwitterFeed.TwitterSearchType.Accounts:
								//searching added through settings has \n instead of newline as line break;
								var accounts = SearchString.Replace(Environment.NewLine, "\n").Split(new string[] { ";", "\n" }, StringSplitOptions.RemoveEmptyEntries).Where(a => !string.IsNullOrWhiteSpace(a)).Select(a => a.Replace("@", ""));
								var accountsFrom = accounts.Select(a => "from:" + a);

								// More then 25 accounts does not work
								// so split by 20
								int pageIndex = 0;
								int pageSize = 20;
								var page = accounts.Take(pageSize);
								while (page.Count() > 0)
								{
									foreach (var account in page)
									{
										twitterStatusQuery = from tweet in twitterContext.Status
															 where tweet.Type == StatusType.User
																&& tweet.ScreenName == account
																&& tweet.Count == MessageLimit
																&& tweet.IncludeEntities == true
															 select tweet;


										if (twitterStatusQuery.Count() > 0)
										{
											results.AddRange(twitterStatusQuery.Distinct(new CustomStatusComparer()));
										}
									}

									pageIndex++;
									page = accounts.Skip(pageIndex * pageSize).Take(pageSize);
								}

								refreshUrl = "?q=" + string.Join("%20OR%20", accountsFrom);

								// do distinct, reorder and take MaxItemCount
								results = results.Distinct(new CustomStatusComparer()).OrderByDescending(a => a.CreatedAt).Take(MessageLimit).ToList();
								break;

							case TwitterFeed.TwitterSearchType.HashTags:
								//searching added through settings has \n instead of newline as line break;
								var hashTags = SearchString.Replace(Environment.NewLine, "\n").Split(new string[] { ";", "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(tag => tag.StartsWith("#") ? tag : "#" + tag);
								query = string.Join(" OR ", hashTags);
								DateTime untilDate = DateTime.Now.ToUniversalTime();
								twitterQuery = from search in twitterContext.Search
												where search.Type == LinqToTwitter.SearchType.Search
														&& search.Query == query
														&& search.ResultType == ResultType.Mixed
														&& search.Count == MessageLimit
														&& search.IncludeEntities == true
													   // && search.Statuses.Any(o => o.Type == StatusType.User) //&& search.ShowUser == true
												select search;

								srch = twitterQuery.FirstOrDefault();
								if (srch != null)
								{
									refreshUrl = srch.SearchMetaData.RefreshUrl;
									results.AddRange(srch.Statuses.Distinct(new CustomStatusComparer()));
								}

								break;

							case TwitterFeed.TwitterSearchType.Custom:
								twitterQuery = from search in twitterContext.Search
												where search.Type == LinqToTwitter.SearchType.Search
														&& search.Query == SearchString
														&& search.ResultType == ResultType.Mixed
														&& search.Count == MessageLimit
														&& search.IncludeEntities == true
														//&& search.Statuses.Any(o => o.Type == StatusType.User) //&& search.ShowUser == true
												select search;

								srch = twitterQuery.FirstOrDefault();
								if (srch != null)
								{
									refreshUrl = srch.SearchMetaData.RefreshUrl;
									results.AddRange(srch.Statuses.Distinct(new CustomStatusComparer()));
								}

								break;

							default:
								break;
						}

						if (results.Any())
						{
							flatDataList = results.Select(tweet => new TwitterFeedItem()
															{
																Name = tweet.User.Name,
																User = tweet.User.Identifier.ScreenName,
																Message = tweet.Text,
																ImageUrl = tweet.User.ProfileImageUrl,
																CreatedAt = tweet.CreatedAt,
																Url = BaseUrl + tweet.User.Identifier.ScreenName
															}).ToList();
						}

					}
				}
				catch(Exception)
				{
				}

				if (flatDataList != null && flatDataList.Any())
				{
					// Set datasource to repeater
					rprTwitterFeedList.DataSource = flatDataList.OrderByDescending(i => i.CreatedAt.DateTime).Take(MessageLimit);
					rprTwitterFeedList.DataBind();

					if (this.ShowReadMore)
					{
						hlLinkToTwitterSearch.NavigateUrl = SearchBaseUrl + refreshUrl;
						hlLinkToTwitterSearch.Text = !String.IsNullOrWhiteSpace(ReadMoreText) ? ReadMoreText : ResourceManager.GetString("ReadMore");
					}
					else
					{
						phrReadMore.Visible = false;
					}

				}
				else
				{
					this.Visible = false;
				}
			}
			else
			{
				this.Controls.Clear();
			}
		}


        /// <summary>
        /// Handles repeat event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		void rprTwitterFeedList_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				TwitterFeedItem item = e.Item.DataItem as TwitterFeedItem;
				
				Literal ltlTwitterFeedMessageDate = e.Item.FindControl("ltlTwitterFeedMessageDate") as Literal;
				if (ltlTwitterFeedMessageDate != null)
				{
					ltlTwitterFeedMessageDate.Text = item.CreatedAt.PrettyDateTodayExpanded();
				}

				Literal ltlTwitterFeedMessage = e.Item.FindControl("ltlTwitterFeedMessage") as Literal;
				if (ltlTwitterFeedMessage != null)
				{
					string message = HttpUtility.HtmlDecode(item.Message).TextAsHtml(BaseUrl);
					ltlTwitterFeedMessage.Text = message;
				}

				//Image imgTwitterFeedImage = e.Item.FindControl("imgTwitterFeedImage") as Image;
				var ltlTwitterFeedImage = e.Item.FindControl("ltlTwitterFeedImage") as Literal;
				if (ltlTwitterFeedImage != null)
				{
					ltlTwitterFeedImage.Text = "<a href='"+item.Url+"' rel='external'><img src='" + item.ImageUrl + "' alt='' /></a>";
					//imgTwitterFeedImage.ImageUrl = item.ImageUrl;
					//imgTwitterFeedImage.AlternateText = item.Name;
					//imgTwitterFeedImage.Attributes["title"] = item.Name;
				}
			}
		}
        #endregion
             
        internal class CustomSearchEntryComparer : IEqualityComparer<SearchEntry>
		{

			#region IEqualityComparer<SearchEntry> Members

			public bool Equals(SearchEntry x, SearchEntry y)
			{
				return x.ID == y.ID;
			}

			public int GetHashCode(SearchEntry obj)
			{
				return obj.ID.GetHashCode();
			}

			#endregion

		}
		internal class CustomStatusComparer : IEqualityComparer<Status>
		{

			#region IEqualityComparer<Status> Members

			public bool Equals(Status x, Status y)
			{
				return x.StatusID == y.StatusID;
			}

			public int GetHashCode(Status obj)
			{
				return obj.StatusID.GetHashCode();
			}

			#endregion

		}
		
        /// <summary>
        /// Available Twitter search types.
        /// </summary>
		public enum TwitterSearchType
		{
			Accounts,
			HashTags,
			Custom
		}

        /// <summary>
        /// Class that holds Tweet information.
        /// </summary>
		internal class TwitterFeedItem
		{
			public TwitterFeedItem()
			{
			}

			public String Name { get; set; }
			public String User { get; set; }
			public String Url { get; set; }
			public String Message { get; set; }
			public String ImageUrl { get; set; }
			public DateTimeOffset CreatedAt { get; set; }
		}

		public static readonly string layoutTemplateName = "Fenetre.Sitefinity.TwitterFeed.Widgets.TwitterFeed.ascx";
	}
}