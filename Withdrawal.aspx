<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Withdrawal.aspx.cs" Inherits="Withdrawal" MaintainScrollPositionOnPostback="true" %>
<%@ Register src="headerMenu.ascx" tagname="headerMenu" tagprefix="uc1" %>
<!DOCTYPE html>
<html lang="en">
    <head>
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
    <link rel="icon" type="image/png" sizes="32x32" href="images/5955--favicon.ico">
    <link rel="icon" type="image/png" sizes="16x16" href="images/favicon-16x16.png">
    <link rel="manifest" href="images/site.webmanifest">
    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.1/css/jquery.dataTables.min.css" />
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
	margin-top: 0.1em;
	margin-bottom: 0.3em;
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

 .footer {
   
    height: 60px;
    line-height: 60px;
    
}

 .form-control, .typeahead, .tt-query, .tt-hint {
    display: block;
    width: 100%;
    padding: 0.5rem 1.375rem;
    font-size: 0.875rem;
    font-weight: 400;
    line-height: 1;
    color: #212529;
    background-color: color(white);
    background-clip: padding-box;
    border: 1px solid #ced4da;
    appearance: none;
    border-radius: 2px;
    transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
}

 .form-group label {
    font-size: 1rem;
    line-height: 0.4rem;
    vertical-align: top;
    margin-bottom: 0.4rem;
}

 .form-group {
     margin-bottom: 0rem;
}

    </style>
