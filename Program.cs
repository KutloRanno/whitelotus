using Microsoft.EntityFrameworkCore;
using WhiteLotus.Main.Service.Data;
using WhiteLotus.Main.Service.Entities;
using WhiteLotus.Main.Service.Repositories;

var builder = WebApplication.CreateBuilder(args);

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

builder.Services.AddDbContext<WhiteLotusContext>(options =>
  options.UseNpgsql(builder.Configuration.GetConnectionString("lotusDb")));//by me

builder.Services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames=false;
            });

// builder.Services.AddScoped<IRepository<IEntity>, EntityFrameworkWhiteLotusRepository<IEntity>>();
builder.Services.AddScoped(typeof(IRepository<>),typeof( EntityFrameworkWhiteLotusRepository<>));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();//me

  

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// now to enable CORS in the app
builder.Services.AddCors(options=>
{
    options.AddPolicy("myAppCors",policy=>
    {
        policy.WithOrigins(allowedOrigins)
        .AllowAnyHeader()
        .AllowAnyMethod();
});
});

var app = builder.Build(); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{//me
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{//me
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<WhiteLotusContext>();
    context.Database.EnsureCreated();
    
}

app.UseHttpsRedirection();

app.UseCors("myAppCors");

app.MapControllers();

app.Run();


