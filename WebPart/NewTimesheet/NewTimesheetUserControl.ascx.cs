using Microsoft.SharePoint;
using System;
using System.Web.UI;
using TimesheetTracker.Helper;

namespace TimesheetTracker.WebPart.NewTimesheet
{
    /// <summary>
    /// New timsheet class
    /// </summary>
    public partial class NewTimesheetUserControl : UserControl
    {
        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Submit button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool validHours = false;
                bool dataSave = false;
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList lstTimesheet = web.Lists["Timesheet"];
                        if (lstTimesheet != null)
                        {
                            validHours = Common.ValidHours(lstTimesheet, dtDate.SelectedDate, Convert.ToDouble(txtHours.Text.Trim()), 0);
                            if (validHours)
                            {
                                SPListItem item = lstTimesheet.Items.Add();
                                item["Hours"] = txtHours.Text.Trim();
                                item["Description"] = txtDescription.Text.Trim();
                                item["Category"] = ddlCategory.SelectedItem.Text;
                                item["TimesheetDate"] = dtDate.SelectedDate;
                                item.Update();
                                dataSave = true;
                            }
                        }
                        else
                        {
                            validHours = true;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Timesheet list not found.');", true);
                        }
                    }
                }

                if (!validHours)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Total hours per day should not be greater than 8.');", true);
                }
                if (dataSave)
                {
                    string pageUrl = SPContext.Current.Site.Url + "/SitePages/Home.aspx";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Timesheet Added..!'); window.location = '" + pageUrl + "';", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Cancel button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(SPContext.Current.Site.Url + "/SitePages/Home.aspx");
        }
    }
}
