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
    /// <para>The create shared link with settings error object</para>
    /// </summary>
    public class CreateSharedLinkWithSettingsError
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<CreateSharedLinkWithSettingsError> Encoder = new CreateSharedLinkWithSettingsErrorEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<CreateSharedLinkWithSettingsError> Decoder = new CreateSharedLinkWithSettingsErrorDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see
        /// cref="CreateSharedLinkWithSettingsError" /> class.</para>
        /// </summary>
        public CreateSharedLinkWithSettingsError()
        {
        }

        /// <summary>
        /// <para>Gets a value indicating whether this instance is Path</para>
        /// </summary>
        public bool IsPath
        {
            get
            {
                return this is Path;
            }
        }

        /// <summary>
        /// <para>Gets this instance as a Path, or <c>null</c>.</para>
        /// </summary>
        public Path AsPath
        {
            get
            {
                return this as Path;
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether this instance is EmailNotVerified</para>
        /// </summary>
        public bool IsEmailNotVerified
        {
            get
            {
                return this is EmailNotVerified;
            }
        }

        /// <summary>
        /// <para>Gets this instance as a EmailNotVerified, or <c>null</c>.</para>
        /// </summary>
        public EmailNotVerified AsEmailNotVerified
        {
            get
            {
                return this as EmailNotVerified;
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether this instance is
        /// SharedLinkAlreadyExists</para>
        /// </summary>
        public bool IsSharedLinkAlreadyExists
        {
            get
            {
                return this is SharedLinkAlreadyExists;
            }
        }

        /// <summary>
        /// <para>Gets this instance as a SharedLinkAlreadyExists, or <c>null</c>.</para>
        /// </summary>
        public SharedLinkAlreadyExists AsSharedLinkAlreadyExists
        {
            get
            {
                return this as SharedLinkAlreadyExists;
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether this instance is SettingsError</para>
        /// </summary>
        public bool IsSettingsError
        {
            get
            {
                return this is SettingsError;
            }
        }

        /// <summary>
        /// <para>Gets this instance as a SettingsError, or <c>null</c>.</para>
        /// </summary>
        public SettingsError AsSettingsError
        {
            get
            {
                return this as SettingsError;
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether this instance is AccessDenied</para>
        /// </summary>
        public bool IsAccessDenied
        {
            get
            {
                return this is AccessDenied;
            }
        }

        /// <summary>
        /// <para>Gets this instance as a AccessDenied, or <c>null</c>.</para>
        /// </summary>
        public AccessDenied AsAccessDenied
        {
            get
            {
                return this as AccessDenied;
            }
        }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="CreateSharedLinkWithSettingsError" />.</para>
        /// </summary>
        private class CreateSharedLinkWithSettingsErrorEncoder : enc.StructEncoder<CreateSharedLinkWithSettingsError>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(CreateSharedLinkWithSettingsError value, enc.IJsonWriter writer)
            {
                if (value is Path)
                {
                    WriteProperty(".tag", "path", writer, enc.StringEncoder.Instance);
                    Path.Encoder.EncodeFields((Path)value, writer);
                    return;
                }
                if (value is EmailNotVerified)
                {
                    WriteProperty(".tag", "email_not_verified", writer, enc.StringEncoder.Instance);
                    EmailNotVerified.Encoder.EncodeFields((EmailNotVerified)value, writer);
                    return;
                }
                if (value is SharedLinkAlreadyExists)
                {
                    WriteProperty(".tag", "shared_link_already_exists", writer, enc.StringEncoder.Instance);
                    SharedLinkAlreadyExists.Encoder.EncodeFields((SharedLinkAlreadyExists)value, writer);
                    return;
                }
                if (value is SettingsError)
                {
                    WriteProperty(".tag", "settings_error", writer, enc.StringEncoder.Instance);
                    SettingsError.Encoder.EncodeFields((SettingsError)value, writer);
                    return;
                }
                if (value is AccessDenied)
                {
                    WriteProperty(".tag", "access_denied", writer, enc.StringEncoder.Instance);
                    AccessDenied.Encoder.EncodeFields((AccessDenied)value, writer);
                    return;
                }
                throw new sys.InvalidOperationException();
            }
        }

        #endregion

        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="CreateSharedLinkWithSettingsError" />.</para>
        /// </summary>
        private class CreateSharedLinkWithSettingsErrorDecoder : enc.UnionDecoder<CreateSharedLinkWithSettingsError>
        {
            /// <summary>
            /// <para>Create a new instance of type <see
            /// cref="CreateSharedLinkWithSettingsError" />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override CreateSharedLinkWithSettingsError Create()
            {
                return new CreateSharedLinkWithSettingsError();
            }

            /// <summary>
            /// <para>Decode based on given tag.</para>
            /// </summary>
            /// <param name="tag">The tag.</param>
            /// <param name="reader">The json reader.</param>
            /// <returns>The decoded object.</returns>
            protected override CreateSharedLinkWithSettingsError Decode(string tag, enc.IJsonReader reader)
            {
                switch (tag)
                {
                    case "path":
                        return Path.Decoder.DecodeFields(reader);
                    case "email_not_verified":
                        return EmailNotVerified.Decoder.DecodeFields(reader);
                    case "shared_link_already_exists":
                        return SharedLinkAlreadyExists.Decoder.DecodeFields(reader);
                    case "settings_error":
                        return SettingsError.Decoder.DecodeFields(reader);
                    case "access_denied":
                        return AccessDenied.Decoder.DecodeFields(reader);
                    default:
                        throw new sys.InvalidOperationException();
                }
            }
        }

        #endregion

        /// <summary>
        /// <para>The path object</para>
        /// </summary>
        public sealed class Path : CreateSharedLinkWithSettingsError
        {
            #pragma warning disable 108

            /// <summary>
            /// <para>The encoder instance.</para>
            /// </summary>
            internal static enc.StructEncoder<Path> Encoder = new PathEncoder();

            /// <summary>
            /// <para>The decoder instance.</para>
            /// </summary>
            internal static enc.StructDecoder<Path> Decoder = new PathDecoder();

            /// <summary>
            /// <para>Initializes a new instance of the <see cref="Path" /> class.</para>
            /// </summary>
            /// <param name="value">The value</param>
            public Path(global::Dropbox.Api.Files.LookupError value)
            {
                this.Value = value;
            }
            /// <summary>
            /// <para>Initializes a new instance of the <see cref="Path" /> class.</para>
            /// </summary>
            private Path()
            {
            }

            /// <summary>
            /// <para>Gets the value of this instance.</para>
            /// </summary>
            public global::Dropbox.Api.Files.LookupError Value { get; private set; }

            #region Encoder class

            /// <summary>
            /// <para>Encoder for  <see cref="Path" />.</para>
            /// </summary>
            private class PathEncoder : enc.StructEncoder<Path>
            {
                /// <summary>
                /// <para>Encode fields of given value.</para>
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="writer">The writer.</param>
                public override void EncodeFields(Path value, enc.IJsonWriter writer)
                {
                    WriteProperty("path", value.Value, writer, global::Dropbox.Api.Files.LookupError.Encoder);
                }
            }

            #endregion

            #region Decoder class

            /// <summary>
            /// <para>Decoder for  <see cref="Path" />.</para>
            /// </summary>
            private class PathDecoder : enc.StructDecoder<Path>
            {
                /// <summary>
                /// <para>Create a new instance of type <see cref="Path" />.</para>
                /// </summary>
                /// <returns>The struct instance.</returns>
                protected override Path Create()
                {
                    return new Path();
                }

                /// <summary>
                /// <para>Set given field.</para>
                /// </summary>
                /// <param name="value">The field value.</param>
                /// <param name="fieldName">The field name.</param>
                /// <param name="reader">The json reader.</param>
                protected override void SetField(Path value, string fieldName, enc.IJsonReader reader)
                {
                    switch (fieldName)
                    {
                        case "path":
                            value.Value = global::Dropbox.Api.Files.LookupError.Decoder.Decode(reader);
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
        /// <para>User's email should be verified.</para>
        /// </summary>
        public sealed class EmailNotVerified : CreateSharedLinkWithSettingsError
        {
            #pragma warning disable 108

            /// <summary>
            /// <para>The encoder instance.</para>
            /// </summary>
            internal static enc.StructEncoder<EmailNotVerified> Encoder = new EmailNotVerifiedEncoder();

            /// <summary>
            /// <para>The decoder instance.</para>
            /// </summary>
            internal static enc.StructDecoder<EmailNotVerified> Decoder = new EmailNotVerifiedDecoder();

            /// <summary>
            /// <para>Initializes a new instance of the <see cref="EmailNotVerified" />
            /// class.</para>
            /// </summary>
            private EmailNotVerified()
            {
            }

            /// <summary>
            /// <para>A singleton instance of EmailNotVerified</para>
            /// </summary>
            public static readonly EmailNotVerified Instance = new EmailNotVerified();

            #region Encoder class

            /// <summary>
            /// <para>Encoder for  <see cref="EmailNotVerified" />.</para>
            /// </summary>
            private class EmailNotVerifiedEncoder : enc.StructEncoder<EmailNotVerified>
            {
                /// <summary>
                /// <para>Encode fields of given value.</para>
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="writer">The writer.</param>
                public override void EncodeFields(EmailNotVerified value, enc.IJsonWriter writer)
                {
                }
            }

            #endregion

            #region Decoder class

            /// <summary>
            /// <para>Decoder for  <see cref="EmailNotVerified" />.</para>
            /// </summary>
            private class EmailNotVerifiedDecoder : enc.StructDecoder<EmailNotVerified>
            {
                /// <summary>
                /// <para>Create a new instance of type <see cref="EmailNotVerified" />.</para>
                /// </summary>
                /// <returns>The struct instance.</returns>
                protected override EmailNotVerified Create()
                {
                    return EmailNotVerified.Instance;
                }

            }

            #endregion
        }

        /// <summary>
        /// <para>The shared link already exists. You can call <see
        /// cref="Dropbox.Api.Sharing.Routes.SharingUserRoutes.ListSharedLinksAsync" /> to get
        /// the  existing link, or use the provided metadata if it is returned.</para>
        /// </summary>
        public sealed class SharedLinkAlreadyExists : CreateSharedLinkWithSettingsError
        {
            #pragma warning disable 108

            /// <summary>
            /// <para>The encoder instance.</para>
            /// </summary>
            internal static enc.StructEncoder<SharedLinkAlreadyExists> Encoder = new SharedLinkAlreadyExistsEncoder();

            /// <summary>
            /// <para>The decoder instance.</para>
            /// </summary>
            internal static enc.StructDecoder<SharedLinkAlreadyExists> Decoder = new SharedLinkAlreadyExistsDecoder();

            /// <summary>
            /// <para>Initializes a new instance of the <see cref="SharedLinkAlreadyExists" />
            /// class.</para>
            /// </summary>
            /// <param name="value">The value</param>
            public SharedLinkAlreadyExists(SharedLinkAlreadyExistsMetadata value)
            {
                this.Value = value;
            }
            /// <summary>
            /// <para>Initializes a new instance of the <see cref="SharedLinkAlreadyExists" />
            /// class.</para>
            /// </summary>
            private SharedLinkAlreadyExists()
            {
            }

            /// <summary>
            /// <para>Gets the value of this instance.</para>
            /// </summary>
            public SharedLinkAlreadyExistsMetadata Value { get; private set; }

            #region Encoder class

            /// <summary>
            /// <para>Encoder for  <see cref="SharedLinkAlreadyExists" />.</para>
            /// </summary>
            private class SharedLinkAlreadyExistsEncoder : enc.StructEncoder<SharedLinkAlreadyExists>
            {
                /// <summary>
                /// <para>Encode fields of given value.</para>
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="writer">The writer.</param>
                public override void EncodeFields(SharedLinkAlreadyExists value, enc.IJsonWriter writer)
                {
                    if (value.Value != null)
                    {
                        WriteProperty("shared_link_already_exists", value.Value, writer, global::Dropbox.Api.Sharing.SharedLinkAlreadyExistsMetadata.Encoder);
                    }
                }
            }

            #endregion

            #region Decoder class

            /// <summary>
            /// <para>Decoder for  <see cref="SharedLinkAlreadyExists" />.</para>
            /// </summary>
            private class SharedLinkAlreadyExistsDecoder : enc.StructDecoder<SharedLinkAlreadyExists>
            {
                /// <summary>
                /// <para>Create a new instance of type <see cref="SharedLinkAlreadyExists"
                /// />.</para>
                /// </summary>
                /// <returns>The struct instance.</returns>
                protected override SharedLinkAlreadyExists Create()
                {
                    return new SharedLinkAlreadyExists();
                }

                /// <summary>
                /// <para>Set given field.</para>
                /// </summary>
                /// <param name="value">The field value.</param>
                /// <param name="fieldName">The field name.</param>
                /// <param name="reader">The json reader.</param>
                protected override void SetField(SharedLinkAlreadyExists value, string fieldName, enc.IJsonReader reader)
                {
                    switch (fieldName)
                    {
                        case "shared_link_already_exists":
                            value.Value = global::Dropbox.Api.Sharing.SharedLinkAlreadyExistsMetadata.Decoder.Decode(reader);
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
        /// <para>There is an error with the given settings.</para>
        /// </summary>
        public sealed class SettingsError : CreateSharedLinkWithSettingsError
        {
            #pragma warning disable 108

            /// <summary>
            /// <para>The encoder instance.</para>
            /// </summary>
            internal static enc.StructEncoder<SettingsError> Encoder = new SettingsErrorEncoder();

            /// <summary>
            /// <para>The decoder instance.</para>
            /// </summary>
            internal static enc.StructDecoder<SettingsError> Decoder = new SettingsErrorDecoder();

            /// <summary>
            /// <para>Initializes a new instance of the <see cref="SettingsError" />
            /// class.</para>
            /// </summary>
            /// <param name="value">The value</param>
            public SettingsError(SharedLinkSettingsError value)
            {
                this.Value = value;
            }
            /// <summary>
            /// <para>Initializes a new instance of the <see cref="SettingsError" />
            /// class.</para>
            /// </summary>
            private SettingsError()
            {
            }

            /// <summary>
            /// <para>Gets the value of this instance.</para>
            /// </summary>
            public SharedLinkSettingsError Value { get; private set; }

            #region Encoder class

            /// <summary>
            /// <para>Encoder for  <see cref="SettingsError" />.</para>
            /// </summary>
            private class SettingsErrorEncoder : enc.StructEncoder<SettingsError>
            {
                /// <summary>
                /// <para>Encode fields of given value.</para>
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="writer">The writer.</param>
                public override void EncodeFields(SettingsError value, enc.IJsonWriter writer)
                {
                    WriteProperty("settings_error", value.Value, writer, global::Dropbox.Api.Sharing.SharedLinkSettingsError.Encoder);
                }
            }

            #endregion

            #region Decoder class

            /// <summary>
            /// <para>Decoder for  <see cref="SettingsError" />.</para>
            /// </summary>
            private class SettingsErrorDecoder : enc.StructDecoder<SettingsError>
            {
                /// <summary>
                /// <para>Create a new instance of type <see cref="SettingsError" />.</para>
                /// </summary>
                /// <returns>The struct instance.</returns>
                protected override SettingsError Create()
                {
                    return new SettingsError();
                }

                /// <summary>
                /// <para>Set given field.</para>
                /// </summary>
                /// <param name="value">The field value.</param>
                /// <param name="fieldName">The field name.</param>
                /// <param name="reader">The json reader.</param>
                protected override void SetField(SettingsError value, string fieldName, enc.IJsonReader reader)
                {
                    switch (fieldName)
                    {
                        case "settings_error":
                            value.Value = global::Dropbox.Api.Sharing.SharedLinkSettingsError.Decoder.Decode(reader);
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
        /// <para>Access to the requested path is forbidden.</para>
        /// </summary>
        public sealed class AccessDenied : CreateSharedLinkWithSettingsError
        {
            #pragma warning disable 108

            /// <summary>
            /// <para>The encoder instance.</para>
            /// </summary>
            internal static enc.StructEncoder<AccessDenied> Encoder = new AccessDeniedEncoder();

            /// <summary>
            /// <para>The decoder instance.</para>
            /// </summary>
            internal static enc.StructDecoder<AccessDenied> Decoder = new AccessDeniedDecoder();

            /// <summary>
            /// <para>Initializes a new instance of the <see cref="AccessDenied" />
            /// class.</para>
            /// </summary>
            private AccessDenied()
            {
            }

            /// <summary>
            /// <para>A singleton instance of AccessDenied</para>
            /// </summary>
            public static readonly AccessDenied Instance = new AccessDenied();

            #region Encoder class

            /// <summary>
            /// <para>Encoder for  <see cref="AccessDenied" />.</para>
            /// </summary>
            private class AccessDeniedEncoder : enc.StructEncoder<AccessDenied>
            {
                /// <summary>
                /// <para>Encode fields of given value.</para>
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="writer">The writer.</param>
                public override void EncodeFields(AccessDenied value, enc.IJsonWriter writer)
                {
                }
            }

            #endregion

            #region Decoder class

            /// <summary>
            /// <para>Decoder for  <see cref="AccessDenied" />.</para>
            /// </summary>
            private class AccessDeniedDecoder : enc.StructDecoder<AccessDenied>
            {
                /// <summary>
                /// <para>Create a new instance of type <see cref="AccessDenied" />.</para>
                /// </summary>
                /// <returns>The struct instance.</returns>
                protected override AccessDenied Create()
                {
                    return AccessDenied.Instance;
                }

            }

            #endregion
        }
    }
}
