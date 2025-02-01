using Dados_App.Modelo;
using Dados_App.Repositorios;
using Dados_App.Servicos;
using Moq;

namespace AppTestes;

public class UsuarioTeste
{
    private Mock<IHttpClientServico> _httpClientServicoMock;
    private UsuarioRepositorio _usuarioRepositorio;

    [SetUp]
    public void Setup()
    {
        _httpClientServicoMock = new Mock<IHttpClientServico>();
        _usuarioRepositorio = new UsuarioRepositorio(_httpClientServicoMock.Object);
    }

    [Test]
    public async Task Gravar_DeveChamarPostAsyncComUsuarioCorreto()
    {
        var usuario = new Usuario { Id = Guid.NewGuid(), Email = "joao@example.com", Senha ="4dfsdfd545654dfasdfsd" };

        string result = await _usuarioRepositorio.Gravar(usuario);

        _httpClientServicoMock.Verify(
            x => x.PostAsync<Usuario>("/usuarios", usuario),
            Times.Once);
    }
}