using Bogus;
using Cadmus.Codicology.Parts;
using Cadmus.Core;
using Fusi.Tools.Configuration;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Codicology.Parts;

/// <summary>
/// Seeder for <see cref="CodContentsPart"/>.
/// Tag: <c>seed.it.vedph.codicology.contents</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.codicology.contents")]
public sealed class CodContentsPartSeeder : PartSeederBase,
    IConfigurable<CodContentsPartSeederOptions>
{
    private CodContentsPartSeederOptions? _options;

    /// <summary>
    /// Configures the seeder with the specified options.
    /// </summary>
    /// <param name="options">The options to use for seeding content parts.</param>
    /// <exception cref="ArgumentNullException">options.</exception>
    public void Configure(CodContentsPartSeederOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    private List<CodContentAnnotation> GetAnnotations(int count)
    {
        List<CodContentAnnotation> annotations = [];
        for (int n = 1; n <= count; n++)
        {
            annotations.Add(new Faker<CodContentAnnotation>()
                .RuleFor(a => a.Type,
                    f => f.PickRandom(_options?.AnnotationTypes?.Count > 0
                    ? _options.AnnotationTypes : ["rubric", "dedication"]))
                .RuleFor(c => c.Range, SeedHelper.GetLocationRanges(1)[0])
                .RuleFor(c => c.Text, f => f.Lorem.Sentence())
                .RuleFor(c => c.Incipit, f => f.Lorem.Sentence())
                .RuleFor(c => c.Explicit, f => f.Lorem.Sentence())
                .Generate());
        }
        return annotations;
    }

    private List<CodContent> GetContents(int count)
    {
        List<CodContent> contents = [];
        for (int n = 1; n <= count; n++)
        {
            contents.Add(new Faker<CodContent>()
                .RuleFor(c => c.Eid, f => f.Lorem.Word())
                .RuleFor(c => c.Ranges, SeedHelper.GetLocationRanges(1))
                .RuleFor(c => c.States,
                    f => [f.PickRandom(_options?.ContentStates?.Count > 0
                    ? _options.ContentStates : ["headless", "gaps"])])
                .RuleFor(c => c.Title, f => f.Lorem.Sentence(2, 4))
                .RuleFor(c => c.Location,
                    f => $"{f.Random.Number(1, 12)}.{f.Random.Number(1, 100)}")
                .RuleFor(c => c.ClaimedAuthor, f => f.Person.FullName)
                .RuleFor(c => c.ClaimedTitle, f => f.Lorem.Sentence(2, 4))
                .RuleFor(c => c.Incipit, f => f.Lorem.Sentence())
                .RuleFor(c => c.Explicit, f => f.Lorem.Sentence())
                .RuleFor(c => c.Annotations,
                    f => GetAnnotations(f.Random.Number(1, 3)))
                .Generate());
        }
        return contents;
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

        CodContentsPart part = new Faker<CodContentsPart>()
           .RuleFor(p => p.Contents, f => GetContents(f.Random.Number(1, 3)))
           .Generate();
        SetPartMetadata(part, roleId, item);

        return part;
    }
}

/// <summary>
/// Options for <see cref="CodContentsPartSeeder"/>.
/// </summary>
public class CodContentsPartSeederOptions
{
    /// <summary>
    /// The annotation type IDs to pick from.
    /// </summary>
    public List<string>? AnnotationTypes { get; set; }

    /// <summary>
    /// The content state IDs to pick from.
    /// </summary>
    public List<string>? ContentStates { get; set; }
}
