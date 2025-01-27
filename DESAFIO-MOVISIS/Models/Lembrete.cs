namespace DESAFIO_MOVISIS.Models;

public class Lembrete
{
    private Guid _id;
    private string _titulo;
    private string _descricao;
    private DateTime _vencimento;
    private string _anexo;
      

    public Guid Id { get => _id; set => _id = value; }
    public string Titulo { get => _titulo; set => _titulo = value; }
    public string Descricao { get => _descricao; set => _descricao = value; }
    public DateTime Vencimento { get => _vencimento; set => _vencimento = value; }
    public string Anexo { get => _anexo; set => _anexo = value; }

    public Lembrete(Guid id, string titulo, string descricao, DateTime vencimento, string anexo)
    {
        _id = id;
        _titulo = titulo;
        _descricao = descricao;
        _vencimento = vencimento;
        _anexo = anexo;
    }

    
}
