using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.ErrorManager
{
    [Serializable]
    public class SyntaxError : Exception
    {
        public SyntaxError()
            : base() { }

        public SyntaxError(string message)
            : base(message) { }

        public SyntaxError(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public SyntaxError(string message, Exception innerException)
            : base(message, innerException) { }

        public SyntaxError(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected SyntaxError(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
