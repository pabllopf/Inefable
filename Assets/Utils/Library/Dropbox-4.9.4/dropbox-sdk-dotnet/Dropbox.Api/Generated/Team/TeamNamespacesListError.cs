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
    /// <para>The team namespaces list error object</para>
    /// </summary>
    public class TeamNamespacesListError
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<TeamNamespacesListError> Encoder = new TeamNamespacesListErrorEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<TeamNamespacesListError> Decoder = new TeamNamespacesListErrorDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="TeamNamespacesListError" />
        /// class.</para>
        /// </summary>
        public TeamNamespacesListError()
        {
        }

        /// <summary>
        /// <para>Gets a value indicating whether this instance is InvalidArg</para>
        /// </summary>
        public bool IsInvalidArg
        {
            get
            {
                return this is InvalidArg;
            }
        }

        /// <summary>
        /// <para>Gets this instance as a InvalidArg, or <c>null</c>.</para>
        /// </summary>
        public InvalidArg AsInvalidArg
        {
            get
            {
                return this as InvalidArg;
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
        /// <para>Encoder for  <see cref="TeamNamespacesListError" />.</para>
        /// </summary>
        private class TeamNamespacesListErrorEncoder : enc.StructEncoder<TeamNamespacesListError>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(TeamNamespacesListError value, enc.IJsonWriter writer)
            {
                if (value is InvalidArg)
                {
                    WriteProperty(".tag", "invalid_arg", writer, enc.StringEncoder.Instance);
                    InvalidArg.Encoder.EncodeFields((InvalidArg)value, writer);
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
        /// <para>Decoder for  <see cref="TeamNamespacesListError" />.</para>
        /// </summary>
        private class TeamNamespacesListErrorDecoder : enc.UnionDecoder<TeamNamespacesListError>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="TeamNamespacesListError"
            /// />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override TeamNamespacesListError Create()
            {
                return new TeamNamespacesListError();
            }

            /// <summary>
            /// <para>Decode based on given tag.</para>
            /// </summary>
            /// <param name="tag">The tag.</param>
            /// <param name="reader">The json reader.</param>
            /// <returns>The decoded object.</returns>
            protected override TeamNamespacesListError Decode(string tag, enc.IJsonReader reader)
            {
                switch (tag)
                {
                    case "invalid_arg":
                        return InvalidArg.Decoder.DecodeFields(reader);
                    default:
                        return Other.Decoder.DecodeFields(reader);
                }
            }
        }

        #endregion

        /// <summary>
        /// <para>Argument passed in is invalid.</para>
        /// </summary>
        public sealed class InvalidArg : TeamNamespacesListError
        {
            #pragma warning disable 108

            /// <summary>
            /// <para>The encoder instance.</para>
            /// </summary>
            internal static enc.StructEncoder<InvalidArg> Encoder = new InvalidArgEncoder();

            /// <summary>
            /// <para>The decoder instance.</para>
            /// </summary>
            internal static enc.StructDecoder<InvalidArg> Decoder = new InvalidArgDecoder();

            /// <summary>
            /// <para>Initializes a new instance of the <see cref="InvalidArg" /> class.</para>
            /// </summary>
            private InvalidArg()
            {
            }

            /// <summary>
            /// <para>A singleton instance of InvalidArg</para>
            /// </summary>
            public static readonly InvalidArg Instance = new InvalidArg();

            #region Encoder class

            /// <summary>
            /// <para>Encoder for  <see cref="InvalidArg" />.</para>
            /// </summary>
            private class InvalidArgEncoder : enc.StructEncoder<InvalidArg>
            {
                /// <summary>
                /// <para>Encode fields of given value.</para>
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="writer">The writer.</param>
                public override void EncodeFields(InvalidArg value, enc.IJsonWriter writer)
                {
                }
            }

            #endregion

            #region Decoder class

            /// <summary>
            /// <para>Decoder for  <see cref="InvalidArg" />.</para>
            /// </summary>
            private class InvalidArgDecoder : enc.StructDecoder<InvalidArg>
            {
                /// <summary>
                /// <para>Create a new instance of type <see cref="InvalidArg" />.</para>
                /// </summary>
                /// <returns>The struct instance.</returns>
                protected override InvalidArg Create()
                {
                    return InvalidArg.Instance;
                }

            }

            #endregion
        }

        /// <summary>
        /// <para>The other object</para>
        /// </summary>
        public sealed class Other : TeamNamespacesListError
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
