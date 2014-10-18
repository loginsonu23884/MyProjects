<%@ Page Title="Tables" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<script type="text/javascript">
    var tableName = "";
    function SetTableName(radioButtonObject) {
        var hiddelFieldId = radioButtonObject.id;

        hiddelFieldId = hiddelFieldId.replace("rdselect", "hdnTableName");

        tableName = document.getElementById(hiddelFieldId).value;
        if(tableName=="")
        {
           document.getElementById("<%=btnEdit.ClientID %>").disabled=true;
           document.getElementById("<%=btnColumns.ClientID %>").disabled=true;
          
           
           document.getElementById("<%=btnDoubleHeader.ClientID %>").disabled = true;
        }
        else{
            document.getElementById("<%=btnEdit.ClientID %>").disabled = false;
            document.getElementById("<%=btnColumns.ClientID %>").disabled = false;
          
           
            document.getElementById("<%=btnDoubleHeader.ClientID %>").disabled = false;
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

        Parent = document.getElementById("<%=gvXMLTables.ClientID%>");

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
</script>

   
    <script type="text/javascript">

        function funOpenTableEditWindow() {
            if (tableName == "") {
                Sexy.alert('<h1>Please select table row.</h1>');
            }
            else {
                $.fancybox.open({
                    href: '<%=OAppPath %>/Setting/TableSetting.aspx?TableName=' + tableName,
                    type: 'iframe',
                    width: '50%',
                    minHeight: '550px'
                });
            }
           
            return false;
        }
        function funOpenColumnWindow() {
            if (tableName == "") {
                Sexy.alert('<h1>Please select table row.</h1>');
            }
            else {
                $.fancybox.open({
                    href: '<%=OAppPath %>/Setting/Colums.aspx?TableName=' + tableName,
                    type: 'iframe',
                    width: '90%',
                    minHeight:'600px'
                });
            }
            return false;
        }

        function funOpenDoubleHeaderWindow() {
            if (tableName == "") {
                Sexy.alert('<h1>Please select table row.</h1>');
            }
            else {
                $.fancybox.open({
                    href: '<%=OAppPath %>/Setting/DoubleHeader.aspx?TableName=' + tableName,
                    type: 'iframe',
                    width: '90%',
                    minHeight: '600px'
                });
            }
            return false;
        }
    </script>
    <fieldset>
        <legend>XML Table List </legend><b>Sheets:</b>
        <asp:DropDownList runat="server" ID="ddlSheets" AutoPostBack="true" OnSelectedIndexChanged="ddlSheets_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        <br />
        <asp:GridView ID="gvXMLTables" runat="server" AutoGenerateColumns="false" AllowPaging="false" 
            EnableViewState="true" AllowSorting="false" EmptyDataText="No record Found" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" ID="hdnTableName" Value='<%#Bind("Name") %>' />
                        <asp:HiddenField runat="server" ID="hdnTableID" Value='<%#Bind("Tables_id") %>' />
                       
                       <asp:RadioButton ID="rdselect" runat="server"  onclick="javascript:SelectTableRow(this);SetTableName(this);" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderStyle-HorizontalAlign="Left" HeaderText="Name" />
                <asp:BoundField DataField="Starting-Position-Of-A-Table" HeaderStyle-HorizontalAlign="center"
                    ItemStyle-HorizontalAlign="Center" HeaderText="Column Position" />
                <asp:BoundField DataField="Starting-Position-Of-A-Table-Row" HeaderStyle-HorizontalAlign="center"
                    ItemStyle-HorizontalAlign="Center" HeaderText="Row Position" />
                <asp:BoundField DataField="IsCellFormat" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                    HeaderText="Cell Format" />
                <asp:BoundField DataField="IsRowStyle" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center"
                    HeaderText="Row Style" />
                <asp:BoundField DataField="IsconditionalStyle" HeaderStyle-HorizontalAlign="center"
                    ItemStyle-HorizontalAlign="Center" HeaderText="Conditional Style" />
                <asp:BoundField DataField="IsLastRowColored" HeaderStyle-HorizontalAlign="center"
                    ItemStyle-HorizontalAlign="Center" HeaderText="Last Row Colored" />
                <asp:BoundField DataField="AlterNativeRowColor" HeaderStyle-HorizontalAlign="center"
                    ItemStyle-HorizontalAlign="Center" HeaderText="Alternative Row Color" />
            </Columns>
        </asp:GridView>
    </fieldset>
    <fieldset>
        <legend>Setting</legend>
        <table>
            <tr>
                <td>
                    <input type="button" ID="btnEdit" disabled="disabled"  runat="server" value="Edit" onclick="funOpenTableEditWindow();" />
                </td>
              
              
                <td>
                   <input type="button"  ID="btnColumns" disabled="disabled"  runat="server" value="Show Columns Style" onclick="funOpenColumnWindow();" />
                </td>
              <td>
                  <input type="button"  ID="btnDoubleHeader"  disabled="disabled"  runat="server" value="Double Header"  onclick="funOpenDoubleHeaderWindow();" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
