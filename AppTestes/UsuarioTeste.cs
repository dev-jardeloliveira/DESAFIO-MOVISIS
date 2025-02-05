using Dados_App.Modelo;
using Dados_App.Repositorios;
using Moq;
using Negocio_App.CasoUso;

namespace Negocio_App.Tests
{
    [TestFixture]
    public class UsuarioCasoUsoTests
    {
        private Mock<IUsuarioRepositorio> _mockRepositorio;
        private UsuarioCasoUso _usuarioCasoUso;

        [SetUp]
        public void Setup()
        {
            _mockRepositorio = new Mock<IUsuarioRepositorio>();
            _usuarioCasoUso = new UsuarioCasoUso(_mockRepositorio.Object);
        }

        [Test]
        public async Task Gravar_DeveRetornarResultadoDoRepositorio()
        {
            
            var usuario = new Usuario { Email = "joao@teste.com", Senha="asd1234@dfsd454dasseeeee" };
            var resultadoEsperado = "Usuário gravado com sucesso!";
            _mockRepositorio.Setup(r => r.Gravar(usuario)).ReturnsAsync(resultadoEsperado);

            
            var resultado = await _usuarioCasoUso.Gravar(usuario);

            Assert.AreEqual(resultadoEsperado, resultado);
            _mockRepositorio.Verify(r => r.Gravar(usuario), Times.Once); 
        }

        [Test]
        public async Task Verificar_DeveRetornarTokenRespostaDoRepositorio()
        {
            var usuario = new Usuario { Email = "joao@teste.com", Senha = "asd1234@dfsd454dasseeeee" };
            var tokenRespostaEsperado = new TokenResposta { token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImphcmRlbEBnbWFpbC5jb20iLCJqdGkiOiJhOGQyOGQzMy0yMDg5LTQwMDctODM1Yy0xZjBiOGFjMGM3MWYiLCJleHAiOjE3Mzg4NjUwNjMsImlzcyI6Ik1pbmhhQXBpQ29tVG9rZW4iLCJhdWQiOiJVc3Vhcmlvc0RhQXBpIn0.XQML9vfbzaAKBG6h4Gkw-tS1WVF7UEtO9NMN_0dtyMo", id = "7B9D8C33-C6E8-4FD2-A903-633B9E55FB66" };
            _mockRepositorio.Setup(r => r.Verificar(usuario)).ReturnsAsync(tokenRespostaEsperado);

           
            var resultado = await _usuarioCasoUso.Verificar(usuario);

            
            Assert.AreEqual(tokenRespostaEsperado.token, resultado.token);
            Assert.AreEqual(tokenRespostaEsperado.id, resultado.id);
            _mockRepositorio.Verify(r => r.Verificar(usuario), Times.Once);
        }

        [Test]
        public async Task Verificar_DeveLancarExcecaoQuandoRepositorioFalhar()
        {
            var usuario = new Usuario { Email = "joao@teste.com", Senha = "asd1234@dfsd454dasseeeee" };
            _mockRepositorio.Setup(r => r.Verificar(usuario)).ThrowsAsync(new Exception("Erro no repositório"));

            var ex = Assert.ThrowsAsync<Exception>(() => _usuarioCasoUso.Verificar(usuario));
            Assert.AreEqual("Erro no repositório", ex.Message);
        }
    }
}