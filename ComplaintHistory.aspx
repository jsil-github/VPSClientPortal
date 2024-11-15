<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ComplaintHistory.aspx.cs" Inherits="ComplaintHistory" %>
<%@ Register src="headerMenu.ascx" tagname="headerMenu" tagprefix="uc1" %>
<!DOCTYPE html>
<html lang="en"><head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>SMA Portal</title>
    <!-- base:css -->
    <link rel="stylesheet" href="vendors/mdi/css/materialdesignicons.min.css">
    <link rel="stylesheet" href="vendors/base/vendor.bundle.base.css">
    <link rel="stylesheet" href="fontOwsom/css/font-awesome.css">
   <link href="css/style.css" rel="stylesheet" />
    <link rel="apple-touch-icon" sizes="180x180" href="images/apple-touch-icon.png">
    <link rel="icon" type="image/png" sizes="32x32" href="images/favicon-32x32.png">
    <link rel="icon" type="image/png" sizes="16x16" href="images/favicon-16x16.png">
    <link rel="manifest" href="images/site.webmanifest">
    <link href="css/jsstyle.css" rel="stylesheet" />

    <style>
       
 .span{
	position: absolute;
	margin-left: 5px;
	height: 25px;
	display: flex;
	align-items: center;
}
input{
	 padding-left: 25px;
  
	 outline: none;
}

    </style>
    
</head>
  <body>
      <form  id="form1" runat="server" >


    <div class="container-scroller">
				
		
     <uc1:headerMenu ID="headerMenu1" runat="server" />
    <!-- partial -->
		<div class="container page-body-wrapper" style="background: #f0f3f6;"> 
			<div class="main-panel">
				<div class="content-wrapper">

                   
					<div class="row d-flex justify-content-center">
                       
						<div class="col-md-12">
                            <h3 class="text-dark font-weight-bold mb-4">Complaints History</h3>
              <asp:GridView ID="GridView1" runat="server" CssClass="display compact" Width="100%" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="COMPLAINT_ID" HeaderText="Ticket No." />
                    <asp:BoundField DataField="COMPLAINT_DATE" HeaderText="Date Of Generation" />
                    <asp:BoundField DataField="PROBLEM_TYPE" HeaderText="Complaint Type" />
                    <asp:BoundField DataField="STATUS_DATE" HeaderText="Date Of Closure" />
                   
                </Columns>
            </asp:GridView>
            </div>
					</div>
					
				
				<footer class="footer fixed-bottom">
          <div class="footer-wrap">
            <div class="d-sm-flex justify-content-center justify-content-sm-between">
              <span class="text-muted text-center text-sm-left d-block d-sm-inline-block">&nbsp;</span>
              <span class="float-none float-sm-none d-block mt-1 mt-sm-0 text-center">Copyright © <a href="#" target="_blank">JS Investment </a>2022</span>
            </div>
          </div>
        </footer>
				<!-- partial -->
		
			<!-- main-panel ends -->
		</div>
		<!-- page-body-wrapper ends -->
    </div></div>
		<!-- container-scroller -->
        </div>
        
</form>
    <!-- base:js -->
    <script src="vendors/base/vendor.bundle.base.js"></script>
    <!-- endinject -->
    <!-- Plugin js for this page-->
    <!-- End plugin js for this page-->
    <!-- inject:js -->
    <script src="js/template.js"></script>
    <!-- endinject -->
    <!-- plugin js for this page -->
    <!-- End plugin js for this page -->
    <script src="vendors/chart.js/Chart.min.js"></script>
    <script src="vendors/progressbar.js/progressbar.min.js"></script>
		<script src="vendors/chartjs-plugin-datalabels/chartjs-plugin-datalabels.js"></script>
		<script src="vendors/justgage/raphael-2.1.4.min.js"></script>
		<script src="vendors/justgage/justgage.js"></script>
    <script src="js/jquery.cookie.js" type="text/javascript"></script>
    <!-- Custom js for this page-->
    <script src="js/dashboard.js"></script>
     
   
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
            
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.18.1/moment.min.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/1.10.9/js/jquery.dataTables.min.js"></script>
<link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css" />
       <link href="datetimepicker/css/bootstrap-datepicker.css" rel="stylesheet" />
      <script src="datetimepicker/js/bootstrap-datepicker.min.js"></script>
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


        var table = $('#GridView1').DataTable({
            orderCellsTop: true,
            fixedHeader: true,

            'processing': true,
            columnDefs: [{
                targets: 1, render: function (data) {
                    return moment(data).format('DD/MM/YYYY');
                }
            }],

            initComplete: function () {
                var api = this.api();
                api.columns().eq(0).each(function (colIdx) {
                    var column = $('.filters th').eq(
                        $(api.column(colIdx).header()).index()
                    );

                    if (colIdx == 1) {

                        $(api.columns(colIdx).header()).append("<br>")
                        var cell = $('.filters th').eq(
                            $(api.column(colIdx).header()).index()
                        );
                        var title = $(cell).text();

                        /*$(cell).html('<input type="text" placeholder="' + title + '" />');*/
                        var txt = $('<span class="span"><i class="fa fa-calendar" onclick="setDatepicker()"></i></span><input type="text" style="width:98%" class="datepick" placeholder="' + title + '" />').appendTo($(api.columns(colIdx).header()));


                        // On every keypress in this input
                        //$(
                        //    'input',
                        //    $('.filters th').eq($(api.column(colIdx).header()).index())
                        //)
                        txt.off('keyup change')
                            .on('keyup change', function (e) {
                                e.stopPropagation();

                                // Get the search value
                                $(this).attr('title', $(this).val());
                                var regexr = '({search})'; //$(this).parents('th').find('select').val();

                                var cursorPosition = this.selectionStart;
                                // Search the column for that value
                                api
                                    .column(colIdx)
                                    .search(
                                        this.value != ''
                                            ? regexr.replace('{search}', '(((' + this.value + ')))')
                                            : '',
                                        this.value != '',
                                        this.value == ''
                                    )
                                    .draw();

                                $(this)
                                    .focus()[0]
                                    .setSelectionRange(cursorPosition, cursorPosition);
                            });

                    }

                    else {

                        $(api.columns(colIdx).header()).append("<br>")
                        var select = $('<select style="width:98%;margin-bottom:10px;margin-top:10px"><option value=""></option></select>')
                            .appendTo($(api.columns(colIdx).header()))
                            .on('change', function () {
                                debugger;
                                var val = $.fn.dataTable.util.escapeRegex(
                                    $(this).val()
                                );

                                api.column(colIdx).search(val ? '^' + val + '$' : '', true, false)
                                    .draw();
                            });

                        api.columns(colIdx).data().unique().sort().each(function (d, j) {
                            for (let i = 0; i < d.length; i++) {
                                select.append('<option value="' + d[i] + '">' + d[i] + '</option>')
                            }

                        });
                    }
                });
            }

        });

        $(".datepick").datepicker({

            format: "dd/mm/yyyy",


            autoclose: true,

        });

        //$('.datepick').datepicker("option", "onSelect", function (dateString) {
        //    oTable.fnDraw();
        //});






    });

    function setDatepicker() {



        $(".datepick").datepicker({
            format: "dd/mm/yyyy",
            autoclose: true,
        });
    }
</script>    

</body></html>
