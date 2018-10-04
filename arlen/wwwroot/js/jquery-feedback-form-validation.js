var captcha_done = false;
// Начальное состояния сабмит-кнопки неактивное
$('#submitButton').css('background', 'rgba(50, 50, 50, 0.5)');
$('#submitButton').attr('disabled', true);

function validate() {
    /* Функция проверки полей на пустоту */
    var name = $('#feedback-name-field').val();
    var phone = $('#feedback-phone-field').val();
    var email = $('#feedback-email-field').val();
    var message = $('#feedback-message-field').val();

    if (name != "" && phone != "" && email != "" && message != "" && captcha_done) {
        $('#submitButton').css('background', 'var(--orange-color)');
        $('#submitButton').attr('disabled', false);
    }
    else {
        $('#submitButton').css('background', 'rgba(50, 50, 50, 0.5)');
        $('#submitButton').attr('disabled', true);
    }
}

function captcha_passed() {
    /* Срабатывает, когда пройден captcha challenge */
    captcha_done = true;
    validate();
}

// Проверяем поля на пустоту при изменении каждого из них
$('#feedback-name-field').on("input", validate);
$('#feedback-phone-field').on("input", validate);
$('#feedback-email-field').on("input", validate);
$('#feedback-message-field').on("input", validate);
