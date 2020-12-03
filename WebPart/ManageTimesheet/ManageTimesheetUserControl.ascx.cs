using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
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
                    lblMsg.Text = string.Empty;
                    lblPageTitle.Text = "Timesheet";
                    trForm.Visible = false;
                    BindAllTimesheet();
                }
            }
            catch (Exception ex)
            {
                Common.HandleException(ex, SPContext.Current.Web.Url, "Page_Load", "ManageTimesheetUserControl", SPContext.Current.Web.CurrentUser.Name);
                lblMsg.Text = Constants.ErrorMsg;
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
                            ddlCategory.ClearSelection();
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
                lblMsg.Text = Constants.ErrorMsg;
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
                LinkButton btn = (LinkButton)sender;
                int timesheetId = 0;
                if (btn.CommandArgument != string.Empty)
                    timesheetId = Convert.ToInt32(btn.CommandArgument);

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
                                    BindAllTimesheet();
                                    lblPageTitle.Text = "Timesheet";
                                    lblMsg.Text = string.Empty;
                                    ClearControls();
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
                    if (timesheetId > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('" + Constants.TimesheetUpdatedMsg + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('" + Constants.TimesheetAddedMsg + "');", true);
                    }

                    trGrid.Visible = true;
                    trForm.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Common.HandleException(ex, SPContext.Current.Web.Url, "btnSubmit_Click", "ManageTimesheetUserControl", SPContext.Current.Web.CurrentUser.Name);
                lblMsg.Text = Constants.ErrorMsg;
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
                trGrid.Visible = true;
                trForm.Visible = false;
                lblPageTitle.Text = "Timesheet";
                lblMsg.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Common.HandleException(ex, SPContext.Current.Web.Url, "btnCancel_Click", "ManageTimesheetUserControl", SPContext.Current.Web.CurrentUser.Name);
                lblMsg.Text = Constants.ErrorMsg;
            }
        }

        /// <summary>
        /// Gridview page changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTimesheet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvTimesheet.PageIndex = e.NewPageIndex;
                BindAllTimesheet();
            }
            catch (Exception ex)
            {
                Common.HandleException(ex, SPContext.Current.Web.Url, "gvTimesheet_PageIndexChanging", "ManageTimesheetUserControl", SPContext.Current.Web.CurrentUser.Name);
                lblMsg.Text = Constants.ErrorMsg;
            }
        }

        /// <summary>
        /// Gridview row data bound event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTimesheet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lnkView = e.Row.FindControl("lnkView") as LinkButton;
                    if (lnkView != null)
                    {
                        lnkView.CommandArgument = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
                    }

                    LinkButton lnkEdit = e.Row.FindControl("lnkEdit") as LinkButton;
                    if (lnkEdit != null)
                    {
                        lnkEdit.CommandArgument = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
                    }

                    LinkButton lnkDelete = e.Row.FindControl("lnkDelete") as LinkButton;
                    if (lnkDelete != null)
                    {
                        lnkDelete.CommandArgument = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
                    }

                    Label lblDate = e.Row.FindControl("lblDate") as Label;
                    if (lblDate != null)
                    {
                        DateTime dtDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "TimesheetDate"));
                        lblDate.Text = dtDate.ToString("yyyy-MM-dd");
                    }
                }
            }
            catch (Exception ex)
            {
                Common.HandleException(ex, SPContext.Current.Web.Url, "gvTimesheet_RowDataBound", "ManageTimesheetUserControl", SPContext.Current.Web.CurrentUser.Name);
                lblMsg.Text = Constants.ErrorMsg;
            }
        }

        /// <summary>
        /// Bind all timesheet of current user
        /// </summary>
        private void BindAllTimesheet()
        {
            try
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList lstTimesheet = web.Lists[Constants.TimesheetListName];
                        if (lstTimesheet != null)
                        {
                            TimesheetRespository objRespository = new TimesheetRespository();
                            SPListItemCollection itemColl = objRespository.GetListItemsByAuthor(lstTimesheet, SPContext.Current.Web.CurrentUser.ID);

                            if (itemColl != null && itemColl.Count > 0)
                            {
                                DataTable dtRecords = itemColl.GetDataTable();
                                dtRecords.DefaultView.Sort = "TimesheetDate";
                                gvTimesheet.DataSource = dtRecords.DefaultView.ToTable();
                                gvTimesheet.DataBind();
                            }
                            else
                            {
                                gvTimesheet.DataSource = null;
                                gvTimesheet.DataBind();
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('" + Constants.TimesheetListName + " list not found.');", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.HandleException(ex, SPContext.Current.Web.Url, "BindTimesheet", "ManageTimesheetUserControl", SPContext.Current.Web.CurrentUser.Name);
                lblMsg.Text = Constants.ErrorMsg;
            }
        }

        /// <summary>
        /// View button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkView_Click(object sender, EventArgs e)
        {
            try
            {
                trGrid.Visible = false;
                trForm.Visible = true;

                LinkButton btn = (LinkButton)sender;
                BindTimesheetData(Convert.ToInt32(btn.CommandArgument));
                lblPageTitle.Text = "View Timesheet";
                HideShowEditControls(false);
                HideShowViewControls(true);
                btnCancel.Text = "Back";
                btnSubmit.Visible = false;
            }
            catch (Exception ex)
            {
                Common.HandleException(ex, SPContext.Current.Web.Url, "lnkView_Click", "ManageTimesheetUserControl", SPContext.Current.Web.CurrentUser.Name);
                lblMsg.Text = Constants.ErrorMsg;
            }
        }

        /// <summary>
        /// Edit button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                trGrid.Visible = false;
                trForm.Visible = true;

                LinkButton btn = (LinkButton)sender;
                btnSubmit.CommandArgument = btn.CommandArgument;
                BindTimesheetData(Convert.ToInt32(btn.CommandArgument));

                lblPageTitle.Text = "Edit Timesheet";
                HideShowViewControls(false);
                HideShowEditControls(true);
                btnSubmit.Visible = true;
                btnCancel.Text = "Cancel";
            }
            catch (Exception ex)
            {
                Common.HandleException(ex, SPContext.Current.Web.Url, "lnkEdit_Click", "ManageTimesheetUserControl", SPContext.Current.Web.CurrentUser.Name);
                lblMsg.Text = Constants.ErrorMsg;
            }
        }

        /// <summary>
        /// Delete button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool dataSave = false;
                LinkButton btn = (LinkButton)sender;
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        bool allowUnsafeUpdate = web.AllowUnsafeUpdates;
                        try
                        {
                            web.AllowUnsafeUpdates = true;
                            TimesheetRespository objRepository = new TimesheetRespository();
                            dataSave = objRepository.DeleteListItemById(web, Constants.TimesheetListName, Convert.ToInt32(btn.CommandArgument));
                            BindAllTimesheet();
                            lblMsg.Text = string.Empty;
                        }
                        finally
                        {
                            web.AllowUnsafeUpdates = allowUnsafeUpdate;
                        }
                    }
                }

                if (dataSave)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('" + Constants.TimesheetDeletedMsg + "');", true);
                }
            }
            catch (Exception ex)
            {
                Common.HandleException(ex, SPContext.Current.Web.Url, "btnDelete_Click", "ManageTimesheetUserControl", SPContext.Current.Web.CurrentUser.Name);
                lblMsg.Text = Constants.ErrorMsg;
            }
        }

        /// <summary>
        /// Add new timesheet button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                trGrid.Visible = false;
                trForm.Visible = true;
                lblPageTitle.Text = "Add New Timesheet";
                HideShowEditControls(true);
                HideShowViewControls(false);
                btnSubmit.Visible = true;
                ClearControls();
                btnSubmit.CommandArgument = string.Empty;
                btnCancel.Text = "Cancel";
                lblMsg.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Common.HandleException(ex, SPContext.Current.Web.Url, "lnkAddNew_Click", "ManageTimesheetUserControl", SPContext.Current.Web.CurrentUser.Name);
                lblMsg.Text = Constants.ErrorMsg;
            }

        }

        /// <summary>
        /// Clear values from all controls of timesheet data
        /// </summary>
        private void ClearControls()
        {
            txtDescription.Text = string.Empty;
            txtHours.Text = string.Empty;
            ddlCategory.SelectedIndex = 0;
            dtDate.ClearSelection();
            lblCategory.Text = string.Empty;
            lblCategory.Text = string.Empty;
            lblDescription.Text = string.Empty;
            lblHours.Text = string.Empty;
        }
    }
}
