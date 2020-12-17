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
    public abstract record BaseDTO<TDto> :
        IComparable<TDto>,
        IEquivalent<TDto>
    {
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
        /// Compare two items of same DTO type.
        /// An exception is thrown if an attempt is made to compare obects
        /// of different types.
        /// </summary>
        /// <param name="obj">object to compare to.</param>
        /// <returns>Int32.</returns>
        public abstract Int32 CompareTo(TDto obj);
    }
}