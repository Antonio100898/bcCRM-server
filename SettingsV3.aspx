<%@ Page Title="לקוחות בשרת" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="SettingsV3.aspx.cs" Inherits="SettingsV3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<script src="http://code.jquery.com/jquery-1.9.1.js"></script>--%>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>

    <style>
        th {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 alert-info">
                חפש קבוצה                
                <asp:TextBox ID="txtSearchGroup" runat="server" />
                <asp:HiddenField ID="txtGroupId" runat="server" />
                <asp:Button ID="btnShowGroup" runat="server" Text="הצג" OnClick="btnShowGroup_Click" CssClass="btn btn-primary" Style="min-width: 100px" />
                <button id="btnShowAllGroups" type="button" class="btn btn-primary" style="margin-right: 50px" onclick="ShowDiv('divShowAllGroups')">הצג את כל הקבוצות</button>
                <button id="btnShowAddNewGroup" type="button" class="btn btn-primary" style="margin-right: 25px" onclick="ShowDiv('divAddNewGroup')">הוסף קבוצה חדשה</button>
                <asp:Button ID="btnShowBranches" runat="server" Text="חפש סניף" OnClick="btnShowBranches_Click" OnClientClick="showMsg('עוד לא עובד... מה אתה רוצה לחפש? נגיד סניף רעננה, אז תראה את כל הקבוצות שיש בהן סניף רעננה')" CssClass="btn btn-primary" Style="margin-right: 25px" />
                <button id="btnShowAddNewCity" type="button" class="btn btn-primary pull-left" style="margin-right: 25px" onclick="ShowDiv('divAddNewCity')">הוסף עיר חדשה</button>

            </div>

            <div class="alert-warning">
                <asp:Label ID="lblGroupName" runat="server" Text="Group Name" Style="margin-right: 25px" Font-Size="Large" CssClass="pull-right" />
                <asp:Label ID="lblGroupDatabaseName" runat="server" Text="Group Database" Style="margin-right: 25px" Font-Size="Large" />
            </div>

            <div id="divOverlay" class="web_dialog_overlay"></div>

            <div id="divOverlayserver" runat="server" class="web_dialog_overlay"></div>


            <div id="divShowAllGroups" class="web_dialog" style="vertical-align: top">
                <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="0">
                    <tr>
                        <td class="web_dialog_title">בחר קבוצה מהרשימה</td>
                        <td class="web_dialog_title align_left">
                            <a href="#" id="btnCloseAddNewLesson" onclick="HideDiv('divShowAllGroups')">ביטול</a>
                        </td>
                    </tr>
                </table>
                <asp:ListBox ID="lstGroups" runat="server" DataSourceID="dsGroups" DataTextField="groupName" Width="100%" DataValueField="id" Font-Size="XX-Large"></asp:ListBox>

                <asp:SqlDataSource runat="server" ID="dsGroups" ConnectionString='<%$ ConnectionStrings:BI_masterDBConnectionString %>'
                    SelectCommand="SELECT [id], [groupName], [databaseName] FROM [Groups] ORDER BY [groupName]"
                    UpdateCommand="UPDATE Groups SET groupName = @groupName WHERE (id = @id)">
                    <UpdateParameters>
                        <asp:Parameter Name="groupName" />
                        <asp:Parameter Name="id" />
                    </UpdateParameters>
                </asp:SqlDataSource>
                <asp:Button ID="btnSelectGroup" runat="server" Text="אישור" OnClick="btnSelectGroup_Click" Width="100%" CssClass="btn-primary" />
            </div>


            <div id="divAddNewGroup" class="web_dialog">
                <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="0">
                    <tr>
                        <td class="web_dialog_title">הוסף קבוצה חדשה</td>
                        <td class="web_dialog_title align_left">
                            <a href="#" id="btnHideShowAddNewGroup" onclick="HideDiv('divAddNewGroup')">ביטול</a>
                        </td>
                    </tr>
                </table>
                <div>
                    <b>שם הקבוצה</b><asp:TextBox ID="txtAddGrpopName" runat="server" />
                </div>

                <asp:Button ID="btnAddGroup" runat="server" Text="אישור" CssClass="btn-primary" Width="100%" OnClick="btnAddGroup_Click" />
            </div>

            <div id="divAddNewCity" class="web_dialog">
                <table style="width: 100%">
                    <tr>
                        <td class="web_dialog_title">הוסף עיר חדשה</td>
                        <td class="web_dialog_title align_left">
                            <a href="#" id="btnHideShowAddCity" onclick="HideDiv('divAddNewCity')">ביטול</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>עיר</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddCity" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Latitude</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLatitude" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Longitude</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLongitude" runat="server" Width="100%" />
                        </td>
                    </tr>
                </table>
                <asp:Button ID="btnAddCity" runat="server" Text="אישור" OnClick="btnAddCity_Click" CssClass="btn-primary" Width="100%" />
            </div>

            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 pull-right alert-info">
                <h3>סניפים</h3>
                <button id="btnShowAddNewBranch" type="button" class="btn-primary" onclick="ShowDiv('divAddNewBranch')">הוסף סניף חדש</button>
                <asp:GridView ID="grdBranches" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="Id" AllowSorting="True"
                    OnSelectedIndexChanged="grdBranches_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" SelectText="בחר" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEditBranch" runat="server" Text="ערוך" CssClass="btn-primary" OnClick="btn_EditBranch_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                        <asp:BoundField DataField="branchName" HeaderText="סניף" SortExpression="branchName" />
                        <asp:BoundField DataField="address" HeaderText="כתובת" SortExpression="address" />
                        <asp:BoundField DataField="city" HeaderText="עיר" SortExpression="city" />
                        <asp:BoundField DataField="biCommEmail" HeaderText="Email" SortExpression="biCommEmail" />
                        <asp:CheckBoxField DataField="kosher" HeaderText="כשר" SortExpression="kosher" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ip" HeaderText="ip" SortExpression="ip" />
                        <asp:BoundField DataField="name" HeaderText="עיר" SortExpression="name" />
                    </Columns>
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>

            </div>

            <div id="divAddNewBranch" class="web_dialog">
                <table style="width: 100%">
                    <tr>
                        <td class="web_dialog_title">הוסף סניף חדש </td>
                        <td class="web_dialog_title align_left">
                            <a href="#" onclick="HideDiv('divAddNewBranch')">ביטול</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>סניף</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddBramchName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>IP</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddBranchIP" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>עיר</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="cboAddBranchCity" runat="server" DataSourceID="dsCity" DataTextField="name" DataValueField="Id" Width="100%" />
                            <asp:SqlDataSource runat="server" ID="dsCity" ConnectionString='<%$ ConnectionStrings:BI_masterDBConnectionString %>'
                                SelectCommand="SELECT [Id], [name] FROM [cities] ORDER BY [name]"></asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>רחוב ומספר</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddBranchAddress" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Email</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>כשר</b>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkAddBranchKosher" runat="server" Checked="false" />
                        </td>
                    </tr>
                </table>
                <asp:Button ID="btnAddBranch" runat="server" Text="הוסף סניף" Width="100%" CssClass="btn-primary" OnClick="btnAddBranch_Click" />

            </div>

            <div id="divEditBranch" runat="server" class="web_dialog">
                <table style="width: 100%">
                    <tr>
                        <td class="web_dialog_title">ערוך סניף</td>
                    </tr>
                    <table>
                        <tr>
                            <td>
                                <b>שם הסניף</b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUpdateBranchName" runat="server" Width="100%" />
                                <asp:HiddenField ID="txtUpdateBranchId" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>עיר</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboUpdateBranchCity" runat="server" DataSourceID="dsCity" DataTextField="name" DataValueField="Id" Width="100%" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>כתובת</b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUpdateBranchAddress" runat="server" Width="100%" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Email</b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUpdateBranchEmail" runat="server" Width="100%" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>IP</b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUpdateBranchIP" runat="server" Width="100%" />
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <b>כשר</b>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkUpdateBranchKosher" runat="server" Checked="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnCancelUpdateBranch" runat="server" Text="ביטול" Width="100%" CssClass="btn-warning" OnClick="btnCancelUpdateBranch_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnUpdateBranch" runat="server" Text="אישור" Width="100%" CssClass="btn-success" OnClick="btnUpdateBranch_Click" />
                            </td>
                        </tr>
                    </table>
                </table>
            </div>



            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 alert-info">
                <h3>עמדות</h3>
                <button id="btnShowAddNewPos" type="button" class="btn-primary" onclick="ShowDiv('divAddNewPos')">הוסף עמדה חדשה</button>
                <asp:GridView ID="grdPos" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="Id">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnShowEditPos" runat="server" Text="ערוך" CssClass="btn-primary" OnClick="btnShowEditPos_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" InsertVisible="False" SortExpression="Id"></asp:BoundField>
                        <asp:BoundField DataField="udid" HeaderText="udid" SortExpression="udid"></asp:BoundField>
                        <asp:BoundField DataField="dbVersion" HeaderText="dbVersion" SortExpression="dbVersion"></asp:BoundField>
                        <asp:BoundField DataField="posName" HeaderText="עמדה" SortExpression="posName"></asp:BoundField>
                        <asp:BoundField DataField="branchName" HeaderText="סניף" SortExpression="branchName"></asp:BoundField>
                        <asp:BoundField DataField="deductServiceFromTotal" HeaderText="הורד טיפים מהסהכ" SortExpression="deductServiceFromTotal"></asp:BoundField>
                        <asp:BoundField DataField="deductServiceFromCash" HeaderText="הורד טיפים מהמזומן" SortExpression="deductServiceFromCash"></asp:BoundField>
                        <asp:BoundField DataField="deductServiceFromCredit" HeaderText="הורד טיפים מהאשראי" SortExpression="deductServiceFromCredit"></asp:BoundField>
                        <asp:BoundField DataField="deductCouponComissionFromTotal" HeaderText="הורד עמלות שוברים מהסהכ" SortExpression="deductCouponComissionFromTotal"></asp:BoundField>
                        <asp:BoundField DataField="deductCouponComissionFromCouponTotal" HeaderText="הורד עמלות שוברים מהשוברים" SortExpression="deductCouponComissionFromCouponTotal"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>

            <div id="divAddNewPos" class="web_dialog">
                <table style="width: 100%">
                    <tr>
                        <td class="web_dialog_title">הוסף עמדה חדשה </td>
                        <td class="web_dialog_title align_left">
                            <a href="#" onclick="HideDiv('divAddNewPos')">ביטול</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>עמדה</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddPosName" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>סניף</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="cboAddPosBranches" runat="server" DataTextField="branchName" DataValueField="Id" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>הורד טיפים מהסהכ</b>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkConfigZSumTotalWithoutService" runat="server" Checked="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>הורד טיפים מהמזומן</b>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkConfigZSumCashWithoutService" runat="server" Checked="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>הורד טיפים מהאשראי</b>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkConfigZSumCreditWithoutService" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>הורד עמלות שוברים מהסהכ</b>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkConfigZSumTotalWithoutCuponCommision" runat="server" Checked="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>הורד עמלות שוברים מהשוברים</b>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkConfigZSumCuponWithoutCuponCommision" runat="server" Checked="true" />
                        </td>
                    </tr>
                </table>
                <asp:Button ID="btnAddPos" runat="server" Text="הוסף עמדה" Width="100%" CssClass="btn-primary" OnClick="btnAddPos_Click" />
            </div>


            <div id="divEditPos" runat="server" class="web_dialog">
                <table style="width: 100%">
                    <tr>
                        <td class="web_dialog_title">ערוך פוס</td>
                    </tr>
                    <table>
                        <tr>
                            <td>
                                <b>udid</b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUpdatePosUdid" runat="server" Width="100%" />
                                <asp:HiddenField ID="txtUpdatePosID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>dbVersion</b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUpdatePosDbVersion" runat="server" Width="100%" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>שם העמדה</b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUpdatePosName" runat="server" Width="100%" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>סניף</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboUpdatePosAnotherBranch" runat="server" DataTextField="branchName" DataValueField="Id"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>הורד טיפים מהסהכ</b>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkUpdatePosTotalNoService" runat="server" Checked="false" />
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <b>הורד טיפים מהמזומן</b>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkUpdatePosCashNoService" runat="server" Checked="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>הורד טיפים מהאשראי</b>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkUpdatePosCreditNoService" runat="server" Checked="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>הורד עמלות שוברים מהסהכ</b>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkUpdatePosTotalNoCommision" runat="server" Checked="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>הורד עמלות שוברים מהשוברים</b>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkUpdatePosCuponNoCommision" runat="server" Checked="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnCancelUpdatePos" runat="server" Text="ביטול" Width="100%" CssClass="btn-warning" OnClick="btnCancelUpdatePos_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnUpdatePos" runat="server" Text="אישור" Width="100%" CssClass="btn-success" OnClick="btnUpdatePos_Click" />
                            </td>
                        </tr>
                    </table>
                </table>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        $(document).ready(function () {
            GetGroups();
        });

        function pageLoad(sender, args) {
            $(document).ready(function () {

                // put all your javascript functions here 
                $(function () {
                    $("[id$=txtSearchGroup]").autocomplete({
                        source: function (request, response) {
                            var sss = request.term;
                            sss = sss.trim().replace(/\\/g, "\\\\").replace(/'/g, "\\'").replace(/"/g, '\\"');
                            $.ajax({
                                url: '<%=ResolveUrl("~/SettingsV3.aspx/GetGroups") %>',
                                data: "{ 'groupName': '" + sss + "'}",
                                dataType: "json",
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                success: function (data) {
                                    response($.map(data.d, function (item) {
                                        return {
                                            label: item.name,
                                            val: item.id
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
                            event.preventDefault();
                            $("[id$=txtGroupId]").val(i.item.val);
                            $("[id$=txtSearchGroup]").val(i.item.value);

                        },
                        minLength: 1
                    });
                });
            });
        }



        function ShowDiv(divName) {
            $("#divOverlay").show();
            $("#" + divName).fadeIn(300);
            $("#divOverlay").unbind("click");
        }

        function HideDiv(divName) {
            $("#divOverlay").hide();
            $("#" + divName).fadeOut(300);
        }

        function showMsg(msg) {
            alert(msg);
            return true;
        }


        function SendPostLocal(commandName, dataInfo, onsuccess, onError) {
            //showLoader();

            //console.log("Start SendPost");
            $.ajax({
                type: "POST",
                url: "SettingsV3.aspx/" + commandName,
                data: dataInfo,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: onsuccess,
                error: onError
            });
        }

        function GetGroups() {
            $("#cboGroups").empty();
            $('#cboGroups').append($("<option></option>").attr("value", 0).text('בחר סניף'));

            SendPostLocal("GetAllGroups", '{}', function (response) {
                var customers = response.d;

                $(customers).each(function () {
                    var id = this.id;
                    var groupName = this.name;

                    $('#cboGroups').append($("<option></option>").attr("value", id).text(groupName + " - " + id));
                });


                $.each(response.d, function () {

                });
            })
        }

        function GetBranches() {
            var groupId = $("#cboGroups").val();

            if (groupId==0) {
                alert("אנא בחר סניף");
                return false;
            }

            $("#tblBranches tbody").empty();
            SendPostLocal("GetBranches", '{groupId: ' + groupId + '}', function (response) {
                var branches = response.d;
                console.log(branches);
                $(branches).each(function () {
                    //branches.Id, branches.branchName, branches.address, branches.city, branches.biCommEmail, branches.kosher, branches.ip, branches.cityId, [BI_masterDB].[dbo].[cities].name " +

                    var id = this.id;
                    var branchName = this.branchName;
                    var address = this.address;
                    var city = this.city;
                    var biCommEmail = this.biCommEmail;
                    var kosher = this.kosher;
                    var ip = this.ip;
                    var cityId = this.cityId;
                    var cityName = this.cityName;
                   
                    console.log(branchName);

                    var html = '<tr id="rowBranch' + id + '" class="clickable-row>';
                    html = html + '<td></td>';
                    html = html + '<td>' + branchName + '</td>';
                    html = html + '<td><b>' + address + '</b></td>';
                    html = html + '<td>' + city + '</td>';
                    html = html + '<td>' + biCommEmail + '</td>';
                    html = html + '<td>' + kosher + '</td>';
                    html = html + '<td>' + ip + '</td>';
                    html = html + '<td>' + cityName + '</td>';
                    html = html + '<td></td>';

                    html = html + '</tr>';

                    console.log(html);

                    $(html).appendTo($("#tblBranches tbody"));
                });
            });
        }
    </script>
</asp:Content>