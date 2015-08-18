function GetMacthes() {
    $.ajax({
        url: "/api/match/getteammatches",
        type: "GET",
        dataType: 'json',
        success: function (data) {
            Showmatches(data);
        }
    });
}

function Showmatches(matches) {
    if (matches.length === 0)
        return 0;
    var result = "<table cellpadding='0' cellspacing='0'><tr><th>№</th><th>1-я команда</th><th title='Счёт'>Счёт</th><th title='Счёт'>2-я команда</th></tr>";
            for (var i = 0; i <matches.length; i++) {
                result += "<tr> <td>" + (i + 1) + "</td>";
                result += "<td>" + matches[i].FirstUser.UserName + "</td>";
                result += "<td>" + matches[i].FirstTeamGoals +" : " + matches[i].SecondTeamGoals + "</td>";
                result += "<td>" + matches[i].SecondUser.UserName + "</td>";
                result += "<td class='button'> <a href='http://localhost:9840/Home/Match?match=" + matches[i].Id + "'><button>Подробнее</button></a></td>";
                

            }
            result += "</table";
            $(".team").html(result);
}

function GetAllData() {
    $.ajax({
        url: "/api/teamdata/getdata",
        type: "GET",
        dataType: 'json',
        success: function (data) {
            ShowTeamData(data);
        }
    });
}

function ShowTeamData(data) {
    var result = "<div class='matches'> Матчей <p>" + data.MatchesPlayed + " </p>  </div>";
    result += "<div class='wins'>  Побед <p>" + data.MatchesWin + " </p>  </div>";
    result += "<div class='draws'>  Ничей <p>" + data.MatchesDraw + " </p>  </div>";
    result += "<div class='loses'> Поражений <p>" + data.MatchesLose + " </p>  </div>";
    result += "<div class='position'> Очков <p>" + data.Rate + " </p>  </div>";
    $(".header").html(result);

}

$(document).ready(function() {
    GetMacthes();
    GetAllData();
});