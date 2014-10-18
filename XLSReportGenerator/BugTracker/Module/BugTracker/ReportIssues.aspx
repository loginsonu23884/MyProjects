<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/BT_MasterPage.master"
    CodeFile="ReportIssues.aspx.cs" Inherits="Module_BugTracker_ReportIssues" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="<%=oAppPath%>/Plugin/Calender/jquery-ui.min.js"></script>
    <script src="../../Plugin/FancyBoxNew/lib/jquery.mousewheel-3.0.6.pack.js" type="text/javascript"></script>
    <script src="../../Plugin/FancyBoxNew/source/jquery.fancybox.pack.js" type="text/javascript"></script>
    <script src="../../Plugin/FancyBoxNew/source/jquery.fancybox.js" type="text/javascript"></script>
    <link href="../../Plugin/FancyBoxNew/source/jquery.fancybox.css" rel="stylesheet"
        type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div align="center">
        <asp:Label Text="" Visible="false" Font-Bold="true" ForeColor="Red" ID="lblMessage"
            runat="server"></asp:Label>
    </div>
     <script type="text/javascript">
         jQuery(document).ready(function () {            
             $("#<%=txtduedate.ClientID%>").datepicker({ showOn: 'button', buttonImageOnly: true, buttonImage: '<%=oAppPath%>/Plugin/Calender/icon_cal.png' });
             setTimeout(function () { $("#<%=lblrecordsaved.ClientID%>").hide(); }, 5000);
         });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=ddlproject.ClientID%>").change(function () {
                var projectId = document.getElementById('<%=ddlproject.ClientID %>').value;          
              
                $.ajax({
                    type: "POST",
                    url: '<%=oAppPath %>/Ajax.aspx?Method=GetAssignmentNo&projectId=' + projectId,
                    dataType: 'json',
                    data: { projectId: projectId },
                    success: function (data, status, xhr) {
                        if (data != null) {                          
                            $("#<%=txtAssignmentNo.ClientID%>").val(data);
                        }
                    }
                });

            });
        });
    </script>
    <script type="text/javascript">
        //** This code for create dynamicaly file uploader & textbox's
        var counter = 1;
        //** this function create 1'st Div & Append many Div's when attach more files
        function AddFileUpload() {

            var div = $(document.createElement('div')).attr("id", 'FileUploadContainer' + counter);
            div.html('<input id="txt' + counter + '" name = "textbox' + counter + '" type="text"/>   <input id="file' + counter + '"  name = "file' + counter + '" type="file"  />  <span id="spn' + counter + '" style="display: none;font-size: x-small;"></span>');
            div.appendTo("#TextBoxesGroup");
            document.getElementById('<%=hdnValidation.ClientID %>').value = counter;
            counter = counter + 1;
        }
        //** This code for remove file added.
        function RemoveFileUpload(div) {
            if (document.getElementById('<%=hdnValidation.ClientID %>').value > 0) {
                document.getElementById('<%=hdnValidation.ClientID %>').value = document.getElementById('<%=hdnValidation.ClientID %>').value - 1;
            }
            if (counter > 1) {
                counter = counter - 1;
            }
            if (counter < 1) {
                alert("No more textbox to remove");
                return false;
            }
            $("#FileUploadContainer" + counter).remove();
        }
    </script>
   
   
     <script type="text/javascript">
         function AssignedDeveloper() {

             popup("<%=oAppPath %>/Module/BugTracker/AddUser.aspx");
         }
    </script>
            <script type="text/javascript">
                //this fancy box function open view details of user request
                try {
                    function popup(location) {
                        $(function () {
                            $.fancybox({
                                'width': 900,
                                'height': '120%',
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

     <br />
    <br />

    <fieldset style="width: 95%; margin-left: 20px; margin-right: 20px;">
        <div align="center">
            
            <asp:Label runat="server" ID="lblrecordsaved" Font-Bold="" ForeColor="red" Visible="False"></asp:Label>
        </div>
        <table width="90%">
            <tr>
                   <td valign="top">
                    <table cellpadding="4" cellspacing="0" width="100%">
                        <tr>
                            <td align="right" valign="top">
                                Project:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlproject"  runat="server" initialvalue="0" DataTextField="Project"
                                    DataValueField="id" Width="200px" 
                                   >
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RFVproject" InitialValue="0" runat="server" ControlToValidate="ddlproject"
                                    ErrorMessage="Please select required project"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                         <tr>
                            <td align="right" valign="top">
                                Assignment No:
                            </td>
                            <td>
                               <asp:TextBox ID="txtAssignmentNo" runat="server" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Category:
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblcategory" RepeatDirection="Horizontal" runat="server"
                                    DataTextField="Category" DataValueField="Categoryid">
                                </asp:RadioButtonList>
                                <br />
                                <asp:RequiredFieldValidator ID="RFVCategory" ControlToValidate="rblcategory" runat="server"
                                    ErrorMessage="Please select any category"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Summary:
                            </td>
                            <td>
                                <asp:TextBox ID="txtsummary" TextMode="MultiLine" runat="server" Height="65px" Width="350px"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="RFVtxtsummary" runat="server" ControlToValidate="txtsummary"
                                    ErrorMessage="Please write any summary"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Assigned to:
                            </td>
                            <td align="left">
                            <input id="btnAssignToDeveloper" runat="server" width="100px" type="button" value="Assign Developer" onclick="AssignedDeveloper()"
                         />
                    
                                <%--<asp:DropDownList ID="ddlassignedto" runat="server" initialvalue="0" DataTextField="username"
                                    DataValueField="id">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RFVAssignedto" InitialValue="0" runat="server" ControlToValidate="ddlassignedto"
                                    ErrorMessage="Please select assingned to "></asp:RequiredFieldValidator>
                            --%>
                            </td>
                            
                        </tr>
                     <%--   <tr>
                        <td colspan="2"> 
                        <asp:GridView ID="gv" runat="server"></asp:GridView>
                        </td>
                        <td></td>
                        </tr>--%>
                        <tr>
                            <td align="right" valign="top">
                                Reproducibilty:
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlreproducibilty" runat="server" initialvalue="0" DataTextField="Reproducibilty"
                                    DataValueField="Reproducilbityid">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RFVReproducibility" InitialValue="0" ControlToValidate="ddlreproducibilty"
                                    runat="server" ErrorMessage="Please select any option"> </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Severity:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlseverity" runat="server" intialvalue="0" DataTextField="Severity"
                                    DataValueField="Severityid">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RFVSeverity" InitialValue="0" ControlToValidate="ddlseverity"
                                    runat="server" ErrorMessage="Please select any option"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Priority:
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlpriority" runat="server" initialvalue="0" DataTextField="Priority"
                                    DataValueField="Prorirityid">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RFVPriority" InitialValue="0" ControlToValidate="ddlpriority"
                                    runat="server" ErrorMessage="Please select any option"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Browser:
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlbrowser" runat="server" initialvalue="0" DataTextField="Browser"
                                    DataValueField="Browserid" Height="16px">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RFVBrowser" InitialValue="0" ControlToValidate="ddlbrowser"
                                    runat="server" ErrorMessage="Please select any browser"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                OS Version:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlOSVersion" runat="server" initialvalue="0" DataTextField="OperatingSystem"
                                    DataValueField="Oid">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RFVOSVersion" InitialValue="0" runat="server" ControlToValidate="ddlOSVersion"
                                    ErrorMessage="Please select any OS version"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                      

                        </table>
                </td>
                <td width="50%" valign="top">
                    <table cellpadding="5" cellspacing="10" width="100%">
                        <tr>
                            <td align="right">
                                Status:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlstatus" runat="server" initialvalue="0" DataTextField="Project"
                                    DataValueField="id">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVStatus" InitialValue="0" ControlToValidate="ddlstatus"
                                    runat="server" ErrorMessage="Please select any status"></asp:RequiredFieldValidator>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Due Date:

                            </td>
                            <td>
                                <asp:TextBox ID="txtduedate" runat="server"></asp:TextBox><br />
                                  <asp:RequiredFieldValidator ID="RFVduedate" runat="server" ControlToValidate="txtduedate"
                                    ErrorMessage="Please select any date"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    
                        <tr>
                            <td align="right">
                                Defect Description:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtdefectdescription" Width="300px" TextMode="MultiLine" runat="server"
                                    Height="50px"></asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Additional&nbsp;Information:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtadditionalinformation" Width="300px" TextMode="MultiLine" runat="server"
                                    Height="50px"></asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Expected Result:
                            </td>
                            <td>
                                <asp:TextBox ID="txtexpectedresult" Width="300px" TextMode="MultiLine" runat="server"
                                    Height="50px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Actual Result:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtactualresult" Width="300px" TextMode="MultiLine" runat="server"
                                    Height="50px"></asp:TextBox><br />
                               
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Upload Files:
                            </td>
                            <td>
                                <div id="TextBoxesGroup">
                                    <div id="FileUploadContainer0">
                                        <input id="txt0" name="textbox0" type="text" title="Enter file detail or name" />
                                        <input id="file0" name="file0" type="file" />
                                        <span id="spn0" style="display: none; font-size: x-small;"></span>
                                        <!--FileUpload Controls will be added here -->
                                        &nbsp;&nbsp;
                                    </div>
                                    <span id="spnfile" style="display: none"></span>
                                </div>
                                <a id="hlk1" style="font-size: medium" onclick="AddFileUpload()">
                                    <img id="img1" height="20px" width="20px" src="../../Stylesheet/images/add.png" tabindex="7"
                                        title="Add attachment" /></a> <a id="hlk2 '" onclick="RemoveFileUpload(this)">
                                            <img id="img2" height="20px" width="20px" src="../../Stylesheet/images/delete.png"
                                                tabindex="7" title="Remove attachment" /></a> Add/Remove attachment
                            </td>
                          
                        </tr>
                        <tr>
                            <td>
                                
                            </td>
                            <td>
                              <%=userMsg%>   
                            </td>                               
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </fieldset>
    <table align="center">
        <tr>
           
            <td>
                <asp:Button ID="btnPost" runat="server" Text="Submit" Font-Bold="true" ToolTip="Submit" 
                    OnClick="btnPost_Click1" Style="height: 26px"  />&nbsp;
                <asp:Button ID="btnReset" runat="server" Text="Submit & List" Font-Bold="true" ToolTip="Submit & List"
                    OnClick="btnReset_Click" />
                <asp:Button ID="btnBackToList" runat="server" Text="Back To List" CausesValidation="false"
                    Font-Bold="true" ToolTip="Post" OnClick="btnBackToList_Click" />
            </td>
             <td>
               <asp:Button runat="server" ID="btncancel" Text="Cancel" Font-Bold="True" 
                     ToolTip="Cancel" Visible="False" onclick="btncancel_Click"/>
            </td>
        </tr>
        <asp:HiddenField ID="hdnValidation" runat="server" Value="0" />
        <asp:Literal runat="server" ID="ltrecordsaved"></asp:Literal>
       
    </table>
</asp:Content>
