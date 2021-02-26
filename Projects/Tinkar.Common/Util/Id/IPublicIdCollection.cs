using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public interface IPublicIdCollection<E extends PublicId> : IIdCollection, Iterable<E>
    {

        void forEach(Consumer<? super E> consumer);

        Stream<? extends IPublicId> stream();

        IPublicId[] toIdArray();

        bool contains(IPublicId value);
    }
}
