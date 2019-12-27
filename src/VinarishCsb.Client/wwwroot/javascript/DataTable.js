var table = null;
moment.loadPersian();
//console.log(moment('2016-03-12 13:00:00').fromNow())
window.ConvertAllTimeCells = function () {
    var cells = [];
    cells = document.getElementsByClassName('datetime');
    for (var i = 0; i < cells.length; i++) {
        var item = cells.item(i);
        item.innerHTML = moment(item.dataset.order, "X").format('dddd jD jMMMM  jYY | HH:mm');
    }
}
window.ConvertAllTimeCellsFromNow = function () {
    var cells = [];
    cells = document.getElementsByClassName('datetime');
    for (var i = 0; i < cells.length; i++) {
        var item = cells.item(i);
        item.innerHTML = moment(item.dataset.order, "X").fromNow();
    }
}
window.MakeDataTable = function () {
    table = $('.table').DataTable({
        paging: false,
        fixedHeader: true,
        responsive: true,
        language: {
            "decimal": "",
            "emptyTable": "بدون رده",
            "info": "_START_ تا _END_ از _TOTAL_ رده",
            "infoEmpty": " 0 تا 0 از 0 رده",
            "infoFiltered": "(پالایش شده از _MAX_ رده)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "نمایش _MENU_",
            "loadingRecords": "دریافت داده‌ها ...",
            "processing": "پردازش داده‌ها ...",
            "search": "جستجو:",
            "zeroRecords": "چیزی یافت نشد",
            "paginate": {
                "first": "نخستین",
                "last": "واپسین",
                "next": "پسین",
                "previous": "پیشین"
            }
        }
    });
}
window.DestroyDataTable = function () {
    table.destroy();
}