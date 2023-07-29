<%@ Page Title="הוסף תקלה" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
                    CodeFile="AddProblemNew.aspx.cs" 
    Inherits="AddProblemNew" ValidateRequest="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            z-index: 9999;
            background: url('/Pics/loading.gif') 50% 50% no-repeat rgb(249,249,249);
            opacity: .8;
        }

        td {
            word-wrap: break-word;
        }

        th {
            text-align: center;
            text-decoration: none;
            color: black;
        }

            th:hover {
                color: blue;
            }

        label.active {
            font-size: 21px;
        }
    </style>

    <!-- Latest compiled JavaScript FOR MODALS -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>



    <!-- Data Table 
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/responsive/2.2.0/css/responsive.bootstrap.min.css" />
    <link href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap-glyphicons.css" rel="stylesheet">

    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js">
     </script>
    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap.min.js">
    </script>
    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/responsive/2.2.0/js/dataTables.responsive.min.js">
    </script>
    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/responsive/2.2.0/js/responsive.bootstrap.min.js">
    </script>-->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="loader" style="display: none"></div>

    <div class="panel panel-primary">
        <div class="row text-center" style="margin-right:0;margin-left:0">

            <input id="txtWorkerCreator" type="text" class="pull-right col-sm-3 col-md-3 col-lg-3 input-lg h1" disabled="disabled" style="margin-right: 40px" placeholder="עובד יוצר" />

            <button type="button" class="btn btn-default" style="margin-left: 50px" onclick="OpenProblemAutoOnME()">לפתוח תקלה ממני</button>
            <input id="txtPhoneId" class="col-3 input-lg hide" placeholder="" type="number" />
            <input id="txtPhone" type="number" class="input-lg h1 text-center" placeholder="הזן טלפון" autofocus />
            <button type="button" class="btn btn-default" style="margin-right: 50px" onclick="AnsweredThePhone()">עניתי לטלפון</button>
        </div>
    </div>

    <div>
        <div id="divAddNewProblem" class="panel panel-primary col-12" style="display: none">
            <div class="row h4">

                <div class="col-xs-12 col-sm-2 col-md-2 col-lg-2 pull-right">
                    <input id="txtProblemId" class="col-xs-12 input-lg hide" placeholder="" type="number" />
                    <input id="txtPlaceId" class="col-xs-12 input-lg hide" placeholder="שם המקום" type="number" />
                    <input id="txtPlaceName" class="col-xs-12 input-lg" placeholder="שם המקום" type="text" />

                    <div class="input-group">
                        <input id="txtIp" class="col-xs-12 input-lg" placeholder="IP" type="text" />
                        <span class="input-group-addon"><i class="glyphicon glyphicon-floppy-save" onclick="updateIp()"></i></span>
                    </div>
                    <input id="txtCusName" class="col-xs-12 input-lg" placeholder="שם הלקוח" type="text" />
                </div>

                <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3 pull-right">
                    <textarea id="txtDesc" rows="6" placeholder="תיאור התקלה" class="col-xs-12 col-lg-12"></textarea>
                    <i class="glyphicon glyphicon-question-sign" data-toggle="tooltip" data-placement="top"
                        title="נא להזין שם המקום, תיאור התקלה, מיקום התקלה הקובץ ואז הטופס ואז הכפתור או הפעולה, מידע נוסף שצריך כדי לשחזר את השגיאה"></i>
                </div>

                <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3 pull-right">
                    <textarea id="txtSolution" name="txtSolution" rows="6" class="col-xs-12 col-lg-12" placeholder="תיאור הפיתרון"></textarea>
                    <button class="btn btn-primary" onclick="AddDivider();return false">------------------------------</button>
                </div>




                <div class="col-xs-12 col-sm-2 col-md-2 col-lg-2 pull-right">
                    <div class="btn-group btn-group-toggle col-xs-12" data-toggle="buttons" style="padding-left: 0px; padding-right: 0px">
                        <label id="chkStatusClose" class="btn btn-success active col-xs-4 input-lg">
                            <input type="radio" name="options" class="input-lg" autocomplete="off">
                            סגור
                            </input>
                        </label>
                        <label id="chkStatusHandle" class="btn btn-warning col-xs-4 input-lg">
                            <input type="radio" name="options" class="input-lg" autocomplete="off">
                            בטיפול</input>
                        </label>
                        <label id="chkStatusOpen" class="btn btn-info col-xs-4 input-lg">
                            <input type="radio" name="options" class="input-lg" autocomplete="off">
                            ממתין</input>
                        </label>
                    </div>

                    <select id="cboEmergency" class="col-xs-12 input-lg">
                        <option value="0">רגיל</option>
                        <option value="1">דחוף</option>
                        <option value="2">בעדיפות</option>
                        <option value="3">תקלה מהלילה</option>
                    </select>
                    <div class="input-group">
                        <select id="cboDepartment" class="col-xs-12 input-lg">
                            <option value="1">כללי</option>
                            <option value="2">טכני</option>
                            <option value="3">תוכנה</option>
                            <option value="4">תפריטים</option>
                            <option value="5">איפוסים</option>
                            <option value="6">שדרוגים</option>
                            <option value="7">הנהלת חשבונות</option>
                            <option value="8">שיווק</option>
                            <option value="9">לחזור ללקוח</option>
                            <option value="10">ציוד</option>
                            <option value="11">יוזרים</option>
                            <%--<option value="12">קיוסק</option>--%>
                            <option value="13">dejavoo</option>
                            <option value="15">שרת משלוחים</option>
                        </select>
                        <span class="input-group-addon" onclick="ReturnToCustomer()"><i class="glyphicon glyphicon-repeat"></i></span>
                    </div>

                </div>

                <div class="col-xs-12 col-sm-2 col-md-2 col-lg-2">
                    <div class="input-group">
                        <select id="cboToWorker" class="col-xs-12 input-lg">
                        </select>
                        <span class="input-group-addon" onclick="ReturnToSender()"><i class="glyphicon glyphicon-repeat"></i></span>
                    </div>

                    <div class="btn-group btn-group-toggle col-xs-12" data-toggle="buttons" style="padding-left: 0px; padding-right: 0px">
                        <button id="btnFileManager" class="btn btn-default btn-lg input-lg col-xs-12">מנהל קבצים</button>
                    </div>


                    <button class="btn btn-primary btn-lg input-lg col-xs-12" onclick="UpdateProblem();return false;">עדכן</button>
                </div>
            </div>
        </div>

        <%--style="max-height: 600px; overflow: auto"--%>
        <div class="panel" style="overflow-x: auto">
            <table id="tblProblems" class="table table-bordered table-hover text-center">
                <thead>
                    <tr>
                        <th class="text-center" onclick="sortTable(0)">תאריך</th>
                        <th class="text-center" onclick="sortTable(1)">יוצר</th>
                        <th class="text-center" onclick="sortTable(2)">לקוח</th>
                        <th class="text-center" onclick="sortTable(3)">טלפון</th>
                        <th class="text-center" onclick="sortTable(4)">מקום</th>
                        <th class="text-center" onclick="sortTable(5)">IP</th>
                        <th class="text-center" style="max-width: 200px" onclick="sortTable(6)">תיאור הבעיה</th>
                        <th class="text-center" style="max-width: 200px" onclick="sortTable(7)">תיאור הפיתרון</th>
                        <th class="text-center" onclick="sortTable(8)">סטטוס</th>
                        <th class="text-center" onclick="sortTable(9)">דחיפות</th>
                        <th class="text-center" onclick="sortTable(10)">מחלקה</th>
                        <th class="text-center" onclick="sortTable(11)">תומך</th>
                        <th class="text-center" onclick="sortTable(12)">קבצים</th>
                        <th id="tdEditProblem" class="text-center"></th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </div>

    <div class="modal fade" id="divAddTables" tabindex="-1" role="dialog" aria-labelledby="divAddTables" aria-hidden="true">

        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title text-center" id="divAddTablesLable">בחר מקום</h4>
                </div>
                <div class="modal-body">
                    <input id="txtSelectPlacePhone" class="hide" />
                    <table id="tblPlaces" class="table table-bordered table-hover text-center">
                        <thead>
                            <tr>
                                <th class="text-center">מקום</th>
                                <th class="text-center">IP</th>
                                <th class="text-center">שם לקוח</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>

                    <div class="row">
                        <div class="panel col-xs-12">
                            <input id="txtSelectPlaceId" class="hide" placeholder="placeId" />
                            <input id="txtSelectPlaceName" class="col-xs-11 col-md-3 input-lg pull-right" placeholder="שם המקום" />
                            <button class="btn btn-primary pull-right col-xs-1 col-md-1 input-lg" onclick="SearchPlacesName();return false;"><span class="glyphicon glyphicon-search"></span></button>
                            <input id="txtSelectPlaceIP" class="col-xs-12 col-md-3 input-lg pull-right" placeholder="IP" />
                            <input id="txtSelectPlaceCusName" class="col-xs-12 col-md-3 input-lg pull-right" placeholder="שם הלקוח" />
                            <button class="btn btn-primary input-lg col-xs-12 col-md-2" onclick="AddNewPhonePlace();return false;">הוסף חדש</button>
                        </div>
                    </div>
                    <div style="max-height: 450px; overflow-y: auto">
                        <table id="tblAllPlaces" class="table table-bordered table-hover text-center">
                            <thead>
                                <tr>
                                    <th class="text-center">מקום</th>
                                    <th class="text-center">IP</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="divProblemLog" tabindex="-1" role="dialog" aria-labelledby="divProblemLogTitle" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title text-center" id="divProblemLogTitle">לוג</h4>
                </div>
                <div class="modal-body">
                    <table id="tblLogs" class="table table-bordered table-hover text-center">
                        <thead>
                            <tr>
                                <th class="text-center">עובד מעדכן</th>
                                <th class="text-center">שדה</th>
                                <th class="text-center">ערך קודם</th>
                                <th class="text-center">ערך חדש</th>
                                <th class="text-center">תאריך</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="divFilesManager" tabindex="-1" role="dialog" aria-labelledby="divFilesManagerTitle" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title text-center" id="divFilesManagerTitle">קבצים</h4>
                </div>
                <div class="modal-body">

                    <table id="tblFiles" class="table table-bordered table-hover text-center">
                        <thead>
                            <tr>
                                <th class="text-center">קובץ</th>
                                <th class="text-center"></th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                        <tfoot>
                            <tr>
                                <td colspan="2">
                                    <div class="row">
                                        <iframe id="frmUploadFiles" class="col-12"></iframe>
                                    </div>
                                </td>
                            </tr>
                        </tfoot>
                    </table>


                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript" class="init">
        $(document).ready(function () {
            $("#txtWorkerCreator").hide();
            $("#txtDesc").val('');
            $("#txtSolution").val('');
            GetWorkers();

            GetProblems(false);

            $('#tblPlaces').on('click', '.clickable-row', function (event) {
                $('#tblPlaces tr').each(function (i, row) {
                    $(row).removeClass("active");
                });

                $(this).addClass('active');//.siblings().removeClass('active');
            });

            $('#tblAllPlaces').on('click', '.clickable-row', function (event) {
                $('#tblAllPlaces tr').each(function (i, row) {
                    $(row).removeClass("active");
                });

                $(this).addClass('active');//.siblings().removeClass('active');

                var s = $(this).attr("id");
                s = s.replace("rowPlace", "");
                $("#txtSelectPlaceId").val(s);
                var tds = $(this).find("td");


                $('#txtSelectPlaceName').val(tds[0].innerHTML);
                $('#txtSelectPlaceIP').val(tds[1].innerHTML);
            });

            $("#txtPhone").on("keydown", function (event) {
                if (event.which == 13) {
                    var phone = $("#txtPhone").val();
                    ShowPlaces(phone);
                }
            });

            $("#btnFileManager").click(function () {
                showFileManager();
            });

            $("#txtSelectPlaceName").on('input', function (e) {
                filterTable("tblAllPlaces", "txtSelectPlaceName");
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

        function filterTable(tableName, fielddName) {
            var filter, table, tr, td, i;

            filter = $("#" + fielddName).val();

            table = document.getElementById(tableName);
            tr = table.getElementsByTagName("tr");

            for (i = 0; i < tr.length; i++) {
                var td = tr[i].getElementsByTagName("td")[0];

                if (td) {
                    if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                }
            }
        }


        function SendPostLocal(commandName, dataInfo, onsuccess, onError) {
            //showLoader();

            //console.log("Start SendPost");
            $.ajax({
                type: "POST",
                url: "AddProblemNew.aspx/" + commandName,
                data: dataInfo,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: onsuccess,
                error: onError
            });
        }

        function GetWorkers() {
            SendPostLocal("GetWorkers", '{}', function (response) {
                //console.log(response.d);

                $("#cboToWorker").empty();
                $('#cboToWorker').append($("<option></option>").attr("value", 0).text(''));

                $("#cboWorkerBreakWorkers").empty();
                $('#cboWorkerBreakWorkers').append($("<option></option>").attr("value", 0).text('כולם'));

                var xmlDoc = $.parseXML(response.d);
                var xml = $(xmlDoc);
                var rows = xml.find("Table");

                $.each(rows, function () {
                    var id = parseInt($(this).find("id").text());
                    var workerName = $(this).find("firstName").text() + " " + $(this).find("lastName").text();
                    $('#cboToWorker').append($("<option></option>").attr("value", id).text(workerName));
                    $('#cboWorkerBreakWorkers').append($("<option></option>").attr("value", id).text(workerName));
                });


                return true;

            },
                function (req, response) { console.log(req.d); });
        }



        function ShowPlaces(phone) {

            $("#txtSelectPlacePhone").val(phone);
            $("#txtSelectPlaceId").val(0);
            $("#txtSelectPlaceId").val(0);
            $("#txtSelectPlaceName").val('');
            $("#txtSelectPlaceIP").val('');
            $("#txtSelectPlaceCusName").val('');
            $("#tblAllPlaces tbody").empty();

            SendPostLocal("GetPlacesForPhone", '{phone: "' + phone + '"}',
                function (response) {
                    $("#tblPlaces tbody").empty();

                    $.each(response.d, function (index, value) {
                        //console.log(response.d[index].phone);

                        var id = response.d[index].id;
                        var phoneId = response.d[index].phoneId;
                        var placeId = response.d[index].placeId;
                        var placeName = response.d[index].placeName;
                        var ip = response.d[index].ip;
                        var customerName = response.d[index].customerName;

                        var html = '<tr id="rowPhonePlace' + id + '" onClick="SelectPhonePlace(' + id + ')" class="clickable-row" >';
                        html = html + '<td>' + placeName + '</td>';
                        html = html + '<td>' + ip + '</td>';
                        html = html + '<td>' + customerName + '</td>';
                        html = html + '</tr>';

                        $(html).appendTo($("#tblPlaces tbody"));


                    });

                    $("#divAddTables").modal();
                });
        }


        function AddNewPhonePlace() {

            var phone = $("#txtSelectPlacePhone").val();
            var placeId = $("#txtSelectPlaceId").val();;
            var placeName = $("#txtSelectPlaceName").val();
            var ip = $("#txtSelectPlaceIP").val();
            var cusName = $("#txtSelectPlaceCusName").val();

            if (placeName == '') {
                alert("אנא הזן שם מקום");
                return;
            }

            if (placeName.length>255) {
                alert("השם לא יכול להיות ארוך מ255 תווים");
                return;
            }

            if (cusName == '') {
                alert("אנא הזן שם לקוח");
                return;
            }

            if (ip == '') {
                if (!confirm("שכחת להזין IP האם אתה בטוח שברצונך להמשיך?")) {
                    return;
                }
            }
            else {
                if (ip.length > 20) {
                    alert("הIP לא יכול להיות ארוך מ20 תווים");
                    return;
                }
            }

            

            placeName = placeName.trim().replace(/\\/g, "\\\\").replace(/'/g, "\\'").replace(/"/g, '\\"');
            cusName = cusName.trim().replace(/\\/g, "\\\\").replace(/'/g, "\\'").replace(/"/g, '\\"');


            var params = '{phone: "' + phone + '", placeId: ' + placeId + ',placeName: "' + placeName + '",ip: "' + ip + '",cusName: "' + cusName + '"}';
            console.log(params);
            SendPostLocal("AddNewPhonePlace", params,
                function (response) {
                    SelectPhonePlace(response.d);
                });
        }

        function SelectPhonePlace(phonePlaceId) {
            var phone = $("#txtSelectPlacePhone").val();
            console.log("hi");
            SendPostLocal("AddProblem", '{phone: "' + phone + '", phonePlaceId: ' + phonePlaceId + '}',
                function (response) {

                    var a = response.d.split(";");

                    $("#txtProblemId").val(a[0]);
                    $("#txtPhoneId").val(a[1]);
                    $("#txtPlaceId").val(a[2]);
                    $("#txtPlaceName").val(a[3]);
                    $("#txtIp").val(a[4]);
                    $("#txtCusName").val(a[5]);

                    $("#cboToWorker").val(a[6]);

                    $("#txtDesc").val('');
                    $("#txtSolution").val('');
                    //$("#cboStatus").val(0);
                    SetStatusView(0);
                    $("#cboEmergency").val(0);
                    $("#cboDepartment").val(1);

                    $("#txtPhone").attr('disabled', 'disabled');
                    $("#divAddTables").modal('hide');
                    $("#divAddNewProblem").show();
                    $("#txtPhone").val(phone);

                    $("#txtDesc").focus();

                    GetProblems(true);
                });
        }

        function OpenProblemAutoOnME() {
            $("#txtProblemId").val('');
            $("#txtSelectPlacePhone").val('1122334455');
            SelectPhonePlace(-1);
        }

        function SetStatusView(statudId) {
            console.log("SetStatusView: " + statudId);
            $("#chkStatusOpen").removeClass("active");
            $("#chkStatusHandle").removeClass("active");
            $("#chkStatusClose").removeClass("active");

            if (statudId == 0) {
                $("#chkStatusOpen").addClass("active");
            }

            if (statudId == 1) {
                $("#chkStatusHandle").addClass("active");
            }

            if (statudId == 2) {
                $("#chkStatusClose").addClass("active");
            }
        }

        function GetStatusView() {
            console.log("GetStatusView");

            if ($("#chkStatusOpen").hasClass("active")) {
                return 0;
            }

            if ($("#chkStatusHandle").hasClass("active")) {
                return 1;
            }

            if ($("#chkStatusClose").hasClass("active")) {
                return 2;
            }

            return 0;
        }



        function UpdateProblem() {
            var problemId = $("#txtProblemId").val();
            if (problemId == 0) {
                return;
            }
            var ip = $("#txtIp").val();
            var placeId = $("#txtPlaceId").val();
            var placeName = $("#txtPlaceName").val();
            var customerName = $("#txtCusName").val();

            var desc = $("#txtDesc").val();

            var solution = $("#txtSolution").val();
            //var solution = document.getElementsByName('txtSolution')[0].value;

            //var solution = document.getElementById("txtSolution").value;
            //console.log("solution: " + solution);

            var toWorker = $("#cboToWorker").val();
            //var status = $("#cboStatus").val();
            var status = GetStatusView();
            console.log(status);
            var emergency = $("#cboEmergency").val();
            var department = $("#cboDepartment").val();
            var reportToYaron = $("#cboReportToYaron").val();

            if (desc.length == 0) {
                alert("אנא הזן תיאור של הבעיה");
                $("#txtDesc").focus();
                return;
            }

            if (placeName.length == 0) {
                alert("אנא הזן את שם המקום");
                $("#txtPlaceName").focus();
                return;
            }

            if (placeName.length == 0) {
                alert("אנא הזן את שם הלקוח");
                $("#txtCusName").focus();
                return;
            }

            if (ip.length == 0) {
                if (!confirm("לא הזנת IP האם ברצונך להמשיך?")) {
                    $("#txtCusName").focus();
                    return;
                }
            }

            if (status == null) {
                alert("אנא בחר סטטוס");
                return;
            }

            if (emergency == null) {
                alert("אנא בחר דחיפות");
                return;
            }

            if (department == null) {
                alert("אנא בחר מחלקה");
                return;
            }

            desc = desc.trim().replace(/\\/g, "\\\\").replace(/'/g, "\\'").replace(/"/g, '\\"').replace('<', '').replace('/>', '');
            solution = solution.trim().replace(/\\/g, "\\\\").replace(/'/g, "\\'").replace(/"/g, '\\"').replace('<', '').replace('/>', '');
            customerName = customerName.trim().replace(/\\/g, "\\\\").replace(/'/g, "\\'").replace(/"/g, '\\"').replace('<', '').replace('/>', '');
            placeName = placeName.trim().replace(/\\/g, "\\\\").replace(/'/g, "\\'").replace(/"/g, '\\"').replace('<', '').replace('/>', '');
            ip = ip.trim().replace(/\\/g, "\\\\").replace(/'/g, "\\'").replace(/"/g, '\\"').replace('<', '').replace('/>', '');

            //UpdateProblem(int problemId, string ip, int placeId, string placeName, string customerName, string desc, string solution, int toWorker, int status, int emergency, int department, bool reportToYaron)
            var params = '{problemId: ' + problemId + ',ip: "' + ip + '",placeId: ' + placeId + ',placeName: "' + placeName + '",customerName: "' + customerName + '",desc: "' + desc + '",solution: "' + solution + '",' +
                'toWorker: ' + toWorker + ',status: ' + status + ',emergency: ' + emergency + ',department: ' + department + ',reportToYaron: ' + (reportToYaron == 1) + '}';

            //console.log(params);

            SendPostLocal("UpdateProblem", params,
                function (response) {
                    $("#txtWorkerCreator").hide();
                    $("#divAddNewProblem").hide();
                    $("#txtPhone").val('');
                    $("#txtProblemId").val(0);
                    $("#txtPhoneId").val(0);
                    $("#txtPlaceId").val(0);
                    $("#txtPlaceName").val('');
                    $("#txtIp").val('');
                    $("#txtCusName").val('');
                    $("#txtDesc").val('');
                    $("#txtSolution").val('');
                    $("#cboReportToYaron").val(0);
                    $("#cboDepartment").val(1);
                    $("#cboEmergency").val(0);
                    $("#cboEmergency").val(0);

                    //$("#cboToWorker").val(a[6]);

                    $("#txtProblemId").val('');

                    $("#txtPhone").removeAttr('disabled');
                    $("#txtPhone").focus();

                    GetProblems(false);


                });

            return false;
        }


        function GetProblems(forPhone) {
            showLoader();
            var problemId = 0;
            var phone = '';

            if (forPhone) {
                problemId = $("#txtProblemId").val();
                phone = $("#txtPhone").val();
                $("#tdEditProblem").hide();
            }
            else {
                $("#tdEditProblem").show();
            }

            $("#tblProblems tbody").empty();

            SendPostLocal("GetProblems", '{problemId: ' + problemId + ', phone:"' + phone + '"}',
                function (response) {

                    var xmlDoc = $.parseXML(response.d);
                    var xml = $(xmlDoc);
                    var customers = xml.find("Table");


                    $.each(customers, function () {
                        var id = $(this).find("id").text();

                        var workerName = $(this).find("workerName").text();
                        var customerName = $(this).find("customerName").text();
                        var phone = $(this).find("phone").text();
                        var placeName = $(this).find("placeName").text();
                        var ip = $(this).find("ip").text();

                        var problemDesc = $(this).find("problemDesc").text();
                        problemDesc = problemDesc.replace("\n", "<br>");
                        problemDesc = problemDesc.replace("\n", "<br>");
                        problemDesc = problemDesc.replace("\n", "<br>");
                        problemDesc = problemDesc.replace("\n", "<br>");

                        var problemSolution = $(this).find("problemSolution").text();
                        problemSolution = problemSolution.replace("\n", "<br>");
                        problemSolution = problemSolution.replace("\n", "<br>");
                        problemSolution = problemSolution.replace("\n", "<br>");
                        problemSolution = problemSolution.replace("\n", "<br>");

                        var statusName = $(this).find("statusName").text();
                        var emergencyName = $(this).find("emergencyName").text();
                        var departmentName = $(this).find("departmentName").text();
                        var toWorkerName = $(this).find("toWorkerName").text();
                        var rty = $(this).find("reportToYaron").text();
                        var aarty = $(this).find("pFileId").text();
                        //console.log("pFileId: " + aarty);

                        var reportToYaron = "לא";
                        if (aarty > 0) {
                            reportToYaron = "כן";
                        }


                        var today = new Date($(this).find("startTime").text());
                        today.setHours(today.getHours() - 2);
                        var date = today.getDate() + '/' + (today.getMonth() + 1) + '/' + today.getFullYear();
                        var time = today.getHours() + ":" + today.getMinutes();
                        var startTime = date + ' ' + time;
                        startTime = today.format("dd/MM/yyyy HH:mm");

                        var statusId = parseInt($(this).find("statusId").text());

                        var rowClass = "alert-info";
                        if (statusId == 1) {
                            rowClass = "alert-warning";
                        }

                        if (statusId == 2) {
                            rowClass = "alert-success";
                        }

                        if (emergencyName == "דחוף") {
                            rowClass = "alert-danger";
                        }

                        if (emergencyName == "בעדיפות") {
                            rowClass = "alert-warning";
                        }
                        //style = "cursor: pointer;"
                        var html = '<tr id="rowProblem' + id + '" class="clickable-row ' + rowClass + ' " >';
                        html = html + '<td>' + startTime + '<a class="glyphicon glyphicon-eye-open pull-left" onClick="showLog(' + id + ')" /></td>';
                        html = html + '<td>' + workerName + '</td>';
                        html = html + '<td>' + customerName + '</td>';
                        html = html + '<td>' + phone + '<a class="glyphicon glyphicon-phone btn pull-left" onClick="CallThisPhone(\'' + phone + '\')" /></td>';
                        html = html + '<td>' + placeName + '</td>';
                        html = html + '<td style = "cursor: pointer;" onclick={openRDP("' + ip + '")}>' + ip + '</td>';
                        html = html + '<td  style="max-width: 400px">' + problemDesc + '</td>';
                        html = html + '<td  style="max-width: 400px">' + problemSolution + '</td>';
                        html = html + '<td>' + statusName + '</td>';
                        html = html + '<td>' + emergencyName + '</td>';
                        html = html + '<td>' + departmentName + '</td>';
                        html = html + '<td>' + toWorkerName + '</td>';
                        html = html + '<td>' + reportToYaron + '</td>';

                        if (forPhone == false) {
                            html = html + '<td><a class="glyphicon glyphicon-pencil btn pull-left" onClick="EditProblem(' + id + ')" /></td>';
                        }

                        html = html + '</tr>';

                        $(html).appendTo($("#tblProblems tbody"));


                    });

                    //$("#tblProblems").DataTable({destroty:true});
                    hideLoader();

                });
        }


        function EditProblem(problemId) {
            SendPostLocal("GetProblem", '{problemId: ' + problemId + '}',
                function (response) {


                    var xmlDoc = $.parseXML(response.d);
                    var xml = $(xmlDoc);
                    var customers = xml.find("Table");

                    if (customers.length == 0) {
                        $("#divAddNewProblem").hide();
                    }

                    if (problemId > 0) {
                        var a = $("#rowProblem" + problemId).find("td:eq(1)").text();
                        console.log(a);
                        $("#txtWorkerCreator").show();
                        $("#txtWorkerCreator").val(a);
                    }


                    var prob = customers[0];
                    var id = $(prob).find("id").text();
                    var workerName = $(prob).find("workerName").text();
                    var customerName = $(prob).find("customerName").text();
                    var phoneId = $(prob).find("phoneId").text();
                    var phone = $(prob).find("phone").text();
                    var placeNameId = $(prob).find("placeNameId").text();
                    var placeName = $(prob).find("placeName").text();
                    var ip = $(prob).find("ip").text();

                    var problemDesc = $(prob).find("problemDesc").text();
                    var problemSolution = $(prob).find("problemSolution").text();
                    if (problemSolution == null) {
                        problemSolution = "";
                    }

                    var statusName = $(prob).find("statusName").text();
                    var emergencyId = $(prob).find("emergencyId").text();
                    var departmentId = $(prob).find("departmentId").text();
                    var toWorker = $(prob).find("toWorker").text();
                    var reportToYaron = $(prob).find("reportToYaron").text();
                    var statusId = parseInt($(prob).find("statusId").text());


                    $("#divAddNewProblem").show();

                    //console.log(placeName);
                    $("#txtProblemId").val(problemId);
                    $("#txtPhoneId").val(phoneId);
                    $("#txtPlaceId").val(placeNameId);
                    $("#txtPlaceName").val(placeName);
                    $("#txtIp").val(ip);
                    $("#txtCusName").val(customerName);


                    //console.log(toWorker);
                    $("#cboToWorker").val(toWorker);
                    //$("#cboStatus").val(statusId);
                    SetStatusView(statusId)
                    $("#cboDepartment").val(departmentId);
                    $("#cboEmergency").val(emergencyId);

                    $("#txtDesc").val(problemDesc);
                    $("#txtSolution").val(problemSolution);

                    $("#txtPhone").attr('disabled', 'disabled');


                    $("#txtPhone").val(phone);

                    $("#txtDesc").focus();

                    GetProblems(true);
                });
        }

        function showLog(problemId) {
            $("#tblLogs tbody").empty();

            showLoader();
            SendPostLocal("GetLog", '{problemId: ' + problemId + '}',
                function (response) {

                    var xmlDoc = $.parseXML(response.d);
                    var xml = $(xmlDoc);
                    var customers = xml.find("Table");


                    var lastGroupKey = '';
                    var lastCommitTime = '';
                    var showColor = true;
                    $.each(customers, function () {
                        var groupKey = $(this).find("groupKey").text();


                        var commitTime = lastCommitTime;

                        var rowColor = "";


                        if (groupKey != lastGroupKey) {
                            var commitTime = new Date($(this).find("commitTime").text()).format("dd/MM/yy HH:mm:ss");
                            showColor = !showColor;
                            groupKey = lastGroupKey;

                            console.log(showColor);
                        }

                        if (showColor) {
                            rowColor = "alert-info";
                        }

                        var workerName = $(this).find("workerName").text();
                        var fieldName = $(this).find("fieldName").text();
                        var originalValue = $(this).find("originalValue").text();
                        var newValue = $(this).find("newValue").text();
                        var newValue = $(this).find("newValue").text();



                        var html = '<tr id="rowLog' + groupKey + '" class="clickable-row ' + rowColor + '" >';
                        html = html + '<td>' + workerName + '</td>';
                        html = html + '<td><b>' + fieldName + '</b></td>';
                        html = html + '<td>' + originalValue + '</td>';
                        html = html + '<td>' + newValue + '</td>';
                        html = html + '<td>' + commitTime + '</td>';

                        html = html + '</tr>';

                        $(html).appendTo($("#tblLogs tbody"));
                    });

                    //
                    hideLoader();
                    $("#divProblemLog").modal();
                });
        }


        function showFileManager(problemId) {
            $("#tblFiles tbody").empty();
            var problemId = $("#txtProblemId").val();
            if (problemId == "") {
                return;
            }
            document.getElementById('frmUploadFiles').src = "UploadFiles.aspx";
            console.log("showFileManager problemId: " + problemId);
            showLoader();
            SendPostLocal("GetFiles", '{problemId: ' + problemId + '}',
                function (response) {
                    try {

                        var xmlDoc = $.parseXML(response.d);
                        var xml = $(xmlDoc);
                        var customers = xml.find("Table");

                        $.each(customers, function () {
                            //[id],[filePath],[fileName],[fileDesc]
                            var id = $(this).find("id").text();
                            //var filePath = $(this).find("filePath").text();
                            var fileName = $(this).find("fileName").text();
                            var fileDesc = $(this).find("fileDesc").text();
                            console.log(fileDesc);

                            var html = '<tr id="rowFile' + id + '">';
                            html = html + '<td><a href="Pics/problems/' + fileName + '"  target="_blank" rel="noopener noreferrer">' + fileName + '</a></td>';
                            html = html + '<td><img src="Pics/problems/' + fileName + '" style="max-width:50px;max-height:50px" /></td>';
                            html = html + '</tr>';

                            $(html).appendTo($("#tblFiles tbody"));
                        });



                        hideLoader();
                        $("#divFilesManager").modal();


                    } catch (e) {
                        hideLoader();
                        alert("נכשל להעלות את פרטי הקבצים, נא להראות לשחף")
                    }
                });


        }



        function SearchPlacesName() {

            var placeName = $("#txtSelectPlaceName").val();
            if (placeName == null) {
                alert("אנא הזן שם מקום לחיפוש");
                return;
            }

            if (placeName.length == 0) {
                alert("אנא הזן שם מקום לחיפוש");
                return;
            }

            if (placeName.length < 2) {
                alert("אנא הזן לפחות 2 אותיות לשם המקום");
                return;
            }

            $("#tblAllPlaces tbody").empty();
            showLoader();
            SendPostLocal("SearchPlacesName", '{placeName: "' + placeName + '"}',
                function (response) {

                    var xmlDoc = $.parseXML(response.d);
                    var xml = $(xmlDoc);
                    var customers = xml.find("Table");

                    $.each(customers, function () {
                        var id = $(this).find("id").text();
                        var placeName = $(this).find("placeName").text();
                        var ip = $(this).find("ip").text();


                        var html = '<tr id="rowPlace' + id + '" class="clickable-row">';
                        html = html + '<td>' + placeName + '</td>';
                        html = html + '<td>' + ip + '</td>';
                        html = html + '</tr>';

                        $(html).appendTo($("#tblAllPlaces tbody"));
                    });

                    hideLoader();

                });
        }

        function AnsweredThePhone() {


            showLoader();
            SendPostLocal("GetPhoneNumberAnswered", '',
                function (response) {
                    $("#txtProblemId").val('');
                    var phone = response.d;
                    if (phone.length == 0) {
                        alert("לא נמצא טלפון שענית לו")
                        hideLoader();
                        $("#txtPhone").focus();
                        return;
                    }

                    if (phone.length == '-666') {
                        alert("לא מוגדרת לך מספר שלוחה")
                        hideLoader();
                        $("#txtPhone").focus();
                        return;
                    }

                    hideLoader();
                    $("#txtPhone").val(phone);
                    ShowPlaces(phone);
                });
        }

        function CallThisPhone(phone) {
            console.log(phone);
            SendPostLocal("CallToThisPhone", '{phone: "' + phone + '"}',
                function (response) {
                    console.log(response.d);

                });

        }

        function updateIp() {
            var ip = $("#txtIp").val();
            var placeName = $("#txtPlaceName").val();

            if (!confirm("האם אתה בטוח שברצונך לעדכן למקום את הIP?")) {
                return;
            }

            SendPostLocal("UpdateIp", '{placeName:"' + placeName + '" , ip: "' + ip + '"}',
                function (response) {
                    //console.log(response.d);

                });

        }

        function ReturnToSender() {
            var creator = $("#txtWorkerCreator").val();
            //console.log(creator );
            //$("#cboToWorker").val(creator);


            $("#cboToWorker option").each(function () {
                if (creator == $(this).text()) {
                    console.log($(this).val());
                    console.log($(this).text());
                    $("#cboToWorker").val($(this).val());
                }

            });
        }

        function ReturnToCustomer() {
            $("#cboDepartment").val(9);
        }

        function sortTable(n) {
            var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
            table = document.getElementById("tblProblems");
            switching = true;
            // Set the sorting direction to ascending:
            dir = "asc";
            /* Make a loop that will continue until
            no switching has been done: */
            while (switching) {
                // Start by saying: no switching is done:
                switching = false;
                rows = table.rows;
                /* Loop through all table rows (except the
                first, which contains table headers): */
                for (i = 1; i < (rows.length - 1); i++) {
                    // Start by saying there should be no switching:
                    shouldSwitch = false;
                    /* Get the two elements you want to compare,
                    one from current row and one from the next: */
                    x = rows[i].getElementsByTagName("TD")[n];
                    y = rows[i + 1].getElementsByTagName("TD")[n];
                    /* Check if the two rows should switch place,
                    based on the direction, asc or desc: */
                    if (dir == "asc") {
                        if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                            // If so, mark as a switch and break the loop:
                            shouldSwitch = true;
                            break;
                        }
                    } else if (dir == "desc") {
                        if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
                            // If so, mark as a switch and break the loop:
                            shouldSwitch = true;
                            break;
                        }
                    }
                }

                if (shouldSwitch) {
                    /* If a switch has been marked, make the switch
                    and mark that a switch has been done: */
                    rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
                    switching = true;
                    // Each time a switch is done, increase this count by 1:
                    switchcount++;
                } else {
                    /* If no switching has been done AND the direction is "asc",
                    set the direction to "desc" and run the while loop again. */
                    if (switchcount == 0 && dir == "asc") {
                        dir = "desc";
                        switching = true;
                    }
                }
            }
        }

        function openRDP(ip) {
            if (ip) {
                console.log(ip);
                try {
                    fetch('http://localhost:5150/api/rdp/192.168.' + ip);
                } catch {

                }
            }
        }


        function AddDivider() {
            var txt = $("#txtSolution").val() + "\n----------------------\n";
            $("#txtSolution").val(txt);
            $("#txtSolution").focus();
        }

    </script>
</asp:Content>

