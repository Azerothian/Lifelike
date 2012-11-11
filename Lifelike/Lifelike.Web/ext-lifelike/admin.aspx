<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="Lifelike.WebAdmin.lifelike.admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			<ext:ResourceManager ID="resManager" runat="server" />
			<ext:Desktop ID="dtMain" runat="server">
				<DesktopConfig Wallpaper="/lifelike/Content/img/background.jpg" WallpaperStretch="true" Border="false" />
				<Modules>
				</Modules>
				<TaskBar TrayWidth="100" Height="40">
					<QuickStart>
					</QuickStart>
					<Tray>
					</Tray>
				</TaskBar>
			</ext:Desktop>
		</div>
	</form>
</body>
</html>
