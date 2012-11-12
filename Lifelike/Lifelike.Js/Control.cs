using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Html;
namespace Lifelike.Js
{
		public delegate void BasicControlEventHandler(Control control);
		public abstract class Control
		{
			public Control()
			{
				_children = new List<Control>();
				//ControlEventsManager = new PandoraEventsManager(this);
				SetEvents();
			}
			private void SetEvents()
			{

			}
			//protected PandoraEventsManager ControlEventsManager;



			#region Private Fields

			private bool _isUnloading = false;
			private string _id;
			private List<Control> _children;
			private bool _loadingJsFiles = false;
			private bool _loadingCssFiles = false;
			
			private FloatPosition _float = FloatPosition.DoNotRender;
			private DisplayType _displayType = DisplayType.DoNotRender;
			private int _jsFilesLoaded = 0;
			private int _cssFilesLoaded = 0;
			private string _left = "";
			private string _top = "";
			private string _right = "";
			private string _width = "";
			private string _height = "";
			private PositionType _positionType = PositionType.DoNotRender;
			private string _cssClass = "";
			private bool _isInitialised = false;
			private bool _isRendered = false;
			private Control _parent;

			#endregion

			#region Events

			public PandoraEventManager OnChange
			{
				get
				{
					return ControlEventsManager["change"];
				}
			}
			public PandoraEventManager OnClick
			{
				get
				{
					return ControlEventsManager["click"];
				}
			}
			public PandoraEventManager OnDoubleClick
			{
				get
				{
					return ControlEventsManager["dblclick"];
				}
			}

			public PandoraEventManager OnBlur
			{
				get
				{
					return ControlEventsManager["blur"];
				}
			}

			public PandoraEventManager OnFocus
			{
				get
				{
					return ControlEventsManager["focus"];
				}
			}
			public PandoraEventManager OnMouseDown
			{
				get
				{
					return ControlEventsManager["mousedown"];
				}
			}
			public PandoraEventManager OnMouseMove
			{
				get
				{
					return ControlEventsManager["mousemove"];
				}
			}

			public PandoraEventManager OnMouseOut
			{
				get
				{
					return ControlEventsManager["mouseout"];
				}
			}
			public PandoraEventManager OnMouseOver
			{
				get
				{
					return ControlEventsManager["mouseover"];
				}
			}
			public PandoraEventManager OnMouseUp
			{
				get
				{
					return ControlEventsManager["mouseup"];
				}
			}
			public PandoraEventManager OnKeyDown
			{
				get
				{
					return ControlEventsManager["keydown"];
				}
			}
			public PandoraEventManager OnKeyPress
			{
				get
				{
					return ControlEventsManager["keypress"];
				}
			}
			public PandoraEventManager OnKeyUp
			{
				get
				{
					return ControlEventsManager["keyup"];
				}
			}
			public PandoraEventManager OnSelect
			{
				get
				{
					return ControlEventsManager["onselect"];
				}
			}


			private BasicControlEventHandler _oninitialised = null;
			private BasicControlEventHandler _onAddChild = null;
			private BasicControlEventHandler _onRemoveChild = null;
			private BasicControlEventHandler _onUnloaded = null;




			public BasicControlEventHandler OnRemoveChild { get { return _onRemoveChild; } set { _onRemoveChild = value; } }
			public BasicControlEventHandler OnAddChild { get { return _onAddChild; } set { _onAddChild = value; } }
			public BasicControlEventHandler OnUnloaded { get { return _onUnloaded; } set { _onUnloaded = value; } }
			public BasicControlEventHandler OnInitialised { get { return _oninitialised; } set { _oninitialised = value; } }


			#endregion
			#region Public Properties
			public bool IsUnloading
			{
				get
				{
					return _isUnloading;

				}
				set
				{
					_isUnloading = value;
				}

			}

			public FloatPosition FloatPos
			{
				get
				{
					return _float;
				}
				set
				{
					_float = value;
					SetFloatPosition();
				}
			}

			public PositionType Position
			{
				get
				{
					return _positionType;
				}
				set
				{
					_positionType = value;
					SetPositionType();
				}
			}
			public DisplayType Display
			{
				get
				{
					return _displayType;
				}
				set
				{
					_displayType = value;
					SetDisplayType();
				}
			}

			public string Left
			{
				get
				{
					if (IsRendered)
					{
						return GetStyle("left");
					}
					else
					{
						return _left;
					}
				}
				set { _left = value; SetStyle("left", value); }
			}
			public string Right
			{
				get
				{
					if (IsRendered)
					{
						return GetStyle("right");
					}
					else
					{
						return _right;
					}
				}
				set { _right = value; SetStyle("right", value); }
			}

