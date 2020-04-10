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
    /// <para>The trusted non team member type object</para>
    /// </summary>
    public class TrustedNonTeamMemberType
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<TrustedNonTeamMemberType> Encoder = new TrustedNonTeamMemberTypeEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<TrustedNonTeamMemberType> Decoder = new TrustedNonTeamMemberTypeDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="TrustedNonTeamMemberType" />
        /// class.</para>
        /// </summary>
        public TrustedNonTeamMemberType()
        {
        }

        /// <summary>
        /// <para>Gets a value indicating whether this instance is MultiInstanceAdmin</para>
        /// </summary>
        public bool IsMultiInstanceAdmin
        {
            get
            {
                return this is MultiInstanceAdmin;
            }
        }

        /// <summary>
        /// <para>Gets this instance as a MultiInstanceAdmin, or <c>null</c>.</para>
        /// </summary>
        public MultiInstanceAdmin AsMultiInstanceAdmin
        {
            get
            {
                return this as MultiInstanceAdmin;
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
        /// <para>Encoder for  <see cref="TrustedNonTeamMemberType" />.</para>
        /// </summary>
        private class TrustedNonTeamMemberTypeEncoder : enc.StructEncoder<TrustedNonTeamMemberType>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(TrustedNonTeamMemberType value, enc.IJsonWriter writer)
            {
                if (value is MultiInstanceAdmin)
                {
                    WriteProperty(".tag", "multi_instance_admin", writer, enc.StringEncoder.Instance);
                    MultiInstanceAdmin.Encoder.EncodeFields((MultiInstanceAdmin)value, writer);
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
        /// <para>Decoder for  <see cref="TrustedNonTeamMemberType" />.</para>
        /// </summary>
        private class TrustedNonTeamMemberTypeDecoder : enc.UnionDecoder<TrustedNonTeamMemberType>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="TrustedNonTeamMemberType"
            /// />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override TrustedNonTeamMemberType Create()
            {
                return new TrustedNonTeamMemberType();
            }

            /// <summary>
            /// <para>Decode based on given tag.</para>
            /// </summary>
            /// <param name="tag">The tag.</param>
            /// <param name="reader">The json reader.</param>
            /// <returns>The decoded object.</returns>
            protected override TrustedNonTeamMemberType Decode(string tag, enc.IJsonReader reader)
            {
                switch (tag)
                {
                    case "multi_instance_admin":
                        return MultiInstanceAdmin.Decoder.DecodeFields(reader);
                    default:
                        return Other.Decoder.DecodeFields(reader);
                }
            }
        }

        #endregion

        /// <summary>
        /// <para>The multi instance admin object</para>
        /// </summary>
        public sealed class MultiInstanceAdmin : TrustedNonTeamMemberType
        {
            #pragma warning disable 108

            /// <summary>
            /// <para>The encoder instance.</para>
            /// </summary>
            internal static enc.StructEncoder<MultiInstanceAdmin> Encoder = new MultiInstanceAdminEncoder();

            /// <summary>
            /// <para>The decoder instance.</para>
            /// </summary>
            internal static enc.StructDecoder<MultiInstanceAdmin> Decoder = new MultiInstanceAdminDecoder();

            /// <summary>
            /// <para>Initializes a new instance of the <see cref="MultiInstanceAdmin" />
            /// class.</para>
            /// </summary>
            private MultiInstanceAdmin()
            {
            }

            /// <summary>
            /// <para>A singleton instance of MultiInstanceAdmin</para>
            /// </summary>
            public static readonly MultiInstanceAdmin Instance = new MultiInstanceAdmin();

            #region Encoder class

            /// <summary>
            /// <para>Encoder for  <see cref="MultiInstanceAdmin" />.</para>
            /// </summary>
            private class MultiInstanceAdminEncoder : enc.StructEncoder<MultiInstanceAdmin>
            {
                /// <summary>
                /// <para>Encode fields of given value.</para>
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="writer">The writer.</param>
                public override void EncodeFields(MultiInstanceAdmin value, enc.IJsonWriter writer)
                {
                }
            }

            #endregion

            #region Decoder class

            /// <summary>
            /// <para>Decoder for  <see cref="MultiInstanceAdmin" />.</para>
            /// </summary>
            private class MultiInstanceAdminDecoder : enc.StructDecoder<MultiInstanceAdmin>
            {
                /// <summary>
                /// <para>Create a new instance of type <see cref="MultiInstanceAdmin"
                /// />.</para>
                /// </summary>
                /// <returns>The struct instance.</returns>
                protected override MultiInstanceAdmin Create()
                {
                    return MultiInstanceAdmin.Instance;
                }

            }

            #endregion
        }

        /// <summary>
        /// <para>The other object</para>
        /// </summary>
        public sealed class Other : TrustedNonTeamMemberType
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
