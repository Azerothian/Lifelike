<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlSiteManager.ascx.cs"
    Inherits="Illisian.Lifelike._admin.controls.ctlSiteManager" %>
<ext:GridPanel runat="server" ID="gpSites" Title="Sites">
    <Store>
        <ext:Store ID="storeSites" runat="server">
            <Model>
                <ext:Model ID="modelSites" runat="server" IDProperty="Id">
                    <Fields>
                        <ext:ModelField Name="Id" />
                        <ext:ModelField Name="SiteName" />
                        <ext:ModelField Name="HostName" />
                        <ext:ModelField Name="StartItem" />
                    </Fields>
                </ext:Model>
            </Model>
        </ext:Store>
    </Store>
    <ColumnModel ID="columnSites" runat="server">
        <Columns>
            <ext:Column ID="colSiteName" runat="server" DataIndex="SiteName" Text="Site Name">
                <Editor>
                    <ext:TextField ID="tfSiteName" runat="server" />
                </Editor>
            </ext:Column>
            <ext:Column ID="colHostName" runat="server" DataIndex="HostName" Text="Host Name">
                <Editor>
                    <ext:TextField ID="tfHostName" runat="server" />
                </Editor>
            </ext:Column>
            <ext:Column ID="colStartItem" runat="server" DataIndex="StartItem" Text="Start Name"
                Flex="1">
                <Editor>
                    <ext:TextField ID="tfStartItem" runat="server" />
                </Editor>
            </ext:Column>
        </Columns>
    </ColumnModel>
    <View>
        <ext:GridView runat="server" />
    </View>
    <SelectionModel>
        <ext:CellSelectionModel runat="server" />
    </SelectionModel>
    <Plugins>
        <ext:CellEditing ID="CellEditing1" runat="server">
            <Listeners>
                <Edit Handler="#{DirectMethods}.EditRow(e.record.data.Id, e.field, e.originalValue, e.value, e.record.data)" />
            </Listeners>
        </ext:CellEditing>
    </Plugins>
</ext:GridPanel>
