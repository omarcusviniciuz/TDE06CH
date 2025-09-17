var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
List<Tarefa> tarefas = new List<Tarefa>();
int proximoID = 0;

// /tarefas - retorna todas as tarefas
app.MapGet("/tarefas", () =>
{
    return Results.Ok(tarefas);
});

// /tarefas/id - busca tarefa pelo id
app.MapGet("/tarefas/{Id:int}", (int Id) =>
{
    Tarefa? TarefaEncontrada = null;

    for (int i = 0; i < tarefas.Count; i++)
    {
        if (tarefas[i].Id == Id)
            TarefaEncontrada = tarefas[i];
    }

    if (TarefaEncontrada != null)
        return Results.Ok(TarefaEncontrada);
    else
        return Results.NotFound($"Tarefa com Id {Id} não encontrada");
});

// /tarefas - criar nova tarefa
app.MapPost("/tarefas", (Tarefa novaTarefa) => {
    if (string.IsNullOrWhiteSpace(novaTarefa.Titulo))
        return Results.BadRequest("O título é obrigatório");

    novaTarefa.Id = ++proximoID;
    novaTarefa.DatadeCriacao = DateTime.Now;
    novaTarefa.Concluida = false;

    tarefas.Add(novaTarefa);
    return Results.Created($"/tarefas/{novaTarefa.Id}", novaTarefa);
});

// /tarefas/id - excluir tarefa
app.MapDelete("/tarefas/{id:int}", (int id) => {
    var tarefaEncontrada = tarefas.FirstOrDefault(t => t.Id == id);
    if (tarefaEncontrada == null)
        return Results.NotFound($"Tarefa com Id {id} não encontrada");

    tarefas.Remove(tarefaEncontrada);
    return Results.NoContent();
});

// /tarefas/id - atualizar tarefa
app.MapPut("/tarefas/{Id:int}", (int Id, Tarefa tarefaAtualizada) =>
{
    var tarefaEncontrada = tarefas.FirstOrDefault(t => t.Id == Id);
    if (tarefaEncontrada == null)
        return Results.NotFound($"Tarefa com Id {Id} não encontrada");

    if (string.IsNullOrWhiteSpace(tarefaAtualizada.Titulo))
        return Results.BadRequest("O título é obrigatório");

    tarefaEncontrada.Titulo = tarefaAtualizada.Titulo;
    tarefaEncontrada.Descricao = tarefaAtualizada.Descricao;
    tarefaEncontrada.Concluida = tarefaAtualizada.Concluida;

    return Results.Ok(tarefaEncontrada);
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();

