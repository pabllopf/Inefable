// <auto-generated>
// Auto-generated by StoneAPI, do not modify.
// </auto-generated>

namespace Dropbox.Api.TeamLog
{
    using sys = System;
    using col = System.Collections.Generic;
    using re = System.Text.RegularExpressions;

    using enc = Dropbox.Api.Stone;

    /// <summary>
    /// <para>Deleted files and/or folders.</para>
    /// </summary>
    public class FileDeleteDetails
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<FileDeleteDetails> Encoder = new FileDeleteDetailsEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<FileDeleteDetails> Decoder = new FileDeleteDetailsDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="FileDeleteDetails" />
        /// class.</para>
        /// </summary>
        public FileDeleteDetails()
        {
        }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="FileDeleteDetails" />.</para>
        /// </summary>
        private class FileDeleteDetailsEncoder : enc.StructEncoder<FileDeleteDetails>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(FileDeleteDetails value, enc.IJsonWriter writer)
            {
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="FileDeleteDetails" />.</para>
        /// </summary>
        private class FileDeleteDetailsDecoder : enc.StructDecoder<FileDeleteDetails>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="FileDeleteDetails" />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override FileDeleteDetails Create()
            {
                return new FileDeleteDetails();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(FileDeleteDetails value, string fieldName, enc.IJsonReader reader)
            {
                switch (fieldName)
                {
                    default:
                        reader.Skip();
                        break;
                }
            }
        }

        #endregion
    }
}
