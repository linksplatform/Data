using System.Collections.Generic;

namespace Platform.Data;

public delegate TLink ReadHandler<TLink>(IList<TLink> restriction);
