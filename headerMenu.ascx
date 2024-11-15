<%@ Control Language="C#" AutoEventWireup="true" CodeFile="headerMenu.ascx.cs" Inherits="headerMenu" %>
<div class="horizontal-menu">
      <nav class="navbar top-navbar col-lg-12 col-12 p-0">
        <div class="container">
          <div class="navbar-menu-wrapper d-flex align-items-center justify-content-between">
            

            <div class="text-center navbar-brand-wrapper d-flex align-items-center justify-content-center">
                <a class="navbar-brand brand-logo" href="Default.aspx"><img src="images/logo.png" alt="logo" class="img-responsive img-fluid"></a>
                <a class="navbar-brand brand-logo-mini" href="Default.aspx"><img src="images/logo-mini.png" alt="logo"></a>
            </div>
            <ul class="navbar-nav navbar-nav-right">
                 <li class="nav-item dropdown  d-lg-flex d-none">
                  <asp:button type="button" ID="ChangePasswordBtn" runat="server" class="btn btn-inverse-primary btn-sm" Text="Change Password" OnClick="ChangePasswordBtn_Click"> </asp:button>
                </li>
				<li class="nav-item">

					<!--<span class="nav-profile-name">User ID J12345</span>-->
					
					<!--<i class="mdi mdi mdi-logout-variant"></i>-->
                    <asp:ImageButton ID="btnLogout" src="images/logout.png" height="25" runat="server"  alt="Logout" title="Logout Here" OnClick="btnLogout_Click" />
					<%--<img src="images/logout.png" height="25" runat="server"  alt="Logout" title="Logout Here">--%>

					
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
                <h3 class="font-weight-bold mb-1" style="margin-top: 5px;">
                    <i class="fa fa-user" aria-hidden="true"></i> &nbsp;Hi, <%=Session["TITLE"].ToString() %> <%= Session["FolioNo"].ToString() %>
                </h3>
            </div>
        </div>
    </div>
    <div class="col-sm-6">
        <div class="d-flex align-items-center justify-content-md-end">
            <div class="pe-1 mb-3 mb-xl-0">
                <ul class="nav page-navigation">
                    <li class="nav-item">
                        <a href="Default.aspx" class="nav-link">
                            <span class="menu-title">Dashboard</span>
                            <i class="menu-arrow"></i>
                        </a>
                    </li>
                    <li class="nav-item">
                        <a href="#" class="nav-link">
                            <span class="menu-title">Transactions</span>
                            <i class="menu-arrow"></i>
                        </a>
                        <!-- Child Menu for Transactions -->
                        <div class="submenu">
                            <ul>
                                <li class="nav-item">
                                    <a class="nav-link" href="Withdrawal.aspx">Withdrawal</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="ChangeFund.aspx">Change Fund</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="ChangeAllocation.aspx">Change Allocation</a>
                                </li>
                            </ul>
                        </div>
                    </li>
                    <li class="nav-item">
                        <a href="lodgeComplaint.aspx" class="nav-link">
                            <span class="menu-title" style="margin-right:15px">Contact Us</span>
                            <i class="menu-arrow"></i>
                        </a>
                    </li>
                    <li class="nav-item">
                        <a href="SetupProfile.aspx" class="nav-link">
                            <span class="menu-title">Profile</span>
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


<!-- Bootstrap Small Modal -->
<div class="modal fade" id="dynamicModal" tabindex="-1" aria-labelledby="modalLabel" aria-hidden="true">
  <div class="modal-dialog modal-sm">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="modalLabel">Message</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <p id="modalMessage">This is a default message.</p>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>

<script>
    // jQuery function to open the modal with a dynamic message
    function FireAlert(message) {
        $('#modalMessage').text(message); // Set the dynamic message
        $('#dynamicModal').modal('show'); // Show the modal
    }

    // Example: Trigger the modal with a specific message
  
</script>