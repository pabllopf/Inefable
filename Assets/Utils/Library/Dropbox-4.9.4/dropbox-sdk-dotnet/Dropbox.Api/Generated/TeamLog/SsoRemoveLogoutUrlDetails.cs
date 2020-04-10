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
    /// <para>Removed sign-out URL for SSO.</para>
    /// </summary>
    public class SsoRemoveLogoutUrlDetails
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<SsoRemoveLogoutUrlDetails> Encoder = new SsoRemoveLogoutUrlDetailsEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<SsoRemoveLogoutUrlDetails> Decoder = new SsoRemoveLogoutUrlDetailsDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="SsoRemoveLogoutUrlDetails" />
        /// class.</para>
        /// </summary>
        /// <param name="previousValue">Previous single sign-on logout URL.</param>
        public SsoRemoveLogoutUrlDetails(string previousValue)
        {
            if (previousValue == null)
            {
                throw new sys.ArgumentNullException("previousValue");
            }

            this.PreviousValue = previousValue;
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="SsoRemoveLogoutUrlDetails" />
        /// class.</para>
        /// </summary>
        /// <remarks>This is to construct an instance of the object when
        /// deserializing.</remarks>
        [sys.ComponentModel.EditorBrowsable(sys.ComponentModel.EditorBrowsableState.Never)]
        public SsoRemoveLogoutUrlDetails()
        {
        }

        /// <summary>
        /// <para>Previous single sign-on logout URL.</para>
        /// </summary>
        public string PreviousValue { get; protected set; }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="SsoRemoveLogoutUrlDetails" />.</para>
        /// </summary>
        private class SsoRemoveLogoutUrlDetailsEncoder : enc.StructEncoder<SsoRemoveLogoutUrlDetails>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(SsoRemoveLogoutUrlDetails value, enc.IJsonWriter writer)
            {
                WriteProperty("previous_value", value.PreviousValue, writer, enc.StringEncoder.Instance);
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="SsoRemoveLogoutUrlDetails" />.</para>
        /// </summary>
        private class SsoRemoveLogoutUrlDetailsDecoder : enc.StructDecoder<SsoRemoveLogoutUrlDetails>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="SsoRemoveLogoutUrlDetails"
            /// />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override SsoRemoveLogoutUrlDetails Create()
            {
                return new SsoRemoveLogoutUrlDetails();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(SsoRemoveLogoutUrlDetails value, string fieldName, enc.IJsonReader reader)
            {
                switch (fieldName)
                {
                    case "previous_value":
                        value.PreviousValue = enc.StringDecoder.Instance.Decode(reader);
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
