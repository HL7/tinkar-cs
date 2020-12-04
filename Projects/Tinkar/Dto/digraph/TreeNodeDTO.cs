using System;
using System.Collections.Generic;

namespace Tinkar
{
	public class TreeNodeDTO
	{
        private Guid NodeConceptUuid { get; init; }
        private Guid[][] TypeUuidDestinationUuid { get; init; }
        private Dictionary<Guid, Object> NodeProperties { get; init; }
	}
}

