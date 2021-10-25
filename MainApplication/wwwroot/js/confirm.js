
async function postData() {
  
    var val2 = document.getElementById('inputCode').value;
    const data =
    {
      email:localStorage.getItem("email"),
    code:val2
  }
    console.log(val2);
    document.body.classList.remove('loaded');

    // Default options are marked with *
    const response = await fetch("https://localhost:44363/api/user/confirm", {
      method: 'POST', // *GET, POST, PUT, DELETE, etc.
      mode: 'cors', // no-cors, *cors, same-origin
      cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
      credentials: 'same-origin', // include, *same-origin, omit
      headers: {
        'Content-Type': 'application/json'
        // 'Content-Type': 'application/x-www-form-urlencoded',
      },
      redirect: 'follow', // manual, *follow, error
      referrerPolicy: 'no-referrer', // no-referrer, *client
      body: JSON.stringify(data) // body data type must match "Content-Type" header
    });

    const res = await response.json();

    document.body.classList.add('loaded');    if(res.isSuccessful!=true){
        alert("Неверный код")
    } 
    else{
        window.location.href ="https://localhost:44363/index.html";

    }
    if(localStorage.getItem("jwt") == null){
        window.location.href ="https://localhost:44363/register.html";

    }
  
    
  }
  