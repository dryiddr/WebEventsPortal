using FluentValidation;
using FluentValidation.AspNetCore;
using Application.Dtos.User;
using Application.Mapping;
using Application.Validation;

namespace ServiceExtension;

public class FluentValidationInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<RegisterUserDtoValidator>();
    }
}