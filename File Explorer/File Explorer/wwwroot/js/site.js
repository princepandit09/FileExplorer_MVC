var forward_path = [];
var Backward_path = [];
$(document).ready(function () {
    getDrives(function (response) {
        BindInitialTreeView(response);
        InitialContentData(response)
    })

    $("#backBtn").click(function () {
        // Check backward_path for data
        if (Backward_path.length > 0) {
            // backward_path has data
            forward_path.push($("#navPathfld").val())
            $("#navPathfld").val(Backward_path.pop())
            var path = $("#navPathfld").val()
            getDirectories(path, function (response) {
                BindContentData(response)
            });
            getFiles(path, function (response) {
                BindContentDataFiles(response)
            })
        }
        else {
            getDrives(function (response) {
                InitialContentData(response)
            })
        }
       
    })
    $("#nextBtn").click(function () {  
        if (forward_path.length > 0) {
            Backward_path.push($("#navPathfld").val())
            console.log(Backward_path)
            $("#navPathfld").val(forward_path.pop())
            var path = $("#navPathfld").val()
            getDirectories(path, function (response) {
                BindContentData(response)
            });
            getFiles(path, function (response) {
                BindContentDataFiles(response)
            })
        }
    })

    $("#refreshBtn").click(function () {
        var path = $("#navPathfld").val()
        getDirectories(path, function (response) {
            BindContentData(response)
        })
        getFiles(path, function (response) {
            BindContentDataFiles(response)
        })
    })

    $("#searchBar").keyup(function () {
       // alert("ok")
        var path = $("#navPathfld").val()
        
        var searchPattern = $("#searchBar").val()
        if (searchPattern.length > 0) {
            SearchDirectoriesByName(path, searchPattern, function (response) {
                BindContentData(response)
            })
            SearchFilesByName(path, searchPattern, function (response) {
                console.log(response)
                BindContentDataFiles(response)

            })
        }
        else {
            getDirectories(path, function (response) {
                BindContentData(response)
            });
            getFiles(path, function (response) {
                // console.log(response)
                BindContentDataFiles(response)
            })
        }

    })

    $("body").on('dblclick', "#Contentfiles", function () {
        var element = this;
        console.log(element)
        var dataPathValue = $(this).data('path');
        Backward_path.push(dataPathValue)
        $("#navPathfld").prop("value", dataPathValue);
        getDirectories(dataPathValue, function (response) {
                BindContentData(response)
        });
        getFiles(dataPathValue, function (response) {
            console.log(response)
            BindContentDataFiles(response)
        })

    });
    $("body").on('dblclick', "#files", function () {
        var element = this;
        var dataPathValue = $(this).data('path');
        //for using back button
        Backward_path.push(dataPathValue)
        $("#navPathfld").prop("value", dataPathValue);

        if ($(this).hasClass("tree-node expanded") || $(this).hasClass("tree-node current expanded")) {
            getDirectories(dataPathValue, function (response) {
               // console.log(response)

                BindInternalTreeView(response, element)
                BindContentData(response)

            });

            getFiles(dataPathValue, function (response) {
                   console.log(response)
                BindContentDataFiles(response)
            })


        }
    });

});

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

function BindInitialTreeView(response)
{
   $.each(response, function (drive, files) {
        var driveListItem = ''

        driveListItem = '<li id="mainfiles" data-path="' + drive + '" data-icon="<span class=\'mif-star-full\'></span>" data-caption="' + drive + '" class="tree-node expanded">';
        driveListItem += '<span class="icon"><span class="mif-drive2"></span></span>'
        driveListItem += '<span class="caption">' + drive + '</span>'
        driveListItem += Bind_Li_Data(files)
        driveListItem += '<span class="node-toggle"></span></li>'

        $("#treemain").append(driveListItem);
    })
}
function Bind_Li_Data(files) {

    var filesitem = '<ul id="myList">'
    $.each(files, function (index, file) {

        var lastIndex = file.item2.lastIndexOf('\\');

        var filename = file.item2.substring(lastIndex + 1);

        filesitem += '<li id="files" data-path="' + file.item2 + '" data-icon="<span class=\'mif-library\'></span>" data-caption="' + filename + '"'
        if (file.item1 > 0) {
            filesitem += 'class="tree-node"'
        }
        filesitem += '><span class="icon"><span class="mif-folder"></span></span>'
        filesitem += '<span class="caption">' + filename + '</span>'
        if (file.item1 > 0) {
            filesitem += '<span class="node-toggle"></span>'
        }
        filesitem += '</li>'
    });
    filesitem += '</ul>'
    return filesitem

}

function getDirectories(path,callback) {
   // console.log(element)
    $.ajax({
        "url": "/Home/GetInternalDriveAndDirectories",
        "method": "GET",
        "async": false,
        contentType: JSON,
        data: { "path": path },
        "success": function (response) {

            callback(response);
        },
    })

}

function BindInternalTreeView(response,element)
{
   
    var driveListItem = ''
    $.each(response, function (drive, files) {
        driveListItem += Bind_Li_Data(files)
    })
    var internalEle = $(element).find('ul')
      internalEle.remove()
    var lastSpan = $(element).find('span:last');
    lastSpan.before(driveListItem)

}

