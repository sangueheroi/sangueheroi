function ModalLogin() {
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


function ModalParceriaHemocentro() {
    $.ajax({
        url: './Hemocentro/ParceriaHemocentro',
        datatype: 'json',
        contentType: "application/json",
        type: "GET",
        success: function (data) {
            $('.modalCC').html(data);
            $('#modalCriarConta').modal('show');
        }
    });
}


$('form#frmFaleConosco').submit(function (e) {
   
    var nome = $('#txtSeuNome').val();
    var email = $('#txtSeuEmail').val();
    var mensagem = $('#txtSuaMensagem').val();

    $.ajax({
        type: 'GET',
        url: './Administrador/FaleConosco',
        data: { txtSeuNome: nome, txtSeuEmail: email, txtSuaMensagem: mensagem },
        dataType: 'html',
        success: function () {
            $.notify({
                message: 'Obrigado Pelo Seu Contado! Nossos administradores irão entrar em contato com você em breve. Volte Sempre!',
                status: "info",
                timeout: 4000,
                onClose: function () {
                    window.location.href = 'Login';
                }
            });
        }
    });
    if (e.preventDefault) {
        e.preventDefault();
    } else {
        e.returnValue = false;
    }
});


//$('#btnFaloConosco').on('click', function () {
    
    //var nome = $('#txtSeuNome').val();
    //var email = $('#txtSeuEmail').val();
    //var mensagem = $('#txtSuaMensagem').val();

//    if (nome == '' || nome == 'Seu nome') {
//        $('#txtSeuNome').css('border-color', '#cd0000').focus();
//        return;
//    }

//    if (email == '' || email == 'Seu e-mail') {
//        $('#txtSeuEmail').css('border-color', '#cd0000').focus();
//        return;
//    }

//    if (mensagem == '' || mensagem == 'Sua mensagem') {
//        $('#txtSuaMensagem').css('border-color', '#cd0000').focus();
//        return;
//    }

//    $.ajax({
//        type: 'GET',
//        url: './Administrador/FaleConosco',
//        data: { txtSeuNome: nome, txtSeuEmail: email, txtSuaMensagem: mensagem },
//        dataType: 'html',
//        success: function () {
//            $.notify({
//                message: 'Obrigado Pelo Seu Contado! Nossos administradores irão entrar em contato com você em breve. Volte Sempre!',
//                status: "info",
//                timeout: 4000,
//                onClose: function () {
//                    window.location.href = 'Login';
//                }
//            });
//        }
//    });

//});