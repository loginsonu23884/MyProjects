<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/BT_MasterPage.master" AutoEventWireup="true" CodeFile="ProjectConfigCreation.aspx.cs" Inherits="Module_ProjectConfigCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    jQuery(document).ready(function () {        
        setTimeout(function () { $("#<%=lblMessage.ClientID%>").hide(); }, 5000);
    });
    </script>
 <br/>     
     <fieldset style="width: 95%; margin-left: 20px; margin-right: 20px;">
         <legend style="font-weight: bold">Configured Project</legend>         
         <div align="center">
        <asp:Label Text="" Visible="false" Font-Bold="true" ForeColor="Red" ID="lblMessage"
            runat="server"></asp:Label>
    </div>            
    <div>
     <fieldset><legend>Project Name</legend>
                  <asp:CheckBoxList runat="server" CellSpacing="2" ID="chkProjectName" RepeatDirection="Horizontal" RepeatColumns="5"
                        TextAlign="Right">
                      </asp:CheckBoxList>
      </fieldset>     
        <br />
      <asp:Button ID="btnSave" runat="server" Text="Save" 
                    Font-Bold="true" ToolTip="Save" onclick="btnSave_Click"
             />
    </div>
 
    </fieldset>






</asp:Content>

