// <auto-generated>
// Auto-generated by StoneAPI, do not modify.
// </auto-generated>

namespace Dropbox.Api.Files
{
    using sys = System;
    using col = System.Collections.Generic;
    using re = System.Text.RegularExpressions;

    using enc = Dropbox.Api.Stone;

    /// <summary>
    /// <para>The save copy reference arg object</para>
    /// </summary>
    public class SaveCopyReferenceArg
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<SaveCopyReferenceArg> Encoder = new SaveCopyReferenceArgEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<SaveCopyReferenceArg> Decoder = new SaveCopyReferenceArgDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="SaveCopyReferenceArg" />
        /// class.</para>
        /// </summary>
        /// <param name="copyReference">A copy reference returned by <see
        /// cref="Dropbox.Api.Files.Routes.FilesUserRoutes.CopyReferenceGetAsync" />.</param>
        /// <param name="path">Path in the user's Dropbox that is the destination.</param>
        public SaveCopyReferenceArg(string copyReference,
                                    string path)
        {
            if (copyReference == null)
            {
                throw new sys.ArgumentNullException("copyReference");
            }

            if (path == null)
            {
                throw new sys.ArgumentNullException("path");
            }
            if (!re.Regex.IsMatch(path, @"\A(?:/(.|[\r\n])*)\z"))
            {
                throw new sys.ArgumentOutOfRangeException("path", @"Value should match pattern '\A(?:/(.|[\r\n])*)\z'");
            }

            this.CopyReference = copyReference;
            this.Path = path;
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="SaveCopyReferenceArg" />
        /// class.</para>
        /// </summary>
        /// <remarks>This is to construct an instance of the object when
        /// deserializing.</remarks>
        [sys.ComponentModel.EditorBrowsable(sys.ComponentModel.EditorBrowsableState.Never)]
        public SaveCopyReferenceArg()
        {
        }

        /// <summary>
        /// <para>A copy reference returned by <see
        /// cref="Dropbox.Api.Files.Routes.FilesUserRoutes.CopyReferenceGetAsync" />.</para>
        /// </summary>
        public string CopyReference { get; protected set; }

        /// <summary>
        /// <para>Path in the user's Dropbox that is the destination.</para>
        /// </summary>
        public string Path { get; protected set; }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="SaveCopyReferenceArg" />.</para>
        /// </summary>
        private class SaveCopyReferenceArgEncoder : enc.StructEncoder<SaveCopyReferenceArg>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(SaveCopyReferenceArg value, enc.IJsonWriter writer)
            {
                WriteProperty("copy_reference", value.CopyReference, writer, enc.StringEncoder.Instance);
                WriteProperty("path", value.Path, writer, enc.StringEncoder.Instance);
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="SaveCopyReferenceArg" />.</para>
        /// </summary>
        private class SaveCopyReferenceArgDecoder : enc.StructDecoder<SaveCopyReferenceArg>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="SaveCopyReferenceArg" />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override SaveCopyReferenceArg Create()
            {
                return new SaveCopyReferenceArg();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(SaveCopyReferenceArg value, string fieldName, enc.IJsonReader reader)
            {
                switch (fieldName)
                {
                    case "copy_reference":
                        value.CopyReference = enc.StringDecoder.Instance.Decode(reader);
                        break;
                    case "path":
                        value.Path = enc.StringDecoder.Instance.Decode(reader);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }
        }

        #endregion
    }
}
