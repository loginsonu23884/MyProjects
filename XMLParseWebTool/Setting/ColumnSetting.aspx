<%@ Page Title="" Language="C#" MasterPageFile="~/POPPage.master" AutoEventWireup="true"
    CodeFile="ColumnSetting.aspx.cs" Inherits="Setting_ColumnSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">



<div style="margin:1%;margin-bottom:4%;border:1px solid #000;">
        <h2 align="center">
            Column Setting
        </h2>
    <hr />
            <table cellpadding="2" >
          <tr>
          <td colspan="2" align="left"> <asp:Label ForeColor="Red" Font-Bold="true" runat="server" Visible="false" ID="lblMessage"></asp:Label></td>
          </tr>
                <tr>
                    <td>
                        Table
                    </td>
                    <td>
                    <%=strTableName %>
                     
                    </td>
                </tr>
                <tr>
                    <td>
                        Column
                    </td>
                    <td>
                    <%=strColumnName %>
            
                    </td>
                </tr>
           
                <tr>
                    <td>
                        text for cell header
                    </td>
                    <td>
                        <asp:TextBox ID="txtStextforcellheader" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Font-Bold="true" runat="server" ID="RequiredFieldValidator2"
                            ForeColor="Red" ControlToValidate="txtStextforcellheader" ErrorMessage="*">*</asp:RequiredFieldValidator>
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
                        Cell Number Format
                    </td>
                    <td>
                        <asp:TextBox ID="txtcellNumberFormat" runat="server" MaxLength="25"></asp:TextBox>
                       <!-- <asp:RequiredFieldValidator Font-Bold="true" runat="server" ID="RequiredFieldValidator11"
                            ForeColor="Red" ControlToValidate="txtcellNumberFormat" ErrorMessage="*">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator Font-Bold="true" ForeColor="Red" runat="server" ID="RegularExpressionValidator5"
                            ValidationExpression="^([0-9]*\s?[0-9]*)+$" ErrorMessage="Invalid!" ControlToValidate="txtcellNumberFormat">Invalid!</asp:RegularExpressionValidator>-->
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
                        Is Visible
                    </td>
                    <td>
                        <asp:CheckBox ID="chkIsVisible" runat="server"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Is Cell Italic
                    </td>
                    <td>
                        <asp:CheckBox ID="chkIsCellItalic" runat="server"></asp:CheckBox>
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
                        Cell Warping
                    </td>
                    <td>
                     <asp:CheckBox ID="chkCellWarping" runat="server"></asp:CheckBox>
                    

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
