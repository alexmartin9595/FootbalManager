function GetAllPlayers() {
    $.ajax({
        url: "/api/teamplayers/getplayers",
        type: "GET",
        dataType: 'json',
        success: function (data) {
            ShowTeamPlayers(data);
        }
    });
}


function AddAttack(id) {
    $.ajax({
        url: "/api/teamplayers/addattack/" + id,
        type: "POST",
        dataType: 'json',
        success: function (data) {
            $('.message').html(data);
            $('.message').fadeIn(1000);
            $('.message').fadeOut(1500);
            GetAllPlayers();
            GetAllData();
        }
    });
}

function AddDefence(id) {
    $.ajax({
        url: "/api/teamplayers/adddefence/" + id,
        type: "POST",
        dataType: 'json',
        success: function (data) {
            $('.message').html(data);
            $('.message').fadeIn(1000);
            $('.message').fadeOut(1500);
            GetAllPlayers();
            GetAllData();
        }
    });
}

function UpdateNumber(id) {
    var player = {
        Number: $('#number').val()
    }

    $.ajax({
        url: "/api/teamplayers/updatenumber/" + id,
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(player),
        success: function (notify) {
            $('.message').html(notify);
            $('.message').fadeIn(1000);
            $('.message').fadeOut(1500);
            GetAllPlayers();
            GetAllData();
        }
    });

}

function ShowTeamPlayers(players) {
    if (players.length == 0)
        return;
    var defcount = 0;
    var midfieldercount = 0;
    var forwardcount = 0;
    var result = "<table cellpadding='0' cellspacing='0'><tr><th>№</th><th>Игрок</th><th>Позиция</th><th>Возраст</th><th>Атака</th><th>Защита</th><th>Цена</th></tr>";
    for (var i = 0; i < players.length; i++) {
        result += "<tr> <td>" + players[i].Number + "</td>";
        result += "<td>" + players[i].Name + "</td>";
        result += "<td>" + players[i].Position+ "</td> <td>" + players[i].Age + "</td><td>" + players[i].Atack + "</td>";
        result += " <td>" + players[i].Defence + "</td> <td>" + players[i].Price + "</td>";
        result += "<td class='button'><button class='attack' id='attack" + players[i].Id + "'>+1 к атаке</button></td>";
        result += "<td class='button'><button class='defence' id='defence" + players[i].Id + "'>+1 к защите</button></td> ";
        result += "<td class='button'><button class='number' id='number" + players[i].Id + "'>Изменить номер</button></td> </tr>";
        
        switch (players[i].Position) {
            case "вратарь":
                {
                    $(".goalcipper").html(players[i].Name);
                    break;
                }
            case "защитник":
                {
                    if (defcount === 0) {
                        $('.rightback').html(players[i].Name);
                        defcount++;
                        break;
                    } else if (defcount === 1) {
                        $('.leftcenterback').html(players[i].Name);
                        defcount++;
                        break;
                    } else if (defcount === 2) {
                        $('.rightcenterback').html(players[i].Name);
                        defcount++;
                        break;
                    } else {
                        $('.leftback').html(players[i].Name);
                        defcount++;
                        break;
                    }
                }
        case "полузащитник":
            {
                if (midfieldercount === 0) {
                    $('.rightmidfield').html(players[i].Name);
                    midfieldercount++;
                    break;
                }
                else if (midfieldercount === 1) {
                    $('.leftcentermidfield').html(players[i].Name);
                    midfieldercount++;
                    break;
                }
                else if (midfieldercount === 2) {
                    $('.rightcentermidfield').html(players[i].Name);
                    midfieldercount++;
                    break;
                } 
                else {
                    $('.leftmidfield').html(players[i].Name);
                    midfieldercount++;
                    break;
                }
            }    
            case "нападающий":
                {
                    if (forwardcount === 0) {
                        $('.rightforward').html(players[i].Name);
                        forwardcount++;
                        break;
                    }
                    else if (forwardcount === 1) {
                        $('.leftforward').html(players[i].Name);
                        forwardcount++;
                        break;
                    }
                }
        }
    }
    result += "</table";
    $(".team").html(result);

}

$(document).ready(function () {
    $('body').on('click', '.attack', function () {
        var id = $(this).attr("id");
        var newId = id.replace(/attack/g, "");
        AddAttack(newId);
    });
    
    $('body').on('click', '.defence', function () {
        var id = $(this).attr("id");
        var newId = id.replace(/defence/g, "");
        AddDefence(newId);
    });

    $('body').on('click', '.number', function () {
        var id = $(this).attr("id");
        var newId = id.replace(/number/g, "");
        $('.number_send').attr('id', newId);
        $('.message_dialog').show();
        $('.message_box').show();
    });

    $('body').on('click', '.message_reset', function () {
        $('#number').val("");
        $('.message_dialog').hide();
        $('.message_box').hide();
        $('.message_view').hide();
    });

    $('.number_send').click(function () {
        var id = $(this).attr("id");
        UpdateNumber(id);
        $('#number').val("");
        $('.message_dialog').hide();
        $('.message_box').hide();
      
    });


    GetAllPlayers();
});
