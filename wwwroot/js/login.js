const url = '/login';

async function login() {
    alert("hello to login");
    const Name = document.getElementById('name');
    const password = document.getElementById('password');
    const nameValue = Name.value.trim();
    const passwordValue = password.value.trim();

    const user = {

        "id": 0,
        "userName": nameValue,
        "password": passwordValue,
        "isAdmin": true,
        "taskList": [{
            "id": 0,
            "name": "string",
            "isDone": true
        }]
    }


    await fetch('todo/Login', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        })
        .then(response => response.json())
        .then((res) => localStorage.setItem("token", res))

    .then(() => {
        password.value = '';
        Name.value = '';
    })

    .catch(error => console.error('Unable to enter to site please speak with the manager', error));
}