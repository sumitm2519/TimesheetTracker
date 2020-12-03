using System;

namespace TimesheetTracker.Model
{
    /// <summary>
    /// Timesheet Model
    /// </summary>
    public class Timesheet
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Hours
        /// </summary>
        public double Hours { get; set; }

        /// <summary>
        /// Category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// TimesheetDate
        /// </summary>
        public DateTime TimesheetDate { get; set; }
    }
}
