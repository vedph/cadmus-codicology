using Bogus;
using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Tools.Configuration;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Codicology.Parts;

/// <summary>
/// Manuscript's shelfmarks part seeder.
/// Tag: <c>seed.it.vedph.codicology.shelfmarks</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.codicology.shelfmarks")]
public sealed class CodShelfmarksPartSeeder : PartSeederBase,
    IConfigurable<CodShelfmarksPartSeederOptions>
{
    private CodShelfmarksPartSeederOptions? _options;

    /// <summary>
    /// Configures the seeder with the specified options.
    /// </summary>
    /// <param name="options">The options to use for configuring the seeder.</param>
    /// <exception cref="ArgumentNullException">options</exception>
    public void Configure(CodShelfmarksPartSeederOptions options)
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
        ArgumentNullException.ThrowIfNull(factory);

        CodShelfmarksPart part = new();
        SetPartMetadata(part, roleId, item);

        for (int n = 1; n <= Randomizer.Seed.Next(1, 3 + 1); n++)
        {
            part.Shelfmarks.Add(new Faker<CodShelfmark>()
                .RuleFor(s => s.City, f => _options?.Cities?.Count > 0
                    ? f.PickRandom(_options.Cities) : f.Address.City())
                .RuleFor(s => s.Library, f => _options?.Libraries?.Count > 0
                    ? f.PickRandom(_options.Libraries) : f.Lorem.Word())
                .RuleFor(s => s.Fund, f => f.Lorem.Sentence(1, 3))
                .RuleFor(s => s.Location, f => f.Random.AlphaNumeric(8))
                .Generate());
        }

        return part;
    }
}

/// <summary>
/// Options for <see cref="CodShelfmarksPartSeeder"/>.
/// </summary>
public class CodShelfmarksPartSeederOptions
{
    /// <summary>
    /// City IDs to pick from.
    /// </summary>
    public List<string>? Cities { get; set; }

    /// <summary>
    /// Library IDs to pick from.
    /// </summary>
    public List<string>? Libraries { get; set; }
}
