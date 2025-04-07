using VoskanyanFinBeatApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();
app.UseCors(options 
    => options.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VoskanyanFinBeatAPI v1"));
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
