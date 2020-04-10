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
    /// <para>The team folder permanently delete type object</para>
    /// </summary>
    public class TeamFolderPermanentlyDeleteType
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<TeamFolderPermanentlyDeleteType> Encoder = new TeamFolderPermanentlyDeleteTypeEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<TeamFolderPermanentlyDeleteType> Decoder = new TeamFolderPermanentlyDeleteTypeDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="TeamFolderPermanentlyDeleteType"
        /// /> class.</para>
        /// </summary>
        /// <param name="description">The description</param>
        public TeamFolderPermanentlyDeleteType(string description)
        {
            if (description == null)
            {
                throw new sys.ArgumentNullException("description");
            }

            this.Description = description;
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="TeamFolderPermanentlyDeleteType"
        /// /> class.</para>
        /// </summary>
        /// <remarks>This is to construct an instance of the object when
        /// deserializing.</remarks>
        [sys.ComponentModel.EditorBrowsable(sys.ComponentModel.EditorBrowsableState.Never)]
        public TeamFolderPermanentlyDeleteType()
        {
        }

        /// <summary>
        /// <para>Gets the description of the team folder permanently delete type</para>
        /// </summary>
        public string Description { get; protected set; }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="TeamFolderPermanentlyDeleteType" />.</para>
        /// </summary>
        private class TeamFolderPermanentlyDeleteTypeEncoder : enc.StructEncoder<TeamFolderPermanentlyDeleteType>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(TeamFolderPermanentlyDeleteType value, enc.IJsonWriter writer)
            {
                WriteProperty("description", value.Description, writer, enc.StringEncoder.Instance);
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="TeamFolderPermanentlyDeleteType" />.</para>
        /// </summary>
        private class TeamFolderPermanentlyDeleteTypeDecoder : enc.StructDecoder<TeamFolderPermanentlyDeleteType>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="TeamFolderPermanentlyDeleteType"
            /// />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override TeamFolderPermanentlyDeleteType Create()
            {
                return new TeamFolderPermanentlyDeleteType();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(TeamFolderPermanentlyDeleteType value, string fieldName, enc.IJsonReader reader)
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
