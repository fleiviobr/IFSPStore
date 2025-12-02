using AutoMapper;
using IFSPStore.App.Others;
using IFSPStore.App.Register;
using IFSPStore.App.ViewModel;
using IFSPStore.Domain.Base;
using IFSPStore.Domain.Entities;
using IFSPStore.Repository.Context;
using IFSPStore.Repository.Repository;
using IFSPStore.Service.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualBasic.Logging;

namespace IFSPStore.App.Infra
{
    public static class ConfigureDI
    {
        public static ServiceCollection services;
        public static IServiceProvider? serviceProvider;
        public static void ConfigureServices()
        {
            //Database config
            var dbConfigFile = "Config/DBConfig.txt";
            var strCon = File.ReadAllText(dbConfigFile);
            services = new ServiceCollection();
            services.AddDbContext<IFSPStoreContext>(
                options =>
                {
                    options.LogTo(Console.WriteLine);
                    options.UseMySQL(strCon);
                }
                );
            services.AddScoped<IBaseRepository<Category>, BaseRepository<Category>>();
            services.AddScoped<IBaseRepository<City>, BaseRepository<City>>();
            services.AddScoped<IBaseRepository<Customer>, BaseRepository<Customer>>();
            services.AddScoped<IBaseRepository<Product>, BaseRepository<Product>>();
            services.AddScoped<IBaseRepository<Sale>, BaseRepository<Sale>>();
            services.AddScoped<IBaseRepository<SaleItem>, BaseRepository<SaleItem>>();
            services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();


            services.AddScoped<IBaseService<Category>, BaseService<Category>>();
            services.AddScoped<IBaseService<City>, BaseService<City>>();
            services.AddScoped<IBaseService<Customer>, BaseService<Customer>>();
            services.AddScoped<IBaseService<Product>, BaseService<Product>>();
            services.AddScoped<IBaseService<Sale>, BaseService<Sale>>();
            services.AddScoped<IBaseService<SaleItem>, BaseService<SaleItem>>();
            services.AddScoped<IBaseService<User>, BaseService<User>>();

            services.AddTransient<LoginForm, LoginForm>();
            services.AddTransient<CategoryForm, CategoryForm>();
            services.AddTransient<CityForm, CityForm>();
            services.AddTransient<CustomerForm, CustomerForm>();
            services.AddTransient<ProductForm, ProductForm>();
            services.AddTransient<SaleForm, SaleForm>();
            services.AddTransient<UserForm, UserForm>();

            services.AddSingleton(new MapperConfiguration(config => 
            {
                config.CreateMap<Category, CategoryViewModel>();
                config.CreateMap<City, CityViewModel>()
                    .ForMember(d => d.NameState, d => d.MapFrom(src => $"{src.Name} - {src.State}"));
                config.CreateMap<Customer, CustomerViewModel>()
                    .ForMember(d => d.City, d => d.MapFrom(src => $"{src.City.Name} - {src.City.State}"))
                    .ForMember(d => d.IdCity, d => d.MapFrom(src => src.City!.Id));
                config.CreateMap<Product, ProductViewModel>()
                    .ForMember(d => d.Category, d => d.MapFrom(src => $"{src.Category!.Name}"))
                    .ForMember(d => d.IdCategory, d => d.MapFrom(src => src.Category!.Id))
                    .ForMember(d => d.PurchaseDate, d => d.MapFrom(src => $"{src.PurchaseDate}"));
                config.CreateMap<User, UserViewModel>();
                config.CreateMap<Sale, SaleViewModel>()
                    .ForMember(d => d.IdCustomer, d => d.MapFrom(x => x.Customer!.Id))
                    .ForMember(d => d.Customer, d => d.MapFrom(x => x.Customer!.Name))
                    .ForMember(d => d.IdSalesman, d => d.MapFrom(x => x.Salesman!.Id))
                    .ForMember(d => d.Salesman, d => d.MapFrom(x => x.Salesman!.Name));
                config.CreateMap<SaleItem, SaleItemViewModel>()
                    .ForMember(d => d.IdProduct, d => d.MapFrom(x => x.Product!.Id))
                    .ForMember(d => d.Product, d => d.MapFrom(x => x.Product!.Name));

            }, NullLoggerFactory.Instance).CreateMapper());
            
            serviceProvider  = services.BuildServiceProvider();
        }
    }
}
