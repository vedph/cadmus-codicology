using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Configuration;

namespace Cadmus.Codicology.Parts;

/// <summary>
/// Manuscript's hands part.
/// <para>Tag: <c>it.vedph.codicology.hands</c>.</para>
/// </summary>
[Tag("it.vedph.codicology.hands")]
public sealed class CodHandsPart : PartBase
{
    /// <summary>
    /// Gets or sets the entries.
    /// </summary>
    public List<CodHand> Hands { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodHandsPart"/> class.
    /// </summary>
    public CodHandsPart()
    {
        Hands = [];
    }

    /// <summary>
    /// Get all the key=value pairs (pins) exposed by the implementor.
    /// </summary>
    /// <param name="item">The optional item. The item with its parts
    /// can optionally be passed to this method for those parts requiring
    /// to access further data.</param>
    /// <returns>The pins: <c>tot-count</c> and a collection of pins with
    /// these keys: <c>eid</c>, <c>script</c>, <c>typology</c>,
    /// <c>color</c>, <c>subs-count</c>, <c>subs-language</c>.</returns>
    public override IEnumerable<DataPin> GetDataPins(IItem? item = null)
    {
        DataPinBuilder builder = new();

        builder.Set("tot", Hands?.Count ?? 0, false);
        int subsCount = 0;

        if (Hands?.Count > 0)
        {
            foreach (CodHand hand in Hands)
            {
                // eid
                builder.AddValue("eid", hand.Eid);

                // ids
                if (hand.Ids?.Count > 0)
                {
                    builder.AddValues("id", hand.Ids
                        .Where(cid => cid.Target != null)
                        .Select(cid => cid.Target!.Gid));
                }

                if (hand.Instances?.Count > 0)
                {
                    foreach (CodHandInstance instance in hand.Instances)
                    {
                        // script+
                        builder.AddValues("script", instance.Scripts);

                        // typology+
                        if (instance.Typologies?.Count > 0)
                            builder.AddValues("typology", instance.Typologies);

                        // color+
                        if (instance.Colors?.Count > 0)
                            builder.AddValues("color", instance.Colors);
                    }
                }

                if (hand.Subscriptions?.Count > 0)
                {
                    // subs-count
                    subsCount += hand.Subscriptions.Count;

                    // subs-language
                    builder.AddValues("subs-language",
                        hand.Subscriptions.Select(s => s.Language!));
                }

                if (hand.Descriptions?.Count > 0)
                {
                    foreach (CodHandDescription dsc in hand.Descriptions)
                    {
                        // mufi
                        builder.AddValues("mufi",
                            dsc.Signs.Select(s => s.Mufi)
                                .Where(n => n != null)
                                .Select(n => $"{n:X6}"));
                    }
                }
            }
        }

        if (subsCount > 0) builder.AddValue("subs-count", subsCount);

        return builder.Build(this);
    }

    /// <summary>
    /// Gets the definitions of data pins used by the implementor.
    /// </summary>
    /// <returns>Data pins definitions.</returns>
    public override IList<DataPinDefinition> GetDataPinDefinitions()
    {
        return [.. new[]
        {
            new DataPinDefinition(DataPinValueType.Integer,
               "tot-count",
               "The total count of hands."),
            new DataPinDefinition(DataPinValueType.String,
               "eid",
               "The list of hands EIDs.",
               "M"),
            new DataPinDefinition(DataPinValueType.String,
               "id",
               "The list of hands identifications.",
               "M"),
            new DataPinDefinition(DataPinValueType.String,
               "script",
               "The list of hands scripts.",
               "M"),
            new DataPinDefinition(DataPinValueType.String,
               "typology",
               "The list of hands typologies.",
               "M"),
            new DataPinDefinition(DataPinValueType.String,
               "color",
               "The list of hands colors.",
               "M"),
            new DataPinDefinition(DataPinValueType.Integer,
               "subs-count",
               "The total count of subscriptions.",
               "M"),
            new DataPinDefinition(DataPinValueType.String,
               "subs-language",
               "The list of hands subscriptions languages.",
               "M"),
            new DataPinDefinition(DataPinValueType.String,
                "mufi",
                "The list of hex MUFI codes.",
                "M"),
        }];
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        StringBuilder sb = new();

        sb.Append("[CodHands]");

        if (Hands?.Count > 0)
        {
            sb.Append(' ');
            int n = 0;
            foreach (var entry in Hands)
            {
                if (++n > 3) break;
                if (n > 1) sb.Append("; ");
                sb.Append(entry);
            }
            if (Hands.Count > 3)
                sb.Append("...(").Append(Hands.Count).Append(')');
        }

        return sb.ToString();
    }
}
