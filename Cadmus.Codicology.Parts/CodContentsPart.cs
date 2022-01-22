using System;
using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// Manuscript's contents part.
    /// <para>Tag: <c>it.vedph.codicology.contents</c>.</para>
    /// </summary>
    [Tag("it.vedph.codicology.contents")]
    public sealed class CodContentsPart : PartBase
    {
        /// <summary>
        /// Gets or sets the entries.
        /// </summary>
        public List<CodContent> Contents { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodContentsPart"/> class.
        /// </summary>
        public CodContentsPart()
        {
            Contents = new List<CodContent>();
        }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins: <c>tot-count</c> and a collection of pins with
        /// these keys: <c>eid</c>, <c>state</c>, <c>title</c>,
        /// <c>claimed-author</c>, <c>claimed-title</c>, <c>annotation-count</c>.
        /// </returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item = null)
        {
            DataPinBuilder builder = new DataPinBuilder(
                DataPinHelper.DefaultFilter);

            builder.Set("tot", Contents?.Count ?? 0, false);

            if (Contents?.Count > 0)
            {
                int ac = 0;
                foreach (CodContent content in Contents)
                {
                    builder.AddValue("eid", content.Eid);
                    if (content.States?.Count > 0)
                        builder.AddValues("state", content.States);
                    builder.AddValue("title", content.Title,
                        filter: true, filterOptions: true);
                    builder.AddValue("claimed-author", content.ClaimedAuthor,
                        filter: true, filterOptions: true);
                    builder.AddValue("claimed-title", content.ClaimedTitle,
                        filter: true, filterOptions: true);

                    if (content.Annotations?.Count > 0)
                        ac += content.Annotations.Count;
                }
                if (ac > 0) builder.AddValue("annotation-count", ac);
            }

            return builder.Build(this);
        }

        /// <summary>
        /// Gets the definitions of data pins used by the implementor.
        /// </summary>
        /// <returns>Data pins definitions.</returns>
        public override IList<DataPinDefinition> GetDataPinDefinitions()
        {
            return new List<DataPinDefinition>(new[]
            {
                new DataPinDefinition(DataPinValueType.Integer,
                   "tot-count",
                   "The total count of contents."),
                new DataPinDefinition(DataPinValueType.String,
                   "eid",
                   "The contents IDs.",
                   "M"),
                new DataPinDefinition(DataPinValueType.String,
                   "state",
                   "The contents states.",
                   "M"),
                new DataPinDefinition(DataPinValueType.String,
                   "title",
                   "The contents titles.",
                   "MF"),
                new DataPinDefinition(DataPinValueType.String,
                   "claimed-author",
                   "The contents claimed authors.",
                   "MF"),
                new DataPinDefinition(DataPinValueType.String,
                   "claimed-title",
                   "The contents claimed titles.",
                   "MF"),
            });
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[CodContents]");

            if (Contents?.Count > 0)
            {
                sb.Append(' ');
                int n = 0;
                foreach (var entry in Contents)
                {
                    if (++n > 3) break;
                    if (n > 1) sb.Append("; ");
                    sb.Append(entry);
                }
                if (Contents.Count > 3)
                    sb.Append("...(").Append(Contents.Count).Append(')');
            }

            return sb.ToString();
        }
    }
}
