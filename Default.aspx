<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mainmaster.master"  CodeFile="Default.aspx.cs" Inherits="DefaultPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
    <link href="NewsTicker/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="NewsTicker/marquee/css/marquee.css" />
    <link rel="stylesheet" href="datetimepicker/css/bootstrap-datepicker3.min.css" />
    <style>

        .demof {
             /* border: 1px solid #ccc;
              margin: 25px 0;*/
          }

              .demof ul {
                  padding: 0;
                  list-style: none;
              }

              .demof li {
                  padding: 10px;
                  border-bottom: 1px dashed #ccc;
              }

                  .demof li.odd {
                      background: #fafafa;
                  }

                  .demof li:after {
                      content: '';
                      display: block;
                      clear: both;
                  }

              .demof img {
                  float: left;
                  width: 100px;
                  margin: 5px 15px 0 0;
              }

              .demof a {
                  
                  color: #06f;
                  text-decoration:none
              }

              .demof p {
                  margin: 15px 0 0;
                  font-size: 12px;
              }
		.fontval {
			font-size: 12px;
		}
		.fontSym {
			font-size: 11px;
		}
		.demof div {
		  
  		line-height: 1;
  		
              }
		.demof div span {
			font-size: 0.9em;
		}
        #content{
      /*display:table-row;*/
      padding: 2%;
    }
    
        #content > div{
          display:table-cell;
          /*color: #5E50F9;*/
          color:#011d5b;
          /*padding: 2%;*/
        }   
        .leftside{
            /*width: 50%;*/
            width: 50%;
            float: left;
        }

		.leftside,.rightside{
            font-size:1.10rem;
            font-weight: bold;
            line-height: 1;
            text-shadow:none;
            margin-bottom:0.5rem;
            display: inline;
            

		}
        .rightside{
            text-align:right;
            /*margin-left: 50%;*/
            float:right
        }



    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" Runat="Server">
    <div class="section topIndices" style="font-size:0.8em">
        <div class="col-sm-3" style="background-color:#ccc">
           <div class="d-flex justify-content-between align-items-center breaking-news">
                
                <marquee class="news-scroll" behavior="scroll" direction="left" onmouseover="this.stop();" onmouseout="this.start();"> 
                      
                    <div class="glide__slides2">
                   

                        <%
                            System.Data.DataTable tbl = getStock();
                            for (int i = 0; i < tbl.Rows.Count; i++)
                            {
                                %>
                     <div class="glide__slide">
                        <div class="topIndices__item">
                            <div>
                                <div class="topIndices__item__name fontSym"><% Response.Write(tbl.Rows[i]["NAME"].ToString()); %></div>
                                <div class="topIndices__item__val fontval"><% Response.Write(tbl.Rows[i]["RATE"].ToString()); %></div>
                            </div>
                            <%
                                        if (Convert.ToDecimal(tbl.Rows[i]["STATUS"].ToString().Trim()) > 0)
                                        {%>
                            <div class="change__text--pos">
                                <div class="topIndices__item__change fontval">
                                                   
<i class="icon-up-dir"></i>
                   
                                        <%Response.Write(tbl.Rows[i]["PERSTAT"].ToString());%>
                                </div>
                                <%--<div class="topIndices__item__changep">(2.43%)</div>--%>
                            </div>
                            <% } else

                    {%>
                            <div class="change__text--neg">
                                <div class="topIndices__item__change fontval">
                                                   
                            <i class="icon-down-dir"></i>
                   
                                        <%Response.Write(tbl.Rows[i]["PERSTAT"].ToString()); %>
                                </div>
                                <%--<div class="topIndices__item__changep">(2.43%)</div>--%>
                            </div>
                 <% } %>

                        </div></div>

                           <%} %> 
                  


                </div></marquee>


           </div>
            </div>
						
				
        
        <div class="col-sm-9">
        <div class="glide multi">

           <%--TICKER 2 PLACE--%>

             <div class="glide__track" data-glide-el="track">
                <div class="glide__slides">
                   

                        <%
                            tbl = getStock2();
                            for (int i = 0; i < tbl.Rows.Count; i++)
                            {
                                %>
                     <div class="glide__slide">
                        <div class="topIndices__item">
                            <div>
                                <div class="topIndices__item__name fontSym"><% Response.Write(tbl.Rows[i]["symbol"].ToString()); %></div>
                                <div class="topIndices__item__val fontval"><% Response.Write(tbl.Rows[i]["RATE"].ToString()); %></div>
                            </div>
                            <%
                                        if (Convert.ToDecimal(tbl.Rows[i]["CHANGE"].ToString().Trim()) > 0)
                                        {%>
                            <div class="change__text--pos">
                                <div class="topIndices__item__change fontval">
                                                   
<i class="icon-up-dir"></i>
                   
                                        <%Response.Write(tbl.Rows[i]["CHANGE"].ToString() + "<span>%</span>"); %>
                                </div>
                                <%--<div class="topIndices__item__changep">(2.43%)</div>--%>
                            </div>
                            <% } else

                    {%>
                            <div class="change__text--neg">
                                <div class="topIndices__item__change fontval">
                                                   
                            <i class="icon-down-dir"></i>
                   
                                        <%Response.Write(tbl.Rows[i]["CHANGE"].ToString() + "<span>%</span>"); %>
                                </div>
                                <%--<div class="topIndices__item__changep">(2.43%)</div>--%>
                            </div>
                 <% } %>

                        </div></div>

                           <%} %> 
                  


                </div>
               <%-- <div class="glide__arrows" data-glide-el="controls">
                    <button class="glide__arrow glide__arrow--left" data-glide-dir="<"><i class="icon-left-open"></i></button>
                    <button class="glide__arrow glide__arrow--right" data-glide-dir=">"><i class="icon-right-open"></i></button>
                </div>--%>
            
        </div>
    </div>
            </div>
        </div>
    <div class="row flex-grow">

        <div class="col-sm-4 grid-margin stretch-card">
                            <div class="card">
                                 <div class="loading-gif loader"></div> 
                                <div class="card-header">
                                    <i class="fa fa-address-card-o" aria-hidden="true"></i>
                                    <h4> Account Summary </h4>
                                </div>
                                <div class="scrollable">
                                <div class="card-body">
                                <div id="content">
                                    <%--<h3 style="display:inline; text-align:left">
                                        Investment Amount
                                    </h3>
                                    <h3 style="display:inline; margin-left:32%">
                                        19583
                                    </h3>--%>
                                    <div class="leftside">
                                        CURRENT BALANCE
                                    </div>
                                    <div class="rightside">
                                        <%= amount %>
                                    </div>
                                </div>
                                   <asp:GridView ID="grdData" CssClass="table table-hover text-nowrap" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="None" Font-Size="10pt">
                                       <Columns>
                                          
            <asp:BoundField DataField="FOLIO_NUMBER" HeaderText="FOLIO NUMBER" Visible="False" />
            <asp:BoundField DataField="FUND_NAME" HeaderText="FUND" >
            
            <ItemStyle Font-Size="9pt"/>

            </asp:BoundField>
            <asp:BoundField DataField="NAV" HeaderText="NAV" >
            <HeaderStyle HorizontalAlign="Right" CssClass="alright" />
                <ItemStyle Font-Size="9pt" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="UNIT" HeaderText="UNITS" >
                <HeaderStyle HorizontalAlign="Right" CssClass="alright" />
                <ItemStyle Font-Size="9pt" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" DataFormatString="{0:N2}">
            <HeaderStyle HorizontalAlign="Right" CssClass="alright" />
            <ItemStyle HorizontalAlign="Right" Font-Size="9pt"/>
            </asp:BoundField>
        </Columns>
                      </asp:GridView>

                                </div></div>
                            </div>
						</div>
        <%--<div class="col-sm-4 grid-margin stretch-card">
                            <div class="card">
                                 <div class="loading-gif loader"></div> 
                                <div class="card-header">
                                    <i class="fa fa-calculator" aria-hidden="true"></i>
                                    <h4>Transaction History</h4>
                                </div>
                                 <div class="scrollable">
                                <div class="card-body">
                                   <asp:GridView ID="grdTransSummary" CssClass="table table-hover webgrid-table-hidden" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="None">
        <Columns>
            
            <asp:BoundField DataField="SALE_DATE" HeaderText="DATE" DataFormatString="{0:d}" />
            <asp:BoundField DataField="TRANSACTION_TYPE" HeaderText="TYPE" />
           <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" DataFormatString="{0:n0}">
                <HeaderStyle HorizontalAlign="Right" CssClass="alright" />
            <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
        </Columns>
                      </asp:GridView>

                                </div></div>
                            </div>
                        </div>--%>
        <%--<div class="col-sm-4 grid-margin stretch-card">
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
            <asp:BoundField DataField="INCEPTION" HeaderText="INCEPTION" DataFormatString="{0:n0}">
           
            <HeaderStyle HorizontalAlign="Right" CssClass="alright" />
            <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
           
        </Columns>
                      </asp:GridView>

                                </div></div>
                  
                </div>
              </div>
           </div>--%>
        <div class="col-sm-4 grid-margin stretch-card">
                            <div class="card">
                                 <div class="loading-gif loader"></div> 
                                <div class="card-header">
                                    <i class="fa fa-area-chart" aria-hidden="true"></i>
                                    <h4>Investment Performance</h4>
                                </div>
                                <div class="card-body">
