﻿using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimesheetTracker.Helper;
using TimesheetTracker.Repository;

namespace TimesheetTracker.WebPart.ListTimesheet
{
    /// <summary>
    /// Timesheet listing class
    /// </summary>
    public partial class ListTimesheetUserControl : UserControl
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
                    BindTimesheet();
                }
            }
            catch (Exception ex)
            {
                Common.HandleException(ex, SPContext.Current.Web.Url, "Page_Load", "ListTimesheetUserControl", SPContext.Current.Web.CurrentUser.Name);
            }
        }


        private void BindTimesheet()
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
                Common.HandleException(ex, SPContext.Current.Web.Url, "BindTimesheet", "ListTimesheetUserControl", SPContext.Current.Web.CurrentUser.Name);
            }
        }
        /// <summary>
        /// Gridview row data bound event to bind view, edit link and formatted timesheet date.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTimesheet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HyperLink lnkView = e.Row.FindControl("lnkView") as HyperLink;
                    if (lnkView != null)
                    {
                        lnkView.NavigateUrl = SPContext.Current.Site.Url + Constants.TimesheetPageUrl + "?op=r&tid=" + DataBinder.Eval(e.Row.DataItem, "ID");
                    }

                    HyperLink lnkEdit = e.Row.FindControl("lnkEdit") as HyperLink;
                    if (lnkEdit != null)
                    {
                        lnkEdit.NavigateUrl = SPContext.Current.Site.Url + Constants.TimesheetPageUrl + "?op=e&tid=" + DataBinder.Eval(e.Row.DataItem, "ID");
                    }

                    HyperLink lnkDelete = e.Row.FindControl("lnkDelete") as HyperLink;
                    if (lnkDelete != null)
                    {
                        lnkDelete.NavigateUrl = SPContext.Current.Site.Url + Constants.TimesheetPageUrl + "?op=d&tid=" + DataBinder.Eval(e.Row.DataItem, "ID");
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
                Common.HandleException(ex, SPContext.Current.Web.Url, "gvTimesheet_RowDataBound", "ListTimesheetUserControl", SPContext.Current.Web.CurrentUser.Name);
            }

        }


        /// <summary>
        /// page changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTimesheet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvTimesheet.PageIndex = e.NewPageIndex;
                BindTimesheet();
            }
            catch (Exception ex)
            {
                Common.HandleException(ex, SPContext.Current.Web.Url, "gvTimesheet_PageIndexChanging", "ListTimesheetUserControl", SPContext.Current.Web.CurrentUser.Name);
            }
        }
    }
}
