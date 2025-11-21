using Bogus;
using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Antiquity.Chronology;
using Fusi.Tools.Configuration;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Codicology.Parts;

/// <summary>
/// Seeder for <see cref="CodEditsPart"/>.
/// Tag: <c>seed.it.vedph.codicology.edits</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.codicology.edits")]
public sealed class CodEditsPartSeeder : PartSeederBase,
    IConfigurable<CodEditsPartSeederOptions>
{
    private CodEditsPartSeederOptions? _options;

    /// <summary>
    /// Configures the seeder with the specified options.
    /// </summary>
    /// <param name="options">The options to use for configuring the seeder.
    /// </param>
    /// <exception cref="ArgumentNullException">options.</exception>
    public void Configure(CodEditsPartSeederOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    private IList<CodEdit> GetEdits(int count)
    {
        List<CodEdit> edits = [];
        IList<string> types = _options?.Types ?? ["correction", "comment"];
        IList<string> techniques = _options?.Techniques ?? ["ink", "lapis"];
        IList<string> languages = _options?.Languages ?? ["la", "grc"];
        IList<string> colors = _options?.Colors ?? ["black", "red"];

        for (int n = 1; n <= count; n++)
        {
            edits.Add(new Faker<CodEdit>()
                .RuleFor(p => p.Eid, f => f.Lorem.Word())
                .RuleFor(p => p.Type, f => f.PickRandom(types))
                .RuleFor(p => p.Techniques, f => [f.PickRandom(techniques)])
                .RuleFor(p => p.Language, f => f.PickRandom(languages))
                .RuleFor(p => p.Colors, f => [ f.PickRandom(colors) ])
                .RuleFor(p => p.Ranges,
                    f => SeedHelper.GetLocationRanges(f.Random.Number(1, 3)))
                .RuleFor(p => p.Date, HistoricalDate.Parse($"{1400 + n} AD"))
                .RuleFor(p => p.Description,
                    f => f.PickRandom(f.Lorem.Sentence(), null))
                .RuleFor(p => p.Text, f=> f.Lorem.Sentence())
                .RuleFor(p => p.References,
                    f => SeedHelper.GetDocReferences(f.Random.Number(1, 3)))
                .Generate());
        }
        return edits;
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

        CodEditsPart part = new Faker<CodEditsPart>()
           .RuleFor(p => p.Edits, f => GetEdits(f.Random.Number(1, 3)))
           .Generate();
        SetPartMetadata(part, roleId, item);

        return part;
    }
}

/// <summary>
/// Options for <see cref="CodEditsPartSeeder"/>.
/// </summary>
public class CodEditsPartSeederOptions
{
    /// <summary>
    /// The type IDs to pick from.
    /// </summary>
    public List<string>? Types { get; set; }

    /// <summary>
    /// The technique IDs to pick from.
    /// </summary>
    public List<string>? Techniques { get; set; }

    /// <summary>
    /// The language IDs to pick from.
    /// </summary>
    public List<string>? Languages { get; set; }

    /// <summary>
    /// The color IDs to pick from.
    /// </summary>
    public List<string>? Colors { get; set; }
}