<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Lifelike.WebAdmin.lifelike.Layouts.Main" %>

<%@ Register TagPrefix="ll" TagName="ItemEditor" Src="~/lifelike/Modules/ItemEditor.ascx" %>
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
					<ext:DesktopModule ModuleID="dmItemEditor">
						<Window>
							<ext:Window ID="winItemEditor" runat="server" Width="600" Height="400" Title="Item Editor"
								Maximizable="true" Minimizable="true" Layout="BorderLayout">
								<Content>
									<%--<ll:ItemEditor ID="llItemEditor" runat="server" />--%>
								</Content>
							</ext:Window>
						</Window>
						<Shortcut Name="Item Editor" SortIndex="3" />
						<Launcher Text="Item Editor" />
					</ext:DesktopModule>
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
