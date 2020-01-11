var table = null;
moment.loadPersian();
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
    $('.' + scrollerClassName).bind('scroll', function (e) { //scrollerClassName: Defined in ScrollPosition.js
        table.fixedHeader.adjust();
    });

    table = $('.table').DataTable({
        fixedHeader: true,
        paging: false,
        responsive: {
            details: {
                type: 'column',
                target: 'tr',
                renderer: function (api, rowIdx, columns) {
                    var data = $.map(columns, function (col, i) {
                        return col.hidden ?
                            '<tr data-dt-row="' + col.rowIndex + '" data-dt-column="' + col.columnIndex + '">' +
                            '<td>' + col.data + '</td>' +
                            '</tr>' :
                            '';
                    }).join('');

                    return data ?
                        $('<table/>')
                            .attr('style', 'width:100%')
                            .append(data) :
                        false;
                }
            }
        },
        language: {
            searchPlaceholder: String.fromCharCode(0xf002),
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
            "search": "",
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