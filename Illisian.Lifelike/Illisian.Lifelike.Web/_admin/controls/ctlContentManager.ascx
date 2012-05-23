<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlContentManager.ascx.cs"
    Inherits="Illisian.Lifelike._admin.controls.ctlContentManager" %>
<ext:TabPanel ID="TabPanel1" runat="server" Region="West" Width="250">
    <Items>
        <ext:TreePanel ID="tpContents" runat="server" Title="Sites" RootVisible="false">
            <Store>
                <ext:TreeStore ID="TreeStore1" runat="server" OnReadData="NodeLoad">
                    <Proxy>
                        <ext:PageProxy />
                    </Proxy>
                    <Parameters>
                        <%-- <ext:StoreParameter Name="guid" Value="#{TextField1}.getValue()" Mode="Raw" />--%>
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
