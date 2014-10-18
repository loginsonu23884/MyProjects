<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/BT_MasterPage.master"
    CodeFile="ViewIssue.aspx.cs" Inherits="Module_BugTracker_ReportIssueList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="<%=oAppPath %>/JS/jquery-1.5.1.min.js" type="text/javascript"></script>
    <script src="<%=oAppPath %>/JS/grid.locale-en.js" type="text/javascript"></script>
    <script src="<%=oAppPath %>/JS/jquery.jqGrid.min.js" type="text/javascript"></script>
    <%--Fancy box popup Css style--%>
    <%-- Jquery grid Scripts--%>
    <%--<script src="Scripts/FancyBoxNew/lib/jquery-1.7.1.min.js" type="text/javascript"></script>--%>
    <script src="../../Plugin/FancyBoxNew/lib/jquery.mousewheel-3.0.6.pack.js" type="text/javascript"></script>
    <script src="../../Plugin/FancyBoxNew/source/jquery.fancybox.pack.js" type="text/javascript"></script>
    <script src="../../Plugin/FancyBoxNew/source/jquery.fancybox.js" type="text/javascript"></script>
    <link href="../../Plugin/FancyBoxNew/source/jquery.fancybox.css" rel="stylesheet"
        type="text/css" />
    <%-- Jquery grid Css style--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="<%=oAppPath%>/Plugin/Calender/jquery-ui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtDate.ClientID %>").datepicker({ showOn: 'button', buttonImageOnly: true, buttonImage: '<%=oAppPath%>/Plugin/Calender/icon_cal.png' });
            $("#<%=txtDateTo.ClientID %>").datepicker({ showOn: 'button', buttonImageOnly: true, buttonImage: '<%=oAppPath%>/Plugin/Calender/icon_cal.png' });
        });
    </script>
    <style type="text/css">
        .myAltRowClass
        {
            background-color: Aqua;
            background-image: none;
            font-weight: bold;
        }
        .myHiredRowClass
        {
            background-color: Lime;
            background-image: none;
            font-weight: bold;
        }
        #divButton
        {
            height: 27px;
        }
    </style>
    <script type="text/javascript">



        function getfilter() {

            Projectlistid = getfiltervalue(document.getElementById('<%=lstproject.ClientID %>'));


            Statuslistid = getfiltervalue(document.getElementById('<%=lststatus.ClientID %>'));

            Prioritylistid = getfiltervalue(document.getElementById('<%=lstpriority.ClientID %>'));

            Assignedtolistid = getfiltervalue(document.getElementById('<%=lstassignedto.ClientID %>'));

            if (document.getElementById('ctl00_ContentPlaceHolder1_rblcategory_0').checked == true) {

                categoryid = document.getElementById('ctl00_ContentPlaceHolder1_rblcategory_0').value;


            } else {
                if (document.getElementById('ctl00_ContentPlaceHolder1_rblcategory_1').checked == true) {
                    categoryid = document.getElementById('ctl00_ContentPlaceHolder1_rblcategory_1').value;

                }

            }
        }
           

    </script>
    <script type="text/javascript">

        function getfiltervalue(x) {

            var idList = "";

            for (var i = 0; i < x.options.length; i++) {

                if (x.options[i].selected == true) {
                    if (idList == "") {
                        idList = x.options[i].value;
                    } else {
                        idList = idList + "," + x.options[i].value;
                    }
                }
            }
            return idList;
        }
    </script>
    <script type="text/javascript">

        function funFileDetail(result) {

            popup('<%=oAppPath %>/Ajax.aspx?Type=Veiwpopup&ReportIssueid=' + result);

        }
    </script>
    <script type="text/javascript">
        function funComments(reportIssueid) {
            window.location = ("<%=oAppPath %>/Module/BugTracker/Communication.aspx?ReportIssueid=" + reportIssueid);
        }
    </script>
    <script type="text/javascript">
        function OpenAttachment(reportIssueId) {
            popup('<%=oAppPath %>/Module/BugTracker/ViewAttachment.aspx?ReportIssueID=' + reportIssueId);
        }
        function ViewAssignedUser(reportIssueId) {
            popup('<%=oAppPath %>/Module/BugTracker/ViewAssignUser.aspx?ReportIssueID=' + reportIssueId);
        }
    </script>
    <script type="text/javascript">
        //this fancy box function open view details of user request
        try {
            function popup(location) {
                $(function () {
                    $.fancybox({
                        'AutoResize': true,
                        'autoScale': true,
                        'transitionIn': 'fade',
                        'transitionOut': 'fade',
                        'type': 'iframe',
                        'href': location

                    });

                });
            }
        } catch (e) {
            alert(e);
        }
    </script>
    <script type="text/javascript">

        var Query = "";
        var Summary = false;
        var Description = false;
        var Assignedtouserid = 0;
        var Category = 1;
        var Priority = 0;
        var Status = 0;
        var Project = 0;
        var Userid = '<%=UserId %>';
        var IsDate = true;
        var FromDate = '<%=System.DateTime.Now.AddDays(-18).ToShortDateString()%>';
        var EndDate = '<%=System.DateTime.Now.ToShortDateString()%>';

        jQuery(document).ready(function () {
            loadgrid();
            $(window).bind('resize', function () {
                var width = $(window).width();
                width = width - ((width * 20) / 100);
                $("#list").setGridWidth(width);
            }).trigger('resize');
        });

        function ClientGridData() {
            jQuery("#list").jqGrid().setGridParam({
                postData: {
                    Query: document.getElementById('<%=txtfilter.ClientID %>').value,
                    Summary: document.getElementById('<%=chksummary.ClientID %>').checked,
                    Description: document.getElementById('<%=chkdescription.ClientID %>').checked,
                    Assignedtouserid: Assignedtolistid,
                    Priority: Prioritylistid,
                    Status: Statuslistid,
                    Project: Projectlistid,
                    Category: categoryid,
                    IsDate: document.getElementById('<%=chkisDate.ClientID %>').checked,
                    FromDate: document.getElementById('<%=txtDate.ClientID %>').value,
                    EndDate: document.getElementById('<%=txtDateTo.ClientID %>').value
                }
            }).trigger("reloadGrid");
        }




        function loadgrid() {

            jQuery("#list").jqGrid({
                url: '<%=oAppPath %>/Ajax.aspx?Type=VeiwIssue',
                postData: {
                    Query: Query,
                    Summary: Summary,
                    Description: Description,
                    Assignedtouserid: Assignedtouserid,
                    Priority: Priority,
                    Status: Status,
                    Project: Project,
                    Category: Category,
                    IsDate: IsDate,
                    FromDate: FromDate,
                    EndDate: EndDate,
                    Userid: Userid
                },
                datatype: 'xml',
                mtype: 'POST',
                colNames: ['IssueNo', 'Summary', 'Assigned', 'Reportedby ', 'Priority', 'Status', 'Postdate', 'Updated', 'Duedate', 'Attachment'],
                colModel: [
                    { name: 'IssueNo', index: 'IssueNo', width: '3%', align: 'center' },
                    { name: 'Summary', index: 'Summary', width: '5%', align: 'center' },
                    { name: 'Assigned', index: 'Assigned', width: '4%', align: 'center' },
                    { name: 'username', index: 'username', width: '4%', align: 'center' },
                    { name: 'Priority', index: 'Priority', width: '4%', align: 'center' },
                    { name: 'StatusName', index: 'StatusName', width: '4%', align: 'center' },
                    { name: 'Postdate', index: 'PostDate', width: '4%', align: 'center', sortable: true },
                    { name: 'Updated', index: 'Updated', width: '0%', align: 'center', sortable: true, hidden: true },
                    { name: 'Duedate', index: 'Duedate', width: '4%', align: 'center' },
                    { name: 'AttachementCount', index: 'AttachementCount', width: '5%', align: 'center' }
                ],

                pager: jQuery('#pager'),
                rowNum: 20,
                rowList: [5, 10, 20, 50, 100],
                sortname: 'Postdate',
                sortorder: "desc",
                viewrecords: true,
                imgpath: '',
                hoverrows: false,
                hover: false,
                height: "100%",
                loadComplete: function () {
                    try {

                    } catch (error) { }
                },
                caption: 'Report Issue List'

            });
        }           

    </script>
    <input type="hidden" id="hdnEmailId" />
    <br />
    <div align="center">
        <asp:Label Text="" Visible="false" Font-Bold="true" ForeColor="Red" ID="lblMessage"
            runat="server"></asp:Label>
    </div>
    <br />
    <table>
        <tr>
            <td>
                <b>Search By</b>
                  <asp:CheckBox runat="server" ID="chksummary" /><b>Summary</b>
                <asp:CheckBox runat="server" ID="chkdescription" /><b>Description</b>
            </td>
            <td>
               
                <asp:TextBox runat="server" ID="txtfilter" Width="150px"></asp:TextBox>
            </td>
            <td  colspan="2">
           
            </td>
        </tr>
        <tr>
            <td  id="trAssignedTo" runat="server" visible="false">
                <b>Assigned To:</b><br />
                <asp:ListBox ID="lstassignedto" Width="250px" Height="100px" runat="server" SelectionMode="Multiple">
                </asp:ListBox>
            </td>
            <td>
                <b>Priority:</b><br />
                <asp:ListBox ID="lstpriority" Width="250px" Height="100px" runat="server" SelectionMode="Multiple">
                </asp:ListBox>
            </td>
            <td>
                <b>Status:</b><br />
                <asp:ListBox ID="lststatus" Width="250px" Height="100px" runat="server" SelectionMode="Multiple">
                </asp:ListBox>
            </td>
           
            <td><b>Project:</b> <br />
                <asp:ListBox ID="lstproject" Width="250px" Height="100px" runat="server" SelectionMode="Multiple">
                </asp:ListBox>
            </td>
        </tr>
        
        <tr>
            <td>
                <b>Issue Type</b>
            </td>
            <td colspan="3">
                <asp:RadioButtonList ID="rblcategory" RepeatDirection="Horizontal" runat="server"
                    DataTextField="Category" DataValueField="Categoryid">
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                <b>IsDate:</b>
            </td>
            <td colspan="2">
                <asp:CheckBox ID="chkisDate" runat="server" Text="" Checked="true" />
                <asp:TextBox ID="txtDate" runat="server" ToolTip="Select Date"></asp:TextBox>
                <asp:TextBox ID="txtDateTo" runat="server" ToolTip="Select Date"></asp:TextBox>
            </td>
            <td>
             <input type="button" value="Search" id="ButtonSearch" onclick="getfilter();ClientGridData();" />
            </td>
        </tr>
      
    </table>
    <div id="pager" class="scroll" style="text-align: center;">
    </div>
    <table id="list" class="scroll" cellpadding="0" cellspacing="0">
    </table>
</asp:Content>
