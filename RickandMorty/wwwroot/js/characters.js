function fillcharTable(getCharacters,getCharacter) {
    $("#chartable").DataTable().destroy();
    $("#tBody").empty(); 
    $.ajax({
        type: "get",
        url: getCharacters,
        data: {
            spezies: $('#spezies').val(), geschlecht: $('#geschlecht').val(), herkunft: $('#herkunft').val(),
            status: $('#status').val(), ort: $('#ort').val(), epi: $('#episode').val()
        },
        success: function (data) {
            if (data.length > 0) {
                $('#chartable').removeClass("invisible").addClass("visible");

                $(document).ready(function () {
                    $('#chartable').DataTable();
                });

                for (var i = 0; i < data.length; i++) {
                    var Id = data[i].id;
                    var but = "<button value=" + Id + " id='" + Id + "' class='btn btn-light'>+</button>";

                    $("#tBody").append(
                        "<tr><td style='width:10%;height:10%'><div><img src='" + data[i].image + "' class='img-thumbnail img-fluid'>" + "</div></td>" +
                        "<td>" + data[i].name + "</td>" + "<td>" + data[i].species + "</td>" +
                        "<td>" + data[i].gender + "</td>" + "<td>" + data[i].origin + "</td>"
                        + "<td>" + data[i].epis + "</td>" + "<td>" + but + "</td></tr>");

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
                            url: getCharacter,
                            data: { id: e.target.value },
                            success: function (data) {
                                $("#modal").modal('show');
                                $("#modalimg").attr('src', data[0].image);
                                $("#modaltitle").text(data[0].name);
                                $("#species").append("<b>Spezies: <b/>", data[0].species);
                                $("#gender").append("<b>Geschlecht: <b/>", data[0].gender);
                                $("#name").append("<b>Name: <b/>", data[0].name);
                                $("#statuss").append("<b>Status: <b/>", data[0].status);
                                $("#origin").append("<b>Herkunft: <b/>", data[0].origin);
                                $("#location").append("<b>Aufenthaltsort: <b/>", data[0].location);
                                //jeweilige Episoden dem entsprechenden Character zuordnen
                                $("#epischar").empty();
                                for (var i = 0; i < data[0].epis.length; i++) {
                                    if (data[0].epi.includes(data[0].epis[i].url))
                                        $("#epischar").append("<li class='list-group-item'>" + data[0].epis[i].episode+" " +data[0].epis[i].name+"</li");
                                  
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