using TuberTreats.Models.DTO;
using TuberTreats.Models;

List<Customer> customers = new List<Customer>
{
    new Customer { Id = 1, Name = "Alice Johnson", Address = "123 Maple Street" },
    new Customer { Id = 2, Name = "Bob Smith", Address = "456 Oak Avenue" },
    new Customer { Id = 3, Name = "Charlie Davis", Address = "789 Pine Lane" },
    new Customer { Id = 4, Name = "Diana Lee", Address = "321 Birch Blvd" },
    new Customer { Id = 5, Name = "Ethan Martinez", Address = "654 Cedar Road" }
};

List<Topping> toppings = new List<Topping>
{
    new Topping { Id = 1, Name = "Pepperoni" },
    new Topping { Id = 2, Name = "Mushrooms" },
    new Topping { Id = 3, Name = "Onions" },
    new Topping { Id = 4, Name = "Green Peppers" },
    new Topping { Id = 5, Name = "Black Olives" }
};

List<TuberDriver> drivers = new List<TuberDriver>
{
    new TuberDriver { Id = 1, Name = "Liam Carter" },
    new TuberDriver { Id = 2, Name = "Sophia Nguyen" },
    new TuberDriver { Id = 3, Name = "Marcus Bennett" }
};

 List<TuberOrder> orders = new List<TuberOrder> {
            new TuberOrder
            {
                Id = 1,
                OrderPlacedOnDate = new DateTime(2025, 4, 20, 14, 15, 0),
                DeliveredOnDate = new DateTime(2025, 4, 20, 14, 45, 0),
                CustomerId = 1,
                TuberDriverId = 1,
            },
            new TuberOrder
            {
                Id = 2,
                OrderPlacedOnDate = new DateTime(2025, 4, 21, 10, 30, 0),
                DeliveredOnDate = new DateTime(2025, 4, 21, 11, 0, 0),
                CustomerId = 2,
                TuberDriverId = 2,
            },
            new TuberOrder
            {
                Id = 3,
                OrderPlacedOnDate = new DateTime(2025, 4, 19, 18, 0, 0),
                DeliveredOnDate = new DateTime(2025, 4, 19, 18, 25, 0),
                CustomerId = 3,
                TuberDriverId = null,
            },
 };

List<TuberTopping> tuberToppings = new List<TuberTopping>
{
    new TuberTopping { Id = 1, TuberOrderId = 1, ToppingId = 1 },
    new TuberTopping { Id = 2, TuberOrderId = 1, ToppingId = 2 }, 
    new TuberTopping { Id = 3, TuberOrderId = 2, ToppingId = 3 }
};


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/getorders", () => 
{
    return orders.Select(o => 
    {
        List<TuberTopping> TT = tuberToppings.Where(t => t.ToppingId == o.Id).ToList();
        List<Topping> topping = TT.Select(t => toppings.FirstOrDefault(c => t.ToppingId == c.Id)).ToList();
        return new TuberOrderDTO {
            Id = o.Id,
            OrderPlacedOnDate = o.OrderPlacedOnDate,
            CustomerId = o.CustomerId,
            TuberDriverId = o.TuberDriverId,
            DeliveredOnDate = o.DeliveredOnDate,
            Toppings = toppings.Select(t => new ToppingsDTO 
            {
                Id = t.Id,
                Name = t.Name
            }).ToList()
        }; 
    });
});

app.Run();
//don't touch or move this!
public partial class Program { }