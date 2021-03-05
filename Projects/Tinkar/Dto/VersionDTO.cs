using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    /// <summary>
    /// 
    /// </summary>
    public abstract record VersionDTO : IDTO, IVersion, IEquivalent, IComparable
    {
        /// <summary>
        /// Gets public id.
        /// </summary>
        public IPublicId PublicId { get; init; }

        /// <summary>
        /// Gets the Stamp DTO.
        /// </summary>
        public StampDTO StampDTO { get; init; }

        /// <summary>
        /// Gets the Stamp.
        /// </summary>
        public IStamp Stamp => this.StampDTO;

        public VersionDTO(IPublicId componentPublicId, StampDTO stamp)
        {
            this.PublicId = componentPublicId;
            this.StampDTO = stamp;
        }

        /// <summary>
        /// Implementation of IEquivalent.IsEquivalent
        /// We manually create this rather than using the default
        /// record implementation because we want to compare to
        /// do a deep comparison, not just compare reference equality.
        /// </summary>
        /// <param name="other">Item to compare to for equivalence.</param>
        /// <returns>true if equal.</returns>
        public Boolean IsEquivalent(Object other) => this.CompareTo(other) == 0;

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="otherObject">Item to compare to.</param>
        /// <returns>-1, 0, or 1.</returns>
        public virtual Int32 CompareTo(Object otherObject)
        {
            VersionDTO other = otherObject as VersionDTO;
            if (other == null)
                return -1;

            Int32 cmp = this.PublicId.CompareTo(other.PublicId);
            if (cmp != 0)
                return cmp;
            cmp = this.Stamp.CompareTo(other.Stamp);
            if (cmp != 0)
                return cmp;
            return 0;
        }
    }
}
