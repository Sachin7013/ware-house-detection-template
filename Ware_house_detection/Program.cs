using Microsoft.Extensions.FileProviders;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Serve additional static folders outside wwwroot
// Map "/evidence" and "/Logo" to physical directories in the content root
{
    var contentRoot = app.Environment.ContentRootPath;

    var evidenceDir = Path.Combine(contentRoot, "evidence");
    if (Directory.Exists(evidenceDir))
    {
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(evidenceDir),
            RequestPath = "/evidence"
        });
    }

    var logoDir = Path.Combine(contentRoot, "Logo");
    if (Directory.Exists(logoDir))
    {
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(logoDir),
            RequestPath = "/Logo"
        });
    }
}

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
