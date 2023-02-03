using Bogus;
using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Tools.Configuration;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Codicology.Parts;

/// <summary>
/// Seeder for <see cref="CodBindingsPart"/> part.
/// Tag: <c>seed.it.vedph.codicology.bindings</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.codicology.bindings")]
public sealed class CodBindingsPartSeeder : PartSeederBase
{
    private static List<CodBinding> GetBindings(int count)
    {
        List<CodBinding> bindings = new();
        for (int n = 1; n <= count; n++)
        {
            bindings.Add(new Faker<CodBinding>()
                .RuleFor(b => b.Tag, f => f.PickRandom(null, "tag"))
                .RuleFor(b => b.CoverMaterial,
                    f => f.PickRandom("skin", "velvet"))
                .RuleFor(b => b.BoardMaterial,
                    f => f.PickRandom("wood", "card"))
                .RuleFor(b => b.Size, SeedHelper.GetPhysicalSize())
                .RuleFor(b => b.Chronotope, SeedHelper.GetAssertedChronotopes(1)[0])
                .RuleFor(b => b.Description, f => f.Lorem.Sentence())
                .Generate());
        }
        return bindings;
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
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        CodBindingsPart part = new Faker<CodBindingsPart>()
           .RuleFor(p => p.Bindings, f => GetBindings(f.Random.Number(1, 3)))
           .Generate();
        SetPartMetadata(part, roleId, item);

        return part;
    }
}
