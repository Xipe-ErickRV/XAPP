//event popup
var closeBut_event = document.getElementsByClassName('close-event')[0],
    modal_event = document.getElementsByClassName('modal-cont-event')[0],
    cancelBut_event = document.getElementsByClassName('cancel-event')[0],
    loginBut_event = document.getElementsByClassName('create_event')[0];

function x() {
    modal_event.style.display = "none";
}
closeBut_event.onclick = x;
cancelBut_event.onclick = x;

loginBut_event.onclick = function () {
    modal_event.style.display = "block";
}

window.onclick = function (e) {
    if (e.target.className === 'modal-cont-event') {
        e.target.style.display = 'inline-block';
    }
}
