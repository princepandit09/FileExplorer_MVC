function getDrives(callback) {

    $("#navPathfld").prop("value", "ThisPc");
    $.ajax({
        "url": "/Home/GetInitialDriveAndDirectories",
        "method": "GET",
        contentType: JSON,
        "success": function (response) {
            callback(response);
        }
    })
}

function getDirectories(path, callback) {
    // console.log(element)
    $.ajax({
        "url": "/Home/GetInternalDriveAndDirectories",
        "method": "GET",
        "async": false,
        contentType: JSON,
        data: { "path": path },
        "success": function (response) {
            // console.log(response.data)
            callback(response.data);
        },
    })

}

function SearchDirectoriesByName(path, searchPattren, callback) {
    data = {
        "path": path,
        "searchPattern": searchPattren
    }
    $.ajax({
        "url": "/Home/GetTnternalDirectoriesByName",
        "method": "GET",
        contentType: JSON,
        data: data,
        "success": function (response) {
            callback(response);
        },
    })
}
