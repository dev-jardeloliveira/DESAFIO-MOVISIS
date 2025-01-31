namespace Data_Api.Contexto;

public class DataContexto : DbContext
{
    public DataContexto(DbContextOptions<DataContexto> options)
           : base(options)
    {
    }

    public DataContexto()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-73SMSOP;Database=LembreteDB;User Id=sa;Password=@Jard1978el;TrustServerCertificate=true;",
                b => b.MigrationsAssembly("Api-Lembrete"));
        }
    }

    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Lembrete> Lembrete { get; set; }
}
