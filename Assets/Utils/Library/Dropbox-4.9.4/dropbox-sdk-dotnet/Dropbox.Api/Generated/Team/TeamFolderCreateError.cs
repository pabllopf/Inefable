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
    /// <para>The team folder create error object</para>
    /// </summary>
    public class TeamFolderCreateError
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<TeamFolderCreateError> Encoder = new TeamFolderCreateErrorEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<TeamFolderCreateError> Decoder = new TeamFolderCreateErrorDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="TeamFolderCreateError" />
        /// class.</para>
        /// </summary>
        public TeamFolderCreateError()
        {
        }

        /// <summary>
        /// <para>Gets a value indicating whether this instance is InvalidFolderName</para>
        /// </summary>
        public bool IsInvalidFolderName
        {
            get
            {
                return this is InvalidFolderName;
            }
        }

        /// <summary>
        /// <para>Gets this instance as a InvalidFolderName, or <c>null</c>.</para>
        /// </summary>
        public InvalidFolderName AsInvalidFolderName
        {
            get
            {
                return this as InvalidFolderName;
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether this instance is FolderNameAlreadyUsed</para>
        /// </summary>
        public bool IsFolderNameAlreadyUsed
        {
            get
            {
                return this is FolderNameAlreadyUsed;
            }
        }

        /// <summary>
        /// <para>Gets this instance as a FolderNameAlreadyUsed, or <c>null</c>.</para>
        /// </summary>
        public FolderNameAlreadyUsed AsFolderNameAlreadyUsed
        {
            get
            {
                return this as FolderNameAlreadyUsed;
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether this instance is FolderNameReserved</para>
        /// </summary>
        public bool IsFolderNameReserved
        {
            get
            {
                return this is FolderNameReserved;
            }
        }

        /// <summary>
        /// <para>Gets this instance as a FolderNameReserved, or <c>null</c>.</para>
        /// </summary>
        public FolderNameReserved AsFolderNameReserved
        {
            get
            {
                return this as FolderNameReserved;
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether this instance is SyncSettingsError</para>
        /// </summary>
        public bool IsSyncSettingsError
        {
            get
            {
                return this is SyncSettingsError;
            }
        }

        /// <summary>
        /// <para>Gets this instance as a SyncSettingsError, or <c>null</c>.</para>
        /// </summary>
        public SyncSettingsError AsSyncSettingsError
        {
            get
            {
                return this as SyncSettingsError;
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
        /// <para>Encoder for  <see cref="TeamFolderCreateError" />.</para>
        /// </summary>
        private class TeamFolderCreateErrorEncoder : enc.StructEncoder<TeamFolderCreateError>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(TeamFolderCreateError value, enc.IJsonWriter writer)
            {
                if (value is InvalidFolderName)
                {
                    WriteProperty(".tag", "invalid_folder_name", writer, enc.StringEncoder.Instance);
                    InvalidFolderName.Encoder.EncodeFields((InvalidFolderName)value, writer);
                    return;
                }
                if (value is FolderNameAlreadyUsed)
                {
                    WriteProperty(".tag", "folder_name_already_used", writer, enc.StringEncoder.Instance);
                    FolderNameAlreadyUsed.Encoder.EncodeFields((FolderNameAlreadyUsed)value, writer);
                    return;
                }
                if (value is FolderNameReserved)
                {
                    WriteProperty(".tag", "folder_name_reserved", writer, enc.StringEncoder.Instance);
                    FolderNameReserved.Encoder.EncodeFields((FolderNameReserved)value, writer);
                    return;
                }
                if (value is SyncSettingsError)
                {
                    WriteProperty(".tag", "sync_settings_error", writer, enc.StringEncoder.Instance);
                    SyncSettingsError.Encoder.EncodeFields((SyncSettingsError)value, writer);
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
        /// <para>Decoder for  <see cref="TeamFolderCreateError" />.</para>
        /// </summary>
        private class TeamFolderCreateErrorDecoder : enc.UnionDecoder<TeamFolderCreateError>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="TeamFolderCreateError"
            /// />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override TeamFolderCreateError Create()
            {
                return new TeamFolderCreateError();
            }

            /// <summary>
            /// <para>Decode based on given tag.</para>
            /// </summary>
            /// <param name="tag">The tag.</param>
            /// <param name="reader">The json reader.</param>
            /// <returns>The decoded object.</returns>
            protected override TeamFolderCreateError Decode(string tag, enc.IJsonReader reader)
            {
                switch (tag)
                {
                    case "invalid_folder_name":
                        return InvalidFolderName.Decoder.DecodeFields(reader);
                    case "folder_name_already_used":
                        return FolderNameAlreadyUsed.Decoder.DecodeFields(reader);
                    case "folder_name_reserved":
                        return FolderNameReserved.Decoder.DecodeFields(reader);
                    case "sync_settings_error":
                        return SyncSettingsError.Decoder.DecodeFields(reader);
                    default:
                        return Other.Decoder.DecodeFields(reader);
                }
            }
        }

        #endregion

        /// <summary>
        /// <para>The provided name cannot be used.</para>
        /// </summary>
        public sealed class InvalidFolderName : TeamFolderCreateError
        {
            #pragma warning disable 108

            /// <summary>
            /// <para>The encoder instance.</para>
            /// </summary>
            internal static enc.StructEncoder<InvalidFolderName> Encoder = new InvalidFolderNameEncoder();

            /// <summary>
            /// <para>The decoder instance.</para>
            /// </summary>
            internal static enc.StructDecoder<InvalidFolderName> Decoder = new InvalidFolderNameDecoder();

            /// <summary>
            /// <para>Initializes a new instance of the <see cref="InvalidFolderName" />
            /// class.</para>
            /// </summary>
            private InvalidFolderName()
            {
            }

            /// <summary>
            /// <para>A singleton instance of InvalidFolderName</para>
            /// </summary>
            public static readonly InvalidFolderName Instance = new InvalidFolderName();

            #region Encoder class

            /// <summary>
            /// <para>Encoder for  <see cref="InvalidFolderName" />.</para>
            /// </summary>
            private class InvalidFolderNameEncoder : enc.StructEncoder<InvalidFolderName>
            {
                /// <summary>
                /// <para>Encode fields of given value.</para>
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="writer">The writer.</param>
                public override void EncodeFields(InvalidFolderName value, enc.IJsonWriter writer)
                {
                }
            }

            #endregion

            #region Decoder class

            /// <summary>
            /// <para>Decoder for  <see cref="InvalidFolderName" />.</para>
            /// </summary>
            private class InvalidFolderNameDecoder : enc.StructDecoder<InvalidFolderName>
            {
                /// <summary>
                /// <para>Create a new instance of type <see cref="InvalidFolderName"
                /// />.</para>
                /// </summary>
                /// <returns>The struct instance.</returns>
                protected override InvalidFolderName Create()
                {
                    return InvalidFolderName.Instance;
                }

            }

            #endregion
        }

        /// <summary>
        /// <para>There is already a team folder with the provided name.</para>
        /// </summary>
        public sealed class FolderNameAlreadyUsed : TeamFolderCreateError
        {
            #pragma warning disable 108

            /// <summary>
            /// <para>The encoder instance.</para>
            /// </summary>
            internal static enc.StructEncoder<FolderNameAlreadyUsed> Encoder = new FolderNameAlreadyUsedEncoder();

            /// <summary>
            /// <para>The decoder instance.</para>
            /// </summary>
            internal static enc.StructDecoder<FolderNameAlreadyUsed> Decoder = new FolderNameAlreadyUsedDecoder();

            /// <summary>
            /// <para>Initializes a new instance of the <see cref="FolderNameAlreadyUsed" />
            /// class.</para>
            /// </summary>
            private FolderNameAlreadyUsed()
            {
            }

            /// <summary>
            /// <para>A singleton instance of FolderNameAlreadyUsed</para>
            /// </summary>
            public static readonly FolderNameAlreadyUsed Instance = new FolderNameAlreadyUsed();

            #region Encoder class

            /// <summary>
            /// <para>Encoder for  <see cref="FolderNameAlreadyUsed" />.</para>
            /// </summary>
            private class FolderNameAlreadyUsedEncoder : enc.StructEncoder<FolderNameAlreadyUsed>
            {
                /// <summary>
                /// <para>Encode fields of given value.</para>
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="writer">The writer.</param>
                public override void EncodeFields(FolderNameAlreadyUsed value, enc.IJsonWriter writer)
                {
                }
            }

            #endregion

            #region Decoder class

            /// <summary>
            /// <para>Decoder for  <see cref="FolderNameAlreadyUsed" />.</para>
            /// </summary>
            private class FolderNameAlreadyUsedDecoder : enc.StructDecoder<FolderNameAlreadyUsed>
            {
                /// <summary>
                /// <para>Create a new instance of type <see cref="FolderNameAlreadyUsed"
                /// />.</para>
                /// </summary>
                /// <returns>The struct instance.</returns>
                protected override FolderNameAlreadyUsed Create()
                {
                    return FolderNameAlreadyUsed.Instance;
                }

            }

            #endregion
        }

        /// <summary>
        /// <para>The provided name cannot be used because it is reserved.</para>
        /// </summary>
        public sealed class FolderNameReserved : TeamFolderCreateError
        {
            #pragma warning disable 108

            /// <summary>
            /// <para>The encoder instance.</para>
            /// </summary>
            internal static enc.StructEncoder<FolderNameReserved> Encoder = new FolderNameReservedEncoder();

            /// <summary>
            /// <para>The decoder instance.</para>
            /// </summary>
            internal static enc.StructDecoder<FolderNameReserved> Decoder = new FolderNameReservedDecoder();

            /// <summary>
            /// <para>Initializes a new instance of the <see cref="FolderNameReserved" />
            /// class.</para>
            /// </summary>
            private FolderNameReserved()
            {
            }

            /// <summary>
            /// <para>A singleton instance of FolderNameReserved</para>
            /// </summary>
            public static readonly FolderNameReserved Instance = new FolderNameReserved();

            #region Encoder class

            /// <summary>
            /// <para>Encoder for  <see cref="FolderNameReserved" />.</para>
            /// </summary>
            private class FolderNameReservedEncoder : enc.StructEncoder<FolderNameReserved>
            {
                /// <summary>
                /// <para>Encode fields of given value.</para>
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="writer">The writer.</param>
                public override void EncodeFields(FolderNameReserved value, enc.IJsonWriter writer)
                {
                }
            }

            #endregion

            #region Decoder class

            /// <summary>
            /// <para>Decoder for  <see cref="FolderNameReserved" />.</para>
            /// </summary>
            private class FolderNameReservedDecoder : enc.StructDecoder<FolderNameReserved>
            {
                /// <summary>
                /// <para>Create a new instance of type <see cref="FolderNameReserved"
                /// />.</para>
                /// </summary>
                /// <returns>The struct instance.</returns>
                protected override FolderNameReserved Create()
                {
                    return FolderNameReserved.Instance;
                }

            }

            #endregion
        }

        /// <summary>
        /// <para>An error occurred setting the sync settings.</para>
        /// </summary>
        public sealed class SyncSettingsError : TeamFolderCreateError
        {
            #pragma warning disable 108

            /// <summary>
            /// <para>The encoder instance.</para>
            /// </summary>
            internal static enc.StructEncoder<SyncSettingsError> Encoder = new SyncSettingsErrorEncoder();

            /// <summary>
            /// <para>The decoder instance.</para>
            /// </summary>
            internal static enc.StructDecoder<SyncSettingsError> Decoder = new SyncSettingsErrorDecoder();

            /// <summary>
            /// <para>Initializes a new instance of the <see cref="SyncSettingsError" />
            /// class.</para>
            /// </summary>
            /// <param name="value">The value</param>
            public SyncSettingsError(global::Dropbox.Api.Files.SyncSettingsError value)
            {
                this.Value = value;
            }
            /// <summary>
            /// <para>Initializes a new instance of the <see cref="SyncSettingsError" />
            /// class.</para>
            /// </summary>
            private SyncSettingsError()
            {
            }

            /// <summary>
            /// <para>Gets the value of this instance.</para>
            /// </summary>
            public global::Dropbox.Api.Files.SyncSettingsError Value { get; private set; }

            #region Encoder class

            /// <summary>
            /// <para>Encoder for  <see cref="SyncSettingsError" />.</para>
            /// </summary>
            private class SyncSettingsErrorEncoder : enc.StructEncoder<SyncSettingsError>
            {
                /// <summary>
                /// <para>Encode fields of given value.</para>
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="writer">The writer.</param>
                public override void EncodeFields(SyncSettingsError value, enc.IJsonWriter writer)
                {
                    WriteProperty("sync_settings_error", value.Value, writer, global::Dropbox.Api.Files.SyncSettingsError.Encoder);
                }
            }

            #endregion

            #region Decoder class

            /// <summary>
            /// <para>Decoder for  <see cref="SyncSettingsError" />.</para>
            /// </summary>
            private class SyncSettingsErrorDecoder : enc.StructDecoder<SyncSettingsError>
            {
                /// <summary>
                /// <para>Create a new instance of type <see cref="SyncSettingsError"
                /// />.</para>
                /// </summary>
                /// <returns>The struct instance.</returns>
                protected override SyncSettingsError Create()
                {
                    return new SyncSettingsError();
                }

                /// <summary>
                /// <para>Set given field.</para>
                /// </summary>
                /// <param name="value">The field value.</param>
                /// <param name="fieldName">The field name.</param>
                /// <param name="reader">The json reader.</param>
                protected override void SetField(SyncSettingsError value, string fieldName, enc.IJsonReader reader)
                {
                    switch (fieldName)
                    {
                        case "sync_settings_error":
                            value.Value = global::Dropbox.Api.Files.SyncSettingsError.Decoder.Decode(reader);
                            break;
                        default:
                            reader.Skip();
                            break;
                    }
                }
            }

            #endregion
        }

        /// <summary>
        /// <para>The other object</para>
        /// </summary>
        public sealed class Other : TeamFolderCreateError
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
