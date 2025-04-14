'use strict';

$(function () {
    $("#datepicker").datepicker({
        autoclose: true,
        todayHighlight: true,
        language: 'es'
    }).datepicker('update', new Date());
});

$(function () {
    $("#datepickerUltimaLicitacion").datepicker({
        autoclose: true,
        todayHighlight: true,
        language: 'es'
    }).datepicker('update');
});

function isDelete() {
    return confirm("\u00BFDesea eliminar esta fila?");
}