﻿using System;
using System.Collections.Generic;
using System.Html;
using System.Text;
using System.Linq;
using jQueryApi;
namespace Lifelike.JScript.Admin
{
	public abstract class Control
	{
		//public Control()
		//{
		//	_children = new List<Control>();
		//	ControlContainer = Document.CreateElement("div");
		//}
		public Control(string name)
		{
			_children = new List<Control>();
			ControlContainer = Document.CreateElement("div");
			Name = name;
			
		}
		private List<Control> _children;
		public bool IsRendered { get; set; }
		public Element ControlContainer { get; set; }
		private Control _parent;
		public Control Parent { get {
			return _parent;
		}
			set { _parent = value; }
		}
		private string _name;
		public string Name
		{
			get { return _name; }
			set
			{
				if (Parent != null)
				{
					setClientId();
				}
				_name = value;
			}
		}
		public string ClientId
		{
			get
			{
				return (string)ControlContainer.GetAttribute("id");
			}
			private set
			{
				ControlContainer.SetAttribute("id", value);
			}
		}
		public string CssClass
		{
			get
			{
				return (string)ControlContainer.GetAttribute("class");
			}
			set
			{
				ControlContainer.SetAttribute("class", value);
			}
		}

		public int Height
		{
			get
			{
				return jQuery.FromElement(ControlContainer).GetHeight();
			}
			set
			{
				jQuery.FromElement(ControlContainer).Height(value);
			}
		}
		public int Width
		{
			get
			{
				return jQuery.FromElement(ControlContainer).GetWidth();
			}
			set
			{
				jQuery.FromElement(ControlContainer).Width(value);
			}
		}
		public string Left
		{
			get
			{
				return jQuery.FromElement(ControlContainer).GetCSS("left");
			}
			set
			{
				jQuery.FromElement(ControlContainer).CSS("left",value);
			}
		}
		public string Top
		{
			get
			{
				return jQuery.FromElement(ControlContainer).GetCSS("top");
			}
			set
			{
				jQuery.FromElement(ControlContainer).CSS("top", value);
			}
		}
		public bool Visible
		{
			get
			{
				return jQuery.FromElement(ControlContainer).GetCSS("display") == "none";
			}
			set
			{
				if (value)
				{
					jQuery.FromElement(ControlContainer).Show();
				}
				else
				{
					jQuery.FromElement(ControlContainer).Hide();
				}
			}

		}

		public Control FindControl(List<Control> collection, Func<Control, bool> func)
		{
			foreach (var v in collection)
			{
				if (func(v))
				{
					return v;
				}
				else if (v.Children != null)
				{
					return FindControl(v.Children, func);

				}
			}
			return null;
		}


		public Control FindControl(Func<Control, bool> func)
		{
			return FindControl(Children, func);
		}

		internal void AddChildren(List<Control> list)
		{
			foreach (var v in list)
			{
				AddChild(v);
			}
		}

		public List<Control> Children
		{
			get
			{
				return _children;

			}
		}
		public virtual void AddChild(Control control)
		{
			
			control.Parent = this;
			control.setClientId();
			if (IsRendered)
			{
				Util.Console().log(".control.AddChild.render", control.ClientId);
				control.Render();
			}
			_children.Add(control);
		}
		public virtual void RemoveChild(Control control)
		{
			control.Parent = null;
			_children.Remove(control);
			if (control.IsRendered)
			{	
				control.RemoveRender();
			}
		}
		public virtual void RemoveRender()
		{

			foreach (var c in _children)
			{
				c.RemoveRender();
			}
			if (this.IsRendered)
			{
				jQuery.FromElement(this.ControlContainer).Remove();
				this.IsRendered = false;
			}
			
		}
		public virtual void Render()
		{
			if (!IsRendered)
			{
				setClientId();
				PreRender();
				Util.Console().log(".control.render", ClientId);
				if (Parent != null && Parent.ControlContainer != null)
				{
					Parent.ControlContainer.AppendChild(ControlContainer);
				}
			}
			if (Children != null)
			{
				foreach (var c in Children)
				{
					c.Render();
				}
			}
			if (!IsRendered)
			{
				PostRender();
			}
			IsRendered = true;
		}
		public void setClientId()
		{
			var control = this;
			var result = "";
			while (control != null)
			{
				result =  control.Name + "_" + result ;
				control = control.Parent;
			}
			ClientId = result;
		}

		internal Control FindControlByClientId(string id)
		{
			Control control = null;

			if (ClientId == id)
			{
				control = this;
			}
			else
			{
				foreach (var c in _children)
				{
					control = c.FindControlByClientId(id);
					if (control != null) break;
				}
			}
			return control;
		}

		public string getProperties(string name)
		{
			return (string)ControlContainer.GetAttribute("data-" + name);
		}
		public void setProperties(string name, string value)
		{
			ControlContainer.SetAttribute("data-" + name, value);
		}

		public abstract void PreRender();
		public virtual void PostRender() { }
	}
}
