function GetUserName(getdata, id) {
    $.ajax({
        url: "/api/users/getusername/" + id,
        type: "GET",
        dataType: 'json',
        success: function (data) {
            getdata(data);
        }
    });
}

function GetInputMessages(firstindex, secondindex) {
    $.ajax({
        url: "/api/message/getinputmessages",
        type: "GET",
        dataType: 'json',
        success: function (data) {
            ShowInputMessages(data, firstindex, secondindex);
        }
    });
}

function GetOutputMessages(firstindex, secondindex) {
    $.ajax({
        url: "/api/message/getoutputmessages",
        type: "GET",
        dataType: 'json',
        success: function (data) {
            ShowOutputMessages(data, firstindex, secondindex);
        }
    });
}

function GetUserId(getdata) {
    $.ajax({
        url: "/api/users/getcurrentuserid",
        type: "GET",
        dataType: 'json',
        success: function (data) {
            getdata(data);
        }
    });
}

function SendMessage(senderId, recieverId) {
    var message = {
        SenderId: senderId,
        ReceiverId: recieverId,
        Text: $('.message_text').val()
    };
    
    $.ajax({
        url: "/api/message/sendmessage",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(message),
        success: function (notify) {
            $('.message').html(notify);
            $('.message').fadeIn(1000);
            $('.message').fadeOut(1500);
            GetInputMessages(0, 3);
        }
    });
}

function SendInvitation(senderId, recieverId) {
    var message = {
        SenderId: senderId,
        ReceiverId: recieverId,
        Text: "Сударь, соизвольте пригласить сыграть матч",
        IsInvitation: true
    };

    $.ajax({
        url: "/api/message/sendmessage",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(message),
        success: function (notify) {
            $('.message').html(notify);
            $('.message').fadeIn(1000);
            $('.message').fadeOut(1500);
            GetInputMessages(0, 3);
        }
    });
}


function SendConfurmation(senderId, recieverId, matchId) {
    var message = {
        SenderId: senderId,
        ReceiverId: recieverId,
        Text: "Я согласен, милостливый государь. Перейдите по " + "<a href = '" + "http://localhost:9840/Home/Match?match=" + matchId + "'> ссылке </a>",
        IsInvitation: false,
        IsSeen: false,
        IsRefused: false
    };

    $.ajax({
        url: "/api/message/sendmessage",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(message),
        success: function (notify) {
            $('.message').html(notify);
            $('.message').fadeIn(1000);
            $('.message').fadeOut(1500);
            GetInputMessages(0, 3);
        }
    });
}

