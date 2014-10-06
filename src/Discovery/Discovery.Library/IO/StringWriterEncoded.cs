using System.IO;
using System.Text;

namespace Discovery.Library.IO
{
    public sealed class StringWriterEncoded : StringWriter
    {
        #region Fields

        private readonly Encoding _encoding;

        #endregion //***** Fields

        #region Constructors

        public StringWriterEncoded(StringBuilder sb, Encoding encoding)
            : base(sb)
        {
            _encoding = encoding;
        }

        #endregion //***** Constructors

        #region Methods

        public override Encoding Encoding
        {
            get
            {
                return _encoding;
            }
        }

        #endregion //***** Methods
    }
}
