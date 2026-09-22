using Bogus;
using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Tools.Configuration;
using System;

namespace Cadmus.Seed.Codicology.Parts;

/// <summary>
/// Seeder for <see cref="CodLocationRangesPart"/>.
/// Tag: <c>seed.it.vedph.codicology.location-ranges</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.codicology.location-ranges")]
public sealed class CodLocationRangesPartSeeder : PartSeederBase
{
    /// <summary>
    /// Creates and seeds a new part.
    /// </summary>
    /// <param name="item">The item this part should belong to.</param>
    /// <param name="roleId">The optional part role ID.</param>
    /// <param name="factory">The part seeder factory. This is used
    /// for layer parts, which need to seed a set of fragments.</param>
    /// <returns>A new part or null.</returns>
    /// <exception cref="ArgumentNullException">item or factory</exception>
    public override IPart? GetPart(IItem item, string? roleId,
        PartSeederFactory? factory)
    {
        ArgumentNullException.ThrowIfNull(item);

        CodLocationRangesPart part = new Faker<CodLocationRangesPart>()
           .RuleFor(p => p.Ranges,
                f => SeedHelper.GetLocationRanges(f.Random.Number(1, 2)))
           .RuleFor(p => p.Note,
                f => f.Random.Bool(0.25f) ? f.Lorem.Sentence() : null)
           .Generate();

        SetPartMetadata(part, roleId, item);

        return part;
    }
}
