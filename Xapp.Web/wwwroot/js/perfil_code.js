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