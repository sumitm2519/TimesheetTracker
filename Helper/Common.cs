using Microsoft.SharePoint;
using System;
using System.Configuration;
using System.IO;
using TimesheetTracker.Repository;

namespace TimesheetTracker.Helper
{
    /// <summary>
    /// Common helper class
    /// </summary>
    public static class Common
    {
        private static StreamWriter writerForErrLog;
        public const string Custom_LOGFILE_NAME = "CustomLogFile";
        public const string Custom_LOGFOLDER_NAME = "CustomErrorLOGS";

        /// <summary>
        /// Validate timesheet hours - should not be greater than 8 per day
        /// </summary>
        /// <param name="list"></param>
        /// <param name="timesheetDate"></param>
        /// <param name="enteredHours"></param>
        /// <param name="timesheetId"></param>
        /// <param name="objRepository"></param>
        /// <returns></returns>
        public static bool ValidHours(SPList list, DateTime timesheetDate, double enteredHours, int timesheetId, TimesheetRespository objRepository)
        {
            bool isValid = true;

            SPListItemCollection itemColl = objRepository.GetTimesheetsByUserAndDate(list, SPContext.Current.Web.CurrentUser.ID, timesheetDate);
            if (itemColl != null && itemColl.Count > 0)
            {
                double totalHours = 0;
                foreach (SPListItem item in itemColl)
                {
                    if (!item.ID.Equals(timesheetId))
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

        /// <summary>
        /// Write exceptions in custom log file
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="strWeb"></param>
        /// <param name="strMethod"></param>
        /// <param name="strClass"></param>
        /// <param name="strUser"></param>
        public static void HandleException(Exception ex, string strWeb, string strMethod, string strClass, string strUser)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    string strDirPath = ConfigurationManager.AppSettings["LogFilePath"];
                    string strErrLogFilePath = ConfigurationManager.AppSettings["LogFilePath"]
                    + "\\" + Custom_LOGFOLDER_NAME + "\\";
                    strErrLogFilePath += Custom_LOGFILE_NAME + "_" + DateTime.Now.Ticks.ToString() + ".log";
                    if (!File.Exists(strErrLogFilePath))
                    {
                        DirectoryInfo dirInfo =
                        new DirectoryInfo(strDirPath);
                        dirInfo.CreateSubdirectory(Custom_LOGFOLDER_NAME);
                    }
                    //Creation of log file for execution entries  
                    //------------------------------------------
                    writerForErrLog =
                       new System.IO.StreamWriter(strErrLogFilePath);
                    if (File.Exists(strErrLogFilePath))
                    {
                        writerForErrLog.WriteLine
                        ("Web: " + strWeb + Environment.NewLine);
                        writerForErrLog.WriteLine
                        ("Method: " + strMethod + Environment.NewLine);
                        writerForErrLog.WriteLine
                        ("Class: " + strClass + Environment.NewLine);
                        writerForErrLog.WriteLine
                        ("User: " + strUser + Environment.NewLine);
                        writerForErrLog.WriteLine
                        ("Date: " + System.DateTime.Now + Environment.NewLine);
                        writerForErrLog.WriteLine
                        ("Message : " + ex.Message + Environment.NewLine);
                        writerForErrLog.WriteLine
                        ("Description : " + ex.StackTrace + Environment.NewLine);
                        writerForErrLog.WriteLine("*****END****");
                        writerForErrLog.Close();
                    }
                });
            }
            catch (Exception)
            {
                writerForErrLog.Close();
            }
        }
    }
}
