$(document).ready(function () {
    var checkedIds = new Array();

    $('#Grid').on('click', '.chkbx', function () {
        var checked = $(this).is(':checked');
        var grid = $('#Grid').data().kendoGrid;
        var dataItem = grid.dataItem($(this).closest('tr'));
        //   console.log(dataItem);
        // console.log(dataItem.CarId);
        checkedIds[dataItem.BorrowId] = checked;

        //console.log(checkedIds[dataItem.CarId]);

        dataItem.set(checked);

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

            var stringtab = checked.toString();

            $.ajax({
                type: 'GET',
                data: { choosencar: stringtab },
                url: '@Url.Action("ReturnCars","Borrow")',
                success: function (result) {

                    //  console.log("jestem tu sukces");
                    //$("#check").append("zuuuuuuuuuuupa");
                    var grid = $("#Grid").data("kendoGrid");
                    //dont delete this!~!!!
                    grid.dataSource.read();

                }

            });

        }
    });

});