<figure class="highcharts-figure">
     <div id="piePortret"></div>
    
</figure>
                                    

                                </div>
                            </div>
						</div>
		<div class="col-sm-4 grid-margin stretch-card">
              <div class="card">
                      <div class="loading-gif loader"></div>              
                                <div class="card-header">
                                    <i class="fa fa-bar-chart" aria-hidden="true"></i>
                                    <h4>JSIL Media</h4>
                                </div>
                <div class="card-body">
                    
                    <div class="scrollable">
                                <div class="card-body">
                                <div class="demo1 demof">
                                <ul>
                            
                                </ul>
                                </div>  

                                </div>

                    </div>
                  
                </div>
              </div>
           </div>	
						
					</div>
					
					
                    <div class="row flex-grow">
                     <div class="col-sm-4 grid-margin stretch-card">
                            <div class="card">
                                 <div class="loading-gif loader"></div> 
                                <div class="card-header">
                                    <i class="fa fa-address-card-o" aria-hidden="true"></i>
                                    <h4> Transaction Status </h4>
                                </div>
                                <div class="scrollable">
                                <div class="card-body">
                                
                                   <asp:GridView ID="TransactionView"  OnPageIndexChanging="TransactionView_PageIndexChanging" CssClass="table table-hover text-nowrap" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="None" Font-Size="10pt" OnPreRender="TransactionView_PreRender">
        <Columns>
            <%--<asp:BoundField DataField="FUND_CODE" HeaderText="FUND CODE" Visible="False" />--%>
            <asp:BoundField DataField="TRANSACTION_DATE" HeaderText="DATE" DataFormatString="{0:dd/MM/yyyy}">
            
            <ItemStyle Font-Size="9pt"/>

            </asp:BoundField>
            <asp:BoundField DataField="FUND_SHORT_NAME" HeaderText="FUND" >
                <ItemStyle Font-Size="9pt" />
            </asp:BoundField>
            <asp:BoundField DataField="TRANSACTION_TYPE" HeaderText="TYPE" >
                <ItemStyle Font-Size="9pt" />
            </asp:BoundField>
            <asp:BoundField DataField="QUANTITY" HeaderText="UNITS" DataFormatString="{0:N2}" >
                <HeaderStyle HorizontalAlign="Right" CssClass="alright" />
                <ItemStyle Font-Size="9pt" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" DataFormatString="{0:N2}" >
            <HeaderStyle HorizontalAlign="Right" CssClass="alright" />
            <ItemStyle HorizontalAlign="Right" Font-Size="9pt"/>
            </asp:BoundField>
        </Columns>
                      </asp:GridView>

                                </div></div>
                            </div>
						</div> 
                        


        <div class="col-sm-4 grid-margin stretch-card" style="display:none">
                            <div class="card">
                                 <div class="loading-gif loader"></div> 
                                <div class="card-header">
                                    <i class="fa fa-calendar-plus-o" aria-hidden="true"></i>
                                    <h4>Asset Allocation</h4>
                                </div>
                                <div class="card-body">
                                   <figure class="highcharts-figure">
                                       
     <div id="piePortAllow"> </div>
    
