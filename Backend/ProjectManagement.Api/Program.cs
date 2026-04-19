using ProjectManagement.Api.Extensions; 

var builder = WebApplication.CreateBuilder(args);

// 1. ÅÖÇİÉ ÇáÎÏãÇÊ ÇáÃÓÇÓíÉ
builder.Services.AddControllers();

// 2. ÇÓÊÎÏÇã ÇáÜ Extension ÇáĞí ÃäÔÃäÇå áÊÓÌíá Swagger
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

// 3. ÅÚÏÇÏ ÇáÜ Pipeline (Middlewares)
if (app.Environment.IsDevelopment())
{
	// ÇÓÊÎÏÇã ÇáÜ Extension áÊİÚíá æÇÌåÉ ÓæÇÌíÑ
	app.UseSwaggerDocumentation();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();