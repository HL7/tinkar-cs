using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public class PublicIdListFactory
    {
        public static  PublicIdListFactory INSTANCE = new PublicIdListFactory();

        public IPublicIdList Empty() => PublicIdCollections.ListN.EMPTY_LIST;
        public IPublicIdList Of() => this.Empty();
        public IPublicIdList Of(IPublicId one) => new PublicIdCollections.List12(one);
        public IPublicIdList of(IPublicId one, IPublicId two)
        {
            return new PublicIdCollections.List12(one, two);
        }

        public IPublicIdList of(IPublicId one, IPublicId two, IPublicId three)
        {
            return new PublicIdCollections.ListN<>(one, two, three);
        }

        public IPublicIdList of(IPublicId one, IPublicId two, IPublicId three, IPublicId four)
        {
            return new PublicIdCollections.ListN<>(one, two, three, four);
        }

        public IPublicIdList of(IPublicId one, IPublicId two, IPublicId three, IPublicId four, IPublicId five)
        {
            return new PublicIdCollections.ListN<>(one, two, three, four, five);
        }

        public IPublicIdList of(IPublicId one, IPublicId two, IPublicId three, IPublicId four, IPublicId five, IPublicId six)
        {
            return new PublicIdCollections.ListN<>(one, two, three, four, five, six);
        }

        public IPublicIdList of(IPublicId one, IPublicId two, IPublicId three, IPublicId four, IPublicId five, IPublicId six, IPublicId seven)
        {
            return new PublicIdCollections.ListN<>(one, two, three, four, five, six, seven);
        }


        public IPublicIdList of(IPublicId one, IPublicId two, IPublicId three, IPublicId four, IPublicId five, IPublicId six, IPublicId seven, IPublicId eight)
        {
            return new PublicIdCollections.ListN<>(one, two, three, four, five, six, seven, eight);
        }

        public IPublicIdList of(IPublicId one, IPublicId two, IPublicId three, IPublicId four, IPublicId five, IPublicId six, IPublicId seven, IPublicId eight, IPublicId nine)
        {
            return new PublicIdCollections.ListN<>(one, two, three, four, five, six, seven, eight, nine);
        }

        public IPublicIdList of(IPublicId one, IPublicId two, IPublicId three, IPublicId four, IPublicId five, IPublicId six, IPublicId seven, IPublicId eight, IPublicId nine, IPublicId ten)
        {
            return new PublicIdCollections.ListN<>(one, two, three, four, five, six, seven, eight, nine, ten);
        }
        public IPublicIdList ofArray(IPublicId[] items)
        {
            return of(items);
        }

        public IPublicIdList of(IPublicId...items)
        {
            if (items == null || items.length == 0)
            {
                return this.empty();
            }

            switch (items.length)
            {
                case 1:
                    return this.of(items[0]);
                case 2:
                    return this.of(items[0], items[1]);
                case 3:
                    return this.of(items[0], items[1], items[2]);
                case 4:
                    return this.of(items[0], items[1], items[2], items[3]);
                case 5:
                    return this.of(items[0], items[1], items[2], items[3], items[4]);
                case 6:
                    return this.of(items[0], items[1], items[2], items[3], items[4], items[5]);
                case 7:
                    return this.of(items[0], items[1], items[2], items[3], items[4], items[5], items[6]);
                case 8:
                    return this.of(items[0], items[1], items[2], items[3], items[4], items[5], items[6], items[7]);
                case 9:
                    return this.of(items[0], items[1], items[2], items[3], items[4], items[5], items[6], items[7], items[8]);
                case 10:
                    return this.of(items[0], items[1], items[2], items[3], items[4], items[5], items[6], items[7], items[8], items[9]);

                default:
                    return new PublicIdCollections.ListN<>(items);
            }
        }

        private IPublicIdList of(List<IPublicId> items)
        {
            switch (items.size())
            {
                case 0:
                    return this.empty();
                case 1:
                    return this.of(items.get(0));
                case 2:
                    return this.of(items.get(0), items.get(1));
                case 3:
                    return this.of(items.get(0), items.get(1), items.get(2));
                case 4:
                    return this.of(items.get(0), items.get(1), items.get(2), items.get(3));
                case 5:
                    return this.of(items.get(0), items.get(1), items.get(2), items.get(3), items.get(4));
                case 6:
                    return this.of(items.get(0), items.get(1), items.get(2), items.get(3), items.get(4), items.get(5));
                case 7:
                    return this.of(items.get(0), items.get(1), items.get(2), items.get(3), items.get(4), items.get(5), items.get(6));
                case 8:
                    return this.of(items.get(0), items.get(1), items.get(2), items.get(3), items.get(4), items.get(5), items.get(6), items.get(7));
                case 9:
                    return this.of(items.get(0), items.get(1), items.get(2), items.get(3), items.get(4), items.get(5), items.get(6), items.get(7), items.get(8));
                case 10:
                    return this.of(items.get(0), items.get(1), items.get(2), items.get(3), items.get(4), items.get(5), items.get(6), items.get(7), items.get(8), items.get(9));

                default:
                    return new PublicIdCollections.ListN<>(items.toArray(new IPublicId[items.size()]));
            }
        }

    }
}
