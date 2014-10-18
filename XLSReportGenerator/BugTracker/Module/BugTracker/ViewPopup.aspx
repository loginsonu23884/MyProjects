<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPopup.aspx.cs" Inherits="Module_BugTracker_VeiwPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="<%=oAppPath %>/JS/jquery-1.5.1.min.js" type="text/javascript"></script>
    <script src="<%=oAppPath %>/JS/grid.locale-en.js" type="text/javascript"></script>
    <script src="<%=oAppPath %>/JS/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript" src="<%=oAppPath%>/Js/jquery-1.7.1.min.js"></script>
    <%-- Grey Box Start Script--%>
    <%--   <script type="text/javascript" src="../../Plugin/Editor/nicEdit-latest.js"></script> 
                 <%--  <script src="../../Plugin/Editor/nicEditIcons-latest.gif" type="text/javascript"></script>--%>
    <%--    <script type="text/javascript">
                  bkLib.onDomLoaded(function () { nicEditors.allTextAreas() });
              </script>--%>
    <script type="text/javascript">

        //** This code for create dynamicaly file uploader & textbox's
        var counter = 1;
        //** this function create 1'st Div & Append many Div's when attach more files
        function AddFileUpload() {

            var div = $(document.createElement('div')).attr("id", 'FileUploadContainer' + counter);
            div.html('<input id="txt' + counter + '" name = "textbox' + counter + '" type="text"/>   <input id="file' + counter + '" onchange="validateAttachment();" name = "file' + counter + '" type="file"  />  <span id="spn' + counter + '" style="display: none;font-size: x-small;"></span>');
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

        function funattachmentdelete(result1, IssueNo, ReportIssueid) {

            $.ajax({
                type: 'post',
                url: '<%=oAppPath %>/Ajax.aspx?Type=Attachment&ID=' + result1,
                success: function (getValue) {
//                    parent.location.reload(true);
                    parent.$.fancybox.close(); 
                    parent.window.location.href = parent.window.location.href;
                    window.location.href = "<%=oAppPath %>/Module/BugTracker/Communication.aspx?IssueNo=" + IssueNo + "&ReportIssueid=" + ReportIssueid;
                }
            });
            //        window.location.href ="<%=oAppPath %>/Ajax.aspx?Type=Attachment&ID=" + result1;
            ////     
            //        parent.$.fancybox.close(); parent.location.reload(true);
            //       window.location.href ="<%=oAppPath %>/Module/BugTracker/Communication.aspx?IssueNo=" + IssueNo + "&ReportIssueid=" + ReportIssueid;
        }
    </script>
    <script type="text/javascript">


        $(document).ready(function () {
            $("[id$=btnsave]").click(function () {

                var data = new nicEditors.findEditor('<%=txtDetail.ClientID %>'); // bee is the ID of the textarea
                didIT = data.getContent(); // return the content

                if (didIT.trim() == "<br>") {

                    $("[id$=spnDetails]").html("Detail is required.").css("display", "inline").css("color", "Red");
                    // strMsg = strMsg + "\n" + "* Detail is required. ";
                    alert("* Message is required");

                    return false;
                }

            });
        });
    </script>
    <%--<script type="text/javascript">

   function funattachmentedit(result,id)
    {
      
        if (id == 'edit_0') 
        {
           
        
            document.getElementById('divattachment1').style.display = 'block';
        }
        if (id == 'edit_1') 
        {
          
            
            document.getElementById('divattachment2').style.display = 'block';
        }
        if (id == 'edit_2')
       {
           
           document.getElementById('divattachment3').style.dispaly = 'block';
       }
     // document.getElementById(div3.
    }
</script>--%>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div>
        <table style="width: 100%">
            <tr>
                <td align="right">
                    Comments
                </td>
                <td>
                    <script type="text/javascript" src="<%=oAppPath %>/Plugin/Editor/nicEdit-latest.js"></script>
                    <script type="text/javascript">
                        bkLib.onDomLoaded(function () { nicEditors.allTextAreas() });
                    </script>
                    <textarea id="txtDetail" runat="server" class="textArea" 
                    style="height:150px; width:95%;" name="Detail"></textarea>
                 
                    <span id="spnDetails" runat="server"></span>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Attachments
                </td>
                <td>
                    <%=userMsg%>
                </td>
            </tr>
            <tr>
                <td align="right">
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
                        <img alt="" id="img1" height="20px" width="20px" src="../../Stylesheet/images/add.png"
                            tabindex="7" title="Add attachment" /></a> <a id="hlk2 '" onclick="RemoveFileUpload(this)">
                                <img id="img2" height="20px" width="20px" src="../../Stylesheet/images/delete.png"
                                    tabindex="7" title="Remove attachment" /></a> Add/Remove attachment
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnsave" Text="Save" runat="server" OnClick="btnPost_Click" Style="height: 26px" />
                </td>
            </tr>
            <asp:HiddenField ID="hdnValidation" runat="server" Value="0" />
            <asp:Literal ID="ltfancycloseparentupload" runat="server"></asp:Literal>
        </table>
    </div>
    </form>
</body>
</html>
