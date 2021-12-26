using System.Collections.Generic;

namespace Platform.Data;

public delegate TLink WriteHandler<TLink>(IList<TLink> restriction, IList<TLink> substitution);
