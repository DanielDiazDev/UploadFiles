document.addEventListener("DOMContentLoaded", (event) => {
    
    document.getElementById("btnUpload").addEventListener("click", uploadFile);
});
function uploadFile() {
    // Mueve la obtención del nombre del archivo aquí
    let selectedFileName = $("#file").val();

    // Crea un objeto de datos que contiene el nombre del archivo
    let dataToSend = {
        FileName: selectedFileName
    };

    $.ajax({
        url: '/File/UploadFile',
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dataToSend), // Envía el objeto JSON
        success: function (data) {
            if (data.ok) {
                alert(data.message);
                window.location = '/File/Index';
            }
        },
        error: function (data) {
            alert('Error: ' + data.message);
        }
    });
}


