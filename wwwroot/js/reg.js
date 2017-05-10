function myMap() {
    $("#err-pass").hide();

    $("#rep-pass").keyup(function () {
        if ($(this).val() === $("#pass").val()) {
            $("#btn-reg").removeAttr("disabled");
            $("#err-pass").hide();
        }
        else {
            $("#btn-reg").attr("disabled",true);
            $("#err-pass").show();
        }
    })
}