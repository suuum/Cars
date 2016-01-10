$(document).ready(function () {
var windowObject;
var windowObject2;
var windowObject3;

    windowObject = $("#Edit").data("kendoWindow");
    windowObject2 = $("#Create").data("kendoWindow");
    windowObject3 = $("#Details").data("kendoWindow");
 
    console.log("wykonalem kod");




     

function editItem(e) {
    e.preventDefault();
            
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    console.log(dataItem.CarId);
    windowObject.refresh({
        url: "/Cars/Edit/" + dataItem.CarId
                

    });
            

    windowObject.open();
    windowObject.center();
           
           
}

function CreateItem(e) {
            
    windowObject2.refresh({
        url: "/Cars/Create"

    });
    windowObject2.open();
    windowObject2.center();
}
function DetailsItem(e) {

    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));

    windowObject3.refresh({
        url: "/Cars/Details/" + dataItem.CarId

    });
    windowObject3.open();
    windowObject3.center();
}
});