</figure>

   
    

                                </div>
                            </div>
                        </div>

<asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>

<div class="col-sm-4 grid-margin stretch-card">
                            <div class="card">
                                 <div class="loading-gif loader"></div> 
                                <div class="card-header">
                                    <i class="fa fa-area-chart" aria-hidden="true"></i>
                                    <h4>Portfolio Allocation</h4>
                                </div>
                                <div class="card-body">
                                    <asp:UpdatePanel ID="updatepanel1" runat="server">
    <ContentTemplate>
<figure class="highcharts-figure">
                                          
                                           <asp:DropDownList ID="accType" runat="server" OnSelectedIndexChanged="accType_SelectedIndexChanged" CssClass="d-none" AutoPostBack="true">
                                           <asp:ListItem Text="Islamic" Value="Islamic"></asp:ListItem>
                                           <asp:ListItem Text="Islamic" Value="Conventional"></asp:ListItem>
                                       </asp:DropDownList>
     <div id="profiledetail"></div>
    
</figure>
             </ContentTemplate>
        </asp:UpdatePanel>                       

                                </div>
                            </div>
						</div>
        
        <div class="col-sm-4 grid-margin stretch-card">
                            <%--<div class="card">
                                 <div class="loading-gif loader"></div> 
                                <div class="card-header">
                                    <i class="fa fa-file-text" aria-hidden="true"></i>
                                    <h4>Fund Manager Reports Archive</h4>
                                </div>
                                
                                <div class="scrollable">
                                <div class="card-body">
                                   <asp:GridView ID="grdReport" CssClass="table table-hover" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="None">
        <Columns>
            
            <asp:BoundField DataField="Month" HeaderText="DATE" DataFormatString="{0:d}" />
            <asp:TemplateField HeaderText="Report">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Link") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("Link") %>' OnCommand="LinkButton1_Command" Text='<%# Eval("text") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
          
        </Columns>
                      </asp:GridView>

                                </div></div>
                            </div>--%>
             <div class="card ">
                <div class="loading-gif loader"></div> 
                                <div class="card-header">
                                    <i class="fa fa-area-chart" aria-hidden="true"></i>
                                    <h4>Reports</h4>
                                </div>
                <div class="card-block first">
                    <div class="table-responsive">
  <table class="table p-0" style="border-style: none;">
    
    <tbody>
      <tr>
        
        <td class="p-3">FY Investment Certificate</td>
        <td><asp:Button ID="btnAgreementRpt" CssClass="btn btn-link" runat="server" Text="View Report"  /></td>
        
      </tr>
      <tr style="display:none">
        
        <td class="p-3">FMR Reports</td>
        <td><asp:Button ID="btnFMRReport" CssClass="btn btn-link" runat="server" Text="View Report"  /></td>
        
      </tr>
        <tr style="display:none;">
        
        <td class="p-3">Account Statements</td>
        <td><asp:Button ID="btnTaxStat" CssClass="btn btn-link" runat="server" Text="View Report"  /></td>
        
      </tr>
        <tr>
        
        <td class="p-3">Account Statements</td>
        <td><asp:Button ID="btnTransStatement" CssClass="btn btn-link" runat="server" Text="View Report"  /></td>
        
      </tr>
    </tbody>
  </table>
