<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="ResetPassword" %>
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
    <link rel="icon" type="image/png" sizes="32x32" href="images/5955--favicon.ico">
    <link rel="icon" type="image/png" sizes="16x16" href="images/favicon-16x16.png">
    <link rel="manifest" href="images/site.webmanifest">
   
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
        #togglePassword {
        position: absolute;
    	margin-left: 29.5%;
    	top: 47%;
        cursor: pointer;
    }
    </style>
</head>
  <body>
      <form  id="form1" runat="server" >


    <div class="container-scroller">
				
		
    <!-- partial -->
		<div class="container page-body-wrapper" style="background: #f0f3f6;"> 
			<div class="main-panel">
				<div class="content-wrapper">

                   
					<div class="row d-flex justify-content-center">
                       
						<div class="col-md-9 p-4">
                            <h3 class="text-dark font-weight-bold mb-4">Change Password</h3>
              <div class="card">
                  <div class="card-header">
                                  
                                    <h4> Reset Your Password

                                    </h4>
                                </div>
                
                                    
                  <div class="card-body">
                 


                  <p class="card-description d-flex justify-content-center pb-3" style="color:red">
                    All asterisk (*) fields are required                   
                  </p>

                       <p class="card-description d-flex justify-content-center pb-3" style="color:red">
                           <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>                  
                  </p>

  <div class="row d-flex justify-content-center ms-lg-0 m-2">
                                <div class="col-md-8">   
                                    


        
                                                                        <div class="form-group row">
    <label for="inputPassword3" class="col-sm-4 col-form-label">New Password <span style="color:red">*</span></label>
    <div class="col-sm-7">
	
        <asp:TextBox ID="NewPass" type="password" CssClass="form-control pass" runat="server" placeholder="Enter New Password"></asp:TextBox><i id="togglePassword" class="fa fa-eye"></i>
                              
        
	<asp:RequiredFieldValidator runat="server" ID="ReqNewPass" ValidationGroup="Password" ControlToValidate="NewPass" Display="Dynamic" ErrorMessage="Please Enter New Password" ForeColor="Red" />        

      </div>
        <div class="col-sm-1">
    </div>
  </div>
                                                                        <div class="form-group row">
    <label for="inputPassword3" class="col-sm-4 col-form-label">Confirm Password <span style="color:red">*</span></label>
    <div class="col-sm-7">
        <asp:TextBox ID="ConfirmPass" type="password" CssClass="form-control" runat="server" placeholder="Re-type New Password"></asp:TextBox>
        <asp:CompareValidator ID="compateValidator1" runat="server" ValidationGroup="Password" ControlToCompare="NewPass" ControlToValidate="ConfirmPass" ErrorMessage="Password Must Match" ToolTip="Password must be the same" Display="Dynamic" ForeColor="Red" />	
<asp:RequiredFieldValidator runat="server" ID="reqPass" ValidationGroup="Password" ControlToValidate="ConfirmPass" Display="Dynamic" ErrorMessage="Please Re-type New Password" ForeColor="Red" />        

      </div>

        <div class="col-sm-1">
    </div>

  </div>
  
  <div class="form-group row">
      <div class="col-sm-4">
          </div>
    <div class="col-sm-8 mt-3">
        <asp:Button ID="btnSubmit" CssClass="btn btn-primary" ValidationGroup="Password" runat="server" Text="Change Password" OnClick="btnSubmit_Click" />
      
    </div>
  </div>

                </div>
              </div> 
 </div>
                            </div>
            </div>
					</div>
					
				
				<footer class="footer fixed-bottom">
          <div class="footer-wrap">
            <div class="d-sm-flex justify-content-center justify-content-sm-between">
              <span class="text-muted text-center text-sm-left d-block d-sm-inline-block">&nbsp;</span>
              <span class="float-none float-sm-none d-block mt-1 mt-sm-0 text-center">Copyright © <a href="#" target="_blank">JS Investments Limited </a>2022</span>
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
          <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
</form>
<script type="text/javascript">
    var togglePassword = document.querySelector('#togglePassword');
    var password = document.querySelector('#NewPass');
    togglePassword.addEventListener('click', function (e) {
        const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
        password.setAttribute('type', type);
        this.classList.toggle('fa-eye-slash');
    });
</script>
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

<script src="https://code.highcharts.com/modules/exporting.js"></script>
<script src="https://code.highcharts.com/modules/export-data.js"></script>
<script src="https://code.highcharts.com/modules/accessibility.js"></script>

      

</body></html>
