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
    /// <para>The tfa add security key type object</para>
    /// </summary>
    public class TfaAddSecurityKeyType
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<TfaAddSecurityKeyType> Encoder = new TfaAddSecurityKeyTypeEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<TfaAddSecurityKeyType> Decoder = new TfaAddSecurityKeyTypeDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="TfaAddSecurityKeyType" />
        /// class.</para>
        /// </summary>
        /// <param name="description">The description</param>
        public TfaAddSecurityKeyType(string description)
        {
            if (description == null)
            {
                throw new sys.ArgumentNullException("description");
            }

            this.Description = description;
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="TfaAddSecurityKeyType" />
        /// class.</para>
        /// </summary>
        /// <remarks>This is to construct an instance of the object when
        /// deserializing.</remarks>
        [sys.ComponentModel.EditorBrowsable(sys.ComponentModel.EditorBrowsableState.Never)]
        public TfaAddSecurityKeyType()
        {
        }

        /// <summary>
        /// <para>Gets the description of the tfa add security key type</para>
        /// </summary>
        public string Description { get; protected set; }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="TfaAddSecurityKeyType" />.</para>
        /// </summary>
        private class TfaAddSecurityKeyTypeEncoder : enc.StructEncoder<TfaAddSecurityKeyType>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(TfaAddSecurityKeyType value, enc.IJsonWriter writer)
            {
                WriteProperty("description", value.Description, writer, enc.StringEncoder.Instance);
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="TfaAddSecurityKeyType" />.</para>
        /// </summary>
        private class TfaAddSecurityKeyTypeDecoder : enc.StructDecoder<TfaAddSecurityKeyType>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="TfaAddSecurityKeyType"
            /// />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override TfaAddSecurityKeyType Create()
            {
                return new TfaAddSecurityKeyType();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(TfaAddSecurityKeyType value, string fieldName, enc.IJsonReader reader)
            {
                switch (fieldName)
                {
                    case "description":
                        value.Description = enc.StringDecoder.Instance.Decode(reader);
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
