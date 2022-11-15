using Microsoft.AspNetCore.StaticFiles;
using MinimalWebsite.MinimalHTML;

namespace MinimalHTML;

public static class MinimalHTMLExtensions
{

    public static void MapMinimalHTML(this WebApplication webApp)
    {
        var options = new List<MinimalHTMLOptions>
        {
            new MinimalHTMLOptions
            {
                SearchOption = SearchOption.AllDirectories,
                FileExtension = "css",
                Blacklist = new[] { "node_modules/" }
            },

            new MinimalHTMLOptions
            {
                SearchOption = SearchOption.AllDirectories,
                FileExtension = "js",
                WebRootPath = "node_modules/flowbite/dist/"
            },

            new MinimalHTMLOptions
            {
                SearchOption = SearchOption.AllDirectories,
                FileExtension = "html"
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

            if (!string.IsNullOrWhiteSpace(option.WebRootPath))
            {
                searchPath = Path.Combine(searchPath, option.WebRootPath);
            }

            var names = Directory.EnumerateFiles(searchPath, "*." + option.FileExtension, option.SearchOption);

            names = names.Select((s) => s.Replace("\\", "/"));

            foreach (string path in names)
            {
                if (option.Blacklist.ToList().Find(s => path.Contains(s)) is not null) continue;

                var apiUri = Path.GetRelativePath(webApp.Environment.WebRootPath, path);
                apiUri = apiUri.Replace($".{option.FileExtension}", "");
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
