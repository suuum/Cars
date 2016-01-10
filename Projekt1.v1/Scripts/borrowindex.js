
    var windowObject;
    windowObject = $("#Borrow").data("kendoWindow");

function returnItem(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    $.ajax({
        type: 'GET',
        data: { id: dataItem.BorrowId},
        url: '@Url.Action("Return","Borrow")',
        success: function (result) {

                
            var grid = $("#grid").data("kendoGrid");
            //dont delete this!~!!!
            grid.dataSource.read();

        }

    });

    

}

var checkedIds = new Array();
$(function () {
    $('#grid').on('click', '.chkbx', function () {
        var checked = $(this).is(':checked');
        var grid = $('#grid').data().kendoGrid;
        var dataItem = grid.dataItem($(this).closest('tr'));
        //   console.log(dataItem);
        // console.log(dataItem.CarId);
        checkedIds[dataItem.CarId] = checked;
            
        //console.log(checkedIds[dataItem.CarId]);
        if (checked) {
            //-select the row
            
            dataItem.addClass("k-state-selected");
        } else {
            //-remove selection
            dataItem.removeClass("k-state-selected");
        }
        dataItem.set(checked);
            
    })
})
$("#show").on("click", function () {
    var checked = [];
    for (var i in checkedIds) {
        if (checkedIds[i]) {
            checked.push(i);
        }
    }
    if (checked.length == 0) {
        alert("Musisz wybrać jakis samochod!")  
    }
    else {
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        //console.log(dataItem.BorrowId);

        $.ajax({
            type: 'GET',
            data: { choosencar:checked  },
            url: '@Url.Action("Return","Borrow")',


        });

        var grid = $("#grid").data("kendoGrid");
        //dont delete this!~!!!
        grid.dataSource.read();
   
    }
       
});

function checkAll(ele) {
    var state = $(ele).is(':checked');
        
    var grid = $('#grid').data().kendoGrid;
       
    if (state) {
        //-select the row
        row.addClass("k-state-selected");
    } else {
        //-remove selection
        row.removeClass("k-state-selected");
    }
        
    grid.refresh();
    grid.refresh();
}

function selectRow(event) {
      
    var checked = this.checked,
        row = $(this).closest("tr"),
        grid = $("#grid"),
        dataItem = grid.dataItem(row);
       
    checkedIds[dataItem.id] = checked;
    if (checked) {
        //-select the row
        row.addClass("k-state-selected");
    } else {
        //-remove selection
        row.removeClass("k-state-selected");
    }
}

function handleClick(cb) {
    alert("Clicked, new value = " + cb.checked);
}

       
        


    //on click of the checkbox:
		


function Grid_OnRowSelect(e) {
    var dataItem = e.dataItem;
    var productID = dataItem.ProductID;

    $(e.row).find("a.t-button")// find the link defined in the template column
        .click(function (e) { // subscribe to its click event
            //handle the click event e.g. navigate to a detail page passing the current ProductID

            location.href = "/Borrow/UserHaveCar/" + productID;
        });
}

function BorrowCar(e) {

    windowObject.refresh({
        url: "/Borrow/BorrowCarWindow"


    });



    windowObject.open();
    windowObject.center();

}

