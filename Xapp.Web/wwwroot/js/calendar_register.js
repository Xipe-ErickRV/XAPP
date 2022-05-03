var closeBut = document.getElementsByClassName('close')[0],
    modal = document.getElementsByClassName('modal-cont')[0],
    cancelBut = document.getElementsByClassName('cancel')[0],
    loginBut = document.getElementsByClassName('register')[0];

//close
function x() {
    modal.style.display = "none";
}
closeBut.onclick = x;
cancelBut.onclick = x;

loginBut.onclick = function () {
    modal.style.display = "block";
}

window.onclick = function (e) {
    if (e.target.className === 'modal-cont') {
        e.target.style.display = 'none';
    }
}