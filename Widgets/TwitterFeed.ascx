<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TwitterFeed.ascx.cs" Inherits="Fenetre.Sitefinity.TwitterFeed" %>
<asp:PlaceHolder ID="phrTwitterFeed" runat="server" >
	<div class="TwitterFeed">
		<span class="Title"><asp:Literal ID="ltlHeader" runat="server" /></span>
		<asp:Repeater ID="rprTwitterFeedList" runat="server">
			<ItemTemplate>
				<div class="TwitterItem">
					<div class="TwitterFeedImage">
						<asp:literal ID="ltlTwitterFeedImage" runat="server" />
					</div>
					<div class="TwitterFeedMessage">
						<asp:Literal ID="ltlTwitterFeedMessage" runat="server" />
					</div>
					<div class="TwitterFeedMessageDate">
						<asp:Literal ID="ltlTwitterFeedMessageDate" runat="server" />
					</div>
				</div>
			</ItemTemplate>
			<SeparatorTemplate><hr /></SeparatorTemplate>
		</asp:Repeater>
		
		<asp:PlaceHolder ID="phrReadMore" runat="server">
			<div class="ReadMore">
				<asp:HyperLink class="aLinkToTwitter" rel="external" runat="server" ID="hlLinkToTwitterSearch"></asp:HyperLink>
			</div>
		</asp:PlaceHolder>
	</div>
</asp:PlaceHolder>
