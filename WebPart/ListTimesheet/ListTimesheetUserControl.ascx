<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListTimesheetUserControl.ascx.cs" Inherits="TimesheetTracker.WebPart.ListTimesheet.ListTimesheetUserControl" %>

<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
<style>
    body {
        margin: 0;
        font-family: -apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji";
        /*font-size: 1rem;*/
        font-weight: 400;
        line-height: 1.5;
        color: #212529;
        text-align: left;
        background-color: #fff;
    }
</style>
<table>
    <tr>
        <td align="left">
            <h4>Timesheet</h4>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:GridView CssClass="table table-responsive table-striped table-hover" ID="gvTimesheet" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvTimesheet_RowDataBound"
                AllowPaging="true" OnPageIndexChanging="gvTimesheet_PageIndexChanging" PageSize="10">
                <HeaderStyle CssClass="thead-dark" />
                <Columns>
                    <asp:BoundField DataField="ID" Visible="false" />
                    <asp:TemplateField HeaderText="Timesheet Date" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblDate"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TimesheetDate" Visible="false" />
                    <asp:BoundField DataField="Hours" HeaderText="Hours" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Category" HeaderText="Category" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkView" runat="server"
                                Text="View"></asp:HyperLink>&nbsp;&nbsp;
                                        <asp:HyperLink ID="lnkEdit" runat="server"
                                            Text="Edit"></asp:HyperLink>&nbsp;&nbsp;
                                        <asp:HyperLink ID="lnkDelete" runat="server"
                                            Text="Delete"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
</table>
