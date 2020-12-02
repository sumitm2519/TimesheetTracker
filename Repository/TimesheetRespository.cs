using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimesheetTracker.Model;

namespace TimesheetTracker.Repository
{
    public class TimesheetRespository
    {
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

        public SPListItemCollection GetListItemsByAuthor(SPList lst, int userId)
        {
            SPQuery query = new SPQuery();
            query.Query = @"<Where><Eq><FieldRef Name='Author' LookupId='TRUE' /><Value Type='Integer'>" + userId + @"</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";

            return lst.GetItems(query);
        }

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
