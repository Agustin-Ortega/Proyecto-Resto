
$(document).ready(function () {
    cargarTarjeta();
});

$("#ItemPlatoId").change(function () {
    cargarTarjeta();
});

function cargarTarjeta() {
    let id = $("#ItemPlatoId").val();
    $.ajax({
        url: 'ObtenerFoto',
        contentype: 'application/json; charset=utf-8',
        type: 'GET',
        data: { id },
        success: function (result) {
            $("#FotoTarjeta").attr("src", result.Imagen);
            $("#FotoTarjeta").attr("alt", result.nombre);
            $("#TituloTarjeta").html(result.nombre);
            $("#TextoTarjeta").html(result.descripcion);
            $("#PrecioTarjeta").html("$" + result.precio);
        }
    })
}

