﻿function ModalLogin() {
    $.ajax({
        url: './Login/Login',
        datatype: 'json',
        contentType: "application/json",
        type: "GET",
        success: function (data) {
            $('.modal-content').html(data);
            $('#modalLogin').modal('show');
        }
    });
}

function ModalEsqueciMinhaSenha() {
    $.ajax({
        url: './Login/EsqueciMinhaSenha',
        datatype: 'json',
        contentType: "application/json",
        type: "GET",
        success: function (data) {
            $('.modalEMS').html(data);
            $('#modalEsqueciMinhaSenha').modal('show');
        }
    });
}


function ModalRegistrar()
{
    $.ajax({
        url: './Login/Registrar',
        datatype: 'json',
        contentType: "application/json",
        type: "GET",
        success: function (data) {
            $('.modalCC').html(data);
            $('#modalCriarConta').modal('show');
        }
    });
}