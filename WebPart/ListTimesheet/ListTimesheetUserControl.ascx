<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListTimesheetUserControl.ascx.cs" Inherits="TimesheetTracker.WebPart.ListTimesheet.ListTimesheetUserControl" %>
<table width="100%">
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td align="left" width="100%">
            <h1>Timesheet</h1>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td width="100%">
            <asp:GridView ID="gvTimesheet" runat="server" Width="40%" AutoGenerateColumns="false" BorderStyle="Solid" OnRowDataBound="gvTimesheet_RowDataBound"
                BorderWidth="1px">
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
                            <table>
                                <tr>
                                    <td>
                                        <asp:HyperLink ID="lnkView" runat="server"
                                            Text="View"></asp:HyperLink></td>
                                    <td>&nbsp;&nbsp;</td>
                                    <td>
                                        <asp:HyperLink ID="lnkEdit" runat="server"
                                            Text="Edit"></asp:HyperLink></td>
                                </tr>
                            </table>
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
