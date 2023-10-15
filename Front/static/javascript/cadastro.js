window.onload = function(){
    const registerForm = document.getElementById("sign-up");

    registerForm.addEventListener("submit", (e) => {
        e.preventDefault();

        let name = document.getElementById("name").value;
        let accessKey = document.getElementById("email").value;
        let phone = document.getElementById("number").value;
        let password = document.getElementById("password").value;   

        let user = {
            name: name,
            accessKey: accessKey,
            phone: phone,
            password: password,
            email: accessKey
        }

        fetch("http://www.isteak.somee.com/v1/sign-up", {
            method: "POST",
            headers:{
                "Content-Type": "application/json"
            },
            body: JSON.stringify(user)
        })
        .then(
            (res) => {
                
                    window.location.replace("../login/index.html")
            }
        )
        .catch(
            (err) => {
                console.log(err);
                console.log("erro")
            }
        )

    })


}