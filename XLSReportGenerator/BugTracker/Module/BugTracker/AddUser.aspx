<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/PopUpMaster.master" AutoEventWireup="true" CodeFile="AddUser.aspx.cs" Inherits="Module_BugTracker_AddUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">

//    $(document).ready(function () {
//        $("[id$=btnAdd]").click(function () {
//            var flag = 0;
//            var isChecked = document.getElementById('<%= chkisNotification.ClientID %>');    //enter u r checkbox id
//            if (isChecked.checked == true) {
//                var e = document.getElementById('<%=ddlemail.ClientID %>');
//                var userId = e.options[e.selectedIndex].value;
//                if (userId == "0") {             
//                    flag = 1;
//                    $("#errorReason").html("email is required");
//                    $("#errorReason").show();
//                } else {
//                    $("#errorReason").hide();
//                }
//            }
//            if (flag) {
//                return false;
//            } else {
//                return true;
//            }
//        });
//    });
//  


    function forRentClicked() {
        var tb1 = document.getElementById('trDetails');
        var chkName = document.getElementById('<%= chkisNotification.ClientID %>');
        if (chkName.checked) {
            $(tb1).show();
        }
        else {
            $(tb1).hide();
        }
    }



    /*Function for deleting the Nature Of Interest item*/
    function DeleteAssignDeveloper(rowNumber) {        
        if (confirm("Are you sure, you want to delete the record?")) {
            $("#gvAssignDeveloper").html("");
            $.ajax({
                type: 'Get',
                url: "<%=oAppPath%>/Ajax.aspx?Method=DeleteAssignDeveloper",
                data: { Type: 'Json', rowNumber: rowNumber },
                dataType: 'json',
                success: function (data, status) {                
                    var Table = '<table cellpadding="4"  class="table table-striped" cellspacing="4" style="width:100%;"><tr><td><b>User</b></td> <td><b>Notification</b></td><td><b>Email</b></td><td><b>Delete</b></td></tr>', Tr = '';
                    for (var i = 0; i < data.length; i++) {

                       
                        Tr = Tr + "<tr><td>" + data[i].User + "</td> <td>" + data[i].Notifications + "</td><td>" + data[i].Email + "</td> <td> <img src='<%=oAppPath%>/Images/Delete.jpg' style='cursor:pointer;' onclick='javascript:DeleteAssignDeveloper(" + i + ")'/> </td></tr>";
                    }
                    Table = Table + Tr + "</table>";
                    $("#gvAssignDeveloper").html(Table);

                },
                error: function (xhr, status, error) {

                }
            });
        }
    }


    /*function to load the existing list*/

    function loadMailNotification() {
        $("#gvAssignDeveloper").html("");
        try {
            $.ajax({
                type: 'Get',
                url: "<%=oAppPath%>/Ajax.aspx?Method=loadMailNotification",
                data: { Type: 'Json' },
                dataType: 'json',
                success: function (data, status) {
                    if (data != null) {
                        var Table = '<table cellpadding="4"  class="table table-striped" cellspacing="4" style="width:100%;"><tr><td><b>User</b></td> <td><b>Notification</b></td><td><b>Email</b></td> <td><b>Delete</b></td></tr>', Tr = '';
                        for (var i = 0; i < data.length; i++) {

                            Tr = Tr + "<tr><td>" + data[i].User + "</td> <td>" + data[i].Notifications + "</td> <td>" + data[i].Email + "</td><td> <img src='<%=oAppPath%>/Images/Delete.jpg' onclick='javascript:DeleteAssignDeveloper(" + i + ")'/> </td></tr>";
                        }
                        Table = Table + Tr + "</table>";
                        $("#gvAssignDeveloper").html(Table);
                    }                    
                    document.getElementById('<%=ddlassignedto.ClientID %>').value = '0';
                    document.getElementById('<%=chkisNotification.ClientID %>').checked = false;
                    document.getElementById('<%=ddlemail.ClientID %>').value = '0';
                },
                error: function (xhr, status, error) {

                }
            });            
        }
        catch (e) {
            alert(e);
        }
    }
    /*Function for adding the name and Nature Of Interest details*/
    function addAssignDeveloper() {

        var e = document.getElementById('<%=ddlassignedto.ClientID %>');
        var userName = e.options[e.selectedIndex].text;
        var userId = e.options[e.selectedIndex].value;
        var chkName = document.getElementById('<%= chkisNotification.ClientID %>');
        var chkvalue = chkName.checked;
        var emailId = document.getElementById('<%=ddlemail.ClientID %>');
        var emailName = emailId.options[emailId.selectedIndex].text;


        $("#gvAssignDeveloper").html("");
        try {
            $.ajax({
                type: 'Get',
                url: "<%=oAppPath%>/Ajax.aspx?Method=addAssignDeveloper",
                data: { Type: 'Json', userId: userId, userName: userName, chkvalue: chkvalue, emailName: emailName },
                dataType: 'json',
                success: function (data, status) {
                    var Table = '<table cellpadding="4"  class="table table-striped" cellspacing="4" style="width:100%;"><tr><td><b>User</b></td> <td><b>Notification</b></td><td><b>Email</b></td> <td><b>Delete</b></td></tr>', Tr = '';
                    for (var i = 0; i < data.length; i++) {

                        Tr = Tr + "<tr><td>" + data[i].User + "</td> <td>" + data[i].Notifications + "</td> <td>" + data[i].Email + "</td><td> <img src='<%=oAppPath%>/Images/Delete.jpg' onclick='javascript:DeleteAssignDeveloper(" + i + ")'/> </td></tr>";
                    }
                    Table = Table + Tr + "</table>";
                    $("#gvAssignDeveloper").html(Table);
                    document.getElementById('<%=ddlassignedto.ClientID %>').value = '0';
                    document.getElementById('<%=chkisNotification.ClientID %>').checked = false;
                    document.getElementById('<%=ddlemail.ClientID %>').value = '0';
                },
                error: function (xhr, status, error) {
                }
            });
        }
        catch (e) {
            alert(e);
        }

    } 
     
 function pageNatureOfInterestAdd() {               
               
               var StatusReturn = false;
                if (Page_ClientValidate('NatureOfInterestItem'))
                {
                StatusReturn = true;  
                addAssignDeveloper();
               
               }            
               return StatusReturn;
           }
           </script>
