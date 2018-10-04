$(".content").text(function (i, text) {

    if (text.length >= 100) {
        text = text.substring(0, 100);
        var lastIndex = text.lastIndexOf(" ");       // позиция последнего пробела
        text = text.substring(0, lastIndex) + '...'; // обрезаем до последнего слова
    }

    $(this).text(text);

});