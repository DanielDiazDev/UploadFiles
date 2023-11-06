document.addEventListener("DOMContentLoaded", (event) => {
    document.getElementById("btnRegisterUser").addEventListener("click", registerUser);
    document.getElementById("btnLoginUser").addEventListener("click", loginUser);
    document.getElementById("btnToggle").addEventListener("click", toggle);

});

function registerUser() {
    let name = $("#txtName").val();
    let lastName = $("#txtLastName").val();
    let userName = $("#txtUserName").val();
    let email = $("#txtEmail").val();
    let password = $("#txtPassword").val();


    let user = {
        Name: name,
        LastName: lastName,
        UserName: userName,
        Email: email,
        PasswordHash: password
    };

    $.ajax({
        url: '/Account/Register',
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(user),
        success: function (result) {
            if (result.ok) {
                alert(result.message);
                window.location = '/File/Index'
            } 
        },
        error: function (result) {
            alert('Error: ' + result.message);
        }
    });

}


function loginUser() {
    let userName = $("#txtUserNameLogin").val();
    let password = $("#txtPasswordLogin").val();


    let user = {
        UserName: userName,
        PasswordHash: password
    };

    $.ajax({
        url: '/Account/Login',
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(user),
        success: function (data) {
            if (data.ok) {
                alert(data.message);
                window.location = '/File/Index'
            }
        },
        error: function (data) {
            alert('Error: ' + data.message);
        }
    });

}

function toggle() {
    const registerForm = document.getElementById('registerForm');
    const loginForm = document.getElementById('loginForm');
    const toggleButton = document.getElementById('btnToggle');
    const toggleText = document.getElementById('toggleText');
    if (registerForm.style.display === 'block') {
        registerForm.style.display = 'none'
        loginForm.style.display = 'block';
        toggleButton.value = 'Cambiar a Registro';
        toggleText.innerHTML = 'Inicio de sesion'
    } else {
        registerForm.style.display = 'block';
        loginForm.style.display = 'none';
        toggleButton.value = 'Cambiar a Inicio de Sesión';
        toggleText.innerHTML = 'Registro'
    }
};
