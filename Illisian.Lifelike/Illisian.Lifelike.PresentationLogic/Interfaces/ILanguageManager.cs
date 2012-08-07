using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;

namespace Illisian.Lifelike.PresentationLogic.Interfaces
{
    public interface ILanguageManager
    {
        Store Datastore { get; }
        string Name { get; set; }
        string Code { get; set; }
        int? LanguageId { get; set; }
        int SelectedRowId { get; }
    }
}
