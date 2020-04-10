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
    /// <para>Changed whether members can download shared file/folder.</para>
    /// </summary>
    public class SharedContentChangeDownloadsPolicyDetails
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<SharedContentChangeDownloadsPolicyDetails> Encoder = new SharedContentChangeDownloadsPolicyDetailsEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<SharedContentChangeDownloadsPolicyDetails> Decoder = new SharedContentChangeDownloadsPolicyDetailsDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see
        /// cref="SharedContentChangeDownloadsPolicyDetails" /> class.</para>
        /// </summary>
        /// <param name="newValue">New downloads policy.</param>
        /// <param name="previousValue">Previous downloads policy. Might be missing due to
        /// historical data gap.</param>
        public SharedContentChangeDownloadsPolicyDetails(DownloadPolicyType newValue,
                                                         DownloadPolicyType previousValue = null)
        {
            if (newValue == null)
            {
                throw new sys.ArgumentNullException("newValue");
            }

            this.NewValue = newValue;
            this.PreviousValue = previousValue;
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see
        /// cref="SharedContentChangeDownloadsPolicyDetails" /> class.</para>
        /// </summary>
        /// <remarks>This is to construct an instance of the object when
        /// deserializing.</remarks>
        [sys.ComponentModel.EditorBrowsable(sys.ComponentModel.EditorBrowsableState.Never)]
        public SharedContentChangeDownloadsPolicyDetails()
        {
        }

        /// <summary>
        /// <para>New downloads policy.</para>
        /// </summary>
        public DownloadPolicyType NewValue { get; protected set; }

        /// <summary>
        /// <para>Previous downloads policy. Might be missing due to historical data
        /// gap.</para>
        /// </summary>
        public DownloadPolicyType PreviousValue { get; protected set; }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="SharedContentChangeDownloadsPolicyDetails" />.</para>
        /// </summary>
        private class SharedContentChangeDownloadsPolicyDetailsEncoder : enc.StructEncoder<SharedContentChangeDownloadsPolicyDetails>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(SharedContentChangeDownloadsPolicyDetails value, enc.IJsonWriter writer)
            {
                WriteProperty("new_value", value.NewValue, writer, global::Dropbox.Api.TeamLog.DownloadPolicyType.Encoder);
                if (value.PreviousValue != null)
                {
                    WriteProperty("previous_value", value.PreviousValue, writer, global::Dropbox.Api.TeamLog.DownloadPolicyType.Encoder);
                }
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="SharedContentChangeDownloadsPolicyDetails" />.</para>
        /// </summary>
        private class SharedContentChangeDownloadsPolicyDetailsDecoder : enc.StructDecoder<SharedContentChangeDownloadsPolicyDetails>
        {
            /// <summary>
            /// <para>Create a new instance of type <see
            /// cref="SharedContentChangeDownloadsPolicyDetails" />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override SharedContentChangeDownloadsPolicyDetails Create()
            {
                return new SharedContentChangeDownloadsPolicyDetails();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(SharedContentChangeDownloadsPolicyDetails value, string fieldName, enc.IJsonReader reader)
            {
                switch (fieldName)
                {
                    case "new_value":
                        value.NewValue = global::Dropbox.Api.TeamLog.DownloadPolicyType.Decoder.Decode(reader);
                        break;
                    case "previous_value":
                        value.PreviousValue = global::Dropbox.Api.TeamLog.DownloadPolicyType.Decoder.Decode(reader);
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
