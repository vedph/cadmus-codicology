using Bogus;
using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Tools.Configuration;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Codicology.Parts;

/// <summary>
/// Seeder for <see cref="CodHandsPart"/>.
/// Tag: <c>seed.it.vedph.codicology.hands</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.codicology.hands")]
public sealed class CodHandsPartSeeder : PartSeederBase
{
    private static List<CodHandSign> GetSigns(int count)
    {
        List<CodHandSign> signs = new();
        for (int n = 1; n <= count; n++)
        {
            signs.Add(new Faker<CodHandSign>()
                .RuleFor(s => s.Eid, f => f.Lorem.Word())
                .RuleFor(s => s.Type, f => f.PickRandom("letter", "punct"))
                .RuleFor(s => s.SampleLocation,
                    SeedHelper.GetLocationRanges(1)[0].Start)
                .RuleFor(s => s.Description, f => f.Lorem.Sentence())
                .Generate());
        }
        return signs;
    }

    private static List<CodHandInstance> GetInstances(int count)
    {
        List<CodHandInstance> instances = new();
        for (int n = 1; n <= count; n++)
        {
            instances.Add(new Faker<CodHandInstance>()
                .RuleFor(d => d.Script, f => f.PickRandom("got", "mea"))
                .RuleFor(d => d.Typologies, f => new List<string>
                {
                    f.PickRandom("text", "note")
                })
                .RuleFor(d => d.Colors, f => new List<string>
                {
                    f.PickRandom("red", "blue")
                })
                .RuleFor(d => d.Ranges, SeedHelper.GetLocationRanges(1))
                .RuleFor(d => d.Rank, f => f.Random.Short(1, 3))
                .RuleFor(d => d.DescriptionKey, "d1")
                .RuleFor(d => d.Chronotope, SeedHelper.GetAssertedChronotopes(1)[0])
                .RuleFor(d => d.Images,
                    f => SeedHelper.GetCodImages(f.Random.Number(1, 3)))
                .Generate());
        }
        return instances;
    }

    private static List<CodHandDescription> GetDescriptions(int count)
    {
        List<CodHandDescription> descriptions = new();
        for (int n = 1; n <= count; n++)
        {
            descriptions.Add(new Faker<CodHandDescription>()
                .RuleFor(d => d.Key, "d" + n)
                .RuleFor(d => d.Description, f => f.Lorem.Sentence())
                .RuleFor(d => d.Initials, f => f.Lorem.Sentence())
                .RuleFor(d => d.Corrections, f => f.Lorem.Sentence())
                .RuleFor(d => d.Punctuation, f => f.Lorem.Sentence())
                .RuleFor(d => d.Abbreviations, f => f.Lorem.Sentence())
                .RuleFor(d => d.Signs, f => GetSigns(f.Random.Number(1, 3)))
                .Generate());
        }
        return descriptions;
    }

    private static List<CodHandSubscription> GetSubscriptions(int count)
    {
        List<CodHandSubscription> subscriptions = new();
        for (int n = 1; n <= count; n++)
        {
            subscriptions.Add(new Faker<CodHandSubscription>()
                .RuleFor(s => s.Ranges, SeedHelper.GetLocationRanges(1))
                .RuleFor(s => s.Language, f => f.PickRandom("lat", "grc"))
                .RuleFor(s => s.Text, f => f.Lorem.Sentence())
                .RuleFor(s => s.Note, f => f.Lorem.Sentence().OrNull(f))
                .Generate());
        }
        return subscriptions;
    }

    private static List<CodHand> GetHands(int count)
    {
        List<CodHand> hands = new();
        for (int n = 1; n <= count; n++)
        {
            hands.Add(new Faker<CodHand>()
                .RuleFor(h => h.Eid, f => f.Lorem.Word())
                .RuleFor(h => h.Name, f => f.Lorem.Word())
                .RuleFor(h => h.Instances,
                    f => GetInstances(f.Random.Number(1, 3)))
                .RuleFor(h => h.Descriptions,
                    f => GetDescriptions(f.Random.Number(1, 3)))
                .RuleFor(h => h.Subscriptions,
                    f => GetSubscriptions(f.Random.Number(1, 3)))
                .RuleFor(h => h.References,
                    f => SeedHelper.GetDocReferences(f.Random.Number(1, 3)))
                .Generate());
        }
        return hands;
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

        CodHandsPart part = new Faker<CodHandsPart>()
           .RuleFor(p => p.Hands, f => GetHands(f.Random.Number(1, 2)))
           .Generate();
        SetPartMetadata(part, roleId, item);

        return part;
    }
}
