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
public sealed class CodBindingsPartSeeder : PartSeederBase,
    IConfigurable<CodBindingsPartSeederOptions>
{
    private CodBindingsPartSeederOptions? _options;

    /// <summary>
    /// Configures the seeder with the specified options.
    /// </summary>
    /// <param name="options">The options to use for configuring the seeder.</param>
    /// <exception cref="ArgumentNullException">options</exception>
    public void Configure(CodBindingsPartSeederOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    private List<CodBinding> GetBindings(int count)
    {
        List<CodBinding> bindings = [];
        for (int n = 1; n <= count; n++)
        {
            bindings.Add(new Faker<CodBinding>()
                .RuleFor(b => b.Tag,
                    f => f.PickRandom(_options?.Tags?.Count > 0
                    ? _options.Tags : ["previous", "-"]))
                .RuleFor(b => b.CoverMaterial,
                    f => f.PickRandom(_options?.CoverMaterials?.Count > 0
                    ? _options.CoverMaterials : ["skin", "velvet"]))
                .RuleFor(b => b.BoardMaterial,
                    f => f.PickRandom(_options?.BoardMaterials?.Count > 0
                    ? _options.BoardMaterials : ["wood", "card"]))
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
        ArgumentNullException.ThrowIfNull(item);

        CodBindingsPart part = new Faker<CodBindingsPart>()
           .RuleFor(p => p.Bindings, f => GetBindings(f.Random.Number(1, 3)))
           .Generate();
        SetPartMetadata(part, roleId, item);

        return part;
    }
}

/// <summary>
/// Options for <see cref="CodBindingsPartSeeder"/>.
/// </summary>
public class CodBindingsPartSeederOptions
{
    /// <summary>
    /// Binding tag IDs to pick from.
    /// </summary>
    public List<string>? Tags { get; set; }

    /// <summary>
    /// Cover material IDs to pick from.
    /// </summary>
    public List<string>? CoverMaterials { get; set; }

    /// <summary>
    /// Board material IDs to pick from.
    /// </summary>
    public List<string>? BoardMaterials { get; set; }

}