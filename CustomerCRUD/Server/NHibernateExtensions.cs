using CustomerCRUD.Shared;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.Reflection;

namespace CustomerCRUD.Server
{
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNHibernate(this IServiceCollection services, string fileName, Assembly assembly)
        {
            bool initialize = false;

            if (!File.Exists(fileName))
            {
                initialize = true;
            }

            var sessionFactory = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile(fileName))
                .Mappings(m => m.FluentMappings.AddFromAssembly(assembly))
                .ExposeConfiguration(initialize ? BuildSchema : null)
                .BuildSessionFactory();

            if (initialize)
            {
                using (var session = sessionFactory.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        var customer = new Customer()
                        {
                            Name = "Torpedo",
                            Surname = "Pradera Floreada",
                            PostalCode = 21001,
                            Address = "Avenida de Gato Negro",
                            Email = "grijander@gmail.com",
                            BirthDate= new DateTime(2000, 12, 1),
                            Country = "Barbate",
                            Gender = Customer.GenderEnum.Unspecified,
                        };
                        session.SaveOrUpdate(customer);

                        customer = new Customer()
                        {
                            Name = "Fino",
                            Surname = "Filipino Minino",
                            PostalCode = 41011,
                            Address = "Calle Nomedigas",
                            Email = "ertiolavara@hotmail.com",
                            BirthDate = new DateTime(2000, 2, 14),
                            Country = "Moderdonia",
                            Gender = Customer.GenderEnum.Male,
                        };
                        session.SaveOrUpdate(customer);

                        transaction.Commit();
                    }
                }
            }

            services.AddSingleton(sessionFactory);
            services.AddScoped(_ => sessionFactory.OpenSession());

            return services;
        }

        private static void BuildSchema(Configuration config)
        {
            new SchemaExport(config).Create(false, true);
        }
    }
}
