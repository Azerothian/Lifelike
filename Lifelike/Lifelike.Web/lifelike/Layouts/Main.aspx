<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Lifelike.WebAdmin.lifelike.Layouts.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			<telerik:RadSkinManager ID="RadSkinManager1" runat="server" ></telerik:RadSkinManager>
			<telerik:RadScriptManager ID="scriptManager" runat="server" />
			<telerik:RadAjaxManager ID="ajaxManager" runat="server" />
			<telerik:RadWindowManager ID="windowManager" ShowContentDuringLoad="false" VisibleStatusbar="false"
				ReloadOnShow="true" runat="server" EnableShadow="true" />

			<ll:Placeholder ID="plcHolder" runat="server" Key="control" />


		</div>
	</form>
</body>
</html>
