<%@ Page Title="" Language="C#" MasterPageFile="~/POPPage.master" AutoEventWireup="true" CodeFile="ManageRule.aspx.cs" Inherits="Setting_ManageRule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<div style="margin:1%;margin-bottom:4%;border:1px solid #000;">
        <h2 align="center">
            Manage Rule
        </h2>
    <hr />
            <table cellpadding="2" >
          <tr>
          <td colspan="2" align="left"> <asp:Label ForeColor="Red" Font-Bold="true" runat="server" Visible="false" ID="lblMessage"></asp:Label></td>
          </tr>
                <tr>
                    <td>
                        Table Name:
                    </td>
                    <td>
                   <b><%=strTableName %></b> 
                     
                    </td>
                </tr>
                <tr>
                    <td>
                        Column Name:
                    </td>
                    <td>
                     <b><%=strColumnName %></b> 
            
                    </td>
                </tr>
           
                <tr>
                    <td>
                        Type:
                    </td>
                    <td>
                        
                        <asp:DropDownList runat="server" ID="ddlType">
                        
                        </asp:DropDownList>

                        <asp:RequiredFieldValidator Font-Bold="true" runat="server" ID="RequiredFieldValidator2" InitialValue='0'
                            ForeColor="Red" ControlToValidate="ddlType" ErrorMessage="*">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Condition:
                    </td>
                    <td>
                         <asp:DropDownList runat="server" ID="ddlCondition">
                        
                        </asp:DropDownList>

                        <asp:RequiredFieldValidator Font-Bold="true" runat="server" ID="RequiredFieldValidator1" InitialValue='0'
                            ForeColor="Red" ControlToValidate="ddlCondition" ErrorMessage="*">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                       Value:
                    </td>
                    <td>
                        <asp:TextBox ID="txtValue" MaxLength="50" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Font-Bold="true" runat="server" ID="RequiredFieldValidator3"
                            ForeColor="Red" ControlToValidate="txtValue" ErrorMessage="*">*</asp:RequiredFieldValidator>
                        

                    </td>
                </tr>
                <tr>
                    <td>
                        BG Color:
                    </td>
                    <td>
                    <asp:DropDownList runat="server" ID="ddlBGColor">
                        
                        </asp:DropDownList>
                     
                    </td>
                </tr>
                <tr>
                    <td>
                        Fore Color:
                    </td>
                    <td>
                      <asp:DropDownList runat="server" ID="ddlForeColor">
                        
                        </asp:DropDownList>
                      
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <br />
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <br />
                    </td>
                </tr>
            </table>
        
    </div>
</asp:Content>

