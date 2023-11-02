document.addEventListener("DOMContentLoaded", (event) => {

});

function registerUser() {
    let name = $("#txtName").val();
    let lastName = $("#txtLastName").val();
    let userName = $("txtUserName").val();
    let email = $("#txtEmail").val();
    let password = $("#txtPassword").val();


    let user = {
        Name: name,
        LastName: lastName,
        UserName: userName,
        Email: email,
        Password: password
    };

    $.ajax({
        url: '/User/Register',
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(user),
        success: function (data) {
            if (data.ok) {
                alert(data.message);
            } else {
                alert('Error: ' + data.message);
            }
        },
        error: function (data) {
            alert("Error al crear usuario");
        }
    });

}
//const registerForm = document.getElementById('registerForm');
//const loginForm = document.getElementById('loginForm');
//const toggleButton = document.getElementById('toggleButton');

//toggleButton.addEventListener('click', function () {
//    if (registerForm.style.display === 'block') {
//        // Ocultar formulario de registro y mostrar formulario de inicio de sesión
//        registerForm.style.display = 'none';
//        loginForm.style.display = 'block';
//        toggleButton.textContent = 'Cambiar a Registro';
//    } else {
//        // Ocultar formulario de inicio de sesión y mostrar formulario de registro
//        registerForm.style.display = 'block';
//        loginForm.style.display = 'none';
//        toggleButton.textContent = 'Cambiar a Inicio de Sesión';
//    }
//});