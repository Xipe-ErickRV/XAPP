// Cambiar foto de perfil
const imgDiv = document.querySelector('.profile-pic');
const img = document.querySelector('#photo');
const file = document.querySelector('#file');
const uploadBtn = document.querySelector('#uploadBtn');

imgDiv.addEventListener('mouseenter', function () {
    uploadBtn.style.display = "block";
});



imgDiv.addEventListener('mouseleave', function () {
    uploadBtn.style.display = "none";
});


// Mostrar imagen al escogerla
file.addEventListener('change', function () {       // Refiere al archivo
    const choosedFile = this.files[0];

    if (choosedFile) {
        const reader = new FileReader();

        reader.addEventListener('load', function () {
            img.setAttribute('src', reader.result);
        })

        reader.readAsDataURL(choosedFile);
    }
});




// PopUp de Skills
var closeBut = document.getElementsByClassName('close')[0],
    modal = document.getElementsByClassName('modal-cont')[0],
    loginBut = document.getElementsByClassName('skill-button')[0];

//close
function x() {
    modal.style.display = "none";
}
closeBut.onclick = x;

loginBut.onclick = function () {
    modal.style.display = "block";
}

window.onclick = function (e) {
    if (e.target.className === 'modal-cont') {
        e.target.style.display = 'inline-block';
    }
}




// Agregar skills dependiendo del nivel
var BasicBtn = document.getElementsByClassName('basic')[0],
    MediumBtn = document.getElementsByClassName('medium')[0],
    AdvancedBtn = document.getElementsByClassName('advanced')[0];
    ExpertBtn = document.getElementsByClassName('expert')[0];
    Field1 = document.getElementsByClassName('field1')[0];
    Field2 = document.getElementsByClassName('field2')[0];

// Funcion para el boton de Basic
function AgregarBasic() {
    document.getElementById("field2").value = document.getElementById("field1").value;
    document.getElementById("field2").style.backgroundColor = "#36C895";
}

// Funcion para el boton de Medium
function AgregarMedium() {
    document.getElementById("field2").value = document.getElementById("field1").value;
    document.getElementById("field2").style.backgroundColor = "#DBAA50";
}

// Funcion para el boton de Advanced
function AgregarAdvanced() {
    document.getElementById("field2").value = document.getElementById("field1").value;
    document.getElementById("field2").style.backgroundColor = "#53C7E7";
}

// Funcion para el boton de Expert
function AgregarExpert() {
    document.getElementById("field2").value = document.getElementById("field1").value;
    document.getElementById("field2").style.backgroundColor = "#d478cb";
}


// Realizar funciones al presionar los botones correspondientes
BasicBtn.onclick = AgregarBasic;
MediumBtn.onclick = AgregarMedium;
AdvancedBtn.onclick = AgregarAdvanced;
ExpertBtn.onclick = AgregarExpert;