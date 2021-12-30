using Cadmus.Mat.Bricks;
using Cadmus.Refs.Bricks;
using System.Text;

namespace Cadmus.Codicology.Parts
{
    /// <summary>
    /// Minimalist information about manuscript's binding.
    /// </summary>
    public class CodBinding
    {
        /// <summary>
        /// Gets or sets an optional tag, typically used to label bindings
        /// other than the current one.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the cover material.
        /// </summary>
        public string CoverMaterial { get; set; }

        /// <summary>
        /// Gets or sets the support material.
        /// </summary>
        public string SupportMaterial { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        public PhysicalSize Size { get; set; }

        /// <summary>
        /// Gets or sets the place/date of the binding.
        /// </summary>
        public AssertedChronotope Chronotope { get; set; }

        /// <summary>
        /// Gets or sets the binding's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(Tag)) sb.Append('[').Append(Tag).Append(']');
            if (!string.IsNullOrEmpty(CoverMaterial))
            {
                if (sb.Length > 0) sb.Append(' ');
                sb.Append("c=").Append(CoverMaterial);
            }
            if (!string.IsNullOrEmpty(SupportMaterial))
            {
                if (sb.Length > 0) sb.Append(' ');
                sb.Append("c=").Append(SupportMaterial);
            }
            if (Size != null)
            {
                if (sb.Length > 0) sb.Append(' ');
                sb.Append(Size);
            }
            if (Chronotope != null)
            {
                if (sb.Length > 0) sb.Append(' ');
                sb.Append(Chronotope);
            }
            return base.ToString();
        }
    }
}
