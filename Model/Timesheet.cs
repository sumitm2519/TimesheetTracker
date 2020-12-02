using System;

namespace TimesheetTracker.Model
{
    public class Timesheet
    {
        public int ID { get; set; }
        public double Hours { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime TimesheetDate { get; set; }
    }
}
