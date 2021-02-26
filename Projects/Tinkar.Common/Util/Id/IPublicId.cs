using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Immutable;

namespace Tinkar.Common
{
	public interface IPublicId
	{
		Guid[] AsUuidArray();

		int uuidCount();

		//**
		// * Presents ordered list of longs, from the UUIDs in the order: msb, lsb, msb, lsb, ...
		// * @param consumer
		// */
		//void forEach(LongConsumer consumer);

		public ImmutableList<Guid> AsUuidList() => AsUuidArray().ToImmutableList();
	}
}
