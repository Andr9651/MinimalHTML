namespace MinimalHTML;

public class MinimalHTMLOptions
{
    public SearchOption SearchOption { get; set; } = SearchOption.AllDirectories;

    public string FileExtension { get; set; } = "html";

    public string WebRootPath { get; set; } = "";

    public string[] Blacklist { get; set; } = { };
}