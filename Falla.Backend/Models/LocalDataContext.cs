namespace Falla.Backend.Models
{
    using Domain;

    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<Falla.Domain.Act> Acts { get; set; }

        public System.Data.Entity.DbSet<Falla.Domain.ActAssistance> ActAssistances { get; set; }

        public System.Data.Entity.DbSet<Falla.Domain.Component> Components { get; set; }
    }
}