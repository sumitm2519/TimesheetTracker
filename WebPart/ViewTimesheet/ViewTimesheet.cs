using System.ComponentModel;
using System.Web.UI;

namespace TimesheetTracker.WebPart.ViewTimesheet
{
    [ToolboxItemAttribute(false)]
    public class ViewTimesheet : System.Web.UI.WebControls.WebParts.WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/TimesheetTracker.WebPart/ViewTimesheet/ViewTimesheetUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }
    }
}
