// SelectMenu.cs
//

using System;
using System.Collections.Generic;

using PandoraJs.Utils.Extension;
using jQueryApi;

namespace PandoraJs.Controls
{
	public class Select : Control
	{

		private string _dataGroupTextField = "";
		private string _dataGroupDataField = "";

		private string _dataTextField = "";
		private string _dataValueField = "";

		private object _dataSource = null;

		public string DataTextField
		{
			get
			{
				return _dataTextField;
			}
			set
			{
				_dataTextField = value;
			}
		}
		public string DataValueField
		{
			get
			{
				return _dataValueField;
			}
			set
			{
				_dataValueField = value;
			}
		}

		public string DataGroupTextField
		{
			get{
				return _dataGroupTextField;

			}
			set {
				_dataGroupTextField = value;
			}
		}
		public string DataGroupDataField
		{
			get
			{
				return _dataGroupDataField;

			}
			set
			{
				_dataGroupDataField = value;
			}
		}
		public object DataSource
		{
			get
			{
				return _dataSource;
			}
			set
			{
				_dataSource = value;
			}
		}

		public void DataBind()
		{

			string data = "";
			if (_dataSource != null && !string.IsNullOrEmpty(_dataTextField) && !string.IsNullOrEmpty(_dataValueField))
			{
				if (_dataSource is Array)
				{
					Array a = (Array)_dataSource;
					if (!string.IsNullOrEmpty(_dataGroupTextField))
					{
						foreach (object o in a)
						{
							string grpFld = (string)Type.GetField(o, _dataGroupTextField);
							data += "<optgroup label='" + grpFld + "'>";
							Array grpData = (Array)Type.GetField(o, _dataGroupDataField);
							data += ParseOptionList(grpData);

						}
					}
					else
					{
						data += ParseOptionList(a);

					}

					jQuery.Select("#" + ControlId).Html(data);
				}
			}

		}

		private string ParseOptionList(Array arr)
		{
			string data = "";
			foreach (object o in arr)
			{
				string text = (string)Type.GetField(o, _dataTextField);
				string value = (string)Type.GetField(o, _dataValueField);
				data += "<option value='" + value + "'>" + text + "</option>";
			}
			return data;
		}
		
		protected override void Control_Render()
		{
			jQuery.Select("#" + Parent.ControlId).Append("<select style='width: 100%; height: 100%' name='" + ControlId + "' id='" + ControlId + "'></select>");
			DataBind();
			
		}
		protected override void Control_SetProperties()
		{
			if (IsRendered)
			{
				if (String.IsNullOrEmpty(Width) || Width == "0px")
					Width = "200px";
				base.Control_SetProperties();
			}
		}

		public string SelectedValue
		{
			get
			{
				if (IsRendered)
				{
					return jQuery.Select("#" + ControlId).GetValue();
				}
				return "";
			}
		}

	}
}
