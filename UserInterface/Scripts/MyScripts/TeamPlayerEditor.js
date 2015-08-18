
function GetAllPlayers() {
       $.ajax({
           url: "http://localhost:12904/api/TeamPlayers/1",
           type: "GET",
           routeTemplate: "api/{controller}/{action}/{id}",
           success: function (data) {
               ShowTeamPlayers(data);
           },
           error: function (x, y, z) {
               alert(x + '\n' + y + '\n' + z);
           }
       });
   }
   
function ShowTeamPlayers(players) {
    var result = "<table cellpadding='0' cellspacing='0'><tr><th>№</th><th>Игрок</th><th>Позиция</th><th>Возраст</th><th>Атака</th><th>Защита</th><th>Цена</th></tr>";
    $.each(players, function(index, player) {
        result += "<tr> <td>player.Number</td>";
        result += "<td>player.Name</td>";
        result += "<td>player.Position</td> <td>player.Age</td><td>player.Atack</td>";
        result += "<td>player.Atack</td> <td>player.Defence</td> <td>player.Price</td>";
        result += "<td class='button'><button class='attack' id='attack.player.Id'>+1 к атаке</button></td>"
        result += "<td class='button'><button class='number' id='number.player.Id'>Изменить номер</button></td>"
        result += "<td class='button'><button class='defence' id='defence.player.Id'>+1 к защите</button></td> </tr>"
    });
    result += "</table";
    $(".team").html(result);
    
}

$(document).ready(function () {
    GetAllPlayers();
        //$('.attack').click(function () {
            
        //    //addDefence($(this).attr("id"));
        //});
        //$('.number').click(function () {
            
        //    //addDefence($(this).attr("id"));
        //});
        //$('.defence').click(function () {
        //    //addDefence($(this).attr("id"));
        //});
        ////$('.defence').on("click", addDefence);
        ////$('.number').on("click", editNumber);
    });
