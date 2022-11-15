namespace MinimalHTML;
using Microsoft.AspNetCore.StaticFiles;
public static class MinimalHTMLExtensions
{

    public static void MapMinimalHTML(this WebApplication webApp)
    {
        var options = new List<MinimalHTMLOptions>
        {
            new MinimalHTMLOptions
            {
                searchOption = SearchOption.AllDirectories,
                fileExtension = "css",
                blacklist = new[] { "node_modules/" }
            },

            new MinimalHTMLOptions
            {
                searchOption = SearchOption.AllDirectories,
                fileExtension = "js",
                webRootPath = "node_modules/flowbite/dist/"
            },

            new MinimalHTMLOptions
            {
                searchOption = SearchOption.AllDirectories,
                fileExtension = "html"
            }
        };

        MapMinimalHTML(webApp, options.ToArray());
    }
    public static void MapMinimalHTML(this WebApplication webApp, params MinimalHTMLOptions[] options)
    {
        var ContentTypeMapper = new FileExtensionContentTypeProvider();


        foreach (var option in options)
        {
            var searchPath = webApp.Environment.WebRootPath;

            if (!string.IsNullOrWhiteSpace(option.webRootPath))
            {
                searchPath = Path.Combine(searchPath, option.webRootPath);
            }

            var names = Directory.EnumerateFiles(searchPath, "*." + option.fileExtension, option.searchOption);

            names = names.Select((s) => s.Replace("\\", "/"));

            foreach (string path in names)
            {
                if (option.blacklist.ToList().Find(s => path.Contains(s)) is not null) continue;

                var apiUri = Path.GetRelativePath(webApp.Environment.WebRootPath, path);
                apiUri = apiUri.Replace($".{option.fileExtension}", "");
                apiUri = apiUri.Replace("\\", "/");


                webApp.MapGet("/" + apiUri, () =>
                {
                    string html = File.ReadAllText(path);

                    if (!ContentTypeMapper.TryGetContentType(path, out string contentType))
                    {
                        contentType = "";
                    };

                    return Results.Content(html, contentType);
                });

            }
        }
    }
}
