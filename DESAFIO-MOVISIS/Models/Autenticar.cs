namespace DESAFIO_MOVISIS.Models;

public class Autenticar
{
    private Guid _id;
    private string _email;
    private string _senha;
    private string _confirmarSenha;

    public Guid Id { get => _id; set => _id = value; }
    public string Email { get => _email; set => _email = value; }
    public string Senha { get => _senha; set => _senha = value; }
    public string ConfirmarSenha { get => _confirmarSenha; set => _confirmarSenha = value; }

    public Autenticar()
    {
        
    }
    public Autenticar(Guid id, string email, string senha)
    {
        _id = id;
        _email = email;
        _senha = senha;
    }
}
