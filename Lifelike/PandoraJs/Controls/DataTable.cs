// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;

using PandoraJs.Utils.Extension;
using PandoraJs.Utils;
using System.Collections;

namespace PandoraJs.Controls
{

	public class DataTable : Table
	{

		private IEnumerable<DataColumn> _dataColumns;

		public IEnumerable<DataColumn> DataColumns
		{
			get
			{
				return _dataColumns;
			}
			set
			{
				_dataColumns = value;
			}
		}

		private IEnumerable<DataRow> _dataRows;

		public IEnumerable<DataRow> DataRows
		{
			get
			{
				return _dataRows;
			}
			set
			{
				_dataRows = value;
			}
		}


		private object _dataSource = null;
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
			if (_dataSource != null)
			{
				Dictionary data = Dictionary.GetDictionary(_dataSource);
				if (_dataColumns == null)
				{
					Dictionary header = Dictionary.GetDictionary(data[data.Keys[0]]);
					List<DataColumn> _columns = new List<DataColumn>();
					foreach (string headerKey in header.Keys)
					{
						DataColumn dc = new DataColumn();
						dc.HeaderText = headerKey;
						dc.Name = headerKey;
						_columns.Add(dc);
					}
					_dataColumns = _columns;
				}
				List<DataRow> _rows = new List<DataRow>();
				foreach (string key in data.Keys)
				{
					Dictionary dataRow = Dictionary.GetDictionary(data[key]);
					DataRow dr = new DataRow();
					dr.DataKey = key;
					foreach (DataColumn dc in _dataColumns)
					{
						dr[dc.Name] = dataRow[dc.Name];
					}
					_rows.Add(dr);
				}
				_dataRows = _rows;

			}
			RenderTable();
		}
		public void RenderTable()
		{
			if (_dataColumns != null)
			{
				foreach (DataColumn dc in _dataColumns)
				{
					if (!Header.ContainsChildWithId(dc.Name))
					{
						TableHeaderColumn _header = new TableHeaderColumn();
						_header.Id = dc.Name;
						_header.Text = dc.HeaderText;
						if (!string.IsNullOrEmpty(dc.Width))
							_header.Width = dc.Width;
						AddChild(_header);
					}
				}
			}

			if (_dataRows != null)
			{
				ClearBody();


				foreach (DataRow dr in _dataRows)
				{
					TableRow _row = new TableRow();
					_row.Id = dr.DataKey;
					foreach (DataColumn dc in _dataColumns)
					{
						TableColumn _column = new TableColumn();
						_column.Id = dc.Name;

						string display = "";
						if (dr[dc.Name] != null)
						{
							object o =dr[dc.Name];

							if (dc.FormatEvent != null)
							{
								display = dc.FormatEvent(o);
							}
							else if (!string.IsNullOrEmpty(dc.FormatString))
							{
								display = String.Format(dc.FormatString, o);
							}
							else
							{

								display = o.ToString();
							}
						}


						_column.Text = display;
						_row.AddChild(_column);
					}
					AddChild(_row);
				}

			}
		}
		protected override void Control_Render()
		{
			base.Control_Render();
			DataBind();
		}
	}
	public delegate string DataColumnFormatHandler(object o);




	public class DataColumn
	{
		public string Name;
		public string HeaderText;
		public string Width;
		public string Height;
		public string FormatString;
		public DataColumnFormatHandler FormatEvent;
	}
	public class DataRow
	{
		private string _dataKey = "";


		public string DataKey
		{
			get
			{
				return _dataKey;
			}
			set
			{
				_dataKey = value;
			}

		}

		private RealDictionary _data = new RealDictionary();
		public object this[string key]
		{
			get
			{
				return _data[key];
			}
			set
			{
				_data[key] = value;
			}
		}
	}
}
