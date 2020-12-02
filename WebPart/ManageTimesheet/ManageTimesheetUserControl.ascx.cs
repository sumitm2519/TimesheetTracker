using Microsoft.SharePoint;
using System;
using System.Web.UI;
using TimesheetTracker.Helper;
using TimesheetTracker.Model;
using TimesheetTracker.Repository;

namespace TimesheetTracker.WebPart.ManageTimesheet
{
    /// <summary>
    /// ManageTimesheetUserControl
    /// </summary>
    public partial class ManageTimesheetUserControl : UserControl
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
                if (!IsPostBack)
                {
                    if (Request.QueryString["tid"] != null)
                    {
                        BindTimesheetData(Convert.ToInt32(Request.QueryString["tid"]));
                        if (Request.QueryString["op"] != null)
                        {
                            if (Convert.ToString(Request.QueryString["op"]).Equals("e"))
                            {
                                lblPageTitle.Text = "Edit Timesheet";
                                HideShowViewControls(false);
                                btnDelete.Visible = false;
                            }
                            else if (Convert.ToString(Request.QueryString["op"]).Equals("r"))
                            {
                                lblPageTitle.Text = "View Timesheet";
                                HideShowEditControls(false);
                                btnDelete.Visible = false;
                                btnSubmit.Visible = false;
                            }
                            else if (Convert.ToString(Request.QueryString["op"]).Equals("d"))
                            {
                                lblPageTitle.Text = "Delete Timesheet";
                                HideShowViewControls(false);
                                btnSubmit.Visible = false;
                            }
                        }
                        else
                        {
                            HideShowEditControls(false);
                            HideShowViewControls(false);
                            btnDelete.Visible = false;
                            btnSubmit.Visible = false;
                            btnCancel.Visible = true;
                        }
                    }
                    else
                    {
                        lblPageTitle.Text = "Add New Timesheet";
                        HideShowViewControls(false);
                        btnDelete.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.HandleException(ex, SPContext.Current.Web.Url, "Page_Load", "ManageTimesheetUserControl", SPContext.Current.Web.CurrentUser.Name);
            }
        }

