<%@ Page Title="" Language="C#" MasterPageFile="~/POPPage.master" AutoEventWireup="true" CodeFile="DoubleHeaderSetting.aspx.cs" Inherits="Setting_DoubleHeaderSetting" %>

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
                       Header Text:
                    </td>
                    <td>
                        
                      <asp:TextBox ID="txtHeaderText" MaxLength="50" runat="server"></asp:TextBox>
                       
                    </td>
                </tr>
                  <tr>
                    <td>
                        Cell Merge
                    </td>
                    <td>
                     <asp:CheckBox ID="chkCellMerge" runat="server"></asp:CheckBox>
                      

                    </td>
                </tr>
                <tr>
                    <td>
                        Cell Merge Length
                    </td>
                    <td>
                        <asp:TextBox ID="txtCellMergeLength" runat="server"  MaxLength="2"></asp:TextBox>
                        <asp:RequiredFieldValidator Font-Bold="true" runat="server" ID="RequiredFieldValidator14"
                            ForeColor="Red" ControlToValidate="txtCellMergeLength" ErrorMessage="*">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator Font-Bold="true" ForeColor="Red" runat="server" ID="RegularExpressionValidator2"
                            ValidationExpression="^([0-9]*\s?[0-9]*)+$" ErrorMessage="Invalid!" ControlToValidate="txtCellMergeLength">Invalid!</asp:RegularExpressionValidator>
                    </td>
                </tr>
                   <tr>
                    <td>
                        Is Cell Format
                    </td>
                    <td>
                        <asp:CheckBox ID="chkIsCellFormat" runat="server"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td>
                      AlterNative Row Color:
                    </td>
                    <td>
                        
                      <asp:TextBox ID="txtAlterNativeRowColor" MaxLength="50" runat="server"></asp:TextBox>
                       
                    </td>
                </tr>
                  <tr>
                    <td>
                        Is Cell Bold
                    </td>
                    <td>
                        <asp:CheckBox ID="chkIsCellBold" runat="server"></asp:CheckBox>
                    </td>
                </tr> 
                <tr>
                    <td>
                        Font Size
                    </td>
                    <td>
                        <asp:TextBox ID="txtFontSize" MaxLength="2" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Font-Bold="true" runat="server" ID="RequiredFieldValidator3"
                            ForeColor="Red" ControlToValidate="txtFontSize" ErrorMessage="*">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator Font-Bold="true" ForeColor="Red" runat="server" ID="RegularExpressionValidator6"
                            ValidationExpression="^([0-9]*\s?[0-9]*)+$" ErrorMessage="Invalid!" ControlToValidate="txtFontSize">Invalid!</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Font Style
                    </td>
                    <td>
                     <asp:DropDownList runat="server" ID="ddlFontStyle">
                        
                        </asp:DropDownList>

                        <asp:RequiredFieldValidator Font-Bold="true" runat="server" ID="RequiredFieldValidator1" InitialValue='0'
                            ForeColor="Red" ControlToValidate="ddlFontStyle" ErrorMessage="*">*</asp:RequiredFieldValidator>
                       
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
                        Text Alignment
                    </td>
                    <td>
                      <asp:DropDownList runat="server" ID="ddlTextAlignment">
                        
                        </asp:DropDownList>

                        <asp:RequiredFieldValidator Font-Bold="true" runat="server" ID="RequiredFieldValidator4" InitialValue='0'
                            ForeColor="Red" ControlToValidate="ddlTextAlignment" ErrorMessage="*">*</asp:RequiredFieldValidator>
                     
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

