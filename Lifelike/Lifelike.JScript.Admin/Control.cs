using System;
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
				setClientId();
				_name = value;
			}
		}
		public string ClientId
		{
			get
			{
				return (string)ControlContainer.GetAttribute("id");
			}
			set
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
		public List<Control> Children
		{
			get
			{
				return _children;

			}
		}
		public void AddChild(Control control)
		{
			
			control.Parent = this;

			if (IsRendered)
			{
				control.Render();
			}
			_children.Add(control);
		}
		public void RemoveChild(Control control)
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
				if (Parent != null && Parent.ControlContainer != null)
				{
					Parent.ControlContainer.AppendChild(ControlContainer);
				}
				PreRender();
			}
			foreach (var c in Children)
			{

				c.Render();
				if (!IsRendered)
				{
					c.PostRender();
				}
			}
			IsRendered = true;
		}
		private void setClientId()
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

		public abstract void PreRender();
		public virtual void PostRender() { }
	}
}
