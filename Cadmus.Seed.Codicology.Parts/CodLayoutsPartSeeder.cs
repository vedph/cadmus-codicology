﻿using Bogus;
using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Cadmus.Refs.Bricks;
using Fusi.Tools.Configuration;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Codicology.Parts;

/// <summary>
/// Manuscript's dimensions part seeder.
/// <para>Tag: <c>seed.it.vedph.codicology.layouts</c>.</para>
/// </summary>
[Tag("seed.it.vedph.codicology.layouts")]
public sealed class CodLayoutsPartSeeder : PartSeederBase
{
    private static List<DecoratedCount> GetCounts(int count)
    {
        List<DecoratedCount> counts = [];

        for (int n = 1; n <= count; n++)
        {
            counts.Add(new Faker<DecoratedCount>()
                .RuleFor(d => d.Id, f => f.Lorem.Word())
                .RuleFor(d => d.Value, f => f.Random.Number(1, 100))
                .RuleFor(d => d.Note,
                    f => f.PickRandom(null, f.Lorem.Sentence()))
                .Generate());
        }

        return counts;
    }

    private static CodLayout GetLayout()
    {
        return new Faker<CodLayout>()
            .RuleFor(p => p.Sample,
                f => new CodLocation
                {
                    N = f.Random.Number(1, 60),
                    S = f.Random.Bool() ?
                        "v" : "r",
                })
            .RuleFor(p => p.Ranges, SeedHelper.GetLocationRanges(1))
            .RuleFor(p => p.Dimensions,
                f => SeedHelper.GetDimensions(f.Random.Number(1, 3)))
            .RuleFor(p => p.RulingTechniques,
                f => [f.PickRandom("dry", "color")])
            .RuleFor(p => p.Derolez, f => f.Lorem.Word())
            .RuleFor(p => p.Pricking, f => f.Lorem.Word())
            .RuleFor(p => p.ColumnCount, f => f.Random.Number(1, 4))
            .RuleFor(p => p.Counts, f => GetCounts(f.Random.Number(1, 3)))
            .RuleFor(p => p.Tag, f => f.Random.Bool(0.25f) ? "tag" : null)
            .RuleFor(p => p.Note, f => f.Random.Bool(0.25f)
                ? f.Lorem.Sentence() : null)
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

        CodLayoutsPart part = new();

        int count = Randomizer.Seed.Next(1, 3);
        for (int n = 1; n <= count; n++) part.Layouts.Add(GetLayout());

        SetPartMetadata(part, roleId, item);

        return part;
    }
}
