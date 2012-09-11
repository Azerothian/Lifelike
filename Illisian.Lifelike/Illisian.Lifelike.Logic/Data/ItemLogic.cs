using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illisian.Lifelike.Data;
using NHibernate;

namespace Illisian.Lifelike.Logic.Data
{
	public class ItemLogic : LogicAbstract<Item>
	{
		public Item Create(string name, Guid? parentId, Guid languageId, ISession session, ITransaction tx)
		{

			LanguageLogic _langLogic = new LanguageLogic();

			var language = _langLogic.LoadBy(session, tx, new Func<Language, bool>(l=>l.Active && l.Id == languageId));

			Item i = new Item();
			i.Active = true;
			i.DateCreated = DateTime.Now;
			i.DateModified = DateTime.Now;
			i.Language = language;
			
            if(parentId.HasValue)
                i.Parent = LoadBy(session, tx, new Func<Item, bool>(ii=>ii.Active && ii.Id == parentId));
            i.Name = name;
            i.Save(session, tx);
			return i;
		}
		

	}
}
