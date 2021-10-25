function exit() {
    localStorage.removeItem("jwt");
    localStorage.removeItem("email");
    window.location.reload();
}