using Microsoft.EntityFrameworkCore;
using BankApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options => 
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder => 
                    {
                        builder.WithOrigins("http://localhost:3000");
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    }
    );
});

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("TodoList")); 
builder.Services.AddDbContext<AccountsContext>(opt =>
    opt.UseInMemoryDatabase("Accounts"));
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new() { Title = "BankApp", Version = "v1" });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BankApp v1"));
}

app.UseDefaultFiles();
app.UseStaticFiles(); 

// app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
