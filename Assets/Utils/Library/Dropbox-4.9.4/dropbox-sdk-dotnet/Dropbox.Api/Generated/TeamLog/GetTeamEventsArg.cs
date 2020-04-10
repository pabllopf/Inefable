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
    /// <para>The get team events arg object</para>
    /// </summary>
    public class GetTeamEventsArg
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<GetTeamEventsArg> Encoder = new GetTeamEventsArgEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<GetTeamEventsArg> Decoder = new GetTeamEventsArgDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="GetTeamEventsArg" />
        /// class.</para>
        /// </summary>
        /// <param name="limit">The maximal number of results to return per call. Note that
        /// some calls may not return <paramref name="limit" /> number of events, and may even
        /// return no events, even with `has_more` set to true. In this case, callers should
        /// fetch again using <see
        /// cref="Dropbox.Api.TeamLog.Routes.TeamLogTeamRoutes.GetEventsContinueAsync"
        /// />.</param>
        /// <param name="accountId">Filter the events by account ID. Return ony events with
        /// this account_id as either Actor, Context, or Participants.</param>
        /// <param name="time">Filter by time range.</param>
        /// <param name="category">Filter the returned events to a single category.</param>
        public GetTeamEventsArg(uint limit = 1000,
                                string accountId = null,
                                global::Dropbox.Api.TeamCommon.TimeRange time = null,
                                EventCategory category = null)
        {
            if (limit < 1U)
            {
                throw new sys.ArgumentOutOfRangeException("limit", "Value should be greater or equal than 1");
            }
            if (limit > 1000U)
            {
                throw new sys.ArgumentOutOfRangeException("limit", "Value should be less of equal than 1000");
            }

            if (accountId != null)
            {
                if (accountId.Length < 40)
                {
                    throw new sys.ArgumentOutOfRangeException("accountId", "Length should be at least 40");
                }
                if (accountId.Length > 40)
                {
                    throw new sys.ArgumentOutOfRangeException("accountId", "Length should be at most 40");
                }
            }

            this.Limit = limit;
            this.AccountId = accountId;
            this.Time = time;
            this.Category = category;
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="GetTeamEventsArg" />
        /// class.</para>
        /// </summary>
        /// <remarks>This is to construct an instance of the object when
        /// deserializing.</remarks>
        [sys.ComponentModel.EditorBrowsable(sys.ComponentModel.EditorBrowsableState.Never)]
        public GetTeamEventsArg()
        {
            this.Limit = 1000;
        }

        /// <summary>
        /// <para>The maximal number of results to return per call. Note that some calls may
        /// not return <see cref="Limit" /> number of events, and may even return no events,
        /// even with `has_more` set to true. In this case, callers should fetch again using
        /// <see cref="Dropbox.Api.TeamLog.Routes.TeamLogTeamRoutes.GetEventsContinueAsync"
        /// />.</para>
        /// </summary>
        public uint Limit { get; protected set; }

        /// <summary>
        /// <para>Filter the events by account ID. Return ony events with this account_id as
        /// either Actor, Context, or Participants.</para>
        /// </summary>
        public string AccountId { get; protected set; }

        /// <summary>
        /// <para>Filter by time range.</para>
        /// </summary>
        public global::Dropbox.Api.TeamCommon.TimeRange Time { get; protected set; }

        /// <summary>
        /// <para>Filter the returned events to a single category.</para>
        /// </summary>
        public EventCategory Category { get; protected set; }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="GetTeamEventsArg" />.</para>
        /// </summary>
        private class GetTeamEventsArgEncoder : enc.StructEncoder<GetTeamEventsArg>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(GetTeamEventsArg value, enc.IJsonWriter writer)
            {
                WriteProperty("limit", value.Limit, writer, enc.UInt32Encoder.Instance);
                if (value.AccountId != null)
                {
                    WriteProperty("account_id", value.AccountId, writer, enc.StringEncoder.Instance);
                }
                if (value.Time != null)
                {
                    WriteProperty("time", value.Time, writer, global::Dropbox.Api.TeamCommon.TimeRange.Encoder);
                }
                if (value.Category != null)
                {
                    WriteProperty("category", value.Category, writer, global::Dropbox.Api.TeamLog.EventCategory.Encoder);
                }
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="GetTeamEventsArg" />.</para>
        /// </summary>
        private class GetTeamEventsArgDecoder : enc.StructDecoder<GetTeamEventsArg>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="GetTeamEventsArg" />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override GetTeamEventsArg Create()
            {
                return new GetTeamEventsArg();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(GetTeamEventsArg value, string fieldName, enc.IJsonReader reader)
            {
                switch (fieldName)
                {
                    case "limit":
                        value.Limit = enc.UInt32Decoder.Instance.Decode(reader);
                        break;
                    case "account_id":
                        value.AccountId = enc.StringDecoder.Instance.Decode(reader);
                        break;
                    case "time":
                        value.Time = global::Dropbox.Api.TeamCommon.TimeRange.Decoder.Decode(reader);
                        break;
                    case "category":
                        value.Category = global::Dropbox.Api.TeamLog.EventCategory.Decoder.Decode(reader);
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
