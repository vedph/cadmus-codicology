﻿using Bogus;
using Cadmus.Codicology.Parts;
using Cadmus.Mat.Bricks;
using Cadmus.Refs.Bricks;
using Fusi.Antiquity.Chronology;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Codicology.Parts;

internal static class SeedHelper
{
    /// <summary>
    /// Truncates the specified value to the specified number of decimals.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="decimals">The decimals.</param>
    /// <returns>Truncated value.</returns>
    private static float Truncate(float value, int decimals)
    {
        double factor = Math.Pow(10, decimals);
        return (float)((float)Math.Truncate(factor * value) / factor);
    }

    public static List<CodLocationRange> GetLocationRanges(int count)
    {
        List<CodLocationRange> ranges = [];
        for (int n = 1; n <= count; n++)
        {
            ranges.Add(new CodLocationRange
            {
                Start = new CodLocation { N = n * 3 },
                End = new CodLocation { N = (n * 3) + 2 }
            });
        }
        return ranges;
    }

    public static CodLocationRange GetLocationRange()
        => GetLocationRanges(1)[0];

    public static CodLocation GetLocation()
    {
        return new Faker<CodLocation>()
            .RuleFor(l => l.N, f => f.Random.Number(1, 50))
            .RuleFor(l => l.V, f => f.Random.Bool())
            .Generate();
    }

    /// <summary>
    /// Gets a random number of document references.
    /// </summary>
    /// <param name="count">The number of references to get.</param>
    /// <returns>References.</returns>
    public static List<DocReference> GetDocReferences(int count)
    {
        List<DocReference> refs = [];

        for (int n = 1; n <= count; n++)
        {
            refs.Add(new Faker<DocReference>()
                .RuleFor(r => r.Tag, f => f.PickRandom(null, "tag"))
                .RuleFor(r => r.Type, "biblio")
                .RuleFor(r => r.Citation,
                    f => f.Person.LastName + " " + f.Date.Past(10).Year)
                .RuleFor(r => r.Note, f => f.Lorem.Sentence())
                .Generate());
        }

        return refs;
    }

    public static List<ExternalId> GetExternalIds(int count)
    {
        List<ExternalId> ids = [];

        for (int n = 1; n <= count; n++)
        {
            ids.Add(new Faker<ExternalId>()
                .RuleFor(r => r.Tag, f => f.PickRandom(null, "tag"))
                .RuleFor(r => r.Scope, f => f.Lorem.Word())
                .RuleFor(r => r.Value, f => f.Internet.Url())
                .Generate());
        }

        return ids;
    }

    public static List<AssertedId> GetAssertedIds(int count)
    {
        List<AssertedId> ids = [];

        for (int n = 1; n <= count; n++)
        {
            ids.Add(new Faker<AssertedId>()
                .RuleFor(r => r.Tag, f => f.PickRandom(null, "tag"))
                .RuleFor(r => r.Scope, f => f.Lorem.Word())
                .RuleFor(r => r.Value, f => f.Internet.Url())
                .RuleFor(r => r.Assertion, GetAssertion())
                .Generate());
        }

        return ids;
    }

    public static List<AssertedCompositeId> GetAssertedCompositeIds(int count)
    {
        List<AssertedCompositeId> ids = [];

        for (int n = 1; n <= count; n++)
        {
            ids.Add(new Faker<AssertedCompositeId>()
                .RuleFor(r => r.Tag, f => f.PickRandom(null, "tag"))
                .RuleFor(r => r.Scope, f => f.Lorem.Word())
                .RuleFor(r => r.Target, f =>
                    new PinTarget
                    {
                        Gid = f.Internet.Url(),
                        Label = $"n{n}"
                    })
                .RuleFor(r => r.Assertion, GetAssertion())
                .Generate());
        }

        return ids;
    }

    public static Assertion GetAssertion()
    {
        return new Faker<Assertion>()
            .RuleFor(a => a.Tag, f => f.PickRandom("a", "b", null))
            .RuleFor(a => a.Rank, f => f.Random.Short(1, 3))
            .RuleFor(a => a.References, GetDocReferences(2))
            .RuleFor(a => a.Note, f => f.Lorem.Sentence().OrNull(f))
            .Generate();
    }

    public static List<AssertedChronotope> GetAssertedChronotopes(int count)
    {
        List<AssertedChronotope> chronotopes = [];
        for (int n = 1; n <= count; n++)
        {
            bool even = n % 2 == 0;
            chronotopes.Add(new AssertedChronotope
            {
                Place = new AssertedPlace
                {
                    Value = even ? "Even" : "Odd"
                },
                Date = new AssertedDate(HistoricalDate.Parse($"{1300 + n} AD")!)
            });
        }
        return chronotopes;
    }

    public static List<CodImage> GetCodImages(int count)
    {
        List<CodImage> images = [];
        for (int n = 1; n <= count; n++)
        {
            images.Add(new Faker<CodImage>()
                .RuleFor(i => i.Id, f => f.Lorem.Word() + n)
                .RuleFor(i => i.Type, f => f.PickRandom("photo", "drawing"))
                .RuleFor(i => i.SourceId, f => f.Lorem.Word())
                .RuleFor(i => i.Label, f => f.Lorem.Sentence())
                .RuleFor(i => i.Copyright, f => f.Person.FullName)
                .Generate());
        }
        return images;
    }

    public static List<PhysicalDimension> GetDimensions(int count)
    {
        List<PhysicalDimension> dimensions = [];

        for (int n = 1; n <= count; n++)
        {
            dimensions.Add(new Faker<PhysicalDimension>()
                .RuleFor(d => d.Tag, f => f.Lorem.Word())
                .RuleFor(d => d.Value, f => Truncate(f.Random.Float(2, 10), 2))
                .RuleFor(d => d.Unit, "cm")
                .Generate());
        }

        return dimensions;
    }

    public static PhysicalSize GetPhysicalSize()
    {
        List<PhysicalDimension> dimensions = GetDimensions(2);

        Faker faker = new();
        return new PhysicalSize
        {
            Tag = "tag",
            W = dimensions[0],
            H = dimensions[1],
            Note = faker.Random.Bool(0.25f)? faker.Lorem.Sentence() : null
        };
    }
}
