function GetAllPlayers (firstindex, secondindex) {
    $.ajax({
        url: "/api/players/getplayers",
        type: "GET",
        dataType: 'json',
        success: function (data) {
            ShowPlayers(data, firstindex, secondindex);
        }
    });
}

function GetForwards(firstindex, secondindex) {
    $.ajax({
        url: "/api/players/getforwards",
        type: "GET",
        dataType: 'json',
        success: function (data) {
            ShowPlayers(data, firstindex, secondindex);
        }
    });
}

function GetMidfielders(firstindex, secondindex) {
    $.ajax({
        url: "/api/players/getmidfielders",
        type: "GET",
        dataType: 'json',
        success: function (data) {
            ShowPlayers(data, firstindex, secondindex);
        }
    });
}

function GetDefenders(firstindex, secondindex) {
    $.ajax({
        url: "/api/players/getdefenders",
        type: "GET",
        dataType: 'json',
        success: function (data) {
            ShowPlayers(data, firstindex, secondindex);
        }
    });
}

function GetCeapers(firstindex, secondindex) {
    $.ajax({
        url: "/api/players/getceapers",
        type: "GET",
        dataType: 'json',
        success: function (data) {
            ShowPlayers(data, firstindex, secondindex);
        }
    });
}

function BuyPlayer (id) {
    $.ajax({
        url: "/api/players/buyplayer/" + id,
        type: "POST",
        dataType: 'json',
        success: function (data) {
            $('.message').html(data);
            $('.message').fadeIn(1000);
            $('.message').fadeOut(1500);
            GetAllData();
        }
    });
}

function ShowPlayers(players, firstindex, secondindex) {
    var result = "<table cellpadding='0' cellspacing='0'><tr><th>№</th><th>Игрок</th><th>Позиция</th><th>Возраст</th><th>Атака</th><th>Защита</th><th>Цена</th></tr>";
    for (var i = firstindex; i < secondindex; i++) {
        result += "<tr> <td>" + players[i].Id + "</td>";
        result += "<td>" + players[i].Name + "</td>";
        result += "<td>" + players[i].Position + "</td> <td>" + players[i].Age + "</td><td>" + players[i].Atack + "</td>";
        result += " <td>" + players[i].Defence + "</td> <td>" + players[i].Price + "</td>";
        result += "<td class='button'><button class='number' id='buy" + players[i].Id + "'>Купить</button></td></tr>";
    }
    result += "</table";
    $(".team").html(result);
    ShowPagination(players);
}

function DefineCurrentNumber() {
    var currentPageItem = $('.active').children();
    var currentPageNumber = 0;
    if (currentPageItem.html() > 0)
        currentPageNumber = currentPageItem.html();
    else
        currentPageNumber = 1;
    return currentPageNumber;
}


function ShowPagination(players) {
    var currentPageNumber = DefineCurrentNumber();
    $('#pagination').pagination({
        items: players.length,
        itemsOnPage: 10,
        currentPage: currentPageNumber,
        cssStyle: 'light-theme',
        onPageClick: function() {
            var currentNumber = DefineCurrentNumber();
            if ((players.length - (currentNumber -1) * 10) >= 10)
                ShowPlayers(players, (currentNumber - 1) * 10, (currentNumber - 1) * 10 + 10);
            else
                ShowPlayers(players, (currentNumber - 1) * 10, players.length);
        }
    });
}

function ShowPlayersByPosition(position) {
    if (position == "все")
        GetAllPlayers(0, 10);
    else if (position == "нападающий")
        GetForwards(0, 10);
    else if (position == "полузащитник")
        GetMidfielders(0, 10);
    else if (position == "защитники")
        GetDefenders(0, 10);
    else if (position == "вратари")
        GetCeapers (0, 10);
}

$(document).ready(function () {
    GetAllPlayers(0, 10);
    $('#positionsearch').selectmenu({
        width: 170,
        select: function () {
            var searchtext = $('.ui-selectmenu-text').html();
            ShowPlayersByPosition(searchtext);
        }
    });
    
    $('body').on('click', '.number', function () {
        var id = $(this).attr("id");
        var newId = id.replace(/buy/g, "");
        BuyPlayer(newId);
    });
});
