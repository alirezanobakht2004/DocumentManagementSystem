using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using ContractManager.Infrastructure.Data;
using ContractManager.Application.Interfaces;
using ContractManager.Application.Services;
using ContractManager.Infrastructure.Repositories;
using ContractManager.Presentation.ViewModels;
using System;
using Microsoft.EntityFrameworkCore;
using ContractManager.Application.Configuration;
using ContractManager.Infrastructure.Storage;
using ContractManager.Infrastructure.Services;

namespace ContractManager.Presentation
{
    public partial class App : System.Windows.Application
    {
        public IServiceProvider? ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // جلوگیری از بسته شدن ناگهانی برنامه هنگام بروز خطاهای پیش‌بینی نشده
            this.DispatcherUnhandledException += (sender, args) =>
            {
                MessageBox.Show($"یک خطای غیرمنتظره رخ داد:\n\n{args.Exception.Message}\n\n{args.Exception.InnerException?.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                args.Handled = true; // جلوگیری از کرش کردن
            };

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            // اعمال خودکار مایگریشن‌ها و اطمینان از وجود دیتابیس در مسیر اجرایی
            using (var scope = ServiceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();

                dbContext.Database.ExecuteSqlRaw("PRAGMA journal_mode=WAL;");
                dbContext.Database.ExecuteSqlRaw("PRAGMA synchronous=NORMAL;");
                dbContext.Database.ExecuteSqlRaw("PRAGMA foreign_keys=ON;");
            }

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(
                    "Data Source=ContractManager.db;Cache=Shared;Pooling=True;Foreign Keys=True",
                    sqliteOptions =>
                    {
                        sqliteOptions.CommandTimeout(30);
                    }));
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IContractService, ContractService>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainWindow>();
            var storageOptions = new FileStorageOptions
            {
                RootPath = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "DMSStorage")
            };
            services.AddSingleton(storageOptions);
            services.AddSingleton<IFileStorageService, FileStorageService>();
            services.AddScoped<IAttachmentService, AttachmentService>();
        }
    }
}
