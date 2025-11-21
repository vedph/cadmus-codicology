using Bogus;
using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Tools.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cadmus.Seed.Codicology.Parts;

/// <summary>
/// Manuscript's decorations description seeder.
/// <para>Tag: <c>seed.it.vedph.codicology.decorations</c>.</para>
/// </summary>
[Tag("seed.it.vedph.codicology.decorations")]
public sealed class CodDecorationsPartSeeder : PartSeederBase,
    IConfigurable<CodDecorationsPartSeederOptions>
{
    private CodDecorationsPartSeederOptions? _options;
    private readonly IList<string> _flags;
    private readonly IList<string> _colors;

    public CodDecorationsPartSeeder()
    {
        _flags = ["not-original", "incomplete"];
        _colors = ["red", "blue", "other"];
    }

    /// <summary>
    /// Configures the seeder with the specified options.
    /// </summary>
    /// <param name="options">The options to use for configuring the seeder.</param>
    /// <exception cref="ArgumentNullException">options</exception>
    public void Configure(CodDecorationsPartSeederOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    private List<string> GetColors()
    {
        List<string> colors = [];
        IList<string> availableColors = _options?.Colors?.Count > 0
            ? _options.Colors: _colors;
        int count = Randomizer.Seed.Next(1, 3 + 1);

        for (int n = 1; n <= count; n++)
        {
            colors.Add(availableColors[Randomizer.Seed.Next(
                0, availableColors.Count)]);
        }

        return colors;
    }

    private List<CodDecorationElement> GetElements(int count, Faker faker)
    {
        List<CodDecorationElement> elements = [];
        IList<string> gildings = _options?.ElementGildings?.Count > 0
            ? _options.ElementGildings : ["leaf", "powder"];
        IList<string> techniques = _options?.ElementTechniques?.Count > 0
            ? _options.ElementTechniques : ["ink", "watercolor"];
        IList<string> tools = _options?.ElementTools?.Count > 0
            ? _options.ElementTools : ["pen", "brush"];

        for (int n = 1; n <= count; n++)
        {
            elements.Add(new CodDecorationElement
            {
                Key = n == 1 ? "e1" : null,
                ParentKey = n == 2 ? "e1" : null,
                Type = _options?.ElementTypes?.Count > 0
                    ? _options.ElementTypes[0] : "ill",
                Flags = [.. new[] { faker.PickRandom(_options?.Flags?.Count > 0
                    ? _options.Flags : _flags) }],
                Ranges = SeedHelper.GetLocationRanges(n % 2 == 0? 1 : 2),
                Typologies = [],
                Subject = faker.Lorem.Word(),
                Colors = GetColors(),
                Gildings = [faker.PickRandom(gildings)],
                Techniques = [faker.PickRandom(techniques)],
                Tools = [faker.PickRandom(tools)],
                Positions = [],
                LineHeight = faker.Random.Short(1, 10),
                TextRelation = faker.Lorem.Sentence(),
                Description = faker.Lorem.Sentence(),
                Note = faker.Random.Bool(0.25F) ? faker.Lorem.Sentence() : null
            });
        }

        return elements;
    }

    private CodDecorationArtist GetArtist()
    {
        return new Faker<CodDecorationArtist>()
            .RuleFor(a => a.Eid, f => f.Lorem.Word())
            .RuleFor(a => a.Type, f => f.PickRandom(
                _options?.ArtistTypes?.Count > 0
                ? _options.ArtistTypes : ["workshop", "illuminator"]))
            .RuleFor(a => a.Name, f => f.Person.FullName)
            .RuleFor(a => a.Styles,
            [
                new CodDecorationArtistStyle
                {
                    Name = "style",
                    Chronotope = SeedHelper.GetAssertedChronotopes(1)[0]
                }
            ])
            .Generate();
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
        ArgumentNullException.ThrowIfNull(factory);

        CodDecorationsPart part = new();
        SetPartMetadata(part, roleId, item);
        IList<string> flags = _options?.Flags?.Count > 0
            ? _options.Flags : _flags;

        int count = Randomizer.Seed.Next(1, 3);
        for (int n = 1; n <= count; n++)
        {
            part.Decorations.Add(new Faker<CodDecoration>()
                .RuleFor(d => d.Eid, f => "d" + f.UniqueIndex)
                .RuleFor(d => d.Name, f => f.Lorem.Word())
                .RuleFor(d => d.Flags,
                    f => [.. new[] { f.PickRandom(flags) }])
                .RuleFor(d => d.Chronotopes, SeedHelper.GetAssertedChronotopes(1))
                .RuleFor(d => d.Artists,
                    [.. new[] { GetArtist() }])
                .RuleFor(d => d.Note, f => f.Random.Bool(0.25f)
                    ? f.Lorem.Sentence() : null)
                .RuleFor(d => d.References,
                    f => SeedHelper.GetDocReferences(f.Random.Number(1, 3)))
                .RuleFor(d => d.Elements, f => GetElements(f.Random.Number(1, 3), f))
                .Generate());
        }

        return part;
    }
}

/// <summary>
/// Options for <see cref="CodDecorationsPart"/>.
/// </summary>
public class CodDecorationsPartSeederOptions
{
    /// <summary>
    /// The flag IDs to pick from.
    /// </summary>
    public List<string>? Flags { get; set; }

    /// <summary>
    /// The color IDs to pick from.
    /// </summary>
    public List<string>? Colors { get; set; }

    /// <summary>
    /// The element type IDs to pick from.
    /// </summary>
    public List<string>? ElementTypes { get; set; }

    /// <summary>
    /// The gilding IDs to pick from.
    /// </summary>
    public List<string>? ElementGildings { get; set; }

    /// <summary>
    /// The technique IDs to pick from.
    /// </summary>
    public List<string>? ElementTechniques { get; set; }

    /// <summary>
    /// The tool IDs to pick from.
    /// </summary>
    public List<string>? ElementTools { get; set; }

    /// <summary>
    /// The artist type IDs to pick from.
    /// </summary>
    public List<string>? ArtistTypes { get; set; }
}