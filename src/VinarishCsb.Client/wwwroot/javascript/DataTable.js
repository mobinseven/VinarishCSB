var table = null;
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