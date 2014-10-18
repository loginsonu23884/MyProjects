<%@ Page Title="" Language="C#" MasterPageFile="~/POPPage.master" AutoEventWireup="true"
    CodeFile="Colums.aspx.cs" Inherits="Setting_Colums" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
 
    <script type="text/javascript">
        var ColumnName = "";
        function SetTableName(radioButtonObject) {
            var hiddelFieldId = radioButtonObject.id;

            hiddelFieldId = hiddelFieldId.replace("rdselect", "hdnColumnName");
           
            ColumnName = document.getElementById(hiddelFieldId).value;
            if (ColumnName == "") {
                document.getElementById("<%=btnEdit.ClientID %>").disabled = true;
               
        
                document.getElementById("<%=btnConditionalStyle.ClientID %>").disabled = true; 
            }
            else {
                document.getElementById("<%=btnEdit.ClientID %>").disabled = false;
               
             
                document.getElementById("<%=btnConditionalStyle.ClientID %>").disabled = false; 
            }
        }
        function SelectTableRow(radioButtonObject) {

            var IsChecked = radioButtonObject.checked;

            if (IsChecked) {

                radioButtonObject.parentElement.parentElement.style.backgroundColor = '#228b22';

                radioButtonObject.parentElement.parentElement.style.color = 'white';

            }

            var CurrentRdbID = radioButtonObject.id;

            var Chk = radioButtonObject;

            Parent = document.getElementById("<%=gvXMLColumns.ClientID%>");

            var items = Parent.getElementsByTagName('input');

            for (i = 0; i < items.length; i++) {

                if (items[i].id != CurrentRdbID && items[i].type == "radio") {

                    if (items[i].checked) {

                        items[i].checked = false;

                        items[i].parentElement.parentElement.style.backgroundColor = 'white'

                        items[i].parentElement.parentElement.style.color = 'black';
                    }

                }

            }

        }

        function funOpenColumnEditWindow() {
            if (ColumnName == "") {
                Sexy.alert('<h1>Please select row.</h1>');
            }
            else {
                $.fancybox.open({
                    href: '<%=OAppPath %>/Setting/ColumnSetting.aspx?TableName=<%=strTableName %>&ColumnName=' + ColumnName,
                    type: 'iframe',
                    width: '50%'
                });
            }
          
            return false;
        }

        function funOpenConditionalStyleWindow() {
            if (ColumnName == "") {
                Sexy.alert('<h1>Please select row.</h1>');
            }
            else {
                $.fancybox.open({
                    href: '<%=OAppPath %>/Setting/conditionalStyle.aspx?TableName=<%=strTableName %>&ColumnName=' + ColumnName,
                    type: 'iframe',
                    width: '90%',
                    minHeight: '450px'
                });
            }

            return false;
        }

        
    </script>
    <fieldset>
        <legend>Columns Style</legend>
             <b> Table-
            <%=strTableName%></b>
        <br /> <br />
        <asp:GridView ID="gvXMLColumns" runat="server" AutoGenerateColumns="false" AllowPaging="false"
            EnableViewState="true" AllowSorting="false" EmptyDataText="No record Found" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" ID="hdnColumnName" Value='<%#Bind("ColumnName") %>' />
                      <asp:RadioButton ID="rdselect" runat="server"  onclick="javascript:SelectTableRow(this);SetTableName(this);" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                </asp:TemplateField>
                <asp:BoundField DataField="ColumnName" HeaderStyle-HorizontalAlign="Left" HeaderText="Name" />
                <asp:BoundField DataField="text-for-cell-header" HeaderStyle-HorizontalAlign="Left"
                    HeaderText="Header" />
                <asp:BoundField DataField="IsCellFormat" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Cell Format" />
                <asp:BoundField DataField="IsCellBold" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Bold" />
                <asp:BoundField DataField="FontSize" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Font Size" />
                <asp:BoundField DataField="FontStyle" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Font Style" />
                <asp:BoundField DataField="BGColor" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="BG Color" />
                <asp:BoundField DataField="ForeColor" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Fore Color" />
                <asp:BoundField DataField="cellNumberFormat" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Cell Number Format" />
                <asp:BoundField DataField="CellWarping" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Warping" />
                <asp:BoundField DataField="CellMerge" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Merge" />
                <asp:BoundField DataField="CellMergeLength" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Merge Length" />
                <asp:BoundField DataField="TextAlignment" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Text Alignment" />
                <asp:BoundField DataField="Visible" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Visible" />
                <asp:BoundField DataField="IsCellItalic" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Italic" />
            </Columns>
        </asp:GridView>
    </fieldset>
    <fieldset>
        <legend>Setting</legend>
        <table>
            <tr>
                <td>
                    <input type="button" id="btnEdit" disabled="disabled" runat="server" value="Edit" onclick="funOpenColumnEditWindow();" />
                </td>
                 <td>
                    <input type="button" id="btnConditionalStyle" disabled="disabled" runat="server" value="Show Conditional Rule" onclick="funOpenConditionalStyleWindow();" />
                </td>
               
            </tr>
        </table>
    </fieldset>
    <asp:HiddenField ID="hdncolumn" runat="server" Value="" />
</asp:Content>
