<%@ Page Title="" Language="C#" MasterPageFile="~/POPPage.master" AutoEventWireup="true"
    CodeFile="conditionalStyle.aspx.cs" Inherits="Setting_conditionalStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<script type="text/javascript">
    // this is for open the conditional rule edit window.
    function funOpenEditWindow(Buttonid) {
        var rowId = 0;
        var hiddelFieldId = "";
      
        hiddelFieldId = Buttonid.replace("btnEdit", "hdnRowId");
        rowId = document.getElementById(hiddelFieldId).value;
       
        $.fancybox.open({
            href: '<%=OAppPath %>/Setting/ManageRule.aspx?rowId=' + rowId + '&TableName=<%=strTableName %>&ColumnName=<%=strColumnName %>',
            type: 'iframe',
            width: '90%',
            minHeight: '350px'
        });

        return false;
    }
</script>
    <fieldset>
        <legend>Conditional Style</legend>
  <b> Table-
            <%=strTableName%></b>
        <br /> <br />
        <asp:GridView ID="gvXMLConditionalStyle" runat="server" AutoGenerateColumns="false"
            AllowPaging="false" EnableViewState="true" AllowSorting="false" EmptyDataText="No record Found"
            Width="100%">
            <Columns>
                <asp:BoundField DataField="ColumnName" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                    HeaderText="Column" />
                <asp:BoundField DataField="Type" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                    HeaderText="Type" />
                <asp:BoundField DataField="Condition" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                    HeaderText="Condition" />
                <asp:BoundField DataField="Value" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                    HeaderText="Value" />
                <asp:BoundField DataField="BGColor" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                    HeaderText="BG Color" />
                <asp:BoundField DataField="ForeColor" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                    HeaderText="Fore Color" />
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hdnRowId" Value='<%#Bind("Id") %>' />
                       <input type="button" id="btnEdit" runat="server" value=" Edit Rule" onclick='javascript:funOpenEditWindow(this.id)' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </fieldset>
    <asp:HiddenField ID="hdncolumn" runat="server" Value="" />
</asp:Content>
