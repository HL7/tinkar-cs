using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
    public class PublicIdSetFactory
    {
        public static  PublicIdSetFactory INSTANCE = new PublicIdSetFactory();

        public <E extends IPublicId> IPublicIdSet<E> empty()
        {
            return (IPublicIdSet<E>)PublicIdCollections.SetN.EMPTY_SET;
        }

        public <E extends IPublicId> IPublicIdSet<E> of()
        {
            return this.empty();
        }

        public <E extends IPublicId> IPublicIdSet<E> of(E one)
        {
            return new PublicIdCollections.Set12<>(one);
        }

        public <E extends IPublicId> IPublicIdSet<E> of(E one, E two)
        {
            if (Objects.equals(one, two))
            {
                return this.of(one);
            }
            return new PublicIdCollections.Set12<>(one, two);
        }

        public <E extends IPublicId> IPublicIdSet<E> of(E one, E two, E three)
        {
            if (Objects.equals(one, two))
            {
                return this.of(one, three);
            }
            if (Objects.equals(one, three))
            {
                return this.of(one, two);
            }
            if (Objects.equals(two, three))
            {
                return this.of(one, two);
            }
            return new PublicIdCollections.SetN<>(one, two, three);
        }

        public <E extends IPublicId> IPublicIdSet<E> of(E one, E two, E three, E four)
        {
            if (Objects.equals(one, two))
            {
                return this.of(one, three, four);
            }
            if (Objects.equals(one, three))
            {
                return this.of(one, two, four);
            }
            if (Objects.equals(one, four))
            {
                return this.of(one, two, three);
            }
            if (Objects.equals(two, three))
            {
                return this.of(one, two, four);
            }
            if (Objects.equals(two, four))
            {
                return this.of(one, two, three);
            }
            if (Objects.equals(three, four))
            {
                return this.of(one, two, three);
            }
            return new PublicIdCollections.SetN<>(one, two, three, four);
        }
        public IPublicIdSet<IPublicId> ofArray(IPublicId[] items)
        {
            return of(items);
        }

        public <E extends IPublicId> IPublicIdSet<E> of(E...items)
        {
            if (items == null || items.length == 0)
            {
                return this.of();
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
                default:
                    return new PublicIdCollections.SetN<>(items);
            }
        }
        public static  PublicId[] emptyArray = new IPublicId[0];
    public <E extends IPublicId> IPublicIdSet<E> of(Iterable<? extends E> items)
        {
            if (items instanceof PublicIdSet<?>)
                {
                    return (IPublicIdSet<E>)items;
                }

            if (Iterate.isEmpty(items))
            {
                return this.of();
            }
            return this.of((E[])Iterate.toArray(items, emptyArray));
        }
    }
}
