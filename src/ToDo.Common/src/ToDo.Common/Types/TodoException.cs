using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Common.Types
{
    public class TodoException : Exception
    {
        public string Code { get; }

        public TodoException()
        {
        }

        public TodoException(string code)
        {
            Code = code;
        }

        public TodoException(string message, params object[] args)
            : this(string.Empty, message, args)
        {
        }

        public TodoException(string code, string message, params object[] args)
            : this(null, code, message, args)
        {
        }

        public TodoException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public TodoException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
