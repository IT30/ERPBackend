using Farma.Entities;
using Farma.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Stripe;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Azure.Core;

var AllowCors = "_allowCors";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Repositories
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IOriginRepository, OriginRepository>();
builder.Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<FarmaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FarmaDB")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowCors, builder =>
    {
        builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
    });
});

var app = builder.Build();

string? connectionString = builder.Configuration.GetConnectionString("FarmaDB");
string? scriptPathDDL = builder.Configuration.GetConnectionString("ScriptPathDDL");

if (connectionString != null && scriptPathDDL != null)
{
    string? scriptDDL = System.IO.File.ReadAllText(scriptPathDDL);
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();

        SqlCommand command = new SqlCommand(scriptDDL, connection);
        command.ExecuteNonQuery();

        connection.Close();
    }
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<FarmaContext>();
    //    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors(AllowCors);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Photos")),
    RequestPath = "/Photos"
});

app.Run();

//STRIPE
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

[Route("create-payment-intent")]
[ApiController]
public class PaymentIntentApiController : Controller
{
    [HttpPost]
    public ActionResult Create(PaymentIntentCreateRequest request)
    {
        StripeConfiguration.ApiKey = "sk_test_51NFRY6Aww2h82Tm02K7WQ7qT47OMEZPHYjm4pItFDd4g3hv372gPZrRQpivDOVblQl2DJ2Mty2VuSuCc9PxM8ocd00kTLKQaSw";
        var paymentIntentService = new PaymentIntentService();
        var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
        {
            Amount = CalculateOrderAmount(request.TotalAmount),
            Currency = "eur",
            PaymentMethodTypes = new List<string> { "card" },
        });

        return Json(new { clientSecret = paymentIntent.ClientSecret });
    }
    private int CalculateOrderAmount(int amount)
    {
        if(amount <1)
            return 100;
        // Replace this constant with a calculation of the order's amount
        // Calculate the order total on the server to prevent
        // people from directly manipulating the amount on the client
        //return items[0].TotalAmount;
        return amount;
    }

    public class Item
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
    }

    public class PaymentIntentCreateRequest
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }
        [JsonProperty("totalAmount")]
        public int TotalAmount { get; set; }
    }
}
