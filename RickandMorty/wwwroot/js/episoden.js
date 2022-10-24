var episodenmodalaktiv = false;
function fillepisTable(getEpisoden, getEpi, getCharacter) {
    $("#epistable").DataTable().destroy();

    $("#tBody").empty();
    $.ajax({
        type: "get",
        url: getEpisoden,
        data: {
            staffel: $('#staffel').val()
        },
        success: function (data) {
            if (data.length > 0) {
                $('#epistable').removeClass("invisible").addClass("visible");

                $(document).ready(function () {
                    $('#epistable').DataTable();
                });

                for (var i = 0; i < data.length; i++) {
                    var Id = data[i].id;
                    var but = "<button value=" + Id + " id='" + Id + "' class='btn btn-light'>+</button>";

                    $("#tBody").append(
                        "<tr><td>" + data[i].episode + "</td>" + "<td>" + data[i].name + "</td>" +
                        "<td>" + data[i].airdate + "</td>" +
                        "<td>" + data[i].characters + "</td>" + "<td>" + but + "</td></tr>");

                    $('#' + Id).on('click', function (e) {
                        ///modal clearen
                        $("#modal-body-epi").children('div').each(function () {
                            if ($(this).text) {
                                $(this).text("");
                            }
                        });
                        //modal füllen
                        $.ajax({
                            type: "GET",
                            url: getEpi,
                            data: { id: e.target.value },
                            success: function (data) {
                                $("#modal-epi").modal('show');
                                $("#modaltitle-epi").text(data[0].episode);
                                $("#epi").append("<b>Episode: <b/>", data[0].episode);
                                $("#epi-name").append("<b>Name: <b/>", data[0].name);
                                $("#airdate").append("<b>Erschienen: <b/>", data[0].airdate);
                                //jeweilige Charactere dem entsprechenden Episode zuordnen
                                $("#charsepi").empty();
                                for (var i = 0; i < data[0].characters.length; i++) {
                                    if (data[0].character.includes(data[0].characters[i].url)) {
                                        var Id = data[0].characters[i].id;
                                        $("#charsepi").append("<li class='list-group-item'>" +
                                            "<button value=" + Id + " id='" + Id + "' style = 'background-color:transparent;border:none;text-align:left;display:block;display:inline-block' > <img style='width:10%;height:10%' src='" + data[0].characters[i].image + "' class='img - thumbnail img - fluid'> "
                                            + data[0].characters[i].name + "</button></li>");

                                       getchar(Id, getCharacter);

                                    }
                                }
                            }
                        });
                        episodenmodalaktiv = true;
                    });
                }
            }
        }
    });
}

function exitmodalEpi() {
    $("#modal-epi").modal('hide'); episodenmodalaktiv = false;
}
function closeClickEpi() {
    $("#modal-epi").modal('hide'); episodenmodalaktiv = false;
}