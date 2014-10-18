<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssignIssue.aspx.cs" Inherits="Module_BugTracker_AssignIssue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

      <script type="text/javascript" src="<%=oAppPath %>/Plugin/Editor/nicEdit-latest.js"></script>

    <script type="text/javascript">
        bkLib.onDomLoaded(function () { nicEditors.allTextAreas() });
    </script>
    

     <script type="text/javascript">         //validation summary for all fileds

         $(document).ready(function () {
             $("[id$=btnPost]").click(function () {
                 var data = new nicEditors.findEditor('<%=txtDetail.ClientID %>'); // bee is the ID of the textarea
                 didIT = data.getContent(); // return the content
                 if (didIT.trim() == "<br>") {
                 $("[id$=spnDetails]").html("Detail is required.").css("display", "inline").css("color", "Red");
                     // strMsg = strMsg + "\n" + "* Detail is required. ";
                     alert("* Comment is required");
                     return false;
                 }

             });
         });

    </script>
     
   
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div>
        <table>
            <tr>
     <td align="left" valign="top">
                                Assigned to:
                                <asp:DropDownList ID="ddlassignedto" runat="server" Height="29px" Width="151px">
                                </asp:DropDownList>
           </td>     
            
                            </tr>
                             <tr>
            <td >
                Comments:
            </td>
        </tr>
        <tr>
            <td align="left">
                <textarea id="txtDetail" runat="server" class="textArea" 
                    style="height:164px; width:293%;" name="Detail"></textarea>
                  <span id="spnDetails" runat="server"></span>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Button ID="btnAssign" Text="Assign" runat="server" 
                    style="height: 26px" onclick="btnAssign_Click" />
                <asp:Button ID="Button1" Text="Cancel" runat="server"  
                    style="height: 26px" onclick="Cancel_Click" />
            </td>
            </tr>
        <asp:Literal runat="server" ID="ltfancycloseparentupload"></asp:Literal>
        <asp:HiddenField ID="hdnValidation" runat="server" Value="0" />
                            </table>
    </div>
    </form>
</body>
</html>
