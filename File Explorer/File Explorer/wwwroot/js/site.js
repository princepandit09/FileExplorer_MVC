
$(document).ready(function () {
    getDrives()

    $("body").on('dblclick', "#files", function () {
        console.log("fromdblClick")

        console.log($(this).hasClass("tree-node expanded"))

        var dataPathValue = $(this).data('path');
        $("#navPathfld").prop("value", dataPathValue);


        if ($(this).hasClass("tree-node expanded") || $(this).hasClass("tree-node current expanded")) {

            //console.log("hello")

            console.log(dataPathValue)
           // getDirectories(dataPathValue, this);
            getDirectories(dataPathValue, this, function (response) {
                console.log(response)

                BindContentData(response)
               
            });


        }
        else {
            $(this).toggleClass("tree-node")
            getDirectories(dataPathValue, this, function (response) {
                console.log(response)

                BindContentData(response)

            });
           // console.log("hello")
        }

    });
    $("body").on('click', "#files", function () {

        var dataPathValue = $(this).data('path');
        //console.log(dataPathValue)
        $("#navPathfld").prop("value", dataPathValue);

        getDirectories(dataPathValue, this, function (response) {
            console.log(response)
            BindContentData(response)
        })
    });
});

function getDrives() {

   // $("#navPathfld").text = 'ThisPC'
    $("#navPathfld").prop("value", "ThisPc");
    $.ajax({
        "url": "/Home/GetInitialDriveAndDirectories",
        "method": "GET",
        contentType: JSON,
        "success": function (response) {
           // console.log(response)                    
            $.each(response, function (drive, files) {
                var driveListItem = ''

                driveListItem = '<li id="mainfiles" data-path="' + drive + '" data-icon="<span class=\'mif-star-full\'></span>" data-caption="' + drive + '" class="tree-node expanded">';
                driveListItem += '<span class="icon"><span class="mif-drive2"></span></span>'
                driveListItem += '<span class="caption">' + drive + '</span>'
                driveListItem += Bind_Li_Data(files)
                driveListItem += '<span class="node-toggle"></span></li>'

                $("#treemain").append(driveListItem);
            })
            InitialContentData(response)
        }
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

function getDirectories(path, element,callback) {
    console.log(element)
    var deferred = $.Deferred();

    $.ajax({
        "url": "/Home/GetInternalDriveAndDirectories",
        "method": "GET",
        contentType: JSON,
        data: { "path": path },
        "success": function (response) {
                       
            var driveListItem = ''
            $.each(response, function (drive, files) {
                driveListItem += Bind_Li_Data(files)
            })
            $(element).append(driveListItem);

            callback(response);
            //var lastElement = element:last";
            //console.log(lastElement)
           // $(element ).before('<div class="content"><div class="subcontent">Some stuff</div></div>');

          // Resolve the deferred object with the response
           
        },
        "error": function (error) {
            // Reject the deferred object with the error
            deferred.reject(error);
        }
    })

}

function BindInitialTreeView(response,element) { }

function BindContentData(response) {
    console.log("hii from content")
    console.log(response)
    var mainElement = ''

    //mainElement += '<li data-caption="" class="node-group expanded" ><div class="data"><div class="caption">Devices and disks</div></div>'
    $.each(response, function (drive, files) {
        mainElement += '<li><ul class="listview view-icons-medium">'
        $.each(files, function (index, file) {
                var lastIndex = file.item2.lastIndexOf('\\');

                var filename = file.item2.substring(lastIndex + 1);

                mainElement += '<li id="files" data-path="' + file.item2 + '" data-icon="images/folder.png" data-caption="' + filename + '" class="node"><label class="checkbox transition-on"><input type="checkbox" data-role="checkbox" data-style="1" data-role-checkbox="true" class=""><span class="check"></span><span class="caption"></span></label><span class="icon"><img src="images/folder.png"></span><div class="data"><div class="caption">' + filename + '</div></div></li>'
            
        })
    })

    mainElement += '</ul>'
    mainElement += '<span class="node-toggle"></span></li>'

    $("#listview").empty();
    $("#listview").append(mainElement);
}
function InitialContentData(response) {
    console.log("hii from content")
    console.log(response)
    var mainElement = ''

    mainElement += '<li data-caption="Devices and disks" class="node-group expanded" ><div class="data"><div class="caption">Devices and disks</div></div>'
    mainElement += '<ul class="listview view-icons-medium">'
    $.each(response, function (drive, files) {
        mainElement += '<li id="files" data-path="' + drive + '" data-icon="images/drive1.png" data-caption="' + drive + '" class="node"><label class="checkbox transition-on"><input type="checkbox" data-role="checkbox" data-style="1" data-role-checkbox="true" class=""><span class="check"></span><span class="caption"></span></label><span class="icon"><img src="images/drive1.png"></span><div class="data"><div class="caption">' + drive + '</div></div></li>'
    })

    mainElement += '</ul>'
    mainElement += ' <span class="node-toggle"></span></li>'
    $("#listview").append(mainElement);
}

   