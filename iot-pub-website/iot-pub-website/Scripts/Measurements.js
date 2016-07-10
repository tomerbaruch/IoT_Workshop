var ctx1 = document.getElementById("co");
var ctx2 = document.getElementById("sound");
var ctx3 = document.getElementById("alcohol");
var global_id = 1; //default;
var global_time = "oneMonth"; //default

setInterval(function () {
    createGraph(global_time, global_id);
}, 60000);

var x;
var y;
$.ajax({
    type: "GET",
    url: "/Devices/getDevices",
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function(data){
        successFunc3(data);
    },
    error: errorFunc,
    async: false
});

function successFunc3(data) {
    for (var i=0; i<data.length; i++){
        $("#selectDevice").append("<option id='" + data[i].Id + "'>" + data[i].Id + " - " + data[i].Name + "</option>");
    }
}

$("#selectDevice").change(function () {
    global_id = $(this).find('option:selected').attr('id');
    $('.changeLink').attr('href', function () {
        var x = this.href.substring(this.href.indexOf('=') + 1, this.href.length);
        return this.href.replace(x, global_id);
    });
    createGraph(global_time, global_id);
});

$('.changeLink').attr('href', function () {
    return this.href.replace('_parameter', global_id);
});

function createGraph(idTime, global_id) {
    $(".my-loader").show();
    $("#co_nodata").hide();
    $("#sound_nodata").hide();
    $("#alcohol_nodata").hide();


    getData(1, ctx1, idTime, global_id);
    getData(2, ctx2, idTime, global_id);
    getData(3, ctx3, idTime, global_id);
    
}

function getData(type, graph, idTime) {
    $.ajax({
        type: "POST",
        url: "/Measurements/data",
        data: {
            'dataType': type,
            'idTime': idTime,
            'deviceId' : global_id
        },
        dataType: "json",
        success: function (data) {
            getLabels(data, type, graph, idTime, global_id);
        },
        error: errorFunc,
        async: false
    });
}

function getLabels(data, type, graph, idTime, global_id) {
    $.ajax({
        type: "POST",
        url: "/Measurements/labels",
        data: {
            'dataType': type,
            'idTime': idTime,
            'deviceId' : global_id
        },
        //contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (labels) {
            buildGraph(data, labels, graph, type);
        },
        error: errorFunc,
        async: false
    });
}

function buildGraph(data, labels, graph, type) {
    if (data.length == 0) {
        if (type == 1) {
            $("#co_nodata").show();
        }
        else if (type == 2) {
            $("#sound_nodata").show();
        }
        else {
            $("#alcohol_nodata").show();
        }
    }
    new Chart(graph, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: getTitle(type),
                data: data,
                backgroundColor: getColor(type),
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            },
            responsive: false,
            display: false
        }
    });
    if (type == 3) {
        $(".my-loader").hide();
    }
}

getData(1, ctx1, global_id);
getData(2, ctx2, global_id);
getData(3, ctx3, global_id);

function errorFunc(data, status) {
    //alert('error');
}

function getTitle(type) {
    if (type == 1) {
        return "# CO2 level";
    }
    else if (type == 2) {
        return "# Sound level";
    }
    else {
        return "# Alcohol level";
    }
}

function getColor(type) {
    if (type == 1) {
        return "";
    }
    else if (type == 2) {
        return "lightBlue";
    }
    else {
        return "lightGreen";
    }
}

$(".link").click(function (event) {
    var id = event.target.id;
    //alert(event.target.id);
    global_time = id;
    createGraph(global_time, global_id);
});


