using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifelike.Entities
{
    public class Item : Entity<Item>
    {
        public virtual string Name { get; set; }
        public virtual Language Language { get; set; }
        public virtual Item Parent { get; set; }
		public virtual Item Template { get; set; }
        public virtual ISet<Item> Children { get; set; }
		public virtual string Value { get; set; }
		public virtual string FullPath { get; set; }
		public virtual ISet<View> Views { get; set; }
        public override void ModelOverride(FluentNHibernate.Automapping.AutoPersistenceModel model)
        {
            model.Override<Item>(map =>
            {
                map.HasMany<Item>(p => p.Children).KeyColumn("Parent_id");
            });
            base.ModelOverride(model);
        }
		
    }
}
