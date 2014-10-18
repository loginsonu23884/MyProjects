<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" MasterPageFile="~/MasterPages/BT_MasterPage.master"
    Inherits="Module_BugTracker_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="<%=oAppPath%>/Plugin/Calender/jquery-ui.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script type="text/javascript">
        function funComments(reportIssueid) {

            window.location = ("<%=oAppPath %>/Module/BugTracker/Communication.aspx?ReportIssueid=" + reportIssueid);
        }


     
    </script>
    <br />
    <table style="width: 98%; marign: 1%;">
        <tr>
            <td style="width: 60%" valign="top">
                <fieldset>
                    <legend><b>Activity Stream</b></legend>
                    <%=cmnt%>
                    <div align="center">
                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Show More..............." Visible="false"
                            Font-Bold="true" ToolTip="Show More" Font-Underline="false" Font-Italic="true"
                            OnClick="LinkButton1_Click"></asp:LinkButton>
                    </div>
                </fieldset>
            </td>
            <td valign="top" style="width: 40%">
                <b style="font-size: 14px">Assinged To Me</b>
                <asp:GridView ID="gvassignedissues" runat="server" AutoGenerateColumns="False" Width="100%"
                    Height="100%" CellPadding="4" CellSpacing="4">
                    <Columns>
                        <asp:BoundField DataField="Priority" HeaderText="Priority" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="IssueNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <a href="javascript:funComments(<%#Eval("ReportIssueid")%>)">
                                    <%#Eval("IssueNo")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Summary" HeaderText="Summary" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="left" />
                        <asp:BoundField DataField="Severity" HeaderText="Severity" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center" />
                    </Columns>
                </asp:GridView>
                <br />
                <fieldset>
                    <legend style="font-weight: bold">Project Status</legend>
                    <asp:GridView ID="gvProjectStatusList" runat="server" AutoGenerateColumns="false"
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="ProjectName" HeaderText="Project Name" ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="Left" />
                            <asp:TemplateField HeaderText="Issue No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <a href="javascript:funComments(<%#Eval("ReportIssueid")%>)">
                                        <%#Eval("IssueNo")%></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="StatusName" HeaderText="Status" ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="Left" />
                        </Columns>
                    </asp:GridView>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
