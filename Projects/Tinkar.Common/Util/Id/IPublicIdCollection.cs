using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public interface IPublicIdCollection<E> : IIdCollection, IEnumerable<E>
        where E : IPublicId
    {

        //$void forEach(Consumer<? super E> consumer);

        //$Stream<? extends IPublicId> stream();

        IPublicId[] ToIdArray();

        bool Contains(IPublicId value);
    }
}
