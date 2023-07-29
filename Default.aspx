<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Beecomm</title>
    <link rel="shortcut icon" href="Pics/BeecommLogoIco.ico" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://code.highcharts.com/highcharts.js"></script>
    <script type="text/javascript" src="https://code.highcharts.com/modules/exporting.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <!-- Optional theme -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" />
    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>

<body>
    <div class="container" style="max-width:300px">
        <form id="form1" runat="server" class="form-signin"  dir="rtl">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />

            <h2 class="form-signin-heading">
                <img src="Pics/BeecommLogo.JPG" class="img-rounded" alt="Cinque Terre" width="100%" height="100%" />
            </h2>
            <label for="inputEmail" class="sr-only">Email address</label>
            <input type="text" id="txtUserName" name="txtUserName" class="form-control" placeholder="שם משתמש" required autofocus />
            <label for="inputPassword" class="sr-only">סיסמה</label>
            <input type="password" id="txtPassword" name="txtPassword" class="form-control" placeholder="Password" required />
            <asp:Button ID="btnSighIn" runat="server" class="btn btn-lg btn-primary btn-block" Text="היכנס" OnClick="btnSighIn_Click" />
            <asp:Label ID="lblStatus" runat="server" Text="Label" Visible="false" />
            <br />
            <br />
            <br />
            <br />
            <h2>-><a href="https://bccrm-334f4.web.app/" style="color:red">האתר מפסיק לעבוד בסוף ינואר, כדאי להתרגל לאתר החדש</a><-</h2>
        </form>
    </div>
</body>
</html>
