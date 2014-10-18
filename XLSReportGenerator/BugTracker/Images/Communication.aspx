<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/BT_MasterPage.master"
    AutoEventWireup="true" CodeFile="Communication.aspx.cs" Inherits="Module_BugTracker_Communication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="<%=OAppPath %>/JS/jquery-1.5.1.min.js" type="text/javascript"></script>
    <script src="<%=OAppPath %>/JS/grid.locale-en.js" type="text/javascript"></script>
    <script src="<%=OAppPath %>/JS/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="../../Plugin/FancyBoxNew/lib/jquery.mousewheel-3.0.6.pack.js" type="text/javascript"></script>
    <script src="../../Plugin/FancyBoxNew/source/jquery.fancybox.pack.js" type="text/javascript"></script>
    <script src="../../Plugin/FancyBoxNew/source/jquery.fancybox.js" type="text/javascript"></script>
    <link href="../../Plugin/FancyBoxNew/source/jquery.fancybox.css" rel="stylesheet"
        type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
    <script type="text/javascript">
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

        function validateAttachment() {
            var count = document.getElementById('<%=hdnValidation.ClientID %>').value;
            for (i = 0; i <= count; i++) {
                if ($("#file" + i).val() != "") {
                    var strPhoto = $("#file" + i).val();
                    var ext = strPhoto.substring(strPhoto.lastIndexOf('.') + 1);
                    if (ext == "jpg" || ext == "JPG" || ext == "jpeg" || ext == "JPEG" || ext == "png" || ext == "PNG" || ext == "gif" || ext == "GIF" || ext == "ico" || ext == "bmp" || ext == "tiff" || ext == "tif" || ext == "bob") {
                        $("#spn" + i).css({ 'display': 'none' });
                    } else {
                        $("#file" + i).val('');
                        $("#spn" + i).html("* Upload only jpg/jpeg/png/gif/bmp/tif/ico/bob images.").css({ 'display': 'inline', 'color': 'red' });
                    }
                }
            }
        }


    </script>
    <script type="text/javascript">        //validation summary for all fileds

        $(document).ready(function () {
            $("[id$=btnPost]").click(function () {

                var data = new nicEditors.findEditor('<%=txtDetail.ClientID %>'); // bee is the ID of the textarea
                var didIT = data.getContent(); // return the content

                if (didIT.trim() == "<br>") {

                    $("[id$=spnDetails]").html("Detail is required.").css("display", "inline").css("color", "Red");
                    // strMsg = strMsg + "\n" + "* Detail is required. ";
                    alert("* Message is required");

                    return false;
                }

            });
        });

    </script>
    <script type="text/javascript">

        function funCommentedit(commnicationId) {


            popup('<%=OAppPath %>/Module/BugTracker/ViewPopup.aspx?CommnicationID=' + commnicationId);

        }

    </script>
    <script type="text/javascript">
        function funCommentdelete(commnicationId) {

            var status = confirm("Are you sure, you want to delete?");
            if (status) {
                $.ajax({
                    type: "POST",
                    url: "<%=OAppPath %>/Ajax.aspx?Type=AttachmentandComment",
                    data: { CommnicationID: commnicationId },
                    success: function (data, status, xhr) {
                        window.location.href = window.location;
                    }
                });
            }

        }
        
    </script>
    <script type="text/javascript">

        function Assignedissue() {

            var reportissueid = document.getElementById('<%=hdnreportissueid.ClientID %>').value;
            var Assignedtouserid = document.getElementById('<%=hdnassigndetouserid.ClientID %>').value;

            popup("<%=OAppPath %>/Module/BugTracker/AssignIssue.aspx?Reportissueid=" + reportissueid + "&Assignedtouserid=" + Assignedtouserid + " ");
        }
    </script>

    <script type="text/javascript">
        function ResolvedIssue() {
            var reportissueid = document.getElementById('<%=hdnreportissueid.ClientID %>').value;
           
          
            popup("<%=OAppPath %>/Module/BugTracker/ResolveIssue.aspx?Reportissueid=" + reportissueid + "");
          
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
    <div style="width: 98%; margin: 1%;">
        <br />
        <div align="left">
            <table>
                
                <tr>
                    
                    <td><img src='<%=OAppPath %>/Images/Bug.png'  /></td>
                    <td valign="middle">  <h4>
                <%=ProjectName%>/<%=IssueNo%>
            </h4>
            <h2>
                <%=Summary%></h2></td>
                </tr>
            </table>
          
        </div>
        <table>
            <tr>
                <td>
                    <asp:Button runat="server" Text="Edit" Width="100px" OnClick="Edit_Click" />
                    <span style="margin-left:10px;"></span>
                    <input id="btnassign" type="button" width="100px" value="Assign" onclick="Assignedissue()" />
                    <span style="margin-left:10px;"></span>
                    <input id="btnresolve" runat="server" width="100px" type="button" value="Resolved"
                        onclick="ResolvedIssue()" />
                    <span style="margin-left:10px;"></span>
                    <asp:Button ID="btnreopen" runat="server" Width="100px" Visible="False" Text="Reopen Issue"
                        OnClick="Reopen_Click" />
                    <span style="margin-left:10px;"></span>
                    <asp:Button ID="btncloseissue" Width="100px" runat="server" Visible="False" Text="Close Issue"
                        OnClick="Close_Click" />
                </td>
            </tr>
        </table>  <hr />
        <table width="80%">
           
            <tr>
                <td >
                    Prirority:-
                    <%=Priority%>
                </td>
                <td>
                    Assignedto:-
                    <%=Assigned%>
                </td>
                <td>
                    Due date:-
                    <%=Duedate%>
                </td>
            </tr>
            <tr>
                <td>
                    Status:-
                    <%=Status%>
                </td>
                <td>
                    Reported by:-
                    <%=Reportedby%>
                </td>
                <td>
                    Created On:-
                    <%=CreatedOn%>
                </td>
            </tr>
        </table>
        <br />
        <div id="divdescription" runat="server"  >
            <table style="width:1300px">
                <tr><td><b>Description </b> <hr/><%=Description%></td></tr>
              <tr><td><%=Attachmentsstring%></td></tr>
            </table>
        </div>
        <div id="divcmnt" runat="server" style="width: 98%;">
            <%=UserMsg%>
        </div>
        <table style="width: 98%">
            <tr>
                <td>
                    Comments:
                </td>
            </tr>
            <tr>
                  <td>
                      <script type="text/javascript" src="<%=OAppPath %>/Plugin/Editor/nicEdit-latest.js"></script>

                    <script type="text/javascript">
                        bkLib.onDomLoaded(function () { nicEditors.allTextAreas() });
                    </script>
                    <asp:TextBox runat="server" ID="txtDetail" TextMode="MultiLine" style="height:150px; width:95%;"></asp:TextBox>
                   <%-- <textarea ID="txtDetail" runat="server" class="textArea" 
                    style="height:150px; width:95%;" name="Detail"></textarea>--%>
                 
                    <span id="spnDetails" runat="server"></span>
                </td>
                
            </tr>
            <tr>
                <td align="left">
                    Upload Files:
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
                    <asp:Button ID="btnPost" Text="Post" runat="server" OnClick="btnPost_Click" />
                </td>
            </tr>
            <asp:HiddenField ID="hdnValidation" runat="server" Value="0" />
            <%-- <asp:HiddenField ID="hdnissueno" runat="server" />--%>
            <%-- <asp:HiddenField ID="hdnreportissueid" runat="server" />--%>
            <asp:HiddenField runat="server" ID="hdnassigndetouserid" />
            <asp:Literal ID="ltpageload" runat="server"></asp:Literal>
            <asp:Literal runat="server" ID="ltassign"></asp:Literal>
            <asp:Literal runat="server" ID="ltresolve"></asp:Literal>
            <asp:Literal runat="server" ID="ltparentreload"></asp:Literal>
        </table>
    </div>
    <asp:HiddenField runat="server" ID="hdnissueno" />
    <asp:HiddenField runat="server" ID="hdnreportissueid" />
</asp:Content>
