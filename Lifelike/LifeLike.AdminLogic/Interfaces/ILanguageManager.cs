using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;

namespace Lifelike.AdminLogic.Interfaces
{
    public interface ILanguageManager
    {
        Store Datastore { get; }
        Window Window { get; }
        string Name { get; set; }
        string Code { get; set; }
        Guid? LanguageId { get; set; }
        Guid SelectedRowId { get; }
    }
}
