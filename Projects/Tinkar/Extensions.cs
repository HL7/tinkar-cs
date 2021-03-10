using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkar
{
    public static class Extensions
    {
        /// <summary>
        /// Convert list of KeyValuePairs into an immutable dictionary.
        /// </summary>
        /// <typeparam name="TConcept"></typeparam>
        /// <typeparam name="TField"></typeparam>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static ImmutableDictionary<TConcept, TField> ToImmutableDict<TConcept, TField>(this IEnumerable <KeyValuePair<TConcept, TField>> properties)
        {
            var builder = ImmutableDictionary<TConcept, TField>.Empty.ToBuilder();
            builder.AddRange(properties);
            return builder.ToImmutable();
        }
    }
}
