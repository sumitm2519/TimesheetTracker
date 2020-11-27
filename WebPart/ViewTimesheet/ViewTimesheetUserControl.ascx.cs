using Microsoft.SharePoint;
using System;
using System.Web.UI;

namespace TimesheetTracker.WebPart.ViewTimesheet
{
    /// <summary>
    /// View timesheet class
    /// </summary>
    public partial class ViewTimesheetUserControl : UserControl
    {
        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["tid"] != null)
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList lstTimesheet = web.Lists["Timesheet"];
                            if (lstTimesheet != null)
                            {
                                SPListItem item = lstTimesheet.GetItemById(Convert.ToInt32(Request.QueryString["tid"]));
                                if (item != null)
                                {
                                    lblCategory.Text = Convert.ToString(item["Category"]);
                                    lblDescription.Text = Convert.ToString(item["Description"]);
                                    lblDate.Text = Convert.ToDateTime(item["TimesheetDate"]).ToString("yyyy-MM-dd");
                                    lblHours.Text = Convert.ToString(item["Hours"]);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Timesheet list not found.');", true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Linkbutton click event to go back to home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(SPContext.Current.Site.Url + "/SitePages/Home.aspx");
        }
    }
}
