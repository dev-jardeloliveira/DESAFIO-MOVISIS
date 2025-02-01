namespace Api_Lembrete.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UsuarioCasoUso casoUso;

    public TokenController(IConfiguration configuration, UsuarioCasoUso casoUso)
    {
        _configuration = configuration;
        this.casoUso = casoUso;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] Usuario usuario)
    {
        Usuario? resultado = await casoUso.Filtrar(usuario.Email!);

        if (usuario.Senha == resultado?.Senha)
        {
            string token = GenerateJwtToken(resultado!.Email!);
            return Ok(new { Token = token, Id = resultado.Id });
        }

        return Unauthorized();
    }

    private string GenerateJwtToken(string email)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
