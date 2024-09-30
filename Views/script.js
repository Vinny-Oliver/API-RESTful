document.getElementById("tarefaForm").addEventListener("submit", async function(event) {
    event.preventDefault(); // Impede o envio do formulário padrão

    const titulo = document.getElementById("titulo").value;
    const descricao = document.getElementById("descricao").value;
    const data = new Date(document.getElementById("data").value).toISOString();
    const status = parseInt(document.getElementById("status").value);

    const response = await fetch("http://localhost:5068/Tarefa", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            Titulo: titulo,
            Descricao: descricao,
            Data: data,
            Status: status
        })
    });

    if (response.ok) {
        const result = await response.json();
        alert(`Tarefa adicionada com sucesso! ID: ${result.id}`);
    } else {
        const error = await response.json();
        alert(`Erro: ${error.Erro}`);
    }
});
