<%@ Page Title="" Language="C#" MasterPageFile="~/_admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="index.aspx.cs" Inherits="Illisian.Lifelike._admin.index" %>
<%@ Register TagPrefix="uc" TagName="ContentManager" Src="~/_admin/controls/ctlContentManager.ascx" %>
<%@ Register TagPrefix="uc" TagName="DomainManager" Src="~/_admin/controls/ctlDomainManager.ascx" %>
<%@ Register TagPrefix="uc" TagName="LanguageManager" Src="~/_admin/controls/ctlLanguageManager.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <ext:Desktop ID="dtMain" runat="server">
        <Modules>
<%--            <ext:DesktopModule ModuleID="dmContentEditor" AutoRun="true">
                <Window>
                    <ext:Window ID="winContent" runat="server" Width="600" Height="400" Title="Content Manager"
                        Maximizable="true" Minimizable="true" Layout="BorderLayout">
                        <Content>
                            <uc:ContentManager ID="ucContentManager" runat="server" />
                        </Content>
                    </ext:Window>
                </Window>
                <Shortcut Name="Content Manager" SortIndex="3" />
                <Launcher Text="Content Manager" />
            </ext:DesktopModule>--%>
<%--             <ext:DesktopModule ModuleID="dmDomainManager">
                <Window>
                    <ext:Window ID="winDomainManager" runat="server" Width="600" Height="400" Title="Domain Manager"
                        Maximizable="true" Minimizable="true" Layout="BorderLayout">
                        <Content>
                            <uc:DomainManager ID="ucDomainManager" runat="server" />
                        </Content>
                    </ext:Window>
                </Window>
                <Shortcut Name="Domain Manager" SortIndex="3" />
                <Launcher Text="Domain Manager" />
            </ext:DesktopModule>--%>
                         <ext:DesktopModule ModuleID="dmLanguageManager">
                <Window>
                    <ext:Window ID="winLanguageManager" runat="server" Width="600" Height="400" Title="Language Manager"
                        Maximizable="true" Minimizable="true" Layout="FitLayout">
                        <Content>
                            <uc:LanguageManager ID="ucLanguageManager" runat="server" />
                        </Content>
                    </ext:Window>
                </Window>
                <Shortcut Name="Language Manager" SortIndex="3" />
                <Launcher Text="Language Manager" />
            </ext:DesktopModule>
        </Modules>
    </ext:Desktop>


</asp:Content>
