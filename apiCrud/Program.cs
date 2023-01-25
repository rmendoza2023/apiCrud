using apiCrud.Data;
using apiCrud.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<OfficeDb>(options =>
    options.UseNpgsql(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHttpsRedirection();
app.MapPost("/insertEmployee/", async (Employee e, OfficeDb db) =>
{
    db.Employees.Add(e);
    await db.SaveChangesAsync();

    return Results.Created($"/employee/{e.idEmployee}",e);
});

app.MapGet("/selectEmployee/{idEmployee}", async (int idEmployee, OfficeDb db) =>
{
    return await db.Employees.FindAsync(idEmployee)
    is Employee employee
    ? Results.Ok(employee)
    : Results.NotFound();
});

app.MapGet("/listAllEmployees", async (OfficeDb db) =>
await db.Employees.ToListAsync());



app.MapPut("/upEmployee/{idEmployee}", async (int idEmployee, Employee inputEmployee, OfficeDb db) =>
{
    var employee = await db.Employees.FindAsync(idEmployee);
    if (employee is null) return Results.NotFound();

    employee.employeeName = inputEmployee.employeeName;
    employee.employeeLastName = inputEmployee.employeeLastName;
    employee.employeeMail = inputEmployee.employeeMail;
    employee.department = inputEmployee.department;
    employee.extension = inputEmployee.extension;
    employee.phoneNumber = inputEmployee.phoneNumber;
    employee.collaboratorNumber = inputEmployee.collaboratorNumber;
    employee.dateBirth = inputEmployee.dateBirth;
    //employee.employeedPhoto = inputEmployee.employeedPhoto;
    employee.workingHours = inputEmployee.workingHours;
    employee.jobTitle = inputEmployee.jobTitle;

    await db.SaveChangesAsync();
    return Results.NoContent();

});


app.MapDelete("/deleteEmployee/{idEmployee}", async (int idEmployee, OfficeDb db) =>
{
    if (await db.Employees.FindAsync(idEmployee) is Employee employee)
    {
        db.Employees.Remove(employee);
        await db.SaveChangesAsync();
        return Results.NotFound();
    }
    return Results.NotFound();
});


app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}