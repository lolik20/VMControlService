
async function postData() {
    var val1 = document.getElementById('inputEmail').value;
    var val2 = document.getElementById('inputPassword').value;
    const data =
    {
      email:val1,
    password:val2
    }
    document.body.classList.remove('loaded');
    // Default options are marked with *
    const response = await fetch("https://localhost:44363/api/user/register", {
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

    document.body.classList.add('loaded');

  if(res.token==null){
      alert("такой юзер уже есть");
  }
  else{
    localStorage.setItem("email",val1);
    localStorage.setItem("jwt",res.token);
  window.location.href ="https://localhost:44363/confirm.html";
  }
 

  }
  