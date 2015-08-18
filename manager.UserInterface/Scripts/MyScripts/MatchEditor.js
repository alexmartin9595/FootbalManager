function CalculateFirstTeamScore(macthId) {
    $.ajax({
        url: "/api/match/calculatefirstteamscore/" + macthId,
        type: "GET",
        dataType: 'json',
        success: function(data) {
            $('#firstscore').text(data);
        }
    });
}


function CalculateSecondTeamScore(macthId) {
    $.ajax({
        url: "/api/match/calculatesecondteamscore/" + macthId,
        type: "GET",
        dataType: 'json',
        success: function (data) {
            $('#secondscore').text(data);
        }
    });
}

function GetFirstTeamScore(macthId) {
    $.ajax({
        url: "/api/match/getfirstteamscore/" + macthId,
        type: "GET",
        dataType: 'json',
        success: function (data) {
            $('#firstscore').text(data);
        }
    });
}

function GetSecondTeamScore(macthId) {
    $.ajax({
        url: "/api/match/getsecondteamscore/" + macthId,
        type: "GET",
        dataType: 'json',
        success: function (data) {
            $('#secondscore').text(data);
        }
    });
}

function GetFirstTeamPlayers(id) {
    $.ajax({
        url: "/api/teamplayers/getteamplayers/" + id,
        type: "GET",
        dataType: 'json',
        success: function (data) {
            ShowFirstTeamPlayers(data);
        },
        error: function () {
            window.location.href = "http://localhost:9840/Home/Messages";
        }
    });
}

function GetSecondTeamPlayers(id) {
    $.ajax({
        url: "/api/teamplayers/getteamplayers/" + id,
        type: "GET",
        dataType: 'json',
        success: function (data) {
            ShowSecondTeamPlayers(data);
        },
        error: function () {
            window.location.href = "http://localhost:9840/Home/Messages";
        }
    });
}

function GetFirstTeamId(matchId, getId) {
    $.ajax({
        url: "/api/match/getfirstteamid/" + matchId,
        type: "GET",
        dataType: 'json',
        success: function (data) {
            getId(data);
            GetFirstTeamPlayers(data);
        }
    });
}

function GetSecondTeamId(matchId, getId) {
    $.ajax({
        url: "/api/match/getsecondteamid/" + matchId,
        type: "GET",
        dataType: 'json',
        success: function (data) {
            getId(data);
            GetSecondTeamPlayers(data);
        }
    });
}

function GetGoalscorers(macthId) {
    $.ajax({
        url: "/api/match/getgoalscorers/" + macthId,
        type: "GET",
        dataType: 'json',
        success: function (data) {
            ShowGoalscorers(data);
        }
    });
}

function GetCurrentMinute(matchId, getdata) {
    $.ajax({
        url: "/api/match/getcurrentminute/" + matchId,
        type: "GET",
        dataType: 'json',
        success: function (data) {
            getdata(data);
        }
    });
}

function AddMatch(firstteamId, secondteamId, getId) {
    var match = {
        FirstTeamId: firstteamId,
        SecondTeamId: secondteamId,
        CurrentMinute: 0,
        IsStarted: true,
        FirstTeamGoals: 0,
        SecondTeamGoals: 0
    };

    $.ajax({
        url: "/api/match/addmatch",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(match),
        success: function (notify) {
            getId(notify);
        }
    });
}

function UpdateFirstTeamWins(id) {
    $.ajax({
        url: "/api/teamdata/updatematcheswin/" + id,
        type: "PUT",
        contentType: "application/json"
    });
}

function UpdateFirstTeamDraw(id) {
    $.ajax({
        url: "/api/teamdata/updatematchesdraw/" + id,
        type: "PUT",
        contentType: "application/json"
    });
}

function UpdateFirstTeamLose(id) {
    $.ajax({
        url: "/api/teamdata/updatematcheslose/" + id,
        type: "PUT",
        contentType: "application/json"
    });
}

function GetCurrentUserId(getId) {
    $.ajax({
        url: "/api/users/getcurrentuserid",
        type: "GET",
        dataType: 'json',
        success: function(data) {
            getId(data);
        }
    });
}

