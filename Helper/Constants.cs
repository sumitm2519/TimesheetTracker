using System.Configuration;

namespace TimesheetTracker.Helper
{
    /// <summary>
    /// Constants Class
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Timesheet list name fetching from web.config file.
        /// </summary>
        public static string TimesheetListName = ConfigurationManager.AppSettings["TimesheetList"];

        /// <summary>
        /// Message for notifying user for timesheet updation
        /// </summary>
        public static string TimesheetUpdatedMsg = "Timesheet entry updated..!";

        /// <summary>
        /// Message for notifying user for timesheet addition
        /// </summary>
        public static string TimesheetAddedMsg = "Timesheet entry added..!";

        /// <summary>
        /// Message for notifying user for timesheet deletion
        /// </summary>
        public static string TimesheetDeletedMsg = "Timesheet entry deleted..!";

        /// <summary>
        /// Custom error message
        /// </summary>
        public static string ErrorMsg = "Error in Operation, Contact the administrator";
    }
}
