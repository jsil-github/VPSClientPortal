<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default_sqlserver.aspx.cs" Inherits="Default_sqlserver" %>

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
     <link rel="stylesheet" href="css/jquery.dataTables.min.css">
    <link rel="stylesheet" href="css/fixedHeader.dataTables.min.css">
    
    <style>
        .horizontal-menu .top-navbar .navbar-menu-wrapper {
            height: 50px;
        }

        .horizontal-menu .top-navbar .navbar-brand-wrapper .navbar-brand img {
            max-width: 100%;
            height: 45px;
        }

        .nav-item .active {
            background-color: #005596;
            color: white;
        }
        .navbar {
    position: relative;
}

        .horizontal-menu .top-navbar {
    font-weight: 400;
    background: #ffffff;
    border-bottom: none;
}

@font-face {font-family: "Calisto MT V2";
  src: url("fonts/58862b5f5172c3609c9b0ced6da89b12.eot"); /* IE9*/
  src: url("fonts/58862b5f5172c3609c9b0ced6da89b12.eot?#iefix") format("embedded-opentype"), /* IE6-IE8 */
  url("fonts/58862b5f5172c3609c9b0ced6da89b12.woff2") format("woff2"), /* chrome、firefox */
  url("fonts/58862b5f5172c3609c9b0ced6da89b12.woff") format("woff"), /* chrome、firefox */
  url("fonts/58862b5f5172c3609c9b0ced6da89b12.ttf") format("truetype"), /* chrome、firefox、opera、Safari, Android, iOS 4.2+*/
  url("fonts/58862b5f5172c3609c9b0ced6da89b12.svg#Calisto MT V2") format("svg"); /* iOS 4.1- */
}

 h4 {
    margin-top: 0;
    margin-bottom: 0.1rem;
    font-weight: 500;
    line-height: 1.2;
     font-family:"Calisto MT V2" !important;
    font-size:14px;font-style:normal;
    }

 .card-header h4 {
  text-align-last: justify;
      display: inline-block;
      padding-left:5px;
}

        .topHeading {
           
            margin-bottom: 0;
            background-color: #011d5b;
            /* border-bottom: 2px solid #011d5b; */
            color: white;
            margin-left: -0.7rem;
            margin-right: -0.7rem;
        }
 
    .scrollable{
  overflow-y: auto;
  max-height: 206px;
}

    .card .card-body {
     padding: 10px;
     
}

    .card{
  
  height: 250px;
 position: relative;
}
  


   div#piePortAllow {
    margin: 0px;
    height: 180px;
    }

   div#piePortret {
    margin: 0px;
    height: 180px;
    }

  


     thead input {
        width: 100%;
    }
 .dataTables_filter {
display: none;
}
 .dataTables_length
 {
     display: none;
 }

 table.dataTable tbody th, table.dataTable tbody td {
    padding: 6px 0px;
}

 table.dataTable thead th, table.dataTable thead td {
    padding: 0px 0px;
    
}

 .dataTables_info
 {
     display: none;
 }

 .dataTables_length
 {
     display: none;
 }  

 .webgrid-table-hidden
{
    display: none;
}
 

.loading-gif {
position: absolute;
top: 50%;
left: 50%;
margin: -25px 0 0 -40px;
}

.loader {
  border: 16px solid #f3f3f3;
  border-radius: 50%;
  border-top: 16px solid #3498db;
  width: 80px;
  height: 80px;
  -webkit-animation: spin 2s linear infinite; /* Safari */
  animation: spin 2s linear infinite;
}

/* Safari */
@-webkit-keyframes spin {
  0% { -webkit-transform: rotate(0deg); }
  100% { -webkit-transform: rotate(360deg); }
}

@keyframes spin {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}
.table thead tr{
    position: sticky;
    top: 0;
    z-index: 1020;
    background-color:white;

}
    </style>
   
</head>
  <body>
      <form  id="form1" runat="server" >


    <div class="container-scroller">
				
		<!-- partial:partials/_horizontal-navbar.html -->
    <div class="horizontal-menu">
      <nav class="navbar top-navbar col-lg-12 col-12 p-0">
        <div class="container">
          <div class="navbar-menu-wrapper d-flex align-items-center justify-content-between">
            

            <div class="text-center navbar-brand-wrapper d-flex align-items-center justify-content-center">
                <a class="navbar-brand brand-logo" href="index.html"><img src="images/logo.png" alt="logo" class="img-responsive img-fluid"></a>
                <a class="navbar-brand brand-logo-mini" href="index.html"><img src="images/logo-mini.png" alt="logo"></a>
            </div>
            <ul class="navbar-nav navbar-nav-right">
                 <li class="nav-item dropdown  d-lg-flex d-none">
                  <button type="button" class="btn btn-inverse-primary btn-sm">Change Password </button>
                </li>
				<li class="nav-item">

					<!--<span class="nav-profile-name">User ID J12345</span>-->
					
					<!--<i class="mdi mdi mdi-logout-variant"></i>-->
					<img src="images/logout.png" height="25" alt="Logout" title="Logout Here">

					
				</li>
               
            </ul>
            <button class="navbar-toggler navbar-toggler-right d-lg-none align-self-center" type="button" data-toggle="horizontal-menu-toggle">
              <span class="mdi mdi-menu"></span>
            </button>
          </div>
        </div>
      </nav>
      <nav class="bottom-navbar">
         
        <div class="container" style="