</div>
                      </div>
                    <div class="slidediv card-body">
                  <div class="slidedivaggrement">
                      <div class="container">
  <h3>Investment Certificate</h3>
  <p>&nbsp;</p>
  
    <div class="row">
      <div class="col">
          <asp:TextBox ID="StartDateInvestment"  class="form-control date-withicon" placeholder="Date from" runat="server"></asp:TextBox>
          
        
      </div>
        <div class="col" >
            <asp:TextBox ID="EndDateInvestment" class="form-control date-withicon" placeholder="Date To" runat="server"></asp:TextBox>
            </div>
     
    </div>
      <asp:Button ID="SubmitInvestment" runat="server" Text="Submit" class="btn btn-primary mt-3 float-end spinner-button" OnClick="SubmitInvestment_Click" />
        
    <button type="submit" class="btn btn-primary mt-3 backbutton">Back</button>
         
  
</div>
                        
                    </div>
                    <div class="slidedivfmr">
                        <div class="container">
  <h3>FMR Reports</h3>
  <p>&nbsp;</p>
  
    <div class="row">
      <div class="col">
       
          <asp:DropDownList ID="ddlFMRList" class="form-control" runat="server"></asp:DropDownList>
      </div>
     
    </div>
      <asp:Button ID="btnFMRSubmit" runat="server" Text="Submit" class="btn btn-primary mt-3 float-end spinner-button" OnClick="btnFMRSubmit_Click" />
        
    <button type="submit" class="btn btn-primary mt-3 backbutton">Back</button>
         
  
