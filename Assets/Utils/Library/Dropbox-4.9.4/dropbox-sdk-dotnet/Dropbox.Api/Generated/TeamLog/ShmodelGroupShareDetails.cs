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
    /// <para>Shared link with group.</para>
    /// </summary>
    public class ShmodelGroupShareDetails
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<ShmodelGroupShareDetails> Encoder = new ShmodelGroupShareDetailsEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<ShmodelGroupShareDetails> Decoder = new ShmodelGroupShareDetailsDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="ShmodelGroupShareDetails" />
        /// class.</para>
        /// </summary>
        public ShmodelGroupShareDetails()
        {
        }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="ShmodelGroupShareDetails" />.</para>
        /// </summary>
        private class ShmodelGroupShareDetailsEncoder : enc.StructEncoder<ShmodelGroupShareDetails>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(ShmodelGroupShareDetails value, enc.IJsonWriter writer)
            {
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="ShmodelGroupShareDetails" />.</para>
        /// </summary>
        private class ShmodelGroupShareDetailsDecoder : enc.StructDecoder<ShmodelGroupShareDetails>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="ShmodelGroupShareDetails"
            /// />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override ShmodelGroupShareDetails Create()
            {
                return new ShmodelGroupShareDetails();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(ShmodelGroupShareDetails value, string fieldName, enc.IJsonReader reader)
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
