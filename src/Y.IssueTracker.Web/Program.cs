using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Y.IssueTracker.Web.Infrastructure;
using Y.IssueTracker.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddScoped(x =>
{
    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
    var factory = x.GetRequiredService<IUrlHelperFactory>();

    return factory.GetUrlHelper(actionContext);
});

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        x.LoginPath = "/Account/Login";
        x.AccessDeniedPath = "/Account/Login";
        x.ExpireTimeSpan = TimeSpan.FromHours(1);
        x.Cookie.HttpOnly = true;
    });

builder.Services.AddSingleton<ITicketStore, SimpleTicketStore>();

builder.Services.AddOptions<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme)
    .Configure<ITicketStore>((x, y) => x.SessionStore = y);

builder.Services.AddRouting(x =>
{
    x.LowercaseUrls = true;
    x.LowercaseQueryStrings = true;
});

builder.Services.AddHttpClient("api", client =>
{
    client.BaseAddress = new Uri((builder.Configuration.GetValue<string>("ApiUrl")));
});

// Web services
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPriorityService, PriorityService>();
builder.Services.AddScoped<IIssueService, IssueService>();
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.UseStatusCodePagesWithRedirects("/Error");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
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
