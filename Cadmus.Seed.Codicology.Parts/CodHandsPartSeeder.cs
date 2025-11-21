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
public sealed class CodHandsPartSeeder : PartSeederBase,
    IConfigurable<CodHandsPartSeederOptions>
{
    private CodHandsPartSeederOptions? _options;

    /// <summary>
    /// Configures this seeder.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <exception cref="ArgumentNullException">options</exception>
    public void Configure(CodHandsPartSeederOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    private List<CodHandSign> GetSigns(int count)
    {
        List<CodHandSign> signs = [];
        IList<string> types = _options?.SignTypes ?? ["letter", "punct"];

        for (int n = 1; n <= count; n++)
        {
            signs.Add(new Faker<CodHandSign>()
                .RuleFor(s => s.Eid, f => f.Lorem.Word())
                .RuleFor(s => s.Type, f => f.PickRandom(types))
                .RuleFor(s => s.SampleLocation,
                    SeedHelper.GetLocationRanges(1)[0].Start)
                .RuleFor(s => s.Description, f => f.Lorem.Sentence())
                .Generate());
        }
        return signs;
    }

    private List<CodHandInstance> GetInstances(int count)
    {
        List<CodHandInstance> instances = [];
        IList<string> scripts = _options?.Scripts ?? ["got", "mea"];
        IList<string> typologies = _options?.Typologies ?? ["text", "note"];
        IList<string> colors = _options?.Colors ?? ["red", "blue"];

        for (int n = 1; n <= count; n++)
        {
            instances.Add(new Faker<CodHandInstance>()
                .RuleFor(d => d.Scripts, f => [f.PickRandom(scripts)])
                .RuleFor(d => d.Typologies, f => [f.PickRandom(typologies)])
                .RuleFor(d => d.Colors, f => [f.PickRandom(colors)])
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

    private List<CodHandDescription> GetDescriptions(int count)
    {
        List<CodHandDescription> descriptions = [];
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

    private List<CodHandSubscription> GetSubscriptions(int count)
    {
        List<CodHandSubscription> subscriptions = [];
        IList<string> languages = _options?.Languages ?? ["la", "grc"];

        for (int n = 1; n <= count; n++)
        {
            subscriptions.Add(new Faker<CodHandSubscription>()
                .RuleFor(s => s.Ranges, SeedHelper.GetLocationRanges(1))
                .RuleFor(s => s.Language, f => f.PickRandom(languages))
                .RuleFor(s => s.Text, f => f.Lorem.Sentence())
                .RuleFor(s => s.Note, f => f.Lorem.Sentence().OrNull(f))
                .Generate());
        }
        return subscriptions;
    }

    private List<CodHand> GetHands(int count)
    {
        List<CodHand> hands = [];
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
        ArgumentNullException.ThrowIfNull(item);

        CodHandsPart part = new Faker<CodHandsPart>()
           .RuleFor(p => p.Hands, f => GetHands(f.Random.Number(1, 2)))
           .Generate();
        SetPartMetadata(part, roleId, item);

        return part;
    }
}

/// <summary>
/// Options for <see cref="CodHandsPartSeeder"/>.
/// </summary>
public class CodHandsPartSeederOptions
{
    /// <summary>
    /// Sign type IDs to pick from.
    /// </summary>
    public List<string>? SignTypes { get; set; }

    /// <summary>
    /// Script IDs to pick from.
    /// </summary>
    public List<string>? Scripts { get; set; }

    /// <summary>
    /// Typology IDs to pick from.
    /// </summary>
    public List<string>? Typologies { get; set; }

    /// <summary>
    /// Color IDs to pick from.
    /// </summary>
    public List<string>? Colors { get; set; }

    /// <summary>
    /// Language IDs to pick from.
    /// </summary>
    public List<string>? Languages { get; set; }
}
