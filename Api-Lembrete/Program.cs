
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Api-Lembrete")));

builder.Services.AddControllers();
builder.Services.AddScoped<DataContexto>();
builder.Services.AddScoped<UsuarioRegras>();
builder.Services.AddScoped<LembreteRegras>();
builder.Services.AddScoped<UsuarioCasoUso>();
builder.Services.AddScoped<LembreteCasoUso>();
builder.Services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddTransient<ILembreteRepositorio, LembreteRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
