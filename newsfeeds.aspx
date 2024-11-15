<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newsfeeds.aspx.cs" Inherits="newsfeeds" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>

        .demof {
              border: 1px solid #ccc;
              margin: 25px 0;
          }

              .demof ul {
                  padding: 0;
                  list-style: none;
              }

              .demof li {
                  padding: 20px;
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
                  font-family: Arial, sans-serif;
                  font-size: 20px;
                  font-weight: bold;
                  color: #06f;
              }

              .demof p {
                  margin: 15px 0 0;
                  font-size: 14px;
              }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="d-flex justify-content-between align-items-center breaking-news bg-white">
                <div class="d-flex flex-row flex-grow-1 flex-fill justify-content-center bg-danger py-2 text-white px-1 news"><span class="d-flex align-items-center">&nbsp;CNN News</span></div>
                <marquee class="news-scroll" behavior="scroll" direction="left" onmouseover="this.stop();" onmouseout="this.start();"> <a href="#">Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. </a> <span class="dot"></span> <a href="#">Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut </a> <span class="dot"></span> <a href="#">Duis aute irure dolor in reprehenderit in voluptate velit esse </a></marquee>
            </div>
            <div id="CsharpBlog" runat ="server" > </div>   
             <div id="rss-default"> </div>   
            <div class="demo1 demof">
          <ul>
             
          </ul>
      </div>
        </div>
    <script src="vendors/base/vendor.bundle.base.js"></script>
    <script src="NewsTicker/jquery.easy-ticker.js"></script>
   
    <script type="text/javascript">
        $(document).ready(function () {
            $('.demo1').easyTicker({
                direction: 'up',
                interval: 2000,
               
                visible: 3
            });
            var url = 'https://mettisglobal.news/feed'; //Data in XML format
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
                        $(".demo1 ul").append("<li><img src=" + data.items[index].enclosure.link + " alt='News Image' /><a href=" + data.items[index].link + ">" + data.items[index].title + "</a><p>" + data.items[index].description.substr(0,250) + "</p></li>");
                        console.log(data.feed.description);
                    });
                                                                               
                }
            });
        });
    </script>

    <script>
        var prtcl = document.location.protocol;
        var url = prtcl + "//eoceanwab.com/digitalconnectwidgets/eoceanWidget.js";
        var s = document.createElement("script");
        s.type = "text/javascript";
        s.async = true;
        s.src = url;
        var options = {
            "enabled": true,
            "chatButtonSetting": {
                "backgroundColor": "#363a77",
                "position": "right",
                "iconColor": "#ffffff"
            },
            "brandSetting": {
                "org": "dftl",
                "brandImg": prtcl + "//eoceanwab.com/digitalconnectwidgets/logo/dftl.png",
                "welcomeText": "Hi! How can we help you?",
                "backgroundColor": "#0089ff",
                "brandQr": prtcl + "//eoceanwab.com/digitalconnectwidgets/qr/QR_dftl.png",
                "iconImgUrl": "svg5",
                "phoneNumber": "9221111338786",
                "fbMessenger": "",
                "title": "Contact Us"

            }
        };
        s.onload = function () {
            CreateWhatsappChatWidget(options);
        };
        var x = document.getElementsByTagName("script")[0];
        x.parentNode.insertBefore(s, x);
    </script>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    </form>
    </body>
    
</html>
