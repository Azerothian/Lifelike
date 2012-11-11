<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Lifelike.WebAdmin.files.layouts.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>

	<form id="form1" runat="server">
		<div>
			<h1>Page</h1>
			<asp:Label ID="lblMessage" runat="server" /><br />
			<h1>Modules</h1>
			<ll:Placeholder ID="plcHolder" Key="test" runat="server" />
		</div>
	</form>
</body>
</html>
