using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lifelike.Kernel.Entities;
using NHibernate;

namespace Lifelike.Kernel.EntityLogic
{
	public class ModuleLogic : LogicAbstract<Module>
	{
		public Module Create(Lifelike.WebComponents.Module mod, ISession session, ITransaction tx)
		{
			
			var m = new Module();

			
			m.Path = mod.AppRelativeVirtualPath;

			//mod.GetType().GetProperties().Where(p=>p.
			

			return m;
			//m

			//LanguageLogic _langLogic = new LanguageLogic();

			//var language = _langLogic.LoadBy(session, tx, new Func<Language, bool>(l => l.Active && l.Id == languageId));

			//Item i = new Item();
			//i.Active = true;
			//i.DateCreated = DateTime.Now;
			//i.DateModified = DateTime.Now;
			//i.Language = language;
			////i.Parent
			//return i;
		}
	}
}
