using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace Lifelike.Kernel.Entities
{
    public class Item : Entity<Item>
    {
        public virtual string Name { get; set; }
       // public virtual Language Language { get; set; }
        public virtual Item Parent { get; set; }
        public virtual ISet<Item> Children { get; set; }
		public virtual string Value { get; set; }
		//public virtual string Template { get; set; }
		public virtual string FullPath { get; set; }
		public virtual ISet<View> Views { get; set; }
        public override void ModelOverride(FluentNHibernate.Automapping.AutoPersistenceModel model)
        {
            model.Override<Item>(map =>
            {
                map.HasMany<Item>(p => p.Children).KeyColumn("Parent_id");
				map.Map(p => p.Value).Length(10000);
				map.HasManyToMany<View>(p => p.Views);
            });

		

            base.ModelOverride(model);
        }
		public override void PreSave()
		{
			//FullPath = GetFullPath(this, "");	
		}
		protected internal virtual string GetFullPath(Item i, string path)
		{
			if (i == null)
				return path;
			path += "/" + GetFullPath(i.Parent, path);
			return path;
		}
    }

}
