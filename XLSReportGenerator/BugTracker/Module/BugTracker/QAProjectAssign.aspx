<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/BT_MasterPage.master"
    AutoEventWireup="true" CodeFile="QAProjectAssign.aspx.cs" Inherits="Module_BugTracker_QAProjectAssign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
    jQuery(document).ready(function () {
        setTimeout(function () { $("#<%=lblMessage.ClientID%>").hide(); }, 5000);
    });
    </script>
    <fieldset style="width: 95%; margin-left: 20px; margin-right: 20px;">
        <legend style="font-weight: bold">QA Assign Project</legend>
        <div align="center">
            <asp:Label Text="" Visible="false" Font-Bold="true" ForeColor="Red" ID="lblMessage"
                runat="server"></asp:Label>
        </div>
        <table cellpadding="2" cellspacing="10" width="100%">
            <tr>
                <td>
                    QA
                </td>
                <td>
                    <asp:DropDownList ID="ddlQA" runat="server" AutoPostBack="true"
                        onselectedindexchanged="ddlQA_SelectedIndexChanged">
                    </asp:DropDownList>                     
                    <br />
                     <asp:RequiredFieldValidator ID="RFVQA" InitialValue="0" runat="server" ControlToValidate="ddlQA"
                                    ErrorMessage="Required"></asp:RequiredFieldValidator>
                </td>               
            </tr>
            <tr>
                <td valign="top">
                    Project Name:
                </td>
                <td style="width: 85%;">
                    <asp:CheckBoxList runat="server" RepeatColumns="5" CellSpacing="2" ID="chkProjectName" RepeatDirection="Horizontal"
                        TextAlign="Right">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Font-Bold="true" ToolTip="Save"
                        OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
