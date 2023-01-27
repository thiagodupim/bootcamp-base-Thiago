
using Tarefas.DTO;

public interface ITarefaDAO
{
	void Atualizar(TarefaDTO tarefa);
	List<TarefaDTO> Consultar();
	TarefaDTO Consultar(int id);
	void Criar(TarefaDTO tarefa);
	void Excluir(int id);
}
