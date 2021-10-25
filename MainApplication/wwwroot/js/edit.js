async function edit() {
    var val1 = document.getElementById('teamEdit').value;
    var val2 = document.getElementById('tariffEdit').value;
    try {
        var status = document.getElementById(val1 + "33").innerText;
    }
    catch (exp) {
        console.log(exp);
    }

    if (status == "Running") {
        alert("VM Runned");
        throw new Error("VM Runned");
    } else {

    }
    var jwt = localStorage.getItem("jwt");
    const data =
    {
        vmName: val1,
        tariff: val2
    }
    console.log(val1);
    // Default options are marked with *
    const response = await fetch("https://localhost:44363/api/vm/addvm", {
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


}