function ShowFirstTeamPlayers(players) {
    $('#firstteam').text(players[0].CurrentUser.UserName);
    var result = "<h4>" + players[0].CurrentUser.UserName + "</h4><table cellpadding='0' cellspacing='0'><tr><th>№</th><th>Игрок</th><th>Позиция</th></tr>";
    for (var i = 0; i < players.length; i++) {
        result += "<tr> <td>" + players[i].Number + "</td>";
        result += "<td>" + players[i].Name + "</td>";
        result += "<td>" + players[i].Position + "</td></tr>";

    }
    result += "</table";
    $("#team1").html(result);
}

function ShowSecondTeamPlayers(players) {
    $('#secondteam').text(players[0].CurrentUser.UserName);
    var result = "<h4>"+ players[0].CurrentUser.UserName + "</h4><table cellpadding='0' cellspacing='0'><tr><th>№</th><th>Игрок</th><th>Позиция</th></tr>";
    for (var i = 0; i < players.length; i++) {
        result += "<tr> <td>" + players[i].Number + "</td>";
        result += "<td>" + players[i].Name + "</td>";
        result += "<td>" + players[i].Position + "</td></tr>";

    }
    result += "</table";
    $("#team2").html(result);
}

function ShowGoalscorers(players) {
    if (players.length === 0)
        return 0;
    var result = "<h4>Голы</h4><table cellpadding='0' cellspacing='0'><tr><th>Игрок</th><th>Команда</th><th>Минута</th></tr>";
    for (var i = 0; i < players.length; i++) {
        result += "<tr> <td>" + players[i].CurrentPlayer.Name + "</td>";
        result += "<td>" + players[i].CurrentUser.UserName + "</td>";
        result += "<td>" + players[i].Minute + "</td></tr>";

    }
    result += "</table";
    $("#goalscorers").html(result);
}

function UpdateStats(firstscore, secondscore, matchId) {
    if (firstscore > secondscore) {
        UpdateFirstTeamWins(matchId);
    }
    else if (firstscore === secondscore)
        UpdateFirstTeamDraw(matchId);
    else
        UpdateFirstTeamLose(matchId);
}


function manageTimer(timer, matchId, minute) {
    if (minute === 90) {
        clearInterval(timer);
        $('#endtimer').show();
        var firstscore = $('#firstscore').text();
        var secondscore = $('#secondscore').text();
        UpdateStats(firstscore, secondscore, matchId);
        return 0;
    }

    GetCurrentUserId(function(id) {
        GetFirstTeamId(matchId, function(firstteamId) {
            if (id === firstteamId) {
                CalculateFirstTeamScore(matchId);
                CalculateSecondTeamScore(matchId);
            }
        });
        GetSecondTeamId(matchId, function(secondteamId) {
        });

    });
    GetGoalscorers(matchId);
}


function GetMacthId(url) {
    var array = url.split("?");
    var paramstring = array[1].split("&");
    paramstring[0] = paramstring[0].replace(/match=/, "");
    return paramstring[0];
}


$(document).ready(function () {
    var matchId = GetMacthId(window.location.href);
    $('.begin_match').show();
    GetCurrentMinute(matchId, function (minute) {
        GetCurrentUserId(function (id) {
            GetSecondTeamId(matchId, function (secondteamId) {
                if (id === secondteamId && minute !== 90) {
                    $('.begin_match').hide();
                    $('#notendtimer').show();
                }
            });
            $('.matсhheader').show();
        });

        if (minute === 90) {
            $('#endtimer').show();
            $('.begin_match').hide();
            $('#notendtimer').hide();
        }
        
        $('.timer span').text(minute);
        
    });
    
    
    GetFirstTeamScore(matchId);
    GetSecondTeamScore(matchId);
    GetFirstTeamId(matchId, function() {
    });
    GetSecondTeamId(matchId, function() {
    });
    GetGoalscorers(matchId);
    
    $('.begin_match').click(function () {
        var timer = setInterval(function () {
            GetCurrentMinute(matchId, function (minute) {
                manageTimer(timer, matchId, minute);
                $('.timer span').text(minute);
            });
       }, 2000);
        $(this).hide();
    });
   
});