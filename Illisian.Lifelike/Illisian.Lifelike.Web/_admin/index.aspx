<%@ Page Title="" Language="C#" MasterPageFile="~/_admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="index.aspx.cs" Inherits="Illisian.Lifelike._admin.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <ext:Desktop ID="dtMain" runat="server">
        <Modules>
            <ext:DesktopModule ModuleID="dmContentEditor" AutoRun="true">
                <Window>
                    <ext:Window ID="winContent" runat="server" Width="600" Height="400" Title="Content Manager"
                        Maximizable="true" Minimizable="true" Layout="BorderLayout">
                        <Items>
                            <ext:Panel ID="Panel2" runat="server" Title="Items" Region="West" BodyPadding="6"
                                Collapsible="false" Width="200" />
                            <ext:Panel ID="Panel4" runat="server" Title="" Region="Center" Collapsible="false" Layout="BorderLayout">
                                <Items>
                                    <ext:Panel ID="Panel1" runat="server" Title="" Region="North" BodyPadding="6" Height="75"
                                        Collapsible="false" />
                                    <ext:Panel ID="Panel3" runat="server" Title="Details" Region="Center" BodyPadding="6"
                                        Html="" Collapsible="false" />
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Window>
                </Window>
                <Shortcut Name="Content Manager" SortIndex="3" />
                <Launcher Text="Content Manager" />
            </ext:DesktopModule>
        </Modules>
    </ext:Desktop>
</asp:Content>
