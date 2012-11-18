////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Program
var $Lifelike_JScript_Admin_$Program = function() {
};
$Lifelike_JScript_Admin_$Program.$main = function() {
	$(function() {
		//Tree tree = new Tree(".itemEditor");
		//var node = new Node() { Text = "Text", Value = "Value" };
		//node.Children = new List<Node>()
		// {
		//	 new Node() { Text = "Text", Value = "Value" , Parent = node },
		//	 new Node() { Text = "Text", Value = "Value", Parent = node },
		//	 new Node() { Text = "Text", Value = "Value", Parent = node },
		// };
		//var ss = new Node() { Text = "Text", Value = "Value", Parent = node };
		//ss.Children = new List<Node>()
		// {
		//	 new Node() { Text = "Text", Value = "Value" , Parent = node },
		//	 new Node() { Text = "Text", Value = "Value", Parent = node },
		//	 new Node() { Text = "Text", Value = "Value", Parent = node },
		// };
		//node.Children.Add(ss);
		//tree.AddNode(null, node);
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
		$Lifelike_JScript_Admin_Managers_HubManager.get_context().initialise();
		$Lifelike_JScript_Admin_Managers_PageManager.get_context().initialise();
		$Lifelike_JScript_Admin_PageRenderer.get_context().render();
	});
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
		this.$setClientId();
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
	get_children: function() {
		return this.$_children;
	},
	addChild: function(control) {
		control.set_parent(this);
		if (this.get_isRendered()) {
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
			this.$setClientId();
			if (ss.isValue(this.get_parent()) && ss.isValue(this.get_parent().get_controlContainer())) {
				this.get_parent().get_controlContainer().appendChild(this.get_controlContainer());
			}
			this.preRender();
		}
		var $t1 = this.get_children();
		for (var $t2 = 0; $t2 < $t1.length; $t2++) {
			var c = $t1[$t2];
			c.render();
			if (!this.get_isRendered()) {
				c.postRender();
			}
		}
		this.set_isRendered(true);
	},
	$setClientId: function() {
		var control = this;
		var result = '';
		while (ss.isValue(control)) {
			result = control.get_name() + '_' + result;
			control = control.get_parent();
		}
		this.set_clientId(result);
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
	this.$_loginManager.loginResponseEvent = Function.mkdel(this, this.$loginResponse);
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
		this.get_controlContainer().appendChild(this.$_element);
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Interfaces.ILogin
var $Lifelike_JScript_Admin_Interfaces_ILogin = function() {
};
$Lifelike_JScript_Admin_Interfaces_ILogin.prototype = { get_username: null, get_password: null, get_remember: null };
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.jQueryUI.Node
var $Lifelike_JScript_Admin_jQueryUI_Node = function() {
	this.$_nodes = null;
	this.$1$TreeField = null;
	this.$1$ElementField = null;
	this.$1$ImageField = null;
	this.$1$TextElementField = null;
	this.$1$ContainerElementField = null;
	this.$1$LabelElementField = null;
	this.$1$ParentField = null;
	this.set_element(document.createElement('li'));
	this.set_labelElement(document.createElement('div'));
	this.get_labelElement().setAttribute('class', 'label');
	this.set_image(document.createElement('span'));
	this.get_image().setAttribute('class', 'ui-icon ui-icon-carat-1-e');
	$(this.get_image()).click(Function.mkdel(this, function(p) {
		if (this.get_expanded()) {
			this.close();
		}
		else {
			this.expand();
		}
	}));
	this.set_textElement(document.createElement('div'));
	this.get_labelElement().appendChild(this.get_image());
	this.get_labelElement().appendChild(this.get_textElement());
	this.get_element().appendChild(this.get_labelElement());
	this.set_id($Lifelike_JScript_Admin_Guid.createGuid());
};
$Lifelike_JScript_Admin_jQueryUI_Node.prototype = {
	get_id: function() {
		return this.$getProperties(this.get_element(), 'id');
	},
	set_id: function(value) {
		this.$setProperties(this.get_element(), 'id', value);
	},
	get_tree: function() {
		return this.$1$TreeField;
	},
	set_tree: function(value) {
		this.$1$TreeField = value;
	},
	get_value: function() {
		return this.$getProperties(this.get_element(), 'value');
	},
	set_value: function(value) {
		this.$setProperties(this.get_element(), 'value', value);
	},
	get_text: function() {
		return $(this.get_textElement()).text();
	},
	set_text: function(value) {
		$(this.get_textElement()).text(value);
	},
	get_expanded: function() {
		var c = this.$getProperties(this.get_element(), 'expanded');
		if (ss.isValue(c)) {
			return Boolean.parse(c);
		}
		return false;
	},
	set_expanded: function(value) {
		this.$setProperties(this.get_element(), 'expanded', value.toString());
	},
	get_index: function() {
		return parseInt(this.$getProperties(this.get_element(), 'expanded'));
	},
	set_index: function(value) {
		this.$setProperties(this.get_element(), 'expanded', value.toString());
	},
	get_element: function() {
		return this.$1$ElementField;
	},
	set_element: function(value) {
		this.$1$ElementField = value;
	},
	get_image: function() {
		return this.$1$ImageField;
	},
	set_image: function(value) {
		this.$1$ImageField = value;
	},
	get_textElement: function() {
		return this.$1$TextElementField;
	},
	set_textElement: function(value) {
		this.$1$TextElementField = value;
	},
	get_containerElement: function() {
		return this.$1$ContainerElementField;
	},
	set_containerElement: function(value) {
		this.$1$ContainerElementField = value;
	},
	get_labelElement: function() {
		return this.$1$LabelElementField;
	},
	set_labelElement: function(value) {
		this.$1$LabelElementField = value;
	},
	get_parent: function() {
		return this.$1$ParentField;
	},
	set_parent: function(value) {
		this.$1$ParentField = value;
	},
	get_children: function() {
		return this.$_nodes;
	},
	set_children: function(value) {
		if (ss.isNullOrUndefined(value) && ss.isValue(this.$_nodes)) {
			for (var $t1 = 0; $t1 < this.$_nodes.length; $t1++) {
				var v = this.$_nodes[$t1];
				v.delete();
			}
			this.$deleteContainer();
		}
		else if (ss.isValue(value) && ss.isNullOrUndefined(this.$_nodes)) {
			this.$createContainer();
		}
		this.$_nodes = value;
	},
	$deleteContainer: function() {
		$(this.get_containerElement()).remove();
	},
	$createContainer: function() {
		this.set_containerElement(document.createElement('ul'));
		this.get_containerElement().setAttribute('class', 'container');
		this.get_element().appendChild(this.get_containerElement());
	},
	$getProperties: function(element, name) {
		return Type.cast(element.getAttribute('data-' + name), String);
	},
	$setProperties: function(element, name, value) {
		element.setAttribute('data-' + name, value);
	},
	delete: function() {
		if (ss.isValue(this.get_children())) {
			var $t1 = this.get_children();
			for (var $t2 = 0; $t2 < $t1.length; $t2++) {
				var n = $t1[$t2];
				n.delete();
			}
			this.set_children(null);
		}
		$(this.get_element()).remove();
		this.get_parent().get_children().remove(this);
	},
	expand: function() {
		this.set_expanded(true);
		if (ss.isValue(this.get_children())) {
			var $t1 = this.get_children();
			for (var $t2 = 0; $t2 < $t1.length; $t2++) {
				var e = $t1[$t2];
				e.render(this.get_containerElement());
			}
		}
	},
	close: function() {
		this.set_expanded(false);
		if (ss.isValue(this.get_children())) {
			var $t1 = this.get_children();
			for (var $t2 = 0; $t2 < $t1.length; $t2++) {
				var e = $t1[$t2];
				e.close();
				$(e.get_element()).remove();
			}
		}
	},
	render: function(parent) {
		if (ss.isNullOrUndefined(this.get_parent()) || this.get_parent().get_expanded()) {
			parent.appendChild(this.get_element());
			if (ss.isValue(this.get_children())) {
				var $t1 = this.get_children();
				for (var $t2 = 0; $t2 < $t1.length; $t2++) {
					var e = $t1[$t2];
					e.render(this.get_containerElement());
				}
			}
		}
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.jQueryUI.Tree
var $Lifelike_JScript_Admin_jQueryUI_Tree = function(selector) {
	this.$_element = null;
	this.$_containerElement = null;
	this.$_treeCollection = null;
	this.$_treeCollection = [];
	this.$_element = $(selector).get(0);
};
$Lifelike_JScript_Admin_jQueryUI_Tree.prototype = {
	addNode: function(parent, newnode) {
		newnode.set_tree(this);
		if (ss.isValue(parent)) {
			newnode.set_parent(parent);
			if (ss.isNullOrUndefined(parent.get_children())) {
				parent.set_children([]);
			}
			parent.get_children().add(newnode);
		}
		else {
			this.$_treeCollection.add(newnode);
		}
	},
	findNode$1: function(collection, func) {
		for (var $t1 = 0; $t1 < collection.length; $t1++) {
			var v = collection[$t1];
			if (func(v)) {
				return v;
			}
			else if (ss.isValue(v.get_children())) {
				return this.findNode$1(v.get_children(), func);
			}
		}
		return null;
	},
	findNode: function(func) {
		return this.findNode$1(this.$_treeCollection, func);
	},
	removeNode: function(node) {
		node.get_parent().get_children().remove(node);
	},
	refreshTree: function(node) {
		//TODO: if visible then refresh
	},
	$render: function() {
		this.$createContainer();
		for (var $t1 = 0; $t1 < this.$_treeCollection.length; $t1++) {
			var v = this.$_treeCollection[$t1];
			v.render(this.$_containerElement);
		}
	},
	$deleteContainer: function() {
		$(this.$_containerElement).remove();
	},
	$createContainer: function() {
		this.$_containerElement = document.createElement('ul');
		this.$_containerElement.setAttribute('class', 'tree');
		this.$_element.appendChild(this.$_containerElement);
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Managers.HubManager
var $Lifelike_JScript_Admin_Managers_HubManager = function() {
};
$Lifelike_JScript_Admin_Managers_HubManager.prototype = {
	initialise: function() {
		//GetConnection().chat.addMessage = new ChatMessage(msg => {  });
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
	this.loginResponseEvent = null;
	this.$_inf = null;
	this.$_inf = login;
};
$Lifelike_JScript_Admin_Managers_LoginManager.prototype = {
	$loginUser: function() {
		$.connection.auth.server.login(this.$_inf.get_username(), this.$_inf.get_password(), this.$_inf.get_remember());
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Managers.PageManager
var $Lifelike_JScript_Admin_Managers_PageManager = function() {
	this.$1$UsernameField = null;
	this.$1$IsLoggedInField = false;
	this.$1$ConsoleModuleField = null;
	this.$_loginForm = null;
	this.$1$hasRenderedField = false;
	this.$_loginForm = new $Lifelike_JScript_Admin_LoginForm('frmLogin');
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
	get_hasRendered: function() {
		return this.$1$hasRenderedField;
	},
	set_hasRendered: function(value) {
		this.$1$hasRenderedField = value;
	},
	initialise: function() {
		$.connection.auth.client.loginResponse = Function.mkdel(this, this.$loginResponse);
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
		var console = new $Lifelike_JScript_Admin_Modules_Log_ConsoleModule('console');
		var chatModule = new $Lifelike_JScript_Admin_Modules_Chat_ChatModule('chat');
		//Util.Debugger();
		$Lifelike_JScript_Admin_PageRenderer.get_context().addChild(console);
		$Lifelike_JScript_Admin_PageRenderer.get_context().addChild(chatModule);
		chatModule.render();
		console.render();
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
	$Lifelike_JScript_Admin_Control.call(this, name);
	//_element = Document.CreateElement("a");
	//ControlContainer.AppendChild(_element);
	this.set_$rooms([]);
	this.set_cssClass('chatbar');
	$.connection.chat.client.recieveMessage = Function.mkdel(this, this.recieveMessageResponse);
	$.connection.chat.client.getCurrentRoomsResponse = Function.mkdel(this, this.getCurrentRoomsResponse);
	$.connection.chat.client.getAvailableRoomsResponse = Function.mkdel(this, this.getAvailableRoomsResponse);
	$.connection.chat.client.registerNameResponse = Function.mkdel(this, this.registerNameResponse);
	$.connection.chat.client.joinRoomResponse = Function.mkdel(this, this.joinRoomResponse);
	console.log('registerName');
	this.registerName($Lifelike_JScript_Admin_Managers_PageManager.get_context().get_username());
};
$Lifelike_JScript_Admin_Modules_Chat_ChatModule.prototype = {
	get_$rooms: function() {
		return this.$2$RoomsField;
	},
	set_$rooms: function(value) {
		this.$2$RoomsField = value;
	},
	joinRoomResponse: function(success) {
		console.log('joinRoomResponse' + success);
	},
	registerNameResponse: function(success) {
		console.log('joinRoom');
		this.joinRoom($Lifelike_JScript_Admin_Managers_PageManager.get_context().get_username());
	},
	recieveMessageResponse: function(room, user, message) {
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
			roomEnt = new $Lifelike_JScript_Admin_Modules_Chat_RoomControl(room);
			this.get_children().add(roomEnt);
			roomEnt.render();
		}
		roomEnt.$addNewMessage(user, message);
	},
	getCurrentRoomsResponse: function(rooms) {
	},
	getAvailableRoomsResponse: function(rooms) {
	},
	registerName: function(name) {
		$.connection.chat.server.registerName($Lifelike_JScript_Admin_Managers_PageManager.get_context().get_username());
	},
	sendMessage: function(room, message) {
		$.connection.chat.server.sendMessage(room, message);
	},
	getCurrentRooms: function() {
		$.connection.chat.server.getCurrentRooms();
	},
	getAvailableRooms: function() {
		$.connection.chat.server.getAvailableRooms();
	},
	joinRoom: function(room) {
		$.connection.chat.server.joinRoom(room);
	},
	leaveRoom: function(room) {
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
	this.$_txtMessage = null;
	this.$_btnSend = null;
	$Lifelike_JScript_Admin_Control.call(this, name);
	this.$_txtMessage = new $Lifelike_JScript_Admin_Controls_TextBox('txtMessage');
	this.$_btnSend = new $Lifelike_JScript_Admin_Controls_Button('btnSend');
	this.$_btnSend.set_text('Send');
	this.addChild(this.$_txtMessage);
	this.addChild(this.$_btnSend);
};
$Lifelike_JScript_Admin_Modules_Chat_MessengerControl.prototype = {
	preRender: function() {
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Modules.Chat.RoomControl
var $Lifelike_JScript_Admin_Modules_Chat_RoomControl = function(name) {
	this.$2$ColourField = null;
	this.$_title = null;
	this.$_messageContainer = null;
	this.$_messenger = null;
	$Lifelike_JScript_Admin_Control.call(this, name);
	this.$_messageContainer = new $Lifelike_JScript_Admin_Modules_Chat_BaseControl('MessageContainer');
	this.$_messageContainer.set_cssClass('messageContainer');
	this.$_messenger = new $Lifelike_JScript_Admin_Modules_Chat_MessengerControl('Messenger');
	this.$_messenger.set_cssClass('messenger');
	this.$_title = new $Lifelike_JScript_Admin_Controls_Label(name);
	this.$_title.set_cssClass('title');
	this.$_title.set_text(name);
	this.addChild(this.$_title);
	this.addChild(this.$_messageContainer);
	this.addChild(this.$_messenger);
};
$Lifelike_JScript_Admin_Modules_Chat_RoomControl.prototype = {
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
	preRender: function() {
	},
	$addNewMessage: function(user, message) {
		var msg = new $Lifelike_JScript_Admin_Modules_Chat_MessageControl('message' + this.$_messageContainer.get_children().length + 1);
		msg.set_username(user);
		msg.set_message(message);
		this.$_messageContainer.get_children().add(msg);
		msg.render();
	}
};
////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Modules.Log.ConsoleModule
var $Lifelike_JScript_Admin_Modules_Log_ConsoleModule = function(name) {
	this.$lblMessages = null;
	this.$txtInput = null;
	this.$btnSend = null;
	this.$dlgWindow = null;
	$Lifelike_JScript_Admin_Control.call(this, name);
	this.$lblMessages = new $Lifelike_JScript_Admin_Controls_Label('lblMessages');
	this.$txtInput = new $Lifelike_JScript_Admin_Controls_TextBox('txtInput');
	this.$btnSend = new $Lifelike_JScript_Admin_Controls_Button('btnSend');
	this.$dlgWindow = new $Lifelike_JScript_Admin_Controls_Dialog('dlgWindow');
};
$Lifelike_JScript_Admin_Modules_Log_ConsoleModule.prototype = {
	preRender: function() {
		this.set_cssClass('consoleModule');
		this.$dlgWindow.set_options({ autoOpen: true, closeOnEscape: false, height: 300, width: 500, title: 'Console' });
		this.$lblMessages.set_cssClass('messages');
		this.$txtInput.set_cssClass('input');
		this.$btnSend.set_text('Send');
		this.$dlgWindow.addChild(this.$lblMessages);
		this.$dlgWindow.addChild(this.$txtInput);
		this.$dlgWindow.addChild(this.$btnSend);
		this.addChild(this.$dlgWindow);
	},
	postRender: function() {
		$Lifelike_JScript_Admin_Control.prototype.postRender.call(this);
	},
	logMessage: function(message) {
		this.$lblMessages.set_text(this.$lblMessages.get_text() + '<br/>' + message);
	}
};
Type.registerClass(null, 'Lifelike.JScript.Admin.$Program', $Lifelike_JScript_Admin_$Program, Object);
Type.registerClass(global, 'Lifelike.JScript.Admin.Control', $Lifelike_JScript_Admin_Control, Object);
Type.registerClass(global, 'Lifelike.JScript.Admin.Guid', $Lifelike_JScript_Admin_Guid, Object);
Type.registerClass(global, 'Lifelike.JScript.Admin.PageRenderer', $Lifelike_JScript_Admin_PageRenderer, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Util', $Lifelike_JScript_Admin_Util, Object);
Type.registerClass(global, 'Lifelike.JScript.Admin.Controls.Button', $Lifelike_JScript_Admin_Controls_Button, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Controls.Dialog', $Lifelike_JScript_Admin_Controls_Dialog, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Controls.Label', $Lifelike_JScript_Admin_Controls_Label, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Controls.TextBox', $Lifelike_JScript_Admin_Controls_TextBox, $Lifelike_JScript_Admin_Control);
Type.registerInterface(global, 'Lifelike.JScript.Admin.Interfaces.ILogin', $Lifelike_JScript_Admin_Interfaces_ILogin, []);
Type.registerClass(global, 'Lifelike.JScript.Admin.jQueryUI.Node', $Lifelike_JScript_Admin_jQueryUI_Node, Object);
Type.registerClass(global, 'Lifelike.JScript.Admin.jQueryUI.Tree', $Lifelike_JScript_Admin_jQueryUI_Tree, Object);
Type.registerClass(global, 'Lifelike.JScript.Admin.Managers.HubManager', $Lifelike_JScript_Admin_Managers_HubManager, Object);
Type.registerClass(global, 'Lifelike.JScript.Admin.Managers.LoginManager', $Lifelike_JScript_Admin_Managers_LoginManager, Object);
Type.registerClass(global, 'Lifelike.JScript.Admin.Managers.PageManager', $Lifelike_JScript_Admin_Managers_PageManager, Object);
Type.registerClass(global, 'Lifelike.JScript.Admin.Modules.Chat.BaseControl', $Lifelike_JScript_Admin_Modules_Chat_BaseControl, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Modules.Chat.ChatModule', $Lifelike_JScript_Admin_Modules_Chat_ChatModule, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Modules.Chat.MessageControl', $Lifelike_JScript_Admin_Modules_Chat_MessageControl, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Modules.Chat.MessengerControl', $Lifelike_JScript_Admin_Modules_Chat_MessengerControl, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Modules.Chat.RoomControl', $Lifelike_JScript_Admin_Modules_Chat_RoomControl, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.Modules.Log.ConsoleModule', $Lifelike_JScript_Admin_Modules_Log_ConsoleModule, $Lifelike_JScript_Admin_Control);
Type.registerClass(global, 'Lifelike.JScript.Admin.LoginForm', $Lifelike_JScript_Admin_LoginForm, $Lifelike_JScript_Admin_Control, $Lifelike_JScript_Admin_Interfaces_ILogin);
$Lifelike_JScript_Admin_PageRenderer.$_context = null;
$Lifelike_JScript_Admin_Managers_HubManager.$_context = null;
$Lifelike_JScript_Admin_Managers_PageManager.$_context = null;
$Lifelike_JScript_Admin_$Program.$main();
