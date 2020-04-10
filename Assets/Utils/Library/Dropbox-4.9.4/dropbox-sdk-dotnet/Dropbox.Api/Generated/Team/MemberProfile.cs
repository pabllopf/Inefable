// <auto-generated>
// Auto-generated by StoneAPI, do not modify.
// </auto-generated>

namespace Dropbox.Api.Team
{
    using sys = System;
    using col = System.Collections.Generic;
    using re = System.Text.RegularExpressions;

    using enc = Dropbox.Api.Stone;

    /// <summary>
    /// <para>Basic member profile.</para>
    /// </summary>
    /// <seealso cref="GroupMemberInfo" />
    /// <seealso cref="TeamMemberProfile" />
    public class MemberProfile
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<MemberProfile> Encoder = new MemberProfileEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<MemberProfile> Decoder = new MemberProfileDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="MemberProfile" /> class.</para>
        /// </summary>
        /// <param name="teamMemberId">ID of user as a member of a team.</param>
        /// <param name="email">Email address of user.</param>
        /// <param name="emailVerified">Is true if the user's email is verified to be owned by
        /// the user.</param>
        /// <param name="status">The user's status as a member of a specific team.</param>
        /// <param name="name">Representations for a person's name.</param>
        /// <param name="membershipType">The user's membership type: full (normal team member)
        /// vs limited (does not use a license; no access to the team's shared quota).</param>
        /// <param name="externalId">External ID that a team can attach to the user. An
        /// application using the API may find it easier to use their own IDs instead of
        /// Dropbox IDs like account_id or team_member_id.</param>
        /// <param name="accountId">A user's account identifier.</param>
        /// <param name="joinedOn">The date and time the user joined as a member of a specific
        /// team.</param>
        /// <param name="suspendedOn">The date and time the user was suspended from the team
        /// (contains value only when the member's status matches <see
        /// cref="Dropbox.Api.Team.TeamMemberStatus.Suspended" />.</param>
        /// <param name="persistentId">Persistent ID that a team can attach to the user. The
        /// persistent ID is unique ID to be used for SAML authentication.</param>
        /// <param name="isDirectoryRestricted">Whether the user is a directory restricted
        /// user.</param>
        /// <param name="profilePhotoUrl">URL for the photo representing the user, if one is
        /// set.</param>
        public MemberProfile(string teamMemberId,
                             string email,
                             bool emailVerified,
                             TeamMemberStatus status,
                             global::Dropbox.Api.Users.Name name,
                             TeamMembershipType membershipType,
                             string externalId = null,
                             string accountId = null,
                             sys.DateTime? joinedOn = null,
                             sys.DateTime? suspendedOn = null,
                             string persistentId = null,
                             bool? isDirectoryRestricted = null,
                             string profilePhotoUrl = null)
        {
            if (teamMemberId == null)
            {
                throw new sys.ArgumentNullException("teamMemberId");
            }

            if (email == null)
            {
                throw new sys.ArgumentNullException("email");
            }

            if (status == null)
            {
                throw new sys.ArgumentNullException("status");
            }

            if (name == null)
            {
                throw new sys.ArgumentNullException("name");
            }

            if (membershipType == null)
            {
                throw new sys.ArgumentNullException("membershipType");
            }

            if (accountId != null)
            {
                if (accountId.Length < 40)
                {
                    throw new sys.ArgumentOutOfRangeException("accountId", "Length should be at least 40");
                }
                if (accountId.Length > 40)
                {
                    throw new sys.ArgumentOutOfRangeException("accountId", "Length should be at most 40");
                }
            }

            this.TeamMemberId = teamMemberId;
            this.Email = email;
            this.EmailVerified = emailVerified;
            this.Status = status;
            this.Name = name;
            this.MembershipType = membershipType;
            this.ExternalId = externalId;
            this.AccountId = accountId;
            this.JoinedOn = joinedOn;
            this.SuspendedOn = suspendedOn;
            this.PersistentId = persistentId;
            this.IsDirectoryRestricted = isDirectoryRestricted;
            this.ProfilePhotoUrl = profilePhotoUrl;
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="MemberProfile" /> class.</para>
        /// </summary>
        /// <remarks>This is to construct an instance of the object when
        /// deserializing.</remarks>
        [sys.ComponentModel.EditorBrowsable(sys.ComponentModel.EditorBrowsableState.Never)]
        public MemberProfile()
        {
        }

        /// <summary>
        /// <para>ID of user as a member of a team.</para>
        /// </summary>
        public string TeamMemberId { get; protected set; }

        /// <summary>
        /// <para>Email address of user.</para>
        /// </summary>
        public string Email { get; protected set; }

        /// <summary>
        /// <para>Is true if the user's email is verified to be owned by the user.</para>
        /// </summary>
        public bool EmailVerified { get; protected set; }

        /// <summary>
        /// <para>The user's status as a member of a specific team.</para>
        /// </summary>
        public TeamMemberStatus Status { get; protected set; }

        /// <summary>
        /// <para>Representations for a person's name.</para>
        /// </summary>
        public global::Dropbox.Api.Users.Name Name { get; protected set; }

        /// <summary>
        /// <para>The user's membership type: full (normal team member) vs limited (does not
        /// use a license; no access to the team's shared quota).</para>
        /// </summary>
        public TeamMembershipType MembershipType { get; protected set; }

        /// <summary>
        /// <para>External ID that a team can attach to the user. An application using the API
        /// may find it easier to use their own IDs instead of Dropbox IDs like account_id or
        /// team_member_id.</para>
        /// </summary>
        public string ExternalId { get; protected set; }

        /// <summary>
        /// <para>A user's account identifier.</para>
        /// </summary>
        public string AccountId { get; protected set; }

        /// <summary>
        /// <para>The date and time the user joined as a member of a specific team.</para>
        /// </summary>
        public sys.DateTime? JoinedOn { get; protected set; }

        /// <summary>
        /// <para>The date and time the user was suspended from the team (contains value only
        /// when the member's status matches <see
        /// cref="Dropbox.Api.Team.TeamMemberStatus.Suspended" />.</para>
        /// </summary>
        public sys.DateTime? SuspendedOn { get; protected set; }

        /// <summary>
        /// <para>Persistent ID that a team can attach to the user. The persistent ID is unique
        /// ID to be used for SAML authentication.</para>
        /// </summary>
        public string PersistentId { get; protected set; }

        /// <summary>
        /// <para>Whether the user is a directory restricted user.</para>
        /// </summary>
        public bool? IsDirectoryRestricted { get; protected set; }

        /// <summary>
        /// <para>URL for the photo representing the user, if one is set.</para>
        /// </summary>
        public string ProfilePhotoUrl { get; protected set; }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="MemberProfile" />.</para>
        /// </summary>
        private class MemberProfileEncoder : enc.StructEncoder<MemberProfile>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(MemberProfile value, enc.IJsonWriter writer)
            {
                WriteProperty("team_member_id", value.TeamMemberId, writer, enc.StringEncoder.Instance);
                WriteProperty("email", value.Email, writer, enc.StringEncoder.Instance);
                WriteProperty("email_verified", value.EmailVerified, writer, enc.BooleanEncoder.Instance);
                WriteProperty("status", value.Status, writer, global::Dropbox.Api.Team.TeamMemberStatus.Encoder);
                WriteProperty("name", value.Name, writer, global::Dropbox.Api.Users.Name.Encoder);
                WriteProperty("membership_type", value.MembershipType, writer, global::Dropbox.Api.Team.TeamMembershipType.Encoder);
                if (value.ExternalId != null)
                {
                    WriteProperty("external_id", value.ExternalId, writer, enc.StringEncoder.Instance);
                }
                if (value.AccountId != null)
                {
                    WriteProperty("account_id", value.AccountId, writer, enc.StringEncoder.Instance);
                }
                if (value.JoinedOn != null)
                {
                    WriteProperty("joined_on", value.JoinedOn.Value, writer, enc.DateTimeEncoder.Instance);
                }
                if (value.SuspendedOn != null)
                {
                    WriteProperty("suspended_on", value.SuspendedOn.Value, writer, enc.DateTimeEncoder.Instance);
                }
                if (value.PersistentId != null)
                {
                    WriteProperty("persistent_id", value.PersistentId, writer, enc.StringEncoder.Instance);
                }
                if (value.IsDirectoryRestricted != null)
                {
                    WriteProperty("is_directory_restricted", value.IsDirectoryRestricted.Value, writer, enc.BooleanEncoder.Instance);
                }
                if (value.ProfilePhotoUrl != null)
                {
                    WriteProperty("profile_photo_url", value.ProfilePhotoUrl, writer, enc.StringEncoder.Instance);
                }
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="MemberProfile" />.</para>
        /// </summary>
        private class MemberProfileDecoder : enc.StructDecoder<MemberProfile>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="MemberProfile" />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override MemberProfile Create()
            {
                return new MemberProfile();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(MemberProfile value, string fieldName, enc.IJsonReader reader)
            {
                switch (fieldName)
                {
                    case "team_member_id":
                        value.TeamMemberId = enc.StringDecoder.Instance.Decode(reader);
                        break;
                    case "email":
                        value.Email = enc.StringDecoder.Instance.Decode(reader);
                        break;
                    case "email_verified":
                        value.EmailVerified = enc.BooleanDecoder.Instance.Decode(reader);
                        break;
                    case "status":
                        value.Status = global::Dropbox.Api.Team.TeamMemberStatus.Decoder.Decode(reader);
                        break;
                    case "name":
                        value.Name = global::Dropbox.Api.Users.Name.Decoder.Decode(reader);
                        break;
                    case "membership_type":
                        value.MembershipType = global::Dropbox.Api.Team.TeamMembershipType.Decoder.Decode(reader);
                        break;
                    case "external_id":
                        value.ExternalId = enc.StringDecoder.Instance.Decode(reader);
                        break;
                    case "account_id":
                        value.AccountId = enc.StringDecoder.Instance.Decode(reader);
                        break;
                    case "joined_on":
                        value.JoinedOn = enc.DateTimeDecoder.Instance.Decode(reader);
                        break;
                    case "suspended_on":
                        value.SuspendedOn = enc.DateTimeDecoder.Instance.Decode(reader);
                        break;
                    case "persistent_id":
                        value.PersistentId = enc.StringDecoder.Instance.Decode(reader);
                        break;
                    case "is_directory_restricted":
                        value.IsDirectoryRestricted = enc.BooleanDecoder.Instance.Decode(reader);
                        break;
                    case "profile_photo_url":
                        value.ProfilePhotoUrl = enc.StringDecoder.Instance.Decode(reader);
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
