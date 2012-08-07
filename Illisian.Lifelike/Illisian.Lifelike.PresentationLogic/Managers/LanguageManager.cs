using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illisian.Lifelike.PresentationLogic.Interfaces;
using Illisian.Lifelike.Logic.Data;
using Illisian.Lifelike.Logic;
using NHibernate;
using Illisian.Lifelike.Data;

namespace Illisian.Lifelike.PresentationLogic.Managers
{
    public class LanguageManager
    {
        LanguageLogic _langLogic = null;
        ILanguageManager _inf = null;
        public LanguageManager(ILanguageManager inf)
        {
            _inf = inf;
            _langLogic = new LanguageLogic();
        }

        public void Initialise()
        {
            var session = Database.Context.CurrentSession;
            using (ITransaction tx = session.BeginTransaction())
            {
                _inf.Datastore.DataSource = _langLogic.LoadAllBy(session, tx);
                _inf.Datastore.DataBind();
            }
        }

        public void Save()
        {
            using (var session = Database.Context.OpenSession())
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    tx.Begin();
                    try
                    {

                        Language l = null;

                        if (_inf.LanguageId == null)
                        {
                            l = new Language();
                            l.Active = true;
                        }
                        else
                        {
                            l = _langLogic.LoadBy(session, tx, (x => x.Id == _inf.LanguageId));
                        }
                        if (l == null)
                        {
                            throw new Exception("Language is null while trying to save");
                        }

                        l.Code = _inf.Code;
                        l.Name = _inf.Name;
                        _langLogic.Save(l, session, tx);
                        tx.Commit();
                        Initialise();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        throw new Exception("Error while trying to save the language.", ex);
                    }
                }
            }
        }

        public void Load()
        {
            if (_inf.LanguageId != null)
            {
                var session = Database.Context.CurrentSession;
                using (ITransaction tx = session.BeginTransaction())
                {
                    tx.Begin();
                    try
                    {
                        var l = _langLogic.LoadBy(session, tx, (x => x.Id == _inf.LanguageId));
                        if (l != null)
                        {
                            _inf.Name = l.Name;
                            _inf.Code = l.Code;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error while trying to load the language.", ex);
                    }
                }
            }
        }

        public void Delete()
        {
            var session = Database.Context.CurrentSession;
            using (ITransaction tx = session.BeginTransaction())
            {
                tx.Begin();
                try
                {
                    var l = _langLogic.LoadBy(session, tx, (x => x.Id == _inf.SelectedRowId));
                    if (l != null)
                    {
                        l.Active = false;
                        tx.Commit();
                        Initialise();
                    }
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    throw new Exception("Error while trying to delete the language.", ex);
                }
            }

        }
    }
}
