async function GetUserVms() {
    var jwt = localStorage.getItem("jwt");
    const response = await fetch("https://localhost:44363/api/vm/uservm", {
        method: "GET",
        headers: { "Accept": "application/json",
    "Authorization": "Bearer "+jwt }
    });

    const res = await response.json();
    
    //const form = document.forms["addForm"];
    res.forEach(item => {
        console.log
        // добавляем полученные элементы в таблицу
        var table = document.getElementById('table');
    table.append(getTr(item));
    });
    res.forEach(item => {
        // добавляем полученные элементы в таблицу
        var form = document.getElementById('teamEdit');
        const element = document.createElement('option');
        element.text = item.name;
        form.appendChild(element);
    });

    console.log(res);

}
GetUserVms();
﻿function getTr(item) {

    const tr = document.createElement("tr");
    tr.setAttribute("data-rowid", item.id);

    const id = document.createElement("td");
    id.append(item.id);
    tr.append(id);

    const name = document.createElement("td");
    name.append(item.name);
    tr.append(name);

     const status = document.createElement("td");
     status.setAttribute("id", item.name+"33");
     if (item.status == "Off") {
         status.setAttribute("style", "color:red;border-radius:3px;");
     }
     if (item.status == "Running") {
         status.setAttribute("style", "color:green;border-radius:3px;");
     }
     status.append(item.status);
    tr.append(status);
    
    const tariff = document.createElement("td");
    tariff.append(item.tariff);
    tr.append(tariff);

    const ram = document.createElement("td");
    if(item.tariff=="Start"){
        ram.append("1GB");
    }
      if(item.tariff=="Business"){
        ram.append("4GB");
    }
    if(item.tariff=="Professional"){
        ram.append("8GB");
    }
    tr.append(ram);

    const hdd = document.createElement("td");
    if(item.tariff=="Start"){
        hdd.append("8GB");
    }
      if(item.tariff=="Business"){
        hdd.append("16GB");
    }
    if(item.tariff=="Professional"){
        hdd.append("32GB");
    }
     tr.append(hdd);

     const threads = document.createElement("td");
     if (item.tariff == "Start") {
         threads.append("1");
     }
     if (item.tariff == "Business") {
         threads.append("2");
     }
     if (item.tariff == "Professional") {
         threads.append("4");
     }
     tr.append(threads);

    const button = document.createElement("button");
    button.setAttribute("id", item.name);
    if(item.status=="Off"){
        button.setAttribute("onclick", "on(this);");
        button.append("Вкл");
    }
    if(item.status=="Running"){
        button.setAttribute("onclick", "off(this);");
        button.append("Выкл");
    }
    
    button.setAttribute("class", "btn");
   
    tr.append(button);

    return tr;

}
async function on(obj) {
    
    var jwt = localStorage.getItem("jwt");
    const data =
    {
      vmName:obj.id
    }
    document.body.classList.remove('loaded');

  const response = await fetch("https://localhost:44363/api/vm/turnon", {
    method: 'POST', // *GET, POST, PUT, DELETE, etc.
    mode: 'cors', // no-cors, *cors, same-origin
    cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
    credentials: 'same-origin', // include, *same-origin, omit
    headers: {
      'Content-Type': 'application/json',
      'Authorization':"Bearer " + jwt 

    },
    redirect: 'follow', // manual, *follow, error
    referrerPolicy: 'no-referrer', // no-referrer, *client
    body: JSON.stringify(data) // body data type must match "Content-Type" header
  });
    document.body.classList.add('loaded');

    window.location.reload();
}
async function off(obj) {
    var jwt = localStorage.getItem("jwt");
    const data =
    {
      vmName:obj.id
    }
    document.body.classList.remove('loaded');

  const response = await fetch("https://localhost:44363/api/vm/turnoff", {
    method: 'POST', // *GET, POST, PUT, DELETE, etc.
    mode: 'cors', // no-cors, *cors, same-origin
    cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
    credentials: 'same-origin', // include, *same-origin, omit
    headers: {
      'Content-Type': 'application/json',
      'Authorization': "Bearer " + jwt 

    },
    redirect: 'follow', // manual, *follow, error
    referrerPolicy: 'no-referrer', // no-referrer, *client
    body: JSON.stringify(data) // body data type must match "Content-Type" header
  });

    window.location.reload();
}