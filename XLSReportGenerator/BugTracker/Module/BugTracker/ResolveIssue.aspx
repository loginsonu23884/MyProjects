<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResolveIssue.aspx.cs" Inherits="Module_BugTracker_ResolveIssue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="<%=OAppPath %>/Plugin/Editor/nicEdit-latest.js"></script>
    <script type="text/javascript">
        bkLib.onDomLoaded(function () { nicEditors.allTextAreas() });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$=btnresolveforpopup]").click(function () {
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
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div>
        <table width="88%">
            <tr>
                <td>
                    Comments:
                </td>
            </tr>
            <tr>
                <td align="left">
                    <textarea id="txtDetail" runat="server" class="textArea" style="height: 150px; width: 99%;"
                        name="Detail"></textarea>
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
                <td align="left">
                    <asp:Button ID="btnresolveforpopup" Text="Resolve" runat="server" Style="height: 26px" OnClick="btnresolve_Click" />
                    <asp:Button ID="btncancel" Text="Cancel" runat="server" Style="height: 26px" OnClick="btncancel_Click" />
                </td>
            </tr>
        </table>
        <asp:HiddenField runat="server" ID="hdnValidation" />
        <asp:Literal runat="server" ID="ltfancycloseparentupload"></asp:Literal>
     
    </div>
    </form>
</body>
</html>
