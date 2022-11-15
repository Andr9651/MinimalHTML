public class MinimalHTMLOptions
{
    public SearchOption searchOption { get; set; } = SearchOption.AllDirectories;

    public string fileExtension { get; set; } = "html";

    public string webRootPath { get; set; } = "";

    public string[] blacklist { get; set; } = {};
}