using Library.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connStr = configuration.GetConnectionString("SqliteConnection");
            string dataDirectory = String.Empty;
            connStr = String.Format(connStr, dataDirectory);

            var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connStr).Options;
            services.AddScoped<AppDbContext>(s => new AppDbContext(options));

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookBorrowingRepository, BookBorrowingRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
