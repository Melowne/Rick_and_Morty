function filllocationTable(getStandorte, getStandort) {
    $("#locationtable").DataTable().destroy();
    $("#tBody").empty();
    $.ajax({
        type: "get",
        url: getStandorte,
        data: {
            typ: $('#typ').val(), dimension: $('#dimension').val()
        },
        success: function (data) {
            if (data.length > 0) {
                $('#locationtable').removeClass("invisible").addClass("visible");

                $(document).ready(function () {
                    $('#locationtable').DataTable();
                });

                for (var i = 0; i < data.length; i++) {
                    var Id = data[i].id;
                    var but = "<button value=" + Id + " id='" + Id + "' class='btn btn-light'>+</button>";

                    $("#tBody").append(
                        "<tr><td>" + data[i].name + "</td>" + "<td>" + data[i].typ + "</td>" +
                        "<td>" + data[i].dimension + "</td>" +
                        "<td>" + data[i].residents + "</td>" + "<td>" + but + "</td></tr>");

                    $('#' + Id).on('click', function (e) {
                        ///modal clearen
                        $("#modal-body").children('div').each(function () {
                            if ($(this).text) {
                                $(this).text("");
                            }
                        });
                        //modal füllen
                        $.ajax({
                            type: "GET",
                            url: getStandort,
                            data: { id: e.target.value },
                            success: function (data) {
                                debugger;
                                $("#modal").modal('show');
                                $("#modaltitle").text(data[0].name);
                                $("#name").append("<b>Name: <b/>", data[0].name);
                                $("#typp").append("<b>Typ: <b/>", data[0].typ);
                                $("#dimensionn").append("<b>Dimension: <b/>", data[0].dimension);
                                //jeweilige Charactere dem entsprechenden Ort zuordnen
                                $("#chars").empty();
                                for (var i = 0; i < data[0].residents.length; i++) {
                                    if (data[0].resident.includes(data[0].residents[i].url))
                                        $("#chars").append("<li class='list-group-item'><img style='width:10%;height:10%' src='" + data[0].residents[i].image+"' class='img - thumbnail img - fluid'> "
                                            + data[0].residents[i].name + "</li");
                                }
                            }
                        });
                    });
                }
            }
        }
    });
}


$("#modal").click(function () {
    $("#modal").modal('hide');
});
function exitmodal() {
    $("#modal").modal('hide');
}
function closeClick() {
    $("#modal").modal('hide');
}