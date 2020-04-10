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
    /// <para>Permanently deleted archived team folder.</para>
    /// </summary>
    public class TeamFolderPermanentlyDeleteDetails
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<TeamFolderPermanentlyDeleteDetails> Encoder = new TeamFolderPermanentlyDeleteDetailsEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<TeamFolderPermanentlyDeleteDetails> Decoder = new TeamFolderPermanentlyDeleteDetailsDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see
        /// cref="TeamFolderPermanentlyDeleteDetails" /> class.</para>
        /// </summary>
        public TeamFolderPermanentlyDeleteDetails()
        {
        }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="TeamFolderPermanentlyDeleteDetails" />.</para>
        /// </summary>
        private class TeamFolderPermanentlyDeleteDetailsEncoder : enc.StructEncoder<TeamFolderPermanentlyDeleteDetails>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(TeamFolderPermanentlyDeleteDetails value, enc.IJsonWriter writer)
            {
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="TeamFolderPermanentlyDeleteDetails" />.</para>
        /// </summary>
        private class TeamFolderPermanentlyDeleteDetailsDecoder : enc.StructDecoder<TeamFolderPermanentlyDeleteDetails>
        {
            /// <summary>
            /// <para>Create a new instance of type <see
            /// cref="TeamFolderPermanentlyDeleteDetails" />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override TeamFolderPermanentlyDeleteDetails Create()
            {
                return new TeamFolderPermanentlyDeleteDetails();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(TeamFolderPermanentlyDeleteDetails value, string fieldName, enc.IJsonReader reader)
            {
                switch (fieldName)
                {
                    default:
                        reader.Skip();
                        break;
                }
            }
        }

        #endregion
    }
}
