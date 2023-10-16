window.onload = function(){
    document.getElementById("create").addEventListener("click", Create);
    
    List();
}
    
function Create(event){
    let update = localStorage.getItem("product");

    if(update == null){
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
    else
    {
    event.preventDefault();
    let token = JSON.parse(localStorage.getItem("token"));
    let item = JSON.parse(localStorage.getItem("product"));

    let id = document.getElementById("id").value;
    let name = document.getElementById("product-name").value;
    let note = document.getElementById("product-note").value;
    let quantity = document.getElementById("quantity").value;
    let categorie = document.getElementById("categorie").value;
    let price = document.getElementById("price").value;

    let product = {
        id: item.id,
        name: name,
        note: note,
        quantity: quantity,
        categorie: categorie,
        price: price
    }

   fetch("http://localhost:5255/v1/products/"+item.id, {
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

    localStorage.removeItem("product");
    }



    
    
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
            let quantity = tr.insertCell();
            let price = tr.insertCell();
            let type = tr.insertCell();
            let actions = tr.insertCell();
            
            id.innerText = item.id;
            code.innerText = item.code;
            name.innerText = item.name;
            note.innerText = item.note;
            quantity.innerText = item.quantity+"KG";
            price.innerText = "R$"+item.price+"/KG";
            type.innerText = item.statusName;

            id.classList.add("id");
            id.id = "id";
            code.classList.add("code");

            let imgEdit = document.createElement("img");
            imgEdit.src = "../../static/images/pencil.svg";
            imgEdit.id = "edit";
            imgEdit.setAttribute("onclick", "update("+JSON.stringify(item)+")");

            let imgDelete = document.createElement("img");
            imgDelete.src = "../../static/images/trash.svg";
            imgDelete.setAttribute("onclick", "deleteProduct("+JSON.stringify(item.id)+")")

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

function update(item) {

    $('#exampleModal').modal('show');

    let id = document.getElementById("id");
    let name = document.getElementById("product-name")
    let note = document.getElementById("product-note");
    let quantity = document.getElementById("quantity");
    let categorie = document.getElementById("categorie");
    let price = document.getElementById("price");

    name.value = item.name;
    note.value = item.note;
    quantity.value = item.quantity;
    categorie.value = item.statusName;
    price.value = item.price;

    let product = {
        id: item.idd,
        name: name.value,
        note: note.value,
        quantity: quantity.value

    }

    console.log(product)

    localStorage.setItem("product", JSON.stringify(item));
  }

  function cancel(){
    localStorage.removeItem("type");
  }

  function updateProduct(){
    let product = JSON.parse(localStorage.getItem("product"));
    let token = JSON.parse(localStorage.getItem("token"));

    console.log(product.name)

    fetch("http://localhost:5255/v1/products/"+product.id, {
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

    localStorage.removeItem("product");
  }

  function deleteProduct(id){
    console.log(id);

    let token = JSON.parse(localStorage.getItem("token"));

    fetch("http://localhost:5255/v1/products/"+id, {
        method: "DELETE",
        headers: {
            "Authorization": "Bearer "+ token,
        }
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





   