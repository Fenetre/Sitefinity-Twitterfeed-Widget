using System;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Fenetre.Sitefinity.TwitterFeed
{
    /// <summary>
    /// Represents a designer for the <typeparamref name="Mejudice.Website.Web.TwitterFeed"/> widget
    /// </summary>
    public class TwitterFeedDesigner : ControlDesignerBase
    {
        #region Properties
        /// <summary>
        /// Gets the layout template path
        /// </summary>
        public override string LayoutTemplatePath
        {
            get
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Gets the layout template name
        /// </summary>
        protected override string LayoutTemplateName
        {
            get
            {
				return TwitterFeedDesigner.layoutTemplateName;
            }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }
        #endregion

        #region Control references
        /// <summary>
        /// Gets the control that is bound to the HeaderTitle property
        /// </summary>
        protected virtual Control HeaderTitle
        {
            get
            {
                return this.Container.GetControl<Control>("HeaderTitle", true);
            }
        }
		/// <summary>
		/// Gets the control that is bound to the SearchType property
		/// </summary>
		protected virtual DropDownList SearchType
		{
			get
			{
				return this.Container.GetControl<DropDownList>("SearchType", true);
			}
		}

        /// <summary>
        /// Gets the control that is bound to the SearchString property
        /// </summary>
        protected virtual Control SearchString
        {
            get
            {
                return this.Container.GetControl<Control>("SearchString", true);
            }
        }
		protected virtual Control SearchStringMultiLine
		{
			get
			{
				return this.Container.GetControl<Control>("SearchStringMultiLine", true);
			}
		}

        /// <summary>
        /// Gets the control that is bound to the MessageLimit property
        /// </summary>
        protected virtual Control MessageLimit
        {
            get
            {
                return this.Container.GetControl<Control>("MessageLimit", true);
            }
        }

        /// <summary>
		/// Gets the control that is bound to the ReadMoreText property
        /// </summary>
		protected virtual Control ReadMoreText
        {
            get
            {
				return this.Container.GetControl<Control>("ReadMoreText", true);
            }
        }

        /// <summary>
        /// Gets the control that is bound to the ShowReadMore property
        /// </summary>
        protected virtual Control ShowReadMore
        {
            get
            {
                return this.Container.GetControl<Control>("ShowReadMore", true);
            }
        }

        /// <summary>
        /// Token used to access Twitter
        /// </summary>
		protected virtual Control TwitterUserAccessToken
		{
			get
			{
				return this.Container.GetControl<Control>("TwitterUserAccessToken", true);
			}
		}

        /// <summary>
        /// Secret token used to access Twitter
        /// </summary>
		protected virtual Control TwitterUserAccessTokenSecret
		{
			get
			{
				return this.Container.GetControl<Control>("TwitterUserAccessTokenSecret", true);
			}
		}

        /// <summary>
        /// Consumer key used to access Twitter
        /// </summary>
		protected virtual Control TwitterConsumerKey
		{
			get
			{
				return this.Container.GetControl<Control>("TwitterConsumerKey", true);
			}
		}

        /// <summary>
        /// Consumer secret used to access Twitter
        /// </summary>
		protected virtual Control TwitterConsumerSecret
		{
			get
			{
				return this.Container.GetControl<Control>("TwitterConsumerSecret", true);
			}
		}

        #endregion

        #region Methods
        /// <summary>
        /// Initializes the Twitter feed.
        /// </summary>
        /// <param name="container"></param>
        protected override void InitializeControls(Telerik.Sitefinity.Web.UI.GenericContainer container)
        {
			SearchType.Items.Clear();
			SearchType.Items.Add(new ListItem("Accounts", "Accounts"));
			SearchType.Items.Add(new ListItem("HashTags", "HashTags"));
			SearchType.Items.Add(new ListItem("Custom", "Custom"));
        }
        #endregion

        #region IScriptControl implementation
        /// <summary>
        /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
        /// </summary>
        public override System.Collections.Generic.IEnumerable<System.Web.UI.ScriptDescriptor> GetScriptDescriptors()
        {
            var scriptDescriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
            var descriptor = (ScriptControlDescriptor)scriptDescriptors.Last();

            descriptor.AddElementProperty("headerTitle", this.HeaderTitle.ClientID);
            descriptor.AddElementProperty("searchString", this.SearchString.ClientID);
            descriptor.AddElementProperty("messageLimit", this.MessageLimit.ClientID);
			descriptor.AddElementProperty("showReadMore", this.ShowReadMore.ClientID);
			descriptor.AddElementProperty("searchType", this.SearchType.ClientID);
			descriptor.AddElementProperty("searchStringMultiLine", this.SearchStringMultiLine.ClientID);
			descriptor.AddElementProperty("twitterConsumerKey", this.TwitterConsumerKey.ClientID);
			descriptor.AddElementProperty("twitterConsumerSecret", this.TwitterConsumerSecret.ClientID);
			descriptor.AddElementProperty("twitterUserAccessToken", this.TwitterUserAccessToken.ClientID);
			descriptor.AddElementProperty("twitterUserAccessTokenSecret", this.TwitterUserAccessTokenSecret.ClientID);
			descriptor.AddElementProperty("readMoreText", this.ReadMoreText.ClientID);

            return scriptDescriptors;
        }

        /// <summary>
        /// Gets a collection of ScriptReference objects that define script resources that the control requires.
        /// </summary>
        public override System.Collections.Generic.IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>(base.GetScriptReferences());
            scripts.Add(new ScriptReference(TwitterFeedDesigner.scriptReference, "Fenetre.Sitefinity.TwitterFeed"));
            return scripts;
        }
        #endregion

        #region Private members & constants
		public static readonly string layoutTemplateName = "Fenetre.Sitefinity.TwitterFeed.Widgets.TwitterFeedDesigner.ascx";
		public const string scriptReference = "Fenetre.Sitefinity.TwitterFeed.Widgets.TwitterFeedDesigner.js";

        #endregion
    }
}
 
