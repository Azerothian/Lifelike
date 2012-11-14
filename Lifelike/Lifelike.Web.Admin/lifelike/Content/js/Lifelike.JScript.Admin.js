////////////////////////////////////////////////////////////////////////////////
// Lifelike.JScript.Admin.Program
var $Lifelike_JScript_Admin_$Program = function() {
};
$Lifelike_JScript_Admin_$Program.$main = function() {
	$(function() {
		var tree = new $Lifelike_JScript_Admin_jQueryUI_Tree('.itemEditor');
		var $t1 = new $Lifelike_JScript_Admin_jQueryUI_Node();
		$t1.set_text('Text');
		$t1.set_value('Value');
		var node = $t1;
		var $t2 = [];
		var $t3 = new $Lifelike_JScript_Admin_jQueryUI_Node();
		$t3.set_text('Text');
		$t3.set_value('Value');
		$t3.set_parent(node);
		$t2.add($t3);
		var $t4 = new $Lifelike_JScript_Admin_jQueryUI_Node();
		$t4.set_text('Text');
		$t4.set_value('Value');
		$t4.set_parent(node);
		$t2.add($t4);
		var $t5 = new $Lifelike_JScript_Admin_jQueryUI_Node();
		$t5.set_text('Text');
		$t5.set_value('Value');
		$t5.set_parent(node);
		$t2.add($t5);
		node.set_children($t2);
		var $t6 = new $Lifelike_JScript_Admin_jQueryUI_Node();
		$t6.set_text('Text');
		$t6.set_value('Value');
		$t6.set_parent(node);
		var ss = $t6;
		var $t7 = [];
		var $t8 = new $Lifelike_JScript_Admin_jQueryUI_Node();
		$t8.set_text('Text');
		$t8.set_value('Value');
		$t8.set_parent(node);
		$t7.add($t8);
		var $t9 = new $Lifelike_JScript_Admin_jQueryUI_Node();
		$t9.set_text('Text');
		$t9.set_value('Value');
		$t9.set_parent(node);
		$t7.add($t9);
		var $t10 = new $Lifelike_JScript_Admin_jQueryUI_Node();
		$t10.set_text('Text');
		$t10.set_value('Value');
		$t10.set_parent(node);
		$t7.add($t10);
		ss.set_children($t7);
		node.get_children().add(ss);
		tree.addNode(null, node);
		tree.$render();
		$('.itemEditor').dialog({ autoOpen: true, width: 400, title: 'ITEM EDITOR' });
		$('.button').button();
		$('.expand').click(function(p) {
			node.expand();
		});
		$('.close').click(function(p1) {
			node.close();
		});
		var hubmanger = new $Lifelike_JScript_Admin_HubManager();
		hubmanger.initialise();
	});
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
// Lifelike.JScript.Admin.HubManager
var $Lifelike_JScript_Admin_HubManager = function() {
};
$Lifelike_JScript_Admin_HubManager.prototype = {
	initialise: function() {
		$.connection.chat.addMessage = function(msg) {
			window.alert(msg);
		};
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
		window.alert('CONNECTED');
		$.connection.chat.sendMessage('HI!');
	}
};
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
Type.registerClass(null, 'Lifelike.JScript.Admin.$Program', $Lifelike_JScript_Admin_$Program, Object);
Type.registerClass(global, 'Lifelike.JScript.Admin.Guid', $Lifelike_JScript_Admin_Guid, Object);
Type.registerClass(global, 'Lifelike.JScript.Admin.HubManager', $Lifelike_JScript_Admin_HubManager, Object);
Type.registerClass(global, 'Lifelike.JScript.Admin.jQueryUI.Node', $Lifelike_JScript_Admin_jQueryUI_Node, Object);
Type.registerClass(global, 'Lifelike.JScript.Admin.jQueryUI.Tree', $Lifelike_JScript_Admin_jQueryUI_Tree, Object);
$Lifelike_JScript_Admin_$Program.$main();
