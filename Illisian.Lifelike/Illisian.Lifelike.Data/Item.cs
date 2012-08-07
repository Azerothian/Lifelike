using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illisian.Lifelike.Data
{
    public class Item : Entity<Item>
    {
        public virtual string Name { get; set; }
        public virtual string Language { get; set; }
        public virtual Item Parent { get; set; }
        public virtual ISet<Item> Children { get; set; }
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
