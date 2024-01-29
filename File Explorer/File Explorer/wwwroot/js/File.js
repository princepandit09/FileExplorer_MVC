function getFiles(path, callback) {

    $.ajax({
        "url": "/Home/GetInternalFiles",
        "method": "GET",
        contentType: JSON,
        data: { "path": path },
        "success": function (response) {
            callback(response.data);
        },
    })

}

function SearchFilesByName(path, searchPattren, callback) {
    data = {
        "path": path,
        "searchPattern": searchPattren
    }
    $.ajax({
        "url": "/Home/GetInternalFilesByName",
        "method": "GET",
        contentType: JSON,
        data: data,
        "success": function (response) {
            callback(response);
        },
    })
}
