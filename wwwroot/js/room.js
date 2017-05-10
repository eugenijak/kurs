function myMap() {
    $(".gpx-file input").change(function () {
        var filename = $(this).val().replace(/.*\\/, "");
        $("#filename").val(filename);
        $('#errorfile').hide();
    });

    var password;

    $("#err-pass").hide();
    $("#old-err").hide();

    $.post("/Home/Get")
        .done(function (result) {
            password = result.password;
        })

    $("#rep-pass").keyup(function () {
        if ($(this).val() === $("#pass").val()) {
            $("#err-pass").hide();
        }
        else {
            $("#btn-reg").attr("disabled", true);
            $("#err-pass").show();
        }
    })

    $("#old-pass").keyup(function () {
        if ($("#old-pass").val() === password) {
            $("#old-err").hide();
            $("#btn-reg").removeAttr("disabled");
           
        }
        else {
            $("#btn-reg").attr("disabled", true);
            $("#old-err").show();
        }
    })   
}