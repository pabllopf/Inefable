// <auto-generated>
// Auto-generated by StoneAPI, do not modify.
// </auto-generated>

namespace Dropbox.Api.Auth
{
    using sys = System;
    using col = System.Collections.Generic;
    using re = System.Text.RegularExpressions;

    using enc = Dropbox.Api.Stone;

    /// <summary>
    /// <para>The token from o auth1 result object</para>
    /// </summary>
    public class TokenFromOAuth1Result
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<TokenFromOAuth1Result> Encoder = new TokenFromOAuth1ResultEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<TokenFromOAuth1Result> Decoder = new TokenFromOAuth1ResultDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="TokenFromOAuth1Result" />
        /// class.</para>
        /// </summary>
        /// <param name="oauth2Token">The OAuth 2.0 token generated from the supplied OAuth 1.0
        /// token.</param>
        public TokenFromOAuth1Result(string oauth2Token)
        {
            if (oauth2Token == null)
            {
                throw new sys.ArgumentNullException("oauth2Token");
            }
            if (oauth2Token.Length < 1)
            {
                throw new sys.ArgumentOutOfRangeException("oauth2Token", "Length should be at least 1");
            }

            this.Oauth2Token = oauth2Token;
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="TokenFromOAuth1Result" />
        /// class.</para>
        /// </summary>
        /// <remarks>This is to construct an instance of the object when
        /// deserializing.</remarks>
        [sys.ComponentModel.EditorBrowsable(sys.ComponentModel.EditorBrowsableState.Never)]
        public TokenFromOAuth1Result()
        {
        }

        /// <summary>
        /// <para>The OAuth 2.0 token generated from the supplied OAuth 1.0 token.</para>
        /// </summary>
        public string Oauth2Token { get; protected set; }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="TokenFromOAuth1Result" />.</para>
        /// </summary>
        private class TokenFromOAuth1ResultEncoder : enc.StructEncoder<TokenFromOAuth1Result>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(TokenFromOAuth1Result value, enc.IJsonWriter writer)
            {
                WriteProperty("oauth2_token", value.Oauth2Token, writer, enc.StringEncoder.Instance);
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="TokenFromOAuth1Result" />.</para>
        /// </summary>
        private class TokenFromOAuth1ResultDecoder : enc.StructDecoder<TokenFromOAuth1Result>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="TokenFromOAuth1Result"
            /// />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override TokenFromOAuth1Result Create()
            {
                return new TokenFromOAuth1Result();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(TokenFromOAuth1Result value, string fieldName, enc.IJsonReader reader)
            {
                switch (fieldName)
                {
                    case "oauth2_token":
                        value.Oauth2Token = enc.StringDecoder.Instance.Decode(reader);
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
