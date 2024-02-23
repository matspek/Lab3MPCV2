using Lab3MPCV.Data;
using Lab3MPCV2.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var db = new MongoCRUD("CVDB");
builder.Services.AddSingleton(db);

//Create instance of database seeder


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/cv/{name}", async (string name, MongoCRUD datebase) =>
{
    var CV = await datebase.GetPerson("CVBD2", name);
    if (CV != null)
    {
        return Results.Ok(CV);
    }

    return Results.NotFound();
    
});

app.MapPost("/work", async (string name, WorkExperience workexperice, MongoCRUD database) =>
{
    await database.AddWorkExperience("CVDB2", name, workexperice);
    return Results.Ok();
});

app.MapPost("/education", async (string name, Education eudcation, MongoCRUD database) =>
{
    await database.AddEducation("CVBD2", name, eudcation);
    return Results.Ok();
});

app.MapPost("/skill/{name}", async (string name, Kompetenser kompetenser, MongoCRUD database) =>
{
    await database.AddSkill("CVBD2", name, kompetenser);
    return Results.Ok();
});

app.MapPut("/jobtitle/{name}/{jobtitle}", async (string name, string jobtitle, MongoCRUD database) =>
{
    await database.UpdateJobTitle("CVBD2", name, jobtitle);
    return Results.Ok();
});

app.MapDelete("/work/{name}/{role}", async (string name, string role, MongoCRUD database) =>
{
    await database.DeteteWork("CVBD2", name, role);
    return Results.Ok();
});



app.Run();

