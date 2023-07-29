<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="workerPlace.aspx.cs" Inherits="workerPlace" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel" dir="rtl">
        <div class="panel-body col-lg-12">
            <div class="col-lg-3 pull-right">
                <strong>פרטים כלליים</strong>
                <table>
                    <tr>
                        <td>
                            <strong>שם פרטי</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="input-lg" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>שם משפחה</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="input-lg" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>טלפון</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="input-lg" />
                        </td>
                    </tr>                    
                    <tr>
                        <td>
                            <strong>שם משתמש</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="input-lg" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>סיסמה</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="input-lg" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>שלוחה</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtShluha" runat="server" TextMode="Number" CssClass="input-lg" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>רכב</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCarType" runat="server" CssClass="input-lg" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>מספר רכב</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCarNumber" runat="server" CssClass="input-lg" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnUpdateWorkerInfo" runat="server" Text="עדכן" CssClass="btn-primary btn-lg" Width="100%" OnClick="btnUpdateWorkerInfo_Click" ValidationGroup="vgUpdateWorker" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFirstName" ErrorMessage="חובה להזין שם הפרטי" ForeColor="Red" Display="Dynamic" ValidationGroup="vgUpdateWorker" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLastName" ErrorMessage="חובה להזין שם משפחה" ForeColor="Red" Display="Dynamic" ValidationGroup="vgUpdateWorker" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPhone" ErrorMessage="חובה להזין טלפון" ForeColor="Red" Display="Dynamic" ValidationGroup="vgUpdateWorker" />
                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtPhone" ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{7,12}$" runat="server" ForeColor="Red" ErrorMessage="הטלפון חייב להיות לפחות 7 עד 12 תווים" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPhone" ErrorMessage="הטלפון יכול להיות מספרים בלבד" ForeColor="Red" ValidationExpression="^[0-9]*$" ValidationGroup="NumericValidate" Display="Dynamic" />
                        </td>
                        <td></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUserName" ErrorMessage="חובה להזין שם משתמש" ForeColor="Red" Display="Dynamic" ValidationGroup="vgUpdateWorker" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPassword" ErrorMessage="חובה להזין סיסמה" ForeColor="Red" Display="Dynamic" ValidationGroup="vgUpdateWorker" />
                        </td>
                    </tr>
                </table>
            </div>

            <div class="col-lg-3 pull-right">
                <asp:Image ID="imgWorker" runat="server" Width="250px" Height="250px" CssClass="img-rounded" />
                <asp:FileUpload ID="FileUploadControl" runat="server" />
                <asp:Button runat="server" ID="UploadButton" Text="העלה תמונה" OnClick="UploadButton_Click" />
                <asp:Label runat="server" ID="StatusLabel" Text="" />
            </div>
            <div class="col-lg-6">
                <asp:Label runat="server" ID="lblOrderInMostProblems" Text="" Font-Size="Larger" />
            </div>
        </div>
    </div>

    <div class="col-lg-12 alert-info">
        <h3>הודעות</h3>
        <asp:GridView ID="grdMsgs" runat="server" DataSourceID="dsMsgs" AutoGenerateColumns="False" DataKeyNames="id" Width="100%">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" SortExpression="id" />
                <asp:BoundField DataField="workerNme" HeaderText="נשלח" ReadOnly="True" SortExpression="workerNme" />
                <asp:BoundField DataField="msg" HeaderText="הודעה" SortExpression="msg" />
                <asp:BoundField DataField="commitTime" HeaderText="זמן השליחה" SortExpression="commitTime" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource runat="server" ID="dsMsgs" ConnectionString='<%$ ConnectionStrings:BeecommDBConnectionString %>' SelectCommand="SELECT Msgs.id, workers.firstName + N' ' + workers.lastName AS workerNme, Msgs.msg, Msgs.commitTime FROM Msgs INNER JOIN workers ON Msgs.workerId = workers.id WHERE (Msgs.toWorkerId = @toWorkerId) ORDER BY Msgs.commitTime">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="0" Name="toWorkerId" SessionField="WorkerId" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</asp:Content>