			public string Top
			{
				get
				{
					if (IsRendered)
					{
						return GetStyle("top");
					}
					else
					{
						return _top;
					}
				}
				set { _top = value; SetStyle("top", value); }
			}
			public string Width
			{
				get
				{
					if (IsRendered)
					{
						return GetStyle("width");
					}
					else
					{
						return _width;
					}
				}
				set { _width = value; SetStyle("width", value); }
			}
			public string Height
			{
				get
				{
					if (IsRendered)
					{
						return GetStyle("height");
					}
					else
					{
						return _height;
					}
				}
				set { _height = value; SetStyle("height", value); }
			}
			public string CssClass
			{
				get
				{
					if (IsRendered)
					{
						return GetAttribute("class");
					}
					else
					{
						return _cssClass;
					}
				}
				set { _cssClass = value; SetAttribute("class", value); }
			}


			#endregion

			public string Id { get { return _id; } set { _id = value; } }
			public string ControlId { get { return GenerateControlId(this, ""); } }
			public Control[] Children
			{
				get
				{
					Control[] _return = new Control[_children.Count];
					for (int i = 0; i < _children.Count; i++)
					{
						_return[i] = _children[i];
					}
					return _return;
				}
			}
			public Control Parent { get { return _parent; } set { _parent = value; } }

			public bool IsRendered { get { return _isRendered; } }
			public bool IsInitialised { get { return _isInitialised; } }
			public bool IsAllControlsInitialised
			{
				get
				{

					foreach (Control c in _children)
					{
						if (!c.IsAllControlsInitialised)
							return false;
						if (!c.IsInitialised)
							return false;
					}
					if (!IsInitialised)
						return false;
					return true;


				}
			}

			public void Control_Initialise()
			{

				if (_id == null)
				{
					Logging.Log(LoggingType.Error, "A control's ID must be set on the contructor of the control, it is currently null", new object[] { this });
				}
				if (!IsInitialised)
				{
					Logging.Log(LoggingType.Debug, "Initialising Control, ID: " + Id, null);

					if (JavascriptFiles != null)
					{
						int _filesToLoad = JavascriptFiles.Length;
						if (_jsFilesLoaded < _filesToLoad)
						{
							if (!_loadingJsFiles)
							{
								_loadingJsFiles = true;
								foreach (string s in JavascriptFiles)
								{
									FileLoader.Context.ProcessJavascript(s, delegate { _jsFilesLoaded++; });
								}
							}
							Window.SetTimeout(Control_Initialise, 50);
							return;
						}
						_loadingJsFiles = false;

					}
					if (CssFiles != null)
					{

						int _filesToLoad = CssFiles.Length;
						if (_cssFilesLoaded < _filesToLoad)
						{
							if (!_loadingCssFiles)
							{
								_loadingCssFiles = true;
								foreach (string c in CssFiles)
								{
									FileLoader.Context.ProcessCSS(c, delegate { _cssFilesLoaded++; });
								}
							}
							Window.SetTimeout(Control_Initialise, 50);
							return;
						}
						_loadingCssFiles = false;

					}

					Control_PreRender();
					Logging.Log(LoggingType.Debug, "Initiate Render Id:" + Id, null);
					Control_Render();
					_isRendered = true;
					Control_SetProperties();
					foreach (Control c in _children)
					{
						c.Control_Initialise();
					}
					_isInitialised = true;
					Control_Load();
					if (_oninitialised != null)
					{
						FireControlOnInitialiseEvent();
					}
				}
			}

			private void FireControlOnInitialiseEvent()
			{
				if (IsAllControlsInitialised)
				{
					_oninitialised.Invoke(this);
				}
				else
				{
					Window.SetTimeout(FireControlOnInitialiseEvent, 50);
				}
			}

			protected virtual void Control_PreRender()
			{

			}

			protected abstract void Control_Render();

			protected virtual void Control_SetProperties()
			{
				if (IsRendered)
				{
					Logging.Log(LoggingType.Debug, "Setting control properties Id:" + Id, null);
					if (!string.IsNullOrEmpty(_cssClass))
					{
						jQuery.Select("#" + ControlId).Attribute("class", _cssClass);
					}
					// TODO: change style storage to a Dictionary or Manager, and create a generic way to apply it
					if (_left != null)
					{
						SetStyle("left", _left);
					}
					if (_top != null)
					{
						SetStyle("top", _top);
					}
					if (_right != null)
					{
						SetStyle("right", _right);
					}
					if (_width != null)
					{
						SetStyle("width", _width);
					}
					if (_height != null)
					{
						SetStyle("height", _height);
					}

					ControlEventsManager.Rebind();

					SetPositionType();
					SetDisplayType();
					SetFloatPosition();
				}
			}

			protected virtual void Control_Load()
			{

			}

