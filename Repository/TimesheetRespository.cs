using Microsoft.SharePoint;
using System;
using TimesheetTracker.Model;

namespace TimesheetTracker.Repository
{
    /// <summary>
    /// TimesheetRespository Class
    /// </summary>
    public class TimesheetRespository
    {
        /// <summary>
        /// Add/Update Timesheet
        /// </summary>
        /// <param name="lstTimesheet"></param>
        /// <param name="timesheet"></param>
        /// <returns></returns>
        public bool SaveTimesheet(SPList lstTimesheet, Timesheet timesheet)
        {
            SPListItem item = null;
            if (timesheet.ID > 0)
            {
                item = lstTimesheet.GetItemById(timesheet.ID);
            }
            else
            {
                item = lstTimesheet.Items.Add();
            }
            item["Hours"] = timesheet.Hours;
            item["Description"] = timesheet.Description;
            item["Category"] = timesheet.Category;
            item["TimesheetDate"] = timesheet.TimesheetDate;
            item.Update();

            return true;
        }

        /// <summary>
        /// Get items by author
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public SPListItemCollection GetListItemsByAuthor(SPList lst, int userId)
        {
            SPQuery query = new SPQuery();
            query.Query = @"<Where><Eq><FieldRef Name='Author' LookupId='TRUE' /><Value Type='Integer'>" + userId + @"</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";

            return lst.GetItems(query);
        }

        /// <summary>
        /// Get list item by id
        /// </summary>
        /// <param name="web"></param>
        /// <param name="listName"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public SPListItem GetListItemById(SPWeb web, string listName, int itemId)
        {
            SPListItem item = null;
            SPList lst = web.Lists[listName];
            if (lst != null)
            {
                item = lst.GetItemById(itemId);
            }
            return item;
        }

        /// <summary>
        /// Delete list item by id
        /// </summary>
        /// <param name="web"></param>
        /// <param name="listName"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public bool DeleteListItemById(SPWeb web, string listName, int itemId)
        {
            bool isDelete = false; ;
            SPList lst = web.Lists[listName];
            if (lst != null)
            {
                SPListItem item = lst.GetItemById(itemId);
                if (item != null)
                {
                    item.Delete();
                    isDelete = true;
                }
            }
            return isDelete;
        }

        /// <summary>
        /// Get timesheet data by user and timesheet date
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="userId"></param>
        /// <param name="timesheetDate"></param>
        /// <returns></returns>
        public SPListItemCollection GetTimesheetsByUserAndDate(SPList lst, int userId, DateTime timesheetDate)
        {
            SPQuery query = new SPQuery();
            query.Query = @"<Where><And><Eq><FieldRef Name='Author' LookupId='TRUE' /><Value Type='Integer'>" + userId + @"</Value></Eq>
<Eq><FieldRef Name='TimesheetDate' /><Value Type='DateTime' IncludeTimeValue='FALSE'>" + timesheetDate.ToString("yyyy-MM-dd") + @"</Value></Eq>                            
</And></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";

            return lst.GetItems(query);
        }
    }
}
