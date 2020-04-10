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
    /// <para>Declined team member's invite to shared folder.</para>
    /// </summary>
    public class SharedFolderDeclineInvitationDetails
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<SharedFolderDeclineInvitationDetails> Encoder = new SharedFolderDeclineInvitationDetailsEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<SharedFolderDeclineInvitationDetails> Decoder = new SharedFolderDeclineInvitationDetailsDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see
        /// cref="SharedFolderDeclineInvitationDetails" /> class.</para>
        /// </summary>
        public SharedFolderDeclineInvitationDetails()
        {
        }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="SharedFolderDeclineInvitationDetails" />.</para>
        /// </summary>
        private class SharedFolderDeclineInvitationDetailsEncoder : enc.StructEncoder<SharedFolderDeclineInvitationDetails>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(SharedFolderDeclineInvitationDetails value, enc.IJsonWriter writer)
            {
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="SharedFolderDeclineInvitationDetails" />.</para>
        /// </summary>
        private class SharedFolderDeclineInvitationDetailsDecoder : enc.StructDecoder<SharedFolderDeclineInvitationDetails>
        {
            /// <summary>
            /// <para>Create a new instance of type <see
            /// cref="SharedFolderDeclineInvitationDetails" />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override SharedFolderDeclineInvitationDetails Create()
            {
                return new SharedFolderDeclineInvitationDetails();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(SharedFolderDeclineInvitationDetails value, string fieldName, enc.IJsonReader reader)
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