function BindContentData(response) {
    var mainElement = ''
    $.each(response, function (drive, files) {
        mainElement += '<li><ul class="listview view-icons-medium">'
        $.each(files, function (index, file) {
                var lastIndex = file.item2.lastIndexOf('\\');

                var filename = file.item2.substring(lastIndex + 1);

                mainElement += '<li id="Contentfiles" data-path="' + file.item2 + '" data-icon="images/folder.png" data-caption="' + filename + '" class="node"><label class="checkbox transition-on"><input type="checkbox" data-role="checkbox" data-style="1" data-role-checkbox="true" class=""><span class="check"></span><span class="caption"></span></label><span class="icon"><img src="images/folder.png"></span><div class="data"><div class="caption">' + filename + '</div></div></li>'
            
        })
    })

    mainElement += '</ul>'
    mainElement += '</li>'

    $("#listview").empty();
    $("#listview").append(mainElement);
}
function InitialContentData(response) {

    var mainElement = ''

    mainElement += '<li data-caption="Devices and disks" class="node-group expanded" ><div class="data"><div class="caption">Devices and disks</div></div>'
    mainElement += '<ul class="listview view-icons-medium">'
    $.each(response, function (drive, files) {
        mainElement += '<li id="Contentfiles" data-path="' + drive + '" data-icon="images/drive.png" data-caption="' + drive + '" class="node"><label class="checkbox transition-on"><input type="checkbox" data-role="checkbox" data-style="1" data-role-checkbox="true" class=""><span class="check"></span><span class="caption"></span></label><span class="icon"><img src="images/drive.png"></span><div class="data"><div class="caption">' + drive + '</div></div></li>'
    })

    mainElement += '</ul>'
    mainElement += '</li>'
    $("#listview").empty();
    $("#listview").append(mainElement);
}

function getFiles(path, callback) {
   
    $.ajax({
        "url": "/Home/GetInternalFiles",
        "method": "GET",
        contentType: JSON,
        data: { "path": path },
        "success": function (response) {
            callback(response);
        },
    })

}

function BindContentDataFiles(response) {
        var mainElement = ''
    $.each(response, function (drive, files) {
        mainElement += '<li><ul class="listview view-icons-medium">'
        $.each(files, function (index, file) {
            //console.log(file.item2.lastIndexOf('\\'))
            var lastIndex = file.item2.lastIndexOf('\\');

            var filename = file.item2.substring(lastIndex + 1);

            mainElement += '<li id="Contentfiles" data-path="' + file.item2 + '"'
           // data - icon="images/folder.png"
            var extension = file.item1;
            switch (extension) {
                case '.mp3':
                    mainElement += 'data - icon="images/audio.png"'
                    break;
                case '.pdf':
                    mainElement += 'data - icon="images/pdf.png"'
                    break;
                case '.docx':
                    mainElement += 'data - icon="images/docx.png"'
                    break;
                case '.sql':
                    mainElement += 'data - icon="images/sql.png"'
                    break;
                case '.mp4':
                    mainElement += 'data - icon="images/mp4.png"'
                    break;
                case '.txt':
                    mainElement += 'data - icon="images/text.png"'
                    break;
                case '.exe':
                    mainElement += 'data - icon="images/exe.png"'
                    break;
                case '.cs':
                    mainElement += 'data - icon="images/c-sharp.png"'
                    break;
                case '.zip':
                    mainElement += 'data - icon="images/Zip.jpeg"'
                    break;
                case '.jpg':
                case '.jpeg':
                case '.png':
                    mainElement += 'data-icon="images/image.png"'
                    break;
                default:
                    mainElement += 'data-icon="images/default.png"'
            }

            mainElement += 'data - caption="' + filename + '" class="node" ><label class="checkbox transition-on"><input type="checkbox" data-role="checkbox" data-style="1" data-role-checkbox="true" class=""><span class="check"></span><span class="caption"></span></label><span class="icon">'
           

            switch (extension) {
                case '.mp3':
                    mainElement += '<img src="images/audio.png">'
                    break;
                case '.pdf':
                    mainElement += '<img src="images/pdf.png">'
                    break;
                case '.docx':
                    mainElement += '<img src="images/docx.png">'
                    break;
                case '.sql':
                    mainElement += '<img src="images/sql.png">'
                    break;
                case '.mp4':
                    mainElement += '<img src="images/mp4.png">'
                    break;
                case '.txt':
                    mainElement += '<img src="images/text.png">'
                    break;
                case '.exe':
                    mainElement += '<img src="images/exe.png">'
                    break;
                case '.cs':
                    mainElement += '<img src="images/c-sharp.png">'
                    break;
                case '.zip':
                    mainElement += '<img src="images/Zip.jpeg">'
                    break;
                case '.jpg':
                case '.jpeg':
                case '.png':
                    mainElement += '<img src="images/image.png">'
                    break;
                default:
                    mainElement += '<img src="images/default.png">'
            }
            
            mainElement += '</span > <div class="data"><div class="caption">' + filename + '</div></div></li > '

        })
    })

    mainElement += '</ul>'
    mainElement += '</li>'

   // $("#listview").empty();
    $("#listview").append(mainElement);
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