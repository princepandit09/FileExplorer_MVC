function getDrives()
{
    $.ajax({
        "url": "/Home/GetInitialDriveAndDirectories",
        "method": "GET",
        contentType: JSON,
        "success": function (response) {
            console.log(response)
            var driveListItem =''
            $.each(response, function (drive, files) {

                driveListItem = '<li data-icon="<span class=\'mif-star-full\'></span>" data-caption="' + drive + '">';
                driveListItem += '<span class="icon"><span class="mif-drive2"></span></span>'
                driveListItem += '<span class="caption">' + drive + '</span>'
               driveListItem += Bind_Li_Data(files)
               driveListItem += '</li>'

            })

            $("#treemain").append(driveListItem);

           

        }
    })
}

function Bind_Li_Data(files) {

    var filesitem = '<ul>'
    $.each(files, function (index, file) {

        var lastIndex = file.item2.lastIndexOf('\\');

        var filename = file.item2.substring(lastIndex + 1);

        filesitem += '<li id="files" data-path="'+file+'" data-icon="<span class=\'mif-library\'></span>" data-caption="' + filename + '">'
        filesitem += '<span class="icon"><span class="mif-folder"></span></span>'
        filesitem += '<span class="caption">' + filename + '</span></li>'

                   
    });
    filesitem += '</ul>'
    return filesitem

}