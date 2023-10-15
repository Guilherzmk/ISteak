window.onload = function(){
    document.getElementById("create").addEventListener("click", Create);

    List();
}
    
function Create(event){
    event.preventDefault();

    let productName = document.getElementById("product-name").value;
    let productNote = document.getElementById("product-note").value;
    let quantity = document.getElementById("quantity").value;
    let categorie = document.getElementById("categorie").value;
    let price = document.getElementById("price").value;

    let product = {
        name: productName,
        note: productNote,
        price: price,
        statusCode: SetCode(categorie),
        statusName: categorie,
        quantity: quantity
    }

    let token = JSON.parse(localStorage.getItem("token"))

    fetch("http://www.isteak.somee.com/v1/products", {
        method: "POST",
        headers: {
            "Authorization": "Bearer "+ token,
            "Content-Type": "application/json"
        },
        body: JSON.stringify(product)
    })
    .then(
        (res) => {
            window.location.reload();
            if(res.status == 401){
                window.location.replace("../login/")
            }
        }
    )
    .catch(
        (err) => {
            console.log("erro");
            window.location.replace("../login/");
        }
    );


    var allInputs = document.querySelectorAll('input');
    allInputs.forEach(singleInput => singleInput.value = '');
    
}

function List(){

    let token = JSON.parse(localStorage.getItem("token"))

    fetch("http://www.isteak.somee.com/v1/products", {
        method: "GET",
        headers: {
            "Authorization": "Bearer " + token
        }
    })
    .then((res) => res.json())
    .then((data) => {
        const tbody = document.getElementById("tbody");

        data.map((item) => {
            let tr = tbody.insertRow();

            let id = tr.insertCell();
            let code = tr.insertCell();
            let name = tr.insertCell();
            let note = tr.insertCell();
            let price = tr.insertCell();
            let type = tr.insertCell();
            let actions = tr.insertCell();
            
            id.innerText = item.id;
            code.innerText = item.code;
            name.innerText = item.name;
            note.innerText = item.note;
            price.innerText = item.price;
            type.innerText = item.statusName;

            id.classList.add("id");
            code.classList.add("code");

            let imgEdit = document.createElement("img");
            imgEdit.src = "../../static/images/pencil.svg";

            let imgDelete = document.createElement("img");
            imgDelete.src = "../../static/images/trash.svg";

            actions.appendChild(imgEdit);
            actions.appendChild(imgDelete);
        })
    })
}

function SetCode(statusName){
    if(statusName == "bovinos" ){
        return 1;
    }else if(statusName == "pescados"){
        return 2;
    }else if(statusName == "suinos"){
        return 3;
    }else if(statusName == "churrasco"){
        return 4;
    }else if(statusName == "aves"){
        return 5;
    }else {
        return 0;
    }
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }


   