async function GetAllTeams() {
    var jwt = localStorage.getItem("jwt");
    document.body.classList.remove('loaded');
    const response = await fetch("https://localhost:44363/api/vm/all", {
        method: "GET",
        headers: { "Accept": "application/json",
    "Authorization": "Bearer "+jwt }
    });


    const res = await response.json();
    document.body.classList.add('loaded');

    //const form = document.forms["addForm"];
    res.forEach(item => {
        // добавляем полученные элементы в таблицу
        var form = document.getElementById('teamSelect');
        const element = document.createElement('option');
        element.text = item.name;
        form.appendChild(element);
    });
    
    console.log(res);
}

GetAllTeams();