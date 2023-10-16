let startext = document.getElementById("starreview")

let clienttext = document.getElementById("clientregister")

fetch('https://api.exemplo.com/dados')
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
