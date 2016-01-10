function onChange() {
        var selected = $.map(this.select(), function (item) {

            return $(item).text();

        });
        var spl = selected.toString();
        //  alert(spl);
        spl = spl.split('');

        window.location.replace("/Borrow/UserCars/"+spl[0]);

    }
