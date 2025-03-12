using Bogus;
using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Tools.Configuration;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Codicology.Parts;

/// <summary>
/// Seeder for <see cref="CodIllumInstructionsPart"/>.
/// Tag: <c>seed.it.vedph.codicology.cod-illum-instructions</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.codicology.cod-illum-instructions")]
public sealed class CodIllumInstructionsPartSeeder : PartSeederBase
{
    private static List<CodIllumInstruction> GetInstructions(int min, int max)
    {
        int n = Randomizer.Seed.Next(min, max + 1);

        return new Faker<CodIllumInstruction>()
            .RuleFor(i => i.Types, f => [f.PickRandom("rubrics", "instructions")])
            .RuleFor(i => i.Subject, f => f.Lorem.Word())
            .RuleFor(i => i.Text, f => f.Lorem.Sentence())
            .RuleFor(i => i.Range, _ => SeedHelper.GetLocationRange())
            .RuleFor(i => i.Position, f => f.PickRandom(
                "margin-top", "margin-bottom"))
            .RuleFor(i => i.Description, f => f.Lorem.Sentence())
            .RuleFor(i => i.Languages, f => [f.PickRandom("lat", "ita")])
            .Generate(n);
    }

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

        CodIllumInstructionsPart part = new Faker<CodIllumInstructionsPart>()
           .RuleFor(p => p.Instructions, f => GetInstructions(1, 3))
           .Generate();
        SetPartMetadata(part, roleId, item);

        return part;
    }
}
