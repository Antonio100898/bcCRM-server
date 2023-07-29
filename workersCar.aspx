<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="workersCar.aspx.cs" Inherits="workersCar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js">
    </script>
    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap.min.js">
    </script>
    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/responsive/2.2.0/js/dataTables.responsive.min.js">
    </script>
    <script type="text/javascript" language="javascript" src="https://cdn.datatables.net/responsive/2.2.0/js/responsive.bootstrap.min.js">
    </script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <table id="tblWorkers" class="table table-bordered table-hover text-center">
            <thead>
                <tr>                    
                    <th class="text-center">שם 
                    </th>                    
                    <th class="text-center">טלפון
                    </th>
                    <th class="text-center">רכב
                    </th>
                    <th class="text-center">מספר רכב
                    </th>                    
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>

    <script type="text/javascript" class="init">
        $(document).ready(function () {
            
            showWorkers();
        });

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
                        var carType = $(this).find("carType").text();
                        var carNumber = $(this).find("carNumber").text();

                        try {
                            if (carType != '' || carNumber != '') {


                                var html = '<tr id="rowWorker' + id + '" class="clickable-row" >';
                                html = html + '<td>' + firstName + ' ' + lastName + '</td>';
                                html = html + '<td>' + phone + '</td>';
                                html = html + '<td>' + carType + '</td>';
                                html = html + '<td>' + carNumber + '</td>';
                                html = html + '</tr>';

                                $(html).appendTo($("#tblWorkers tbody"));
                            }//$("#cboWorkerActive" + id).val(active);

                        } catch (ec) {

                        }
                    });

                    var table = $("#tblWorkers").DataTable();

                    //table
                    //    .rows()
                    //    .invalidate()
                    //    .draw();
                    //
                    hideLoader();

                    toggleDiv('divWorkers');
                });
        }
    </script>
</asp:Content>

