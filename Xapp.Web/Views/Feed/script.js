// function that displays create post modal
function show_popup() {
    document.getElementById('yeet').style.display = "flex";
}

// function that closes create post modal
function close_popup() {
    document.getElementById('yeet').style.display = "none";
}

// function that changes like icon color when clicked
function change_like_color(like_id) {
    if (like_id.style.color === "rgb(52, 52, 52)") {
        like_id.style.color = "rgb(226, 36, 50)";
    } else {
        like_id.style.color = "rgb(52, 52, 52)";
    }
}