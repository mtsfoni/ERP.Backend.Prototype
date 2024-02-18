namespace ERP.Backend.REST
{
    public record Provider(string Name, string Assembly)
    {
        public static Provider Sqlite = new(nameof(Sqlite), typeof(ERP.Backend.SQLite.Marker).Assembly.GetName().Name!);
        public static Provider Postgres = new(nameof(Postgres), typeof(ERP.Backend.Postgres.Marker).Assembly.GetName().Name!);
    }
}
