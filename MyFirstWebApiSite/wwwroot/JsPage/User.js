

const Login = async () => {

    const userData = {
        Password: document.getElementById("Passwordl").value,
        UserName: document.getElementById("UserNamel").value
    }

    const responseUser = await fetch('api/User/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userData)
    });
    const dataUser = await responseUser;

    if (dataUser.ok) {
        sessionStorage.setItem("user", JSON.stringify(await dataUser.json()))
        window.location.href = "Products.html"
    }
    else {
        alert(" user un  ")
        window.location.href = "AddUser.html"
    }

}
const Register = async () => {
    const postData = {
        FirstName: document.getElementById("FirstNamea").value,
        LastName: document.getElementById("LastNamea").value,
        Password:document.getElementById("Password").value,
        Email: document.getElementById("UsetNamea").value
    }
    const responsePost = await fetch('api/User/register', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(postData)
    });
    console.log(responsePost.status)
    if (responsePost.status == 401) {
        alert("this password is not valid")
    }
    else {
        
        const dataPost = await responsePost.json();

    if (!dataPost) {
        window.location.href = "AddUser.html"
    }
    else {
        window.location.href = "Login.html"
    }}

}




const UpdateUser = async () => {
    const userData = {
    FirstName: document.getElementById("FirstNameu").value,
    LastName: document.getElementById("LastNameu").value,
    Password: document.getElementById("Password").value,
    Email: document.getElementById("UserNameu").value
    }
    const id = JSON.parse(sessionStorage.getItem("user")).userId
    const responseUser = await fetch(`api/User/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userData)
    });

    if (!responseUser.ok) {
        alert("this password is not valid")
    }
    else {   
            alert("User update!!!!!!!!!!!!")
            window.location.href = "Login.html"
        }
    }


const CheckPassword = async () => {
    Password= document.getElementById("Password").value
    console.log(Password)

        const responsePost = await fetch('api/User/CheckPassword', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(Password)
        });
        const dataPost = await responsePost.json();

        var color=''
        document.getElementById("progress").setAttribute("value", dataPost)
        if (dataPost <= 1)
            color = '#ff0000'
        else { 
        if (dataPost <=3)
                color = 'blue'
            else
              color='#4cff00'
        }
           
        document.getElementById("progress").style.setProperty("accent-color", color)
        document.getElementById("strentgh").innerHTML = "strength: " + dataPost;
        console.log(dataPost)
}