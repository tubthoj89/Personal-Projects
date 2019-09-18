var display = null;
var edit = null;
var create = null;
var index = null;

$(document).ready(function () {
    setupPage();
    displayPage();
    displayMovie();
    
    $('#create').click(function(){
        createPage();
        $('#create-Create').click(function(){
            var haveValidationErrors = checkAndDisplayValidationErrors($('#CreateDiv').find('input'));

            if(haveValidationErrors) {
                return false;
            }
            $.ajax({
                type: 'POST',
                url: 'http://localhost:59985/dvd',
                data: JSON.stringify({
                    title: $('#Create-title').val(),
                    realeaseYear: $('#Create-release').val(),
                    director: $('#Create-director').val(),
                    rating: $('#Create-Rating').val(),
                    notes: $('#Create-Note').val()
                }),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                'dataType': 'json',
                success: function() {
                    $('#Create-title').val('');
                    $('#Create-release').val('');
                    $('#Create-director').val('');
                    $('#Create-Rating').val('');
                    $('#Create-Note').val('');
                    setupPage();
                    displayPage();
                    displayMovie();
                },
                error: function() {
                    $('#errorMessages')
                   .append($('<li>')
                   .attr({class: 'list-group-item list-group-item-danger'})
                   .text('Error calling web service.  Please try again later.'));
                }
            });
        });
    });

    $('#Edit-save').click(function() {
        var haveValidationErrors = checkAndDisplayValidationErrors($('#EditDiv').find('input'));
        if (haveValidationErrors) {

            return false;
        }
        $.ajax({
            type: 'PUT',
            url: 'http://localhost:59985/dvd/' + $('#dvdId').val(),
            data: JSON.stringify({
                title: $('#Edit-title').val(),
                realeaseYear: $('#Edit-release').val(),
                director: $('#Edit-director').val(),
                rating: $('#Edit-rating').val(),
                notes: $('#Edit-Note').val()
            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'dataType': 'json',
            success: function() {
                $('#errorMessages').empty();
                setupPage();
                displayPage();
                displayMovie();
                
            },
            error: function() {
                $('#errorMessages')
                .append($('<li>')
                .attr({class: 'list-group-item list-group-item-danger'})
                .text('Error calling web service.  Please try again later.'));
            }
        });
    });

    $('#searchDvd').click(function(){
        var term = $('#searchInput').val();
        var category = $('#Select').val();
        var tbody = $('#movieTable');
        clearTable();
        $.ajax({
            type: 'GET',
            url: 'http://localhost:59985/dvds/' + category + '/' + term,
            success: function(data, status){
                $.each(data, function(index, item){
                    var id = item.dvdId;
                    var title = item.title;
                    var release = item.realeaseYear;
                    var director = item.director;
                    var rate = item.rating;
    
                    var row = '<tr>';
                    row += '<td>' + id + '</td>';
                    row += '<td>' + title + '</td>';
                    row += '<td>' + release + '</td>';
                    row += '<td>' + director + '</td>';
                    row += '<td>' + rate + '</td>';
                    row += '<td><a onclick="showEditForm(' + id + ')">Edit</a></td>';
                    row += '<td><a onclick="deleteDVD(' + id + ')">Delete</a></td>';
                    row += '</tr>';
                    tbody.append(row);
                })
            },
            error: function() {
                $('#errorMessages')
                .append($('<li>')
                .attr({class: 'list-group-item list-group-item-danger'})
                .text('Error calling web service.  Please try again later.'));
            }
        })
    });

    $('#Create-cancel').click(function() {
        $('#Create-title').val('');
        $('#Create-release').val('');
        $('#Create-director').val('');
        $('#Create-Rating').val('');
        $('#Create-Note').val('');
        setupPage();
        displayPage();
        displayMovie();
    });

    $('#Edit-cancel').click(function() {
        $('#Edit-title').val('');
        $('#Edit-release').val('');
        $('#Edit-director').val('');
        $('#Edit-rating').val('');
        $('#Edit-Note').val('');
        setupPage();
        displayPage();
        displayMovie();
    });

});

function deleteDVD(dvdId) {
    $.ajax ({
        type: 'DELETE',
        url: 'http://localhost:59985/dvd/' + dvdId,
        success: function(status) {
            displayMovie();
        }
    });
}

function showEditForm(dvdId) {
    $('#errorMessages').empty();
    $('#Edi-title').val('');
    $('#Edit-release').val('');
    $('#Edit-director').val('');
    $('#Edit-rating').val('');
    $('#Edit-Note').val('');
    editPage();
    $.ajax({
        type: 'GET',
        url: 'http://localhost:59985/dvd/' + dvdId,
        success: function(data, status) {
            $('#Edit-title').val(data.title);
            $('#Edit-release').val(data.realeaseYear);
            $('#Edit-director').val(data.director);
            $('#Edit-rating').val(data.rating);
            $('#Edit-Note').val(data.notes);
            $('#dvdId').val(data.dvdId);
        },
        error: function() {
            $('#errorMessages')
            .append($('<li>')
            .attr({class: 'list-group-item list-group-item-danger'})
            .text('Error calling web service.  Please try again later.'));
        }
    });
}

function checkAndDisplayValidationErrors(input) {
    $('#errorMessages').empty();
    var errorMessages = [];
    input.each(function() {
        if(!this.validity.valid)
        {
            var errorField = $('label[for='+this.id+']').text();
            errorMessages.push(errorField + ' ' + this.validationMessage);
        }
    });
    if (errorMessages.length > 0){
        $.each(errorMessages,function(index,message){
            $('#errorMessages').append($('<li>').attr({class: 'list-group-item list-group-item-danger'}).text(message));
        });
        return true;
    } else {
        return false;
    }
}


function displayMovie() {
    clearTable();
    var tbody = $('#movieTable')
    $.ajax({
        type: 'GET',
        url: 'http://localhost:59985/dvds',
        success: function(data, status){
            $.each(data, function(index, item){
                var id = item.dvdId;
                var title = item.title;
                var release = item.realeaseYear;
                var director = item.director;
                var rate = item.rating;

                var row = '<tr>';
                row += '<td>' + id + '</td>';
                row += '<td><a onclick="showDvd(' + id + ')">'+ title +'</a></td>';
                row += '<td>' + release + '</td>';
                row += '<td>' + director + '</td>';
                row += '<td>' + rate + '</td>';
                row += '<td><a onclick="showEditForm(' + id + ')">Edit</a></td>';
                row += '<td><a onclick="deleteDVD(' + id + ')">Delete</a></td>';
                row += '</tr>';
                tbody.append(row);
            })
        },
        error: function() {
            $('#errorMessages')
            .append($('<li>')
            .attr({class: 'list-group-item list-group-item-danger'})
            .text('Error calling web service.  Please try again later.'));
        }
    });
}

function showDvd(dvdId){
    indexPage();
    $.ajax({
        type: 'GET',
        url: 'http://localhost:59985/dvd/' + dvdId,
        success: function(data, status) {
            $('#Success').val(data.title);
            $('#Success-release').val(data.realeaseYear);
            $('#Success-director').val(data.director);
            $('#Success-rating').val(data.rating);
            $('#Success-Note').val(data.notes);
            $('#Success-dvdId').val(data.dvdId);
        },
        error: function() {
            $('#errorMessages')
            .append($('<li>')
            .attr({class: 'list-group-item list-group-item-danger'})
            .text('Error calling web service.  Please try again later.'));
        }
    });
}

function clearTable() {
    $('#movieTable').empty();
}

function displayPage() {
    display.show();
    edit.hide();
    create.hide();
    index.hide();
}

function editPage() {
    display.hide();
    edit.show();
    create.hide();
    index.hide();
}

function createPage() {
    display.hide();
    edit.hide();
    create.show();
    index.hide();
}

function indexPage() {
    display.hide();
    edit.hide();
    create.hide();
    index.show();
}

function setupPage() {
    display = $('.DisplayDVD');
    edit = $('.EditDVD');
    create = $('.CreateDVD');
    index = $('.SuccessDVD');
}
