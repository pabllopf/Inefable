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
    /// <para>Information about an invited member of a shared content.</para>
    /// </summary>
    /// <seealso cref="Global::Dropbox.Api.Sharing.MembershipInfo" />
    public class InviteeMembershipInfo : MembershipInfo
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<InviteeMembershipInfo> Encoder = new InviteeMembershipInfoEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<InviteeMembershipInfo> Decoder = new InviteeMembershipInfoDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="InviteeMembershipInfo" />
        /// class.</para>
        /// </summary>
        /// <param name="accessType">The access type for this member. It contains inherited
        /// access type from parent folder, and acquired access type from this folder.</param>
        /// <param name="invitee">Recipient of the invitation.</param>
        /// <param name="permissions">The permissions that requesting user has on this member.
        /// The set of permissions corresponds to the MemberActions in the request.</param>
        /// <param name="initials">Never set.</param>
        /// <param name="isInherited">True if the member has access from a parent
        /// folder.</param>
        /// <param name="user">The user this invitation is tied to, if available.</param>
        public InviteeMembershipInfo(AccessLevel accessType,
                                     InviteeInfo invitee,
                                     col.IEnumerable<MemberPermission> permissions = null,
                                     string initials = null,
                                     bool isInherited = false,
                                     UserInfo user = null)
            : base(accessType, permissions, initials, isInherited)
        {
            if (invitee == null)
            {
                throw new sys.ArgumentNullException("invitee");
            }

            this.Invitee = invitee;
            this.User = user;
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="InviteeMembershipInfo" />
        /// class.</para>
        /// </summary>
        /// <remarks>This is to construct an instance of the object when
        /// deserializing.</remarks>
        [sys.ComponentModel.EditorBrowsable(sys.ComponentModel.EditorBrowsableState.Never)]
        public InviteeMembershipInfo()
        {
        }

        /// <summary>
        /// <para>Recipient of the invitation.</para>
        /// </summary>
        public InviteeInfo Invitee { get; protected set; }

        /// <summary>
        /// <para>The user this invitation is tied to, if available.</para>
        /// </summary>
        public UserInfo User { get; protected set; }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="InviteeMembershipInfo" />.</para>
        /// </summary>
        private class InviteeMembershipInfoEncoder : enc.StructEncoder<InviteeMembershipInfo>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(InviteeMembershipInfo value, enc.IJsonWriter writer)
            {
                WriteProperty("access_type", value.AccessType, writer, global::Dropbox.Api.Sharing.AccessLevel.Encoder);
                WriteProperty("invitee", value.Invitee, writer, global::Dropbox.Api.Sharing.InviteeInfo.Encoder);
                if (value.Permissions.Count > 0)
                {
                    WriteListProperty("permissions", value.Permissions, writer, global::Dropbox.Api.Sharing.MemberPermission.Encoder);
                }
                if (value.Initials != null)
                {
                    WriteProperty("initials", value.Initials, writer, enc.StringEncoder.Instance);
                }
                WriteProperty("is_inherited", value.IsInherited, writer, enc.BooleanEncoder.Instance);
                if (value.User != null)
                {
                    WriteProperty("user", value.User, writer, global::Dropbox.Api.Sharing.UserInfo.Encoder);
                }
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="InviteeMembershipInfo" />.</para>
        /// </summary>
        private class InviteeMembershipInfoDecoder : enc.StructDecoder<InviteeMembershipInfo>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="InviteeMembershipInfo"
            /// />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override InviteeMembershipInfo Create()
            {
                return new InviteeMembershipInfo();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(InviteeMembershipInfo value, string fieldName, enc.IJsonReader reader)
            {
                switch (fieldName)
                {
                    case "access_type":
                        value.AccessType = global::Dropbox.Api.Sharing.AccessLevel.Decoder.Decode(reader);
                        break;
                    case "invitee":
                        value.Invitee = global::Dropbox.Api.Sharing.InviteeInfo.Decoder.Decode(reader);
                        break;
                    case "permissions":
                        value.Permissions = ReadList<MemberPermission>(reader, global::Dropbox.Api.Sharing.MemberPermission.Decoder);
                        break;
                    case "initials":
                        value.Initials = enc.StringDecoder.Instance.Decode(reader);
                        break;
                    case "is_inherited":
                        value.IsInherited = enc.BooleanDecoder.Instance.Decode(reader);
                        break;
                    case "user":
                        value.User = global::Dropbox.Api.Sharing.UserInfo.Decoder.Decode(reader);
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
