<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewTimesheetUserControl.ascx.cs" Inherits="TimesheetTracker.WebPart.NewTimesheet.NewTimesheetUserControl" %>
<table>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>Timesheet Date :
        </td>
        <td>
            <SharePoint:DateTimeControl ID="dtDate" runat="server" DateOnly="true" />
        </td>
    </tr>
    <tr>
        <td>Hours :
        </td>
        <td>
            <asp:TextBox ID="txtHours" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Category :
        </td>
        <td>
            <asp:DropDownList ID="ddlCategory" runat="server">
                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                <asp:ListItem Text="Billable" Value="Billable"></asp:ListItem>
                <asp:ListItem Text="Non-Billable" Value="Non-Billable"></asp:ListItem>
                <asp:ListItem Text="Upskilling" Value="Upskilling"></asp:ListItem>
                <asp:ListItem Text="Meeting" Value="Meeting"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Description :
        </td>
        <td>
            <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Width="500px" Height="100px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            <asp:Button runat="server" Text="Submit" ID="btnSubmit" OnClick="btnSubmit_Click" OnClientClick="return ValidateSubmit();" />&nbsp;<asp:Button runat="server" Text="Cancel" ID="btnCancel" OnClick="btnCancel_Click" />
        </td>
    </tr>
</table>
<script type="text/javascript">
    function ValidateSubmit() {
        var hoursValue = document.getElementById("<%=txtHours.ClientID%>").value;
        if (hoursValue != "") {
            //hoursValue.match(/^-?\d*(\.\d+)?$/)
            //isNumber(hoursValue)
            if (hoursValue.match(/^-?\d*(\.\d+)?$/)) {

                if (hoursValue <= 0) {
                    alert("Enter valid hours.");
                    return false;
                }

                if (hoursValue > 8) {
                    alert("Timesheet hours should not be more than 8 hours.");
                    return false;
                }
            }
            else {
                alert("Enter valid hours.");
                return false;
            }
        }
        else {
            alert("Enter Timesheet hours.");
            return false;
        }

        var category = document.getElementById("<%=ddlCategory.ClientID%>").value;
        if (category == 0) {
            alert("Select Category.");
            return false;
        }

        return true;
    }
</script>
