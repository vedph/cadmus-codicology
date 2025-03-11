using Bogus;
using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Tools.Configuration;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Codicology.Parts;

/// <summary>
/// Manuscript's decorations description seeder.
/// <para>Tag: <c>seed.it.vedph.codicology.decorations</c>.</para>
/// </summary>
[Tag("seed.it.vedph.codicology.decorations")]
public sealed class CodDecorationsPartSeeder : PartSeederBase
{
    private readonly string[] _flags;
    private readonly string[] _colors;

    public CodDecorationsPartSeeder()
    {
        _flags = new[] { "original", "unitary", "complete" };
        _colors = new[] { "red", "green", "blue", "gold" };
    }

    private List<string> GetColors()
    {
        List<string> colors = [];
        int count = Randomizer.Seed.Next(1, 3 + 1);

        for (int n = 1; n <= count; n++)
            colors.Add(_colors[Randomizer.Seed.Next(0, _colors.Length)]);
        return colors;
    }

    private List<CodDecorationElement> GetElements(int count, Faker faker)
    {
        List<CodDecorationElement> elements =
            [];
        string[] typologies = new[] { "frieze", "frame" };
        string[] gildings = new[] { "leaf", "powder" };
        string[] positions = new[]
        {
            "ill.full-page", "ill.margin-l", "ill.margin-r"
        };

        for (int n = 1; n <= count; n++)
        {
            elements.Add(new CodDecorationElement
            {
                Key = n == 1 ? "e1" : null,
                ParentKey = n == 2 ? "e1" : null,
                Type = "ill",
                Flags = [.. new[] { faker.PickRandom(_flags) }],
                Ranges = SeedHelper.GetLocationRanges(n % 2 == 0? 1 : 2),
                Typologies = [.. new[] { faker.PickRandom(typologies) }],
                Subject = faker.Lorem.Word(),
                Colors = GetColors(),
                Gildings = [faker.PickRandom(gildings)],
                Techniques = [faker.PickRandom("ink", "watercolor")],
                Tools = [faker.PickRandom("pen", "brush")],
                Positions = [faker.PickRandom(positions)],
                LineHeight = faker.Random.Short(1, 10),
                TextRelation = faker.Lorem.Sentence(),
                Description = faker.Lorem.Sentence(),
                Note = faker.Random.Bool(0.25F) ? faker.Lorem.Sentence() : null
            });
        }

        return elements;
    }

    private static CodDecorationArtist GetArtist()
    {
        return new Faker<CodDecorationArtist>()
            .RuleFor(a => a.Eid, f => f.Lorem.Word())
            .RuleFor(a => a.Type, f => f.PickRandom("painter", "illuminator"))
            .RuleFor(a => a.Name, f => f.Person.FullName)
            .RuleFor(a => a.Styles,
            [
                new CodDecorationArtistStyle
                {
                    // TODO: use thesaurus
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

        int count = Randomizer.Seed.Next(1, 3);
        for (int n = 1; n <= count; n++)
        {
            part.Decorations.Add(new Faker<CodDecoration>()
                .RuleFor(d => d.Eid, f => "d" + f.UniqueIndex)
                .RuleFor(d => d.Name, f => f.Lorem.Word())
                .RuleFor(d => d.Flags,
                    f => [.. new[] { f.PickRandom(_flags) }])
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
