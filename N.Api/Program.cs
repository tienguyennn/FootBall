using Microsoft.Extensions.FileProviders;
using N.Extensions;
using NDrive.Core.Middleware;
using N;
using N.Core.SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.UseAppSettings();

builder.Services.UseConfigurationServices();


//builder.Services.AddTransient<FileProtectedMiddleware>();
//builder.Services.AddTransient<TokenMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});


app.UseSwagger();
app.UseSwaggerUI();
app.UseDeveloperExceptionPage();

//app.UseMiddleware<FileProtectedMiddleware>();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Root")),
    RequestPath = "/Root",
});

//app.UseMiddleware<TokenMiddleware>();

//app.UseAuthentication();
//app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();
//app.MapHub<ChatHub>("/chatHub");

app.Run();