</div>
                    </div>
                    <div class="slidedivaccstat">
                        <div class="container">
  <h3>Account Statements Reports</h3>
  <p>&nbsp;</p>
 
    <div class="row">
      <div class="col">
       <%-- <asp:TextBox ID="txtAccountStatDatefrom" class="form-control" placeholder="Date from" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtAccountStatDateTo" class="form-control" placeholder="Date To" runat="server"></asp:TextBox>--%>
      </div>
     
    </div>
      <asp:Button ID="btnAccountStatementReport" runat="server" Text="Submit" class="btn btn-primary mt-3 float-end spinner-button" OnClick="btnAccountStatementReport_Click" />
        
    <button type="submit" class="btn btn-primary mt-3 backbutton">Back</button>
         
  
</div>

                    </div>
                    <div class="slidedivtransstat">
<div class="container">
  <h3>Account Statements</h3>
  <p>&nbsp;</p>
  
    <div class="row">
      <div class="col">
          <asp:TextBox ID="StartDateTax"  class="form-control date-withicon" placeholder="Date from" runat="server"></asp:TextBox>
          
        
      </div>
        <div class="col" >
            <asp:TextBox ID="EndDateTax" class="form-control date-withicon" placeholder="Date To" runat="server"></asp:TextBox>
            </div>
     
    </div>
      <asp:Button ID="SubmitTax" runat="server" Text="Submit" class="btn btn-primary mt-3 float-end spinner-button" OnClick="SubmitTax_Click" />
        
    <button type="submit" class="btn btn-primary mt-3 backbutton">Back</button>
         
  
