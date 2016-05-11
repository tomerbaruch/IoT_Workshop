var ctx1 = document.getElementById("co");
var ctx2 = document.getElementById("sound");
var ctx3 = document.getElementById("alcohol");
var global_id = 1 //default;

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
});

$('.changeLink').attr('href', function () {
    return this.href.replace('_parameter', global_id);
});

//$.ajax({
//    type: "POST",
//    url: "/Measurements/data",
//    data: param = "",
//    contentType: "application/json; charset=utf-8",
//    dataType: "json",
//    success: successFunc2,
//    error: errorFunc,
//    async: false
//});

function createGraph(idTime) {
    getData(1, ctx1, idTime);
    getData(2, ctx2, idTime);
    getData(3, ctx3, idTime);
}

function getData(type, graph, idTime) {
    $.ajax({
        type: "POST",
        url: "/Measurements/data",
        data: {
            'dataType': type,
            'idTime': idTime
        },
        dataType: "json",
        success: function (data) {
            getLabels(data, type, graph, idTime);
        },
        error: errorFunc,
        async: false
    });
}

function getLabels(data, type, graph, idTime) {
    $.ajax({
        type: "POST",
        url: "/Measurements/labels",
        data: {
            'dataType': type,
            'idTime' : idTime
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
}

//function successFunc2(data, status) {
//    //alert(data);
//    y = data;
//}

getData(1, ctx1);
getData(2, ctx2);
getData(3, ctx3);

function errorFunc(data, status) {
    alert('error');
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
//var myChart1 = new Chart(ctx1, {
//    type: 'line',
//    data: {
//        labels: x,
//        datasets: [{
//            label: '# CO2 level',
//            data: y,
//        }]
//    },
//    options: {
//        scales: {
//            yAxes: [{
//                ticks: {
//                    beginAtZero:true
//                }
//            }]
//        },
//        responsive: false,
//        display: false
//    }
//});

//var myChart2 = new Chart(ctx2, {
//    type: 'line',
//    data: {
//        labels: x,
//        datasets: [{
//            label: '# Sound level',
//            data: y,
//            //borderColor: "blue",
//            backgroundColor: "lightBlue"
//        }]
//    },
//    options: {
//        scales: {
//            yAxes: [{
//                ticks: {
//                    beginAtZero: true
//                }
//            }]
//        },
//        responsive: false,
//        display: false
//    }
//});

//var myChart3 = new Chart(ctx3, {
//    type: 'line',
//    data: {
//        labels: x,
//        datasets: [{
//            label: '# Alcohol level',
//            data: y,
//            //borderColor: "green",
//            backgroundColor: "lightGreen"
//        }]
//    },
//    options: {
//        scales: {
//            yAxes: [{
//                ticks: {
//                    beginAtZero: true
//                }
//            }]
//        },
//        responsive: false,
//        display: false,
//    }
//});

$(".link").click(function (event) {
    var id = event.target.id;
    //alert(event.target.id);
    createGraph(id);
});


