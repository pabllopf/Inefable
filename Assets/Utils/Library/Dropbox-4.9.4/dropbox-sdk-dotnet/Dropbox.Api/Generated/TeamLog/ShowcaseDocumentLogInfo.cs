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
    /// <para>Showcase document's logged information.</para>
    /// </summary>
    public class ShowcaseDocumentLogInfo
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<ShowcaseDocumentLogInfo> Encoder = new ShowcaseDocumentLogInfoEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<ShowcaseDocumentLogInfo> Decoder = new ShowcaseDocumentLogInfoDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="ShowcaseDocumentLogInfo" />
        /// class.</para>
        /// </summary>
        /// <param name="showcaseId">Showcase document Id.</param>
        /// <param name="showcaseTitle">Showcase document title.</param>
        public ShowcaseDocumentLogInfo(string showcaseId,
                                       string showcaseTitle)
        {
            if (showcaseId == null)
            {
                throw new sys.ArgumentNullException("showcaseId");
            }

            if (showcaseTitle == null)
            {
                throw new sys.ArgumentNullException("showcaseTitle");
            }

            this.ShowcaseId = showcaseId;
            this.ShowcaseTitle = showcaseTitle;
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="ShowcaseDocumentLogInfo" />
        /// class.</para>
        /// </summary>
        /// <remarks>This is to construct an instance of the object when
        /// deserializing.</remarks>
        [sys.ComponentModel.EditorBrowsable(sys.ComponentModel.EditorBrowsableState.Never)]
        public ShowcaseDocumentLogInfo()
        {
        }

        /// <summary>
        /// <para>Showcase document Id.</para>
        /// </summary>
        public string ShowcaseId { get; protected set; }

        /// <summary>
        /// <para>Showcase document title.</para>
        /// </summary>
        public string ShowcaseTitle { get; protected set; }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="ShowcaseDocumentLogInfo" />.</para>
        /// </summary>
        private class ShowcaseDocumentLogInfoEncoder : enc.StructEncoder<ShowcaseDocumentLogInfo>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(ShowcaseDocumentLogInfo value, enc.IJsonWriter writer)
            {
                WriteProperty("showcase_id", value.ShowcaseId, writer, enc.StringEncoder.Instance);
                WriteProperty("showcase_title", value.ShowcaseTitle, writer, enc.StringEncoder.Instance);
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="ShowcaseDocumentLogInfo" />.</para>
        /// </summary>
        private class ShowcaseDocumentLogInfoDecoder : enc.StructDecoder<ShowcaseDocumentLogInfo>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="ShowcaseDocumentLogInfo"
            /// />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override ShowcaseDocumentLogInfo Create()
            {
                return new ShowcaseDocumentLogInfo();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(ShowcaseDocumentLogInfo value, string fieldName, enc.IJsonReader reader)
            {
                switch (fieldName)
                {
                    case "showcase_id":
                        value.ShowcaseId = enc.StringDecoder.Instance.Decode(reader);
                        break;
                    case "showcase_title":
                        value.ShowcaseTitle = enc.StringDecoder.Instance.Decode(reader);
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