</head>
  <body>
      
      <form  id="form2" runat="server" >


    <div class="container-scroller">
				
		
     <uc1:headerMenu ID="headerMenu1" runat="server" />
    <!-- partial -->
	<div class="container page-body-wrapper" style="background: #f0f3f6;"> 
			<div class="main-panel">
				<div class="content-wrapper">
					<div class="row d-flex justify-content-center">   
						<div class="col-md-9 p-2">
                            <h3 class="text-dark font-weight-bold mb-2">Withdrawal Request</h3>
                                <div class="card h-auto">
                                    <div class="card-header">
                                        <h4>Make Online Withdrawal</h4>
                                </div>
                
                                <div class="scrollable">                  
                                    <div class="card-body">
                                   <asp:GridView ID="grdData"  CssClass="table table-hover text-nowrap" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="None" Font-Size="10pt">
                                        <Columns>
                                            <asp:BoundField DataField="AMOUNT" HeaderText="TOTAL BALANCE" ItemStyle-CssClass="amount-field" HeaderStyle-Width="20%">
                                            <ItemStyle Font-Size="9pt"/>
                                            </asp:BoundField>
           
          
                                            <asp:TemplateField HeaderText="WITHDRAW % OF BALANCE" HeaderStyle-Width="30%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                    <ItemTemplate>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="withdrawPercent"  CssClass="withdrawPercent" Width="70%"  BorderWidth="1px" runat="server" type="Number" ValidationGroup="Taxation" min="0" max="<%# withpercent %>" step="0.01"></asp:TextBox>
                                                            <div class="input-group-append ml-1">
                                                            <asp:RequiredFieldValidator Display="Dynamic"  ToolTip="Please Enter Percent" ID="ReqwithdrawPercent" ControlToValidate="withdrawPercent" ForeColor="Red" Text="?" runat="server" ErrorMessage="Percent is Required" ValidationGroup="Taxation"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>-

                                                    </ItemTemplate>
                                                </asp:TemplateField>
            
                                              <asp:TemplateField HeaderText="WITHDRAW BALANCE AMOUNT" HeaderStyle-Width="30%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                    <ItemTemplate>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="withdrawAmount" CssClass="withdrawAmount" Width="70%"  BorderWidth="1px" runat="server" type="Number" ValidationGroup="Taxation" step="0.01"></asp:TextBox>
                                                            <div class="input-group-append ml-1">
                                                               <asp:RequiredFieldValidator Display="Dynamic" ToolTip="Please Enter Amount" ID="ReqwithdrawAmount" ControlToValidate="withdrawAmount" ForeColor="Red" Text="?" runat="server" ErrorMessage="Amount is Required" ValidationGroup="taxation"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="BUTTON">
                                                    <ItemTemplate>
                                                       <asp:Button ID="btnSubmit"  runat="server" CssClass="btn-primary"
                                                          OnClick="btnSubmit_Click" Text="Submit" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>--%>
                                        </Columns>
                            </asp:GridView>
                      </div>
                                </div></div>
                            </div>
                       <%-- <div class="col-md-3 p-2">
                            <h3 class="text-dark font-weight-bold mb-2">Tax Certificate upload</h3>
                            <div class="card" >
                                    <div class="card-header">
                                        <h4>Upload File </h4>
                                </div>
                              
                                    <div class="card-body" style="padding-top: 2rem !important; padding-bottom: 2rem !important;">
                                        
                                        </div>
                                </div>
                            </div>
						</div>--%>
                    <div class="row d-flex justify-content-center">   
						<div class="col-md-10 p-2">
                            <h3 class="text-dark font-weight-bold mb-2">Taxation Details</h3>
                                <div class="card h-auto">
                                    <div class="card-header">
                                        <h4>Enter Details </h4>
                                </div>
                              
                                    <div class="card-body">
                                   <table class="table table-hover text-nowrap">
                                    <thead>
                                        <tr>
                                            <th>YEAR</th>
                                            <th>Tax Paid/Payable (Rs)</th>
                                            <th>Total Taxable Income (Rs)</th>
                                            <th>Upload Tax Certificate</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <div class="input-group">
                                                    <asp:TextBox ID="Year1Taxable" ValidationGroup='Taxation' BorderWidth="1px" runat="server" TextMode="SingleLine" type="Number" Disabled="true"></asp:TextBox>
                                                     <%--<asp:TextBox ID="percentWithdraw" CssClass="percentWithdraw" ValidationGroup='Taxation' BorderWidth="1px" runat="server" TextMode="SingleLine" type="Number" style="display: none;"></asp:TextBox>
                                                     <asp:TextBox ID="amountWithdraw" CssClass="amountWithdraw" ValidationGroup='Taxation' BorderWidth="1px" runat="server" TextMode="SingleLine" type="Number" style="display: none;"></asp:TextBox>--%>
                                                    <div class="input-group-append ml-1">
                                                        <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup='Taxation' ToolTip="Please Enter Amount" ID="ReqYear1Taxable" ControlToValidate="Year1Taxable" ForeColor="Red" Text="?" runat="server" ErrorMessage="Taxable Amount is Required"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="input-group">
                                                    <asp:TextBox ID="taxPayable1" ValidationGroup='Taxation' BorderWidth="1px" runat="server" TextMode="SingleLine" type="Number" min="0"></asp:TextBox>
                                                    <div class="input-group-append ml-1">
                                                        <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup='Taxation' ToolTip="Please Enter Amount" ID="RequiredFieldValidator3" ControlToValidate="taxPayable1" ForeColor="Red" Text="?" runat="server" ErrorMessage="Taxable Amount is Required"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="input-group">
                                                    <asp:TextBox ID="totalTaxable1" ValidationGroup='Taxation' BorderWidth="1px" runat="server" TextMode="SingleLine" type="Number" min="0"></asp:TextBox>
                                                    <div class="input-group-append ml-1">
                                                        <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup='Taxation' ToolTip="Please Enter Amount" ID="RequiredFieldValidator6" ControlToValidate="totalTaxable1" ForeColor="Red" Text="?" runat="server" ErrorMessage="Taxable Amount is Required"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="input-group">
                                                    <input type="file" id="taxcertificate1" accept="/*" runat="server" ValidationGroup="Taxation"/>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="input-group">
                                                    <asp:TextBox ID="Year2Taxable" ValidationGroup='Taxation' BorderWidth="1px" runat="server" TextMode="SingleLine" type="Number" Disabled="true"></asp:TextBox>
                                                    <div class="input-group-append ml-1">
                                                        <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup='Taxation' ToolTip="Please Enter Amount" ID="RequiredFieldValidator1" ControlToValidate="Year2Taxable" ForeColor="Red" Text="?" runat="server" ErrorMessage="Taxable Amount is Required"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="input-group">
                                                    <asp:TextBox ID="taxPayable2" ValidationGroup='Taxation' BorderWidth="1px" runat="server" TextMode="SingleLine" type="Number" min="0"></asp:TextBox>
                                                    <div class="input-group-append ml-1">
                                                        <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup='Taxation' ToolTip="Please Enter Amount" ID="RequiredFieldValidator4" ControlToValidate="taxPayable2" ForeColor="Red" Text="?" runat="server" ErrorMessage="Taxable Amount is Required"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="input-group">
                                                    <asp:TextBox ID="totalTaxable2" ValidationGroup='Taxation' BorderWidth="1px" runat="server" TextMode="SingleLine" type="Number" min="0"></asp:TextBox>
                                                    <div class="input-group-append ml-1">
                                                        <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup='Taxation' ToolTip="Please Enter Amount" ID="RequiredFieldValidator7" ControlToValidate="totalTaxable2" ForeColor="Red" Text="?" runat="server" ErrorMessage="Taxable Amount is Required"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="input-group">
                                                    <input type="file" id="taxcertificate2" accept="/*" runat="server" ValidationGroup="Taxation"/>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="input-group">
                                                    <asp:TextBox ID="Year3Taxable" ValidationGroup='Taxation' BorderWidth="1px" runat="server" TextMode="SingleLine" type="Number" Disabled="true"></asp:TextBox>
                                                    <div class="input-group-append ml-1">
                                                        <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup='Taxation' ToolTip="Please Enter Amount" ID="RequiredFieldValidator2" ControlToValidate="Year3Taxable" ForeColor="Red" Text="?" runat="server" ErrorMessage="Taxable Amount is Required"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="input-group">
                                                    <asp:TextBox ID="taxPayable3" ValidationGroup='Taxation' BorderWidth="1px" runat="server" TextMode="SingleLine" type="Number" min="0"></asp:TextBox>
                                                    <div class="input-group-append ml-1">
                                                        <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup='Taxation' ToolTip="Please Enter Amount" ID="RequiredFieldValidator5" ControlToValidate="taxPayable3" ForeColor="Red" Text="?" runat="server" ErrorMessage="Taxable Amount is Required"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="input-group">
                                                    <asp:TextBox ID="totalTaxable3" ValidationGroup='Taxation' BorderWidth="1px" runat="server" TextMode="SingleLine" type="Number" min="0"></asp:TextBox>
                                                    <div class="input-group-append ml-1">
                                                        <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup='Taxation' ToolTip="Please Enter Amount" ID="RequiredFieldValidator8" ControlToValidate="totalTaxable3" ForeColor="Red" Text="?" runat="server" ErrorMessage="Taxable Amount is Required"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="input-group">
                                                    <input type="file" id="taxcertificate3" accept="/*" runat="server" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr class="text-center">
                                            <td colspan="4">
                                                <asp:Button ID="btnSubmit" ValidationGroup="Taxation" runat="server" CssClass="btn btn-primary"
                                                    OnClick="btnSubmit_Click" Text="Submit" />

                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                      </div>
                                </div>
                            </div>
						</div>
                    <div class="card-header">
                                        <h4>Previous Withdrawal History</h4>
                                </div>
                
                    <asp:GridView ID="GridView1"  CssClass="table table-hover text-nowrap" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="None" Font-Size="10pt">
                                        <Columns>
                                            <%--<asp:BoundField DataField="FOLIO_NUMBER" HeaderText="FOLIO NUMBER" Visible="False" />--%>
                                            <asp:BoundField DataField="FUND_NAME" HeaderText="FUND" >

                                            </asp:BoundField>
                                            <asp:BoundField DataField="NAV" HeaderText="NAV" >

                                            </asp:BoundField>
                                            <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" >

                                            </asp:BoundField>
                                            <asp:BoundField DataField="UNITS" HeaderText="UNITS" >

                                            </asp:BoundField>
                                              <asp:BoundField DataField="REMAINING_UNITS" HeaderText="REMAINING UNITS" >

                                            </asp:BoundField>
                                              <asp:BoundField DataField="REDEMPTION_DATE" HeaderText="REDEMPTION DATE"  DataFormatString="{0:dd-MMM-yy}" >

                                            </asp:BoundField>
                                              <%--<asp:BoundField DataField="APPROVED_DATE" HeaderText="APPROVED DATE"  DataFormatString="{0:dd-MMM-yy}">

                                            </asp:BoundField>--%>
                                             <asp:BoundField DataField="STATUS" HeaderText="STATUS">

                                            </asp:BoundField>   
                                        </Columns>
                            </asp:GridView>
                <%--</div>
              </div> 
 </div>
                            </div>
            </div>
					</div>--%>
					
				
				<footer class="footer fixed-bottom">
          <div class="footer-wrap">
            <div class="d-sm-flex justify-content-center justify-content-sm-between">
              <span class="text-muted text-center text-sm-left d-block d-sm-inline-block">&nbsp;</span>
              <span class="float-none float-sm-none d-block mt-1 mt-sm-0 text-center">Copyright © <a href="https://www.jsil.com" target="_blank">JS Investments Limited </a>2022</span>
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
      <script src="plugins/jquery/jquery.min.js"></script>
    <script src="js/dashboard.js"></script>
      <script src="https://cdn.datatables.net/1.13.1/js/jquery.dataTables.min.js"></script>
      <script type="text/javascript">

          $(document).ready(function () {
              var table = $('#GridView1').DataTable();
              var amountField = $('.amount-field');
              var withrawPercent = $('.withdrawPercent');
              var amountValue = parseFloat(amountField.text().replace(/,/g, ''));
              var withdrawAmount = $('.withdrawAmount');
              //var amountWithdraw = $('.amountWithdraw');
              //var percentWithdraw = $('.percentWithdraw');
              var percentValue;
              var percent;
              var totalBalance;
              var Balance;
              var percentage;
              var totalPercent;
              $('.withdrawPercent').on('keyup', function () {
                  percentValue = parseFloat(withrawPercent.val());
                  percent = percentValue / 100;
                  totalBalance = (amountValue * percent).toFixed(2);
                  withdrawAmount.val(totalBalance);
                  //amountWithdraw.val(totalBalance);
                  sendDataToServer();
              });
              $('.withdrawAmount').on('keyup', function () {
                  Balance = parseFloat(withdrawAmount.val());
                  percentage = Balance / amountValue;
                  totalPercent = (percentage * 100).toFixed(2);
                  withrawPercent.val(totalPercent);
                  //percentWithdraw.val(totalPercent);
                  sendDataToServer();
              });
              

          });

          function sendDataToServer() {
              var percentValue = parseFloat($('.withdrawPercent').val());
              var amountValue = parseFloat($('.withdrawAmount').val());

              // Use AJAX to send data to the server
              $.ajax({
                  type: 'POST',
                  url: 'Withdrawal.aspx/SaveData',
                  data: JSON.stringify({ percent: percentValue, amount: amountValue }),
                  contentType: 'application/json; charset=utf-8',
                  dataType: 'json',
                  success: function (response) {
                      console.log('Data sent successfully');
                      // You can handle the server response here if needed
                  },
                  error: function (error) {
                      console.error('Error sending data to server');
                  }
              });
          }

      </script>
      

</body>

</html>
