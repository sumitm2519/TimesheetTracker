<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageTimesheetUserControl.ascx.cs" Inherits="TimesheetTracker.WebPart.ManageTimesheet.ManageTimesheetUserControl" %>
<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
<script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
<script type="text/javascript" src="../../../../_layouts/15/TimesheetTracker/JS/ValidateTimesheetData.js"></script>

<table>
    <tr>
        <td align="left">
            <h4>
                <asp:Label ID="lblPageTitle" runat="server"></asp:Label></h4>
        </td>
    </tr>
    <tr>
        <td>&nbsp;

            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Timesheet Date
        </td>
        <td>
            <SharePoint:DateTimeControl ID="dtDate" runat="server" DateOnly="true" />
            <asp:Label ID="lblDate" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>Hours
        </td>
        <td>
            <asp:TextBox ID="txtHours" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Label ID="lblHours" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>Category
        </td>
        <td>
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                <asp:ListItem Text="Billable" Value="Billable"></asp:ListItem>
                <asp:ListItem Text="Non-Billable" Value="Non-Billable"></asp:ListItem>
                <asp:ListItem Text="Upskilling" Value="Upskilling"></asp:ListItem>
                <asp:ListItem Text="Meeting" Value="Meeting"></asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="lblCategory" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Description
        </td>
        <td>
            <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Width="500px" Height="100px" CssClass="form-control input-lg"></asp:TextBox>
            <asp:Label ID="lblDescription" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            <asp:LinkButton CssClass="btn btn-primary" runat="server" Text="Submit" ID="btnSubmit" OnClientClick="return ValidateSubmit();" OnClick="btnSubmit_Click"></asp:LinkButton>
            &nbsp;&nbsp;<asp:LinkButton CssClass="btn btn-primary" runat="server" Text="Delete" ID="btnDelete" OnClick="btnDelete_Click"></asp:LinkButton>
            &nbsp;&nbsp;<asp:LinkButton CssClass="btn btn-primary" runat="server" Text="Cancel" ID="btnCancel" OnClick="btnCancel_Click"></asp:LinkButton>
        </td>
    </tr>
</table>
