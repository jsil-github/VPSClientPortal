<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetNavReturnsPerformance.aspx.cs" Inherits="GetNavReturnsPerformance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server" CssClass="display compact" Width="100%" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="MarketName" HeaderText="MarketName" />
                    <asp:BoundField DataField="FundName" HeaderText="FundName" />
                    <asp:BoundField DataField="Rating" HeaderText="Rating" />
                    <asp:BoundField DataField="ValidityDate" HeaderText="ValidityDate" />
                    <asp:BoundField DataField="NAV" HeaderText="NAV" />
                    <asp:BoundField DataField="ThreeSixtyFiveDay" HeaderText="ThreeSixtyFiveDay" />
                </Columns>
            </asp:GridView>
        </div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Get Nav Returns Performance" />
    </form>
</body>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/1.10.9/js/jquery.dataTables.min.js"></script>
<link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css" />
<script type="text/javascript">
    //$(function () {
    //    $("[id*=GridView1]").DataTable(
    //        {
    //            bLengthChange: true,
    //            lengthMenu: [[5, 10, -1], [5, 10, "All"]],
    //            bFilter: true,
    //            bSort: true,
    //            bPaginate: true
    //        });
    //});

    $(document).ready(function () {
        // Setup - add a text input to each footer cell
        $('#GridView1 thead tr')
            .clone(true)
            .addClass('filters')
            .appendTo('#example thead');

        var table = $('#GridView1').DataTable({
            orderCellsTop: true,
            fixedHeader: true,
            //initComplete: function () {
            //    var api = this.api();

            //    // For each column
            //    api
            //        .columns()
            //        .eq(0)
            //        .each(function (colIdx) {
            //            // Set the header cell to contain the input element
            //            var cell = $('.filters th').eq(
            //                $(api.column(colIdx).header()).index()
            //            );
            //            var title = $(cell).text();
            //            $(cell).html('<input type="text" placeholder="' + title + '" />');

            //            // On every keypress in this input
            //            $(
            //                'input',
            //                $('.filters th').eq($(api.column(colIdx).header()).index())
            //            )
            //                .off('keyup change')
            //                .on('keyup change', function (e) {
            //                    e.stopPropagation();

            //                    // Get the search value
            //                    $(this).attr('title', $(this).val());
            //                    var regexr = '({search})'; //$(this).parents('th').find('select').val();

            //                    var cursorPosition = this.selectionStart;
            //                    // Search the column for that value
            //                    api
            //                        .column(colIdx)
            //                        .search(
            //                            this.value != ''
            //                                ? regexr.replace('{search}', '(((' + this.value + ')))')
            //                                : '',
            //                            this.value != '',
            //                            this.value == ''
            //                        )
            //                        .draw();

            //                    $(this)
            //                        .focus()[0]
            //                        .setSelectionRange(cursorPosition, cursorPosition);
            //                });
            //        });
            //},

            initComplete: function () {
                this.api().columns().every(function () {
                    var column = this;
                    $(column.header()).append("<br>")
                    var select = $('<select><option value=""></option></select>')
                        .appendTo($(column.header()))
                        .on('change', function () {
                            var val = $.fn.dataTable.util.escapeRegex(
                                $(this).val()
                            );

                            column
                                .search(val ? '^' + val + '$' : '', true, false)
                                .draw();
                        });
                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option value="' + d + '">' + d + '</option>')
                    });
                });
            }
        });
    });
</script>
</html>
