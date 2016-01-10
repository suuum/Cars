

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

$(document).ready(function () {
    var node = "";
    var count = 1;
    var ccc = 0;
    var getstartedcarlist = new Array();
    var selectedcarlist = new Array();
    $.ajaxPrefilter(function (options, originalOptions, jqXHR) {
        options.async = false;
    });

    $('#Submit').on('click', function () {
        var userID = $('#menuLevel1 option:selected').val();
       // console.log(selectedcarlist);
        if (selectedcarlist.length == 0 || selectedcarlist[0] == -1 || $('#start').val() == "" || $('#end').val() == "") { alert("Type correct value"); }
        else {
            var caridtab = "";
            for (var i = 0; i < selectedcarlist.length; i++) {

                if (i == 0) { caridtab += selectedcarlist[i]; }
                else {
                    caridtab += "," + selectedcarlist[i];
                }
            }

            $.ajax({
                type: 'GET',
                error: function () {
                    console.log("Error");
                    window.location = "/Borrow/Index";
                },
                data: { 'userid': userID, select: caridtab, fromDate: $('#start').val(), toDate: $('#end').val() },
                url: '/Borrow/BorrowCars',
                success: function () {
                   window.location = "/Borrow/Index";

                }
            });


        }

    });

    $('#add').on('click', function () {
        if ($('#menuLevel1 option:selected').text() != "---Select User ---") {


            var node = '';
            var index = $('.borrowcar li').size();
            var categoryIdLevel2 = $('#Car' + index + ' option:selected').val();
         //   console.log("value" + categoryIdLevel2);
            if (categoryIdLevel2 != -1) {
                var items = $('#Car' + index + ' > option').map(function () {
                    var opt =
                    opt = $(this).val();

                    return opt;
                }).get();
                var caridtab = "";
                for (var i = 1; i < items.length; i++) {

                    if (i == 1) { caridtab += items[i]; }
                    else {
                        caridtab += " " + items[i];
                    }
                }



                //console.log(caridtab);

                $.ajax({
                    type: 'GET',
                    error: function () { console.log("Error"); },
                    data: { 'id': categoryIdLevel2, select: caridtab },
                    url: '/Borrow/fillnextcar',
                    success: function (result) {

//console.log(result.length);
                        if (result.length != 1) {
                            var carcount = $('.borrowcar li').size() + 1;

                            var s = '<li>Car nr ' + carcount + ' <select id="Car' + carcount + '" class="CarsOption">';
                            s += '<option value="-1">Select Value</option>';
                            for (var i = 0; i < result.length; i++) {
                                if (result[i].Value != categoryIdLevel2) {
                                    s += '<option value="' + result[i].Value + '">' + result[i].Text + '</option>'
                                }
                            }
                            s += '</select><a class="Remove" href="#">Remove<a> </li>';
                            node = s;

                            $('.borrowcar li > a').remove();
                            $('#Car' + index).attr('disabled', 'disabled');
                           // console.log($('#Car' + index));
                            $('.borrowcar').append(node);

                        }
                        else {
                            alert("Nie ma juz samochodów do wyporzyczenia!");
                        }

                    }
                });

                //var c = count - 1;
                //console.log(node);
                //$('#inputs').append(node);

                //$('[id^="remove' + c + '"]').remove();
                //ccc = count - 1;
                //count++;
            }
            else {
                alert("Select correct value");
            }

        } else {
            alert("Select User!")
        }

    });

    jQuery(document).on('click', '.Remove', function () {

    //    console.log($('.borrowcar > li:last option').val());
        if ($('.borrowcar li').size() > 1) {
            if ($('.borrowcar li').size() > 2)
                $('.borrowcar li:last').prev().append("<a class='Remove' href='#'>Remove<a> </li>");

            $('.borrowcar li:last').remove();
            $('.borrowcar > li:last option').removeAttr('disabled');
        }
        var index = $('.borrowcar li').size();
        $('#Car' + index).removeAttr('disabled');
    });

    jQuery(document).on('change', '.CarsOption', function () {
        selectedcarlist = new Array();
        // console.log(getstartedcarlist[1].value);
        //  console.log($(this).val());
        //     console.log("Rozmiar selectu"+$('.borrowcar > li').children().length);
        // console.log($('.borrowcar li').size());
        for (var i = 1; i <= $('.borrowcar li').size() ; i++) {

            selectedcarlist.push($('#Car' + i + ' option:selected').val());
           // console.log("Wartość zaznaczona: " + $('#Car' + i + ' option:selected').val());


        }

        //for (var i = 1; i <= $('.borrowcar li').size() ; i++) {
        //    var length = $('#Car'+i+' li select').children('option').length;
        //    $('#Car'+i+' option').each(function(i){
        //        if ($(this).val() != -1) {
        //            for (var j = 0; j < selectedcarlist.length; j++) {
        //                if ($(this).val() == selectedcarlist[j]) {
        //                    if ($('#Car' + i + ' option:selected').val() == $(this).val()) {
        //                        $(this).remove();
        //                    }
        //                }
        //            }
        //        }
        //});
        //    }



       // console.log(selectedcarlist);
    });



    //fill second car list
    $('#menuLevel1').change(function () {
        if ($('#menuLevel1 option:selected').text() != "---Select User ---") {
            $('#Car1').removeAttr('disabled');
            getstartedcarlist = new Array();
        //    console.log("zmieniam wartosc");
          //  console.log($(".borrowcar>li").size());
            var categoryIdLevel1 = $('#menuLevel1 option:selected').val();
            var lenght = $(".borrowcar>li").size();
            for (var i = 2; i <= lenght ; i++) {
                $('.borrowcar li:last').remove();
            }
            $('#borrowcar').val();
            $.ajax({
                type: 'GET',
                data: { id: categoryIdLevel1 },
                url: '/Borrow/fillcatlevel2',
                success: function (result) {
                    var s = '';
                    s += '<option value="-1">Select Value</option>'
                    for (var i = 0; i < result.length; i++) {
                        s += '<option value="' + result[i].Value + '">' + result[i].Text + '</option>'
                        getstartedcarlist.push({ value: result[i].Value, text: result[i].Text });

                    }
                    $('#Car1').html(s);
                 //   console.log(getstartedcarlist);
                }


            });
        }
        else {
            $('#Car1').children("option").remove();
            var categoryIdLevel1 = $('#menuLevel1 option:selected').val();
            var lenght = $(".borrowcar>li").size();
            $('#Car1').removeAttr('disabled');
            for (var i = 2; i <= lenght ; i++) {
                $('.borrowcar li:last').remove();
            }


        }


    });

})