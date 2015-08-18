function GetAllUsers(firstindex, secondindex) {
    $.ajax({
        url: "/api/teamdata/getalldata",
        type: "GET",
        dataType: 'json',
        success: function (data) {
            ShowUsers(data, firstindex, secondindex);
        }
    });
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


function ShowPagination(users) {
    var currentPageNumber = DefineCurrentNumber();
    $('#compact-pagination').pagination({
        items: users.length,
        itemsOnPage: 10,
        currentPage: currentPageNumber,
        cssStyle: 'light-theme',
        onPageClick: function () {
            var currentNumber = DefineCurrentNumber();
            if ((users.length - (currentNumber - 1) * 10) >= 10)
                ShowUsers(users, (currentNumber - 1) * 10, (currentNumber - 1) * 10 + 10);
            else
                ShowUsers(users, (currentNumber - 1) * 10, users.length);
        }
    });
}

function ShowUsers(users, firstindex, secondindex) {
    GetUserId(function(senderId) {
        var result = "<table cellpadding='0' cellspacing='0'><tr><th>№</th><th>Команда</th><th title='Очки'>О</th><th title='Матчи'>М</th><th  title='Выигрыши'>В</th><th title=Ничьи'>Н</th><th title='Поражения'>П</th></tr>";
        for (var i = firstindex; i < secondindex; i++) {
            result += "<tr> <td>" + (i + 1) + "</td>";
            result += "<td>" + users[i].CurrentUser.UserName + "</td>";
            result += "<td class='points'>" + users[i].Rate + "</td>";
            result += "<td>" + users[i].MatchesPlayed + "</td>";
            result += "<td>" + users[i].MatchesWin + "</td>";
            result += "<td>" + users[i].MatchesDraw + "</td>";
            result += "<td>" + users[i].MatchesLose + "</td>";
            if (senderId !== users[i].Id) {
                result += "<td class='button'><button class='answer'  id='sendmessage" + users[i].Id + "'>Отправить сообщение</button></td>";
                if (users[i].PlayersNumber === 11)
                    result += "<td class='button'><button class='invitation' id='invite" + users[i].Id + "'>Отправить приглашение</button></td></tr>";
            }
            
        }
        result += "</table";
        $(".team").html(result);
        ShowPagination(users);
    });
}

$(document).ready(function () {
    GetAllUsers(0, 10);
});