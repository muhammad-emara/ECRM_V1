<%@ Page Title="" StylesheetTheme="ecrm" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="ECRM.aspx.cs" Inherits="ECRM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {




            $(".customer").hide();
            $(".class").hide();
            $(".team").hide();
            $(".partner").hide();
            $(".admin").hide();
            $(".date").hide();
            $(".year").hide();
            $(".market").hide();
            $(".parent").hide();

            $(".openclass").click(function () {



                if ($('.filteroption').is(':visible')) {
                    // it's visible, do something
                    $(".class").slideToggle(200);
                }
                else {
                    // it's not visible so do something else
                    $(".filteroption").slideToggle(200);
                    $(".class").slideToggle(200);
                }



                return false;
            });
            $(".opencustomer").click(function () {

                if ($('.filteroption').is(':visible')) {
                    // it's visible, do something
                    $(".customer").slideToggle(200);
                }
                else {
                    // it's not visible so do something else
                    $(".filteroption").slideToggle(200);
                    $(".customer").slideToggle(200);
                }




                return false;
            });
            $(".openteam").click(function () {

                if ($('.filteroption').is(':visible')) {
                    // it's visible, do something
                    $(".team").slideToggle(200);
                }
                else {
                    // it's not visible so do something else
                    $(".filteroption").slideToggle(200);
                    $(".team").slideToggle(200);
                }


                return false;
            });
            $(".openpartner").click(function () {

                if ($('.filteroption').is(':visible')) {
                    // it's visible, do something
                    $(".partner").slideToggle(200);
                }
                else {
                    // it's not visible so do something else
                    $(".filteroption").slideToggle(200);
                    $(".partner").slideToggle(200);
                }




                return false;
            });

            $(".openadmin").click(function () {

                if ($('.filteroption').is(':visible')) {
                    // it's visible, do something
                    $(".admin").slideToggle(200);
                }
                else {
                    // it's not visible so do something else
                    $(".filteroption").slideToggle(200);
                    $(".admin").slideToggle(200);
                }




                return false;
            });
            $(".opendate").click(function () {

                if ($('.filteroption').is(':visible')) {
                    // it's visible, do something
                    $(".date").slideToggle(200);
                }
                else {
                    // it's not visible so do something else
                    $(".filteroption").slideToggle(200);
                    $(".date").slideToggle(200);
                }



                return false;
            });


            $(".openyear").click(function () {

                if ($('.filteroption').is(':visible')) {
                    // it's visible, do something
                    $(".year").slideToggle(200);
                }
                else {
                    // it's not visible so do something else
                    $(".filteroption").slideToggle(200);
                    $(".year").slideToggle(200);
                }



                return false;


            });




            $(".openmarket").click(function () {

                if ($('.filteroption').is(':visible')) {
                    // it's visible, do something
                    $(".market").slideToggle(200);
                }
                else {
                    // it's not visible so do something else
                    $(".filteroption").slideToggle(200);
                    $(".market").slideToggle(200);
                }



                return false;


            });



            $(".openparent").click(function () {

                if ($('.filteroption').is(':visible')) {
                    // it's visible, do something
                    $(".parent").slideToggle(200);
                }
                else {
                    // it's not visible so do something else
                    $(".filteroption").slideToggle(200);
                    $(".parent").slideToggle(200);
                }



                return false;


            });



            $(".showfilters").click(function () {

                $(".filtersselect").fadeToggle(300);
            });

        });

    </script>
    <script type="text/javascript">


        $(document).ready(function () {


            jQuery.fn.dataTableExt.aTypes.unshift(
        function (sData) {
            var sValidChars = "0123456789-,";
            var Char;
            var bDecimal = false;

            /* Check the numeric part */
            for (i = 0; i < sData.length; i++) {
                Char = sData.charAt(i);
                if (sValidChars.indexOf(Char) == -1) {
                    return null;
                }

                /* Only allowed one decimal place... */
                if (Char == ",") {
                    if (bDecimal) {
                        return null;
                    }
                    bDecimal = true;
                }
            }

            return 'numeric-comma';
        }
    );

            jQuery.fn.dataTableExt.oSort['numeric-comma-asc'] = function (a, b) {
                var x = (a == "-") ? 0 : a.replace(/,/, ".");
                var y = (b == "-") ? 0 : b.replace(/,/, ".");
                x = parseFloat(x);
                y = parseFloat(y);
                return ((x < y) ? -1 : ((x > y) ? 1 : 0));
            };

            jQuery.fn.dataTableExt.oSort['numeric-comma-desc'] = function (a, b) {
                var x = (a == "-") ? 0 : a.replace(/,/, ".");
                var y = (b == "-") ? 0 : b.replace(/,/, ".");
                x = parseFloat(x);
                y = parseFloat(y);
                return ((x < y) ? 1 : ((x > y) ? -1 : 0));
            };

            //for asp.net
            //$('#example').GridviewFix().dataTable({
            //    "bJQueryUI": true,
            //    "sPaginationType": "full_numbers"
            //});

            $('table').GridviewFix().dataTable({
                "bJQueryUI": true,
                "bLengthChange": false,
                "bSort": false,
                "sDom": '<"toolbar"><"wrapper"flipt>'
            });
            $("div.toolbar").html('<b style="width: 0%; float: left;margin-top: 7px;padding-left: 10px; color:#fff;"></b>');







            // toogle search input 
            //            $('.searchinsidegrid').click(function () {


            //                var xx = $(this).closest('div[id*="wrapper"]');

            //                $(xx).find(".dataTables_filter").fadeToggle(500)
            //                //$(xd).children().find(".dataTables_filter").fadeToggle(500);
            //                return true;
            //            });



        });
