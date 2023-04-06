var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


/// <summary>
/// This Interface IAuthenticationService
/// contains all your needs to serilize and encode and save in IO (cookie,token ...)
/// https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.authentication.iauthenticationservice?view=aspnetcore-7.0
/// </summary>
/// Note : Scheme name provides a logical grouping for the handlers,identity and claimes principles all togetter
/// Note : you can have multiple authentication scheemes but only one scheme can be used by middlewar
/// AddAuthentication("MyCookieAuth")
/// 
builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth" , options =>
{
    options.Cookie.Name = "MyCookieAuth";

});
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

app.UseRouting();

//This is a Middleware that going to http request header and then populate
//security context values , when the middleware is looking to httpcontext header it should 
//be able to see the cookie and then intreprate the cookie
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
