let startext = document.getElementById("starreview")

let clienttext = document.getElementById("clientregister")

const url = 'http://xd'

let somatorio = 0

let nomeproduct = []
let estoqueproduct = []

//Mostrar clientes registrados
fetch(url)
  .then(response => {
    if (!response.ok) {
      throw new Error('Erro na solicitação');
    }
    return response.json();
  })
  .then(data => {
    const tamanhoDoArray = Object.keys(data.data).length;
    clienttext.innerHTML = tamanhoDoArray + " Clientes";
    
  })
  .catch(error => {
    console.error('Error:', error);
  });


//Media de notas
fetch(url)
  .then(response => {
    if (!response.ok) {
        throw new Error('Erro na solicitação');
    }
    return response.json();
  })
  .then(data => {
    if(Array.isArray(data) && data.length > 0) {
        data.forEach(position => {
            somatoria = somatorio + position.star
        });
        startext.innerHTML = somatorio/data.length
    }
  })
  .catch(error => {
    console.error('Error:', error)
  })

//Definir valores no arrays para o gráfico
fetch(url)
  .then(response => {
    if (!response.ok) {
        throw new Error('Erro na solicitação');
    }
    return response.json();
  })
  .then(data => {
    if(Array.isArray(data) && data.length > 0) {
        data.forEach(element => {
            nomeproduct.push(element.name)
            estoqueproduct.push(element.quantity)
        });
    }
  })
  .catch(error => {
    console.error('Error:', error)
  })

//Estoque do gráfico de estatística
const ctx = document.getElementById('graph').getContext('2d');
const chart = new Chart(ctx, {
  type: 'bar',
  data: {
    labels: nomeproduct,
    datasets: [{
      label: 'Estoque',
      backgroundColor: 'rgba(240, 66, 66, 1)',
      data: estoqueproduct,
    }],
  },
  options: {
    indexAxis: 'y', // <-- here
    responsive: true
  }
});

