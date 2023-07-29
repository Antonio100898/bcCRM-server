<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="MsgCenter.aspx.cs" Inherits="MsgCenter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="alert-info">
        <b>סניף</b>
        <asp:DropDownList ID="cboBranches" runat="server" DataTextField="branchName" DataValueField="id" AutoPostBack="true" OnSelectedIndexChanged="cboBranches_SelectedIndexChanged"></asp:DropDownList>
        <b style="padding-right: 20px">עמדה</b>
        <asp:DropDownList ID="cboPos" runat="server" AppendDataBoundItems="true" DataTextField="posName" DataValueField="id">
            <asp:ListItem Value="0" Text="כל העמדות" />
        </asp:DropDownList>
        <asp:CheckBox ID="chkAllPos" runat="server" Style="padding-right: 20px" Text="כל העמדות" Checked="true" />

        <b style="padding-right: 20px">סוג הודעה</b>
        <asp:DropDownList ID="cboMsgType" runat="server">
            <asp:ListItem Value="1" Text="עדכן סייען" />
            <asp:ListItem Value="2" Text="שלח זד" />
        </asp:DropDownList>
        <b style="padding-right: 20px">תוכן הודעה</b>
        <asp:TextBox ID="txtMsgInfo" runat="server" style="margin-left:20px"></asp:TextBox>

        <asp:Button ID="btnAddMsg" runat="server" Text="הוסף הודעה" CssClass="btn-primary" Style="padding-right: 20px" OnClick="btnAddMsg_Click" />
    </div>
    <div style="padding-right: 25px">        
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:RegularExpressionValidator ID="uplValidator" runat="server" Text="בחר תמונה להעלות"
            ControlToValidate="FileUpload1" ErrorMessage="התמונה חייבת להיות מסוג png, jpg, bmp, gif"
            ValidationExpression="(.+\.([Rr][Aa][Rr]))">
        </asp:RegularExpressionValidator>
        <asp:Button ID="btnUpload" runat="server" Text="העלה את הקובץ" OnClick="Upload" />
    </div>


    <div class="text-center">
        <asp:GridView ID="grdMsgs" runat="server" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="dsMsgs" Width="100%" CssClass="table table-bordered">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" InsertVisible="False" SortExpression="id" HeaderStyle-CssClass="text-center"></asp:BoundField>
                <asp:BoundField DataField="posId" HeaderText="קוד עמדה" SortExpression="posId" HeaderStyle-CssClass="text-center"></asp:BoundField>
                <asp:BoundField DataField="branchName" HeaderText="סניף" SortExpression="branchName" HeaderStyle-CssClass="text-center"></asp:BoundField>
                <asp:BoundField DataField="posName" HeaderText="עמדה" SortExpression="posName" HeaderStyle-CssClass="text-center"></asp:BoundField>
                <asp:BoundField DataField="msgType" HeaderText="סוג הודעה" SortExpression="msgType" HeaderStyle-CssClass="text-center"></asp:BoundField>
                <asp:BoundField DataField="extraParams" HeaderText="נתונים נוספים" SortExpression="extraParams" HeaderStyle-CssClass="text-center"></asp:BoundField>
                <asp:CheckBoxField DataField="posRecived" HeaderText="התקבל בעמדה" SortExpression="posRecived" HeaderStyle-CssClass="text-center"></asp:CheckBoxField>
                <asp:BoundField DataField="recivedTime" HeaderText="התקבל בשעה" SortExpression="recivedTime" HeaderStyle-CssClass="text-center"></asp:BoundField>
                <asp:CheckBoxField DataField="finished" HeaderText="סיים" SortExpression="finished" HeaderStyle-CssClass="text-center"></asp:CheckBoxField>
                <asp:BoundField DataField="finishTime" HeaderText="סיים בשעה" SortExpression="finishTime" HeaderStyle-CssClass="text-center"></asp:BoundField>
                <asp:CheckBoxField DataField="failed" HeaderText="נכשל" SortExpression="failed" HeaderStyle-CssClass="text-center"></asp:CheckBoxField>
                <asp:BoundField DataField="responseMsg" HeaderText="תגובה" SortExpression="responseMsg" HeaderStyle-CssClass="text-center"></asp:BoundField>
                <asp:BoundField DataField="commitTime" HeaderText="זמן יצירת ההודעה" SortExpression="commitTime" HeaderStyle-CssClass="text-center"></asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource runat="server" ID="dsMsgs" ConnectionString='<%$ ConnectionStrings:BeecommDBConnectionString %>'
            SelectCommand="SELECT posMsgs.id, posMsgs.posId, posMsgs.branchName, posMsgs.posName, posMsgType.msgType, posMsgs.extraParams, posMsgs.posRecived, posMsgs.recivedTime, posMsgs.finished, posMsgs.finishTime, posMsgs.failed, posMsgs.responseMsg, posMsgs.commitTime FROM posMsgs INNER JOIN posMsgType ON posMsgs.msgType = posMsgType.id Order By commitTime Desc"></asp:SqlDataSource>
    </div>
</asp:Content>

