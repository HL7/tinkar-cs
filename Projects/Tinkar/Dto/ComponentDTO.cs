using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tinkar
{
    /// <summary>
    /// Base class for all Tinkar DTO classes.
    /// </summary>
    /// <typeparam name="TDto">Type of concrete child class.</typeparam>
    public abstract record ComponentDTO<TDto> :
        IComponent,
        IComparable<TDto>,
        IEquivalent<TDto>
        where TDto : IComponent
    {
        /// <summary>
        /// Version of marshalling code.
        /// If code is modified in a way that renders old serialized data
        /// non-conformant, then this number should be incremented.
        /// </summary>
        private const int LocalMarshalVersion = 3;

        /// <summary>
        /// Gets public id.
        /// </summary>
        public IPublicId PublicId { get; init; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="publicId">Public id for this item</param>
        public ComponentDTO(IPublicId publicId)
        {
            this.PublicId = publicId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptVersionDTO"/> class
        /// from binary stream.
        /// </summary>
        /// <param name="input">binary input stream.</param>
        /// <param name = "publicId" > Public id(component ids).</param>
        public ComponentDTO(TinkarInput input, IPublicId publicId) : this(publicId)
        {
            input.CheckMarshalVersion(LocalMarshalVersion);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentDTO"/> class.
        /// from input JSON stream.
        /// </summary>
        /// <param name="jObj">JSON parent container.</param>
        /// <param name = "publicId" > Public id(component ids).</param>
        /// <param name = "jsonClassName" > Name of Json class</param>
        public ComponentDTO(JObject jObj, IPublicId publicId, String jsonClassName) : this(publicId)
        {
            this.PublicId = publicId;

            jObj.GetClass(jsonClassName);
        }

        /// <summary>
        /// Implementation of IEquivalent.IsEquivalent
        /// We manually create this rather than using the default
        /// record implementation because we want to compare to
        /// do a deep comparison, not just compare reference equality.
        /// </summary>
        /// <param name="other">Item to compare to for equivalence.</param>
        /// <returns>true if equal.</returns>
        public Boolean IsEquivalent(TDto other) =>
            this.CompareTo(other) == 0;

        /// <summary>
        /// Compares this to another item.
        /// </summary>
        /// <param name="other">Item to compare to.</param>
        /// <returns>-1, 0, or 1.</returns>
        public virtual Int32 CompareTo(TDto other)
        {
            Int32 cmp = FieldCompare.ComparePublicIds(this.PublicId, other.PublicId);
            if (cmp != 0)
                return cmp;
            return 0;
        }
    }
}