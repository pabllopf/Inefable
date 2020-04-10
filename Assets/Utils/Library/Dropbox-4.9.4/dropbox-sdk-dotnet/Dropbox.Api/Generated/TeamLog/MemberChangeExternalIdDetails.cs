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
    /// <para>Changed the external ID for team member.</para>
    /// </summary>
    public class MemberChangeExternalIdDetails
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<MemberChangeExternalIdDetails> Encoder = new MemberChangeExternalIdDetailsEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<MemberChangeExternalIdDetails> Decoder = new MemberChangeExternalIdDetailsDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="MemberChangeExternalIdDetails"
        /// /> class.</para>
        /// </summary>
        /// <param name="newValue">Current external id.</param>
        /// <param name="previousValue">Old external id.</param>
        public MemberChangeExternalIdDetails(string newValue,
                                             string previousValue)
        {
            if (newValue == null)
            {
                throw new sys.ArgumentNullException("newValue");
            }
            if (newValue.Length > 64)
            {
                throw new sys.ArgumentOutOfRangeException("newValue", "Length should be at most 64");
            }

            if (previousValue == null)
            {
                throw new sys.ArgumentNullException("previousValue");
            }
            if (previousValue.Length > 64)
            {
                throw new sys.ArgumentOutOfRangeException("previousValue", "Length should be at most 64");
            }

            this.NewValue = newValue;
            this.PreviousValue = previousValue;
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="MemberChangeExternalIdDetails"
        /// /> class.</para>
        /// </summary>
        /// <remarks>This is to construct an instance of the object when
        /// deserializing.</remarks>
        [sys.ComponentModel.EditorBrowsable(sys.ComponentModel.EditorBrowsableState.Never)]
        public MemberChangeExternalIdDetails()
        {
        }

        /// <summary>
        /// <para>Current external id.</para>
        /// </summary>
        public string NewValue { get; protected set; }

        /// <summary>
        /// <para>Old external id.</para>
        /// </summary>
        public string PreviousValue { get; protected set; }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="MemberChangeExternalIdDetails" />.</para>
        /// </summary>
        private class MemberChangeExternalIdDetailsEncoder : enc.StructEncoder<MemberChangeExternalIdDetails>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(MemberChangeExternalIdDetails value, enc.IJsonWriter writer)
            {
                WriteProperty("new_value", value.NewValue, writer, enc.StringEncoder.Instance);
                WriteProperty("previous_value", value.PreviousValue, writer, enc.StringEncoder.Instance);
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="MemberChangeExternalIdDetails" />.</para>
        /// </summary>
        private class MemberChangeExternalIdDetailsDecoder : enc.StructDecoder<MemberChangeExternalIdDetails>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="MemberChangeExternalIdDetails"
            /// />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override MemberChangeExternalIdDetails Create()
            {
                return new MemberChangeExternalIdDetails();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(MemberChangeExternalIdDetails value, string fieldName, enc.IJsonReader reader)
            {
                switch (fieldName)
                {
                    case "new_value":
                        value.NewValue = enc.StringDecoder.Instance.Decode(reader);
                        break;
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
