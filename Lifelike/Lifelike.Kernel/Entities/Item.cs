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
			FullPath = GetFullPath(this);
			if (string.IsNullOrEmpty(FullPath))
			{
				FullPath = "/";
			}
		}
		protected internal virtual string GetFullPath(Item item)
		{

			List<Item> _items = new List<Item>();
			Item current = item;
			do
			{
				_items.Add(current);
				current = current.Parent;
			} while (current != null);

			string path = "";
			_items.Reverse();
			foreach(var i in _items)
			{
				if (i.Name != "/")
				{
					path = path +  "/" + i.Name;
				}
			}
			return path;
		}
    }

}
