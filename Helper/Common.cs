using Microsoft.SharePoint;
using System;

namespace TimesheetTracker.Helper
{
    /// <summary>
    /// Common helper class
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// Validate timesheet hours - should not be greater than 8 per day
        /// </summary>
        /// <param name="list"></param>
        /// <param name="enteredDate"></param>
        /// <param name="enteredHours"></param>
        /// <param name="timesheetId"></param>
        /// <returns></returns>
        public static bool ValidHours(SPList list, DateTime enteredDate, double enteredHours, int timesheetId)
        {
            bool isValid = true;
            SPQuery query = new SPQuery();
            query.Query = @"<Where><And><Eq><FieldRef Name='Author' LookupId='TRUE' /><Value Type='Integer'>" + SPContext.Current.Web.CurrentUser.ID + @"</Value></Eq>
<Eq><FieldRef Name='TimesheetDate' /><Value Type='DateTime' IncludeTimeValue='FALSE'>" + enteredDate.ToString("yyyy-MM-dd") + @"</Value></Eq>                            
</And></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";

            SPListItemCollection itemColl = list.GetItems(query);
            if (itemColl != null && itemColl.Count > 0)
            {
                double totalHours = 0;
                foreach (SPListItem item in itemColl)
                {
                    totalHours += Convert.ToDouble(item["Hours"]);
                }

                if (totalHours >= 8)
                {
                    isValid = false;
                }
                else
                {
                    totalHours += enteredHours;
                    if (totalHours > 8)
                    {
                        isValid = false;
                    }
                }
            }
            return isValid;
        }
    }
}
