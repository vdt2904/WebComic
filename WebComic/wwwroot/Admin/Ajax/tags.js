
function getAllTags(pageNumber) {
    const pageSize = 2; // Kích thước trang

    // Gọi API để lấy danh sách các tags
    $.ajax({
        url: `https://localhost:7107/api/tags?pageNumber=${pageNumber}`,
        method: 'GET',
        contentType: 'application/json',
        dataType: 'json',
        success: function (response, status, xhr) {
            // Lấy thông tin phân trang từ header X-Pagination
            const paginationHeader = xhr.getResponseHeader('X-Pagination');
            const paginationInfo = JSON.parse(paginationHeader);

            // Hiển thị thông tin phân trang (ví dụ: in ra console)
            console.log('Total records:', paginationInfo.TotalRecords);
            console.log('Total pages:', paginationInfo.TotalPages);
            console.log('Current page:', paginationInfo.CurrentPage);
            console.log('Page size:', paginationInfo.PageSize);

            // Xử lý dữ liệu tags được trả về
            const tags = response;
            console.log('Tags:', tags);

            // Ví dụ: Hiển thị dữ liệu tags lên giao diện
            renderTags(tags);

              renderPagination(paginationInfo);
        },
        error: function (xhr, status, error) {
            console.error('Error fetching tags:', error);
            // Xử lý lỗi nếu cần thiết
        }
    });
}

// Gọi hàm getAllTags với pageNumber mong muốn
getAllTags(1); // Ví dụ: Lấy trang đầu tiên


function renderTags(response) {
    const len = response.items.length;
    let table = '';
    for (var i = 0; i < len; ++i) {
        table = table + '<tr>';
        table = table + '<td>' + response.items[i].tagId.trim() + '</td>';
        table = table + '<td>' + response.items[i].name.trim() + '</td>';

     
        table = table + '<td>' + ' <ul class="d-flex justify-content-center"> ';
        table = table + '< li class="mr-3" > <a href="#" class="text-secondary"><i class="fa fa-edit"></i></a></li >';
        table = table + '< li > <a href="#" class="text-danger"><i class="ti-trash"></i></a></li ></ul > ' + '</td > ';
        table = table + '< li > <a href="#" class="text-danger"><i class="ti-trash"></i></a></li ></ul > ' + '</td > ';
        table = table + '<\tr>';
    }
    document.getElementById('tags_body').innerHTML = table;
}
