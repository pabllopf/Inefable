// <auto-generated>
// Auto-generated by StoneAPI, do not modify.
// </auto-generated>

namespace Dropbox.Api.Paper
{
    using sys = System;
    using col = System.Collections.Generic;
    using re = System.Text.RegularExpressions;

    using enc = Dropbox.Api.Stone;

    /// <summary>
    /// <para>The ref paper doc object</para>
    /// </summary>
    /// <seealso cref="AddPaperDocUser" />
    /// <seealso cref="ListUsersOnFolderArgs" />
    /// <seealso cref="ListUsersOnFolderContinueArgs" />
    /// <seealso cref="ListUsersOnPaperDocArgs" />
    /// <seealso cref="ListUsersOnPaperDocContinueArgs" />
    /// <seealso cref="PaperDocExport" />
    /// <seealso cref="PaperDocSharingPolicy" />
    /// <seealso cref="PaperDocUpdateArgs" />
    /// <seealso cref="RemovePaperDocUser" />
    public class RefPaperDoc
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<RefPaperDoc> Encoder = new RefPaperDocEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<RefPaperDoc> Decoder = new RefPaperDocDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="RefPaperDoc" /> class.</para>
        /// </summary>
        /// <param name="docId">The Paper doc ID.</param>
        public RefPaperDoc(string docId)
        {
            if (docId == null)
            {
                throw new sys.ArgumentNullException("docId");
            }

            this.DocId = docId;
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="RefPaperDoc" /> class.</para>
        /// </summary>
        /// <remarks>This is to construct an instance of the object when
        /// deserializing.</remarks>
        [sys.ComponentModel.EditorBrowsable(sys.ComponentModel.EditorBrowsableState.Never)]
        public RefPaperDoc()
        {
        }

        /// <summary>
        /// <para>The Paper doc ID.</para>
        /// </summary>
        public string DocId { get; protected set; }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="RefPaperDoc" />.</para>
        /// </summary>
        private class RefPaperDocEncoder : enc.StructEncoder<RefPaperDoc>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(RefPaperDoc value, enc.IJsonWriter writer)
            {
                WriteProperty("doc_id", value.DocId, writer, enc.StringEncoder.Instance);
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="RefPaperDoc" />.</para>
        /// </summary>
        private class RefPaperDocDecoder : enc.StructDecoder<RefPaperDoc>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="RefPaperDoc" />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override RefPaperDoc Create()
            {
                return new RefPaperDoc();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(RefPaperDoc value, string fieldName, enc.IJsonReader reader)
            {
                switch (fieldName)
                {
                    case "doc_id":
                        value.DocId = enc.StringDecoder.Instance.Decode(reader);
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
