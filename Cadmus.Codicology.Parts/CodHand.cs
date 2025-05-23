﻿using Cadmus.Refs.Bricks;
using System.Collections.Generic;

namespace Cadmus.Codicology.Parts;

/// <summary>
/// A manuscript's hand.
/// </summary>
public class CodHand
{
    /// <summary>
    /// Gets or sets the entity ID for this hand.
    /// </summary>
    public string? Eid { get; set; }

    /// <summary>
    /// Gets or sets the conventional hand's name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the instances of this hand in the manuscript.
    /// </summary>
    public List<CodHandInstance> Instances { get; set; }

    /// <summary>
    /// Gets or sets a set of descriptions for this hand. These are
    /// variously referenced by <see cref="Instances"/>.
    /// </summary>
    public List<CodHandDescription> Descriptions { get; set; }

    /// <summary>
    /// Gets or sets the hand's subscriptions.
    /// </summary>
    public List<CodHandSubscription> Subscriptions { get; set; }

    /// <summary>
    /// Gets or sets the documental references.
    /// </summary>
    public List<DocReference> References { get; set; }

    /// <summary>
    /// Gets or sets the identification(s) for this hand.
    /// </summary>
    public List<AssertedCompositeId> Ids { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodHand"/> class.
    /// </summary>
    public CodHand()
    {
        Instances = [];
        Descriptions = [];
        Subscriptions = [];
        References = [];
        Ids = [];
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return $"#{Eid} {Name}";
    }
}
