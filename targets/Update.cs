using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Targets;

public static partial class Update
{
    private const string Prefix = "// UPSTREAM: ";

    public static async Task Upstream()
    {
        var currentDir = Path.GetDirectoryName(GetThisFilePath())!;
        var srcDir = Path.GetFullPath(Path.Combine(currentDir, "..", "src"));
        var nuspec = XDocument.Load(Path.Combine(srcDir, "LambdaTale", "LambdaTale.nuspec"));
        var xUnitVersionRange = nuspec.XPathSelectElement("//dependency[@id=\"xunit.core\"]")!.Attribute("version")!.Value;
        var xUnitVersion = Version.Parse(xUnitVersionRange[1..xUnitVersionRange.IndexOf(',', StringComparison.Ordinal)]).ToString();
        using var httpClient = new HttpClient();
        foreach (var upstreamSource in Directory.EnumerateFiles(Path.Combine(srcDir, "LambdaTale.Execution", "Upstream"), "*.cs"))
        {
            var header = await File.ReadLinesAsync(upstreamSource).FirstAsync();
            var newUpstream = VersionedUpstream().Replace(header, match =>
            {
                var v = match.Groups["version"];
                var start = v.Index - match.Index;
                return match.Value.Remove(start, v.Length).Insert(start, xUnitVersion);
            })[Prefix.Length..];

            await using var newContent = await httpClient.GetStreamAsync(new Uri(newUpstream));
            await using var writer = new StreamWriter(upstreamSource, Encoding.UTF8,
                new FileStreamOptions { Mode = FileMode.Truncate, Access = FileAccess.Write });
            await writer.WriteLineAsync(Prefix + newUpstream);
            await writer.FlushAsync();
            await newContent.CopyToAsync(writer.BaseStream);
        }
    }

    // UPSTREAM: https://raw.githubusercontent.com/xunit/assert.xunit/2.7.0/Sdk/ArgumentFormatter.cs
    [GeneratedRegex(@"https://raw\.githubusercontent\.com/[^/]+/[^/]+/(?<version>[^/]+)/.*")]
    private static partial Regex VersionedUpstream();

    private static string GetThisFilePath([CallerFilePath] string path = null) => path;
}
