document.addEventListener("DOMContentLoaded", (event) => {
    document.getElementById("btnUpload").addEventListener("click", uploadFile);
    document.getElementById("backToForm").addEventListener("click", backToForm);
    showFiles();
});


function uploadFile() {
    let selectFile = ($("#fileInput"))[0].files[0];
    let dataString = new FormData();

    dataString.append("FormFile", selectFile);

    $.ajax({
        url: '/File/UploadFile', 
        type: 'POST',
        data: dataString,
        processData: false, 
        contentType: false,  
        success: function (result) {
            alert(result.message);
        },
        error: function (result) {
            alert('Error: ' + result.message);
        }
    });
}

function showFiles() {
    $.ajax({
        url: '/File/GetFiles',
        type: "GET",
        dataType: "json",
        success: function (result) {
            let html = '';
            for (let file of result) {
                let row = `<tr>
                               <td>${file.fileName}</td>
                               <td><button class="delete-file" data-id="${file.id}">Eliminar</button></td>
                               <td><button class="update-file" data-id="${file.id}">Actualizar</button></td>
                           </tr>`;
                html += row;
            }
            document.querySelector('#tableFiles > tbody').innerHTML = html;
            document.querySelectorAll('.delete-file').forEach((button) => {
                button.addEventListener('click', function () {
                    const fileId = this.getAttribute('data-id');
                    deleteFile(fileId);
                });
            });
            
            document.querySelectorAll('.update-file').forEach((button) => {
                button.addEventListener('click', function () {
                    const fileId = this.getAttribute('data-id');
                    showUpdateForm(fileId); 
                });
            });
        },
        error: function () {
            alert("Error al buscar los usuarios");
        }
    });
}

function deleteFile(fileId) {
    $.ajax({
        url: '/File/DeleteFile',
        type: "DELETE",
        dataType: "json",
        data: JSON.stringify({ Id: fileId }), 
        contentType: "application/json",
        success: function (result) {
            if (result.ok) {
              
                showFiles();
                alert(result.message);
            } else {
                alert("Error al eliminar el archivo: " + result.message);
            }
        },
        error: function () {
            alert("Error al eliminar el archivo");
        }
    });
}
function showUpdateForm(fileId) {
    const currentFileName = ""; 

    
    document.getElementById('newName').value = currentFileName;

   
    document.getElementById('modifyFileForm').style.display = 'block';
    document.getElementById('btnSave').addEventListener('click', function () {
        const newFileName = document.getElementById('newName').value;
        updateFileName(fileId, newFileName);
        document.getElementById('modifyFileForm').style.display = 'none'; 
    });
}
function updateFileName(fileId, newFileName) {
    const data = { Id: fileId, FileName: newFileName };

    $.ajax({
        url: '/File/ModifiedFile', 
        type: 'PUT', 
        data: JSON.stringify(data),
        contentType: "application/json",
        success: function (result) {
            alert(result.message);
            showFiles(); 
        },
        error: function (result) {
            alert('Error: ' + result.message);
        }
    });
}
function backToForm(){
    const modifyFileForm = document.getElementById('modifyFileForm');
    if (modifyFileForm.style.display === 'block') {
        modifyFileForm.style.display = 'none';
    }
}
