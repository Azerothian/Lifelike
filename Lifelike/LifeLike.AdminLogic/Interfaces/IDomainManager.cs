using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lifelike.Entities;

namespace Lifelike.AdminLogic.Interfaces
{
    public interface IDomainManager
    {
        IEnumerable<Domain> GridDataSource { set; }

        void UpdateRow(Guid Id, string name, string value);
        
    }
}
