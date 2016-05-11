
$("#deleteRP").click(function () {
    $.ajax({
        type: "GET",
        url: "/Measurements/deleteRecords",
        data: {
            //'deviceId' : window.location.href.substring(this.href.indexOf('=') + 1)
            'id': 6 // for debug only
        },
        contentType: "application/json; charset=utf-8",
        success: function () {
            location.reload();
        },
        error:function(data){
            alert(data)
        },
        async: false
    });
});


