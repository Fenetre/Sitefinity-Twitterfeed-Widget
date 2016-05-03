Type.registerNamespace("Fenetre.Sitefinity.TwitterFeed");

Fenetre.Sitefinity.TwitterFeed.TwitterFeedDesigner = function (element) {
	this._headerTitle = null;
	this._searchString = null;
	this._searchStringMultiLine = null;
	this._messageLimit = null;
	this._showReadMore = null;
	this._searchType = null;
	this._isOnAuthorDetailpage = null;
	this._twitterConsumerKey = null;
	this._twitterConsumerSecret = null;
	this._twitterUserAccessToken = null;
	this._twitterUserAccessTokenSecret = null;

	Fenetre.Sitefinity.TwitterFeed.TwitterFeedDesigner.initializeBase(this, [element]);
}

Fenetre.Sitefinity.TwitterFeed.TwitterFeedDesigner.prototype = {
	/* --------------------------------- set up and tear down --------------------------------- */
	initialize: function () {
		/* Here you can attach to events or do other initialization */
		Fenetre.Sitefinity.TwitterFeed.TwitterFeedDesigner.callBaseMethod(this, 'initialize');
		this._toggleBasicSettingsDelegate = Function.createDelegate(this, function () {
			dialogBase.resizeToContent();
		});

		this._toggleTwitterApiSettingsDelegate = Function.createDelegate(this, function () {
			dialogBase.resizeToContent();
		});

		//jQuery("#expanderBasicSettings").click(this._toggleDesignSettingsDelegate);
		jQuery("#basicSettingsExp a").click(this._toggleBasicSettingsDelegate).click(function () {
			$(this).parents(".sfExpandableSection:first").toggleClass("sfExpandedSection");
			dialogBase.resizeToContent();
		});
		jQuery("#twitterApiSettingsExp a").click(this._toggleTwitterApiSettingsDelegate).click(function () {
			$(this).parents(".sfExpandableSection:first").toggleClass("sfExpandedSection");
			dialogBase.resizeToContent();
		});
	},
	dispose: function () {
		/* this is the place to unbind/dispose the event handlers created in the initialize method */
		Fenetre.Sitefinity.TwitterFeed.TwitterFeedDesigner.callBaseMethod(this, 'dispose');
	},

	/* --------------------------------- public methods ---------------------------------- */

	findElement: function (id) {
		var result = jQuery(this.get_element()).find("#" + id).get(0);
		return result;
	},

	/* Called when the designer window gets opened and here is place to "bind" your designer to the control properties */
	refreshUI: function () {
		var controlData = this._propertyEditor.get_control(); /* JavaScript clone of your control - all the control properties will be properties of the controlData too */

		/* RefreshUI HeaderTitle */
		jQuery(this.get_headerTitle()).val(controlData.HeaderTitle);

		if (this.get_isOnAuthorDetailpage() !== true) {
			var $searchString = jQuery(this.get_searchString());
			var $searchStringMultiLine = jQuery(this.get_searchStringMultiLine());
			var $sfExample = $searchString.siblings('.sfExample');
			var mlExample = function (type) { return "Enter multiple " + (type === "Accounts" ? "accounts" : "hashtags") + " <b>separated by a ; (semi kolon)</b> or <b>each on a new line</b>"; };
			var custExample = "Enter your custom query, for examples see <a href='https://dev.twitter.com/docs/using-search'>https://dev.twitter.com/docs/using-search</a>";

			/* RefreshUI SearchType */
			jQuery(this.get_searchType()).val(controlData.SearchType).change(function () {
				var val = jQuery(this).val();
				if (val === 'Custom') {
					$searchStringMultiLine.hide();
					$searchString.show();
					$sfExample.html(custExample);
				}
				else {
					$searchString.hide();
					$searchStringMultiLine.show();
					$sfExample.html(mlExample(val));
				}
			});

			if (controlData.SearchType === 'Custom') {
				/* RefreshUI SearchString */
				$searchString.val(controlData.SearchString);
				$searchStringMultiLine.hide();
				$sfExample.html(custExample);
			}
			else {
				/* RefreshUI SearchStringMultiLine */
				$searchStringMultiLine.val(controlData.SearchString);
				$searchString.hide();
				$sfExample.html(mlExample(controlData.SearchType));
			}
		}
		else {

		}
		/* RefreshUI MessageLimit */
		jQuery(this.get_messageLimit()).val(controlData.MessageLimit);
		jQuery(this.get_readMoreText()).val(controlData.ReadMoreText);
		/* RefreshUI ShowReadMore */
		jQuery(this.get_showReadMore()).attr("checked", controlData.ShowReadMore);
		jQuery(this.get_twitterConsumerKey()).val(controlData.TwitterConsumerKey);
		jQuery(this.get_twitterConsumerSecret()).val(controlData.TwitterConsumerSecret);
		jQuery(this.get_twitterUserAccessToken()).val(controlData.TwitterUserAccessToken);
		jQuery(this.get_twitterUserAccessTokenSecret()).val(controlData.TwitterUserAccessTokenSecret);
	},

	/* Called when the "Save" button is clicked. Here you can transfer the settings from the designer to the control */
	applyChanges: function () {
		var controlData = this._propertyEditor.get_control();

		controlData.HeaderTitle = jQuery(this.get_headerTitle()).val();

		if (this.get_isOnAuthorDetailpage() !== true) {
			controlData.SearchType = jQuery(this.get_searchType()).val();
			if (controlData.SearchType === 'Custom') {
				controlData.SearchString = jQuery(this.get_searchString()).val();
			}
			else {
				controlData.SearchString = jQuery(this.get_searchStringMultiLine()).val();
			}
		}
		controlData.MessageLimit = jQuery(this.get_messageLimit()).val();
		controlData.ReadMoreText = jQuery(this.get_readMoreText()).val();
		controlData.ShowReadMore = jQuery(this.get_showReadMore()).is(":checked");
		controlData.TwitterConsumerKey = jQuery(this.get_twitterConsumerKey()).val();
		controlData.TwitterConsumerSecret = jQuery(this.get_twitterConsumerSecret()).val();
		controlData.TwitterUserAccessToken = jQuery(this.get_twitterUserAccessToken()).val();
		controlData.TwitterUserAccessTokenSecret = jQuery(this.get_twitterUserAccessTokenSecret()).val();
	},


	/* --------------------------------- properties -------------------------------------- */

	/* HeaderTitle properties */
	get_headerTitle: function () { return this._headerTitle; },
	set_headerTitle: function (value) { this._headerTitle = value; },

	/* SearchString properties */
	get_searchString: function () { return this._searchString; },
	set_searchString: function (value) { this._searchString = value; },

	/* SearchStringMultiLine properties */
	get_searchStringMultiLine: function () { return this._searchStringMultiLine; },
	set_searchStringMultiLine: function (value) { this._searchStringMultiLine = value; },

	/* MessageLimit properties */
	get_messageLimit: function () { return this._messageLimit; },
	set_messageLimit: function (value) { this._messageLimit = value; },

	/* ReadMoreText properties */
	get_readMoreText: function () { return this._readMoreText; },
	set_readMoreText: function (value) { this._readMoreText = value; },

	/* ShowReadMore properties */
	get_showReadMore: function () { return this._showReadMore; },
	set_showReadMore: function (value) { this._showReadMore = value; },

	/* SearchType properties */
	get_searchType: function () { return this._searchType; },
	set_searchType: function (value) { this._searchType = value; },

	get_isOnAuthorDetailpage: function () { return this._isOnAuthorDetailpage; },
	set_isOnAuthorDetailpage: function (value) { this._isOnAuthorDetailpage = value; },

	get_twitterConsumerKey: function () { return this._twitterConsumerKey; },
	set_twitterConsumerKey: function (value) { this._twitterConsumerKey = value; },

	get_twitterConsumerSecret: function () { return this._twitterConsumerSecret; },
	set_twitterConsumerSecret: function (value) { this._twitterConsumerSecret = value; },

	get_twitterUserAccessToken: function () { return this._twitterUserAccessToken; },
	set_twitterUserAccessToken: function (value) { this._twitterUserAccessToken = value; },

	get_twitterUserAccessTokenSecret: function () { return this._twitterUserAccessTokenSecret; },
	set_twitterUserAccessTokenSecret: function (value) { this._twitterUserAccessTokenSecret = value; }
}

Fenetre.Sitefinity.TwitterFeed.TwitterFeedDesigner.registerClass('Fenetre.Sitefinity.TwitterFeed.TwitterFeedDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);

