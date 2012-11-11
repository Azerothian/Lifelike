using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lifelike.Kernel.Entities;
using NHibernate;

namespace Lifelike.Kernel.EntityLogic
{
	public class DomainLogic : LogicAbstract<Domain>
	{
		//public Item Create( Guid? parentId, Guid languageId, ISession session, ITransaction tx)
		//{

		//	LanguageLogic _langLogic = new LanguageLogic();

		//	var language = _langLogic.LoadBy(session, tx, new Func<Language, bool>(l=>l.Active && l.Id == languageId));

		//	Item i = new Item();
		//	i.Active = true;
		//	i.DateCreated = DateTime.Now;
		//	i.DateModified = DateTime.Now;
		//	i.Language = language;
		//	//i.Parent
		//	return i;
		//}
		public static Domain GetCurrentDomain(string host, ISession session)
		{
			var domain = LoadBy(session, 
					new Func<Domain, bool>(i => i.BaseUri.ToLower() == host.ToLower())
					);
			if (domain == null)
			{
				domain = LoadBy(session, new Func<Domain, bool>(i => i.BaseUri.ToLower() == ""));
			}
			return domain;
		}

	}
}
