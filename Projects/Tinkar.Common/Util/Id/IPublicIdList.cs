using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public interface IPublicIdList<E> : IPublicIdCollection<E>, IdList
        where E : IPublicId
    {
    }
}
