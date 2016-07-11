//default
fillTable(1);

function fillTable(id) {
    $.ajax({
        type: "GET",
        url: "/Devices/getMeasurementsByType",
        contentType: "application/json; charset=utf-8",
        data: {
            'type': id,
        },
        dataType: "json",
        success: function (data) {
            buildTable(data);
        },
        error: errorFunc,
        async: false
    });
}

function buildTable(data) {
    var table = $("#pubsTable");
    table.html("");

    var tr = $('<tr>');
    tr.append('<td>Name</td>');
    tr.append('<td>Location</td>');
    tr.append('<td>Pub CO2 level</td>');
    tr.append('<td>Pub Sound level</td>');
    tr.append('<td>Pub Alcohol level</td>');
    table.append(tr);

    for (i = 0; i < data.length; i++) {
        var tr = $('<tr>');
        if (data[i].Rating > 50){
            tr.css('background-color', 'red');
        }
        tr.append('<td>' + data[i].Name + '</td>');
        tr.append('<td>' + data[i].Location + '</td>');
        tr.append('<td>' + data[i].Rating_Co + '</td>');
        tr.append('<td>' + data[i].Rating_Sound + '</td>');
        tr.append('<td>' + data[i].Rating_Alcohol + '</td>');
        table.append(tr);
        
    }
}

function errorFunc(data, status) {
    alert('error');
}

$(".link").click(function (event) {
    var id = event.target.id;
    fillTable(id);
});