</script>
    <script type="text/javascript">
        function grids() {

            $('#ContentPlaceHolder1_GridView1').GridviewFix().dataTable({
            //$($("[id*='GridView']")).GridviewFix().dataTable({
                "bJQueryUI": true,
                "bLengthChange": false,
                "sDom": '<"toolbar"><"wrapper"flipt>'
            });

            $("div.toolbar").html('<b style="width: 0%; float: left;margin-top: 7px;padding-left: 10px; color:#fff;"></b>');


            var config = {
                '.chzn-select': {},
                '.chzn-select-deselect': { allow_single_deselect: true },
                '.chzn-select-no-single': { disable_search_threshold: 10 },
                '.chzn-select-no-results': { no_results_text: 'Oops, nothing found!' },
                '.chzn-select-width': { width: "95%" }
            }
            for (var selector in config) {
                $(selector).chosen(config[selector]);
            }





        }

      


  </script>
        <script type="text/javascript">
            function selectorstyle() {
                var config = {
                    '.chzn-select': {},
                    '.chzn-select-deselect': { allow_single_deselect: true },
                    '.chzn-select-no-single': { disable_search_threshold: 10 },
                    '.chzn-select-no-results': { no_results_text: 'Oops, nothing found!' },
                    '.chzn-select-width': { width: "95%" }
                }
                for (var selector in config) {
                    $(selector).chosen(config[selector]);
                }

            }
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="leftblock div30 top20 filtersselect">
                
                <div class="accordionblock">
                    <div class="accordionheader"><label>filters</label>
                    <img src="images/icons/menu.png" class="imgonrighthide" alt="Down" /></div>
                    <div class="accordiondetails"> 
                    
                    		<ul id="filtersoptions" class="jcarousel-skin-tango">
          
            <li><a href="#" class="openclass"><img src="images/carsoul/class.png" alt="" /></a></li>
            <li><a href="#" class="opencustomer"><img src="images/carsoul/customer.png" alt="" /></a></li>
            <li><a href="#" class="openteam"><img src="images/carsoul/team.png" alt="" /></a></li>
            <li><a href="#" class="openpartner"><img src="images/carsoul/partenar.png" alt="" /></a></li>
            <li><a href="#" class="openadmin"><img src="images/carsoul/admin.png" alt="" /></a></li>
             <li><a href="#" class="opendate"><img src="images/carsoul/date.png" alt="" /></a></li>
            <li><a href="#" class="openyear"><img src="images/carsoul/date.png" alt="" /></a></li>
            <li><a href="#" class="openmarket"><img src="images/carsoul/date.png" alt="" /></a></li>
            <li><a href="#" class="openparent"><img src="images/carsoul/date.png" alt="" /></a></li>



		</ul>

                       

                    </div>
                
                </div>
                    
            </div>
        

        <div class="leftblock div75 top20  filtersselect">
                
                <div class="accordionblock ">
                    <div class="accordionheader"><label>Filters Options</label>
                    <img src="images/icons/menu.png" class="imgonrighthide" alt="Down" /></div>
                    <div class="accordiondetails filteroption" style="min-height: 77px;"> 
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                    	 <div class="classselect class">
                            <h2 class="blockheaderofcursor">
                                Class Select
                            </h2>

                              <asp:Label ID="Label1" runat="server" Text="Class : "></asp:Label>
                                <asp:ListBox OnSelectedIndexChanged="Class12"  ID="lstItemsWithChosen" runat="server" SelectionMode="Multiple" Width="250" data-placeholder="Please select option..." class="chzn-select" OnTextChanged="lstItemsWithChosen_TextChanged" AutoPostBack="true" >
                                 <asp:ListItem Text="Debit Memo"></asp:ListItem>
                                 <asp:ListItem Text="Invoice"></asp:ListItem>
                              </asp:ListBox>
                             
                        </div>
                       


                                                <div class="classselect customer">
                            <h2 class="blockheaderofcursor">
                                Customer Select
                            </h2>


                            <asp:Label ID="Label2" runat="server" Text="Customer : "></asp:Label>
                              <asp:ListBox OnSelectedIndexChanged="Class12"  ID="ListBox1" runat="server" SelectionMode="Multiple" Width="250" data-placeholder="Please select option..." class="chzn-select" OnTextChanged="lstItemsWithChosen_TextChanged" AutoPostBack="true" >
                              </asp:ListBox>
                        </div>
                                

                                                                        <div class="classselect team">
                            <h2 class="blockheaderofcursor">
                                Team Select
                            </h2>


                            <asp:Label ID="Label3" runat="server" Text="Team : "></asp:Label>
                              <asp:ListBox OnSelectedIndexChanged="Class12"  ID="ListBox2" runat="server" SelectionMode="Multiple" Width="250" data-placeholder="Please select option..." class="chzn-select" OnTextChanged="lstItemsWithChosen_TextChanged" AutoPostBack="true" >
                              </asp:ListBox>
                        </div>




                        

                                                                        <div class="classselect partner">
                            <h2 class="blockheaderofcursor">
                                Partner Select
                            </h2>


                            <asp:Label ID="Label4" runat="server" Text="Partner : "></asp:Label>
                              <asp:ListBox OnSelectedIndexChanged="Class12"  ID="ListBox3" runat="server" SelectionMode="Multiple" Width="250" data-placeholder="Please select option..." class="chzn-select" OnTextChanged="lstItemsWithChosen_TextChanged" AutoPostBack="true" >
                              </asp:ListBox>
                        </div>




                        

                                                                        <div class="classselect admin">
                            <h2 class="blockheaderofcursor">
                                Admin Select
                            </h2>


                            <asp:Label ID="Label5" runat="server" Text="Admin : "></asp:Label>
                              <asp:ListBox OnSelectedIndexChanged="Class12"  ID="ListBox4" runat="server" SelectionMode="Multiple" Width="250" data-placeholder="Please select option..." class="chzn-select" OnTextChanged="lstItemsWithChosen_TextChanged" AutoPostBack="true" >
                              </asp:ListBox>
                        </div>



                        

                                                                        <div class="classselect date">
                            <h2 class="blockheaderofcursor">
                                Month Select
                            </h2>


                            <asp:Label ID="Label6" runat="server" Text="Month : "></asp:Label>
                              <asp:ListBox OnSelectedIndexChanged="Class12"  ID="ListBox5" runat="server" SelectionMode="Multiple" Width="250" data-placeholder="Please select option..." class="chzn-select" OnTextChanged="lstItemsWithChosen_TextChanged" AutoPostBack="true" >
                              </asp:ListBox>
                                     
                        </div>




                                                                                                        <div class="classselect year">
                            <h2 class="blockheaderofcursor">
                                Year Select
                            </h2>


                            <asp:Label ID="Label7" runat="server" Text="Year : "></asp:Label>
                             <asp:ListBox OnSelectedIndexChanged="Class12"  ID="ListBox6" runat="server" SelectionMode="Multiple" Width="250" data-placeholder="Please select option..." class="chzn-select" OnTextChanged="lstItemsWithChosen_TextChanged" AutoPostBack="true" >
                              </asp:ListBox>
                        </div>




                                                                                                        <div class="classselect market">
                            <h2 class="blockheaderofcursor">
                                Market Segment Select
                            </h2>


                            <asp:Label ID="Label8" runat="server" Text="Market : "></asp:Label>
                            <asp:ListBox OnSelectedIndexChanged="Class12"  ID="ListBox7" runat="server" SelectionMode="Multiple" Width="250" data-placeholder="Please select option..." class="chzn-select" OnTextChanged="lstItemsWithChosen_TextChanged" AutoPostBack="true" >
                              </asp:ListBox>
                        </div>




                                                                                                        <div class="classselect parent">
                            <h2 class="blockheaderofcursor">
                                Parent Select
                            </h2>


                            <asp:Label ID="Label9" runat="server" Text="Parent : "></asp:Label>
                             <asp:ListBox OnSelectedIndexChanged="Class12"  ID="ListBox8" runat="server" SelectionMode="Multiple" Width="250" data-placeholder="Please select option..." class="chzn-select" OnTextChanged="lstItemsWithChosen_TextChanged" AutoPostBack="true" >
                              </asp:ListBox>
                        </div>


                                 </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                
                </div>
                    
            </div>


            <div class="leftblock div80 top20">
                
                <div class="accordionblock">
                    <div class="accordionheader"><label>Team Summary</label>
                    <img src="images/icons/menu.png" class="imgonrighthide" alt="Down" />
                    </div>
                    <div class="accordiondetails" style="padding-bottom:0;"> 
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                         <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Enabled="false" Interval="200">
                        </asp:Timer>

                       <asp:GridView ID="GridView1" DataSourceID="" runat="server" AutoGenerateColumns="true" EnableViewState="true" />



                    </div>
                </div>



                                <div class="accordionblock">
                    <div class="accordionheader"><label>Customer Summary</label>
                    <img src="images/icons/menu.png" class="imgonrighthide" alt="Down" />
                    </div>
                    <div class="accordiondetails" style="padding-bottom:0;"> 


                        <asp:Timer ID="Timer2" runat="server" OnTick="Timer1_Tick" Enabled="false" Interval="200">
                        </asp:Timer>
                       <asp:GridView ID="GridView2" DataSourceID="" runat="server" AutoGenerateColumns="true" EnableViewState="true" />


   

                    </div>
                </div>





                </div>



              <div class="leftblock div38 top20">
                
                <div class="accordionblock">
                    <div class="accordionheader"><label>Partner</label>
                    <img src="images/icons/menu.png" class="imgonrighthide" alt="Down" /></div>
                    <div class="accordiondetails" style="padding-bottom:0;"> 
                    

                        <asp:Timer ID="Timer3" runat="server" OnTick="Timer1_Tick" Enabled="false" Interval="200">
                        </asp:Timer>
                       <asp:GridView ID="GridView3" DataSourceID="" runat="server" AutoGenerateColumns="true" EnableViewState="true" />



                    </div>
                </div>
                </div>
    

              <div class="leftblock div38 top20">
                
                <div class="accordionblock">
                    <div class="accordionheader"><label>Parent Name</label>
                    <img src="images/icons/menu.png" class="imgonrighthide" alt="Down" /></div>
                    <div class="accordiondetails" style="padding-bottom:0;"> 
                    

                        <asp:Timer ID="Timer4" runat="server" OnTick="Timer1_Tick" Enabled="false" Interval="200">
                        </asp:Timer>
                       <asp:GridView ID="GridView4" DataSourceID="" runat="server" AutoGenerateColumns="true" EnableViewState="true" />


  

                    </div>
                </div>
                </div>


                  <div class="leftblock div38 top20">
                
                <div class="accordionblock">
                    <div class="accordionheader"><label>Year</label>
                    <img src="images/icons/menu.png" class="imgonrighthide" alt="Down" /></div>
                    <div class="accordiondetails" style="padding-bottom:0;"> 
                    

                        <asp:Timer ID="Timer5" runat="server" OnTick="Timer1_Tick" Enabled="false" Interval="200">
                        </asp:Timer>
                       <asp:GridView ID="GridView5" DataSourceID="" runat="server" AutoGenerateColumns="true" EnableViewState="true" />


                    </div>
                </div>
                </div>

                      <div class="leftblock div38 top20">
                
                <div class="accordionblock">
                    <div class="accordionheader"><label>Market Segment</label>
                    <img src="images/icons/menu.png" class="imgonrighthide" alt="Down" /></div>
                    <div class="accordiondetails" style="padding-bottom:0;"> 
                    

                        <asp:Timer ID="Timer6" runat="server" OnTick="Timer1_Tick" Enabled="false" Interval="200">
                        </asp:Timer>
                       <asp:GridView ID="GridView6" DataSourceID="" runat="server" AutoGenerateColumns="true" EnableViewState="true" />



                    </div>
                </div>
                </div>


                     </ContentTemplate>
                 </asp:UpdatePanel>



              
</asp:Content>

