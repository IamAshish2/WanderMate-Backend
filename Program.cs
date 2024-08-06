using Microsoft.EntityFrameworkCore;
using secondProject.context;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddControllers();
//for relationship we must add this jsonserilizer
//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
//    });

builder.Services.AddControllers();

//Cross-origin resource sharing(CORS)
builder.Services.AddCors(options => {
    options.AddPolicy ("AllowAllOrigins",
        builder => {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins"); // enable the cors policy

app.UseHttpsRedirection();


app.Run();