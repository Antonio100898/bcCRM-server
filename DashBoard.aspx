<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="DashBoard" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://code.highcharts.com/highcharts.js"></script>
    <script type="text/javascript" src="https://code.highcharts.com/modules/exporting.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />
    <!-- Optional theme -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous" />


    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>

    <link rel="stylesheet" type="text/css" href="Scripts/jquery.datetimepicker.css" />
    <script src="Scripts/build/jquery.datetimepicker.full.js"></script>


    <!-- Data Table -->
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/responsive/2.2.0/css/responsive.bootstrap.min.css" />


    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js">
    </script>
    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap.min.js">
    </script>
    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/responsive/2.2.0/js/dataTables.responsive.min.js">
    </script>
    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/responsive/2.2.0/js/responsive.bootstrap.min.js">
    </script>



    <style>
        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('Pics/batman.png') 50% 50% no-repeat rgb(249,249,249);
            opacity: .8;
        }
    </style>
</head>
<body>
    <div class="loader" style="display: none"></div>

    <div class="alert-info" style="width: 100%">

        <nav class="navbar navbar-default navbar-right alert-info col-lg-12" role="navigation"
            style="border: 0; border-top: 1px solid #777777; border-bottom: 1px solid #777777; background-color: #ffffff;">
            <div class="container-fluid">

                <!-- Brand and toggle get grouped for better mobile display -->
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                </div>
                <%--<div class="navbar-header navbar-right hidden-xs hidden-sm">
                        <a class="navbar-brand" href="#">
                            <asp:Label ID="lblWorkerName" runat="server" Text="שם עובד" Font-Bold="true" /></a>
                    </div>--%>
                <!-- Collect the nav links, forms, and other content for toggling -->
                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                    <ul class="nav navbar-nav navbar-right">
                        <%--class="active" --%>
                        <li style="border-right: 2px solid silver;">
                            <a href="DashBoard.aspx">DashBoard</a></li>
                        <% if (((Worker)Session["Worker"]).userTypeId == 1)
                            { %>

                        <li style="border-right: 2px solid silver;">
                            <a href="Developer.aspx">פיתוח</a></li>
                        <li style="border-right: 2px solid silver;">
                            <a href="SettingsV3.aspx">לקוחות בשרת</a></li>
                        <li style="border-right: 2px solid silver;">
                            <a href="Settings.aspx">הגדרות</a></li>
                        <% } %>
                        <li style="border-right: 2px solid silver;">
                            <a href="FindProblem.aspx">חפש תקלה</a></li>
                        <li style="border-right: 2px solid silver;">
                            <a href="AddProblemNew.aspx">תקלה חדשה</a></li>
                    </ul>
                    <%--<ul class="nav navbar-nav navbar-left">
                            <li><a href="#"><span class="glyphicon glyphicon-user"></span>Sign Up</a></li>
                            <li><a href="#"><span class="glyphicon glyphicon-log-in"></span>Login</a></li>
                        </ul>--%>
                </div>
            </div>
        </nav>
    </div>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div>
                    <div class="col-md-6 col-lg-6">
                        <asp:Literal ID="chartTodayHoursProblemsCount" runat="server" />
                    </div>

                    <div class="col-md-6 col-lg-6">
                        <asp:Literal ID="chartProblemDescEmpty" runat="server" />
                    </div>
                    <div class="col-md-12 col-lg-12">
                        <asp:Literal ID="chartProblemCountForWorkerToday" runat="server" />
                    </div>
                    <div class="col-md-12 col-lg-12">
                        <asp:Literal ID="chartProblemCountForWorkerTodayCloseThem" runat="server" />
                    </div>
                    <div class="col-md-6 col-lg-6">
                        <asp:Literal ID="chartProblemCountForDepartmentToday" runat="server" />
                    </div>
                    <div class="col-md-6 col-lg-6">
                        <asp:Literal ID="chartProblemCountForLastWeek" runat="server" />
                    </div>

                    <div class="col-md-12 col-lg-12">
                        <asp:Literal ID="chartMonthsProblemsCount" runat="server" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


        <div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString='<%$ ConnectionStrings:BeecommDBConnectionString %>' SelectCommand="SELECT CONVERT (VARCHAR(10), startTime, 10) AS Expr1, SUM(1) AS pCount FROM problemsClose WHERE (startTime >= '03/01/2017 06:00:00') AND (startTime < '03/09/2017 06:00:00') AND (problemDesc IS NOT NULL) AND (problemDesc <> N'') GROUP BY CONVERT (VARCHAR(10), startTime, 10) ORDER BY Expr1"></asp:SqlDataSource>
        </div>


        <div class="panel text-center" dir="rtl">
            <div class="col-lg-12 col-md-12">
                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-3 pull-right">
                    <input type="text" id="txtFindStartDate" name="txtFindStartDate" style="max-width: 100%; width: 100%" class="center-block input-lg text-center" />
                    <script>
                        $.datetimepicker.setLocale('he');
                        $('#txtFindStartDate').datetimepicker({
                            lang: 'he',
                            timepicker: true,
                            format: 'd/m/y H:i:s'
                        });
                    </script>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-4 col-lg-3 pull-right">
                    <input type="text" id="txtFindFinishDate" name="txtFindFinishDate" style="max-width: 100%; width: 100%" class="center-block input-lg text-center" />
                    <script>
                        $.datetimepicker.setLocale('he');
                        $('#txtFindFinishDate').datetimepicker({
                            lang: 'he',
                            timepicker: true,
                            format: 'd/m/y H:i:s',
                            //formatDate: 'd/m/y'
                        });
                    </script>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-3 col-lg-2 pull-right">
                    <button type="button" class="btn btn-primary btn-lg col-lg-12 pull-right" onclick="GetPlacesCount();return false;">הצג</button>
                </div>
            </div>


            <div id="divInfoProblems" class="col-sm-8 col-md-8 col-lg-8" style="overflow-x: auto">
                <table id="tblProblemsInfo" class="table table-bordered table-hover text-center h5 " width="100%">
                    <thead>
                        <tr>
                            <th class="text-center">מתאריך</th>
                            <th class="text-center">יוצר</th>
                            <th class="text-center">מקום</th>
                            <th class="text-center">IP</th>
                            <th class="text-center col-lg-3">תיאור</th>
                            <th class="text-center col-lg-3">פיתרון</th>
                            <%--<th class="text-center">מחלקה</th>--%>
                            <%--<th class="text-center">סטטוס</th>--%>
                            <th class="text-center">תומך</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
            <div class="col-sm-4 col-md-4 col-lg-4">
                <table id="tblPlacesCount" class="table table-bordered table-hover text-center h5 " width="100%">
                    <thead>
                        <tr>
                            <th class="text-center">מקום
                            </th>
                            <th class="text-center">טלפון
                            </th>
                            <th class="text-center">כמות
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>

        </div>

    </form>



    <script type="text/javascript" class="init">
        $(document).ready(function () {

            //console.log("Start document");
            var today = new Date();
            var date = today.getDate() + '/' + (today.getMonth() + 1) + '/' + today.getFullYear();
            var time = today.getHours() + ":" + today.getMinutes() + ":00";
            var finish = date + ' ' + time;

            var start = today.getDate() + '/' + (today.getMonth() + 1) + '/' + today.getFullYear() + ' 06:00:00';


            $("#txtFindStartDate").val(start);
            $("#txtFindFinishDate").val(finish);


            

            /*SendPost('GetWorkersProblemsCount', '{}', function (response) { });*/

            GetPlacesCount();

            $('#tblPlacesCount').on('click', '.clickable-row', function (event) {
                $('#tblPlacesCount tr').each(function (i, row) {
                    $(row).removeClass("active");
                });

                $(this).addClass('active');

                var rowName = $(this).attr('id');
                var phoneId = rowName.replace("phoneIdrow", "");
                GetProblems(phoneId);
            });
        });

        function showLoader() {
            $(".loader").css("display", "visible");
            $(".loader").fadeIn("slow");

        }
        function hideLoader() {
            $(".loader").css("display", "none");
            $(".loader").fadeOut("slow");
        }

        function toggleDiv(divName) {
            $('#' + divName).slideToggle();
        }

        function SendPost(commandName, dataInfo, onsuccess, onError) {
            showLoader();
            $.ajax({
                type: "POST",
                url: "DashBoard.aspx/" + commandName,
                data: dataInfo,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: onsuccess,
                error: function (e) { console.log("error: " + e.statusText); } //onError
            });
        }

        function GetPlacesCount() {
            var start = $("#txtFindStartDate").val();
            var finish = $("#txtFindFinishDate").val();
            //console.log("GetPlacesCount: " + start + " : " + finish);

            SendPost('GetPlacesCount', '{start: "' + start + '",finish: "' + finish + '"}', function (response) {
                var xmlDoc = $.parseXML(response.d);
                var xml = $(xmlDoc);
                var branches = xml.find("Table");

                $("#tblPlacesCount tbody tr").empty();

                $.each(branches, function () {
                    var phoneId = $(this).find("phoneId").text();
                    var placeName = $(this).find("placeName").text().replace("'", "");;
                    var phone = $(this).find("phone").text();
                    var pCount = $(this).find("pCount").text();

                    var html = "<tr id='phoneIdrow" + phoneId + "' class='clickable-row'>" +
                                       "<td><b>" + placeName + "</b></td>" +
                                       "<td>" + phone + "</td>" +
                                       "<td>" + pCount + "</td>" +
                                   "</tr>";


                    //console.log(html);
                    $(html).appendTo($("#tblPlacesCount tbody"));
                });
                hideLoader();

                //$('#tblPlacesCount').DataTable(
                //{
                //    destroy: true,
                //    "order": [[2, "desc"], [0, 'asc']]

                //});
                //).ajax.reload();
                //();//{ "autoWidth": false, "destroy": true, stateSave: true });
            });

        }

        function GetProblems(phoneId) {
            showLoader();

            var start = $("#txtFindStartDate").val();
            var finish = $("#txtFindFinishDate").val();
            //console.log(start + " " + finish);

            SendPost('GetProblems', '{phoneId: ' + phoneId + ', start: "' + start + '",finish: "' + finish + '"}', function (response) {
                var xmlDoc = $.parseXML(response.d);
                var xml = $(xmlDoc);
                var branches = xml.find("Table");
                //console.log(response.d);

                $("#tblProblemsInfo tbody tr").empty();

                $.each(branches, function () {  
                    var id = $(this).find("id").text();

                    var today = new Date($(this).find("startTime").text());
                    today.setHours(today.getHours() - 2);
                    var date = today.getDate() + '/' + (today.getMonth() + 1) + '/' + today.getFullYear();
                    var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
                    var d = date + ' ' + time;
                    

                    var workerName = $(this).find("workerName").text();
                    var placeName = $(this).find("placeName").text();
                    var ip = $(this).find("ip").text();
                    var problemDesc = $(this).find("problemDesc").text();
                    var problemSolution = $(this).find("problemSolution").text();
                    var departmentName = $(this).find("departmentName").text();
                    var statusName = $(this).find("statusName").text();
                    var toWorkerName = $(this).find("toWorkerName").text();

                    var rowClass = "";
                    if (statusName == "פתוח") {
                        rowClass = "aleart-danger";
                    }

                    if (statusName == "פתוח") {
                        rowClass = "alert-success";
                    }

                    if (statusName == "ממתין") {

                        rowClass = "alert-warning";
                    }

                    if (statusName == "סגור") {
                        rowClass = "alert-success";
                    }

                    var html = "<tr id='problemRowId" + id + "' class='clickable-row " + rowClass + "'>" +
                                       "<td>" + d + "</td>" +
                                       "<td>" + workerName + "</td>" +
                                       "<td><b>" + placeName + "</b></td>" +
                                       "<td>" + ip + "</td>" +
                                       "<td>" + problemDesc + "</td>" +
                                       "<td>" + problemSolution + "</td>" +
                                       //"<td>" + departmentName + "</td>" +
                                       //"<td>" + statusName + "</td>" +
                                       "<td>" + toWorkerName + "</td>" +
                                   "</tr>";

                    $(html).appendTo($("#tblProblemsInfo tbody"));
                });

                hideLoader();

                $('html, body').animate({
                    scrollTop: $("#divInfoProblems").offset().top
                }, 1000);



            });
        }
    </script>
</body>
</html>
