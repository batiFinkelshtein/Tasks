const uri = '/todoTask';
const token = localStorage.getItem("token")
const auth = `Bearer ${token} `

let myuser = document.getElementById('user');

const drawUser = (user) => {
    const div = document.createElement('div');
    const h2 = document.createElement('h2');
    h2.innerHTML = `hello to ${user.userName} 😀😊😉`
    h2.style.color = 'black'
    div.appendChild(h2);
    myuser.appendChild(div);

}
fetch('User/GetUser', {
        method: 'Get',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
    })
    .then(response => response.json())
    .then((data) => {

        drawUser(data);

    })
    .catch((error) => {

        alert("please login again👍")
    });
const deleteTask = async(task) => {
    await fetch(`todo/DeleteTask/${task.id}`, {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },

        })
        .then(response => response.json())
        .then((res) => { if (res == true) { alert(`the delete is success 👌👌👌 `) } })
        .catch(error => console.error('Unable to enter to site please speak with the manager', error));
}
const taskPut = async(task) => {
    await fetch(`todo/UpdateTask/${task.id}`, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(task)
        })
        .then(response => response.json())
        .then((res) => { if (res == true) { alert(`the edit is success 👌👌👌 `) } })
        .catch(error => console.error('Unable to enter to site please speak with the manager', error));
};
const SendNewTask = (Taskname, isdone) => {
    const task = {
        "id": 0,
        "name": Taskname,
        "isDone": isdone
    }
    fetch('todo/AddTask', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(task)
        })
        .then(response => response.json())
        .then((res) => { if (res > 0) { task.id = res, drawTask(task); } })
        .then((res) => { if (res > 0) { alert(`the add is success 👌👌👌 `) } })

    .catch(error => console.error('Unable to enter to site please speak with the manager', error));

}
const tasks = document.getElementById('tasks');
const drawTable = () => {
    const table = document.createElement('table');
    table.id = 'tasksList';
    const trNew = document.createElement('tr');
    const thNew = document.createElement('th');
    const button = document.createElement('button');
    button.innerHTML = 'to add new task🪶';
    thNew.appendChild(button);
    trNew.appendChild(thNew);
    table.appendChild(trNew)
    button.onclick = () => {
        const name = document.createElement('th')
        const nameInput = document.createElement('input')
        nameInput.type = Text;
        const labelth = document.createElement('th')
        nameInput.placeholder = 'name of the task'
        const label = document.createElement('label');
        label.innerHTML = 'is done?'
        labelth.appendChild(label)
        const isDone = document.createElement('th')
        const isDoneInput = document.createElement('input')
        isDoneInput.type = 'checkbox';
        const save = document.createElement('th');
        const buttonSave = document.createElement('button');
        buttonSave.innerHTML = 'save'
        buttonSave.onclick = () => {
            SendNewTask(nameInput.value, isDoneInput.checked);
        }
        save.appendChild(buttonSave);

        name.appendChild(nameInput);
        isDone.appendChild(isDoneInput);
        trNew.appendChild(name);
        trNew.appendChild(labelth)
        trNew.appendChild(isDone);
        trNew.appendChild(save)




    }


    tr = document.createElement('tr');
    const thEdit = document.createElement('th');
    thEdit.innerHTML = 'Edit'
    const thId = document.createElement('th');
    thId.innerHTML = 'Id'
    const thName = document.createElement('th');
    thName.innerHTML = 'Name'
    const thIsDone = document.createElement('th');
    thIsDone.innerHTML = 'IsDone'
    const thDelete = document.createElement('th');
    thDelete.innerHTML = 'Delete'
    const tbody = document.createElement('tbody');
    tbody.id = 'tasksTable';
    tr.appendChild(thEdit);
    tr.appendChild(thId);
    tr.appendChild(thName);
    tr.appendChild(thIsDone);
    tr.appendChild(thDelete);
    table.appendChild(tr);
    table.appendChild(tbody);
    tasks.appendChild(table)
}
const drawTask = (task) => {

    const tasksTable = document.getElementById('tasksTable');
    const tr = document.createElement('tr')

    const edit = document.createElement('th')
    const editButton = document.createElement('button')
    editButton.innerHTML = 'edit'

    const id = document.createElement('th')
    id.innerHTML = task.id;

    const name = document.createElement('th')
    const nameInput = document.createElement('input')
    nameInput.type = Text;
    nameInput.value = task.name

    const isDone = document.createElement('th')
    const isDoneInput = document.createElement('input')
    isDoneInput.type = 'checkbox';

    isDoneInput.checked = task.isDone;

    const deletTask = document.createElement('th')
    const deleteButton = document.createElement('button')
    deleteButton.innerHTML = 'delete'

    nameInput.disabled = true;
    isDoneInput.disabled = true;

    editButton.onclick = () => {
        if (editButton.innerHTML == 'edit') {
            editButton.innerHTML = 'ok'
            nameInput.disabled = false;
            isDoneInput.disabled = false;

        } else {
            editButton.innerHTML = 'edit'
            nameInput.disabled = true;
            isDoneInput.disabled = true;
            task.id = task.id;
            task.name = nameInput.value;
            task.isDone = isDoneInput.checked;
            taskPut(task);
        }
    }

    deleteButton.onclick = () => {
        deleteTask(task);
        tasksTable.removeChild(tr)
    }

    edit.appendChild(editButton)
    name.appendChild(nameInput)
    isDone.appendChild(isDoneInput)
    deletTask.appendChild(deleteButton)

    tr.appendChild(edit)
    tr.appendChild(id)
    tr.appendChild(name)
    tr.appendChild(isDone)
    tr.appendChild(deletTask)

    tasksTable.appendChild(tr)
}
const myTasks = document.getElementById('myTasks')
const getMyTasks = () => {
    myTasks.style = 'display:none';
    fetch('todo/GetMyTasks', {
            method: 'Get',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
        })
        .then(response => response.json())

    .then((data) => {
            drawTable(),
                data.forEach(x => {
                    drawTask(x);
                });

        })
        .catch((error) => {
            console.error('Unable to get Tasks.', error);
            alert("please login again")
            location.href = 'index.html'
        });
}