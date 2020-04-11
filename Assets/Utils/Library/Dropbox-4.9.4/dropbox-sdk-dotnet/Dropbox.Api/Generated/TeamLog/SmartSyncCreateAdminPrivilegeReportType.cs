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
    /// <para>The smart sync create admin privilege report type object</para>
    /// </summary>
    public class SmartSyncCreateAdminPrivilegeReportType
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<SmartSyncCreateAdminPrivilegeReportType> Encoder = new SmartSyncCreateAdminPrivilegeReportTypeEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<SmartSyncCreateAdminPrivilegeReportType> Decoder = new SmartSyncCreateAdminPrivilegeReportTypeDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see
        /// cref="SmartSyncCreateAdminPrivilegeReportType" /> class.</para>
        /// </summary>
        /// <param name="description">The description</param>
        public SmartSyncCreateAdminPrivilegeReportType(string description)
        {
            if (description == null)
            {
                throw new sys.ArgumentNullException("description");
            }

            this.Description = description;
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see
        /// cref="SmartSyncCreateAdminPrivilegeReportType" /> class.</para>
        /// </summary>
        /// <remarks>This is to construct an instance of the object when
        /// deserializing.</remarks>
        [sys.ComponentModel.EditorBrowsable(sys.ComponentModel.EditorBrowsableState.Never)]
        public SmartSyncCreateAdminPrivilegeReportType()
        {
        }

        /// <summary>
        /// <para>Gets the description of the smart sync create admin privilege report
        /// type</para>
        /// </summary>
        public string Description { get; protected set; }

        #region Encoder class

        /// <summary>
        /// <para>Encoder for  <see cref="SmartSyncCreateAdminPrivilegeReportType" />.</para>
        /// </summary>
        private class SmartSyncCreateAdminPrivilegeReportTypeEncoder : enc.StructEncoder<SmartSyncCreateAdminPrivilegeReportType>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(SmartSyncCreateAdminPrivilegeReportType value, enc.IJsonWriter writer)
            {
                WriteProperty("description", value.Description, writer, enc.StringEncoder.Instance);
            }
        }

        #endregion


        #region Decoder class

        /// <summary>
        /// <para>Decoder for  <see cref="SmartSyncCreateAdminPrivilegeReportType" />.</para>
        /// </summary>
        private class SmartSyncCreateAdminPrivilegeReportTypeDecoder : enc.StructDecoder<SmartSyncCreateAdminPrivilegeReportType>
        {
            /// <summary>
            /// <para>Create a new instance of type <see
            /// cref="SmartSyncCreateAdminPrivilegeReportType" />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override SmartSyncCreateAdminPrivilegeReportType Create()
            {
                return new SmartSyncCreateAdminPrivilegeReportType();
            }

            /// <summary>
            /// <para>Set given field.</para>
            /// </summary>
            /// <param name="value">The field value.</param>
            /// <param name="fieldName">The field name.</param>
            /// <param name="reader">The json reader.</param>
            protected override void SetField(SmartSyncCreateAdminPrivilegeReportType value, string fieldName, enc.IJsonReader reader)
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