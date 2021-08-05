<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="LiteResults.aspx.cs" Inherits="Sharon.LiteResults" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sharon - Asset Found</title>
    <link rel="icon" href="favicon.ico" />
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" media="all"/>
    <link href="css/flexslider.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/themify-icons.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/components.min.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/theme-nearblack.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/hover-min.css" rel="stylesheet" media="all"/>

    <link href="css/custom.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/jquery-ui.css" rel="stylesheet" media="all"/>

</head>
<body>
    <form id="form1" runat="server">
      	<div class="nav-container">
         <nav>
	        <div class="nav-bar resultsnav">
		                <div class="module left">
		                <a href="Lite.aspx">
		                    <img class="logo logo-dark searchBar" alt="Foundry" src="img/logo-light2.png"/>
		                </a>
                             <fieldset><input type="search" id="searchQuery" class="navSearchQuery" onkeydown="if (event.keyCode == 13) document.getElementById('searchButton').click()" runat="server"/>
                                 <asp:HiddenField ID="hfCustomerId" runat="server" />
                                <button id="searchButton" class="navSearchButton" type="button" onserverclick="searchButton_OnClick" runat="server"><i class="fa fa-nav fa-search" ></i></button></fieldset>
	
                              	            </div>
		                <div class="module widget-handle offscreen-toggle right">
		                    <i class="ti-info-alt"></i>
		                </div>

		            </div>
		        <div class="offscreen-container bg-dark text-center">
		                <div class="close-nav">
		                    <a href="#">
		                        <i class="ti-close"></i>
		                    </a>
		                </div>
		                <div class="v-align-transform text-center">
		                    <a href="#"><img alt="Logo" class="mb40 mb-xs-24 hvr-pop" src="img/pinup.png"/></a>
		                    <ul class="mb40 mb-xs-24">
		                        <li class="fade-on-hover"><a href="#"><h5 class=" mb8">Simple Holistic Asset Record Online Navigator</h5></a></li>
		                    </ul>
		                    <p class="fade-half">
		                        Version 0.9 - Ruby<br/>

		                    </p>
		                    <ul class="list-inline social-list">
		                        <li>
		                            <a href="#">
		                                <i class="ti-twitter-alt"></i>
		                            </a>
		                        </li>
		                        <li>
		                            <a href="#">
		                                <i class="ti-dribbble"></i>
		                            </a>
		                        </li>
		                        <li>
		                            <a href="#">
		                                <i class="ti-vimeo-alt"></i>
		                            </a>
		                        </li>
		                    </ul>
		                </div>
		            </div>
		    </nav>
        </div>

        <div class="main-container">
            <section class="resultsSection">
				<div class="container">
					<div class="row v-align-children">
                        <asp:Panel ID="resultsPanel" runat="server">
						    <div class="col-sm-9 mb-xs-24">
                           
                                    <div class="portlet box red">
                                       <div class="portlet-title">
                                                                <div class="caption">
                                                                    <i class="fa fa-info-circle"></i>Asset Details</div>
                                                                <ul class="nav nav-tabs">
                                                                    <li id="tab_people" runat="server">
                                                                        <a href="#portlet_tab_2" data-toggle="tab"> People </a>
                                                                    </li>
                                                                    <li id="tab_overview" runat="server" class="active">
                                                                        <a href="#portlet_tab_1" data-toggle="tab"> Overview </a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                       <div class="portlet-body">
                                                                <div class="tab-content">
                                                                    <div class="tab-pane active" id="portlet_tab_1">
                                                                        <table class="zebra">
                                                                        <thead>

                                                                        </thead>
 
                                                                        <tr>   
                                                                            <td>Name</td>
                                                                            <td><asp:Label ID="label_assetName" runat="server"></asp:Label> <a title="Check Remedy Record" id="remedyLink" href="Index.aspx" target="_blank" runat="server"><img src="img\remedy.png" /></a></td>
                                                                        </tr>     
                                                                        <tr>   
                                                                            <td>Asset ID</td>
                                                                            <td><asp:Label ID="label_assetID" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Category</td>
                                                                            <td><asp:Label ID="label_category" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Item</td>
                                                                            <td><asp:Label ID="label_item" runat="server"></asp:Label></td>
                                                                        </tr>   
                                                                        <tr>
                                                                            <td>Type</td>
                                                                            <td><asp:Label ID="label_type" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Serial</td>
                                                                            <td><asp:Label ID="label_serial" runat="server"></asp:Label></td>
                                                                        </tr>                                            
                                                                        <tr>
                                                                            <td colspan="2"><hr class="divider"/></td>
                                                                        </tr>
                                                                        <tr>    
                                                                            <td>Status</td>
                                                                            <td><asp:Label ID="label_status" runat="server"></asp:Label></td>

                                                                        </tr>
                                                                        <tr>    
                                                                            <td>Role</td>
                                                                            <td><asp:Label ID="label_role" runat="server"></asp:Label></td>
                                                                        </tr>  
                                                                        <tr>    
                                                                            <td>Support Coverage</td>
                                                                            <td><asp:Label ID="label_supported" runat="server"></asp:Label></td>
                                                                        </tr>   
                                                                        <tr>
                                                                            <td>Impact</td>
                                                                            <td><asp:Label ID="label_impact" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Urgency</td>
                                                                            <td><asp:Label ID="label_urgency" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2"><hr class="divider"/></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Region</td>
                                                                            <td><asp:Label ID="label_region" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Site</td>
                                                                            <td><asp:Label ID="label_site" runat="server"></asp:Label></td>
                                                                        </tr>    
                                                                        <tr>
                                                                            <td>Address</td>
                                                                            <td><asp:Label ID="label_address" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Cabinet Number</td>
                                                                            <td><asp:Label ID="label_cabinetNumber" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2"><hr class="divider"/></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Company</td>
                                                                            <td><asp:Label ID="label_company" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Organisation</td>
                                                                            <td><asp:Label ID="label_organisation" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Department</td>
                                                                            <td><asp:Label ID="label_department" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Cost Centre</td>
                                                                            <td><asp:Label ID="label_costcentre" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td colspan="2"><hr class="divider"/></td>
                                                                        </tr>                   
                                                                        <tr>   
                                                                            <td>Description</td>
                                                                            <td><asp:Label ID="label_ciDescription" runat="server"></asp:Label></td>
                                                                        </tr>        
                                                                        <tr>    
                                                                            <td>Additional Information</td>
                                                                            <td><asp:Label ID="label_additionalInformation" runat="server"></asp:Label></td>

                                                                        </tr>
                                                                        <tr>    
                                                                            <td>Business Description</td>
                                                                            <td><asp:Label ID="label_businessDescription" runat="server"></asp:Label></td>
                                                                        </tr>    
                                                                    </table>
     
                                                                    </div>
                                                                    <div class="tab-pane" id="portlet_tab_2">
                                                                        <asp:PlaceHolder ID = "table_people" runat="server" />
                                                                    </div>
                                                   
                                                </div>
                                                                  
                                                                </div>
                                      </div>
       						  
						    
						    </div>
                            <div class="col-sm-3 col-sm-6 text-center">
                             <asp:Image alt="Asset" ID="assetType" runat="server" />
					    </div>
                        </asp:Panel>
                        
                        <asp:Panel ID="noresultsPanel" Visible="false" runat="server">

                        </asp:Panel>
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
