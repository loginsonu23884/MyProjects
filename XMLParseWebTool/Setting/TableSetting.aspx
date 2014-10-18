<%@ Page Title="" Language="C#" MasterPageFile="~/POPPage.master" AutoEventWireup="true"
    CodeFile="TableSetting.aspx.cs" Inherits="Setting_TableSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">


    <div style="margin:2%;margin-bottom:4%;border:1px solid #000;">
        <h2 align="center">
            Table Setting
        </h2>
      <hr />
            <table cellpadding="4" >
          <tr>
          <td colspan="2" align="left"> <asp:Label ForeColor="Red" Font-Bold="true" runat="server" Visible="false" ID="lblMessage"></asp:Label></td>
          </tr>
                <tr>
                    <td>
                        Table Name:

                    </td>
                    <td>
                   <b><%=strTableName%></b> 
                     
                    </td>
                </tr>
                <tr>
                    <td>
                        Starting Position Column:
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartingPosition" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Font-Bold="true" runat="server" ID="RequiredFieldValidator5"
                            ForeColor="Red" ControlToValidate="txtStartingPosition" ErrorMessage="*">*</asp:RequiredFieldValidator>
                      
                    </td>
                </tr>
                <tr>
                    <td>
                        Starting Position Row:
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartingPositionRow" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Font-Bold="true" runat="server" ID="RequiredFieldValidator1"
                            ForeColor="Red" ControlToValidate="txtStartingPositionRow" ErrorMessage="*">*</asp:RequiredFieldValidator>
                              <asp:RegularExpressionValidator Font-Bold="true" ForeColor="Red" runat="server" ID="rev"
                            ValidationExpression="^([0-9]*\s?[0-9]*)+$" ErrorMessage="Invalid!" ControlToValidate="txtStartingPositionRow">Invalid!</asp:RegularExpressionValidator>
                    </td>
                </tr>
               
               
                <tr>
                    <td>
                        Alter Native Row Color:
                    </td>
                    <td>
                        <asp:TextBox ID="txtAlterNativeRowColor" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Font-Bold="true" runat="server" ID="RequiredFieldValidator2"
                            ForeColor="Red" ControlToValidate="txtAlterNativeRowColor" ErrorMessage="*">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                 <tr>
                    <td>
                        Is Cell Format:
                    </td>
                    <td>
                        <asp:CheckBox ID="chkIsCellFormat" runat="server"></asp:CheckBox>
                    </td>
                </tr>
               
                <tr>
                    <td>
                        Is Cell Bold:
                    </td>
                    <td>
                        <asp:CheckBox ID="chkIsCellBold" runat="server"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Font Size:
                    </td>
                    <td>
                        <asp:TextBox ID="txtFontSize" runat="server"  MaxLength="2"></asp:TextBox>
                        <asp:RequiredFieldValidator Font-Bold="true" runat="server" ID="RequiredFieldValidator14"
                            ForeColor="Red" ControlToValidate="txtFontSize" ErrorMessage="*">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator Font-Bold="true" ForeColor="Red" runat="server" ID="RegularExpressionValidator2"
                            ValidationExpression="^([0-9]*\s?[0-9]*)+$" ErrorMessage="Invalid!" ControlToValidate="txtFontSize">Invalid!</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Font Style:
                    </td>
                    <td>
                      <asp:DropDownList runat="server" ID="ddlFontStyle">
                        
                        </asp:DropDownList>

                       
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
                        Text Alignment:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTextAlignment" runat="server">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="Left">Left</asp:ListItem>
                            <asp:ListItem Value="Right">Right</asp:ListItem>
                            <asp:ListItem Value="Center">Center</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator InitialValue="0" Font-Bold="true" runat="server" ID="RequiredFieldValidator8"
                            ForeColor="Red" ControlToValidate="ddlTextAlignment" ErrorMessage="*">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Border Size:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBorderSize" runat="server">
                            <asp:ListItem Value="0" Selected="True">0</asp:ListItem>
                            <asp:ListItem Value="1">1</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                  <tr>
                    <td>
                        Is Doble Header:
                    </td>
                    <td>
                        <asp:CheckBox ID="chkIsDobleHeader" runat="server"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Is Row Style:
                    </td>
                    <td>
                        <asp:CheckBox ID="chkIsRowStyle" runat="server"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Conditional Style:
                    </td>
                    <td>
                        <asp:CheckBox ID="chkConditionalStyle" runat="server"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Is Last Row Colored:
                    </td>
                    <td>
                        <asp:CheckBox ID="chkIsLastRowColored" runat="server"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                       
                        <asp:Button ID="btnSave" runat="server" Text="Save"  onclick="btnSave_Click" />
                       
                    </td>
                </tr>
            </table>
       
    </div>
   
</asp:Content>