<fieldset style="height:200px;"><legend style="font-weight: bold">Assign Developer</legend>  
<table cellspacing="2" cellpadding="5" width="100%">
            <tr>
            <td>User
             <asp:DropDownList ID="ddlassignedto" runat="server" initialvalue="0" DataTextField="username"
                                    DataValueField="id">
              </asp:DropDownList>              
               <asp:RequiredFieldValidator ID="RFVddlAssignedTo" InitialValue="0" runat="server" ControlToValidate="ddlassignedto" ValidationGroup="NatureOfInterestItem"
                ErrorMessage="Required"></asp:RequiredFieldValidator>                           
                </td>
                <td> 
                Notification
                <asp:CheckBox ID="chkisNotification" runat="server" onClick="javascript:forRentClicked();"/>
                </td>
                <td id="trDetails">
                Email
                <asp:DropDownList ID="ddlemail" runat="server"  ToolTip="Email">
                        <%--<asp:ListItem Selected="True" Value="0" Text="Select One"></asp:ListItem>--%>
                         <asp:ListItem Selected="True" Value="1" Text="To"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Cc"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Bcc"></asp:ListItem>
                    </asp:DropDownList>
                <span id="errorReason" style="color:Red"></span>
              <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="0" runat="server" ControlToValidate="ddlemail" ValidationGroup="NatureOfInterestItem"
                                    ErrorMessage="Required"></asp:RequiredFieldValidator>--%>
                </td>
                <td>
                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClientClick="javascript:pageNatureOfInterestAdd();return false;" 
                    Font-Bold="true" ToolTip="Add" ValidationGroup="NatureOfInterestItem" />
                </td>
             </tr>
             <tr>
                        <td>
                        <div id="gvAssignDeveloper">
                        </div>
                        </td>
                    </tr>
 </table>
</fieldset>
<script type="text/javascript">
    loadMailNotification();
</script>
<script type="text/javascript">
    forRentClicked();
    </script>
</asp:Content>

