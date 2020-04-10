// <auto-generated>
// Auto-generated by StoneAPI, do not modify.
// </auto-generated>

namespace Dropbox.Api.Sharing
{
    using sys = System;
    using col = System.Collections.Generic;
    using re = System.Text.RegularExpressions;

    using enc = Dropbox.Api.Stone;

    /// <summary>
    /// <para>Settings that apply to a link.</para>
    /// </summary>
    public class LinkSettings
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<LinkSettings> Encoder = new LinkSettingsEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<LinkSettings> Decoder = new LinkSettingsDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="LinkSettings" /> class.</para>
        /// </summary>
        /// <param name="accessLevel">The access level on the link for this file. Currently, it
        /// only accepts 'viewer' and 'viewer_no_comment'.</param>
        /// <param name="audience">The type of audience on the link for this file.</param>
        /// <param name="expiry">An expiry timestamp to set on a link.</param>
        /// <param name="password">The password for the link.</param>
        public LinkSettings(AccessLevel accessLevel = null,
                            LinkAudience audience = null,
                            LinkExpiry expiry = null,
                            LinkPassword password = null)
        {
            this.AccessLevel = accessLevel;
            this.Audience = audience;
            this.Expiry = expiry;
            this.Password = password;
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="LinkSettings" /> class.</para>
        /// </summary>
        /// <remarks>This is to construct an instance of the object when
        /// deserializing.</remarks>
        [sys.ComponentModel.EditorBrowsable(sys.ComponentModel.EditorBrowsableState.Never)]
        public LinkSettings()
        {
        }

        /// <summary>
        /// <para>The access level on the link for this file. Currently, it only accepts
        /// 'viewer' and 'viewer_no_comment'.</para>
        /// </summary>
        public AccessLevel AccessLevel { get; protected set; }

        /// <summary>
        /// <para>The type of audience on the link for this file.</para>
        /// </summary>
        public LinkAudience Audience { get; protected set; }

        /// <summary>
        /// <para>An expiry timestamp to set on a link.</para>
        /// </summary>
        public LinkExpiry Expiry { get; protected set; }

        /// <summary>
        /// <para>The password for the link.</para>
        /// </summary>
        public LinkPassword Password { get; protected set; }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="LinkSettings" />.</para>
        /// </summary>
        private class LinkSettingsEncoder : enc.StructEncoder<LinkSettings>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(LinkSettings value, enc.IJsonWriter writer)
            {
                if (value.AccessLevel != null)
                {
                    WriteProperty("access_level", value.AccessLevel, writer, global::Dropbox.Api.Sharing.AccessLevel.Encoder);
                }
                if (value.Audience != null)
                {
                    WriteProperty("audience", value.Audience, writer, global::Dropbox.Api.Sharing.LinkAudience.Encoder);
                }
                if (value.Expiry != null)
                {
                    WriteProperty("expiry", value.Expiry, writer, global::Dropbox.Api.Sharing.LinkExpiry.Encoder);
                }
                if (value.Password != null)
                {
                    WriteProperty("password", value.Password, writer, global::Dropbox.Api.Sharing.LinkPassword.Encoder);
                }
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="LinkSettings" />.</para>
        /// </summary>
        private class LinkSettingsDecoder : enc.StructDecoder<LinkSettings>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="LinkSettings" />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override LinkSettings Create()
            {
                return new LinkSettings();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(LinkSettings value, string fieldName, enc.IJsonReader reader)
            {
                switch (fieldName)
                {
                    case "access_level":
                        value.AccessLevel = global::Dropbox.Api.Sharing.AccessLevel.Decoder.Decode(reader);
                        break;
                    case "audience":
                        value.Audience = global::Dropbox.Api.Sharing.LinkAudience.Decoder.Decode(reader);
                        break;
                    case "expiry":
                        value.Expiry = global::Dropbox.Api.Sharing.LinkExpiry.Decoder.Decode(reader);
                        break;
                    case "password":
                        value.Password = global::Dropbox.Api.Sharing.LinkPassword.Decoder.Decode(reader);
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
