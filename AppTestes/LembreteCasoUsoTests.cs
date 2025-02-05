using Dados_App.Modelo;
using Dados_App.Repositorios;
using Moq;
using Negocio_App.CasoUso;

namespace Negocio_App.Tests
{
    [TestFixture]
    public class LembreteCasoUsoTests
    {
        private Mock<ILembreteRepositorio> _mockRepositorio;
        private LembreteCasoUso _lembreteCasoUso;

        [SetUp]
        public void Setup()
        {
            _mockRepositorio = new Mock<ILembreteRepositorio>();
            _lembreteCasoUso = new LembreteCasoUso(_mockRepositorio.Object);
        }

        [Test]
        public async Task Gravar_DeveRetornarResultadoDoRepositorio()
        {
            var lembrete = new LembreteResponse {  id = Guid.NewGuid().ToString(), descricao = "Descricao", hora ="13:00", idUsuario = "7B9D8C33-C6E8-4FD2-A903-633B9E55FB66", titulo = "Titulo", vencimento = DateTime.Now };
            var token = "token123";
            var resultadoEsperado = "Lembrete gravado com sucesso!";
            _mockRepositorio.Setup(r => r.Gravar(lembrete, token)).ReturnsAsync(resultadoEsperado);

            var resultado = await _lembreteCasoUso.Gravar(lembrete, token);

            Assert.AreEqual(resultadoEsperado, resultado);
            _mockRepositorio.Verify(r => r.Gravar(lembrete, token), Times.Once);
        }

        [Test]
        public async Task Alterar_DeveRetornarResultadoDoRepositorio()
        {
            var lembrete = new LembreteResponse { id = Guid.NewGuid().ToString(), descricao = "DescricaoUpdate", hora = "13:00", idUsuario = "7B9D8C33-C6E8-4FD2-A903-633B9E55FB66", titulo = "TituloUpdate", vencimento = DateTime.Now };
            var token = "token123";
            var resultadoEsperado = "Lembrete alterado com sucesso!";
            _mockRepositorio.Setup(r => r.Alterar(lembrete, token)).ReturnsAsync(resultadoEsperado);

            var resultado = await _lembreteCasoUso.Alterar(lembrete, token);

            Assert.AreEqual(resultadoEsperado, resultado);
            _mockRepositorio.Verify(r => r.Alterar(lembrete, token), Times.Once);
        }

        [Test]
        public async Task Excluir_DeveRetornarResultadoDoRepositorio()
        {
            var id = Guid.NewGuid();
            var token = "token123";
            var resultadoEsperado = "Lembrete excluído com sucesso!";
            _mockRepositorio.Setup(r => r.Excluir(id, token)).ReturnsAsync(resultadoEsperado);

            var resultado = await _lembreteCasoUso.Excluir(id, token);

            Assert.AreEqual(resultadoEsperado, resultado);
            _mockRepositorio.Verify(r => r.Excluir(id, token), Times.Once);
        }

        [Test]
        public async Task Todos_DeveRetornarListaDeLembretesDoRepositorio()
        {
            var token = "token123";
            var idUsuario = Guid.NewGuid();
            var lembretesEsperados = new List<LembreteResponse>
            {
               new LembreteResponse {  id = Guid.NewGuid().ToString(), descricao = "Descricao", hora ="13:00", idUsuario = "7B9D8C33-C6E8-4FD2-A903-633B9E55FB66", titulo = "Titulo", vencimento = DateTime.Now },
               new LembreteResponse {  id = Guid.NewGuid().ToString(), descricao = "Descricao", hora ="13:00", idUsuario = "7B9D8C33-C6E8-4FD2-A903-633B9E55FB66", titulo = "Titulo", vencimento = DateTime.Now }
            };
            _mockRepositorio.Setup(r => r.Todos(token, idUsuario)).ReturnsAsync(lembretesEsperados);

            var resultado = await _lembreteCasoUso.Todos(token, idUsuario);

            Assert.AreEqual(lembretesEsperados.Count, resultado.Count);
            Assert.AreEqual(lembretesEsperados[0].titulo, resultado[0].titulo);
            Assert.AreEqual(lembretesEsperados[1].titulo, resultado[1].titulo);
            _mockRepositorio.Verify(r => r.Todos(token, idUsuario), Times.Once);
        }

        [Test]
        public async Task Gravar_DeveLancarExcecaoQuandoRepositorioFalhar()
        {
            var lembrete = new LembreteResponse { id = Guid.NewGuid().ToString(), descricao = "Descricao", hora = "13:00", idUsuario = "7B9D8C33-C6E8-4FD2-A903-633B9E55FB66", titulo = "Titulo", vencimento = DateTime.Now };
            var token = "token123";
            _mockRepositorio.Setup(r => r.Gravar(lembrete, token)).ThrowsAsync(new Exception("Erro no repositório"));

            var ex = Assert.ThrowsAsync<Exception>(() => _lembreteCasoUso.Gravar(lembrete, token));
            Assert.AreEqual("Erro no repositório", ex.Message);
        }
    }
}