using Autofac;
using Autofac.Extensions.FluentBuilder;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Etdb.ReportingService.Autofac.Modules;
using Etdb.ReportingService.AutoMapper.Profiles;
using Etdb.ReportingService.Cqrs.CommandHandler;
using Etdb.ReportingService.Repositories;
using Etdb.ServiceBase.DocumentRepository;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Etdb.ReportingService.Autofac.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static void SetupDependencies(this ContainerBuilder containerBuilder, IHostEnvironment environment)
        {
            containerBuilder.AddAutoMapper(typeof(UserRegistrationProfile).Assembly);
            containerBuilder.AddMediatR(typeof(UserRegistrationStoreCommandHandler).Assembly);
            
            new AutofacFluentBuilder(containerBuilder)
                .AddClosedTypeAsScoped(typeof(GenericDocumentRepository<,>), new []
                {
                    typeof(UserRegistrationsRepository).Assembly
                })
                .ApplyModule(new ResourceLockingAdapterModule(false))
                .ApplyModule(new DocumentDbContextModule(environment))
                .ApplyModule(new MessageConsumerModule(environment));
        }
    }
}