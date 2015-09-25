using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SURLYcmps439_DAL.ErrorManager
{
    [Serializable]
    public class RuntimeError : Exception
    {
        public RuntimeError()
            : base() { }

        public RuntimeError(string message)
            : base(message) { }

        public RuntimeError(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public RuntimeError(string message, Exception innerException)
            : base(message, innerException) { }

        public RuntimeError(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected RuntimeError(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
