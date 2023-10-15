window.onload = function() {
    const name = document.getElementById("name");
    const nickname = document.getElementById("nickname");
    const identity = document.getElementById("identity");

    const zipCode = document.getElementById("zipCode");
    const street = document.getElementById("street");
    const number = document.getElementById("number");
    const city = document.getElementById("city");
    const neighborhood = document.getElementById("neighborhood");
    const state = document.getElementById("state");
    const complement = document.getElementById("complement")

    document.getElementById("salvar").addEventListener("click", update);

    
get();

function get(){
    fetch("http://localhost:5255/v1/stores", {
        method: "GET"  
    })
    .then((res) => {
        if (!res.ok) {
            throw new Error('Erro na requisição');
        }
        return res.json();
    })
    .then((data) => {
        console.log(data);
        name.value = data.name;
        nickname.value = data.nickname;
        identity.value = data.identity;
      
        
    })
    .catch((error) => {
        console.error(error);
    });

    fetch("http://localhost:5255/v1/addresses", {
        method: "GET"
    })
    .then((res) => {
        if(!res.ok) {
            throw new Error('Erro na requisição');
        }
        return res.json();
    })
    .then((data) => {
        console.log(data);
        zipCode.value = data.zipCode;
        street.value = data.street;
        number.value = data.number;
        city.value = data.city;
        neighborhood.value = data.neighborhood;
        state.value = data.state;
        complement.value = data.complement;
    })
    .catch((error) => {
        console.error(error);
    });

}

function update(){
    let name = document.getElementById("name").value;
    let nickname = document.getElementById("nickname").value;
    let identity = document.getElementById("identity").value;

    let zipCode = document.getElementById("zipCode").value;
    let street = document.getElementById("street").value;
    let number = document.getElementById("number").value;
    let city = document.getElementById("city").value;
    let neighborhood = document.getElementById("neighborhood").value;
    let state = document.getElementById("state").value;
    let complement = document.getElementById("complement").value;

    let store = {
        name: name,
        nickname: nickname,
        identity: identity
    }

    let address = {
        zipCode: zipCode,
        street: street,
        number: number,
        city: city,
        neighborhood: neighborhood,
        state: state,
        complement: complement
    }


    fetch("http://localhost:5255/v1/stores", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(store)
    })
    .then(
        (res) => {
            window.location.reload();
            if(res.status == 401){
                window.location.replace("../login/")
            }
        }
    )

    fetch("http://localhost:5255/v1/addresses", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(address)
    })
    .then(
        (res) => {
            window.location.reload();
            if(res.status == 401){
                window.location.replace("../login/")
            }
        }
    )
}



}






