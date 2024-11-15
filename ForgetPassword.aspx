<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgetPassword.aspx.cs" Inherits="ForgetPassword" %>

<!DOCTYPE html>

<html lang="en"><head>
  <!-- Required meta tags -->
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <title>VPS Portal</title>
  <!-- base:css -->
  <link rel="stylesheet" href="vendors/mdi/css/materialdesignicons.min.css">
  <link rel="stylesheet" href="vendors/base/vendor.bundle.base.css">
  <link href="css/style.css" rel="stylesheet" />
    <link rel="apple-touch-icon" sizes="180x180" href="images/apple-touch-icon.png">
    <link rel="icon" type="image/png" sizes="32x32" href="images/favicon-32x32.png">
    <link rel="icon" type="image/png" sizes="16x16" href="images/favicon-16x16.png">
    <link rel="manifest" href="images/site.webmanifest">
    <style>

        mydiv {
  
  position: relative;
 
}

.box {
 
  position: absolute;
  top: 35px;
  left: 100px;
  width:40%;
  height:70%;
  
}
.img-responsive {
  display: inline-block;
}

.input-group-append .input-group-text, .input-group-prepend .input-group-text {
   
    padding: 0.4rem 0.75rem;
    
}
    </style>
</head>

<body>
    <form id="form1" runat="server" >
        
  <div class="container-scroller">
    <div class="container-fluid page-body-wrapper full-page-wrapper">
      <div class="content-wrapper d-flex align-items-stretch auth auth-img-bg">
        <div class="row flex-grow">
          <div class="col-lg-4 d-flex align-items-center justify-content-center mydiv" style="background-color:#f57d20">
            <div class="auth-form-transparent text-left p-3 box">
              
            <div class="col-sm-12 text-center pb-4" style="padding-top: 1.9rem;">
                
            <img src="images/logo.png" alt="Example image" class="img-responsive img-fluid" style="height: 45px;">
            </div>
                 <div class="col-sm-12 text-center pb-4">
               <h4>Forgot Password? Please Insert Your User-ID</h4>
                     <h4>
                         <asp:Label ID="lblMessage" ForeColor="Red" runat="server" Text=""></asp:Label></h4>
            </div>
             
              
                <div class="form-group">
                  
                  <div class="input-group" style="">
                    <div class="input-group-prepend bg-transparent" >
                      <span class="input-group-text bg-transparent border-right-0">
                        <i class="mdi mdi-account-outline text-primary" style=""></i>
                      </span>
                    </div>
                      <asp:TextBox ID="txtUsername" class="form-control border-left-0" data-toggle="tooltip" title="User is required!" maxlength="50" required placeholder="Username" runat="server"></asp:TextBox>
                   <%-- <input type="text" class="form-control border-left-0" id="exampleInputEmail" placeholder="Username">--%>
                  </div>
                </div>
                
                <div class="my-3">
                    <asp:Button ID="btnLogin" class="btn btn-block btn-primary btn-lg font-weight-medium auth-form-btn col-12" runat="server" Text="Submit" OnClick="btnLogin_Click" />
                  <%--<a class="btn btn-block btn-primary btn-lg font-weight-medium auth-form-btn col-12" href="index.html">Sign In</a>--%>
                <asp:HyperLink ID="hlBackToLogin" class="btn btn-block btn-secondary btn-lg font-weight-medium auth-form-btn col-12 mt-2" runat="server" NavigateUrl="login.aspx" Text="Back to Login" />
               
                    </div>
              
                
            <%--  <div class="my-3">
            <%-- <asp:Button ID="Button1" class="btn btn-block btn-primary btn-lg font-weight-medium auth-form-btn col-12" runat="server" Text="Submit" OnClick="btnLogin_Click" />--%>
             </div>

                <%--<div class="text-center mt-4 font-weight-light">
                  Don't have an account? <a href="register-2.html" class="text-primary">Create</a>
                </div>--%>
             
            </div>
          </div>
          <div class="col-lg-8 login-half-bg d-flex flex-row">
              <h1 class="text-white font-weight-medium text-center flex-grow align-self-start pt-5 mt-7" style="line-height: 53px;font-family: initial;padding-top: 50px;"><br><br></h1>
             
          </div>
        </div>
      </div>
      <!-- content-wrapper ends -->
    </div>
    <!-- page-body-wrapper ends -->
  </div>
  <!-- container-scroller -->
  <!-- base:js -->

        <%--<div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                Please Inser the correct User ID
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                <button type="button" class="btn btn-primary">Save changes</button>
              </div>
            </div>
          </div>
        </div>--%>
  <script src="vendors/base/vendor.bundle.base.js"></script>
<%-- <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>--%>
  <script src="js/template.js"></script>
 </form>

</body></html>
