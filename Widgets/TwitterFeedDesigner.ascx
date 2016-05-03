<%@ Control %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sfFields" Namespace="Telerik.Sitefinity.Web.UI.Fields" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="designers" Namespace="Telerik.Sitefinity.Web.UI.ControlDesign" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Modules.Pages.Web.UI" TagPrefix="sitefinity" %>

<sitefinity:ResourceLinks ID="resourcesLinks" runat="server">
    <sitefinity:ResourceFile JavaScriptLibrary="JQuery" />
	<sitefinity:ResourceFile Name="Styles/Ajax.css" />
    <sitefinity:ResourceFile Name="Styles/Grid.css" />
    <sitefinity:ResourceFile Name="Styles/ToolBar.css" />
</sitefinity:ResourceLinks>
<div class="sfContentViews">
	 <div id="basicSettings">
        <div id="basicSettingsExp" class="sfExpandableSection">
            <h3>
                <a id="expanderBasicSettings" href="javascript:void(0);" class="sfMoreDetails">Basic settings</a></h3>
			<ul class="sfTargetList">
				<li class="sfFormCtrl">
					<asp:Label runat="server" AssociatedControlID="HeaderTitle" CssClass="sfTxtLbl">Title</asp:Label>
					<asp:TextBox ID="HeaderTitle" runat="server" CssClass="sfTxt" />
					<div class="sfExample">Title shown above tweets (optional)</div>
				</li>

				<li class="sfFormCtrl">
					<asp:Label runat="server" AssociatedControlID="SearchType" CssClass="sfTxtLbl">Search on</asp:Label>
					<asp:DropDownList runat="server" ID="SearchType">
						<asp:ListItem Value="Accounts">Accounts</asp:ListItem>
						<asp:ListItem Value="HashTags">HashTags</asp:ListItem>
						<asp:ListItem Value="Custom">Custom</asp:ListItem>
					</asp:DropDownList>
					<div class="sfExample"></div>
				</li>
				<li class="sfFormCtrl">
					<asp:Label runat="server" AssociatedControlID="SearchString" CssClass="sfTxtLbl">Search query</asp:Label>
					<asp:TextBox ID="SearchString" runat="server" CssClass="sfTxt" />
					<asp:TextBox ID="SearchStringMultiLine" runat="server" TextMode="MultiLine" Rows="8" CssClass="sfTxt" />
					<div class="sfExample"></div>
				</li>

				<li class="sfFormCtrl">
					<asp:Label runat="server" AssociatedControlID="MessageLimit" CssClass="sfTxtLbl">Show</asp:Label>
					<asp:TextBox ID="MessageLimit" runat="server" CssClass="sfTxt" />
					<div class="sfExample">How many tweets are shown</div>
				</li>

				<li class="sfFormCtrl">
					<asp:CheckBox runat="server" ID="ShowReadMore" Text="Show 'Show more'" CssClass="sfCheckBox"/>
					<div class="sfExample">Show 'Show more' link</div>
				</li>

				<li class="sfFormCtrl">
					<asp:Label runat="server" AssociatedControlID="ReadMoreText" CssClass="sfTxtLbl">'Show more' link text</asp:Label>
					<asp:TextBox ID="ReadMoreText" runat="server" CssClass="sfTxt" />
					<div class="sfExample">Text of 'Show more' link</div>
				</li>
			</ul>
		</div>
	 </div>
	<div id="twitterApiSettings">
        <div id="twitterApiSettingsExp" class="sfExpandableSection">
            <h3>
                <a id="expanderTwitterApiSettings" href="javascript:void(0);" class="sfMoreDetails">Twitter API settings</a>
			</h3>
			<ul class="sfTargetList">
				<li class="sfFormCtrl">
					<asp:Label runat="server" AssociatedControlID="TwitterConsumerKey" CssClass="sfTxtLbl">Twitter consumer key</asp:Label>
					<asp:TextBox ID="TwitterConsumerKey" runat="server" CssClass="sfTxt" />
					<div class="sfExample">Consumer key used for the Twitter API</div>
				</li>
				<li class="sfFormCtrl">
					<asp:Label runat="server" AssociatedControlID="TwitterConsumerSecret" CssClass="sfTxtLbl">Twitter consumer secret</asp:Label>
					<asp:TextBox ID="TwitterConsumerSecret" runat="server" CssClass="sfTxt" />
					<div class="sfExample">Consumer secret used for the Twitter API</div>
				</li>
				<li class="sfFormCtrl">
					<asp:Label runat="server" AssociatedControlID="TwitterUserAccessToken" CssClass="sfTxtLbl">Twitter user access token</asp:Label>
					<asp:TextBox ID="TwitterUserAccessToken" runat="server" CssClass="sfTxt" />
					<div class="sfExample">User access token for the Twitter API</div>
				</li>
				<li class="sfFormCtrl">
					<asp:Label runat="server" AssociatedControlID="TwitterUserAccessTokenSecret" CssClass="sfTxtLbl">Twitter user access token secret</asp:Label>
					<asp:TextBox ID="TwitterUserAccessTokenSecret" runat="server" CssClass="sfTxt" />
					<div class="sfExample">User access token secret for the Twitter API</div>
				</li>
			</ul>
		</div>
	</div>
</div>
