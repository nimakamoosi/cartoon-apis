using CartoonApis;
using CartoonApis.DataAccess;
using CartoonApis.GraphQL;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.EntityFrameworkCore;

// Create builder and services
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

//
// Configure services
services.AddDbContextFactory<ApplicationDbContext>(options => options.UseInMemoryDatabase("CartoonFamilies"));

services.AddInMemorySubscriptions(); // Without this:  "message": "No service for type 'HotChocolate.Subscriptions.ITopicEventSender' has been registered.",

// Register the in memory repositories
services.AddScoped<IFamilyRepository, FamilyRepository>();
services.AddScoped<IPersonRepository, PersonRepository>();

// Add GraphQL and define bindings explicitely using types
services.AddGraphQLServer()
    .ModifyOptions(options => { options.DefaultBindingBehavior = BindingBehavior.Explicit; })
    .AddType<FamilyType>()
    .AddType<PersonType>()
    .AddQueryType<QueryType>()
    .AddMutationType<MutationType>()
    .AddSubscriptionType<SubscriptionType>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
    .AddCertificate(options =>
    {
        options.Validate();
        options.AllowedCertificateTypes = CertificateTypes.All;
        options.Events = new CertificateAuthenticationEvents
        {             
            OnCertificateValidated = context =>
            {
                var clientCertificate = context.ClientCertificate;
                if (clientCertificate == null || string.IsNullOrWhiteSpace(clientCertificate.Thumbprint))
                {
                    context.Fail("Invalid client certificate.");
                }
                else
                {
                    context.HttpContext.Response.Headers.Add("Cartoons-ClientCertificate-Thumbprint", clientCertificate.Thumbprint);
                    context.Success();
                }

                return Task.CompletedTask;
            }
        };
    }
);

//
// Build an configure app
var app = builder.Build();

app.UseAuthentication();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

//app.UseDeveloperExceptionPage();

app.UseWebSockets();
app.MapGraphQL();

app.Run();
