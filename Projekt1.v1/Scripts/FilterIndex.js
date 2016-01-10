    $(function () {
        var dropdownlist = $("#BodyType").data("kendoDropDownList");
        var start = $("#start");
        var end = $("#end");
        var model = $("#Model");
        var value = "";

        //var dsSort = [];
        //dsSort.push({ field: "ReleaseDate", dir: "desc" });
        //kendoGrid.dataSource.sort(dsSort);
        //kendoGrid.dataSource.read({ name: value });

        $("#Order").click(function () {
            //get value from dropdownlist --->>>> dropdownlist.value();
            var kendoGrid = $("#Grid").data('kendoGrid');
        
            if ($(this).val() == "Order") {
                $(this).val("To lower");
               
                if (value == "") {
                    var dsSort = [];
                    dsSort.push({ field: "BorrowCount", dir: "asc" });
                    kendoGrid.dataSource.read();
                    kendoGrid.dataSource.sort(dsSort);
                }
                else {
                    var dsSort = [];
                    dsSort.push({ field: "BorrowCount", dir: "asc" });
                    kendoGrid.dataSource.sort(dsSort);
                    kendoGrid.dataSource.read({ name: value });
                }
            }
            else if ($(this).val() == "To lower") {
                $(this).val("To upper");
                var kendoGrid = $("#Grid").data('kendoGrid');
              
                if (value == "") {
                    var dsSort = [];
                    dsSort.push({ field: "BorrowCount", dir: "desc" });
                    kendoGrid.dataSource.read();
                    kendoGrid.dataSource.sort(dsSort);
                }
                else {
                    var dsSort = [];
                    dsSort.push({ field: "BorrowCount", dir: "desc" });
                    kendoGrid.dataSource.sort(dsSort);
                    kendoGrid.dataSource.read({ name: value });
                }
            }
            else if ($(this).val() == "To upper") {
                $(this).val("Order");
                var kendoGrid = $("#Grid").data('kendoGrid');
                if (value == "") {
                    kendoGrid.dataSource.read();
                    kendoGrid.dataSource.sort({});
                }
                else {

                    
                    kendoGrid.dataSource.read({ name: value });
                   
                }

            }

        }
       );

        $("#Filter").click(function () {

            //model yes
            if (model.val() != "") {
                //bodytype yes
                if (dropdownlist.text() != "Select") {
                    if (start.val() !="" && end.val() == "") {
                       
                        var grid = $("#Grid").data("kendoGrid");
                        var newUrl = "/Filter/Car_ReadModBodyTypeMin";
                        grid.dataSource.transport.options.read.url = newUrl;
                        value = $("#Model").val() + "," + dropdownlist.value()+","+$("#start").val();
                        
                        grid.dataSource.read({ name: value });
                    }
                    else if (start.val() =="" && end.val() != "") {
                       
                        var newUrl = "/Filter/Car_ReadModBodyTypeMax";
                        var grid = $("#Grid").data("kendoGrid");
                        grid.dataSource.transport.options.read.url = newUrl;
                        value = $("#Model").val() + "," + dropdownlist.value() + "," + $("#end").val();
                       
                        grid.dataSource.read({ name: value });
                    }
                    else if (start.val() !="" && end.val() != "") {
 
                        var newUrl = "/Filter/Car_ReadModBodyTypeMinMax";
                        var grid = $("#Grid").data("kendoGrid");
                        grid.dataSource.transport.options.read.url = newUrl;
                        value = $("#Model").val() + "," + dropdownlist.value() + "," + $("#start").val() + "," + $("#end").val();
               
                        grid.dataSource.read({ name: value });
                    }
                    else if (start.val() == "" && end.val() == "") {

                        var grid = $("#Grid").data("kendoGrid");
                        var newUrl = "/Filter/Car_ReadModBodyType";
                        grid.dataSource.transport.options.read.url = newUrl;
                        value = $("#Model").val() + "," + dropdownlist.value();

                        grid.dataSource.read({ name: value });
                    }

                }//bodytype no
                else if (dropdownlist.text() == "Select") {
                    if (start.val() != "" && end.val() == "") {
  
                        var newUrl = "/Filter/Car_ReadModMin";
                        var grid = $("#Grid").data("kendoGrid");
                        grid.dataSource.transport.options.read.url = newUrl;
                        value = $("#Model").val() + ","  + $("#start").val();

                        grid.dataSource.read({ name: value });
                    }
                    else if (start.val() == "" && end.val() != "") {
                      
                        var newUrl = "/Filter/Car_ReadModMax";
                        var grid = $("#Grid").data("kendoGrid");
                        grid.dataSource.transport.options.read.url = newUrl;
                        value = $("#Model").val() + ","+  $("#end").val();
                       
                        grid.dataSource.read({ name: value });
                    }
                    else if (start.val() != "" && end.val() != "") {
                       
                        var newUrl = "/Filter/Car_ReadModBodyTypeMinMax";
                        var grid = $("#Grid").data("kendoGrid");
                        grid.dataSource.transport.options.read.url = newUrl;
                        value = $("#Model").val() + "," + $("#start").val() + "," + $("#end").val();
                       
                        grid.dataSource.read({ name: value });
                    }
                    else if (start.val() == "" && end.val() == "") {
                        
                        var grid = $("#Grid").data("kendoGrid");
                        var newUrl = "/Filter/Car_ReadMod";
                        grid.dataSource.transport.options.read.url = newUrl;
                        value = $("#Model").val();
                      
                        grid.dataSource.read({ name: value });
                    }

                }
            }//model no
            else {
                //bodytype yes
                if (dropdownlist.text() != "Select") {
                    if (start.val() !="" && end.val() == "") {
                          
                        var newUrl = "/Filter/Car_ReadBodyTypeMin";
                        var grid = $("#Grid").data("kendoGrid");
                        grid.dataSource.transport.options.read.url = newUrl;
                        value = dropdownlist.value() + "," + $("#start").val();
                           
                        grid.dataSource.read({ name: value });
                    }
                    else if (start.val() =="" && end.val() != "") {
                           
                        var newUrl = "/Filter/Car_ReadBodyTypeMax";
                        var grid = $("#Grid").data("kendoGrid");
                        grid.dataSource.transport.options.read.url = newUrl;
                        value = dropdownlist.value() + "," + $("#end").val();
                         
                        grid.dataSource.read({ name: value });
                    }
                    else if (start.val() !="" && end.val() != "") {
                            
                        var newUrl = "/Filter/Car_ReadBodyTypeMinMax";
                        var grid = $("#Grid").data("kendoGrid");
                        grid.dataSource.transport.options.read.url = newUrl;
                        value = dropdownlist.value() + "," + $("#start").val()+"," + $("#end").val();
                           
                        grid.dataSource.read({ name: value });
                    }
                    else if (start.val() == "" && end.val() == "") {
                          
                        var newUrl = "/Filter/Car_ReadBodyType";
                        var grid = $("#Grid").data("kendoGrid");
                        grid.dataSource.transport.options.read.url = newUrl;
                        value = dropdownlist.value();

                        grid.dataSource.read({ name: value });
                            
                    }  

                }//bodytype no
                else if (dropdownlist.text() == "Select") {
                    if (start.val() !="" && end.val() == "") {
                           
                        var newUrl = "/Filter/Car_ReadMin";
                        var grid = $("#Grid").data("kendoGrid");
                        grid.dataSource.transport.options.read.url = newUrl;
                        value =$("#start").val();
                          
                        grid.dataSource.read({ name: value });
                    }
                    else if (start.val() =="" && end.val() != "") {
                          
                        var newUrl = "/Filter/Car_ReadMax";
                        var grid = $("#Grid").data("kendoGrid");
                        grid.dataSource.transport.options.read.url = newUrl;
                        value = $("#end").val();
                            
                        grid.dataSource.read({ name: value });
                    }
                    else if (start.val() !="" && end.val() != "") {
                            
                        var newUrl = "/Filter/Car_ReadMinMax";
                        var grid = $("#Grid").data("kendoGrid");
                        grid.dataSource.transport.options.read.url = newUrl;
                        value = $("#start").val() + "," + $("#end").val();
                           
                        grid.dataSource.read({ name: value });
                    }
                    else if (start.val() == "" && end.val() == "") {
                           
                    }  
                }

            }

        }
        );

        $("#Reset").click(function () {
            //get value from dropdownlist --->>>> dropdownlist.value();

            $("#Order").val("Order");
            $("#start").val("");
            $("#end").val("");
            dropdownlist.value("Select");

            var grid = $("#Grid").data("kendoGrid");
            //dont delete this!~!!!

            var newUrl = "/Filter/Car_Read";
            grid.dataSource.transport.options.read.url = newUrl;
          
            grid.dataSource.read();
            grid.dataSource.sort({});

        });



    });

function startChange() {
    var endPicker = $("#end").data("kendoDatePicker"),
        startDate = this.value();

    if (startDate) {
        startDate = new Date(startDate);
        startDate.setDate(startDate.getDate() + 1);
        endPicker.min(startDate);
        endPicker.value(startDate);
    }
}

function endChange() {
    var startPicker = $("#start").data("kendoDatePicker"),
        endDate = this.value();

    if (endDate) {
        endDate = new Date(endDate);
        endDate.setDate(endDate.getDate() - 1);
        startPicker.max(endDate);

    }
}
