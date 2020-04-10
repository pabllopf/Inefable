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
    /// <para>The shared note opened type object</para>
    /// </summary>
    public class SharedNoteOpenedType
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<SharedNoteOpenedType> Encoder = new SharedNoteOpenedTypeEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<SharedNoteOpenedType> Decoder = new SharedNoteOpenedTypeDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="SharedNoteOpenedType" />
        /// class.</para>
        /// </summary>
        /// <param name="description">The description</param>
        public SharedNoteOpenedType(string description)
        {
            if (description == null)
            {
                throw new sys.ArgumentNullException("description");
            }

            this.Description = description;
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="SharedNoteOpenedType" />
        /// class.</para>
        /// </summary>
        /// <remarks>This is to construct an instance of the object when
        /// deserializing.</remarks>
        [sys.ComponentModel.EditorBrowsable(sys.ComponentModel.EditorBrowsableState.Never)]
        public SharedNoteOpenedType()
        {
        }

        /// <summary>
        /// <para>Gets the description of the shared note opened type</para>
        /// </summary>
        public string Description { get; protected set; }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="SharedNoteOpenedType" />.</para>
        /// </summary>
        private class SharedNoteOpenedTypeEncoder : enc.StructEncoder<SharedNoteOpenedType>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(SharedNoteOpenedType value, enc.IJsonWriter writer)
            {
                WriteProperty("description", value.Description, writer, enc.StringEncoder.Instance);
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="SharedNoteOpenedType" />.</para>
        /// </summary>
        private class SharedNoteOpenedTypeDecoder : enc.StructDecoder<SharedNoteOpenedType>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="SharedNoteOpenedType" />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override SharedNoteOpenedType Create()
            {
                return new SharedNoteOpenedType();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(SharedNoteOpenedType value, string fieldName, enc.IJsonReader reader)
            {
                switch (fieldName)
                {
                    case "description":
                        value.Description = enc.StringDecoder.Instance.Decode(reader);
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
