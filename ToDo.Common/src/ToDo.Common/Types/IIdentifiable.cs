using System;

namespace ToDo.Common.Types
{
    public interface IIdentifiable
    {
        Guid Id { get; }
    }
}
