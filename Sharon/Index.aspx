<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Sharon.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <title>Sharon</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="icon" href="favicon2.ico" />
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" media="all"/>
    <link href="css/flexslider.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/themify-icons.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/theme-nearblack.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/hover-min.css" rel="stylesheet" media="all"/>
    <link href="css/custom.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/jquery-ui.css" rel="stylesheet" media="all"/>
   
</head>
<body class="scroll-assist">
    <form id="form1" runat="server">
  	<div class="nav-container">
        <nav>
	        <div class="nav-bar">
                      <div class="module-group right">
		               <div class="module left">
                             <ul class="menu">
		                         <li><a href="https://confluence.internal.macquarie.com/pages/viewpage.action?pageId=324110661" target="_blank">Contacts</a></li>
                                 <li><a href="https://confluence.internal.macquarie.com/display/W/A.+QUICK+Incident+Model+Locator" target="_blank">Incident Models</a></li>
                                 <li><a href="https://confluence.internal.macquarie.com/display/AIOSM/IM+Useful+Links" target="_blank">Useful IM Links</a></li>
                                 </ul>
		                </div>
		                <div class="module widget-handle offscreen-toggle right">
		                    <i class="ti-info-alt"></i>
		                </div>
                          </div>
		            </div>
		        <div class="offscreen-container bg-dark text-center">
		                <div class="close-nav">
		                    <a href="#">
		                        <i class="ti-close"></i>
		                    </a>
		                </div>
		                <div class="v-align-transform text-center">
		                    <a href="#"><img alt="Logo" class="mb40 mb-xs-24 hvr-pop image-xs" src="img/logo-nav.png"/></a>
		                    <ul class="mb40 mb-xs-24">
		                        <li class="fade-on-hover"><a href="#"><h5 class=" mb8">Simple Holistic Asset Record Online Navigator</h5></a></li>
		                    </ul>
		                    <p class="fade-half">
		                        Version 1.5 - Beryl<br/>
                                Last Updated by <a href="https://staffdirectory.pc.internal.macquarie.com/UserProfile.aspx?u=kolanday" target="_blank">Kevin Olanday</a><br />
                                For questions and concerns, please contact <a href="mailto:sharon.fajardo@macquarie.com">Sharon Fajardo</a> or the <a href="mailto:millene.dy@macquarie.com;kevin.olanday@macquarie.com;pierre.uy@macquarie.com;wesley.chin@macquarie.com">Automation Taskforce ICE</a> team.

		                    </p>


		                </div>
		            </div>
		</nav>
    </div>
		
	<div class="main-container">
        <section >
		        <div class="container">
		            <div class="row mb24 mb-xs-0">
		                <div class="col-sm-10 col-sm-offset-1 text-center">
                                <a>  <img alt="Sharon" class="hvr-wobble-vertical" src="img/splash2.png" /></a>
		                </div>
		            </div>

		            <div class="row mb24 mb-xs-24">
		                <div class="col-sm-12 text-center">
        
		                    <fieldset><input type="search" id="searchQuery"  onkeydown="if (event.keyCode == 13) document.getElementById('searchButton').click()" runat="server"/>
                                <button id="searchButton" onserverclick="searchButton_OnClick" type="button" runat="server"><i class="fa fa-search" ></i></button></fieldset>

		                </div>
		            </div>
		             <div class="row">
		                <div class="col-md-8 col-md-offset-2 col-sm-10 col-sm-offset-1">
		                    <div class="text-slider slider-paging-controls text-center relative">
		                        <ul class="slides">
		                            <li>
		                                <p class="lead">Search any Remedy asset in seconds.</p>

		                            </li>
		                            <li>
		                                <p class="lead">Sharon's data-on-demand search engine means that information is always updated.</p>

		                            </li>
		                            <li>
		                                <p class="lead">Sharon simplifies investigations by displaying recent changes and incidents related to the asset.</p>
		                            </li>
                                    <li>
		                                <p class="lead">Not sure what the exact name of the asset is? Sharon has an auto suggest feature to help you identify it.</p>
		                            </li>
                                      <li>
		                                <p class="lead">Sharon works on mobile devices.</p>
		                            </li>
		                        </ul>
		                    </div>
		                </div>
		            </div>
                 
    	    
		        </div>
		        
		    </section>
	</div>

    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/flexslider.min.js"></script>
    <script src="js/parallax.js"></script>
    <script src="js/scripts.js"></script>		
           <script src="js/jquery-ui.min.js" type="text/javascript"></script>
        <script type="text/javascript">
                $(function () {
                    $("[id$=searchQuery]").autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                url: '<%=ResolveUrl("Results.aspx/GetAssets") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item,
                                    val: item
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=hfCustomerId]").val(i.item.val);

                    setTimeout(function () {
                        $("#searchButton").click();
                    }, 0);
                },
                minLength: 1
            });
        });
    </script>
    </form>

</body>
</html>