			protected virtual void Control_Unload()
			{

				ControlEventsManager.Clear();

				jQuery.Select("#" + ControlId).Empty();
				jQuery.Select("#" + ControlId).Remove();
				_isInitialised = false;
				_isRendered = false;
				Parent.RemoveChild(this);
				if (OnUnloaded != null)
					OnUnloaded(this);
				_isUnloading = false;
			}

			public virtual void AddChild(Control control)
			{
				control.Parent = this;
				if (control.Parent.IsInitialised)
				{
					control.Control_Initialise();
				}
				_children.Add(control);
				if (OnAddChild != null)
					OnAddChild(control);
			}
			public virtual void RemoveChild(Control control)
			{
				_children.Remove(control);
				if (OnRemoveChild != null)
					OnRemoveChild(control);
			}
			public bool ContainsChildWithId(string id)
			{
				foreach (Control c in Children)
				{
					if (c.Id == id)
						return true;

				}
				return false;

			}
			public virtual string[] CssFiles { get { return null; } }
			public virtual string[] JavascriptFiles { get { return null; } }

			public void Unload()
			{
				if (IsUnloading)
					return;
				Logging.Log(LoggingType.Debug, "Unloading " + Children.Length + " for Control: " + Id, new object[] { this });
				foreach (Control c in Children)
				{
					if (!c.IsUnloading)
						c.Unload();
				}
				Logging.Log(LoggingType.Debug, "Unloading Control: " + Id, new object[] { this });
				_isUnloading = true;
				Control_Unload();
			}


			#region Protected Functions

			public virtual void SetStyle(string styleName, string styleAttr)
			{
				jQuery.Select("#" + ControlId).CSS(styleName, styleAttr);
			}
			public virtual string GetStyle(string styleName)
			{
				return jQuery.Select("#" + ControlId).GetCSS(styleName);
			}


			public virtual void SetBind(string name, Dictionary eventData, PandoraEventHandler evnt)
			{
				if (IsRendered && evnt != null)
				{

					jQueryExtension.Select<jQueryPluginObject>("#" + ControlId).Bind(name, eventData, evnt);
				}
			}

			public virtual void Unbind(string name)
			{
				if (IsRendered)
				{
					jQuery.Select("#" + ControlId).Unbind(name);
				}
			}
			public virtual void SetAttribute(string name, string value)
			{
				if (IsRendered)
				{
					jQuery.Select("#" + ControlId).Attribute(name, value);
				}
			}
			public virtual void RemoveAttribute(string name)
			{
				if (IsRendered)
				{
					jQuery.Select("#" + ControlId).RemoveAttr(name);
				}
			}
			public virtual string GetAttribute(string name)
			{
				if (IsRendered)
				{
					return jQuery.Select("#" + ControlId).GetAttribute(name);
				}
				return null;
			}

			#endregion

			#region Private functions
			private string GenerateControlId(Control control, string append)
			{
				append += control.Id;
				if (control.Parent != null)
				{
					append += "_";
					append = GenerateControlId(control.Parent, append);
				}
				return append;
			}

			private void SetPositionType()
			{
				if (IsRendered && _positionType != PositionType.DoNotRender)
				{
					string paneltype = "";
					switch (_positionType)
					{
						case PositionType.Absolute:
							paneltype = "absolute";
							break;
						case PositionType.Fixed:
							paneltype = "fixed";
							break;
						case PositionType.Relative:
							paneltype = "relative";
							break;
						case PositionType.Statics:
							paneltype = "static";
							break;
						case PositionType.Inherit:
							paneltype = "inherit";
							break;
					}
					if (!string.IsNullOrEmpty(paneltype))
					{
						SetStyle("position", paneltype);
					}
				}
			}
			private void SetFloatPosition()
			{
				if (IsRendered && _float != FloatPosition.DoNotRender)
				{
					string flPosition = "none";
					switch (_float)
					{
						case FloatPosition.Inherit:
							flPosition = "inherit";
							break;
						case FloatPosition.Left:
							flPosition = "left";
							break;
						case FloatPosition.Right:
							flPosition = "right";
							break;
						case FloatPosition.None:
							flPosition = "none";
							break;
					}
					SetStyle("float", flPosition);
				}
			}

			protected virtual void SetDisplayType()
			{
				if (IsRendered && _displayType != DisplayType.DoNotRender)
				{
					string disp = "inherit";
					switch (_displayType)
					{
						case DisplayType.Inherit:
							disp = "inherit";
							break;
						case DisplayType.Inline:
							disp = "inline";
							break;
						case DisplayType.None:
							disp = "none";
							break;
					}
					SetStyle("display", disp);
				}
			}
			#endregion


			public bool HasChildren
			{
				get { return _children.Count > 0; }
			}
		

    }
}
