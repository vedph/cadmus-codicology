using Bogus;
using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Tools.Configuration;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Codicology.Parts;

/// <summary>
/// Seeder for <see cref="CodMaterialDscPart"/>.
/// Tag: <c>seed.it.vedph.codicology.material-dsc</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.codicology.material-dsc")]
public sealed class CodMaterialDscPartSeeder : PartSeederBase,
    IConfigurable<CodMaterialDscPartSeederOptions>
{
    private CodMaterialDscPartSeederOptions? _options;

    /// <summary>
    /// Configures the seeder with the specified options.
    /// </summary>
    /// <param name="options">The options to use for configuring the seeder.</param>
    /// <exception cref="ArgumentNullException">options</exception>
    public void Configure(CodMaterialDscPartSeederOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    private List<CodUnit> GetUnits(int count)
    {
        List<CodUnit> units = [];
        IList<string> materials = _options?.Materials ?? ["parchment", "paper"];
        IList<string> states = _options?.States ?? ["s1", "s2"];

        for (int n = 1; n <= count; n++)
        {
            units.Add(new Faker<CodUnit>()
                .RuleFor(u => u.Eid, f => f.Lorem.Word())
                .RuleFor(u => u.Material, f => f.PickRandom(materials))
                .RuleFor(u => u.State, f => f.PickRandom(states))
                .RuleFor(u => u.Ranges, SeedHelper.GetLocationRanges(1))
                .RuleFor(u => u.Chronotopes,
                    f => SeedHelper.GetAssertedChronotopes(f.Random.Number(1, 2)))
                .RuleFor(u => u.NoGregory, f => f.Random.Bool(0.25f))
                .RuleFor(u => u.Note,
                    f => f.Random.Bool(0.25f)? f.Lorem.Sentence() : null)
                .Generate());
        }
        return units;
    }

    private static List<CodPalimpsest> GetPalimpsests(int count)
    {
        List<CodPalimpsest> palimpsests = [];
        for (int n = 1; n <= count; n++)
        {
            palimpsests.Add(new Faker<CodPalimpsest>()
                .RuleFor(p => p.Ranges, SeedHelper.GetLocationRanges(1))
                .RuleFor(p => p.Chronotope, SeedHelper.GetAssertedChronotopes(1)[0])
                .RuleFor(p => p.Note,
                    f => f.Random.Bool(0.25f)? f.Lorem.Sentence() : null)
                .Generate());
        }
        return palimpsests;
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

        CodMaterialDscPart part = new Faker<CodMaterialDscPart>()
           .RuleFor(p => p.Units, f => GetUnits(f.Random.Number(2, 5)))
           .RuleFor(p => p.Palimpsests, f => GetPalimpsests(f.Random.Number(1, 2)))
           .Generate();
        SetPartMetadata(part, roleId, item);

        return part;
    }
}

/// <summary>
/// Options for <see cref="CodMaterialDscPartSeeder"/>.
/// </summary>
public class CodMaterialDscPartSeederOptions
{
    /// <summary>
    /// The material IDs to pick from.
    /// </summary>
    public List<string>? Materials { get; set; }

    /// <summary>
    /// The state IDs to pick from.
    /// </summary>
    public List<string>? States { get; set; }
}
