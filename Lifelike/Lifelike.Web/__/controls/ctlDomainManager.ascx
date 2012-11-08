<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlDomainManager.ascx.cs" 
	Inherits="Lifelike.WebAdmin.__.controls.ctlDomainManager" %>
<ext:Panel ID="Panel1" runat="server" Layout="FitLayout">
    <TopBar>
        <ext:Toolbar ID="tbDomain" runat="server">
            <Items>
                <ext:Button ID="btnAddDomain" runat="server" Text="Add" IconCls="add16" />
                <ext:Button ID="btnEditDomain" runat="server" Text="Edit" IconCls="edit16" Disabled="true" />
                <ext:Button ID="btnDeleteDomain" runat="server" Text="Delete" IconCls="delete16" Disabled="true" />
            </Items>
        </ext:Toolbar>
    </TopBar>
    <Items>
        <ext:GridPanel runat="server" ID="gpDomains">
            <Store>
                <ext:Store ID="storeDomains" runat="server">
                    <Model>
                        <ext:Model ID="modelDomains" runat="server" IDProperty="Id">
                            <Fields>
                                <ext:ModelField Name="Id" Type="Int" />
                                <ext:ModelField Name="Name" Type="String" />
                                <ext:ModelField Name="Code" Type="String" />
                            </Fields>
                        </ext:Model>
                    </Model>
                </ext:Store>
            </Store>
            <ColumnModel ID="columnDomains" runat="server">
                <Columns>
                    <ext:Column ID="colName" runat="server" DataIndex="Name" Text="Name">
                        <Editor>
                            <ext:TextField ID="tfName" runat="server" />
                        </Editor>
                    </ext:Column>
                    <ext:Column ID="colCode" runat="server" DataIndex="Code" Text="Code">
                        <Editor>
                            <ext:TextField ID="tfCode" runat="server" />
                        </Editor>
                    </ext:Column>
                </Columns>
            </ColumnModel>
            <View>
                <ext:GridView ID="GridView1" runat="server" />
            </View>
            <SelectionModel>
                <ext:RowSelectionModel ID="rowSelectionModel" runat="server" Mode="Single" AllowDeselect="true">

                </ext:RowSelectionModel>
            </SelectionModel>
        </ext:GridPanel>
    </Items>
</ext:Panel>