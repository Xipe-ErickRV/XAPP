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
    loginBut = document.getElementsByClassName('skill-button')[0],
    skillList = document.getElementsById("skill_list"),

    //New skill info
    name = document.getElementById("field1"),
    save = document.getElementById("new_skill"),
    level = null;

//close

// Get the modal
var modal = document.getElementById('id01');

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

loginBut.onclick = function () {
    modal.style.display = "block";
}

window.onclick = function (e) {
    if (e.target.className === 'modal-cont') {
        e.target.style.display = 'inline-block';
    }
    if (e.target.className === 'basic skilloption') {
        level = 1;
    }
    else if (e.target.className === 'medium skilloption') {
        level = 2;
    }
    else if (e.target.className === 'advanced skilloption') {
        level = 3;
    }
    else if (e.target.className === 'expert skilloption') {
        level = 4;
    }

}