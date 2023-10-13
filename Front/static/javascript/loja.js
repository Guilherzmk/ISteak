
    const productForm = document.getElementById("produto-form");

    List();

    productForm.addEventListener("submit", (e) => {
        e.preventDefault();

        let image = document.getElementById("image").value;
        let productName = document.getElementById("product-name").value;
        let productNote = document.getElementById("product-note").value;
        let quantity = document.getElementById("quantity").value;
        let categorie = document.getElementById("categorie").value;
        let price = document.getElementById("price");

        let product = {
            name: productName,
            note: productNote,
            price: price,
            statusCode: 1,
            statusName: categorie,
            quantity: quantity,
            image: image
        }

        let token = JSON.parse(localStorage.getItem("token"))

        fetch("http://localhost:5255/v1/products", {
            method: "POST",
            headers: {
                "Authorization": "Bearer "+ token,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(product)
        })
        .then(
            (res) => {
                console.log(res)
                if(res.status == 401){
                    window.location.replace("../login/")
                }
            }
        )
        .catch(
            (err) => {
                console.log("erro");
                window.locaation.replace("../login/");
            }
        );

        var allInputs = document.querySelectorAll('input');
        allInputs.forEach(singleInput => singleInput.value = '');


    })

    function Create(){
        let image = document.getElementById("image").value;
        let productName = document.getElementById("product-name").value;
        let productNote = document.getElementById("product-note").value;
        let quantity = document.getElementById("quantity").value;
        let categorie = document.getElementById("categorie").value;
        let price = document.getElementById("price").value;

        let product = {
            name: productName,
            note: productNote,
            price: price,
            statusName: categorie,
            quantity: quantity
        }

        console.log(product)

        let token = JSON.parse(localStorage.getItem("token"))

        fetch("http://localhost:5255/v1/products", {
            method: "POST",
            headers: {
                "Authorization": "Bearer "+ token,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(product)
        })
        .then(
            (res) => {
                console.log(res.json())
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

        fetch("http://localhost:5255/v1/products", {
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

                console.log(item);
                
                id.innerText = item.id;
                code.innerText = item.code;
                name.innerText = item.name;
                note.innerText = item.note;
                price.innerText = item.price;
                type.innerText = item.statusName;

                id.classList.add("id");


            })
        })
    }