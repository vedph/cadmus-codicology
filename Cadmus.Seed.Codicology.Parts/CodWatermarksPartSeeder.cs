﻿using Bogus;
using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Tools.Configuration;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Codicology.Parts;

/// <summary>
/// Seeder for <see cref="CodWatermarksPart"/>.
/// Tag: <c>seed.it.vedph.codicology.watermarks</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.codicology.watermarks")]
public class CodWatermarksPartSeeder : PartSeederBase
{
    private static IList<CodWatermark> GetWatermarks(int count)
    {
        List<CodWatermark> watermarks = [];
        IList<CodLocationRange> ranges = SeedHelper.GetLocationRanges(count);

        for (int n = 1; n <= count; n++)
        {
            watermarks.Add(new Faker<CodWatermark>()
                .RuleFor(w => w.Name, f => f.Lorem.Word())
                .RuleFor(w => w.SampleRange, ranges[n - 1])
                .RuleFor(w => w.Ranges,
                    [ranges[n - 1]])
                .RuleFor(w => w.Ids,
                    f => SeedHelper.GetAssertedCompositeIds(f.Random.Number(1, 3)))
                .Generate());
        }
        return watermarks;
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

        CodWatermarksPart part = new Faker<CodWatermarksPart>()
           .RuleFor(p => p.Watermarks, f => GetWatermarks(f.Random.Number(1, 3)))
           .Generate();
        SetPartMetadata(part, roleId, item);

        return part;
    }
}
