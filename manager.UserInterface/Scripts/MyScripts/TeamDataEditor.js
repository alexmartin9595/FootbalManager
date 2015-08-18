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
    var result = "<div class='budget'> Бюджет <p>" + data.Budget + " </p>  </div>";
    result += "<div class='matches'> Матчей <p>" + data.MatchesPlayed + " </p>  </div>";
    result += "<div class='players'> Игроков <p>" + data.PlayersNumber + " </p>  </div>";
    result += "<div class='position'> Очков <p>" + data.Rate + " </p>  </div>";
    $(".header").html(result);

}

$(document).ready(function () {
    GetAllData();
});
