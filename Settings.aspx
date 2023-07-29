<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="Settings" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>


    <style>
        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('/Pics/loading.gif') 50% 50% no-repeat rgb(249,249,249);
            opacity: .8;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="loader" style="display: none"></div>


    <div class="panel panel-default">
        <h3 class="panel panel-heading text-center" onclick="showWorkers()">עובדים</h3>
        <div id="divWorkers" class="panel panel-primary col-xs-12 text-center" style="display: none">


            <div>
                <table id="tblWorkers" class="table table-bordered table-hover text-center">
                    <thead>
                        <tr>
                            <th class="text-center">ID
                            </th>
                            <th class="text-center">שם פרטי
                            </th>
                            <th class="text-center">שם משפחה
                            </th>
                            <th class="text-center">טלפון
                            </th>
                            <th class="text-center">תפקיד
                            </th>
                            <th class="text-center">שם משתמש
                            </th>
                            <th class="text-center">סיסמה
                            </th>
                            <th class="text-center">סוג משתמש
                            </th>
                            <th class="text-center">שלוחה
                            </th>
                            <th class="text-center">פעיל
                            </th>
                            <th class="text-center">תמונה
                            </th>
                            <th class="text-center">רכב
                            </th>
                            <th class="text-center">מספר רכב
                            </th>
                            <th class="text-center">
                                <button class="btn btn-primary pull-left" onclick="showUpdateWorker(0);return false;">הוסף חדש</button></th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    <div class="panel panel-default">
        <h3 class="panel panel-heading text-center" onclick="showPlaces()">מקומות</h3>
        <div id="divPlaces" class="panel panel-primary col-xs-12 text-center" style="display: none">

            <div>
                <table id="tblPlaces" class="table table-bordered table-hover text-center">
                    <thead>
                        <tr>
                            <th class="text-center">ID
                            </th>
                            <th class="text-center">מקום
                            </th>
                            <th class="text-center">IP
                            </th>
                            <th class="text-center">VIP
                            </th>
                            <th class="text-center">עובד אחראי
                            </th>
                            <th class="text-center"></th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>

            <asp:GridView ID="grdPlaces" runat="server" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="dsPlaces" Width="100%" AllowSorting="True" CssClass="table table-bordered table-hover" AllowPaging="True" PageSize="50">
                <Columns>
                    <asp:CommandField ShowEditButton="True" EditText="ערוך" />
                    <asp:BoundField DataField="placeName" HeaderText="שם המקום" SortExpression="placeName" HeaderStyle-CssClass="text-center" />
                    <asp:BoundField DataField="ip" HeaderText="ip" SortExpression="ip" HeaderStyle-CssClass="text-center" />
                    <asp:BoundField DataField="vip" HeaderText="vip" SortExpression="vip" HeaderStyle-CssClass="text-center" />
                    <asp:TemplateField HeaderText="אחראי" ItemStyle-Width="100px" HeaderStyle-CssClass="text-center">
                        <ItemTemplate>
                            <%# Eval("workerName") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="cboPlaceWorkerId" runat="server" SelectedValue='<%# Bind("workerId") %>' AppendDataBoundItems="true" DataSourceID="dsWorkers" DataTextField="workerName" DataValueField="id">
                                <asp:ListItem Value="0" Text="" />
                            </asp:DropDownList>
                        </EditItemTemplate>

                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource runat="server" ID="dsPlaces" ConnectionString='<%$ ConnectionStrings:BeecommDBConnectionString %>'
                SelectCommand="SELECT Places.id, Places.placeName, Places.ip, Places.vip, ISNULL(Places.workerId, 0) workerId, ISNULL(workers.firstName, N'') + N' ' + ISNULL(workers.lastName, N'') AS workerName FROM Places LEFT OUTER JOIN workers ON Places.id = workers.id ORDER BY Places.placeName"
                UpdateCommand="UPDATE Places SET placeName = @placeName, ip = @ip, vip = @vip, workerId = @workerId WHERE (id = @id)">
                <UpdateParameters>
                    <asp:Parameter Name="placeName"></asp:Parameter>
                    <asp:Parameter Name="ip"></asp:Parameter>
                    <asp:Parameter Name="vip"></asp:Parameter>
                    <asp:Parameter Name="id"></asp:Parameter>
                </UpdateParameters>
            </asp:SqlDataSource>
            <%--    </ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
    </div>

    <div class="panel panel-default">
        <h3 class="panel panel-heading text-center" onclick="showMoshikPlaces()">סניפים של מושיק</h3>
        <div id="divMoshikPlaces" class="panel panel-primary col-xs-12 text-center" style="display: none">
            <div class="row">
                <div class="col-lg-11">
                    <table id="tblMoshikBranches" class="table table-bordered table-hover text-center">
                        <thead>
                            <tr>
                                <th class="text-center">רשת</th>
                                <th class="text-center">סניף</th>
                                <th class="text-center">מפתח</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>ספר האוכל</td>
                                <td>ספר האוכל</td>
                                <td>ספר האוכל</td>
                            </tr>
                        </tbody>
                    </table>
                </div>

                <div class="col-lg-1">
                    <table id="tblMoshikGroups" class="table table-bordered table-hover text-center">
                        <thead>
                            <tr>
                                <th class="text-center">מקום</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr onclick="GetMoshikBranches(1);">
                                <td>ספר האוכל</td>
                            </tr>
                            <tr onclick="GetMoshikBranches(2);">
                                <td>בינגו</td>
                            </tr>
                            <tr onclick="GetMoshikBranches(3);">
                                <td>זאפ רסט</td>
                            </tr>
                            <tr onclick="GetMoshikBranches(4);">
                                <td>משלוחה</td>
                            </tr>
                            <tr onclick="GetMoshikBranches(5);">
                                <td>סיבוס</td>
                            </tr>
                        </tbody>
                    </table>
                </div>

            </div>
        </div>
    </div>





    <div class="modal fade" id="divUpdateWorker" tabindex="-1" role="dialog" aria-labelledby="divUpdateWorkerTitle" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title text-center" id="divUpdateWorkerTitle">עובד</h4>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-xs-12 pull-right">
                            <div class="form-group row hide">
                                <label for="txtEditWorkerId" class="col-xs-3 input-lg pull-right">ID</label>
                                <div class="col-xs-9">
                                    <input type="text" class="form-control input-lg disabled" id="txtEditWorkerId" placeholder="workerId" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="txtEditWorkerFirstName" class="col-xs-3 input-lg pull-right">שם פרטי</label>
                                <div class="col-xs-9">
                                    <input type="text" class="form-control input-lg" id="txtEditWorkerFirstName" placeholder="שם פרטי" style="font-weight: bold" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="txtEditWorkerLastName" class="col-xs-3 input-lg pull-right">שם משפחה</label>
                                <div class="col-xs-9">
                                    <input type="text" class="form-control input-lg" id="txtEditWorkerLastName" placeholder="שם משפחה" style="font-weight: bold" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="txtEditWorkerPhone" class="col-xs-3 input-lg pull-right">טלפון</label>
                                <div class="col-xs-9">
                                    <input type="number" class="form-control input-lg" id="txtEditWorkerPhone" placeholder="טלפון" style="font-weight: bold" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="cboEditWorkerType" class="col-xs-3 input-lg pull-right">תפקיד</label>
                                <div class="col-xs-9">
                                    <select id="cboEditWorkerType" class="custom-select input-lg col-xs-12">
                                        <option value="1">מתכנת</option>
                                        <option value="2">תומך</option>
                                        <option value="3">טכני</option>
                                        <option value="4">תפריטים</option>
                                        <option value="5">בוס</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="txtEditWorkerUserName" class="col-xs-3 input-lg pull-right">שם משתמש</label>
                                <div class="col-xs-9">
                                    <input type="text" class="form-control input-lg" id="txtEditWorkerUserName" placeholder="שם משתמש" style="font-weight: bold" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="txtEditWorkerPassword" class="col-xs-3 input-lg pull-right">סיסמה</label>
                                <div class="col-xs-9">
                                    <input type="text" class="form-control input-lg" id="txtEditWorkerPassword" placeholder="סיסמה" style="font-weight: bold" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="cboEditWorkerUserType" class="col-xs-3 input-lg pull-right">סוג משתמש</label>
                                <div class="col-xs-9">
                                    <select id="cboEditWorkerUserType" class="custom-select input-lg col-xs-12">
                                        <option value="1">אדמין</option>
                                        <option value="2">תומך</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="txtEditWorkerPhone" class="col-xs-3 input-lg pull-right">שלוחה</label>
                                <div class="col-xs-9">
                                    <input type="number" class="form-control input-lg" id="txtEditWorkerShluha" placeholder="שלוחה" style="font-weight: bold" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="txtEditWorkerPhone" class="col-xs-3 input-lg pull-right">רכב</label>
                                <div class="col-xs-9">
                                    <input class="form-control input-lg" id="txtCarType" placeholder="סוג רכב" style="font-weight: bold" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="txtEditWorkerPhone" class="col-xs-3 input-lg pull-right">מספר רכב</label>
                                <div class="col-xs-9">
                                    <input class="form-control input-lg" id="txtCarNumber" placeholder="מספר רכב" style="font-weight: bold" />
                                </div>
                            </div>

                            <div class="form-group row">
                                <label for="cboEditWorkerUserActive" class="col-xs-3 input-lg pull-right">פעיל</label>
                                <div class="col-xs-9">
                                    <select id="cboEditWorkerUserActive" class="custom-select input-lg col-xs-12">
                                        <option value="true">פעיל</option>
                                        <option value="false">לא פעיל</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button class="btn btn-primary pull-left" onclick="UpdateWorker();return false;">אישור</button>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript" class="init">
        $(document).ready(function () {

        });

        function toggleDiv(divName) {
            $('#' + divName).slideToggle();
        }

        function showLoader() {
            $(".loader").css("display", "visible");
            $(".loader").fadeIn("slow");

        }

        function hideLoader() {
            $(".loader").css("display", "none");
            $(".loader").fadeOut("slow");
        }


        function SendPostLocal(commandName, dataInfo, onsuccess, onError) {
            //console.log("Start SendPost");
            $.ajax({
                type: "POST",
                url: "Settings.aspx/" + commandName,
                data: dataInfo,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: onsuccess,
                error: onError
            });


        }

        function showWorkers() {
            if ($('#divWorkers').is(':visible')) {
                toggleDiv('divWorkers');
                return;
            }

            showLoader();

            $("#tblWorkers tbody").empty();

            SendPostLocal("GetWorkers", '{}',
                function (response) {

                    var xmlDoc = $.parseXML(response.d);
                    var xml = $(xmlDoc);
                    var customers = xml.find("Table");

                    $.each(customers, function () {
                        var id = $(this).find("id").text();
                        var firstName = $(this).find("firstName").text();
                        var lastName = $(this).find("lastName").text();
                        var phone = $(this).find("phone").text();
                        //var birthDay = $(this).find("birthDay").text();
                        var workerTypeID = $(this).find("workerTypeID").text();
                        var workerTypeName = $(this).find("workerTypeName").text();

                        var userName = $(this).find("userName").text();
                        var password = $(this).find("password").text();
                        var userType = $(this).find("userType").text();
                        var shluha = $(this).find("shluha").text();
                        var carType = $(this).find("carType").text();
                        var carNumber = $(this).find("carNumber").text();


                        var active = $(this).find("active").text();
                        var activeS = "לא";
                        if (active == "true") {
                            activeS = "כן";
                        }
                        try {


                            var imgPath = $(this).find("imgPath").text();
                            console.log(imgPath);

                            //var d = new Date(birthDay);


                            var html = '<tr id="rowWorker' + id + '" class="clickable-row" >';
                            html = html + '<td>' + id + '</td>';
                            html = html + '<td>' + firstName + '</td>';
                            html = html + '<td>' + lastName + '</td>';
                            html = html + '<td>' + phone + '</td>';
                            html = html + '<td>' + workerTypeName + '</td>';
                            html = html + '<td>' + userName + '</td>';
                            html = html + '<td>' + password + '</td>';
                            html = html + '<td>' + userType + '</td>';
                            html = html + '<td>' + shluha + '</td>';
                            html = html + '<td>' + activeS + '</td>';
                            html = html + '<td><img src="Pics/workers/' + imgPath + '" style="width:30px;height:30px" /></td>';
                            html = html + '<td>' + carType + '</td>';
                            html = html + '<td>' + carNumber + '</td>';


                            html = html + '<td><a class="glyphicon glyphicon-pencil btn pull-left icon_info" title="עדכן עובד" onClick="showUpdateWorker(' + id + ')" /></td>';

                            html = html + '</tr>';

                            $(html).appendTo($("#tblWorkers tbody"));
                            //$("#cboWorkerActive" + id).val(active);

                        } catch (ec) {

                        }
                    });

                    //var table = $("#tblWorkers").DataTable();

                    //table
                    //    .rows()
                    //    .invalidate()
                    //    .draw();
                    //
                    hideLoader();

                    toggleDiv('divWorkers');
                });
        }

        function showUpdateWorker(workerId) {
            $("#txtEditWorkerId").val(workerId);
            $("#txtEditWorkerFirstName").val('');
            $("#txtEditWorkerLastName").val('');
            $("#txtEditWorkerPhone").val('');
            $("#cboEditWorkerType").val(2);
            $("#txtEditWorkerUserName").val('');
            $("#txtEditWorkerPassword").val('');
            $("#cboEditWorkerUserType").val(2);
            $("#txtEditWorkerShluha").val(0);
            $("#txtCarType").val('');
            $("#txtCarNumber").val('');
            

            $("#cboEditWorkerUserActive").val("true");

            if (workerId == 0) {
                $("#divUpdateWorker").modal('show');
            }
            else {
                SendPostLocal("GetWorker", '{workerId: ' + workerId + '}',
                    function (response) {

                        var xmlDoc = $.parseXML(response.d);
                        var xml = $(xmlDoc);
                        var customers = xml.find("Table");
                        console.log(customers.length);

                        $.each(customers, function () {
                            //[id], [firstName], [lastName], [phone], [birthDay], [workerTypeID], [userName], [password], [userTypeId], [active]
                            var id = $(this).find("id").text();
                            var firstName = $(this).find("firstName").text();
                            var lastName = $(this).find("lastName").text();
                            var phone = $(this).find("phone").text();
                            var workerTypeID = $(this).find("workerTypeID").text();
                            var userName = $(this).find("userName").text();
                            var password = $(this).find("password").text();
                            var userTypeId = $(this).find("userTypeId").text();
                            var shluha = $(this).find("shluha").text();
                            var carType = $(this).find("carType").text();
                            var carNumber = $(this).find("carNumber").text();
                            var active = $(this).find("active").text();
                            //var d = new Date(birthDay);


                            $("#txtEditWorkerId").val(id);
                            $("#txtEditWorkerFirstName").val(firstName);
                            $("#txtEditWorkerLastName").val(lastName);
                            $("#txtEditWorkerPhone").val(phone);
                            $("#cboEditWorkerType").val(workerTypeID);
                            $("#txtEditWorkerUserName").val(userName);
                            $("#txtEditWorkerUserName").val(userName);
                            $("#txtEditWorkerPassword").val(password);
                            $("#cboEditWorkerUserType").val(userTypeId);
                            $("#txtEditWorkerShluha").val(shluha);

                            $("#txtCarType").val(carType);
                            $("#txtCarNumber").val(carNumber);


                            //console.log(active);
                            $("#cboEditWorkerUserActive").val(active);

                        });


                        $("#divUpdateWorker").modal('show');
                    });
            }


        }

        function UpdateWorker() {
            var workerId = $("#txtEditWorkerId").val();
            var firstName = $("#txtEditWorkerFirstName").val();
            var lastName = $("#txtEditWorkerLastName").val();
            var phone = $("#txtEditWorkerPhone").val();
            var workerTypeID = $("#cboEditWorkerType").val();
            var userName = $("#txtEditWorkerUserName").val();
            var password = $("#txtEditWorkerPassword").val();
            var userTypeId = $("#cboEditWorkerUserType").val();
            var active = $("#cboEditWorkerUserActive").val();
            var shluha = $("#txtEditWorkerShluha").val();
            var carType = $("#txtCarType").val();
            var carNumber = $("#txtCarNumber").val();
            

            if (firstName.length < 1) {
                alert("אנא הזן שם פרטי");
                return;
            }

            if (lastName.length < 2) {
                alert("אנא הזן שם משפחה");
                return;
            }

            if (phone.length < 7) {
                alert("אנא הזן טלפון");
                return;
            }

            if (userName.length < 5) {
                alert("השם משתמש חייב להיות לפחות באורך 5 תוים");
                return;
            }

            if (password.length < 6) {
                alert("הסיסמה חייבת להיות לפחות באורך 6 תוים");
                return;
            }


            //UpdateWorker(workerId, firstName, lastName, phone, workerTypeID,userName, password, userTypeId, active)
            var params = '{workerId: ' + workerId + ',firstName: "' + firstName + '",lastName: "' + lastName + '",phone: "' + phone + '",workerTypeID: ' + workerTypeID +
                ',userName: "' + userName + '",password: "' + password + '", userTypeId: ' + userTypeId + ', shluha: ' + shluha + ',active: "' + active + '", carType: "' + carType + '", carNumber: "' + carNumber +'"}'


            console.log(params);
            SendPostLocal("UpdateWorker", params,
                function (response) {
                    $("#divUpdateWorker").modal('hide');
                    showWorkers();

                });
        }


        function showPlaces() {

            if ($('#divPlaces').is(':visible')) {
                toggleDiv('divPlaces');
                return;
            }

            showLoader();

            $("#tblPlaces tbody").empty();

            SendPostLocal("GetPlaces", '{}',
                function (response) {

                    var xmlDoc = $.parseXML(response.d);
                    var xml = $(xmlDoc);
                    var customers = xml.find("Table");
                    console.log(customers.length);

                    $.each(customers, function () {
                        //[id], [placeName], [ip], vip, workerId "+
                        var id = $(this).find("id").text();
                        var placeName = $(this).find("placeName").text();
                        var ip = $(this).find("ip").text();
                        var vip = $(this).find("vip").text();
                        var workerId = $(this).find("workerId").text();


                        var html = '<tr id="rowPlaceId' + id + '" class="clickable-row" >';
                        html = html + '<td>' + id + '</td>';
                        html = html + '<td>' + placeName + '</td>';
                        html = html + '<td><b>' + ip + '</b></td>';
                        html = html + '<td>' + vip + '</td>';
                        html = html + '<td>' + workerId + '</td>';
                        html = html + '<td></td>';
                        html = html + '</tr>';

                        $(html).appendTo($("#tblPlaces tbody"));
                    });

                    var table = $("#tblPlaces").DataTable();

                    table
                        .rows()
                        .invalidate()
                        .draw();

                    hideLoader();

                    toggleDiv('divPlaces');
                });
        }

        function showMoshikPlaces() {

            if ($('#divMoshikPlaces').is(':visible')) {
                toggleDiv('divMoshikPlaces');
                return;
            }

            toggleDiv('divMoshikPlaces');
            //showLoader();

            //SendPostLocal("GetMoshikBranchesForGroup", '{}',
            //    function (response) {

            //        var xmlDoc = $.parseXML(response.d);
            //        var xml = $(xmlDoc);
            //        var customers = xml.find("Table");
            //        console.log(customers.length);

            //        //$.each(customers, function () {
            //        //    //[id], [placeName], [ip], vip, workerId "+
            //        //    var id = $(this).find("id").text();
            //        //    var placeName = $(this).find("placeName").text();
            //        //    var ip = $(this).find("ip").text();
            //        //    var vip = $(this).find("vip").text();
            //        //    var workerId = $(this).find("workerId").text();


            //        //    var html = '<tr id="rowPlaceId' + id + '" class="clickable-row" >';
            //        //    html = html + '<td>' + id + '</td>';
            //        //    html = html + '<td>' + placeName + '</td>';
            //        //    html = html + '<td><b>' + ip + '</b></td>';
            //        //    html = html + '<td>' + vip + '</td>';
            //        //    html = html + '<td>' + workerId + '</td>';
            //        //    html = html + '<td></td>';
            //        //    html = html + '</tr>';

            //        //    $(html).appendTo($("#tblPlaces tbody"));
            //        //});

            //        //var table = $("#tblPlaces").DataTable();

            //        //table
            //        //    .rows()
            //        //    .invalidate()
            //        //    .draw();

            //        //hideLoader();

            //        //toggleDiv('divPlaces');
            //    });

            //hideLoader();
        }

        function GetMoshikBranches(groupId) {                        
            //console.log(v);

            $("#tblMoshikBranches tbody").empty();
            showLoader();
            SendPostLocal("GetMoshikBranchesForGroup", '{groupId: ' + groupId + '}',
                function (response) {
                    var res = JSON.parse(response.d);                                        
                    var customers = res.customers;
                    console.log(response.d);
                    //console.log(customers.length);
                    //return;
                    for (var i = 0; i < customers.length; i++) {
                        

                        var customerName = customers[i].customerName;//.find("customerName").text();
                        var branchName = customers[i].branchName;//$(this).find("branchName").text();
                        var branchId = customers[i].branchId;// $(this).find("branchId").text();

                        var html = '<tr>';
                        html = html + '<td>' + customerName + '</td>';
                        html = html + '<td>' + branchName + '</td>';
                        html = html + '<td><b>' + branchId + '</b></td>';
                        html = html + '</tr>';

                        $(html).appendTo($("#tblMoshikBranches tbody"));
                    }
        
                    var table = $("#tblMoshikBranches").DataTable();

                    table
                        .rows()
                        .invalidate()
                        .draw();

                    hideLoader();
                });
        }

    </script>

</asp:Content>

