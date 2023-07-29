<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="FindProblem.aspx.cs" Inherits="FindProblem"
    MaintainScrollPositionOnPostback="true" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

        td {
            word-wrap: break-word;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="loader" style="display: none"></div>
    <div class="panel">
        <div class="row">
            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-2 pull-right">
                <input type="text" id="txtFindStartDate" name="txtFindStartDate" class="col-xs-12 h4 input-lg" />
            </div>
            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-2 pull-right">
                <input type="text" id="txtFindFinishDate" name="txtFindFinishDate" class="col-xs-12 h4 input-lg" />
            </div>
            <div class="col-xs-12 col-sm-6 col-md-3 col-lg-2 pull-right">
                <asp:DropDownList ID="cboFindWorker" runat="server" class="dropdown col-xs-12 h4 input-lg" />
            </div>
            <div class="col-xs-12 col-sm-6 col-md-3 col-lg-2 pull-right">
                <asp:TextBox ID="txtFindPhone" runat="server" placeholder="טלפון" class="col-xs-12 h4 input-lg" />
                <asp:HiddenField ID="txtFindPhoneID" runat="server" />
            </div>
            <div class="col-xs-12 col-sm-6 col-md-3 col-lg-2 pull-right">
                <asp:TextBox ID="txtFindPlaceName" runat="server" placeholder="מקום" class="col-xs-12 h4 input-lg" />
                <asp:HiddenField ID="txtFindPlaceNameId" runat="server" />
            </div>
            <div class="col-xs-12 col-sm-6 col-md-3 col-lg-2 center-block">
                <asp:Button ID="btnFind" runat="server" Text="חפש" OnClick="btnFind_Click" class="btn btn-primary btn-lg col-xs-12 h4" OnClientClick="showLoader();" />
                <%--<label id="lbl" runat="server" />--%>
            </div>
        </div>

        <div class="row">
                <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 pull-right">                    
                    <asp:DropDownList ID="cboFindStatus" runat="server" class="col-xs-12 h4 input-lg">
                        <asp:ListItem Value="-1" Text="סטטוס" />
                        <asp:ListItem Value="0" Text="ממתין" />
                        <asp:ListItem Value="1" Text="בטיפול" />
                        <asp:ListItem Value="2" Text="סגור" />
                    </asp:DropDownList>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 pull-right">                    
                    <asp:DropDownList ID="cboFindEmergencyType" runat="server" class="col-xs-12 h4 input-lg">
                        <asp:ListItem Value="-1" Text="דחיפות" />
                        <asp:ListItem Value="0" Text="רגיל" />
                        <asp:ListItem Value="1" Text="דחוף" />
                        <asp:ListItem Value="2" Text="בעדיפות" />
                        <asp:ListItem Value="3" Text="תקלה מהלילה" />
                    </asp:DropDownList>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 pull-right">                    
                    <asp:DropDownList ID="cboFindDepartment" runat="server" class="col-xs-12 h4 input-lg">
                        <asp:ListItem Value="-1" Text="מחלקה" />
                        <asp:ListItem Value="0" Text="פרטי" />
                        <asp:ListItem Value="1" Text="כללי" />
                        <asp:ListItem Value="2" Text="טכני" />
                        <asp:ListItem Value="3" Text="תוכנה" />
                        <asp:ListItem Value="4" Text="תפריטים" />
                        <asp:ListItem Value="5" Text="איפוסים" />
                        <asp:ListItem Value="6" Text="שדרוגים" />
                        <asp:ListItem Value="7" Text="הנהלת חשבונות" />
                        <asp:ListItem Value="8" Text="שיווק" />
                        <asp:ListItem Value="9" Text="לחזור ללקוח" />
                        <asp:ListItem Value="10" Text="ציוד" />
                        <asp:ListItem Value="11" Text="יוזרים" />
                    </asp:DropDownList>
                </div>
                <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 pull-right">                    
                    <asp:DropDownList ID="cboFindToWorker" runat="server" class="col-xs-12 h4 input-lg" />
                </div>
                <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 pull-right">                    
                    <asp:TextBox ID="txtFindProblemDesc" runat="server" class="col-xs-12 h4 input-lg" placeholder="בעיה"  />
                </div>
                <div class="col-xs-12 col-sm-6 col-md-2 col-lg-2 pull-right">                    
                    <asp:TextBox ID="txtFindProblemSol" runat="server" class="col-xs-12 h4 input-lg" placeholder="פיתרון" />
                </div>
            </div>
    </div>

    <%--<div class="panel alert-info">
        <h5 onclick="toggleDiv('divOptions');">אפשרויות נוספות</h5>
        <div id="divOptions" style="display:normal"></div>
            
        
    </div>--%>



    <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div id="divOverlay" class="web_dialog_overlay">
            </div>            
            <div id="dvProgressBar" class="web_dialog">
                <img src="Pics/loading_small.gif" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>--%>

    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>


                <div id="divProblems" runat="server" class="alert-success">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="תוצאות" Font-Bold="true" />
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="lblRowCount" runat="server" Text="" Font-Bold="true" />
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="grdProblems" runat="server" class="table table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="id" Width="100%" AllowSorting="True" OnSorting="grdProblems_Sorting" OnRowCommand="grdProblems_RowCommand">
                        <Columns>
                            <asp:TemplateField SortExpression="ID" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs pull-right">
                                <HeaderTemplate>
                                    <div style="text-align: center">
                                        <asp:LinkButton runat="server" Text="ID" CommandName="Sort" CommandArgument="ID" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center">                                        
                                        <%--<a class='glyphicon glyphicon-eye-open pull-left' onClick='showLog(" + Eval("id")+ ")' />--%>                                        
                                        <asp:Label runat="server" Style="text-align: center" Text='<%# "<div><b>(" +Eval("id")+")</b></div><div>" +  Eval("startTime","{0:dd/MM/yyyy}") + "</div><div>" +  Eval("startTime","{0:HH:mm}")+"</div>" %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="workerName" HeaderStyle-CssClass="col-xs-1" ItemStyle-CssClass="col-xs-1">
                                <HeaderTemplate>
                                    <div style="text-align: center">
                                        <asp:LinkButton runat="server" Text="יוצר" CommandName="Sort" CommandArgument="workerName"  />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:Label runat="server" Text='<%# Eval("workerName") %>' Style="word-break: break-word" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="phone" HeaderStyle-CssClass="col-xs-1" ItemStyle-CssClass="col-xs-1">
                                <HeaderTemplate>
                                    <div style="text-align: center">
                                        <asp:LinkButton runat="server" Text="טלפון" CommandName="Sort" CommandArgument="phone" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:Label ID="txtUpdateOpenProblemPhone" runat="server" Text='<%# Eval("phone") %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="customerName" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs">
                                <HeaderTemplate>
                                    <div style="text-align: center">
                                        <asp:LinkButton runat="server" Text="שם הלקוח" CommandName="Sort" CommandArgument="customerName" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("customerName") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtUpdateOpenProblemCustomerName" runat="server" Width="100%" Text='<%# Bind("customerName") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="placeName" HeaderStyle-CssClass="col-xs-1" ItemStyle-CssClass="col-xs-1">
                                <HeaderTemplate>
                                    <div style="text-align: center">
                                        <asp:LinkButton runat="server" Text="שם המקום" CommandName="Sort" CommandArgument="placeName" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("placeName") %>' Style="word-break: break-word" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtUpdatePlaceNameOpenPlaceName" runat="server" Width="100%" Text='<%# Bind("placeName") %>' />
                                    <%--<asp:HiddenField ID="txtUpdatePlaceIDOpenPlaceName" runat="server" Value='<%# Bind("placeNameId") %>' />--%>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="ip" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs">
                                <HeaderTemplate>
                                    <div style="text-align: center">
                                        <asp:LinkButton runat="server" Text="ip" CommandName="Sort" CommandArgument="ip" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("ip") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtUpdatePlaceNameOpenIp" runat="server" Width="100%" Text='<%# Bind("ip") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="problemDesc" HeaderStyle-CssClass="col-xs-2 col-lg-2" ItemStyle-CssClass="col-lg-2">
                                <HeaderTemplate>
                                    <div style="text-align: center">
                                        <asp:LinkButton runat="server" Text="תיאור התקלה" CommandName="Sort" CommandArgument="problemDesc" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("problemDesc") %>' Style="max-width: 300px; word-break: break-word" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtUpdatePlaceNameOpenDesc" runat="server" Width="100%" Style="max-width: 300px" TextMode="MultiLine" Text='<%# Bind("problemDesc") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="problemSolution" HeaderStyle-CssClass="col-xs-2 col-lg-2" ItemStyle-CssClass="col-lg-2">
                                <HeaderTemplate>
                                    <div style="text-align: center">
                                        <asp:LinkButton runat="server" Text="תיאור הפיתרון" CommandName="Sort" CommandArgument="problemSolution" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Eval("problemSolution") %>' Style="max-width: 300px; word-break: break-word" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtUpdatePlaceNameOpenSolution" runat="server" Width="100%" Style="max-width: 300px" TextMode="MultiLine" Text='<%# Bind("problemSolution") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="statusName" HeaderStyle-CssClass="col-xs-1" ItemStyle-CssClass="col-xs-1">
                                <HeaderTemplate>
                                    <div style="text-align: center">
                                        <asp:LinkButton runat="server" Text="סטטוס" CommandName="Sort" CommandArgument="statusName" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:Label ID="lblInGridStatusName" runat="server" Width="100%" Text='<%# Eval("statusName") %>' />
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="cboUpdateOpenProblemStatus" runat="server" Width="100%" SelectedValue='<%# Bind("statusId") %>'>
                                        <asp:ListItem Value="0" Text="ממתין" />
                                        <asp:ListItem Value="1" Text="בטיפול" />
                                        <asp:ListItem Value="2" Text="סגור" />
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="emergencyName" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs">
                                <HeaderTemplate>
                                    <div style="text-align: center">
                                        <asp:LinkButton runat="server" Text="דחיפות" CommandName="Sort" CommandArgument="emergencyName" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:Label runat="server" Text='<%# Eval("emergencyName") %>' />
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="cboOpenProblemStatusEmergency" runat="server" Width="100%" SelectedValue='<%# Bind("emergencyId") %>'>
                                        <asp:ListItem Value="0" Text="רגיל" />
                                        <asp:ListItem Value="1" Text="דחוף" />
                                        <asp:ListItem Value="2" Text="בעדיפות" />
                                        <asp:ListItem Value="3" Text="תקלה מהלילה" />
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="departmentName" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs">
                                <HeaderTemplate>
                                    <div style="text-align: center">
                                        <asp:LinkButton runat="server" Text="מחלקה" CommandName="Sort" CommandArgument="departmentName" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:Label runat="server" Text='<%# Eval("departmentName") %>' />
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="cboUpdateOpenProblemDepartment" runat="server" Width="100%" SelectedValue='<%# Bind("departmentId") %>'>
                                        <asp:ListItem Value="0" Text=" " />
                                        <asp:ListItem Value="1" Text="כללי" />
                                        <asp:ListItem Value="2" Text="טכני" />
                                        <asp:ListItem Value="3" Text="תוכנה" />
                                        <asp:ListItem Value="4" Text="תפריטים" />
                                        <asp:ListItem Value="5" Text="איפוסים" />
                                        <asp:ListItem Value="6" Text="שדרוגים" />
                                        <asp:ListItem Value="7" Text="הנהלת חשבונות" />
                                        <asp:ListItem Value="8" Text="שיווק" />
                                        <asp:ListItem Value="9" Text="לחזור ללקוח" />
                                        <asp:ListItem Value="10" Text="ציוד" />
                                        <asp:ListItem Value="11" Text="יוזרים" />
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="toWorkerName" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs">
                                <HeaderTemplate>
                                    <div style="text-align: center">
                                        <asp:LinkButton runat="server" Text="תומך" CommandName="Sort" CommandArgument="toWorkerName" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:Label runat="server" Text='<%# Eval("toWorkerName") %>' />
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="cboUpdateOpenProblemtoWorker" runat="server" Width="100%" SelectedValue='<%# Bind("toWorker") %>' DataSourceID="dsWorkersName" DataTextField="workerName" DataValueField="id">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource runat="server" ID="dsWorkersName" ConnectionString='<%$ ConnectionStrings:BeecommDBConnectionString %>' SelectCommand="SELECT id, firstName + N' ' + lastName AS workerName FROM workers ORDER BY workerName"></asp:SqlDataSource>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="reportToYaron" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs">
                                <HeaderTemplate>
                                    <div style="text-align: center">
                                        <asp:LinkButton runat="server" Text="ירון" CommandName="Sort" CommandArgument="reportToYaron" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:CheckBox runat="server" Checked='<%# Eval("reportToYaron") %>' />
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div style="text-align: center">
                                        <asp:CheckBox ID="chkUpdateOpenProblemReportToYaron" runat="server" Checked='<%# Bind("reportToYaron") %>' />
                                    </div>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs">
                                <HeaderTemplate>
                                    <div style="text-align: center">
                                        <asp:LinkButton runat="server" Text="לוג" CommandName="Sort" CommandArgument="HaveLog" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="text-align: center">
                                        <asp:ImageButton ID="btnShowLog" runat="server" Visible='<%# Eval("HaveLog") %>' CommandArgument='<%# Eval("id") %>' ImageUrl="~/Pics/log.png" Width="25px" Height="25px" AlternateText='<%# Eval("id") %>' OnClick="btnShowLog_Click" />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="text-center" />
                    </asp:GridView>
                    <asp:SqlDataSource runat="server" ID="dsProblems" ConnectionString='<%$ ConnectionStrings:BeecommDBConnectionString %>'
                        SelectCommand="SELECT problemsClose.id, problemsClose.workerId, problemsClose.phoneId, problemsClose.phone, problemsClose.ip, problemsClose.placeNameId, problemsClose.placeName, problemsClose.customerName, problemsClose.problemDesc, problemsClose.problemSolution, problemsClose.statusId, problemsClose.emergencyId, problemsClose.departmentId, problemsClose.reportToYaron, problemsClose.startTime, problemsClose.finishTime, problemStatus.statusName, workers.firstName + N' ' + workers.lastName AS workerName, departments.departmentName, emergencyTypes.emergencyName, problemsClose.toWorker, workers_1.firstName + N' ' + workers_1.lastName AS toWorkerName FROM problemsClose INNER JOIN workers ON problemsClose.workerId = workers.id INNER JOIN problemStatus ON problemsClose.statusId = problemStatus.id INNER JOIN emergencyTypes ON problemsClose.emergencyId = emergencyTypes.id INNER JOIN departments ON problemsClose.departmentId = departments.id INNER JOIN workers AS workers_1 ON problemsClose.toWorker = workers_1.id ORDER BY problemsClose.startTime DESC"></asp:SqlDataSource>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div id="divLog" name="divLog" runat="server" class="web_dialog">
                    <table style="width: 100%">
                        <tr>
                            <td class="web_dialog_title">לוג</td>
                        </tr>
                    </table>
                    <div class="panel-body" style="background: white">
                        <div class="col-lg-6">
                            <asp:GridView ID="grdLogs" runat="server" DataSourceID="dsLogDetails" AutoGenerateColumns="False" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="fieldName" HeaderText="שדה" SortExpression="fieldName" HeaderStyle-CssClass="col-lg-1" />
                                    <asp:BoundField DataField="originalValue" HeaderText="ערך ישן" SortExpression="originalValue" HeaderStyle-CssClass="col-lg-2" />
                                    <asp:BoundField DataField="newValue" HeaderText="ערך חדש" SortExpression="newValue" HeaderStyle-CssClass="col-lg-2" />
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="dsLogDetails" runat="server" ConnectionString="<%$ ConnectionStrings:BeecommDBConnectionString %>" SelectCommand="SELECT fieldName, originalValue, newValue FROM problemsLog WHERE (groupKey = @groupKey) ORDER BY commitTime">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="grdLogGroups" DefaultValue="0" Name="groupKey" PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>
                        <div class="col-lg-6">
                            <asp:GridView ID="grdLogGroups" runat="server" AutoGenerateColumns="False" Width="100%" SelectedIndex="0" DataSourceID="dsLogGroups" DataKeyNames="groupKey" AllowSorting="True">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" SelectText="בחר" HeaderStyle-CssClass="col-lg-1" />
                                    <asp:BoundField DataField="workerName" HeaderText="עובד" ReadOnly="True" SortExpression="workerName" HeaderStyle-CssClass="col-lg-1"></asp:BoundField>
                                    <asp:BoundField DataField="changeTime" HeaderText="שעה" ReadOnly="True" SortExpression="changeTime" HeaderStyle-CssClass="col-lg-1"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource runat="server" ID="dsLogGroups" ConnectionString='<%$ ConnectionStrings:BeecommDBConnectionString %>'
                                SelectCommand="SELECT problemsLog.groupKey, problemsLog.workerId, workers.firstName + N' ' + workers.lastName AS workerName, MIN(problemsLog.commitTime) AS changeTime FROM problemsLog INNER JOIN workers ON problemsLog.workerId = workers.id WHERE (problemsLog.problemId = @showLogproblemId) GROUP BY problemsLog.groupKey, problemsLog.workerId, workers.firstName + N' ' + workers.lastName ORDER BY changeTime">
                                <SelectParameters>
                                    <asp:SessionParameter DefaultValue="0" Name="showLogproblemId" SessionField="showLogproblemId" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>
                    </div>
                    <div class="panel-footer">
                        <asp:Button ID="btnHideLog" runat="server" Text="סגור" CssClass="btn-primary center-block" Width="100%" Font-Size="XX-Large" OnClick="btnHideLog_Click" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $.datetimepicker.setLocale('he');
            $('#txtFindStartDate').datetimepicker({
                lang: 'he',
                timepicker: true,
                format: 'd/m/y H:i:s'
            });


            $.datetimepicker.setLocale('he');
            $('#txtFindFinishDate').datetimepicker({
                lang: 'he',
                timepicker: true,
                format: 'd/m/y H:i:s',
                //formatDate: 'd/m/y'
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

        //function ShowProgressBar1() {
        //    document.getElementById('dvProgressBar').style.visibility = 'visible';
        //}

        //function HideProgressBar() {
        //    document.getElementById('dvProgressBar').style.visibility = "collapse";
        //}

        //function ShowProgressBar() {
        //    $("#divOverlay").show();
        //    $("#dvProgressBar").fadeIn(300);
        //    $("#divOverlay").unbind("click");
        //}
    </script>
</asp:Content>

