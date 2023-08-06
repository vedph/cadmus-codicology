using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Cadmus.Codicology.Parts;

/// <summary>
/// Location in a manuscript's sheet.
/// </summary>
public partial class CodLocation
{
    /// <summary>
    /// Gets or sets the endleaf type.
    /// </summary>
    public CodLocationEndleaf Endleaf { get; set; }

    /// <summary>
    /// Gets or sets the reference system. This starts with a-z or A-Z and
    /// then contains only letters a-z or A-Z, underscores, or digits 0-9.
    /// If not specified, the default reference system is assumed, which
    /// is determined by the convention adopted in the project.
    /// </summary>
    public string? S { get; set; }

    /// <summary>
    /// Gets or sets the ordinal sheet number (1-N).
    /// </summary>
    public int N { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether <see cref="N"/> is to be
    /// displayed as a Roman number.
    /// </summary>
    public bool Rmn { get; set; }

    /// <summary>
    /// Gets or sets an optional suffix after N (e.g. "bis").
    /// </summary>
    public string? Sfx { get; set; }

    /// <summary>
    /// Gets or sets recto/verso. If null, the location refers to both
    /// sides or we don't care about specifying one.
    /// </summary>
    public bool? V { get; set; }

    /// <summary>
    /// Gets or sets the column letter(s).
    /// </summary>
    public string? C { get; set; }

    /// <summary>
    /// Gets or sets the line number (1-N, 0 if not specified).
    /// </summary>
    public int L { get; set; }

    /// <summary>
    /// Gets or sets the word we refer to. By scholarly convention, this
    /// is a word picked from the line so that it cannot be ambiguous, i.e.
    /// confused with other instances of the same word in its line.
    /// </summary>
    public string? Word { get; set; }

    /// <summary>
    /// Converts to a parsable string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        StringBuilder sb = new();

        // endleaf open
        if (Endleaf != CodLocationEndleaf.None)
        {
            sb.Append(Endleaf switch
            {
                CodLocationEndleaf.FrontEndleaf => "(",
                CodLocationEndleaf.BackEndleaf => "(/",
                CodLocationEndleaf.FrontCover => "[",
                CodLocationEndleaf.BackCover => "[/",
                _ => ""
            });
        }

        // s:
        if (!string.IsNullOrEmpty(S)) sb.Append(S).Append(':');
        // rmn
        if (Rmn) sb.Append('^');
        // n
        if (N != 0) sb.Append(N);
        // sfx
        if (!string.IsNullOrEmpty(Sfx)) sb.Append(Sfx);
        // v or r
        if (V.HasValue) sb.Append(V.Value ? 'v' : 'r');
        // c
        if (!string.IsNullOrEmpty(C)) sb.Append(C);
        // .l
        if (L != 0) sb.Append('.').Append(L);
        // @word
        if (!string.IsNullOrEmpty(Word)) sb.Append('@').Append(Word);

        // endleaf close
        if (Endleaf != CodLocationEndleaf.None)
        {
            sb.Append(Endleaf switch
            {
                CodLocationEndleaf.FrontEndleaf => ")",
                CodLocationEndleaf.BackEndleaf => ")",
                CodLocationEndleaf.FrontCover => "]",
                CodLocationEndleaf.BackCover => "]",
                _ => ""
            });
        }

        return sb.ToString();
    }

    [GeneratedRegex(
        @"^(?<f>[\[\(]/?)?" +
        @"(?:(?<s>[a-zA-Z][_.a-zA-Z0-9]*):)?" +
        @"(?<rmn>\^)?" +
        @"(?<n>[0-9]*)" +
        @"(?:""(?<sfx>[^""]*)"")?" +
        @"(?<v>[rv])?" +
        @"(?<c>[a-q])?" +
        @"(?:\.(?<l>[0-9]+))?" +
        @"(?:@(?<word>[\p{L}]+))?" +
        @"[\)\]]?$")]
    private static partial Regex LocationRegex();

    /// <summary>
    /// Parses the specified text representing a <see cref="CodLocation"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>Location or null.</returns>
    public static CodLocation? Parse(string? text)
    {
        if (string.IsNullOrEmpty(text)) return null;

        Regex r = LocationRegex();
        Match m = r.Match(text);
        if (!m.Success) return null;

        return new CodLocation
        {
            Endleaf = m.Groups["f"].Value switch
            {
                "(" => CodLocationEndleaf.FrontEndleaf,
                "(/" => CodLocationEndleaf.BackEndleaf,
                "[" => CodLocationEndleaf.FrontCover,
                "[/" => CodLocationEndleaf.BackCover,
                _ => CodLocationEndleaf.None
            },
            S = m.Groups["s"].Value.Length == 0? null : m.Groups["s"].Value,
            Rmn = m.Groups["rmn"].Value.Length > 0,
            N = m.Groups["n"].Value.Length == 0
                ? 0
                : int.Parse(m.Groups["n"].Value, CultureInfo.InvariantCulture),
            Sfx = m.Groups["sfx"].Value.Length == 0? null : m.Groups["sfx"].Value,
            V = m.Groups["v"].Value.Length == 0
                ? null
                : m.Groups["v"].Value == "v",
            C = m.Groups["c"].Value.Length == 0? null : m.Groups["c"].Value,
            L = m.Groups["l"].Value.Length == 0
                ? 0
                : int.Parse(m.Groups["l"].Value, CultureInfo.InvariantCulture),
            Word = m.Groups["word"].Value.Length == 0
                ? null
                : m.Groups["word"].Value,
        };
    }
}

/// <summary>
/// Endleaf type for <see cref="CodLocation"/>.
/// </summary>
public enum CodLocationEndleaf
{
    /// <summary>No endleaf.</summary>
    None = 0,
    /// <summary>Front cover endleaf.</summary>
    FrontCover = 1,
    /// <summary>Front endleaf.</summary>
    FrontEndleaf = 2,
    /// <summary>Back endleaf.</summary>
    BackEndleaf = 3,
    /// <summary>Back cover endleaf.</summary>
    BackCover = 4,
}
