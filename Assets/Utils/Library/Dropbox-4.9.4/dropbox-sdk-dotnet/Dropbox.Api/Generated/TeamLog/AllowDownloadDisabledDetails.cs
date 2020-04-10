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
    /// <para>Disabled downloads.</para>
    /// </summary>
    public class AllowDownloadDisabledDetails
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<AllowDownloadDisabledDetails> Encoder = new AllowDownloadDisabledDetailsEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<AllowDownloadDisabledDetails> Decoder = new AllowDownloadDisabledDetailsDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="AllowDownloadDisabledDetails" />
        /// class.</para>
        /// </summary>
        public AllowDownloadDisabledDetails()
        {
        }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="AllowDownloadDisabledDetails" />.</para>
        /// </summary>
        private class AllowDownloadDisabledDetailsEncoder : enc.StructEncoder<AllowDownloadDisabledDetails>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(AllowDownloadDisabledDetails value, enc.IJsonWriter writer)
            {
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="AllowDownloadDisabledDetails" />.</para>
        /// </summary>
        private class AllowDownloadDisabledDetailsDecoder : enc.StructDecoder<AllowDownloadDisabledDetails>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="AllowDownloadDisabledDetails"
            /// />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override AllowDownloadDisabledDetails Create()
            {
                return new AllowDownloadDisabledDetails();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(AllowDownloadDisabledDetails value, string fieldName, enc.IJsonReader reader)
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
