<%@ Page Title="" Language="C#" MasterPageFile="~/POPPage.master" AutoEventWireup="true" CodeFile="DoubleHeader.aspx.cs" Inherits="Setting_DoubleHeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<script type="text/javascript">
    // this is for open the conditional rule edit window.
    function funOpenEditWindow(Buttonid) {
        var rowId = 0;
        var hiddelFieldId = "";

        hiddelFieldId = Buttonid.replace("btnEdit", "hdnRowId");
        rowId = document.getElementById(hiddelFieldId).value;

        $.fancybox.open({
            href: '<%=OAppPath %>/Setting/DoubleHeaderSetting.aspx?rowId=' + rowId + '&TableName=<%=strTableName %>',
            type: 'iframe',
            width: '90%',
            minHeight: '350px'
        });

        return false;
    }
</script>
    <fieldset>
        <legend>Double Header</legend>
         <b> Table-
            <%=strTableName%></b>
        <br /> <br />
        <asp:GridView ID="gvXMLDoubleHeader" runat="server" AutoGenerateColumns="false"
            AllowPaging="false" EnableViewState="true" AllowSorting="false" EmptyDataText="No record Found"
            Width="100%">
            <Columns>
                <asp:BoundField DataField="HeaderText" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                    HeaderText="Header Text" />
                <asp:BoundField DataField="CellMerge" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                    HeaderText="Cell Merge" />
                <asp:BoundField DataField="CellMergeLength" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                    HeaderText="Cell Merge Length" />
                <asp:BoundField DataField="IsCellFormat" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                    HeaderText="Cell Format" />
                    <asp:BoundField DataField="AlterNativeRowColor" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                    HeaderText="Alternative Row Color" />
                    <asp:BoundField DataField="IsCellBold" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                    HeaderText="Cell Bold" />
                      <asp:BoundField DataField="FontSize" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                    HeaderText="Font Size" />
                      <asp:BoundField DataField="FontStyle" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                    HeaderText="Font Style" />
                <asp:BoundField DataField="BGColor" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                    HeaderText="BG Color" />
                <asp:BoundField DataField="ForeColor" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                    HeaderText="Fore Color" />
                     <asp:BoundField DataField="TextAlignment" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                    HeaderText="Text Alignment" />
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                    <asp:HiddenField runat="server" ID="hdnRowId" Value='<%#Bind("Id") %>' />
                       <input type="button" id="btnEdit" runat="server" value="Edit" onclick='javascript:funOpenEditWindow(this.id)' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </fieldset>
    <asp:HiddenField ID="hdncolumn" runat="server" Value="" />
</asp:Content>

