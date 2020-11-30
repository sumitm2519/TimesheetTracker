﻿using Microsoft.SharePoint;
using System;
using System.Web.UI;
using TimesheetTracker.Helper;

namespace TimesheetTracker.WebPart.EditTimesheet
{
    /// <summary>
    /// Edit timesheet class
    /// </summary>
    public partial class EditTimesheetUserControl : UserControl
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
                    if (Request.QueryString["isdelete"] != null)
                    {
                        btnSubmit.Visible = false;
                        lblPageTitle.Text = "Delete Timesheet";
                    }
                    else
                    {
                        btnDelete.Visible = false;
                        lblPageTitle.Text = "Edit Timesheet";
                    }
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
                                        SPFieldMultiLineText multilineField = item.Fields.GetField("Description") as SPFieldMultiLineText;
                                        if (multilineField != null)
                                        {
                                            txtDescription.Text = multilineField.GetFieldValueAsText(item["Description"]);
                                        }
                                        if (ddlCategory.Items.FindByValue(Convert.ToString(item["Category"])) != null)
                                        {
                                            ddlCategory.Items.FindByValue(Convert.ToString(item["Category"])).Selected = true;
                                        }
                                        //txtDescription.Text = Convert.ToString(item["Description"]);
                                        dtDate.SelectedDate = Convert.ToDateTime(item["TimesheetDate"]);
                                        txtHours.Text = Convert.ToString(item["Hours"]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
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

        /// <summary>
        /// Submit buton click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool dataSave = false;
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList lstTimesheet = web.Lists["Timesheet"];
                        if (lstTimesheet != null)
                        {
                            int itemId = Convert.ToInt32(Request.QueryString["tid"]);
                            if (Common.ValidHours(lstTimesheet, dtDate.SelectedDate, Convert.ToDouble(txtHours.Text.Trim()), itemId))
                            {
                                SPListItem item = lstTimesheet.GetItemById(itemId);
                                if (item != null)
                                {
                                    item["Category"] = ddlCategory.SelectedItem.Text;
                                    item["Description"] = txtDescription.Text;
                                    item["TimesheetDate"] = dtDate.SelectedDate;
                                    item["Hours"] = txtHours.Text;
                                    item.Update();
                                    dataSave = true;
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Total hours per day should not be greater than 8.');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Timesheet list not found.');", true);
                        }
                    }
                }

                if (dataSave)
                {
                    string pageUrl = SPContext.Current.Site.Url + "/SitePages/Home.aspx";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Timesheet Updated..!'); window.location = '" + pageUrl + "';", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Remove timesheet
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
                        SPList lstTimesheet = web.Lists["Timesheet"];
                        if (lstTimesheet != null)
                        {
                            SPListItem item = lstTimesheet.GetItemById(Convert.ToInt32(Request.QueryString["tid"]));
                            if (item != null)
                            {
                                item.Delete();
                                dataSave = true;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Timesheet list not found.');", true);
                        }
                    }
                }

                if (dataSave)
                {
                    string pageUrl = SPContext.Current.Site.Url + "/SitePages/Home.aspx";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Timesheet Deleted..!'); window.location = '" + pageUrl + "';", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
