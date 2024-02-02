function BindInitialTreeView(response) {
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

        var filename = file.item2.substring(lastIndex + 1, lastIndex + 15);

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
function BindInternalTreeView(response, element) {
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

            var filename = file.item2.substring(lastIndex + 1, lastIndex + 15);

            mainElement += '<li id="Contentfiles" data-path="' + file.item2 + '" data-caption="' + filename + '" class="node"><label class="checkbox transition-on"><input type="checkbox" data-role="checkbox" data-style="1" data-role-checkbox="true" class=""><span class="check"></span><span class="caption"></span></label><span class="icon">'
            mainElement += '<div class="folder"></div>'
            mainElement +='</span><div class="data"><div class="caption">' + filename + '</div></div></li>'

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
        mainElement += '<li id="Contentfiles" data-path="' + drive + '" data-caption="' + drive + '" class="node"><label class="checkbox transition-on"><input type="checkbox" data-role="checkbox" data-style="1" data-role-checkbox="true" class=""><span class="check"></span><span class="caption"></span></label><span class="icon">'
        mainElement += '<div class="drive"></div>'
        mainElement += '</span><div class="data"><div class="caption">' + drive + '</div></div></li>'
    })

    mainElement += '</ul>'
    mainElement += '</li>'
    $("#listview").empty();
    $("#listview").append(mainElement);
}
function BindContentDataFiles(response) {
    var mainElement = ''
    $.each(response, function (drive, files) {
        mainElement += '<li><ul class="listview view-icons-medium">'
        $.each(files, function (index, file) {
            //console.log(file.item2.lastIndexOf('\\'))
            var lastIndex = file.item2.lastIndexOf('\\');

            var filename = file.item2.substring(lastIndex + 1, lastIndex + 15);

            mainElement += '<li id="Contentfiles" data-path="' + file.item2 + '"'

            mainElement += 'data - caption="' + filename + '" class="node" ><label class="checkbox transition-on"><input type="checkbox" data-role="checkbox" data-style="1" data-role-checkbox="true" class=""><span class="check"></span><span class="caption"></span></label><span class="icon">'

            var extension = file.item1;

            switch (extension) {
                case '.mp3':
                    mainElement +='<div class="audio"></div>'
                    break;
                case '.pdf':
                    mainElement += '<div class="pdf"></div>'
                    break;
                case '.docx':
                    mainElement += '<div class="docx"></div>'
                    break;
                case '.sql':
                    mainElement += '<div class="sql"></div>'
                    break;
                case '.mp4':
                case '.mpg':
                    mainElement += '<div class="mp4"></div>'
                    break;
                case '.txt':
                    mainElement += '<div class="txt"></div>'
                    break;
                case '.exe':
                    mainElement += '<div class="exe"></div>'
                    break;
                case '.cs':
                    mainElement += '<div class="c-sharp"></div>'
                    break;
                case '.zip':
                    mainElement += '<div class="zip"></div>'
                    break;
                case '.jpg':
                case '.jpeg':
                case '.png':
                    mainElement += '<div class="image"></div>'
                    break;
                default:
                    mainElement += '<div class="default"></div>'
            }

            mainElement += '</span > <div class="data"><div class="caption">' + filename + '</div></div></li > '

        })
    })

    mainElement += '</ul>'
    mainElement += '</li>'

    // $("#listview").empty();
    $("#listview").append(mainElement);
}