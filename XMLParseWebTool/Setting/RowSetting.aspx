<%@ Page Title="" Language="C#" MasterPageFile="~/POPPage.master" AutoEventWireup="true"
    CodeFile="RowSetting.aspx.cs" Inherits="Setting_RowSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <h2>
            Row Setting
        </h2>
        <p>
            <table>
                <tr>
                    <td>
                        Table
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTablename" runat="server">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="1">Summary</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator InitialValue="0" Font-Bold="true" runat="server" ID="RequiredFieldValidator9"
                            ForeColor="Red" ControlToValidate="ddlTablename" ErrorMessage="*">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        CellId
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCelld" runat="server">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="1">Name</asp:ListItem>
                            <asp:ListItem Value="1">Program</asp:ListItem>
                            <asp:ListItem Value="1">Buy</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator InitialValue="0" Font-Bold="true" runat="server" ID="RequiredFieldValidator1"
                            ForeColor="Red" ControlToValidate="ddlCelld" ErrorMessage="*">*</asp:RequiredFieldValidator>
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
                        Is Cell Bold
                    </td>
                    <td>
                        <asp:CheckBox ID="IsCellBold" runat="server"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Font Size
                    </td>
                    <td>
                        <asp:TextBox ID="txtFontSize" runat="server"></asp:TextBox>
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
                        <asp:TextBox ID="txtFontStyle" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Font-Bold="true" runat="server" ID="RequiredFieldValidator4"
                            ForeColor="Red" ControlToValidate="txtFontStyle" ErrorMessage="*">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        BG Color
                    </td>
                    <td>
                        <asp:TextBox ID="txtBGColor" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Font-Bold="true" runat="server" ID="RequiredFieldValidator6"
                            ForeColor="Red" ControlToValidate="txtBGColor" ErrorMessage="*">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Fore Color
                    </td>
                    <td>
                        <asp:TextBox ID="txtForeColor" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Font-Bold="true" runat="server" ID="RequiredFieldValidator7"
                            ForeColor="Red" ControlToValidate="txtForeColor" ErrorMessage="*">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Border Size
                    </td>
                    <td>
                        <asp:TextBox ID="txtBorderSize" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Font-Bold="true" runat="server" ID="RequiredFieldValidator10"
                            ForeColor="Red" ControlToValidate="txtBorderSize" ErrorMessage="*">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator Font-Bold="true" ForeColor="Red" runat="server" ID="RegularExpressionValidator1"
                            ValidationExpression="^([0-9]*\s?[0-9]*)+$" ErrorMessage="Invalid!" ControlToValidate="txtBorderSize">Invalid!</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Cell Number Format
                    </td>
                    <td>
                        <asp:TextBox ID="txtcellNumberFormat" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Font-Bold="true" runat="server" ID="RequiredFieldValidator11"
                            ForeColor="Red" ControlToValidate="txtcellNumberFormat" ErrorMessage="*">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator Font-Bold="true" ForeColor="Red" runat="server" ID="RegularExpressionValidator5"
                            ValidationExpression="^([0-9]*\s?[0-9]*)+$" ErrorMessage="Invalid!" ControlToValidate="txtcellNumberFormat">Invalid!</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Cell Warping
                    </td>
                    <td>
                        <asp:TextBox ID="txtCellWarping" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Font-Bold="true" runat="server" ID="RequiredFieldValidator12"
                            ForeColor="Red" ControlToValidate="txtCellWarping" ErrorMessage="*">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator Font-Bold="true" ForeColor="Red" runat="server" ID="RegularExpressionValidator4"
                            ValidationExpression="^([0-9]*\s?[0-9]*)+$" ErrorMessage="Invalid!" ControlToValidate="txtCellWarping">Invalid!</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        CellMerge
                    </td>
                    <td>
                        <asp:TextBox ID="txtCellMerge" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Font-Bold="true" runat="server" ID="RequiredFieldValidator13"
                            ForeColor="Red" ControlToValidate="txtCellMerge" ErrorMessage="*">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator Font-Bold="true" ForeColor="Red" runat="server" ID="RegularExpressionValidator3"
                            ValidationExpression="^([0-9]*\s?[0-9]*)+$" ErrorMessage="Invalid!" ControlToValidate="txtCellMerge">Invalid!</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Cell Merge Length
                    </td>
                    <td>
                        <asp:TextBox ID="txtCellMergeLength" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Font-Bold="true" runat="server" ID="RequiredFieldValidator14"
                            ForeColor="Red" ControlToValidate="txtCellMergeLength" ErrorMessage="*">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator Font-Bold="true" ForeColor="Red" runat="server" ID="RegularExpressionValidator2"
                            ValidationExpression="^([0-9]*\s?[0-9]*)+$" ErrorMessage="Invalid!" ControlToValidate="txtCellMergeLength">Invalid!</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Text Alignment
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
                    </td>
                    <td>
                        <br />
                        <asp:Button ID="btnSave" runat="server" Text="Save" />
                        <br />
                    </td>
                </tr>
            </table>
    </div>
    </p>
</asp:Content>
