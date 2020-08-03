using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Common.Types
{
    public class BaseEntity : IIdentifiable
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
