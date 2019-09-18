$(document).ready(function () {
 var balance = 0;
  $('#dollar-button').click(function() {
    balance += 1.00;
    updatedBalance();
  });

  $('#quarter-button').click(function() {
    balance += .25;
    updatedBalance();
  });

  $('#dime-button').click(function() {
    balance += .10;
    updatedBalance();
  });

  $('#nickel-button').click(function() {
    balance += .05;
    updatedBalance();
  });

  function updatedBalance(){
    $('#moneyinput').val(balance.toFixed(2));
  }

  $('#purchase-button').click(function() {
    var itemNumber = $('#item-input').val();
    var money = parseFloat($('#moneyinput').val()).toFixed(2);
    $.ajax({
      type: 'GET',
      url: 'http://localhost:8080/money/' + money + '/item/' + itemNumber,
      success: function(data) {
        $('#changes-input').val(data.quarters + ' quarter(s)' + data.dimes + ' dime(s)' + data.nickels + 'nickle(s)' + data.pennies + 'pennie(s)');
        $('#moneyinput').val('$0.00');
        $('#messages-input').val('Thank You!');
        minusItems();
        balance = 0;

      },
      error: function(xhr) {
        var odj = JSON.parse(xhr.responseText);
        $('#messages-input').val(odj.message);
        //alert('Fail');
      }
    });
  });

    $('#changes-button').click(function() {
      minusItems();
      checkBalance();
      $('#item-input').val('');
      $('#messages-input').val('');
      $('#moneyinput').val('$0.00');
      balance = 0;
    });
});

function checkBalance(){
  var inputMoney = $('#moneyinput').val()*100;
  var quarters = parseInt(inputMoney/25);
  quarters%=25;
  var dimes = parseInt(inputMoney/10);
  dimes%=10;
  var nickels = parseInt(inputMoney/5);
  nickels%=5;
  $('#changes-input').val(quarters + ' quarter(s)' + dimes + ' dime(s)' + nickels + 'nickle(s)');

}

function setItem(newId) {
  $('#item-input').val(newId);
}

function minusItems() {
  $('#itemColumn').empty();
    $.ajax({
      type: 'GET',
      url: 'http://localhost:8080/items',
      success: function(items) {
    $('#quan1').text(items[0].quantity);
    $('#quan2').text(items[1].quantity);
    $('#quan3').text(items[2].quantity);
    $('#quan4').text(items[3].quantity);
    $('#quan5').text(items[4].quantity);
    $('#quan6').text(items[5].quantity);
    $('#quan7').text(items[6].quantity);
    $('#quan8').text(items[7].quantity);
    $('#quan9').text(items[8].quantity);

    $('#price1').text(parseFloat(items[0].price).toFixed(2));
    $('#price2').text(parseFloat(items[1].price).toFixed(2));
    $('#price3').text(parseFloat(items[2].price).toFixed(2));
    $('#price4').text(parseFloat(items[3].price).toFixed(2));
    $('#price5').text(parseFloat(items[4].price).toFixed(2));
    $('#price6').text(parseFloat(items[5].price).toFixed(2));
    $('#price7').text(parseFloat(items[6].price).toFixed(2));
    $('#price8').text(parseFloat(items[7].price).toFixed(2));
    $('#price9').text(parseFloat(items[8].price).toFixed(2));

    $('#name1').text(items[0].name);
    $('#name2').text(items[1].name);
    $('#name3').text(items[2].name);
    $('#name4').text(items[3].name);
    $('#name5').text(items[4].name);
    $('#name6').text(items[5].name);
    $('#name7').text(items[6].name);
    $('#name8').text(items[7].name);
    $('#name9').text(items[8].name);

    $('#id1').text(items[0].id);
    $('#id2').text(items[1].id);
    $('#id3').text(items[2].id);
    $('#id4').text(items[3].id);
    $('#id5').text(items[4].id);
    $('#id6').text(items[5].id);
    $('#id7').text(items[6].id);
    $('#id8').text(items[7].id);
    $('#id9').text(items[8].id);
    },
    error: function(response) {
      alert('Site Down');
    }
  });
}
