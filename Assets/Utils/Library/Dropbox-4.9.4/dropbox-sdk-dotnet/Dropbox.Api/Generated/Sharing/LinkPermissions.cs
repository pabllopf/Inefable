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
    /// <para>The link permissions object</para>
    /// </summary>
    /// <seealso cref="FileLinkMetadata" />
    /// <seealso cref="FolderLinkMetadata" />
    /// <seealso cref="SharedLinkMetadata" />
    public class LinkPermissions
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<LinkPermissions> Encoder = new LinkPermissionsEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<LinkPermissions> Decoder = new LinkPermissionsDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="LinkPermissions" />
        /// class.</para>
        /// </summary>
        /// <param name="canRevoke">Whether the caller can revoke the shared link.</param>
        /// <param name="resolvedVisibility">The current visibility of the link after
        /// considering the shared links policies of the the team (in case the link's owner is
        /// part of a team) and the shared folder (in case the linked file is part of a shared
        /// folder). This field is shown only if the caller has access to this info (the link's
        /// owner always has access to this data). For some links, an effective_audience value
        /// is returned instead.</param>
        /// <param name="requestedVisibility">The shared link's requested visibility. This can
        /// be overridden by the team and shared folder policies. The final visibility, after
        /// considering these policies, can be found in <paramref name="resolvedVisibility" />.
        /// This is shown only if the caller is the link's owner and resolved_visibility is
        /// returned instead of effective_audience.</param>
        /// <param name="revokeFailureReason">The failure reason for revoking the link. This
        /// field will only be present if the <paramref name="canRevoke" /> is
        /// <c>false</c>.</param>
        /// <param name="effectiveAudience">The type of audience who can benefit from the
        /// access level specified by the `link_access_level` field.</param>
        /// <param name="linkAccessLevel">The access level that the link will grant to its
        /// users. A link can grant additional rights to a user beyond their current access
        /// level. For example, if a user was invited as a viewer to a file, and then opens a
        /// link with `link_access_level` set to `editor`, then they will gain editor
        /// privileges. The `link_access_level` is a property of the link, and does not depend
        /// on who is calling this API. In particular, `link_access_level` does not take into
        /// account the API caller's current permissions to the content.</param>
        public LinkPermissions(bool canRevoke,
                               ResolvedVisibility resolvedVisibility = null,
                               RequestedVisibility requestedVisibility = null,
                               SharedLinkAccessFailureReason revokeFailureReason = null,
                               LinkAudience effectiveAudience = null,
                               LinkAccessLevel linkAccessLevel = null)
        {
            this.CanRevoke = canRevoke;
            this.ResolvedVisibility = resolvedVisibility;
            this.RequestedVisibility = requestedVisibility;
            this.RevokeFailureReason = revokeFailureReason;
            this.EffectiveAudience = effectiveAudience;
            this.LinkAccessLevel = linkAccessLevel;
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="LinkPermissions" />
        /// class.</para>
        /// </summary>
        /// <remarks>This is to construct an instance of the object when
        /// deserializing.</remarks>
        [sys.ComponentModel.EditorBrowsable(sys.ComponentModel.EditorBrowsableState.Never)]
        public LinkPermissions()
        {
        }

        /// <summary>
        /// <para>Whether the caller can revoke the shared link.</para>
        /// </summary>
        public bool CanRevoke { get; protected set; }

        /// <summary>
        /// <para>The current visibility of the link after considering the shared links
        /// policies of the the team (in case the link's owner is part of a team) and the
        /// shared folder (in case the linked file is part of a shared folder). This field is
        /// shown only if the caller has access to this info (the link's owner always has
        /// access to this data). For some links, an effective_audience value is returned
        /// instead.</para>
        /// </summary>
        public ResolvedVisibility ResolvedVisibility { get; protected set; }

        /// <summary>
        /// <para>The shared link's requested visibility. This can be overridden by the team
        /// and shared folder policies. The final visibility, after considering these policies,
        /// can be found in <see cref="ResolvedVisibility" />. This is shown only if the caller
        /// is the link's owner and resolved_visibility is returned instead of
        /// effective_audience.</para>
        /// </summary>
        public RequestedVisibility RequestedVisibility { get; protected set; }

        /// <summary>
        /// <para>The failure reason for revoking the link. This field will only be present if
        /// the <see cref="CanRevoke" /> is <c>false</c>.</para>
        /// </summary>
        public SharedLinkAccessFailureReason RevokeFailureReason { get; protected set; }

        /// <summary>
        /// <para>The type of audience who can benefit from the access level specified by the
        /// `link_access_level` field.</para>
        /// </summary>
        public LinkAudience EffectiveAudience { get; protected set; }

        /// <summary>
        /// <para>The access level that the link will grant to its users. A link can grant
        /// additional rights to a user beyond their current access level. For example, if a
        /// user was invited as a viewer to a file, and then opens a link with
        /// `link_access_level` set to `editor`, then they will gain editor privileges. The
        /// `link_access_level` is a property of the link, and does not depend on who is
        /// calling this API. In particular, `link_access_level` does not take into account the
        /// API caller's current permissions to the content.</para>
        /// </summary>
        public LinkAccessLevel LinkAccessLevel { get; protected set; }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="LinkPermissions" />.</para>
        /// </summary>
        private class LinkPermissionsEncoder : enc.StructEncoder<LinkPermissions>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(LinkPermissions value, enc.IJsonWriter writer)
            {
                WriteProperty("can_revoke", value.CanRevoke, writer, enc.BooleanEncoder.Instance);
                if (value.ResolvedVisibility != null)
                {
                    WriteProperty("resolved_visibility", value.ResolvedVisibility, writer, global::Dropbox.Api.Sharing.ResolvedVisibility.Encoder);
                }
                if (value.RequestedVisibility != null)
                {
                    WriteProperty("requested_visibility", value.RequestedVisibility, writer, global::Dropbox.Api.Sharing.RequestedVisibility.Encoder);
                }
                if (value.RevokeFailureReason != null)
                {
                    WriteProperty("revoke_failure_reason", value.RevokeFailureReason, writer, global::Dropbox.Api.Sharing.SharedLinkAccessFailureReason.Encoder);
                }
                if (value.EffectiveAudience != null)
                {
                    WriteProperty("effective_audience", value.EffectiveAudience, writer, global::Dropbox.Api.Sharing.LinkAudience.Encoder);
                }
                if (value.LinkAccessLevel != null)
                {
                    WriteProperty("link_access_level", value.LinkAccessLevel, writer, global::Dropbox.Api.Sharing.LinkAccessLevel.Encoder);
                }
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="LinkPermissions" />.</para>
        /// </summary>
        private class LinkPermissionsDecoder : enc.StructDecoder<LinkPermissions>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="LinkPermissions" />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override LinkPermissions Create()
            {
                return new LinkPermissions();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(LinkPermissions value, string fieldName, enc.IJsonReader reader)
            {
                switch (fieldName)
                {
                    case "can_revoke":
                        value.CanRevoke = enc.BooleanDecoder.Instance.Decode(reader);
                        break;
                    case "resolved_visibility":
                        value.ResolvedVisibility = global::Dropbox.Api.Sharing.ResolvedVisibility.Decoder.Decode(reader);
                        break;
                    case "requested_visibility":
                        value.RequestedVisibility = global::Dropbox.Api.Sharing.RequestedVisibility.Decoder.Decode(reader);
                        break;
                    case "revoke_failure_reason":
                        value.RevokeFailureReason = global::Dropbox.Api.Sharing.SharedLinkAccessFailureReason.Decoder.Decode(reader);
                        break;
                    case "effective_audience":
                        value.EffectiveAudience = global::Dropbox.Api.Sharing.LinkAudience.Decoder.Decode(reader);
                        break;
                    case "link_access_level":
                        value.LinkAccessLevel = global::Dropbox.Api.Sharing.LinkAccessLevel.Decoder.Decode(reader);
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
