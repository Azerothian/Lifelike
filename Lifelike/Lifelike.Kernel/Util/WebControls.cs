using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Lifelike.Kernel.Util
{
	public static class WebControls
	{
		public static T FindParentControlByType<T>(Control c) where T : Control
		{
			if (c == null)
				return null;
			if (c is T)
			{
				return (T)c;
			}
			else
			{
				return FindParentControlByType<T>(c.Parent);
			}
		}

		public static T FindChildControlByType<T>(ControlCollection cc) where T : Control
		{
			//int i = 0;
			if (cc == null)
				return null;

			T ctrl = null;

			foreach (var c in cc)
			{
				if (c != null)
				{
					if (c is T)
					{

						ctrl = (T)c;
						break;
					}

					if (((Control)c).Controls != null)
					{
						ctrl = FindChildControlByType<T>(((Control)c).Controls);
					}
				}
				if (ctrl != null)
				{
					break;
				}
			}
			return ctrl;
		}

		public static void FindAllChildControlByType<T>(ControlCollection cc, ref List<T> result) where T : Control
		{
			if (cc == null)
				return;

			foreach (var c in cc)
			{
				if (c != null)
				{
					if (c is T)
					{
						result.Add((T)c);
					}

					if (((Control)c).Controls != null)
					{
						FindAllChildControlByType<T>(((Control)c).Controls, ref result);
					}
				}
			}
			return;
		}

		public static T FindChildControlByTypeAndId<T>(ControlCollection cc, string id) where T : Control
		{
			// int i = 0;
			if (cc == null)
				return null;

			T ctrl = null;

			foreach (var c in cc)
			{
				if (c != null)
				{
					if (c is T)
					{
						if (((T)c).ID == id)
						{
							ctrl = (T)c;
							break;
						}
					}

					if (((Control)c).Controls != null)
					{
						ctrl = FindChildControlByType<T>(((Control)c).Controls);
					}
				}
				if (ctrl != null)
				{
					break;
				}
			}
			return ctrl;
		}
	}
}
