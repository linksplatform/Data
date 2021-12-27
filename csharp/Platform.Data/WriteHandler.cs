using System.Collections.Generic;

namespace Platform.Data;

public delegate TLink WriteHandler<TLink>(IList<TLink> before, IList<TLink> after);
