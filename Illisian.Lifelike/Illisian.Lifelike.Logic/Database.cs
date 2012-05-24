using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Automapping;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Web;
using Illisian.Lifelike.Data;

namespace Illisian.Lifelike.Logic
{
    public class Database
    {
        private FluentConfiguration _config = null;
        private static Database _context = null;
        private ISessionFactory _sessionFactory;
        private ISession _currentSession = null;
        const string VarSession = "nhsession";
        /// <summary>
        /// Gets the context.
        /// </summary>
        public static Database Context
        {
            get
            {
                return _context ?? (_context = new Database());
            }
        }

        public Database()
        {

        }

        public void Configure(Assembly[] asm)
        {
            _config = Fluently.Configure();
            SetupConnection();
            SetupMappings(asm);
            UpdateDbSchema();
            _sessionFactory = _config.BuildSessionFactory();

        }

        protected void SetupMappings(Assembly[] asm)
        {
            AutoPersistenceModel model = AutoMap.Assemblies(asm);
            model.IgnoreBase<BaseEntity>();
            model.Where(x => x.BaseType == typeof(BaseEntity) && ((BaseEntity)Activator.CreateInstance(x)).ModelOverride(model));
            
            _config.Mappings(m => m.AutoMappings.Add(model));
        }
        protected void SetupConnection()
        {
            _config.Database(MySQLConfiguration.Standard
                .ConnectionString(c =>
                    c.Server("127.0.0.1")
                    .Database("cms")
                    .Username("root")
                    .Password("")));
        }

        /// <summary>
        /// Updates the db schema.
        /// </summary>
        protected void UpdateDbSchema()
        {
            Action<string> updateExport = x =>
            {
               string s =  String.Format("[NHibernate.UpdateDbSchema] {0}", x);
               s = s + "";
            };

            Action<Configuration> schemaUpdate = x =>
            {
                new SchemaUpdate(x).Execute(updateExport, true);
            };
            _config.ExposeConfiguration(schemaUpdate);
        }
        /// <summary>
        /// Exports the db schema.
        /// </summary>
        protected void ExportDbSchema()
        {
            Action<Configuration> schemaUpdate = x =>
            {
                var export = new SchemaExport(x);
                export.Execute(true, true, true);
            };
            _config.ExposeConfiguration(schemaUpdate);
        }

        private bool CheckValidSession(ISession _currentSession)
        {
            if (_currentSession == null || !_currentSession.IsConnected || !_currentSession.IsOpen)
                return false;
            return true;

        }
        public ISession CurrentSession
        {
            get
            {
                var currentContext = HttpContext.Current; // Is Http Session or not?
                if (currentContext == null)
                {
                    if (!CheckValidSession(_currentSession))
                    {
                        _currentSession = OpenSession();
                    }
                    return _currentSession;
                }
                else
                {
                    if (!CheckValidSession(currentContext.Items[VarSession] as ISession))
                    {
                        currentContext.Items[VarSession] = OpenSession();
                    }
                    return currentContext.Items[VarSession] as ISession;
                }
            }
            set
            {

                if (HttpContext.Current == null)
                {
                    _currentSession = value;
                }
                else
                {
                    HttpContext.Current.Items[VarSession] = value;
                }

            }
        }
        public ISession OpenSession()
        {
            ISession session = _sessionFactory.OpenSession();
            if (session == null)
            {
                throw new InvalidOperationException("Call to SessionFactory.OpenSession() returned null.");
            }
            return session;
        }

    }
}