        /// <summary>
        /// Bind timsheet details
        /// </summary>
        /// <param name="timesheetId"></param>
        private void BindTimesheetData(int timesheetId)
        {
            try
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        TimesheetRespository objRepository = new TimesheetRespository();
                        SPListItem item = objRepository.GetListItemById(web, Constants.TimesheetListName, timesheetId);
                        if (item != null)
                        {
                            lblCategory.Text = Convert.ToString(item["Category"]);
                            lblDescription.Text = Convert.ToString(item["Description"]);
                            lblDate.Text = Convert.ToDateTime(item["TimesheetDate"]).ToString("yyyy-MM-dd");
                            lblHours.Text = Convert.ToString(item["Hours"]);

                            SPFieldMultiLineText multilineField = item.Fields.GetField("Description") as SPFieldMultiLineText;
                            if (multilineField != null)
                            {
                                txtDescription.Text = multilineField.GetFieldValueAsText(item["Description"]);
                            }
                            if (ddlCategory.Items.FindByValue(Convert.ToString(item["Category"])) != null)
                            {
                                ddlCategory.Items.FindByValue(Convert.ToString(item["Category"])).Selected = true;
                            }
                            dtDate.SelectedDate = Convert.ToDateTime(item["TimesheetDate"]);
                            txtHours.Text = Convert.ToString(item["Hours"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.HandleException(ex, SPContext.Current.Web.Url, "BindTimesheetData", "ManageTimesheetUserControl", SPContext.Current.Web.CurrentUser.Name);
            }

        }

        /// <summary>
        /// Hider read controls
        /// </summary>
        /// <param name="visible"></param>
        private void HideShowViewControls(bool visible)
        {
            lblCategory.Visible = visible;
            lblDate.Visible = visible;
            lblDescription.Visible = visible;
            lblHours.Visible = visible;
        }

        /// <summary>
        /// Hide add/edit controls
        /// </summary>
        /// <param name="visible"></param>
        private void HideShowEditControls(bool visible)
        {
            txtDescription.Visible = visible;
            txtHours.Visible = visible;
            dtDate.Visible = visible;
            ddlCategory.Visible = visible;
        }

        /// <summary>
        /// Submit button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int timesheetId = 0;
                if (Request.QueryString["tid"] != null)
                {
                    timesheetId = Convert.ToInt32(Request.QueryString["tid"]);
                }

                bool validHours = false;
                bool dataSave = false;

                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        bool allowUnsafeUpdate = web.AllowUnsafeUpdates;
                        try
                        {
                            web.AllowUnsafeUpdates = true;

                            SPList lstTimesheet = web.Lists[Constants.TimesheetListName];
                            if (lstTimesheet != null)
                            {
                                Timesheet objTimesheet = new Timesheet()
                                {
                                    ID = timesheetId,
                                    TimesheetDate = dtDate.SelectedDate,
                                    Category = ddlCategory.SelectedItem.Text,
                                    Description = txtDescription.Text.Trim(),
                                    Hours = Convert.ToDouble(txtHours.Text.Trim())
                                };

                                TimesheetRespository objRepository = new TimesheetRespository();
                                validHours = Common.ValidHours(lstTimesheet, dtDate.SelectedDate, Convert.ToDouble(txtHours.Text.Trim()), objTimesheet.ID, objRepository);
                                if (validHours)
                                {
                                    dataSave = objRepository.SaveTimesheet(lstTimesheet, objTimesheet);
                                }
                            }
                            else
                            {
                                validHours = true;
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('" + Constants.TimesheetListName + " list not found.');", true);
                            }
                        }
                        finally
                        {
                            web.AllowUnsafeUpdates = allowUnsafeUpdate;
                        }
                    }
                }

                if (!validHours)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Total hours per day should not be greater than 8.');", true);
                }
                if (dataSave)
                {
                    string pageUrl = SPContext.Current.Site.Url + Constants.HomePageUrl;
                    if (timesheetId > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('" + Constants.TimesheetUpdatedMsg + "'); window.location = '" + pageUrl + "';", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('" + Constants.TimesheetAddedMsg + "'); window.location = '" + pageUrl + "';", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.HandleException(ex, SPContext.Current.Web.Url, "btnSubmit_Click", "ManageTimesheetUserControl", SPContext.Current.Web.CurrentUser.Name);
            }
        }

        /// <summary>
        /// Delete button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool dataSave = false;
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        bool allowUnsafeUpdate = web.AllowUnsafeUpdates;
                        try
                        {
                            web.AllowUnsafeUpdates = true;
                            TimesheetRespository objRepository = new TimesheetRespository();
                            dataSave = objRepository.DeleteListItemById(web, Constants.TimesheetListName, Convert.ToInt32(Request.QueryString["tid"]));
                        }
                        finally
                        {
                            web.AllowUnsafeUpdates = allowUnsafeUpdate;
                        }
                    }
                }

                if (dataSave)
                {
                    string pageUrl = SPContext.Current.Site.Url + Constants.HomePageUrl;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('" + Constants.TimesheetDeletedMsg + "'); window.location = '" + pageUrl + "';", true);
                }
            }
            catch (Exception ex)
            {
                Common.HandleException(ex, SPContext.Current.Web.Url, "btnDelete_Click", "ManageTimesheetUserControl", SPContext.Current.Web.CurrentUser.Name);
            }
        }

        /// <summary>
        /// Cancel button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Response.Redirect(SPContext.Current.Site.Url + Constants.HomePageUrl);
            }
            catch (Exception ex)
            {
                Common.HandleException(ex, SPContext.Current.Web.Url, "btnCancel_Click", "ManageTimesheetUserControl", SPContext.Current.Web.CurrentUser.Name);
            }
        }
    }
}
