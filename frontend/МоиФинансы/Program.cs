using МоиФинансы.Components;
using МоиФинансы.Components.Servies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ApiService>();
builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
options.AddPolicy("AllowBlazor", p =>
p.WithOrigins("https://localhost:5001")
.AllowAnyHeader()
.AllowAnyMethod()));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

