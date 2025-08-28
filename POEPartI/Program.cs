using POEPartI.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAzureStorageService, AzureStorageService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var storageService = scope.ServiceProvider.GetRequiredService<IAzureStorageService>();
        await storageService.InitializeAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"?? Azure Storage initialization failed: {ex.Message}");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
