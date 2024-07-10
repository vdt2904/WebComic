
function getlist(pageNumber) {
    $.ajax({
        url: baseUrl + 'api/Genres/get/' + pageNumber,
        method: 'GET',
        contentType: 'application/json',
        dataType: 'json',
        headers: {
            'Authorization': 'Bearer '
        },
        error: function (response) {
            console.log("Error:", response);
        },
        success: function (response) {

            let data = response.data;
            let table = '';
            for (var i = 0; i < data.length; i++) {
                table += '<tr>';
                table += '<th scope="row">' + (i + 1) + '</th>';
                table += '<td>' + data[i].genreId + '</td>';
                table += '<td>' + data[i].name + '</td>';
                table += '<td>#No data</td>'; // Placeholder for comicGenres
                table += '<td><ul class="d-flex justify-content-center">';
                table += '<li class="mr-3"><a href="/admin/genre/' + data[i].genreId + '" class="text-secondary"><i class="fa fa-edit"></i></a></li>';
                table += '<li><a onclick="deleteGenre(' + data[i].genreId +')" class="text-danger"><i class="ti-trash"></i></a></li>';
                table += '</ul></td>';
                table += '</tr>';
            }
            document.getElementById('list').innerHTML = table;
            updatePagination(response.pageNumber, response.totalPages);
        },
        fail: function (response) {
            console.log("Failed to fetch data");
        }
    });
}

function updatePagination(currentPage, totalPages) {
    let paginationHtml = '<ul class="pagination">';
    if (currentPage == 1) {
        paginationHtml += '<li class="paginate_button page-item previous" ><a aria-controls="dataTable" tabindex="0" class="page-link">Previous</a></li>';
    } else {
        paginationHtml += '<li class="paginate_button page-item previous"><a href="/admin/genre?page='+ (currentPage - 1) + '" aria-controls="dataTable" tabindex="0" class="page-link">Previous</a></li>';
    }

    for (let i = 1; i <= totalPages; i++) {
        if (i === currentPage) {
            paginationHtml += '<li class="paginate_button page-item active"><a href="/admin/genre?page='+i+'" aria-controls="dataTable" tabindex="0" class="page-link">' + i + '</a></li>';
        } else {
            paginationHtml += '<li class="paginate_button page-item"><a href="/admin/genre?page=' + i +'" aria-controls="dataTable" tabindex="0" class="page-link">' + i + '</a></li>';
        }
    }
    if (currentPage == totalPages) {
        paginationHtml += '<li class="paginate_button page-item next"><a aria-controls="dataTable" tabindex="0" class="page-link">Next</a></li>';
    }
    else {

    paginationHtml += '<li class="paginate_button page-item next"><a href="/admin/genre?page=' + (currentPage + 1) + '" aria-controls="dataTable" tabindex="0" class="page-link">Next</a></li>';
    }
    paginationHtml += '</ul>';

    document.getElementById('dataTable_paginate').innerHTML = paginationHtml;
}

function postGene() {  
    $.ajax({
        url: '/api/Genres',
        type: 'POST',
        contentType: 'application/json',
        headers: {
            'Authorization': 'Bearer '
        },
        data: JSON.stringify({ name: $('#genreName').val() }),
        success: function (response) {
            window.location.href = '/admin/genre';
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('#errorSpan').html('Lỗi: ' + jqXHR.responseText);
        }
    });
}

function deleteGenre(id) {
    $.ajax({
        url: '/api/Genres/' + id,
        type: 'DELETE',
        contentType: 'application/json',
        headers: {
            'Authorization': 'Bearer '
        },
        success: function (response) {
            $('#Delete').html('Xóa thành công!').removeClass().addClass('text-success');
            getlist(1)
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('#Delete').html('Lỗi: ' + jqXHR.responseText).removeClass().addClass('text-danger');
        }
    });
}

function getdetail() {
    $.ajax({
        url: baseUrl + 'api/Genres/' + GenreId,
        method: 'GET',
        contentType: 'application/json',
        dataType: 'json',
        headers: {
            'Authorization': 'Bearer '
        },
        error: function (response) {
            console.log("Error:", response);
        },
        success: function (response) {
            $('#genreName').val(response.name);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(jqXHR.responseText)
            window.location.href = '/admin/error';
        }
    });
}

function editgenre() {
    $.ajax({
        url: '/api/Genres/' + GenreId,
        type: 'PUT',
        contentType: 'application/json',
        headers: {
            'Authorization': 'Bearer '
        },
        data: JSON.stringify({ genreId: GenreId,name: $('#genreName').val() }),
        success: function (response) {
            window.location.href = '/admin/genre';
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('#errorSpan').html('Lỗi: ' + jqXHR.responseText);
        }
    });
}
            
                                   

                                    
                                   
                                        
                                            
                                            
                                        
                                    