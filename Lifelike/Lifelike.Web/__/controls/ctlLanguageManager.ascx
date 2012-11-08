<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlLanguageManager.ascx.cs"
    Inherits="Lifelike.WebAdmin.__.controls.ctlLanguageManager" %>
<ext:Panel ID="Panel1" runat="server" Layout="FitLayout">
    <TopBar>
        <ext:Toolbar ID="tbLanguage" runat="server">
            <Items>
                <ext:Button ID="btnAddLanguage" runat="server" Text="Add" IconCls="add16" OnDirectClick="btnNewLanguage_Click" />
                <ext:Button ID="btnEditLanguage" runat="server" Text="Edit" IconCls="edit16" OnDirectClick="btnEditLanguage_Click"
                    Disabled="true" />
                <ext:Button ID="btnDeleteLanguage" runat="server" Text="Delete" IconCls="delete16"
                    Disabled="true">
                    <DirectEvents>
                        <Click OnEvent="btnDeleteLanguage_Click">
                            <Confirmation ConfirmRequest="true" Title="Are you sure?" Message="Are you sure you want to delete this entry?" />
                        </Click>
                    </DirectEvents>
                </ext:Button>
            </Items>
        </ext:Toolbar>
    </TopBar>
    <Items>
        <ext:GridPanel runat="server" ID="gpSites">
            <Store>
                <ext:Store ID="storeLanguages" runat="server">
                    <Model>
                        <ext:Model ID="modelLanguages" runat="server" IDProperty="Id">
                            <Fields>
                                <ext:ModelField Name="Id" Type="Auto" />
                                <ext:ModelField Name="Name" Type="String" />
                                <ext:ModelField Name="Code" Type="String" />
                            </Fields>
                        </ext:Model>
                    </Model>
                </ext:Store>
            </Store>
            <ColumnModel ID="columnSites" runat="server">
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
                    <DirectEvents>
                        <SelectionChange OnEvent="rowSelect_Click" />
                    </DirectEvents>
                </ext:RowSelectionModel>
            </SelectionModel>
        </ext:GridPanel>
    </Items>
</ext:Panel>
<ext:Window ID="winLanguage" runat="server" Title="Add New Language" Icon="Application"
    Height="150" Width="300" BodyStyle="background-color: #fff;" BodyPadding="5"
    Modal="true" Layout="FitLayout" Hidden="true">
    <Content>
        <ext:FormPanel ID="FormPanel1" runat="server" BodyPadding="5" ButtonAlign="Right"
            Layout="Column">
            <Items>
                <ext:Panel ID="Panel2" runat="server" Border="false" Header="false" Layout="Form"
                    LabelAlign="Top">
                    <Defaults>
                        <ext:Parameter Name="AllowBlank" Value="false" Mode="Raw" />
                        <ext:Parameter Name="MsgTarget" Value="side" />
                    </Defaults>
                    <Items>
                        <ext:Hidden ID="hidId" runat="server" />
                        <ext:TextField ID="txtName" runat="server" FieldLabel="Name" />
                        <ext:TextField ID="txtCode" runat="server" FieldLabel="Code" />
                    </Items>
                </ext:Panel>
            </Items>
            <Listeners>
                <ValidityChange Handler="#{btnSave}.setDisabled(!valid);" />
            </Listeners>
            <Buttons>
                <ext:Button ID="btnSave" runat="server" Text="Save" Disabled="true" FormBind="true"
                    OnDirectClick="btnSaveLanguage_Click" />
                <ext:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="#{winLanguage}.hide();#{txtName}.Text = ''; #{txtCode}.Text = ''; " />
            </Buttons>
        </ext:FormPanel>
    </Content>
</ext:Window>
