// <auto-generated>
// Auto-generated by StoneAPI, do not modify.
// </auto-generated>

namespace Dropbox.Api.FileProperties
{
    using sys = System;
    using col = System.Collections.Generic;
    using re = System.Text.RegularExpressions;

    using enc = Dropbox.Api.Stone;

    /// <summary>
    /// <para>The template owner type object</para>
    /// </summary>
    public class TemplateOwnerType
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<TemplateOwnerType> Encoder = new TemplateOwnerTypeEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<TemplateOwnerType> Decoder = new TemplateOwnerTypeDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="TemplateOwnerType" />
        /// class.</para>
        /// </summary>
        public TemplateOwnerType()
        {
        }

        /// <summary>
        /// <para>Gets a value indicating whether this instance is User</para>
        /// </summary>
        public bool IsUser
        {
            get
            {
                return this is User;
            }
        }

        /// <summary>
        /// <para>Gets this instance as a User, or <c>null</c>.</para>
        /// </summary>
        public User AsUser
        {
            get
            {
                return this as User;
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether this instance is Team</para>
        /// </summary>
        public bool IsTeam
        {
            get
            {
                return this is Team;
            }
        }

        /// <summary>
        /// <para>Gets this instance as a Team, or <c>null</c>.</para>
        /// </summary>
        public Team AsTeam
        {
            get
            {
                return this as Team;
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether this instance is Other</para>
        /// </summary>
        public bool IsOther
        {
            get
            {
                return this is Other;
            }
        }

        /// <summary>
        /// <para>Gets this instance as a Other, or <c>null</c>.</para>
        /// </summary>
        public Other AsOther
        {
            get
            {
                return this as Other;
            }
        }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="TemplateOwnerType" />.</para>
        /// </summary>
        private class TemplateOwnerTypeEncoder : enc.StructEncoder<TemplateOwnerType>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(TemplateOwnerType value, enc.IJsonWriter writer)
            {
                if (value is User)
                {
                    WriteProperty(".tag", "user", writer, enc.StringEncoder.Instance);
                    User.Encoder.EncodeFields((User)value, writer);
                    return;
                }
                if (value is Team)
                {
                    WriteProperty(".tag", "team", writer, enc.StringEncoder.Instance);
                    Team.Encoder.EncodeFields((Team)value, writer);
                    return;
                }
                if (value is Other)
                {
                    WriteProperty(".tag", "other", writer, enc.StringEncoder.Instance);
                    Other.Encoder.EncodeFields((Other)value, writer);
                    return;
                }
                throw new sys.InvalidOperationException();
            }
        }

        #endregion

        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="TemplateOwnerType" />.</para>
        /// </summary>
        private class TemplateOwnerTypeDecoder : enc.UnionDecoder<TemplateOwnerType>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="TemplateOwnerType" />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override TemplateOwnerType Create()
            {
                return new TemplateOwnerType();
            }

            /// <summary>
            /// <para>Decode based on given tag.</para>
            /// </summary>
            /// <param name="tag">The tag.</param>
            /// <param name="reader">The json reader.</param>
            /// <returns>The decoded object.</returns>
            protected override TemplateOwnerType Decode(string tag, enc.IJsonReader reader)
            {
                switch (tag)
                {
                    case "user":
                        return User.Decoder.DecodeFields(reader);
                    case "team":
                        return Team.Decoder.DecodeFields(reader);
                    default:
                        return Other.Decoder.DecodeFields(reader);
                }
            }
        }

        #endregion

        /// <summary>
        /// <para>Template will be associated with a user.</para>
        /// </summary>
        public sealed class User : TemplateOwnerType
        {
            #pragma warning disable 108

            /// <summary>
            /// <para>The encoder instance.</para>
            /// </summary>
            internal static enc.StructEncoder<User> Encoder = new UserEncoder();

            /// <summary>
            /// <para>The decoder instance.</para>
            /// </summary>
            internal static enc.StructDecoder<User> Decoder = new UserDecoder();

            /// <summary>
            /// <para>Initializes a new instance of the <see cref="User" /> class.</para>
            /// </summary>
            private User()
            {
            }

            /// <summary>
            /// <para>A singleton instance of User</para>
            /// </summary>
            public static readonly User Instance = new User();

            #region Encoder class

            /// <summary>
            /// <para>Encoder for  <see cref="User" />.</para>
            /// </summary>
            private class UserEncoder : enc.StructEncoder<User>
            {
                /// <summary>
                /// <para>Encode fields of given value.</para>
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="writer">The writer.</param>
                public override void EncodeFields(User value, enc.IJsonWriter writer)
                {
                }
            }

            #endregion

            #region Decoder class

            /// <summary>
            /// <para>Decoder for  <see cref="User" />.</para>
            /// </summary>
            private class UserDecoder : enc.StructDecoder<User>
            {
                /// <summary>
                /// <para>Create a new instance of type <see cref="User" />.</para>
                /// </summary>
                /// <returns>The struct instance.</returns>
                protected override User Create()
                {
                    return User.Instance;
                }

            }

            #endregion
        }

        /// <summary>
        /// <para>Template will be associated with a team.</para>
        /// </summary>
        public sealed class Team : TemplateOwnerType
        {
            #pragma warning disable 108

            /// <summary>
            /// <para>The encoder instance.</para>
            /// </summary>
            internal static enc.StructEncoder<Team> Encoder = new TeamEncoder();

            /// <summary>
            /// <para>The decoder instance.</para>
            /// </summary>
            internal static enc.StructDecoder<Team> Decoder = new TeamDecoder();

            /// <summary>
            /// <para>Initializes a new instance of the <see cref="Team" /> class.</para>
            /// </summary>
            private Team()
            {
            }

            /// <summary>
            /// <para>A singleton instance of Team</para>
            /// </summary>
            public static readonly Team Instance = new Team();

            #region Encoder class

            /// <summary>
            /// <para>Encoder for  <see cref="Team" />.</para>
            /// </summary>
            private class TeamEncoder : enc.StructEncoder<Team>
            {
                /// <summary>
                /// <para>Encode fields of given value.</para>
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="writer">The writer.</param>
                public override void EncodeFields(Team value, enc.IJsonWriter writer)
                {
                }
            }

            #endregion

            #region Decoder class

            /// <summary>
            /// <para>Decoder for  <see cref="Team" />.</para>
            /// </summary>
            private class TeamDecoder : enc.StructDecoder<Team>
            {
                /// <summary>
                /// <para>Create a new instance of type <see cref="Team" />.</para>
                /// </summary>
                /// <returns>The struct instance.</returns>
                protected override Team Create()
                {
                    return Team.Instance;
                }

            }

            #endregion
        }

        /// <summary>
        /// <para>The other object</para>
        /// </summary>
        public sealed class Other : TemplateOwnerType
        {
            #pragma warning disable 108

            /// <summary>
            /// <para>The encoder instance.</para>
            /// </summary>
            internal static enc.StructEncoder<Other> Encoder = new OtherEncoder();

            /// <summary>
            /// <para>The decoder instance.</para>
            /// </summary>
            internal static enc.StructDecoder<Other> Decoder = new OtherDecoder();

            /// <summary>
            /// <para>Initializes a new instance of the <see cref="Other" /> class.</para>
            /// </summary>
            private Other()
            {
            }

            /// <summary>
            /// <para>A singleton instance of Other</para>
            /// </summary>
            public static readonly Other Instance = new Other();

            #region Encoder class

            /// <summary>
            /// <para>Encoder for  <see cref="Other" />.</para>
            /// </summary>
            private class OtherEncoder : enc.StructEncoder<Other>
            {
                /// <summary>
                /// <para>Encode fields of given value.</para>
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="writer">The writer.</param>
                public override void EncodeFields(Other value, enc.IJsonWriter writer)
                {
                }
            }

            #endregion

            #region Decoder class

            /// <summary>
            /// <para>Decoder for  <see cref="Other" />.</para>
            /// </summary>
            private class OtherDecoder : enc.StructDecoder<Other>
            {
                /// <summary>
                /// <para>Create a new instance of type <see cref="Other" />.</para>
                /// </summary>
                /// <returns>The struct instance.</returns>
                protected override Other Create()
                {
                    return Other.Instance;
                }

            }

            #endregion
        }
    }
}