">
            
            <div class="row topHeading">
						<div class="col-sm-6">
							<div class="d-lg-flex align-items-center">
							
							
							<div class="pe-1 mb-3 mb-xl-0">
										
                     
                  		<h3 class="font-weight-bold mb-1" style="margin-top: 5px;"><i class="fa fa-th" aria-hidden="true"></i>  &nbsp;Hi, Mr. Suleman!</h3>
              
    					</div>		
								
								
							</div>
						</div>
						<div class="col-sm-6">
							<div class="d-flex align-items-center justify-content-md-end">
								<div class="pe-1 mb-3 mb-xl-0">
										<ul class="nav page-navigation">

                                            <li class="nav-item">
                  <a href="#" class="nav-link">
                 
                    <span class="menu-title">Connect with Advisor</span>
                    <i class="menu-arrow"></i>
                  </a>
                  <div class="submenu">
                      <ul>
                          <li class="nav-item"><a class="nav-link" href="pages/ui-features/buttons.html">Lodge a complaint</a></li>
                          <li class="nav-item"><a class="nav-link" href="pages/ui-features/typography.html">In process complaints</a></li>
                          <li class="nav-item"><a class="nav-link" href="pages/ui-features/typography.html">Complaints history</a></li>
                      </ul>
                  </div>
              </li>
                           
                  
              <li class="nav-item">
                  <a href="#" class="nav-link">
                   
                    <span class="menu-title">Reports</span>
                    <i class="menu-arrow"></i>
                  </a>
              </li>
                         
              
            </ul>
								</div>
								
							</div>
						</div>
					</div>
        </div>
      </nav>
    </div>
    <!-- partial -->
		<div class="container page-body-wrapper" style="background: #f0f3f6;"> 
			<div class="main-panel">
				<div class="content-wrapper">

					<div class="row flex-grow">
						<div class="col-sm-4 grid-margin stretch-card">
                            <div class="card">
                                 <div class="loading-gif loader"></div> 
                                <div class="card-header">
                                    <i class="fa fa-address-card-o" aria-hidden="true"></i>
                                    <h4> Portfolio Details </h4>
                                </div>
                                <div class="scrollable">
                                <div class="card-body">

                                   <asp:GridView ID="grdData" CssClass="table table-hover text-nowrap" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="None" Font-Size="10pt">
        <Columns>
            <asp:BoundField DataField="FUND_CODE" HeaderText="FUND CODE" Visible="False" />
            <asp:BoundField DataField="FUND_NAME" HeaderText="FUND NAME" />
            <asp:BoundField DataField="TOTAL" HeaderText="TOTAL" />
        </Columns>
                      </asp:GridView>

                                </div></div>
                            </div>
						</div>
                      <div class="col-sm-4 grid-margin stretch-card">
              <div class="card">
                      <div class="loading-gif loader"></div>              
                                <div class="card-header">
                                    <i class="fa fa-bar-chart" aria-hidden="true"></i>
                                    <h4>Profit &amp; Loss</h4>
                                </div>
                <div class="card-body">
                    
                    <div class="scrollable">
                                <div class="card-body">
                                   <asp:GridView ID="grdProfitLoss" CssClass="table table-hover" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="None" Font-Size="10pt">
        <Columns>
            <asp:BoundField DataField="FUND_CODE" HeaderText="FUND CODE" Visible="False" />
            <asp:BoundField DataField="Type" HeaderText="FUND TYPE" />
            <asp:BoundField DataField="INCEPTION" HeaderText="Total" />
           
        </Columns>
                      </asp:GridView>

                                </div></div>
                  
                </div>
              </div>
           </div>
					
						<div class="col-sm-4 grid-margin stretch-card">
                            <div class="card">
                                 <div class="loading-gif loader"></div> 
                                <div class="card-header">
                                    <i class="fa fa-area-chart" aria-hidden="true"></i>
                                    <h4>Portfolio Returns</h4>
                                </div>
                                <div class="card-body">
<figure class="highcharts-figure">
     <div id="piePortret"></div>
    
</figure>
                                    

                                </div>
                            </div>
						</div>
					</div>
					
					
                    <div class="row flex-grow">
                        <div class="col-sm-4 grid-margin stretch-card">
                            <div class="card">
                                 <div class="loading-gif loader"></div> 
                                <div class="card-header">
                                    <i class="fa fa-calendar-plus-o" aria-hidden="true"></i>
                                    <h4>Portfolio Allocation</h4>
                                </div>
                                <div class="card-body">
                                   <figure class="highcharts-figure">
     <div id="piePortAllow"></div>
    
