<%@ Page Title="" Language="C#" MasterPageFile="MasterPages/LoginMaster.master" AutoEventWireup="true" CodeFile="Loginpage.aspx.cs" Inherits="Loginpage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div align="center" style="margin-top:20px">
        <asp:Label Text="" Visible="false" Font-Bold="true" ForeColor="Red" ID="lblMessage"
            runat="server"></asp:Label>
    </div>
    <br />
    <fieldset style="width:50%;margin-left:25%;">
        <legend style="font-weight: bold"><%=Resources.SalesReporting.Login %></legend>
        <table align="center" cellspacing="0" cellpadding="4" style="width:100%;">
            <tr>
                <td align="right" style="width:50%;">
                    <%=Resources.SalesReporting.Username%>
                </td>
                <td  style="width:50%;" align="left">
                    <asp:TextBox ID="txtUsername" runat="server" MaxLength="100" CssClass="textBox" ToolTip="UserName"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <%=Resources.SalesReporting.Password%>
                </td>
                <td  align="left">
                    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" MaxLength="50" CssClass="textBox" ToolTip="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
            <td></td>
                <td align="left">
                <asp:Button ID="btnLogin" runat="server" Text="Login" Font-Bold="true" ToolTip="Login"
                        OnClick="btnLogin_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
