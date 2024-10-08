using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ControlPanel.HelpersFunctions;

namespace ControlPanel
{
    public partial class _default : System.Web.UI.Page
    {
        ControlPanelInit Pageinit = new ControlPanelInit();

        protected void Page_Load(object sender, EventArgs e)
        {

            Pageinit.CheckManagerPermissions();
            System.Web.HttpContext.Current.Response.Redirect("HomePage.aspx");

        }
    }
}