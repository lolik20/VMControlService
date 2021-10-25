async function CreateNew() {
    var val2 = document.getElementById('tariff').value;
    document.body.classList.remove('loaded');

    var jwt = localStorage.getItem("jwt");
    const data =
    {
        tariff: val2
    }
    // Default options are marked with *
    const response = await fetch("https://localhost:44363/api/vm/Create", {
        method: 'POST', // *GET, POST, PUT, DELETE, etc.
        mode: 'cors', // no-cors, *cors, same-origin
        cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
        credentials: 'same-origin', // include, *same-origin, omit
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + jwt

        },
        redirect: 'follow', // manual, *follow, error
        referrerPolicy: 'no-referrer', // no-referrer, *client
        body: JSON.stringify(data) // body data type must match "Content-Type" header
    });
    window.location.reload();

    const res = await response.json();



}