function SendRefusion(senderId, recieverId) {
    var message = {
        SenderId: senderId,
        ReceiverId: recieverId,
        Text: "Я вынужден отказаться, милостливый государь.",
        IsInvitation: false,
        IsSeen: false,
        IsRefused: true
    };

    $.ajax({
        url: "/api/message/sendmessage",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(message),
        success: function (notify) {
            $('.message').html(notify);
            $('.message').fadeIn(1000);
            $('.message').fadeOut(1500);
            GetInputMessages(0, 3);
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

function UpdateMessage(messageId, isRefused) {
    var message = {
        IsSeen: true,
        IsRefused: isRefused
    };

    $.ajax({
        url: "/api/message/updatemessage/" + messageId,
        type: "PUT",
        contentType: "application/json",
        data: JSON.stringify(message),
        success: function () {
            GetInputMessages(0, 3);
        }
    });
}

function ShowNotReadMessages(messages) {
    var number = 0;
    for (var i = 0; i < messages.length; i++) {
        if (messages[i].IsSeen === false)
            number++;
    }
    if (number > 0)
        $('.message_number').text("Сообщения +" + number);
    else
        $('.message_number').text("Сообщения");

}


function ShowInputMessages(messages, firstindex, secondindex) {
    var result = "";
    if (secondindex > messages.length) {
        secondindex = messages.length;
        firstindex = 0;
    }
    ShowNotReadMessages(messages);
    for (var i = firstindex; i < secondindex; i++) {
        if (messages[i].IsSeen === true) {
            result += "<div class='inpox_message' id='message" + messages[i].Id + "'><div class='username'>От: " + messages[i].SenderUser.UserName + " </div> <div class='text'>" + messages[i].Text + "</div> ";
            if (messages[i].IsInvitation === true) {
                if (messages[i].IsRefused === true)
                    result += "<span class='notification'> Отклонено</span>";
                else
                    result += "<span class='notification'> Принято</span>";
            }
            else {
                result += "<br /> <button class='watch'> Просмотреть </button> <button class='answer' id='sendmessage" + messages[i].SenderId + "'>Ответить </button>";
            }
            result += "<span class='notification'> Просмотрено</span>";
        }
        else {
            result += "<div class='inpox_message notread' id='message" + messages[i].Id + "'><div class='username'>От: " + messages[i].SenderUser.UserName + " </div> <div class='text'>" + messages[i].Text + "</div> ";
            if (messages[i].IsInvitation === true) {
                result += "<br /><button class='confurmanswer' id='confirm" + messages[i].SenderId + "'>Согласиться</button>" + "<button class='refuseanswer' id='refuse" + messages[i].SenderId + "'>Отклонить</button>";
                if (messages[i].IsRefused === true)
                    result += "<span class='notification'> Отклонено</span>";
                else
                    result += "<span class='notification'> Принято</span>";
            }
            else {
                result += "<br /> <button class='watch'> Просмотреть </button> <button class='answer' id='sendmessage" + messages[i].SenderId + "'>Ответить </button>";
            }
            result += "<span class='notification'> Не просмотрено</span>";
        }
        result += "<div class='message_full_text'>" + messages[i].Text + " </div></div>";
    }
    $(".message_container").html(result);
    ShowInputPagination(messages);
}

function ShowOutputMessages(messages, firstindex, secondindex) {
    if (secondindex > messages.length) {
        secondindex = messages.length;
        firstindex = 0;
    }
    var result = "";
    for (var i = firstindex; i < secondindex; i++) {
        if (messages[i].IsSeen === true) {
            result += "<div class='outbox_message' id='message" + messages[i].Id + "'><div class='username'>Кому: " + messages[i].ReceiverUser.UserName + " </div> <div class='text'>" + messages[i].Text + "</div> ";
            if (messages[i].IsInvitation === true) {
                if (messages[i].IsRefused === true)
                    result += "<span class='notification'> Отклонено</span>";
                else {
                    result += "<span class='notification'> Принято</span>";
                }
            }
            else {
                result += "<br /> <button class='watch'> Просмотреть </button>";
            }
            result += "<span class='notification'> Просмотрено</span> </div>";
        }
        else {
            result += "<div class='outbox_message notread' id='message" + messages[i].Id + "'><div class='username'>Кому: " + messages[i].ReceiverUser.UserName + " </div> <div class='text'>" + messages[i].Text + "</div> ";

            result += "<br /> <button class='watch'> Просмотреть </button>";
            result += "<span class='notification'> Не просмотрено</span> </div>";
        }
        result += "<div class='message_full_text'>" + messages[i].Text + " </div></div>";
    }
    $(".message_container").html(result);
    ShowOutputPagination(messages);
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


function ShowInputPagination(messages) {
    var currentPageNumber = DefineCurrentNumber();
    $('#light-pagination').pagination({
        items: messages.length,
        itemsOnPage: 3,
        currentPage: currentPageNumber,
        cssStyle: 'light-theme',
        onPageClick: function () {
            var currentNumber = DefineCurrentNumber();
            if ((messages.length - (currentNumber - 1) * 3) >= 3)
                ShowInputMessages(messages, (currentNumber - 1) * 3, (currentNumber - 1) * 3 + 3);
            else
                ShowInputMessages(messages, (currentNumber - 1) * 3, messages.length);
        }
    });
}

function ShowOutputPagination(messages) {
    var currentPageNumber = DefineCurrentNumber();
    $('#light-pagination').pagination({
        items: messages.length,
        itemsOnPage: 3,
        currentPage: currentPageNumber,
        cssStyle: 'light-theme',
        onPageClick: function () {
            var currentNumber = DefineCurrentNumber();
            if ((messages.length - (currentNumber - 1) * 3) >= 3)
                ShowOutputMessages(messages, (currentNumber - 1) * 3, (currentNumber - 1) * 3 + 3);
            else
                ShowOutputMessages(messages, (currentNumber - 1) * 3, messages.length);
        }
    });
}

$(document).ready(function () {
    GetInputMessages(0, 3);
    var inputTimer = setInterval(function() {
        GetInputMessages(0, 3);
    }, 10000);
    $('#message1_1').click(function () {
        GetInputMessages(0, 3);
        inputTimer = setInterval(function () {
            GetInputMessages(0, 3);
        }, 10000);
    });

    $('#light-pagination').click(function() {
        clearInterval(inputTimer);
    });

    $('#message1_2').click(function () {
        clearInterval(inputTimer);
        GetOutputMessages(0, 3);
    });

    
    $('body').on('click', '.message_reset', function () {
        $('.message_dialog').hide();
        $('.message_box').hide();
        $('.message_view').hide();
        $('.message_text').val("");
    });

    $('body').on('click', '.answer', function () {
        $('.message_dialog').show();
        $('.message_box').show();
        $('.message_text').text("");
        var id = $(this).attr("id");
        var newId = id.replace(/sendmessage/g, "");
        $('.message_send').attr('id', newId);
    });

    $('body').on('click', '.confurmanswer', function () {
        var id = $(this).attr('id');
        var newId = id.replace(/confirm/g, "");
        var parentItem = $(this).parent();
        id = parentItem.attr('id');
        var messageId = id.replace(/message/g, "");
        UpdateMessage(messageId, false);
        GetUserId(function (senderId) {
            AddMatch(senderId, newId, function (macthId) {
                UpdateMessage(messageId, false);
                SendConfurmation(senderId, newId, macthId);
            });
            
        });
    });

    $('body').on('click', '.refuseanswer', function () {
        var id = $(this).attr('id');
        var newId = id.replace(/refuse/g, "");
        var parentItem = $(this).parent();
        id = parentItem.attr('id');
        var messageId = id.replace(/message/g, "");
        UpdateMessage(messageId, false);
        GetUserId(function (senderId) {
           
            UpdateMessage(messageId, false);
            SendRefusion(senderId, newId);

        });
    });

    $('body').on('click', '.inpox_message .watch', function () {
        var parentItem = $(this).parent();
        var id = parentItem.attr('id');
        var messageId = id.replace(/message/g, "");
        var text = $('#message' + messageId + ' .message_full_text').text();
        $('.message_view .text').text(text);
        $('.message_view').show();
        $('.message_dialog').show();
        clearInterval(inputTimer);
        UpdateMessage(messageId, false);
    });



    $('body').on('click', '.invitation', function () {
        var id = $(this).attr("id");
        var newId = id.replace(/invite/g, "");
        GetUserId(function(senderId) {
            SendInvitation(senderId, newId);
        });
        $('.message_send').attr('id', newId);
    });

    $('.message_send').click(function () {
        GetUserId(function (senderId) {
            var recieverId = $('.message_send').attr('id');
            SendMessage(senderId, recieverId);
            $('.message_text').val("");
            $('.message_dialog').hide();
            $('.message_box').hide();
        });
    });

});
    
