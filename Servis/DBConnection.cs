using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Amalgama.Servis
{
    public class DBConnection
    {
        public class ApplicationDbContext : DbContext
        {
            public DbSet<User> Users { get; set; }
            public DbSet<Administrator> Administrators { get; set; }
            public DbSet<SuperAdmin> SuperAdmins { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Data Source=login.db");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Настройка схемы для Administrator и SuperAdmin
                modelBuilder.Entity<Administrator>()
                    .Property(a => a.IsAdmin)
                    .HasDefaultValue(true);

                modelBuilder.Entity<SuperAdmin>()
                    .Property(sa => sa.IsSuperAdmin)
                    .HasDefaultValue(true);
            }

            public async Task EnsureDatabaseUpdatedAsync()
            {
                await Database.MigrateAsync(); // Автоматически применяет изменения к схеме
            }

            public async Task InitializeDatabaseAsync()
            {
                // Удаляем старую базу данных, если она существует
                string dbPath = "login.db";
                if (File.Exists(dbPath))
                {
                    File.Delete(dbPath);
                }

                // Создаем новую базу данных
                await Database.EnsureCreatedAsync();

                // Проверяем, существует ли тестовый пользователь
                if (!await Users.AnyAsync(u => u.Login == "Test"))
                {
                    var defaultUser = new User
                    {
                        Login = "Test",
                        Password = "Test",
                    };

                    Users.Add(defaultUser);
                    await SaveChangesAsync();
                }

                // Проверяем, существует ли тестовый администратор
                if (!await Administrators.AnyAsync(a => a.Login == "Admin"))
                {
                    var defaultAdmin = new Administrator
                    {
                        Login = "Admin",
                        Password = "Admin",
                        IsAdmin = true
                    };

                    Administrators.Add(defaultAdmin);
                    await SaveChangesAsync();
                }

                // Проверяем, существует ли тестовый суперадмин
                if (!await SuperAdmins.AnyAsync(sa => sa.Login == "SuperAdmin"))
                {
                    var defaultSuperAdmin = new SuperAdmin
                    {
                        Login = "SuperAdmin",
                        Password = "SuperAdmin",
                        IsSuperAdmin = true
                    };

                    SuperAdmins.Add(defaultSuperAdmin);
                    await SaveChangesAsync();
                }
            }
        }

        public class User
        {
            public int Id { get; set; }
            public string Login { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public class Administrator
        {
            public int Id { get; set; }
            public string Login { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;

            // Определяет, является ли пользователь администратором
            public bool IsAdmin { get; set; }
        }

        public class SuperAdmin
        {
            public int Id { get; set; }
            public string Login { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;

            // Определяет, является ли пользователь суперадмином
            public bool IsSuperAdmin { get; set; }
        }
    }
}

