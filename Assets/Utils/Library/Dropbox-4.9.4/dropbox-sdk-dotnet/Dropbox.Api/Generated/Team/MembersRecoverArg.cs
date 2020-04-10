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
    /// <para>Exactly one of team_member_id, email, or external_id must be provided to identify
    /// the user account.</para>
    /// </summary>
    public class MembersRecoverArg
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<MembersRecoverArg> Encoder = new MembersRecoverArgEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<MembersRecoverArg> Decoder = new MembersRecoverArgDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="MembersRecoverArg" />
        /// class.</para>
        /// </summary>
        /// <param name="user">Identity of user to recover.</param>
        public MembersRecoverArg(UserSelectorArg user)
        {
            if (user == null)
            {
                throw new sys.ArgumentNullException("user");
            }

            this.User = user;
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="MembersRecoverArg" />
        /// class.</para>
        /// </summary>
        /// <remarks>This is to construct an instance of the object when
        /// deserializing.</remarks>
        [sys.ComponentModel.EditorBrowsable(sys.ComponentModel.EditorBrowsableState.Never)]
        public MembersRecoverArg()
        {
        }

        /// <summary>
        /// <para>Identity of user to recover.</para>
        /// </summary>
        public UserSelectorArg User { get; protected set; }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="MembersRecoverArg" />.</para>
        /// </summary>
        private class MembersRecoverArgEncoder : enc.StructEncoder<MembersRecoverArg>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(MembersRecoverArg value, enc.IJsonWriter writer)
            {
                WriteProperty("user", value.User, writer, global::Dropbox.Api.Team.UserSelectorArg.Encoder);
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="MembersRecoverArg" />.</para>
        /// </summary>
        private class MembersRecoverArgDecoder : enc.StructDecoder<MembersRecoverArg>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="MembersRecoverArg" />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override MembersRecoverArg Create()
            {
                return new MembersRecoverArg();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(MembersRecoverArg value, string fieldName, enc.IJsonReader reader)
            {
                switch (fieldName)
                {
                    case "user":
                        value.User = global::Dropbox.Api.Team.UserSelectorArg.Decoder.Decode(reader);
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
