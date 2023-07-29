<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="SettingsBiV3.aspx.cs" Inherits="SettingsBiV3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <style>
        th {
            text-align: center;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="panel">
        <div class="panel-body">
            <div class="row">
                <select id="cboGroups" class="input-lg"></select>
                <button class="btn btn-primary btn-lg" onclick="GetBranches();GetGroupDatabaseName();return false;">הצג</button>

                <button class="btn btn-primary btn-lg" onclick="UpdateGroup(0);return false;">הוסף חדש</button>
                <label id="lblDatabase" style="font-size:large;margin-right:5px"></label>
            </div>
            <div class="row text-center">
                <table id="tblBranches" class="table table-bordered table-hover text-center">
                    <thead>
                        <tr>
                            <th></th>
                            <th>ID</th>
                            <th>סניף</th>
                            <th>IP</th>
                            <th>כשר</th>
                            <th>עיר</th>
                            <th>כתובת</th>
                            <th>Email</th>
                            <th>עיר</th>
                            <th>
                                <button class="btn btn-primary" onclick="ShowUpdateBranch(0);return false;">הוסף</button></th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalEditBranch" tabindex="-1" role="dialog" aria-labelledby="modalEditBranchTitle" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalEditBranchTitle">עדכן סניף</h5>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <input type="text" class="input-lg col-xs-9" id="txtEditBranchID" readonly />
                        <label for="txtEditBranchID" class="input-lg col-xs-3">ID</label>
                    </div>
                    <div class="form-group">
                        <input type="text" class="input-lg col-xs-9" id="txtEditBranchName" />
                        <label for="txtEditBranchName" class="input-lg col-xs-3">סניף</label>
                    </div>
                    <div class="form-group">
                        <input type="text" class="input-lg col-xs-9" id="txtEditBranchIp" />
                        <label for="txtEditBranchIp" class="input-lg col-xs-3">IP</label>
                    </div>
                    <div class="form-group">
                        <select id="cboEditBranchKosher" class="dropdown input-lg col-xs-9">
                            <option value="0">לא</option>
                            <option value="1">כן</option>
                        </select>
                        <label for="cboEditBranchKosher" class="dropdown input-lg col-xs-3">כשר</label>
                    </div>
                    <div class="form-group">
                        <select id="cboAddBranchCity" class="dropdown input-lg col-xs-9">
                        </select>
                        <label for="cboAddBranchCity" class="dropdown input-lg col-xs-3">עיר</label>
                    </div>
                    <div class="form-group">
                        <input type="text" class="input-lg col-xs-9" id="txtAddBranchAddress" />
                        <label for="txtAddBranchAddress" class="input-lg col-xs-3">כתובת</label>
                    </div>
                    <div class="form-group">
                        <input type="text" class="input-lg col-xs-9" id="txtAddBranchEmail" />
                        <label for="txtAddBranchEmail" class="input-lg col-xs-3">Email</label>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary btn-lg" data-dismiss="modal">ביטול</button>
                    <button type="button" class="btn btn-primary btn-lg pull-left" onclick="UpdateBranch()">אישור</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal  fade" id="modalShowPoses" tabindex="-1" role="dialog" aria-labelledby="modalShowPosesTitle" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalShowPosesTitle">עמדות לסניף</h5>
                </div>
                <div class="modal-body">

                    <table id="tblPoses" class="table table-bordered table-hover text-center">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>עמדה</th>
                                <th>dbVersion</th>
                                <th>udid</th>
                                <th>הורד טיפים מהסהכ</th>
                                <th>הורד טיפים מהמזומן</th>
                                <th>הורד טיפים מהאשראי</th>
                                <th>הורד עמלות שוברים מהסהכ</th>
                                <th>הורד עמלות שוברים מהשוברים</th>
                                <th>
                                    <button class="btn btn-primary" onclick="ShowUpdatePos(0);return false;">הוסף</button></th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>

                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="modalEditPos" tabindex="-1" role="dialog" aria-labelledby="modalEditPosTitle" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalEditPosTitle">עדכן עמדה</h5>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <input type="text" class="input-lg col-xs-7" id="txtEditPosID" readonly />
                        <label for="txtEditPosID" class="input-lg col-xs-5">ID</label>
                    </div>
                    <div class="form-group">
                        <input type="text" class="input-lg col-xs-7" id="txtEditPosName" />
                        <label for="txtEditPosName" class="input-lg col-xs-5">עמדה</label>
                    </div>
                    <div class="form-group">
                        <input type="number" class="input-lg col-xs-7" id="txtEditPosDbVersion" />
                        <label for="txtEditPosDbVersion" class="input-lg col-xs-5">DbVersion</label>
                    </div>
                    <div class="form-group">
                        <input type="number" class="input-lg col-xs-7" id="txtEditPosUdid" />
                        <label for="txtEditPosUdid" class="input-lg col-xs-5">udid</label>
                    </div>
                    <div class="form-group">
                        <select id="cboEditPosTotalNoTips" class="dropdown input-lg col-xs-7">
                            <option value="0">לא</option>
                            <option value="1">כן</option>
                        </select>
                        <label for="cboEditPosTotalNoTips" class="dropdown input-lg col-xs-5">הורד טיפים מהסהכ</label>
                    </div>
                    <div class="form-group">
                        <select id="cboEditPosCashNoTips" class="dropdown input-lg col-xs-7">
                            <option value="0">לא</option>
                            <option value="1">כן</option>
                        </select>
                        <label for="cboEditPosCashNoTips" class="dropdown input-lg col-xs-5">הורד טיפים מהמזומן</label>
                    </div>
                    <div class="form-group">
                        <select id="cboEditPosCreditNoTips" class="dropdown input-lg col-xs-7">
                            <option value="0">לא</option>
                            <option value="1">כן</option>
                        </select>
                        <label for="cboEditPosCreditNoTips" class="dropdown input-lg col-xs-5">הורד טיפים מהאשראי</label>
                    </div>
                    <div class="form-group">
                        <select id="cboEditPosTotalNoCuponCommision" class="dropdown input-lg col-xs-7">
                            <option value="0">לא</option>
                            <option value="1">כן</option>
                        </select>
                        <label for="cboEditPosTotalNoCuponCommision" class="dropdown input-lg col-xs-5">הורד עמלות שוברים מהסהכ</label>
                    </div>
                    <div class="form-group">
                        <select id="cboEditPosCupponNoCuponCommision" class="dropdown input-lg col-xs-7">
                            <option value="0">לא</option>
                            <option value="1">כן</option>
                        </select>
                        <label for="cboEditPosCupponNoCuponCommision" class="dropdown input-lg col-xs-5">הורד עמלות מהשוברים</label>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary btn-lg pull-right" data-dismiss="modal">ביטול</button>
                    <button type="button" class="btn btn-primary btn-lg pull-left" onclick="UpdatePos()">אישור</button>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        $(document).ready(function () {
            GetGroups();
            GetCities();
        });

        function SendPostLocal(commandName, dataInfo, onsuccess, onError) {
            //showLoader();

            //console.log("Start SendPost");
            $.ajax({
                type: "POST",
                url: "SettingsBiV3.aspx/" + commandName,
                data: dataInfo,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: onsuccess,
                error: onError
            });


        }

        function GetCities() {
            $("#cboAddBranchCity").empty();
            $('#cboAddBranchCity').append($("<option></option>").attr("value", 0).text('בחר עיר'));

            SendPostLocal("GetCities", '{}', function (response) {
                var customers = response.d;

                $(customers).each(function () {
                    var id = this.id;
                    var cityName = this.cityName;

                    $('#cboAddBranchCity').append($("<option></option>").attr("value", id).text(cityName));
                });
            })
        }


        function GetGroups() {
            $("#cboGroups").empty();
            $('#cboGroups').append($("<option></option>").attr("value", 0).text('בחר סניף'));

            SendPostLocal("GetGroups", '{}', function (response) {
                var customers = response.d;

                $(customers).each(function () {
                    var id = this.id;
                    var groupName = this.name;

                    $('#cboGroups').append($("<option></option>").attr("value", id).text(groupName + " (" + id+")"));
                });
            })
        }

        function UpdateGroup(groupId) {
            if (groupId == 0) {
                var groupName = prompt("הזן את שם הקבוצה החדשה");

                if (groupName == null) {
                    return;
                }

                if (groupName == '') {
                    return;
                }

                SendPostLocal("UpdateGroup", '{groupId: ' + groupId + ', groupName: "' + groupName + '"}', function (response) {
                    GetGroups();
                });
            }
        }

        function GetBranches() {
            var groupId = $("#cboGroups").val();

            if (groupId == 0) {
                alert("אנא בחר סניף");
                return false;
            }

            $("#tblBranches tbody").empty();
            SendPostLocal("GetBranches", '{groupId: ' + groupId + '}', function (response) {
                var branches = response.d;

                $(branches).each(function () {

                    var id = this.id;
                    var branchName = this.branchName;
                    var address = this.address;
                    var city = this.city;
                    var biCommEmail = this.biCommEmail;
                    var kosher = this.kosher;
                    var ip = this.ip;
                    var cityId = this.cityId;
                    var cityName = this.cityName;


                    var kosherS = "לא";
                    if (kosher) {
                        kosherS = "כן";
                    }

                    //console.log(kosher);

                    var html = '<tr id="rowBranch' + id + '" class="clickable-row">';
                    html = html + '<td><img src="Pics/plus.png" style="max-height:20px;max-width:20px" onclick="ShowPoses(' + id + ')"/></td>';
                    html = html + '<td>' + id + '</td>';
                    html = html + '<td><b>' + branchName + '</b></td>';
                    html = html + '<td>' + ip + '</td>';
                    html = html + '<td>' + kosherS + '</td>';
                    html = html + '<td>' + city + '</td>';
                    html = html + '<td>' + address + '</td>';
                    html = html + '<td>' + biCommEmail + '</td>';
                    html = html + '<td>' + cityName + '</td>';
                    html = html + '<td><button type=\"button\" class=\"btn btn-primary\" onclick=\"ShowUpdateBranch(' + id + ')\">ערוך</button></td>';

                    html = html + '</tr>';


                    $(html).appendTo($("#tblBranches tbody"));

                });


            });
        }


        function GetGroupDatabaseName() {
            var groupId = $("#cboGroups").val();

            if (groupId == 0) {
                alert("אנא בחר סניף");
                return false;
            }
                        
            SendPostLocal("GetGroupDatabaseName", '{groupId: ' + groupId + '}', function (response) {
                var branches = response.d;
                console.log(branches);
                $("#lblDatabase").text(branches);
            });
        }


        function ShowUpdateBranch(branchId) {
            //console.log("ShowUpdateBranch: " + branchId);
            $("#txtEditBranchID").val(branchId);

            if (branchId == 0) {
                $("#txtEditBranchName").val('');
                $("#txtEditBranchIp").val('');
                $("#txtAddBranchAddress").val('');
                $("#cboAddBranchCity").val(0);
                $("#txtAddBranchEmail").val('123@123.com');
                $("#cboEditBranchKosher").val(1);

                $("#modalEditBranch").modal();
            }
            else {
                SendPostLocal("GetBranch", '{branchId: ' + branchId + '}', function (response) {
                    var branch = response.d;
                    //console.log(branch.id);
                    $("#txtEditBranchName").val(branch.branchName);
                    $("#txtEditBranchIp").val(branch.ip);
                    $("#txtAddBranchAddress").val(branch.address);
                    $("#cboAddBranchCity").val(branch.cityId);
                    $("#txtAddBranchEmail").val(branch.biCommEmail);

                    var kosherS = 0;
                    if (branch.kosher) {
                        kosherS = 1;
                    }
                    $("#cboEditBranchKosher").val(kosherS);

                    $("#modalEditBranch").modal();
                });
            }
        }

        function UpdateBranch() {
            //UpdateBranch(int branchId, string branchName, string ip, bool kosher, int cityId, string cityName, string address, string email)
            var branchId = $("#txtEditBranchID").val();
            var branchName = $("#txtEditBranchName").val();
            var ip = $("#txtEditBranchIp").val();
            var address = $("#txtAddBranchAddress").val();
            var cityId = $("#cboAddBranchCity").val();

            var email = $("#txtAddBranchEmail").val();
            var k = $("#cboEditBranchKosher").val();
            var kosher = false;
            if (k == 1) {
                kosher = true;
            }

            if (branchName == null) {
                alert("אנא הזן שם סניף");
                return;
            }

            if (branchName.length == 0) {
                alert("אנא הזן שם סניף");
                return;
            }

            if (cityId==0) {
                alert("אנא בחר עיר");
                return;
            }

            var params = '{branchId: ' + branchId + ', branchName: "' + branchName + '", ip: "' + ip + '", kosher: "' + kosher + '", ' +
                                'cityId: ' + cityId + ', address: "' + address + '", email: "' + email + '"}';
            //console.log(params);
            SendPostLocal("UpdateBranch", params, function (response) {
                $("#modalEditBranch").modal('hide');
                GetBranches();
            });
        }

        function ShowPoses(branchId) {
            $("#modalShowPoses").modal('hide');

            $("#txtEditBranchID").val(branchId);

            var branchName = $("#rowBranch" + branchId + " td:eq(2)").text();
            console.log(branchName);
            $("#modalShowPosesTitle").text("עמדות ל" + branchName);

            $("#tblPoses tbody").empty();
            SendPostLocal("GetPoses", '{branchId: ' + branchId + '}', function (response) {
                var poses = response.d;

                $(poses).each(function () {

                    var id = this.id;
                    var udid = this.udid;
                    var dbVersion = this.dbVersion;
                    var posName = this.posName;
                    var deductServiceFromTotal = this.deductServiceFromTotal ? "כן" : "לא";
                    var deductServiceFromCash = this.deductServiceFromCash ? "כן" : "לא";
                    var deductServiceFromCredit = this.deductServiceFromCredit ? "כן" : "לא";
                    var deductCouponComissionFromTotal = this.deductCouponComissionFromTotal ? "כן" : "לא";
                    var deductCouponComissionFromCouponTotal = this.deductCouponComissionFromCouponTotal ? "כן" : "לא";


                    //console.log(kosher);

                    var html = '<tr id="rowPos' + id + '" class="clickable-row">';
                    html = html + '<td>' + id + '</td>';
                    html = html + '<td><b>' + posName + '</b></td>';
                    html = html + '<td>' + dbVersion + '</td>';
                    html = html + '<td>' + udid + '</td>';
                    html = html + '<td>' + deductServiceFromTotal + '</td>';
                    html = html + '<td>' + deductServiceFromCash + '</td>';
                    html = html + '<td>' + deductServiceFromCredit + '</td>';
                    html = html + '<td>' + deductCouponComissionFromTotal + '</td>';
                    html = html + '<td>' + deductCouponComissionFromCouponTotal + '</td>';
                    html = html + '<td><button type=\"button\" class=\"btn btn-primary\" onclick=\"ShowUpdatePos(' + id + ')\">ערוך</button></td>';

                    html = html + '</tr>';


                    $(html).appendTo($("#tblPoses tbody"));

                });

                $("#modalShowPoses").modal();
            });
        }

        function ShowUpdatePos(posId) {

            $("#txtEditPosID").val(posId);

            if (posId == 0) {

                $("#txtEditPosName").val('');
                $("#txtEditPosDbVersion").val(0);
                $("#txtEditPosUdid").val(0);
                $("#cboEditPosTotalNoTips").val(0);
                $("#cboEditPosCashNoTips").val(0);
                $("#cboEditPosCreditNoTips").val(0);
                $("#cboEditPosTotalNoCuponCommision").val(0);
                $("#cboEditPosCupponNoCuponCommision").val(0);

                $("#modalEditPos").modal();
            }
            else {
                SendPostLocal("GetPos", '{posId: ' + posId + '}', function (response) {
                    var pos = response.d;
                    $("#txtEditPosName").val(pos.posName);
                    $("#txtEditPosDbVersion").val(pos.dbVersion);
                    $("#txtEditPosUdid").val(pos.udid);
                    $("#cboEditPosTotalNoTips").val(pos.deductServiceFromTotal ? 1 : 0);
                    $("#cboEditPosCashNoTips").val(pos.deductServiceFromCash ? 1 : 0);
                    $("#cboEditPosCreditNoTips").val(pos.deductServiceFromCredit ? 1 : 0);
                    $("#cboEditPosTotalNoCuponCommision").val(pos.deductCouponComissionFromTotal ? 1 : 0);
                    $("#cboEditPosCupponNoCuponCommision").val(pos.deductCouponComissionFromCouponTotal ? 1 : 0);

                    $("#modalEditPos").modal();
                });
            }
        }


        function UpdatePos() {
            var branchId = $("#txtEditBranchID").val();
            var posId = $("#txtEditPosID").val();

            var posName = $("#txtEditPosName").val();
            var dbVersion = $("#txtEditPosDbVersion").val();
            var udid = $("#txtEditPosUdid").val();
            var deductServiceFromTotal = $("#cboEditPosTotalNoTips").val() == 1 ? true : false;
            var deductServiceFromCash = $("#cboEditPosCashNoTips").val() == 1 ? true : false;
            var deductServiceFromCredit = $("#cboEditPosCreditNoTips").val() == 1 ? true : false;
            var deductCouponComissionFromTotal = $("#cboEditPosTotalNoCuponCommision").val() == 1 ? true : false;
            var deductCouponComissionFromCouponTotal = $("#cboEditPosCupponNoCuponCommision").val() == 1 ? true : false;

            if (posName==null) {
                alert("אנא הזן שם עמדה");
                return;
            }

            if (posName.length == 0) {
                alert("אנא הזן שם עמדה");
                return;
            }

            //posId, branchId,udid, dbVersion, string posName,
            // deductServiceFromTotal, deductServiceFromCash, deductServiceFromCredit, deductCouponComissionFromTotal, deductCouponComissionFromCouponTotal

            var params = '{posId: ' + posId + ', branchId: ' + branchId + ', udid: ' + udid + ', dbVersion: ' + dbVersion + ', posName: "' + posName + '", deductServiceFromTotal: "' + deductServiceFromTotal + '", ' +
                            'deductServiceFromCash: "' + deductServiceFromCash + '", deductServiceFromCredit: "' + deductServiceFromCredit + '", deductCouponComissionFromTotal: "' + deductCouponComissionFromTotal + '", deductCouponComissionFromCouponTotal: "' + deductCouponComissionFromCouponTotal + '"}';

            console.log(params);
            SendPostLocal("UpdatePos", params, function (response) {
                $("#modalEditPos").modal('hide');
                $("#modalShowPoses").modal('hide');
                ShowPoses(branchId);
            });

        }
    </script>
</asp:Content>

