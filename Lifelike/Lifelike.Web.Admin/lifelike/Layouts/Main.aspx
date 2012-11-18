<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Lifelike.WebAdmin.lifelike.Layouts.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<script src="/lifelike/Content/lib/jquery-ui-1.9.1.custom/js/jquery-1.8.2.js"></script>
	<script src="/lifelike/Content/lib/jquery-ui-1.9.1.custom/js/jquery-ui-1.9.1.custom.min.js"></script>
	<script src="/Scripts/jquery.signalR-1.0.0-alpha2.js"></script>
	<script src="/signalr/hubs"></script>
	<script src="/lifelike/Content/js/mscorlib.js"></script>
	<script src="/lifelike/Content/js/Lifelike.JScript.Admin.js"></script>


	<link href="/lifelike/Content/css/itemeditor.css" rel="stylesheet" />

	<link href="/lifelike/Content/theme/jmetro/jquery-ui-1.8.16.custom.css" rel="stylesheet" />

</head>
<body>
	<form id="form1" runat="server">
		<div>
			<ll:Placeholder ID="plcHolder" runat="server" Key="control" />
		</div>
	</form>
</body>
</html>
