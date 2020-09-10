using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.Entity;
using ormMySql.Modelo;

namespace ormMySql
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    class DB : DbContext
    {

        public DbSet<Post> posts { get; set; }
        public DbSet<Usuario> usuarios { get; set; }

        // El string "dbConn" es el nombre del connection string definido en App.config
        public DB() : base("dbConn"){

            // Deshabilita la inicializacion mágica del ORM
            Database.SetInitializer<DB>(null);

        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<Post>()
                .HasRequired<Usuario>(s => s.creador)
                .WithMany(g => g.posts)
                .HasForeignKey<int>(s => s.creador_id);
        }
    }

}
