using MinimalHTML;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

MinimalHTMLOptions[] options = new[]
{
    new MinimalHTMLOptions
    {
        SearchOption = SearchOption.AllDirectories,
        FileExtension = "css"
    },

    new MinimalHTMLOptions
    {
        SearchOption = SearchOption.AllDirectories,
        FileExtension = "js"
    },

    new MinimalHTMLOptions
    {
        SearchOption = SearchOption.AllDirectories,
        Blacklist = new[] {"hidden"},
        HideFileExtenstionInURL = true,
        FileExtension = "html"
    }
};

app.MapMinimalHTML(options);

app.Run();