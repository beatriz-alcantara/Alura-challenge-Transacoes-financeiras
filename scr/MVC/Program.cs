using Alura_Challange_Transacao_Financeira.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ProvedorArmazenamentoIDMongo;
using ProvedorArmazenamentoIDMongo.Modelos;
using ProvedorArmazenamentoIDMongo.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<Usuarios, RegrasUsuario>();
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoDBSettings"));
builder.Services.AddTransient<IUserStore<Usuarios>, UsuarioStore>();
builder.Services.AddTransient<IRoleStore<RegrasUsuario>, RegrasUsuarioRepository>();
builder.Services.AddScoped<TransacaoRepository>();
builder.Services.AddScoped<ImportacoesRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
