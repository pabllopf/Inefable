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
    /// <para>Additional information indicating the action taken that caused status
    /// change.</para>
    /// </summary>
    public class ActionDetails
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<ActionDetails> Encoder = new ActionDetailsEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<ActionDetails> Decoder = new ActionDetailsDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="ActionDetails" /> class.</para>
        /// </summary>
        public ActionDetails()
        {
        }

        /// <summary>
        /// <para>Gets a value indicating whether this instance is TeamJoinDetails</para>
        /// </summary>
        public bool IsTeamJoinDetails
        {
            get
            {
                return this is TeamJoinDetails;
            }
        }

        /// <summary>
        /// <para>Gets this instance as a TeamJoinDetails, or <c>null</c>.</para>
        /// </summary>
        public TeamJoinDetails AsTeamJoinDetails
        {
            get
            {
                return this as TeamJoinDetails;
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether this instance is RemoveAction</para>
        /// </summary>
        public bool IsRemoveAction
        {
            get
            {
                return this is RemoveAction;
            }
        }

        /// <summary>
        /// <para>Gets this instance as a RemoveAction, or <c>null</c>.</para>
        /// </summary>
        public RemoveAction AsRemoveAction
        {
            get
            {
                return this as RemoveAction;
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
        /// <para>Encoder for  <see cref="ActionDetails" />.</para>
        /// </summary>
        private class ActionDetailsEncoder : enc.StructEncoder<ActionDetails>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(ActionDetails value, enc.IJsonWriter writer)
            {
                if (value is TeamJoinDetails)
                {
                    WriteProperty(".tag", "team_join_details", writer, enc.StringEncoder.Instance);
                    TeamJoinDetails.Encoder.EncodeFields((TeamJoinDetails)value, writer);
                    return;
                }
                if (value is RemoveAction)
                {
                    WriteProperty(".tag", "remove_action", writer, enc.StringEncoder.Instance);
                    RemoveAction.Encoder.EncodeFields((RemoveAction)value, writer);
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
        /// <para>Decoder for  <see cref="ActionDetails" />.</para>
        /// </summary>
        private class ActionDetailsDecoder : enc.UnionDecoder<ActionDetails>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="ActionDetails" />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override ActionDetails Create()
            {
                return new ActionDetails();
            }

            /// <summary>
            /// <para>Decode based on given tag.</para>
            /// </summary>
            /// <param name="tag">The tag.</param>
            /// <param name="reader">The json reader.</param>
            /// <returns>The decoded object.</returns>
            protected override ActionDetails Decode(string tag, enc.IJsonReader reader)
            {
                switch (tag)
                {
                    case "team_join_details":
                        return TeamJoinDetails.Decoder.DecodeFields(reader);
                    case "remove_action":
                        return RemoveAction.Decoder.DecodeFields(reader);
                    default:
                        return Other.Decoder.DecodeFields(reader);
                }
            }
        }

        #endregion

        /// <summary>
        /// <para>Additional information relevant when a new member joins the team.</para>
        /// </summary>
        public sealed class TeamJoinDetails : ActionDetails
        {
            #pragma warning disable 108

            /// <summary>
            /// <para>The encoder instance.</para>
            /// </summary>
            internal static enc.StructEncoder<TeamJoinDetails> Encoder = new TeamJoinDetailsEncoder();

            /// <summary>
            /// <para>The decoder instance.</para>
            /// </summary>
            internal static enc.StructDecoder<TeamJoinDetails> Decoder = new TeamJoinDetailsDecoder();

            /// <summary>
            /// <para>Initializes a new instance of the <see cref="TeamJoinDetails" />
            /// class.</para>
            /// </summary>
            /// <param name="value">The value</param>
            public TeamJoinDetails(JoinTeamDetails value)
            {
                this.Value = value;
            }
            /// <summary>
            /// <para>Initializes a new instance of the <see cref="TeamJoinDetails" />
            /// class.</para>
            /// </summary>
            private TeamJoinDetails()
            {
            }

            /// <summary>
            /// <para>Gets the value of this instance.</para>
            /// </summary>
            public JoinTeamDetails Value { get; private set; }

            #region Encoder class

            /// <summary>
            /// <para>Encoder for  <see cref="TeamJoinDetails" />.</para>
            /// </summary>
            private class TeamJoinDetailsEncoder : enc.StructEncoder<TeamJoinDetails>
            {
                /// <summary>
                /// <para>Encode fields of given value.</para>
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="writer">The writer.</param>
                public override void EncodeFields(TeamJoinDetails value, enc.IJsonWriter writer)
                {
                    WriteProperty("team_join_details", value.Value, writer, global::Dropbox.Api.TeamLog.JoinTeamDetails.Encoder);
                }
            }

            #endregion

            #region Decoder class

            /// <summary>
            /// <para>Decoder for  <see cref="TeamJoinDetails" />.</para>
            /// </summary>
            private class TeamJoinDetailsDecoder : enc.StructDecoder<TeamJoinDetails>
            {
                /// <summary>
                /// <para>Create a new instance of type <see cref="TeamJoinDetails" />.</para>
                /// </summary>
                /// <returns>The struct instance.</returns>
                protected override TeamJoinDetails Create()
                {
                    return new TeamJoinDetails();
                }

                /// <summary>
                /// <para>Decode fields without ensuring start and end object.</para>
                /// </summary>
                /// <param name="reader">The json reader.</param>
                /// <returns>The decoded object.</returns>
                public override TeamJoinDetails DecodeFields(enc.IJsonReader reader)
                {
                    return new TeamJoinDetails(global::Dropbox.Api.TeamLog.JoinTeamDetails.Decoder.DecodeFields(reader));
                }
            }

            #endregion
        }

        /// <summary>
        /// <para>Define how the user was removed from the team.</para>
        /// </summary>
        public sealed class RemoveAction : ActionDetails
        {
            #pragma warning disable 108

            /// <summary>
            /// <para>The encoder instance.</para>
            /// </summary>
            internal static enc.StructEncoder<RemoveAction> Encoder = new RemoveActionEncoder();

            /// <summary>
            /// <para>The decoder instance.</para>
            /// </summary>
            internal static enc.StructDecoder<RemoveAction> Decoder = new RemoveActionDecoder();

            /// <summary>
            /// <para>Initializes a new instance of the <see cref="RemoveAction" />
            /// class.</para>
            /// </summary>
            /// <param name="value">The value</param>
            public RemoveAction(MemberRemoveActionType value)
            {
                this.Value = value;
            }
            /// <summary>
            /// <para>Initializes a new instance of the <see cref="RemoveAction" />
            /// class.</para>
            /// </summary>
            private RemoveAction()
            {
            }

            /// <summary>
            /// <para>Gets the value of this instance.</para>
            /// </summary>
            public MemberRemoveActionType Value { get; private set; }

            #region Encoder class

            /// <summary>
            /// <para>Encoder for  <see cref="RemoveAction" />.</para>
            /// </summary>
            private class RemoveActionEncoder : enc.StructEncoder<RemoveAction>
            {
                /// <summary>
                /// <para>Encode fields of given value.</para>
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="writer">The writer.</param>
                public override void EncodeFields(RemoveAction value, enc.IJsonWriter writer)
                {
                    WriteProperty("remove_action", value.Value, writer, global::Dropbox.Api.TeamLog.MemberRemoveActionType.Encoder);
                }
            }

            #endregion

            #region Decoder class

            /// <summary>
            /// <para>Decoder for  <see cref="RemoveAction" />.</para>
            /// </summary>
            private class RemoveActionDecoder : enc.StructDecoder<RemoveAction>
            {
                /// <summary>
                /// <para>Create a new instance of type <see cref="RemoveAction" />.</para>
                /// </summary>
                /// <returns>The struct instance.</returns>
                protected override RemoveAction Create()
                {
                    return new RemoveAction();
                }

                /// <summary>
                /// <para>Set given field.</para>
                /// </summary>
                /// <param name="value">The field value.</param>
                /// <param name="fieldName">The field name.</param>
                /// <param name="reader">The json reader.</param>
                protected override void SetField(RemoveAction value, string fieldName, enc.IJsonReader reader)
                {
                    switch (fieldName)
                    {
                        case "remove_action":
                            value.Value = global::Dropbox.Api.TeamLog.MemberRemoveActionType.Decoder.Decode(reader);
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
        public sealed class Other : ActionDetails
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
