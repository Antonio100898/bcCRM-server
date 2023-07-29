<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadFiles.aspx.cs" Inherits="UploadFiles" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <input type="file" name="postedFile" />
            <input id="txtDesc" name="txtDesc" placeholder="תיאור הקובץ" class="col-3" />
            <input id="txtProblemID" name="txtProblemID" class="col-3" style="visibility:hidden" />
            <input type="button" id="btnUpload" value="Upload" class="col-3" />
            <asp:TextBox ID="txtFileProblemId" runat="server" Enabled="false" CssClass="col-3" style="visibility:hidden"></asp:TextBox>
        </div>
        <progress id="fileProgress" style="display: none"></progress>
        <hr />
        <span id="lblMessage" style="color: Green"></span>
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript">

            $(document).ready(function () {
                var a = $("#txtFileProblemId").val();
                console.log(a); 
                $("#txtProblemID").val(a);
            });

            $("body").on("click", "#btnUpload", function () {

                $.ajax({
                    url: 'HandlerCS.ashx',
                    type: 'POST',
                    data: new FormData($('form')[0]),
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (file) {
                        $("#fileProgress").hide();
                        $("#txtDesc").val('');
                        $("#lblMessage").html("<b>" + file.name + "</b> has been uploaded.");
                    },
                    error: function () {
                        alert('נכשל להעלות את הקובץ');
                    },
                    xhr: function () {
                        var fileXhr = $.ajaxSettings.xhr();
                        if (fileXhr.upload) {
                            $("progress").show();
                            fileXhr.upload.addEventListener("progress", function (e) {
                                if (e.lengthComputable) {
                                    $("#fileProgress").attr({
                                        value: e.loaded,
                                        max: e.total
                                    });
                                }
                            }, false);
                        }
                        return fileXhr;
                    }
                });
            });
        </script>
    </form>
</body>
</html>
