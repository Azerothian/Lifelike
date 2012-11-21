////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Program
var $Lifelike_JScript_Admin_$Program = function() {
};
$Lifelike_JScript_Admin_$Program.$main = function() {
	$(Function.mkdel(this, function() {
		//tree.Render();
		//jQuery.Select(".itemEditor").Dialog(new DialogOptions { AutoOpen = true, Width = 400, Title = "ITEM EDITOR" });
		//jQuery.Select(".button").Button();
		//jQuery.Select(".expand").Click(p => {
		//	node.Expand();
		//});
		//jQuery.Select(".close").Click(p =>
		//{
		//	node.Close();
		//});
		$Lifelike_JScript_Admin_Managers_PageManager.get_context().initialise();
		$Lifelike_JScript_Admin_PageRenderer.get_context().render();
		$Lifelike_JScript_Admin_Managers_HubManager.get_context().initialise();
		$Lifelike_JScript_Admin_Managers_HubManager.get_context().add_onConnection($Lifelike_JScript_Admin_$Program.$context_OnConnection);
	}));
};
$Lifelike_JScript_Admin_$Program.$context_OnConnection = function(msg1) {
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Control
var $Lifelike_JScript_Admin_Control = function(name) {
	this.$_children = null;
	this.$1$IsRenderedField = false;
	this.$1$ControlContainerField = null;
	this.$_parent = null;
	this.$_name = null;
	this.$_children = [];
	this.set_controlContainer(document.createElement('div'));
	this.set_name(name);
};
$Lifelike_JScript_Admin_Control.prototype = {
	get_isRendered: function() {
		return this.$1$IsRenderedField;
	},
	set_isRendered: function(value) {
		this.$1$IsRenderedField = value;
	},
	get_controlContainer: function() {
		return this.$1$ControlContainerField;
	},
	set_controlContainer: function(value) {
		this.$1$ControlContainerField = value;
	},
	get_parent: function() {
		return this.$_parent;
	},
	set_parent: function(value) {
		this.$_parent = value;
	},
	get_name: function() {
		return this.$_name;
	},
	set_name: function(value) {
		if (ss.isValue(this.get_parent())) {
			this.setClientId();
		}
		this.$_name = value;
	},
	get_clientId: function() {
		return Type.cast(this.get_controlContainer().getAttribute('id'), String);
	},
	set_clientId: function(value) {
		this.get_controlContainer().setAttribute('id', value);
	},
	get_cssClass: function() {
		return Type.cast(this.get_controlContainer().getAttribute('class'), String);
	},
	set_cssClass: function(value) {
		this.get_controlContainer().setAttribute('class', value);
	},
	get_height: function() {
		return $(this.get_controlContainer()).height().toString();
	},
	set_height: function(value) {
		$(this.get_controlContainer()).height(value);
	},
	get_width: function() {
		return $(this.get_controlContainer()).width().toString();
	},
	set_width: function(value) {
		$(this.get_controlContainer()).width(value);
	},
	get_background: function() {
		return $(this.get_controlContainer()).css('background');
	},
	set_background: function(value) {
		$(this.get_controlContainer()).css('background', value);
	},
	get_left: function() {
		return $(this.get_controlContainer()).css('left');
	},
	set_left: function(value) {
		$(this.get_controlContainer()).css('left', value);
	},
	get_top: function() {
		return $(this.get_controlContainer()).css('top');
	},
	set_top: function(value) {
		$(this.get_controlContainer()).css('top', value);
	},
	get_visible: function() {
		return $(this.get_controlContainer()).css('display') === 'none';
	},
	set_visible: function(value) {
		if (value) {
			$(this.get_controlContainer()).show();
		}
		else {
			$(this.get_controlContainer()).hide();
		}
	},
	findControl$1: function(collection, func) {
		for (var $t1 = 0; $t1 < collection.length; $t1++) {
			var v = collection[$t1];
			if (func(v)) {
				return v;
			}
			else if (ss.isValue(v.get_children())) {
				return this.findControl$1(v.get_children(), func);
			}
		}
		return null;
	},
	findControl: function(func) {
		return this.findControl$1(this.get_children(), func);
	},
	$addChildren: function(list) {
		for (var $t1 = 0; $t1 < list.length; $t1++) {
			var v = list[$t1];
			this.addChild(v);
		}
	},
	get_children: function() {
		return this.$_children;
	},
	addChild: function(control) {
		control.set_parent(this);
		control.setClientId();
		if (this.get_isRendered()) {
			$Lifelike_JScript_Admin_Util.console().log('.control.AddChild.render', [control.get_clientId()]);
			control.render();
		}
		this.$_children.add(control);
	},
	removeChild: function(control) {
		control.set_parent(null);
		this.$_children.remove(control);
		if (control.get_isRendered()) {
			control.removeRender();
		}
	},
	removeRender: function() {
		for (var $t1 = 0; $t1 < this.$_children.length; $t1++) {
			var c = this.$_children[$t1];
			c.removeRender();
		}
		if (this.get_isRendered()) {
			$(this.get_controlContainer()).remove();
			this.set_isRendered(false);
		}
	},
	render: function() {
		if (!this.get_isRendered()) {
			this.setClientId();
			this.preRender();
			$Lifelike_JScript_Admin_Util.console().log('.control.render', [this.get_clientId()]);
			if (ss.isValue(this.get_parent()) && ss.isValue(this.get_parent().get_controlContainer())) {
				this.get_parent().get_controlContainer().appendChild(this.get_controlContainer());
			}
		}
		if (ss.isValue(this.get_children())) {
			var $t1 = this.get_children();
			for (var $t2 = 0; $t2 < $t1.length; $t2++) {
				var c = $t1[$t2];
				c.render();
			}
		}
		if (!this.get_isRendered()) {
			this.postRender();
		}
		this.set_isRendered(true);
	},
	setClientId: function() {
		var control = this;
		var result = '';
		while (ss.isValue(control)) {
			result = control.get_name() + '_' + result;
			control = control.get_parent();
		}
		this.set_clientId(result);
	},
	$findControlByClientId: function(id) {
		var control = null;
		if (ss.referenceEquals(this.get_clientId(), id)) {
			control = this;
		}
		else {
			for (var $t1 = 0; $t1 < this.$_children.length; $t1++) {
				var c = this.$_children[$t1];
				control = c.$findControlByClientId(id);
				if (ss.isValue(control)) {
					break;
				}
			}
		}
		return control;
	},
	getProperties: function(name) {
		return Type.cast(this.get_controlContainer().getAttribute('data-' + name), String);
	},
	setProperties: function(name, value) {
		this.get_controlContainer().setAttribute('data-' + name, value);
	},
	preRender: null,
	postRender: function() {
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Guid
var $Lifelike_JScript_Admin_Guid = function() {
};
$Lifelike_JScript_Admin_Guid.createGuid = function() {
	return $Lifelike_JScript_Admin_Guid.$s4() + $Lifelike_JScript_Admin_Guid.$s4() + '-' + $Lifelike_JScript_Admin_Guid.$s4() + '-' + $Lifelike_JScript_Admin_Guid.$s4() + '-' + $Lifelike_JScript_Admin_Guid.$s4() + '-' + $Lifelike_JScript_Admin_Guid.$s4() + $Lifelike_JScript_Admin_Guid.$s4() + $Lifelike_JScript_Admin_Guid.$s4();
};
$Lifelike_JScript_Admin_Guid.$s4 = function() {
	var r = Math.floor(Math.random() * 65536);
	return r.toString().substr(0, 4);
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.LoginForm
var $Lifelike_JScript_Admin_LoginForm = function(name) {
	this.$_loginManager = null;
	this.$2$dlgLoginFormField = null;
	this.$2$txtUsernameField = null;
	this.$2$txtPasswordField = null;
	this.$2$btnLoginField = null;
	this.onClose = null;
	$Lifelike_JScript_Admin_Control.call(this, name);
	this.$_loginManager = new $Lifelike_JScript_Admin_Managers_LoginManager(this);
	//_loginManager.LoginResponseEvent = new Response<bool>(LoginResponse);
};
$Lifelike_JScript_Admin_LoginForm.prototype = {
	$loginResponse: function(success) {
		if (success) {
			$Lifelike_JScript_Admin_PageRenderer.get_context().removeChild(this);
		}
	},
	get_$dlgLoginForm: function() {
		return this.$2$dlgLoginFormField;
	},
	set_$dlgLoginForm: function(value) {
		this.$2$dlgLoginFormField = value;
	},
	get_$txtUsername: function() {
		return this.$2$txtUsernameField;
	},
	set_$txtUsername: function(value) {
		this.$2$txtUsernameField = value;
	},
	get_$txtPassword: function() {
		return this.$2$txtPasswordField;
	},
	set_$txtPassword: function(value) {
		this.$2$txtPasswordField = value;
	},
	get_$btnLogin: function() {
		return this.$2$btnLoginField;
	},
	set_$btnLogin: function(value) {
		this.$2$btnLoginField = value;
	},
	get_username: function() {
		return this.get_$txtUsername().get_value();
	},
	set_username: function(value) {
		this.get_$txtUsername().set_value(value);
	},
	get_password: function() {
		return this.get_$txtPassword().get_value();
	},
	set_password: function(value) {
		this.get_$txtPassword().set_value(value);
	},
	get_remember: function() {
		return false;
	},
	removeRender: function() {
		if (ss.isValue(this.onClose)) {
			this.onClose(this, null);
		}
		$Lifelike_JScript_Admin_Control.prototype.removeRender.call(this);
	},
	preRender: function() {
		this.set_$dlgLoginForm(new $Lifelike_JScript_Admin_Controls_Dialog('dlgLoginForm'));
		this.get_$dlgLoginForm().get_options().autoOpen = true;
		this.get_$dlgLoginForm().get_options().closeOnEscape = false;
		this.get_$dlgLoginForm().get_options().draggable = false;
		this.get_$dlgLoginForm().get_options().modal = true;
		this.get_$dlgLoginForm().get_options().title = 'Login';
		this.get_$dlgLoginForm().set_isCloseable(false);
		this.set_$txtPassword(new $Lifelike_JScript_Admin_Controls_TextBox('txtPassword'));
		this.set_$txtUsername(new $Lifelike_JScript_Admin_Controls_TextBox('txtUsername'));
		this.get_$txtUsername().set_placeholder('Username');
		this.get_$txtPassword().set_placeholder('Password');
		this.set_$btnLogin(new $Lifelike_JScript_Admin_Controls_Button('btnLogin'));
		this.get_$btnLogin().set_text('LOGIN');
		var $t1 = this.get_$btnLogin();
		$t1.onClick = Function.combine($t1.onClick, Function.mkdel(this, this.btnLogin_OnClick));
		this.get_$dlgLoginForm().addChild(this.get_$txtUsername());
		this.get_$dlgLoginForm().addChild(this.get_$txtPassword());
		this.get_$dlgLoginForm().addChild(this.get_$btnLogin());
		this.addChild(this.get_$dlgLoginForm());
	},
	btnLogin_OnClick: function(e) {
		if (String.isNullOrEmpty(this.get_$txtUsername().get_value())) {
			window.alert('Please enter in a username');
			return;
		}
		this.$_loginManager.$loginUser();
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.PageRenderer
var $Lifelike_JScript_Admin_PageRenderer = function() {
	$Lifelike_JScript_Admin_Control.call(this, 'body');
	this.set_controlContainer($('body').get(0));
	this.set_isRendered(true);
};
$Lifelike_JScript_Admin_PageRenderer.prototype = {
	preRender: function() {
	}
};
$Lifelike_JScript_Admin_PageRenderer.get_context = function() {
	if (ss.isNullOrUndefined($Lifelike_JScript_Admin_PageRenderer.$_context)) {
		$Lifelike_JScript_Admin_PageRenderer.$_context = new $Lifelike_JScript_Admin_PageRenderer();
	}
	return $Lifelike_JScript_Admin_PageRenderer.$_context;
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Util
var $Lifelike_JScript_Admin_Util = function() {
};
$Lifelike_JScript_Admin_Util.console = function() {
	return $Lifelike_JScript_Admin_Managers_PageManager.get_context().get_consoleModule();
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Controls.Button
var $Lifelike_JScript_Admin_Controls_Button = function(name) {
	this.onClick = null;
	this.$_element = null;
	$Lifelike_JScript_Admin_Control.call(this, name);
	this.$_element = document.createElement('a');
	this.get_controlContainer().appendChild(this.$_element);
};
$Lifelike_JScript_Admin_Controls_Button.prototype = {
	preRender: function() {
	},
	postRender: function() {
		if (ss.isValue(this.onClick)) {
			$(this.$_element).click(this.onClick);
		}
		$(this.$_element).button();
		$Lifelike_JScript_Admin_Control.prototype.postRender.call(this);
	},
	get_text: function() {
		return this.$_element.innerHTML;
	},
	set_text: function(value) {
		this.$_element.innerHTML = value;
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Controls.Dialog
var $Lifelike_JScript_Admin_Controls_Dialog = function(name) {
	this.$2$IsCloseableField = false;
	this.$2$OptionsField = null;
	$Lifelike_JScript_Admin_Control.call(this, name);
	this.set_options({});
};
$Lifelike_JScript_Admin_Controls_Dialog.prototype = {
	get_isCloseable: function() {
		return this.$2$IsCloseableField;
	},
	set_isCloseable: function(value) {
		this.$2$IsCloseableField = value;
	},
	get_options: function() {
		return this.$2$OptionsField;
	},
	set_options: function(value) {
		this.$2$OptionsField = value;
	},
	preRender: function() {
	},
	postRender: function() {
		var dObject = $(this.get_controlContainer()).dialog(this.get_options());
		//Util.Debugger();
		if (!this.get_isCloseable()) {
			var p = $(this.get_controlContainer()).parent();
			p.find('.ui-dialog-titlebar > .ui-dialog-titlebar-close').remove();
		}
		$Lifelike_JScript_Admin_Control.prototype.postRender.call(this);
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Controls.Image
var $Lifelike_JScript_Admin_Controls_Image = function(name) {
	$Lifelike_JScript_Admin_Control.call(this, name);
	this.set_controlContainer(document.createElement('img'));
};
$Lifelike_JScript_Admin_Controls_Image.prototype = {
	get_src: function() {
		return Type.cast(this.get_controlContainer().getAttribute('src'), String);
	},
	set_src: function(value) {
		this.get_controlContainer().setAttribute('src', value);
	},
	preRender: function() {
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Controls.Label
var $Lifelike_JScript_Admin_Controls_Label = function(name) {
	$Lifelike_JScript_Admin_Control.call(this, name);
};
$Lifelike_JScript_Admin_Controls_Label.prototype = {
	get_text: function() {
		return this.get_controlContainer().innerHTML;
	},
	set_text: function(value) {
		this.get_controlContainer().innerHTML = value;
	},
	preRender: function() {
	}
};
$Lifelike_JScript_Admin_Controls_Label.$ctor1 = function(name, tag) {
	$Lifelike_JScript_Admin_Control.call(this, name);
	this.set_controlContainer(document.createElement(tag));
	this.set_name(name);
};
$Lifelike_JScript_Admin_Controls_Label.$ctor1.prototype = $Lifelike_JScript_Admin_Controls_Label.prototype;
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Controls.List
var $Lifelike_JScript_Admin_Controls_List = function(name) {
	$Lifelike_JScript_Admin_Control.call(this, name);
	this.set_controlContainer(document.createElement('ul'));
	this.set_name(name);
};
$Lifelike_JScript_Admin_Controls_List.prototype = {
	preRender: function() {
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Controls.ListItem
var $Lifelike_JScript_Admin_Controls_ListItem = function(name) {
	$Lifelike_JScript_Admin_Control.call(this, name);
	this.set_controlContainer(document.createElement('li'));
	this.set_name(name);
};
$Lifelike_JScript_Admin_Controls_ListItem.prototype = {
	preRender: function() {
	},
	get_text: function() {
		return this.get_controlContainer().innerHTML;
	},
	set_text: function(value) {
		this.get_controlContainer().innerHTML = value;
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Controls.Tabs
var $Lifelike_JScript_Admin_Controls_Tabs = function(name) {
	this.$lstTabs = null;
	$Lifelike_JScript_Admin_Control.call(this, name);
	this.$lstTabs = new $Lifelike_JScript_Admin_Controls_List('lstTabs');
	this.addChild(this.$lstTabs);
};
$Lifelike_JScript_Admin_Controls_Tabs.prototype = {
	addTab: function(display, control) {
		this.addChild(control);
		var p = control.get_parent();
		var clientid = control.get_clientId();
		display = '<a href=\'#' + clientid + '\'>' + display + '</a>';
		var $t2 = this.$lstTabs;
		var $t1 = new $Lifelike_JScript_Admin_Controls_ListItem(display);
		$t1.set_text(display);
		$t2.addChild($t1);
	},
	preRender: function() {
	},
	postRender: function() {
		$(this.get_controlContainer()).tabs();
		$Lifelike_JScript_Admin_Control.prototype.postRender.call(this);
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Controls.TextBox
var $Lifelike_JScript_Admin_Controls_TextBox = function(name) {
	this.$_element = null;
	$Lifelike_JScript_Admin_Control.call(this, name);
	this.$_element = document.createElement('input');
};
$Lifelike_JScript_Admin_Controls_TextBox.prototype = {
	get_value: function() {
		return this.$_element.value;
	},
	set_value: function(value) {
		this.$_element.value = null;
	},
	get_placeholder: function() {
		return Type.cast(this.$_element.getAttribute('placeholder'), String);
	},
	set_placeholder: function(value) {
		this.$_element.setAttribute('placeholder', value);
	},
	preRender: function() {
		this.$_element.setAttribute('type', 'text');
		if (!String.isNullOrEmpty(this.get_cssClass())) {
			if (this.get_cssClass().indexOf('textbox') > -1) {
				this.set_cssClass(this.get_cssClass() + ' textbox');
			}
		}
		this.get_controlContainer().appendChild(this.$_element);
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Controls.Tree
var $Lifelike_JScript_Admin_Controls_Tree = function(name) {
	$Lifelike_JScript_Admin_Control.call(this, name);
	this.set_controlContainer(document.createElement('ul'));
	this.get_controlContainer().setAttribute('class', 'tree');
};
$Lifelike_JScript_Admin_Controls_Tree.prototype = {
	refreshTree: function(node) {
		//TODO: if visible then refresh
	},
	preRender: function() {
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Controls.TreeNode
var $Lifelike_JScript_Admin_Controls_TreeNode = function() {
	this.$2$TreeField = null;
	this.$2$lblDetailField = null;
	this.$2$lblImageField = null;
	this.$2$InnerTreeField = null;
	$Lifelike_JScript_Admin_Control.call(this, 'node');
	this.set_controlContainer(document.createElement('li'));
	this.set_name($Lifelike_JScript_Admin_Guid.createGuid());
	this.set_lblImage(new $Lifelike_JScript_Admin_Controls_Label.$ctor1('lblImage', 'span'));
	this.get_lblImage().set_cssClass('ui-icon ui-icon-carat-1-e');
	this.addChild(this.get_lblImage());
	this.set_lblDetail(new $Lifelike_JScript_Admin_Controls_Label('lblDetail'));
	this.get_lblDetail().set_cssClass('label');
	this.addChild(this.get_lblDetail());
	this.set_innerTree(new $Lifelike_JScript_Admin_Controls_Tree('InnerTree'));
	this.addChild(this.get_innerTree());
};
$Lifelike_JScript_Admin_Controls_TreeNode.prototype = {
	get_id: function() {
		return this.getProperties('id');
	},
	set_id: function(value) {
		this.setProperties('id', value);
	},
	get_tree: function() {
		return this.$2$TreeField;
	},
	set_tree: function(value) {
		this.$2$TreeField = value;
	},
	get_value: function() {
		return this.getProperties('value');
	},
	set_value: function(value) {
		this.setProperties('value', value);
	},
	get_text: function() {
		return this.get_lblDetail().get_text();
	},
	set_text: function(value) {
		this.get_lblDetail().set_text(value);
	},
	get_expanded: function() {
		var c = this.getProperties('expanded');
		if (ss.isValue(c)) {
			return Boolean.parse(c);
		}
		return false;
	},
	set_expanded: function(value) {
		this.setProperties('expanded', value.toString());
	},
	get_index: function() {
		return parseInt(this.getProperties('index'));
	},
	set_index: function(value) {
		this.setProperties('index', value.toString());
	},
	get_lblDetail: function() {
		return this.$2$lblDetailField;
	},
	set_lblDetail: function(value) {
		this.$2$lblDetailField = value;
	},
	get_lblImage: function() {
		return this.$2$lblImageField;
	},
	set_lblImage: function(value) {
		this.$2$lblImageField = value;
	},
	get_innerTree: function() {
		return this.$2$InnerTreeField;
	},
	set_innerTree: function(value) {
		this.$2$InnerTreeField = value;
	},
	delete: function() {
		var $t1 = this.get_innerTree().get_children();
		for (var $t2 = 0; $t2 < $t1.length; $t2++) {
			var n = $t1[$t2];
			this.removeChild(n);
		}
		this.removeChild(this);
	},
	expand: function() {
		this.set_expanded(true);
		var $t1 = this.get_innerTree().get_children();
		for (var $t2 = 0; $t2 < $t1.length; $t2++) {
			var e = $t1[$t2];
			e.set_visible(true);
		}
	},
	close: function() {
		this.set_expanded(false);
		var $t1 = this.get_innerTree().get_children();
		for (var $t2 = 0; $t2 < $t1.length; $t2++) {
			var e = $t1[$t2];
			e.close();
			e.set_visible(false);
		}
	},
	preRender: function() {
	},
	postRender: function() {
		$(this.get_lblImage().get_controlContainer()).click(Function.mkdel(this, function(p) {
			if (this.get_expanded()) {
				this.close();
			}
			else {
				this.expand();
			}
		}));
		if (!this.get_expanded()) {
			this.set_visible(false);
		}
		$Lifelike_JScript_Admin_Control.prototype.postRender.call(this);
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Interfaces.ILogin
var $Lifelike_JScript_Admin_Interfaces_ILogin = function() {
};
$Lifelike_JScript_Admin_Interfaces_ILogin.prototype = { get_username: null, get_password: null, get_remember: null };
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Managers.HubManager
var $Lifelike_JScript_Admin_Managers_HubManager = function() {
	this.$1$OnConnectionField = null;
};
$Lifelike_JScript_Admin_Managers_HubManager.prototype = {
	add_onConnection: function(value) {
		this.$1$OnConnectionField = Function.combine(this.$1$OnConnectionField, value);
	},
	remove_onConnection: function(value) {
		this.$1$OnConnectionField = Function.remove(this.$1$OnConnectionField, value);
	},
	initialise: function() {
		//GetConnection().chat.addMessage = new ChatMessage(msg => {  }); 
		$.connection.hub.logging = true;
		$.connection.hub.start().done(Function.mkdel(this, function(sender, e) {
			this.$connected();
		})).fail(Function.mkdel(this, function(sender1, e1) {
			this.$failed();
		}));
	},
	$failed: function() {
		window.alert('FAILED to connect');
	},
	$connected: function() {
		if (ss.isValue(this.$1$OnConnectionField)) {
			this.$1$OnConnectionField(true);
		}
		//Window.Alert("CONNECTED");
		//var chat = 	GetConnection().chat;
		//chat.server.sendMessage("HI!");
	}
};
$Lifelike_JScript_Admin_Managers_HubManager.get_context = function() {
	if (ss.isNullOrUndefined($Lifelike_JScript_Admin_Managers_HubManager.$_context)) {
		$Lifelike_JScript_Admin_Managers_HubManager.$_context = new $Lifelike_JScript_Admin_Managers_HubManager();
	}
	return $Lifelike_JScript_Admin_Managers_HubManager.$_context;
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Managers.LoginManager
var $Lifelike_JScript_Admin_Managers_LoginManager = function(login) {
	this.$_inf = null;
	this.$_inf = login;
};
$Lifelike_JScript_Admin_Managers_LoginManager.prototype = {
	$loginUser: function() {
		//Util.Console().log(".auth.server.login", _inf);
		$Lifelike_JScript_Admin_Managers_PageManager.get_context().$loginResponse(this.$_inf.get_username(), true);
		//HubManager.Context.GetConnection().auth.server.login(_inf.Username, _inf.Password, _inf.Remember);
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Managers.PageManager
var $Lifelike_JScript_Admin_Managers_PageManager = function() {
	this.$1$UsernameField = null;
	this.$1$IsLoggedInField = false;
	this.$1$ConsoleModuleField = null;
	this.$1$chatModuleField = null;
	this.$1$panelLayoutField = null;
	this.$1$itemTreeModuleField = null;
	this.$_loginForm = null;
	this.$1$hasRenderedField = false;
	this.$_loginForm = new $Lifelike_JScript_Admin_LoginForm('frmLogin');
	this.set_consoleModule(new $Lifelike_JScript_Admin_Modules_Log_ConsoleModule('console'));
	this.set_chatModule(new $Lifelike_JScript_Admin_Modules_Chat_ChatModule('chat'));
	this.set_itemTreeModule(new $Lifelike_JScript_Admin_Modules_Item_ItemTreeModule('items'));
	this.set_panelLayout(new $Lifelike_JScript_Admin_Modules_Panels_PanelLayout('pnlLayout'));
};
$Lifelike_JScript_Admin_Managers_PageManager.prototype = {
	get_username: function() {
		return this.$1$UsernameField;
	},
	set_username: function(value) {
		this.$1$UsernameField = value;
	},
	get_isLoggedIn: function() {
		return this.$1$IsLoggedInField;
	},
	set_isLoggedIn: function(value) {
		this.$1$IsLoggedInField = value;
	},
	get_consoleModule: function() {
		return this.$1$ConsoleModuleField;
	},
	set_consoleModule: function(value) {
		this.$1$ConsoleModuleField = value;
	},
	get_chatModule: function() {
		return this.$1$chatModuleField;
	},
	set_chatModule: function(value) {
		this.$1$chatModuleField = value;
	},
	get_panelLayout: function() {
		return this.$1$panelLayoutField;
	},
	set_panelLayout: function(value) {
		this.$1$panelLayoutField = value;
	},
	get_itemTreeModule: function() {
		return this.$1$itemTreeModuleField;
	},
	set_itemTreeModule: function(value) {
		this.$1$itemTreeModuleField = value;
	},
	get_hasRendered: function() {
		return this.$1$hasRenderedField;
	},
	set_hasRendered: function(value) {
		this.$1$hasRenderedField = value;
	},
	initialise: function() {
		//HubManager.Context.GetConnection().auth.client.loginResponse = new Response<string, bool>(LoginResponse);
		this.check();
	},
	check: function() {
		this.loginCheck();
		window.setTimeout(Function.mkdel(this, this.check), 500);
	},
	loginCheck: function() {
		if (!this.get_isLoggedIn() && !this.$_loginForm.get_isRendered()) {
			$Lifelike_JScript_Admin_PageRenderer.get_context().addChild(this.$_loginForm);
		}
	},
	$loginResponse: function(username, success) {
		//    Util.Console().log(".auth.client.loginResponse", username, success);
		this.set_isLoggedIn(success);
		if (success) {
			this.set_username(this.$_loginForm.get_username());
			$Lifelike_JScript_Admin_PageRenderer.get_context().removeChild(this.$_loginForm);
			if (!this.get_hasRendered()) {
				this.initateSystem();
				// DUN DUN DAAAAHHH
			}
		}
	},
	initateSystem: function() {
		$Lifelike_JScript_Admin_PageRenderer.get_context().addChild(this.get_panelLayout());
		$Lifelike_JScript_Admin_PageRenderer.get_context().addChild(this.get_itemTreeModule());
		$Lifelike_JScript_Admin_PageRenderer.get_context().addChild(this.get_consoleModule());
		$Lifelike_JScript_Admin_PageRenderer.get_context().addChild(this.get_chatModule());
		$Lifelike_JScript_Admin_PageRenderer.get_context().render();
		this.get_chatModule().registerName($Lifelike_JScript_Admin_Managers_PageManager.get_context().get_username());
	},
	getControlByClientId: function(id) {
		return $Lifelike_JScript_Admin_PageRenderer.get_context().$findControlByClientId(id);
	}
};
$Lifelike_JScript_Admin_Managers_PageManager.get_context = function() {
	if (ss.isNullOrUndefined($Lifelike_JScript_Admin_Managers_PageManager.$_context)) {
		$Lifelike_JScript_Admin_Managers_PageManager.$_context = new $Lifelike_JScript_Admin_Managers_PageManager();
	}
	return $Lifelike_JScript_Admin_Managers_PageManager.$_context;
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Modules.Chat.BaseControl
var $Lifelike_JScript_Admin_Modules_Chat_BaseControl = function(name) {
	$Lifelike_JScript_Admin_Control.call(this, name);
};
$Lifelike_JScript_Admin_Modules_Chat_BaseControl.prototype = {
	preRender: function() {
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Modules.Chat.ChatModule
var $Lifelike_JScript_Admin_Modules_Chat_ChatModule = function(name) {
	this.$2$RoomsField = null;
	this.$roomEnt = null;
	$Lifelike_JScript_Admin_Control.call(this, name);
	//_element = Document.CreateElement("a");
	//ControlContainer.AppendChild(_element);
	this.set_$rooms([]);
	this.set_cssClass('chatbar');
	$.connection.chat.client.recieveMessageResponse = Function.mkdel(this, this.recieveMessageResponse);
	$.connection.chat.client.getCurrentRoomsResponse = Function.mkdel(this, this.getCurrentRoomsResponse);
	$.connection.chat.client.getAvailableRoomsResponse = Function.mkdel(this, this.getAvailableRoomsResponse);
	$.connection.chat.client.joinRoomResponse = Function.mkdel(this, this.joinRoomResponse);
	$.connection.chat.client.registerNameResponse = Function.mkdel(this, this.registerNameResponse);
};
$Lifelike_JScript_Admin_Modules_Chat_ChatModule.prototype = {
	get_$rooms: function() {
		return this.$2$RoomsField;
	},
	set_$rooms: function(value) {
		this.$2$RoomsField = value;
	},
	joinRoomResponse: function(room, success) {
		$Lifelike_JScript_Admin_Util.console().log('.chat.client.joinRoomResponse', [room, success]);
		if (success) {
			this.createRoom(room);
		}
	},
	registerNameResponse: function(success) {
		$Lifelike_JScript_Admin_Util.console().log('.chat.client.registerNameResponse', [success]);
		this.joinRoom('General');
	},
	recieveMessageResponse: function(room, user, message, isAlert) {
		$Lifelike_JScript_Admin_Util.console().log('.chat.client.recieveMessageResponse', [room, user, message, isAlert]);
		var roomEnt = null;
		var $t1 = this.get_$rooms();
		for (var $t2 = 0; $t2 < $t1.length; $t2++) {
			var v = $t1[$t2];
			if (ss.referenceEquals(v.get_name(), room)) {
				roomEnt = v;
				break;
			}
		}
		if (ss.isNullOrUndefined(roomEnt)) {
			$Lifelike_JScript_Admin_Util.console().log('the room entity we are looking for is null ', [room, this.get_$rooms(), user, message, isAlert]);
			return;
		}
		roomEnt.$addNewMessage$1(user, message, isAlert);
	},
	createRoom: function(room) {
		this.$roomEnt = new $Lifelike_JScript_Admin_Modules_Chat_RoomControl(room);
		this.$roomEnt.set_parent(this);
		this.$roomEnt.set_user($Lifelike_JScript_Admin_Managers_PageManager.get_context().get_username());
		this.get_children().add(this.$roomEnt);
		this.$roomEnt.render();
		this.get_$rooms().add(this.$roomEnt);
	},
	getCurrentRoomsResponse: function(rooms) {
		$Lifelike_JScript_Admin_Util.console().log('.chat.client.getCurrentRoomsResponse', [rooms]);
	},
	getAvailableRoomsResponse: function(rooms) {
		$Lifelike_JScript_Admin_Util.console().log('.chat.client.getCurrentRoomsResponse', [rooms]);
	},
	registerName: function(name) {
		$Lifelike_JScript_Admin_Util.console().log('.chat.server.registerName', [name]);
		$.connection.chat.server.registerName($Lifelike_JScript_Admin_Managers_PageManager.get_context().get_username());
	},
	sendMessage: function(room, message) {
		$Lifelike_JScript_Admin_Util.console().log('.chat.server.sendMessage', [room, message]);
		$.connection.chat.server.sendMessage(room, message);
	},
	getCurrentRooms: function() {
		$Lifelike_JScript_Admin_Util.console().log('.chat.server.getCurrentRooms', []);
		$.connection.chat.server.getCurrentRooms();
	},
	getAvailableRooms: function() {
		$Lifelike_JScript_Admin_Util.console().log('.chat.server.getAvailableRooms', []);
		$.connection.chat.server.getAvailableRooms();
	},
	joinRoom: function(room) {
		$Lifelike_JScript_Admin_Util.console().log('.chat.server.joinRoom', [room]);
		$.connection.chat.server.joinRoom(room);
	},
	leaveRoom: function(room) {
		$Lifelike_JScript_Admin_Util.console().log('.chat.server.leaveRoom', [room]);
		$.connection.chat.server.leaveRoom(room);
	},
	preRender: function() {
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Modules.Chat.MessageControl
var $Lifelike_JScript_Admin_Modules_Chat_MessageControl = function(name) {
	this.$_avatar = null;
	this.$_username = null;
	this.$_message = null;
	$Lifelike_JScript_Admin_Control.call(this, name);
	//_avatar = (ImageElement)Document.CreateElement("img");
	this.$_username = new $Lifelike_JScript_Admin_Controls_Label('username');
	this.$_message = new $Lifelike_JScript_Admin_Controls_Label('message');
	this.$_message.set_cssClass('message');
	this.$_username.set_cssClass('username');
	//_avatar.CssClass = "avatar";
	//ControlContainer.AppendChild(_avatar);
	this.addChild(this.$_username);
	this.addChild(this.$_message);
	this.set_cssClass('chatmessageContainer');
};
$Lifelike_JScript_Admin_Modules_Chat_MessageControl.prototype = {
	get_username: function() {
		return this.$_username.get_text();
	},
	set_username: function(value) {
		this.$_username.set_text(value);
	},
	get_message: function() {
		return this.$_message.get_text();
	},
	set_message: function(value) {
		this.$_message.set_text(value);
	},
	preRender: function() {
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Modules.Chat.MessengerControl
var $Lifelike_JScript_Admin_Modules_Chat_MessengerControl = function(name) {
	this.$2$RoomField = null;
	this.$2$RoomControlField = null;
	this.$_txtMessage = null;
	this.$_btnSend = null;
	$Lifelike_JScript_Admin_Control.call(this, name);
	this.$_txtMessage = new $Lifelike_JScript_Admin_Controls_TextBox('txtMessage');
	this.$_btnSend = new $Lifelike_JScript_Admin_Controls_Button('btnSend');
	this.$_btnSend.onClick = Function.combine(this.$_btnSend.onClick, Function.mkdel(this, this.btnSend_OnClick));
	this.$_btnSend.set_text('Send');
	this.addChild(this.$_txtMessage);
	this.addChild(this.$_btnSend);
};
$Lifelike_JScript_Admin_Modules_Chat_MessengerControl.prototype = {
	get_room: function() {
		return this.$2$RoomField;
	},
	set_room: function(value) {
		this.$2$RoomField = value;
	},
	get_roomControl: function() {
		return this.$2$RoomControlField;
	},
	set_roomControl: function(value) {
		this.$2$RoomControlField = value;
	},
	btnSend_OnClick: function(e) {
		$Lifelike_JScript_Admin_Managers_PageManager.get_context().get_chatModule().sendMessage(this.get_room(), this.$_txtMessage.get_value());
		this.get_roomControl().$addNewMessage(this.$_txtMessage.get_value(), false);
		this.$_txtMessage.set_value('');
	},
	preRender: function() {
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Modules.Chat.RoomControl
var $Lifelike_JScript_Admin_Modules_Chat_RoomControl = function(room) {
	this.$2$UserField = null;
	this.$2$ColourField = null;
	this.$_title = null;
	this.$_messageContainer = null;
	this.$_dockable = null;
	this.$_messenger = null;
	this.$_userControl = null;
	$Lifelike_JScript_Admin_Control.call(this, room);
	this.$_dockable = new $Lifelike_JScript_Admin_Modules_Panels_DockableControl('MessageContainer');
	this.$_userControl = new $Lifelike_JScript_Admin_Modules_Chat_UserControl('UserControl', room);
	//_dockable.Options = new jQueryApi.UI.Widgets.DialogOptions()
	//{
	//	AutoOpen = true,
	//	Title = ,
	//	Width = 350,
	//	Height = 375
	//};
	this.$_dockable.set_title('Room - ' + room);
	this.$_messageContainer = new $Lifelike_JScript_Admin_Modules_Chat_BaseControl('MessageContainer');
	this.$_messageContainer.set_cssClass('messageContainer');
	this.$_messenger = new $Lifelike_JScript_Admin_Modules_Chat_MessengerControl('Messenger');
	this.$_messenger.set_roomControl(this);
	this.$_messenger.set_room(room);
	this.$_messenger.set_cssClass('messenger');
	this.$_dockable.addChild(this.$_userControl);
	this.$_dockable.addChild(this.$_messageContainer);
	this.$_dockable.addChild(this.$_messenger);
	this.addChild(this.$_dockable);
};
$Lifelike_JScript_Admin_Modules_Chat_RoomControl.prototype = {
	get_user: function() {
		return this.$2$UserField;
	},
	set_user: function(value) {
		this.$2$UserField = value;
	},
	get_colour: function() {
		return this.$2$ColourField;
	},
	set_colour: function(value) {
		this.$2$ColourField = value;
	},
	get_title: function() {
		return this.$_title.get_text();
	},
	set_title: function(value) {
		this.$_title.set_text(value);
	},
	resizeWindow: function(e, re) {
	},
	preRender: function() {
	},
	$addNewMessage$1: function(user, message, isAlert) {
		$Lifelike_JScript_Admin_Util.console().log('.js.modules.chat.roomcontrol AddNewMessage', [user, message]);
		var newcount = this.$_messageContainer.get_children().length + 1;
		var msg = null;
		if (this.$_messageContainer.get_children().length > 1) {
			var m = Type.cast(this.$_messageContainer.get_children()[this.$_messageContainer.get_children().length - 1], $Lifelike_JScript_Admin_Modules_Chat_MessageControl);
			if (!!ss.referenceEquals(m.get_username(), user.Username)) {
				msg = m;
				msg.set_message(msg.get_message() + ' <br/>');
			}
		}
		if (ss.isNullOrUndefined(msg)) {
			msg = new $Lifelike_JScript_Admin_Modules_Chat_MessageControl('message_' + newcount);
		}
		msg.set_username(Type.cast(user, String));
		msg.set_message(msg.get_message() + message);
		msg.set_parent(this.$_messageContainer);
		if (isAlert) {
			msg.set_cssClass(' chatmessageContainer alert');
		}
		else if (!!!ss.referenceEquals(user, $Lifelike_JScript_Admin_Managers_PageManager.get_context().get_username())) {
			msg.set_cssClass(' chatmessageContainer outsider');
		}
		this.$_messageContainer.get_children().add(msg);
		msg.render();
		var spacer = new $Lifelike_JScript_Admin_Modules_Chat_BaseControl('spacer_' + newcount);
		spacer.set_cssClass('clear');
		spacer.set_parent(this.$_messageContainer);
		this.$_messageContainer.get_children().add(spacer);
		spacer.render();
		var result = ss.Int32.div(this.$_messageContainer.get_children().length, 2) * 41 + 'px';
		$Lifelike_JScript_Admin_Util.console().log('result = ', [result]);
		var _dictionary = ss.mkdict(['scrollTop', result]);
		$(this.$_messageContainer.get_controlContainer()).animate(_dictionary, 'slow', 'linear');
	},
	$addNewMessage: function(message, isAlert) {
		this.$addNewMessage$1(this.get_user(), message, isAlert);
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Modules.Chat.UserControl
var $Lifelike_JScript_Admin_Modules_Chat_UserControl = function(name, room) {
	this.$room = null;
	$Lifelike_JScript_Admin_Control.call(this, name);
	this.set_height('30');
	this.set_width('100%');
	this.set_background('#647687');
	this.$room = room;
};
$Lifelike_JScript_Admin_Modules_Chat_UserControl.prototype = {
	preRender: function() {
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Modules.Console.ConsoleView
var $Lifelike_JScript_Admin_Modules_Console_ConsoleView = function(name) {
	this.$lblMessages = null;
	$Lifelike_JScript_Admin_Control.call(this, name);
	this.$lblMessages = new $Lifelike_JScript_Admin_Controls_Label('lblMessages');
	this.addChild(this.$lblMessages);
};
$Lifelike_JScript_Admin_Modules_Console_ConsoleView.prototype = {
	preRender: function() {
	},
	postRender: function() {
		this.$lblMessages.set_cssClass('messages');
		$Lifelike_JScript_Admin_Control.prototype.postRender.call(this);
	},
	logMessage: function(message, arr) {
		if (ss.isValue(arr)) {
			var r = '';
			for (var $t1 = 0; $t1 < arr.length; $t1++) {
				var o = arr[$t1];
				if (Type.isInstanceOfType(o, String)) {
					r = r + ' ' + Type.cast(o, String);
				}
			}
			message = message + r;
		}
		this.$lblMessages.set_text(this.$lblMessages.get_text() + '<br/>' + message);
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Modules.Item.ItemTreeModule
var $Lifelike_JScript_Admin_Modules_Item_ItemTreeModule = function(name) {
	this.$panel = null;
	this.$treeItems = null;
	$Lifelike_JScript_Admin_Control.call(this, name);
	this.$panel = new $Lifelike_JScript_Admin_Modules_Panels_DockableControl('Items');
	this.$panel.set_title('Items');
	this.$treeItems = new $Lifelike_JScript_Admin_Controls_Tree('treeItems');
	//Tree tree = new Tree(".itemEditor");
	var $t1 = new $Lifelike_JScript_Admin_Controls_TreeNode();
	$t1.set_text('Text');
	$t1.set_value('Value');
	$t1.set_expanded(true);
	var node = $t1;
	var $t2 = [];
	var $t3 = new $Lifelike_JScript_Admin_Controls_TreeNode();
	$t3.set_text('Text');
	$t3.set_value('Value');
	$t3.set_parent(node);
	$t2.add($t3);
	var $t4 = new $Lifelike_JScript_Admin_Controls_TreeNode();
	$t4.set_text('Text');
	$t4.set_value('Value');
	$t4.set_parent(node);
	$t2.add($t4);
	var $t5 = new $Lifelike_JScript_Admin_Controls_TreeNode();
	$t5.set_text('Text');
	$t5.set_value('Value');
	$t5.set_parent(node);
	$t2.add($t5);
	node.$addChildren($t2);
	//var ss = new TreeNode() { Text = "Text", Value = "Value", Parent = node };
	//ss.AddChildren(new List<Control>()
	// {
	//	 new TreeNode() { Text = "Text", Value = "Value" , Parent = node },
	//	 new TreeNode() { Text = "Text", Value = "Value", Parent = node },
	//	 new TreeNode() { Text = "Text", Value = "Value", Parent = node },
	// });
	//node.Children.Add(ss);
	this.$treeItems.addChild(node);
	this.$panel.addChild(this.$treeItems);
	this.addChild(this.$panel);
};
$Lifelike_JScript_Admin_Modules_Item_ItemTreeModule.prototype = {
	preRender: function() {
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Modules.Log.ConsoleModule
var $Lifelike_JScript_Admin_Modules_Log_ConsoleModule = function(name) {
	this.$tbViews = null;
	this.$cvLog = null;
	this.$cvDebug = null;
	this.$dlgWindow = null;
	$Lifelike_JScript_Admin_Control.call(this, name);
	//txtInput = new TextBox("txtInput");
	//btnSend = new Button("btnSend");
	this.$dlgWindow = new $Lifelike_JScript_Admin_Modules_Panels_DockableControl('dlgWindow');
	this.$dlgWindow.set_title('Console');
	console.log = Function.mkdel(this, this.log);
	console.debug = Function.mkdel(this, this.debug);
	this.$cvLog = new $Lifelike_JScript_Admin_Modules_Console_ConsoleView('cvLog');
	this.$cvDebug = new $Lifelike_JScript_Admin_Modules_Console_ConsoleView('cvDebug');
	this.$tbViews = new $Lifelike_JScript_Admin_Controls_Tabs('tbViews');
	//tbViews.AddTab("Log", cvLog);
	//tbViews.AddTab("Debug", cvDebug);
	this.$dlgWindow.addChild(this.$tbViews);
};
$Lifelike_JScript_Admin_Modules_Log_ConsoleModule.prototype = {
	preRender: function() {
		this.set_cssClass('consoleModule');
		//dlgWindow.Options = new DialogOptions()
		//{
		//	AutoOpen = true,
		//	CloseOnEscape = false,
		//	Height = 300,
		//	Width = 500,
		//	Title = "Console"
		//};
		this.addChild(this.$dlgWindow);
		this.$dlgWindow.addChild(this.$tbViews);
		this.$tbViews.addTab('Log', this.$cvLog);
		this.$tbViews.addTab('Debug', this.$cvDebug);
		//txtInput.CssClass = "input";
		//btnSend.Text = "Send";
		//dlgWindow.AddChild(txtInput);
		//dlgWindow.AddChild(btnSend);
	},
	postRender: function() {
		$Lifelike_JScript_Admin_Managers_PageManager.get_context().get_panelLayout().get_pnlBottom().dropControl(this.$dlgWindow);
		$Lifelike_JScript_Admin_Control.prototype.postRender.call(this);
	},
	log: function(message, arr) {
		this.$cvLog.logMessage(message, arr);
	},
	debug: function(message, arr) {
		this.$cvDebug.logMessage(message, arr);
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Modules.Panels.DockableControl
var $Lifelike_JScript_Admin_Modules_Panels_DockableControl = function(name) {
	this.$_headerContainer = null;
	this.$_header = null;
	this.$2$draggableOptionsField = null;
	$Lifelike_JScript_Admin_Control.call(this, name);
	this.$_header = new $Lifelike_JScript_Admin_Controls_Label('Header');
	this.$_headerContainer = new $Lifelike_JScript_Admin_Modules_Chat_BaseControl('HeaderContainer');
	this.$_headerContainer.set_cssClass('ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix');
	this.$_header.set_cssClass('ui-dialog-title');
	this.set_cssClass('ui-dialog ui-widget ui-widget-content ui-corner-all');
	this.set_draggableOptions({ handle: this.$_headerContainer.get_controlContainer(), zIndex: 10, scope: 'draggable' });
	this.$_headerContainer.addChild(this.$_header);
	this.addChild(this.$_headerContainer);
};
$Lifelike_JScript_Admin_Modules_Panels_DockableControl.prototype = {
	get_title: function() {
		return this.$_header.get_text();
	},
	set_title: function(value) {
		this.$_header.set_text(value);
	},
	get_draggableOptions: function() {
		return this.$2$draggableOptionsField;
	},
	set_draggableOptions: function(value) {
		this.$2$draggableOptionsField = value;
	},
	dockableControl_Draggable_OnCreate: function(e) {
	},
	preRender: function() {
	},
	postRender: function() {
		$(this.get_controlContainer()).draggable(this.get_draggableOptions());
		$Lifelike_JScript_Admin_Control.prototype.postRender.call(this);
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Modules.Panels.Panel
var $Lifelike_JScript_Admin_Modules_Panels_Panel = function(name) {
	this.$2$droppableOptionsField = null;
	$Lifelike_JScript_Admin_Control.call(this, name);
	this.set_droppableOptions({ scope: 'draggable', activate: Function.mkdel(this, this.$onActive), deactivate: Function.mkdel(this, this.$onDeactivate), drop: Function.mkdel(this, this.$onDrop), out: Function.mkdel(this, this.$onOut), over: Function.mkdel(this, this.$onOver) });
	this.set_cssClass('droppable');
};
$Lifelike_JScript_Admin_Modules_Panels_Panel.prototype = {
	get_droppableOptions: function() {
		return this.$2$droppableOptionsField;
	},
	set_droppableOptions: function(value) {
		this.$2$droppableOptionsField = value;
	},
	preRender: function() {
		//jQuery.FromElement(ControlContainer).CSS("position", "absolute");
		//jQuery.FromElement(ControlContainer).CSS("border", "solid");
	},
	postRender: function() {
		$Lifelike_JScript_Admin_Util.console().log('.modules.panels.panel.postrender ', [this.get_droppableOptions(), this.get_controlContainer()]);
		$(this.get_controlContainer()).droppable(this.get_droppableOptions());
		$Lifelike_JScript_Admin_Control.prototype.postRender.call(this);
	},
	$onActive: function(e, dae) {
		//Window.Alert("act");
	},
	$onDeactivate: function(e, dae) {
		//Window.Alert("deact");
	},
	$onDrop: function(e, dae) {
		var id = dae.draggable.attr('id');
		$Lifelike_JScript_Admin_Util.console().log('.modules.panels.panel.ondrop ', [id]);
		var control = Type.cast($Lifelike_JScript_Admin_Managers_PageManager.get_context().getControlByClientId(id), $Lifelike_JScript_Admin_Modules_Panels_DockableControl);
		this.dropControl(control);
	},
	$onOut: function(e, dae) {
		//Window.Alert("out");
	},
	$onOver: function(e, dae) {
		//Window.Alert("over");
	},
	dropControl: function(control) {
		var spacer = 5;
		var left = parseInt(this.get_left()) + spacer;
		var top = parseInt(this.get_top()) + spacer;
		control.set_left(left + 'px');
		control.set_top(top + 'px');
		;
		control.set_width((parseInt(this.get_width()) - spacer * 2).toString());
		control.set_height((parseInt(this.get_height()) - spacer * 2).toString());
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Modules.Panels.PanelLayout
var $Lifelike_JScript_Admin_Modules_Panels_PanelLayout = function(name) {
	this.$2$pnlLeftSideField = null;
	this.$2$pnlRightSideField = null;
	this.$2$pnlMiddleField = null;
	this.$2$pnlBottomField = null;
	$Lifelike_JScript_Admin_Control.call(this, name);
	this.set_pnlLeftSide(new $Lifelike_JScript_Admin_Modules_Panels_Panel('pnlLeftSide'));
	this.set_pnlRightSide(new $Lifelike_JScript_Admin_Modules_Panels_Panel('pnlRightSide'));
	this.set_pnlMiddle(new $Lifelike_JScript_Admin_Modules_Panels_Panel('pnlMiddle'));
	this.set_pnlBottom(new $Lifelike_JScript_Admin_Modules_Panels_Panel('pnlBottom'));
	window.addEventListener('resize', Function.mkdel(this, this.resize));
	this.addChild(this.get_pnlLeftSide());
	this.addChild(this.get_pnlRightSide());
	this.addChild(this.get_pnlMiddle());
	this.addChild(this.get_pnlBottom());
};
$Lifelike_JScript_Admin_Modules_Panels_PanelLayout.prototype = {
	get_pnlLeftSide: function() {
		return this.$2$pnlLeftSideField;
	},
	set_pnlLeftSide: function(value) {
		this.$2$pnlLeftSideField = value;
	},
	get_pnlRightSide: function() {
		return this.$2$pnlRightSideField;
	},
	set_pnlRightSide: function(value) {
		this.$2$pnlRightSideField = value;
	},
	get_pnlMiddle: function() {
		return this.$2$pnlMiddleField;
	},
	set_pnlMiddle: function(value) {
		this.$2$pnlMiddleField = value;
	},
	get_pnlBottom: function() {
		return this.$2$pnlBottomField;
	},
	set_pnlBottom: function(value) {
		this.$2$pnlBottomField = value;
	},
	resize: function(e) {
		var w = $(window).width();
		var h = $(window).height();
		var bottomHeight = 250;
		var bottomTop = h - bottomHeight;
		var sidePanelWidth = ss.Int32.trunc(w * 0.2);
		var middleWidth = w - sidePanelWidth * 2;
		var rightsideLeft = middleWidth + sidePanelWidth;
		this.get_pnlLeftSide().set_height(bottomTop.toString());
		this.get_pnlLeftSide().set_width(sidePanelWidth.toString());
		this.get_pnlLeftSide().set_top('0px');
		this.get_pnlLeftSide().set_left('0px');
		this.get_pnlMiddle().set_width(middleWidth.toString());
		this.get_pnlMiddle().set_top('0px');
		this.get_pnlMiddle().set_left(sidePanelWidth + 'px');
		this.get_pnlMiddle().set_height(bottomTop.toString());
		this.get_pnlRightSide().set_height(bottomTop.toString());
		this.get_pnlRightSide().set_width(sidePanelWidth.toString());
		this.get_pnlRightSide().set_top('0px');
		this.get_pnlRightSide().set_left(rightsideLeft + 'px');
		this.get_pnlBottom().set_height(bottomHeight.toString());
		this.get_pnlBottom().set_top(bottomTop + 'px');
		this.get_pnlBottom().set_left('0px');
		this.get_pnlBottom().set_width(w.toString());
	},
	preRender: function() {
	},
	postRender: function() {
		this.resize(null);
		$Lifelike_JScript_Admin_Control.prototype.postRender.call(this);
	}
};
Type.registerClass(null, 'Lifelike.JScript.Admin.$Program', $Lifelike_JScript_Admin_$Program, Object);
Type.registerClass(global, 'Lifelike.JScript.Admin.Control', $Lifelike_JScript_Admin_Control, Object);
Type.registerClass(global, 'Lifelike.JScript.Admin.Guid', $Lifelike_JScript_Admin_Guid, Object);
Type.registerClass(global, 'Lifelike.JScript.Admin.PageRenderer', $Lifelike_JScript_Admin_PageRenderer, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Util', $Lifelike_JScript_Admin_Util, Object);
Type.registerClass(global, 'Lifelike.JScript.Admin.Controls.Button', $Lifelike_JScript_Admin_Controls_Button, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Controls.Dialog', $Lifelike_JScript_Admin_Controls_Dialog, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Controls.Image', $Lifelike_JScript_Admin_Controls_Image, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Controls.Label', $Lifelike_JScript_Admin_Controls_Label, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Controls.List', $Lifelike_JScript_Admin_Controls_List, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Controls.ListItem', $Lifelike_JScript_Admin_Controls_ListItem, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Controls.Tabs', $Lifelike_JScript_Admin_Controls_Tabs, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Controls.TextBox', $Lifelike_JScript_Admin_Controls_TextBox, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Controls.Tree', $Lifelike_JScript_Admin_Controls_Tree, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Controls.TreeNode', $Lifelike_JScript_Admin_Controls_TreeNode, $Lifelike_JScript_Admin_Control);
Type.registerInterface(global, 'Lifelike.JScript.Admin.Interfaces.ILogin', $Lifelike_JScript_Admin_Interfaces_ILogin, []);
Type.registerClass(global, 'Lifelike.JScript.Admin.Managers.HubManager', $Lifelike_JScript_Admin_Managers_HubManager, Object);
Type.registerClass(global, 'Lifelike.JScript.Admin.Managers.LoginManager', $Lifelike_JScript_Admin_Managers_LoginManager, Object);
Type.registerClass(global, 'Lifelike.JScript.Admin.Managers.PageManager', $Lifelike_JScript_Admin_Managers_PageManager, Object);
Type.registerClass(global, 'Lifelike.JScript.Admin.Modules.Chat.BaseControl', $Lifelike_JScript_Admin_Modules_Chat_BaseControl, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Modules.Chat.ChatModule', $Lifelike_JScript_Admin_Modules_Chat_ChatModule, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Modules.Chat.MessageControl', $Lifelike_JScript_Admin_Modules_Chat_MessageControl, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Modules.Chat.MessengerControl', $Lifelike_JScript_Admin_Modules_Chat_MessengerControl, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Modules.Chat.RoomControl', $Lifelike_JScript_Admin_Modules_Chat_RoomControl, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Modules.Chat.UserControl', $Lifelike_JScript_Admin_Modules_Chat_UserControl, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Modules.Console.ConsoleView', $Lifelike_JScript_Admin_Modules_Console_ConsoleView, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Modules.Item.ItemTreeModule', $Lifelike_JScript_Admin_Modules_Item_ItemTreeModule, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Modules.Log.ConsoleModule', $Lifelike_JScript_Admin_Modules_Log_ConsoleModule, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Modules.Panels.DockableControl', $Lifelike_JScript_Admin_Modules_Panels_DockableControl, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Modules.Panels.Panel', $Lifelike_JScript_Admin_Modules_Panels_Panel, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Modules.Panels.PanelLayout', $Lifelike_JScript_Admin_Modules_Panels_PanelLayout, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.LoginForm', $Lifelike_JScript_Admin_LoginForm, $Lifelike_JScript_Admin_Control, $Lifelike_JScript_Admin_Interfaces_ILogin);
$Lifelike_JScript_Admin_PageRenderer.$_context = null;
$Lifelike_JScript_Admin_Managers_HubManager.$_context = null;
$Lifelike_JScript_Admin_Managers_PageManager.$_context = null;
$Lifelike_JScript_Admin_$Program.$main();
