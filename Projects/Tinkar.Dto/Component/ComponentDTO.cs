using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tinkar.Dto
{
    /// <summary>
    /// Base class for all Tinkar DTO classes.
    /// </summary>
    public record ComponentDTO : IDTO, IComponent
    {
        /// <summary>
        /// Gets public id.
        /// </summary>
        public IPublicId PublicId { get; init; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="componentPublicId">Public id for this item</param>
        public ComponentDTO(IPublicId componentPublicId)
        {
            this.PublicId = componentPublicId;
        }

        /// <summary>
        /// Implementation of IEquivalent.IsEquivalent
        /// We manually create this rather than using the default
        /// record implementation because we want to compare to
        /// do a deep comparison, not just compare reference equality.
        /// </summary>
        /// <param name="otherObject">Item to compare to for equivalence.</param>
        /// <returns>true if equal.</returns>
        public virtual Boolean IsEquivalent(Object otherObject)
        {
            ComponentDTO other = otherObject as ComponentDTO;
            if (other == null)
                return false;
            if (this.PublicId.IsEquivalent(other.PublicId) == false)
                return false;
            return true;
        }

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="otherObject">Item to compare to.</param>
        /// <returns>-1, 0, or 1.</returns>
        public virtual Int32 CompareTo(Object otherObject)
        {
            ComponentDTO other = otherObject as ComponentDTO;
            if (other == null)
                return -1;
            Int32 cmp = FieldCompare.ComparePublicIds(this.PublicId, other.PublicId);
            if (cmp != 0)
                return cmp;
            return 0;
        }
    }
}