<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlContentManager.ascx.cs"
    Inherits="Illisian.Lifelike.__.admin.controls.ctlContentManager" %>
<ext:TabPanel ID="TabPanel1" runat="server" Region="West" Width="250">
    <Items>
        <ext:TreePanel ID="tpContents" runat="server" Title="Sites" RootVisible="false">
            <DirectEvents>
                <ItemClick OnEvent="OnClick_tpContent">
                    <ExtraParams>
                        <ext:Parameter Name="guid" Value="node.id" Mode="Raw" />
                    </ExtraParams>
                </ItemClick>
            </DirectEvents>
            <TopBar>
                <ext:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <ext:ToolbarTextItem ID="ToolbarTextItem1" runat="server" Text="Create:" />
                        <ext:ToolbarSpacer />
                        <ext:TextField ID="txtNewName" runat="server" />
                        <ext:Button ID="btnCreate" runat="server" Text="Create">
                            <DirectEvents>
                                <Click OnEvent="OnClick_btnCreate">
                                    <ExtraParams>
                                        <ext:Parameter Name="node" Value="#{tpContents}.getSelectionModel().getSelectedNode()"
                                            Mode="Raw" Encode="true" />
                                    </ExtraParams>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <Store>
                <ext:TreeStore ID="TreeStore1" runat="server" OnReadData="NodeLoad">
                    <Proxy>
                        <ext:PageProxy />
                    </Proxy>
                    <Parameters>
                        <%--                        <ext:StoreParameter Name="guid" Value="#{TextField1}.getValue()" Mode="Raw" />
                        <ext:StoreParameter Name="parent" Value="#{TreeStore1}.getValue()" Mode="Raw" />--%>
                    </Parameters>
                </ext:TreeStore>
            </Store>
        </ext:TreePanel>
        <ext:TreePanel ID="tpControls" runat="server" Title="Controls">
        </ext:TreePanel>
        <ext:TreePanel ID="tpTemplates" runat="server" Title="Templates">
        </ext:TreePanel>
    </Items>
</ext:TabPanel>
<ext:Panel ID="pnlCentre" runat="server" Title="" Region="Center" Collapsible="false"
    Layout="FitLayout">
    <Items>
    </Items>
</ext:Panel>
