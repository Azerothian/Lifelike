<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestHubs.aspx.cs" Inherits="Lifelike.Web.Admin.lifelike.Diagnostics.TestHubs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
		<script src="/lifelike/Content/lib/jquery-ui-1.9.1.custom/js/jquery-1.8.2.js"></script>
	<script src="/lifelike/Content/lib/jquery-ui-1.9.1.custom/js/jquery-ui-1.9.1.custom.min.js"></script>
	<script src="/Scripts/jquery.signalR-1.0.0-alpha2.js"></script>
	<script src="/signalr/hubs"></script>
	<link href="/lifelike/Content/theme/jmetro/jquery-ui-1.8.16.custom.css" rel="stylesheet" />

    <title></title>

	<script>

		$(document).ready(function () {
			$.connection.hub.logging = true;
			$.connection.chat.client.registerNameResponse = function (success) {
				console.log('.chat.client.registerNameResponse', success);
				};
			$.connection.hub.start().done(function (sender, e) {
				
				$.connection.chat.server.registerName("AWESOME");
			}).fail(function (sender1, e1) {
				
			});



		});


	</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
