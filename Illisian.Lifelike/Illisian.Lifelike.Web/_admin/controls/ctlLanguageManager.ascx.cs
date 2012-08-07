﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Illisian.Lifelike.PresentationLogic.Interfaces;
using Illisian.Lifelike.PresentationLogic.Managers;

namespace Illisian.Lifelike._admin.controls
{
    public partial class ctlLanguageManager : System.Web.UI.UserControl, ILanguageManager
    {
        LanguageManager _manager;
        protected void Page_Init(object sender, EventArgs e)
        {
            _manager = new LanguageManager(this);

            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                _manager.Initialise();
            }
        }



        protected void btnEditLanguage_Click(object sender, DirectEventArgs e)
        {
            _manager.Edit();
        }
        protected void btnNewLanguage_Click(object sender, DirectEventArgs e)
        {

            _manager.New();

        }

        protected void btnSaveLanguage_Click(object sender, DirectEventArgs e)
        {
            _manager.Save();
        }
        protected void rowSelect_Click(object sender, DirectEventArgs e)
        {
            if (rowSelectionModel.SelectedRow != null)
            {
                btnEditLanguage.Disabled = false;
                btnDeleteLanguage.Disabled = false;
            }
            else
            {
                btnEditLanguage.Disabled = true;
                btnDeleteLanguage.Disabled = true;
            }
        }

        //btnDeleteLanguage_Click
        protected void btnDeleteLanguage_Click(object sender, DirectEventArgs e)
        {
            if (rowSelectionModel.SelectedRow != null)
            {
                _manager.Delete();
                
            }
        }

        public int SelectedRowId
        {
            get
            {
                return int.Parse(rowSelectionModel.SelectedRecordID);
            }
        }


        public Store Datastore
        {
            get { return storeLanguages; }
        }

        public string Name
        {
            get
            {
                return txtName.Text;
            }
            set
            {
                txtName.Text = value;
            }
        }

        public string Code
        {
            get
            {
                return txtCode.Text;
            }
            set
            {
                txtCode.Text = value;
            }
        }

        public int? LanguageId
        {
            get
            {
                int result = -1;
                if(int.TryParse(hidId.Text, out result))
                {
                    return result;
                }
                return null;
            }
            set
            {
                if(value != null)
                    hidId.Text = value.ToString();

            }
        }
        public Window Window
        {
            get
            {
                return winLanguage;
            }

        }
    }
}