</div>
                    </div>
                      
              
            </div>
                        </div>
                  </div> </div>

     <div class="row flex-grow" style="display:none">
         <div class="col-sm-12 grid-margin stretch-card">
                            <div class="card">
                                 <div class="loading-gif loader"></div> 
                                <div class="card-header">
                                    <i class="fa fa-calculator" aria-hidden="true"></i>
                                    <h4>Portfolio Details</h4>
                                </div>
                                 <div class="scrollable">
                                <div class="card-body">
                                   <asp:GridView ID="allocationDetail" CssClass="table table-hover" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="None" OnPreRender="allocationDetail_PreRender">
        <Columns>
            <asp:BoundField DataField="TYPE" HeaderText="Type" />
            
            <asp:BoundField DataField="SECURITY" HeaderText="Security" />
            
                       
            <asp:BoundField DataField="VOLUME" HeaderText="Volume" DataFormatString="{0:n0}">
                <HeaderStyle HorizontalAlign="Right" CssClass="alright" />
            <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>

             <asp:BoundField DataField="AVG_COST" HeaderText="Avg. Cost" DataFormatString="{0:n0}">
                <HeaderStyle HorizontalAlign="Right" CssClass="alright" />
            <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>

            <asp:BoundField DataField="PRICE" HeaderText="Price" DataFormatString="{0:n0}">
                <HeaderStyle HorizontalAlign="Right" CssClass="alright" />
            <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>

             <asp:BoundField DataField="COST" HeaderText="Cost" DataFormatString="{0:n0}">
                <HeaderStyle HorizontalAlign="Right" CssClass="alright" />
            <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>

             <asp:BoundField DataField="MARKETVALUE" HeaderText="Market Value" DataFormatString="{0:n0}">
                <HeaderStyle HorizontalAlign="Right" CssClass="alright" />
            <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>


            <asp:BoundField DataField="GAINLOSSMTM" HeaderText="Unrealized Gain/Loss" DataFormatString="{0:n0}">
                <HeaderStyle HorizontalAlign="Right" CssClass="alright" />
            <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
             


        </Columns>
                      </asp:GridView>

                                </div></div>
                            </div>
                        </div>
     </div>
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
    <script src="NewsTicker/glide.js"></script>
    <script src="NewsTicker/script.js"></script>
    <script src="NewsTicker/jquery.easy-ticker.js"></script>
    <script type="text/javascript" src="NewsTicker/marquee/js/marquee.js"></script>
    <script src="js/bootstrap-datepicker.min.js"></script>

    <script  type="text/javascript" >
        var newvar = JSON.parse('<%= balanceArray1 %>');
        var newvar2 = JSON.parse('<%= balanceArray2 %>');
        var newvar3 = JSON.parse('<%= balanceArray3 %>');
        var table2;
   
        var seriesData = [];

        if (newvar.some(value => value !== 0)) {
            seriesData.push({
                name: 'Money Market',
                data: newvar,
                color: '#f9a66f'
            });
        }

        if (newvar2.some(value => value !== 0)) {
            seriesData.push({
                name: 'Equity',
                data: newvar2,
                color: '#144c8d'
            });
        }

        if (newvar3.some(value => value !== 0)) {
            seriesData.push({
                name: 'Debt',
                data: newvar3,
                color: '#c5e7ea'
            });
        }

        //if (newvar.length > 0) {
        //    seriesData.push({
        //        name: 'Money Market',
        //        data: newvar,
        //        color: '#144c8d'
        //    });
        //}

        //if (newvar2.length > 0) {
        //    seriesData.push({
        //        name: 'Equity',
        //        data: newvar2,
        //        color: '#f9a66f'
        //    });
        //}

        //if (newvar3.length > 0) {
        //    seriesData.push({
        //        name: 'Debt',
        //        data: newvar3,
        //        color: '#c5e7ea'
        //    });
        //}

        let obj = <%=barchartdata%>;
        console.log(obj);
        let barchartdata1 = {
            color: obj[0].color,
            data: obj[0].data,
            name: obj[0].name,
        }
        let barchartdata2 = {
            color: obj[0].color,
            data: obj[0].data2,
            name: 'UNITS',
        }


       <%-- Highcharts.chart('piePortAllow', {
            colors: ["#011d5b", "#1c4ec5", "#FDB913", "#f45b5b", "#781D7E", "#54B948", "#7399C6", "#004990", "#F47B20", "#FDB913", "#f45b5b", "#781D7E", "#54B948", "#7399C6", "#004990"],
            chart: {
                type: 'pie'
            },
            title: {
                text: ''
            },
            legend: { enabled: false },
            tooltip: {
                valueSuffix: '%'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    point: {
                        events: {
                            click: function (e) {
                                debugger;
                                table2.columns(0)
                                    .search(this.name)
                                    .draw();

                                                              
                               
                                $.ajax({
                                    type: "POST",
                                    url: "json.aspx?type=" + this.name + "&code=<%=Session["FUNDCODE"] %>",
                                    data: '{type: Baltal}',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (data) {
                                        profilechart.series[0].setData(data);
                                    }
                                });


                                //profilechart.series[0].data[0].update({
                                //    key: e.point.key,
                                //    }, true);
                                ////profilechart.series[0].data[2].options.key = e.point.key;
                                //profilechart.reflow();
                                //profilechart.redraw();
                            }
                        }
                    },
                    dataLabels: {
                        enabled: true,
                        format: '{point.name}: {y} %'
                    },
                    showInLegend: true
                }
            },
            series: [{
                name: 'Allocation',
                colorByPoint: true,
                innerSize: '65%',
                data: <% = Chartdata%>,
            }],
            credits: {
                enabled: false
            },
        });
        Highcharts.setOptions({
            lang: {
                thousandsSep: ','
            }
        });--%>




        var profilechart = Highcharts.chart('profiledetail', {
            colors: ["#00008b", "#4682b4", "#0000ff"],
            chart: {
                type: 'pie'
            },
            title: {
                text: ''
            },
            tooltip: {
                valueSuffix: ''//'%'
            },
            xAxis: {
                //categories: ["CURRENT MONTH", "CURRENT YTD", "FISCAL YEAR", "INCEPTION"],
                labels: {
                    style: {
                        color: 'blue',
                        fontSize: '7px',
                        rotation: 90
                    }
                }
            },

            legend: {
                labels: {
                    style: {
                        color: 'blue',
                        fontSize: '7px',
                        rotation: 90
                    }
                }
            },
            legend: { enabled: false },
            tooltip: {
                pointFormatter: function () {
                    return '<span style="color:' + this.color + '">' + this.series.name + '</span>: <b>' +
                        Highcharts.numberFormat(this.y, 0, '.', ',') + '</b><br/>';
                },
                valueSuffix: '' // Remove '%' if not needed
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',

                    dataLabels: {
                        enabled: true,

                        //format: '{point.name}: {y:,.1f}'
                        format: '{point.name}'
                    },
                    showInLegend: true
                }
            },
            series: [{
                name: 'Amount',
                colorByPoint: true,
                innerSize: '65%',
                data: <% =profiledetaildata%>,
          }],
          credits: {
              enabled: false
          },
      });




        //column
     Highcharts.chart('piePortret', {
            chart: {
                type: 'column',
                zoomType: 'y',
                
                //backgroundColor:"#FBFAE4"
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: <%=BarChartLable%>,            
         
                labels: {
                    style: {
                        color: 'black',
                        fontSize: '7px',
                        rotation: 90
                    }
                }
            },
            legend: { enabled: false,
		      padding: 0,
        	      itemMarginTop: 0,
        	      itemMarginBottom: 0,
		      itemStyle: {
                    	fontWeight: 'lighter',
                    	fontSize: '7pt'
                	}
	   
	    },
            yAxis: {
                /*min: -100,*/
                title: {
                    text: ''
                }
            },
            tooltip: {
                headerFormat: '<span style="font-size:10px"><b>{point.key}</b></span><table>',
                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b> {point.y}</b></td></tr>',
                footerFormat: '</table>',
                shared: true,
                useHTML: true
            },
            plotOptions: {
                column: {
                    pointPadding: 0.2,
                    borderWidth: 0
                },

            },
         series: [barchartdata1, barchartdata2],
         
            
            credits: {
                enabled: false
            },
        });

        Highcharts.chart('piePortret', {
            chart: {
                type: 'column'
            },
            title: {
                text: ''
            },
            xAxis: {
                categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Percentage'
                }
            },
            tooltip: {
                pointFormatter: function () {
                    return '<span style="color:' + this.color + '">' + this.series.name + '</span>: <b>' +
                        Highcharts.numberFormat(this.y, 0, '.', ',') + '</b> (' +
                        Highcharts.numberFormat(this.percentage, 1) + '%)<br/>';
                },
                shared: true
            },
            plotOptions: {
                column: {
                    stacking: 'percent'
                }
            },
            series: seriesData,
            //series: [{
            //    name: 'Money Market',
            //    data: newvar,
            //    color: '#4682b4'
                
            //}, {
            //    name: 'Equity',
            //    data: newvar2,
            //    color: '#00008b'
               
            //}, {
            //    name: 'Debt',
            //    data: newvar3,
            //    color: '#0000ff'
            //    }],
            credits: {
                enabled: false
            },
        });
    
      
       

        $(document).ready(function () {

            let options = {
                autostart: true,
                
                duration: 1000,
                padding: 0,
               
                hover: true,
                velocity: 0.1,
                direction: 'left'
            }

            $('.simple-marquee-container').SimpleMarquee(options);

            //$('.simple-marquee-container').SimpleMarquee();
            $('.demo1').easyTicker({
                direction: 'up',
                interval: 2000,
                visible: 3
            });
            //var url = 'https://mettisglobal.news/feed'; //Data in XML format
            var url = 'https://www.youtube.com/feeds/videos.xml?channel_id=UC7iYwXZeW-BpBhdXzqAz0xw&v=23041999'; //Data in XML format
            $.ajax({
                type: 'GET',
                url: "https://api.rss2json.com/v1/api.json?rss_url=" + url, //For converting default format to JSON format  
                dataType: 'jsonp', //for making cross domain call  
                success: function (data) {
                    debugger;
                    $.each(data.items, function (index) {
                        debugger;
                        /* alert(data[index].url);*/
                        /*alert(data[index].feed);*/
                        //$(".demo1 ul").append("<li><div><span><a href=" + data.items[index].link + ">" + data.items[index].title + "</a></span></div><p>" + data.items[index].description.substr(0, 250) + "</p></li>");
                        $(".demo1 ul").append("<li><div><img src=" + data.items[index].thumbnail+"><span><a target='_blank' href=" + data.items[index].link + ">" + data.items[index].title + "</a></span></div><p>" + data.items[index].description.substr(0, 250) + "</p></li>");
                        //$(".demo1 ul").append("<li><div><iframe src=" + data.items[index].enclosure.link + " controls='true'></iframe><span><a href=" + data.items[index].link + ">" + data.items[index].title + "</a></span></div><p>" + data.items[index].description.substr(0, 250) + "</p></li>");
                        console.log(data.feed.description);
                    });

                }
            });
            var table = $('#main_grdTransSummary').DataTable({
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
            $('#main_grdTransSummary').show();
            table.columns.adjust().draw();


            table2 = $('#main_TransactionView').DataTable({
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
            $('#main_TransactionView').show();
            table2.columns.adjust().draw();
            $(".slidedivaggrement").hide();
            $(".slidedivfmr").hide();
            $(".slidedivaccstat").hide();
            $(".slidedivtransstat").hide();
                        
            $("#main_btnAgreementRpt").click(function (e) {
                

                $(".first").hide();
                $(".slidediv").show(500);
                $(".slidedivaggrement").show();
                e.preventDefault();
            });

            $("#main_btnFMRReport").click(function (e) {
               
                $(".first").hide();
                $(".slidediv").show(500);
                $(".slidedivfmr").show();
                e.preventDefault();
            });


            $("#main_btnTaxStat").click(function (e) {
               
                $(".first").hide();
                $(".slidediv").show(500);
                $(".slidedivaccstat").show();
                e.preventDefault();
            });


            $("#main_btnTransStatement").click(function (e) {
                
                $(".first").hide();
                $(".slidediv").show(500);
                $(".slidedivtransstat").show();
                e.preventDefault();
            });

            $(".backbutton").click(function (e) {

                $(".first").show();
                $(".slidediv").hide();
                $(".slidedivtransstat").hide();
                $(".slidedivaccstat").hide();
                $(".slidedivfmr").hide();
                $(".slidedivaggrement").hide();
                e.preventDefault();
            });
            $('.date-withicon').attr('readonly', true);
            $('.date-withicon').datepicker({
                format: 'dd/mm/yyyy',
		autoclose: 'true'
            });

            //$(".spinner-button").click(function () {
            //    // disable button
            //    $(this).prop("disabled", true);
            //    // add spinner to button
            //    $(this).html('<i class="fa fa-circle-o-notch fa-spin"></i> loading...');
            //});

        });


        //
        $(window).bind("load", function () {
            $(".loading-gif.loader").hide();
        });

    </script>
    
</asp:Content>




