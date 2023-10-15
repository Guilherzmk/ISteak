window.onload = function(){
    const loginForm = document.getElementById("login_form");

    loginForm.addEventListener("submit", (e) => {
        e.preventDefault();

        let accessKey = document.getElementById("email").value;
        let password = document.getElementById("password").value;

        let user = {
            accessKey: accessKey,
            password: password
        }

        fetch("http://www.isteak.somee.com/v1/sign-in", {
            method: "POST",
            headers:{
                "Content-Type": "application/json"
            } ,
            body: JSON.stringify(user)
        })
        .then(
            async(response) => {
                var body = await response.json();
                console.log(body.token)
                if(response.status == 200){
                    localStorage.setItem("token", JSON.stringify(body.token));
                    console.log("deu certo");
                    window.location.replace("../dashboard/")
                }

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