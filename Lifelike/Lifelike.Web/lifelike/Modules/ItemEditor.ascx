<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemEditor.ascx.cs" Inherits="Lifelike.WebAdmin.lifelike.Modules.ItemEditor" %>
<div class="itemEditor">
	<div class="tree">

		<telerik:RadTreeView ID="rtvEntities" runat="server">
			<WebServiceSettings Path="/admin/windows/itemeditor" Method="GetNodes">
			</WebServiceSettings>
		</telerik:RadTreeView>

		<%--		<telerik:RadPanelBar runat="server" ID="pnlData" Width="250" Height="450">
			<Items>
				<telerik:RadPanelItem Expanded="true" CssClass="items">
					<HeaderTemplate>
						&nbsp;Items
					</HeaderTemplate>
					<ContentTemplate>
						
					</ContentTemplate>
				</telerik:RadPanelItem>
				<telerik:RadPanelItem CssClass="domains">
					<HeaderTemplate>
						&nbsp;Domains
					</HeaderTemplate>
					<ContentTemplate>
						<telerik:RadTreeView ID="rtvDomains" runat="server">
						</telerik:RadTreeView>
					</ContentTemplate>
				</telerik:RadPanelItem>
				<telerik:RadPanelItem CssClass="layouts">
					<HeaderTemplate>
						&nbsp;Layouts
					</HeaderTemplate>
					<ContentTemplate>
						<telerik:RadTreeView ID="rtvLayouts" runat="server">
						</telerik:RadTreeView>
					</ContentTemplate>
				</telerik:RadPanelItem>
				<telerik:RadPanelItem CssClass="modules">
					<HeaderTemplate>
						&nbsp;Modules
					</HeaderTemplate>
					<ContentTemplate>
						<telerik:RadTreeView ID="rtvModules" runat="server">
						</telerik:RadTreeView>
					</ContentTemplate>
				</telerik:RadPanelItem>
				<telerik:RadPanelItem CssClass="views">
					<HeaderTemplate>
						&nbsp;Views
					</HeaderTemplate>
					<ContentTemplate>
						<telerik:RadTreeView ID="rtvViews" runat="server">
						</telerik:RadTreeView>
					</ContentTemplate>
				</telerik:RadPanelItem>
			</Items>
		</telerik:RadPanelBar>--%>
	</div>

</div>
