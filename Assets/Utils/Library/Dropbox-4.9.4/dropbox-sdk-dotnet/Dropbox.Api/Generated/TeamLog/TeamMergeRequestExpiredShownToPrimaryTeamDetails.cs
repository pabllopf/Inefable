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
    /// <para>Team merge request expired.</para>
    /// </summary>
    public class TeamMergeRequestExpiredShownToPrimaryTeamDetails
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<TeamMergeRequestExpiredShownToPrimaryTeamDetails> Encoder = new TeamMergeRequestExpiredShownToPrimaryTeamDetailsEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<TeamMergeRequestExpiredShownToPrimaryTeamDetails> Decoder = new TeamMergeRequestExpiredShownToPrimaryTeamDetailsDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see
        /// cref="TeamMergeRequestExpiredShownToPrimaryTeamDetails" /> class.</para>
        /// </summary>
        /// <param name="secondaryTeam">The secondary team name.</param>
        /// <param name="sentBy">The name of the secondary team admin who sent the request
        /// originally.</param>
        public TeamMergeRequestExpiredShownToPrimaryTeamDetails(string secondaryTeam,
                                                                string sentBy)
        {
            if (secondaryTeam == null)
            {
                throw new sys.ArgumentNullException("secondaryTeam");
            }

            if (sentBy == null)
            {
                throw new sys.ArgumentNullException("sentBy");
            }

            this.SecondaryTeam = secondaryTeam;
            this.SentBy = sentBy;
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see
        /// cref="TeamMergeRequestExpiredShownToPrimaryTeamDetails" /> class.</para>
        /// </summary>
        /// <remarks>This is to construct an instance of the object when
        /// deserializing.</remarks>
        [sys.ComponentModel.EditorBrowsable(sys.ComponentModel.EditorBrowsableState.Never)]
        public TeamMergeRequestExpiredShownToPrimaryTeamDetails()
        {
        }

        /// <summary>
        /// <para>The secondary team name.</para>
        /// </summary>
        public string SecondaryTeam { get; protected set; }

        /// <summary>
        /// <para>The name of the secondary team admin who sent the request originally.</para>
        /// </summary>
        public string SentBy { get; protected set; }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="TeamMergeRequestExpiredShownToPrimaryTeamDetails"
        /// />.</para>
        /// </summary>
        private class TeamMergeRequestExpiredShownToPrimaryTeamDetailsEncoder : enc.StructEncoder<TeamMergeRequestExpiredShownToPrimaryTeamDetails>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(TeamMergeRequestExpiredShownToPrimaryTeamDetails value, enc.IJsonWriter writer)
            {
                WriteProperty("secondary_team", value.SecondaryTeam, writer, enc.StringEncoder.Instance);
                WriteProperty("sent_by", value.SentBy, writer, enc.StringEncoder.Instance);
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="TeamMergeRequestExpiredShownToPrimaryTeamDetails"
        /// />.</para>
        /// </summary>
        private class TeamMergeRequestExpiredShownToPrimaryTeamDetailsDecoder : enc.StructDecoder<TeamMergeRequestExpiredShownToPrimaryTeamDetails>
        {
            /// <summary>
            /// <para>Create a new instance of type <see
            /// cref="TeamMergeRequestExpiredShownToPrimaryTeamDetails" />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override TeamMergeRequestExpiredShownToPrimaryTeamDetails Create()
            {
                return new TeamMergeRequestExpiredShownToPrimaryTeamDetails();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(TeamMergeRequestExpiredShownToPrimaryTeamDetails value, string fieldName, enc.IJsonReader reader)
            {
                switch (fieldName)
                {
                    case "secondary_team":
                        value.SecondaryTeam = enc.StringDecoder.Instance.Decode(reader);
                        break;
                    case "sent_by":
                        value.SentBy = enc.StringDecoder.Instance.Decode(reader);
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
