var forward_path = [];
var Backward_path = [];
var oldPath;
var newPath;
var isDelete;
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
        refresh();
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

    // handle Pop of Cut Copy Paste
    $("body").on("keydown mousedown", "#Contentfiles", function (event) {
        if (event.ctrlKey && event.which === 1) {
            var PopupModal = $("#PopupModal")
            $("#PopupModal").modal("show");
            element = this
            console.log(element)

            // Position the context menu
            PopupModal.css({
                top: event.pageY + "px",
                left: event.pageX + "px",
                innerWidth: 300 + "px",
                outerWidth: 300 + "px",
                width: "300px"
            });

            //copy function
            $("#copy").click(function ()
            {
                //console.log(element)
                oldPath = $(element).data('path');
                //console.log(oldPath)
                isDelete = false;
                refresh();
            })
            $("#cut").click(function ()
            {
                //console.log(element)
                oldPath = $(element).data('path');
                //console.log(oldPath)
                isDelete = true;
                $(element).find(".icon img").fadeTo("fast", 0.15);
                
            })
            $("#delete").click(function ()
            {
                //console.log(element)
                oldPath = $(element).data('path');
                //console.log(oldPath)
                isDelete = true;
                $.ajax({
                    "url": "/Home/DeleteData",
                    "method": "GET",
                    contentType: JSON,
                    data: {"oldPath":oldPath},
                    "success": function (response) {
                        if (response.ok) {
                            refresh();
                        }
                    }
                }) 
                

            })
            $("#paste").click(function ()
            {
                newPath = $(element).data('path');
                console.log(oldPath)
                console.log(newPath)
                var data = {
                    "newPath": newPath,
                    "oldPath": oldPath,
                    "isDelete":isDelete
                }
                $.ajax({
                    "url": "/Home/SaveData",
                    "method": "GET",
                    contentType: JSON,
                    data: data,
                    "success": function (response) {
                        if (response.ok) {
                            refresh();
                        }
                    }
                }) 
              
            })
            // Hide the context menu when clicking outside of it and refresh the content
            $(document).on("click", function () {
                $("#PopupModal").modal("hide");
            });   
        } 
    });

    $("body").on('click', "#mainfiles", function () {
        var dataPathValue = $(this).data('path');
        Backward_path.push(dataPathValue)
        $("#navPathfld").prop("value", dataPathValue);
        getDirectories(dataPathValue, function (response) {
                BindContentData(response)
        });
        getFiles(dataPathValue, function (response) {
           // console.log(response)
            BindContentDataFiles(response)
        })

    });
    $("body").on('dblclick', "#Contentfiles", function () {
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
        console.log(dataPathValue)
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


function refresh() {
    var path = $("#navPathfld").val()
    if (path === "ThisPc") {
        getDrives(function (response) {
            InitialContentData(response)
        })
    }
    else {
        getDirectories(path, function (response) {
            BindContentData(response)
        })
        getFiles(path, function (response) {
            BindContentDataFiles(response)
        })
    }
}
