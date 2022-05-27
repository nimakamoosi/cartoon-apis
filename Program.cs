using CartoonApis.DataAccess;
using CartoonApis.GraphQL;
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

//
// Build an configure app
var app = builder.Build();

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
