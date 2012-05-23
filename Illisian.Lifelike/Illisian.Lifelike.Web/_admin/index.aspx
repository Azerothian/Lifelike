<%@ Page Title="" Language="C#" MasterPageFile="~/_admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="index.aspx.cs" Inherits="Illisian.Lifelike._admin.index" %>

<%@ Register TagPrefix="uc" TagName="ContentManager" Src="~/_admin/controls/ctlContentManager.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <ext:Desktop ID="dtMain" runat="server">
        <Modules>
            <ext:DesktopModule ModuleID="dmContentEditor" AutoRun="true">
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
            </ext:DesktopModule>
        </Modules>
    </ext:Desktop>
</asp:Content>
