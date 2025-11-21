using Bogus;
using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Antiquity.Chronology;
using Fusi.Tools;
using Fusi.Tools.Configuration;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Codicology.Parts;

/// <summary>
/// Seeder for <see cref="CodSheetLabelsPart"/>.
/// Tag: <c>seed.it.vedph.codicology.sheet-labels</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.codicology.sheet-labels")]
public sealed class CodSheetLabelsPartSeeder : PartSeederBase,
    IConfigurable<CodSheetLabelsPartSeederOptions>
{
    private CodSheetLabelsPartSeederOptions? _options;

    /// <summary>
    /// Configures the seeder with the specified options.
    /// </summary>
    /// <param name="options">The options to use for configuring the seeder.</param>
    /// <exception cref="ArgumentNullException">options</exception>
    public void Configure(CodSheetLabelsPartSeederOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    /// <summary>
    /// Creates and seeds a new part.
    /// </summary>
    /// <param name="item">The item this part should belong to.</param>
    /// <param name="roleId">The optional part role ID.</param>
    /// <param name="factory">The part seeder factory. This is used
    /// for layer parts, which need to seed a set of fragments.</param>
    /// <returns>A new part.</returns>
    /// <exception cref="ArgumentNullException">item or factory</exception>
    public override IPart GetPart(IItem item, string? roleId,
        PartSeederFactory? factory)
    {
        ArgumentNullException.ThrowIfNull(item);

        CodSheetLabelsPart part = new();
        SetPartMetadata(part, roleId, item);

        IList<string> systems = _options?.Systems ?? ["roman", "arabic"];
        IList<string> techniques = _options?.Techniques ?? ["ink", "lapis"];
        IList<string> positions = _options?.Positions ?? ["mse", "msc"];
        IList<string> colors = _options?.Colors ?? ["red", "dark-brown"];

        // n.alpha and n.beta
        for (int i = 0; i < 2; i++)
        {
            part.NDefinitions.Add(new Faker<CodSheetNColumnDefinition>()
                .RuleFor(d => d.Id, i == 0? "n.alpha" : "n.beta")
                .RuleFor(d => d.IsPagination, true)
                .RuleFor(d => d.IsByScribe, i == 0)
                .RuleFor(d => d.System, f => f.PickRandom(systems))
                .RuleFor(d => d.Technique, f => f.PickRandom(techniques))
                .RuleFor(d => d.Position, f => f.PickRandom(positions))
                .RuleFor(d => d.Colors, f => [f.PickRandom(colors)])
                .RuleFor(d => d.Date, HistoricalDate.Parse($"{1300 + i} AD"))
                .Generate());
        }

        int div = new Random().Next(3, 14), a = 0, b = 0;
        for (int i = 0; i < 16; i++)
        {
            int n = i / 2 + 1;
            bool v = i % 2 != 0;
            CodSheetRow row = new()
            {
                Id = n + (v ? "v" : "r")
            };
            // q
            row.Columns.Add(new CodSheetColumn
            {
                Id = "q",
                Value = $"{i / 8 + 1}.{n - (i / 8 * 4)}/4",
            });
            // n.alpha
            if (i < div)
            {
                row.Columns.Add(new CodSheetColumn
                {
                    Id = "n.alpha",
                    Value = RomanNumber.ToRoman(++a)
                });
            }
            // n.beta
            else
            {
                row.Columns.Add(new CodSheetColumn
                {
                    Id = "n.beta",
                    Value = $"{++b}"
                });
            }
            part.Rows.Add(row);
        }

        return part;
    }
}

/// <summary>
/// Options for <see cref="CodSheetLabelsPartSeeder"/>.
/// </summary>
public class CodSheetLabelsPartSeederOptions
{
    /// <summary>
    /// System IDs to pick from.
    /// </summary>
    public List<string>? Systems { get; set; }

    /// <summary>
    /// Technique IDs to pick from.
    /// </summary>
    public List<string>? Techniques { get; set; }

    /// <summary>
    /// Position IDs to pick from.
    /// </summary>
    public List<string>? Positions { get; set; }

    /// <summary>
    /// System IDs to pick from.
    /// </summary>
    public List<string>? Colors { get; set; }
}
