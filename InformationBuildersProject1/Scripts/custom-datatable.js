//Courtesy of this guide: https://www.c-sharpcorner.com/article/asp-net-mvc5-datatables-plugin-server-side-integration/
$(document).ready(function () {
    $('#TableId').DataTable
        ({
            "processing": true,
            "serverSide": true,
            "scrollX": true,
            "ajax":
            {
                "url": "/SalesView/GetData",
                "type": "POST",
                "dataType": "JSON"
            },
            "columns": [
                {
                    "data": "TIME_DATE"
                },
                {
                    "data": "STATE_PROV_CODE_ISO_3166_2"
                },
                {
                    "data": "STORE_TYPE"
                },
                {
                    "data": "BRANDTYPE"
                },
                {
                    "data": "BRAND"
                },
                {
                    "data": "MODEL"
                },
                {
                    "data": "QUANTITY_SOLD"
                },
                {
                    "data": "REVENUE_US"
                }]
        });
});  