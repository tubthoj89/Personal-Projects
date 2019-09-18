$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:53140/api/history/calculations',
        success: function (results, status) {
            $.each(results, function (index, cal) {
                var calculation = cal.MathCal;
                $('#calculationHistory').append('<div>' + calculation + '</div>');
            });
        },
        error: function (ts) {
            alert(ts);
        }
    });
    timeout();
});
function timeout() {
    setTimeout(function () {
        $.ajax({
            type: 'GET',
            url: 'http://localhost:53140/api/history/calculations',
            success: function (results, status) {
                $.each(results, function (index, cal) {
                    var calculation = cal.MathCal;
                    $('#calculationHistory').load('<div>' + calculation + '</div>');
                });
            },
            error: function (ts) {
                alert(ts);
            }
        });
        timeout();
    }, 10000);
}