</figure>

   
    

                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4 grid-margin stretch-card">
                            <div class="card">
                                 <div class="loading-gif loader"></div> 
                                <div class="card-header">
                                    <i class="fa fa-calculator" aria-hidden="true"></i>
                                    <h4>Transaction Summary</h4>
                                </div>
                                 <div class="scrollable">
                                <div class="card-body">
                                   <asp:GridView ID="grdTransSummary" CssClass="table table-hover webgrid-table-hidden" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="None" Font-Size="10pt">
        <Columns>
            
            <asp:BoundField DataField="SALE_DATE" HeaderText="Date" DataFormatString="{0:d}" />
            <asp:BoundField DataField="TRANSACTION_TYPE" HeaderText="T.Type" />
           <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" DataFormatString="{0:n0}" />
        </Columns>
                      </asp:GridView>

                                </div></div>
                            </div>
                        </div>
                        <div class="col-sm-4 grid-margin stretch-card">
                            <div class="card">
                                 <div class="loading-gif loader"></div> 
                                <div class="card-header">
                                    <i class="fa fa-file-text" aria-hidden="true"></i>
                                    <h4>Fund Manager Reports Archive</h4>
                                </div>
                                <div class="scrollable">
                                <div class="card-body">
                                   <div class="container h-100">
  <div class="row h-100 justify-content-center align-items-center">
    <div class="col-12">
      
        <div class="form-group" style="padding-top:70px">
       
        <button type="submit" class="btn-lg btn-primary mx-auto d-block">View Report</button>
      </div>
    </div>   
  </div>  
</div>
                                </div>

                                </div>
                            </div>
                        </div>
                    </div>
					

				
				<!-- content-wrapper ends -->
				<!-- partial:partials/_footer.html -->
				<footer class="footer">
          <div class="footer-wrap">
            <div class="d-sm-flex justify-content-center justify-content-sm-between">
              <span class="text-muted text-center text-sm-left d-block d-sm-inline-block">&nbsp;</span>
              <span class="float-none float-sm-right d-block mt-1 mt-sm-0 text-center">Copyright © <a href="#" target="_blank">JS Investment </a>2022</span>
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
    <!-- End custom js for this page-->

       <script src="js/jquery.dataTables.min.js"></script>
       
      <script src="js/dataTables.responsive.min.js"></script>
       <script src="js/dataTables.fixedHeader.min.js"></script>

      



      <script src="js/highcharts.js"></script>
<script src="js/exporting.js"></script>
<script src="js/export-data.js"></script>
<script src="js/accessibility.js"></script>

   <script  type="text/javascript" >

       Highcharts.chart('piePortAllow', {
           chart: {
               plotBackgroundColor: null,
               plotBorderWidth: null,
               plotShadow: false,
               type: 'pie',
               //margin: [0, 0, 0, 0],
               //spacingTop: 0,
               //spacingBottom: 0,
               //spacingLeft: 0,
               //spacingRight: 0
           },
           title: {
               text: ''
           },
           tooltip: {
               pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
           },
           accessibility: {
               point: {
                   valueSuffix: '%'
               }
           },
           plotOptions: {
               pie: {
                   allowPointSelect: true,
                   size: '100%',
                   cursor: 'pointer',
                   dataLabels: {
                       enabled: true,
                       format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                   }
               }
           },
           series: [{
               name: 'Brands',
               colorByPoint: true,
             
               data: <% = Chartdata%>,
           }],
           credits: {
               enabled: false
           },

            
           
       });


       const chart =  Highcharts.chart('piePortret', {
           chart: {
               type: 'column'
           },
           title: {
               text: ''
           },
                      
           accessibility: {
               announceNewData: {
                   enabled: true
               }
           },
           xAxis: {
               type: 'category'
           },
           
           legend: {
               enabled: false
           },
           plotOptions: {
               series: {
                   borderWidth: 0,
                   dataLabels: {
                       enabled: true,
                       format: '{point.y:.1f}%'
                   }
               }
           },

           tooltip: {
               headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
               pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> of total<br/>'
           },

           series: [
               {
                   name: "",
                   colorByPoint: true,
                   
                   data: <%=barchartdata%>,
               }
           ],
           drilldown: {
               breadcrumbs: {
                   position: {
                       align: 'right'
                   }
               },
              
           },
           credits: {
               enabled: false
           },

       });
      
      
       

       $(document).ready(function () {
           
           var table = $('#grdTransSummary').DataTable({
               orderCellsTop: false,
              
               "processing": true,
               "paging": false,
               responsive: true,
               paging: false,
               initComplete: function () {
                   this.api().columns().every(function () {
                       var column = this;
                       $(column.header()).append("<br>")
                       var select = $('<select style="width:95%;margin-bottom:10px;margin-top:10px"><option value=""></option></select>')
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
           $('#grdTransSummary').show();
           table.columns.adjust().draw();
       });
       $(window).bind("load", function () {
           $(".loading-gif.loader").hide();
       });
      
   </script>   

</body></html>
