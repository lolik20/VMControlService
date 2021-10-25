function getEmail() {
    var email = document.getElementById("user-mail");
    var val = localStorage.getItem("email");
    email.textContent = val;
}