// <auto-generated>
// Auto-generated by StoneAPI, do not modify.
// </auto-generated>

namespace Dropbox.Api.Team
{
    using sys = System;
    using col = System.Collections.Generic;
    using re = System.Text.RegularExpressions;

    using enc = Dropbox.Api.Stone;

    /// <summary>
    /// <para>The mobile client platform object</para>
    /// </summary>
    public class MobileClientPlatform
    {
        #pragma warning disable 108

        /// <summary>
        /// <para>The encoder instance.</para>
        /// </summary>
        internal static enc.StructEncoder<MobileClientPlatform> Encoder = new MobileClientPlatformEncoder();

        /// <summary>
        /// <para>The decoder instance.</para>
        /// </summary>
        internal static enc.StructDecoder<MobileClientPlatform> Decoder = new MobileClientPlatformDecoder();

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="MobileClientPlatform" />
        /// class.</para>
        /// </summary>
        public MobileClientPlatform()
        {
        }

        /// <summary>
        /// <para>Gets a value indicating whether this instance is Iphone</para>
        /// </summary>
        public bool IsIphone
        {
            get
            {
                return this is Iphone;
            }
        }

        /// <summary>
        /// <para>Gets this instance as a Iphone, or <c>null</c>.</para>
        /// </summary>
        public Iphone AsIphone
        {
            get
            {
                return this as Iphone;
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether this instance is Ipad</para>
        /// </summary>
        public bool IsIpad
        {
            get
            {
                return this is Ipad;
            }
        }

        /// <summary>
        /// <para>Gets this instance as a Ipad, or <c>null</c>.</para>
        /// </summary>
        public Ipad AsIpad
        {
            get
            {
                return this as Ipad;
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether this instance is Android</para>
        /// </summary>
        public bool IsAndroid
        {
            get
            {
                return this is Android;
            }
        }

        /// <summary>
        /// <para>Gets this instance as a Android, or <c>null</c>.</para>
        /// </summary>
        public Android AsAndroid
        {
            get
            {
                return this as Android;
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether this instance is WindowsPhone</para>
        /// </summary>
        public bool IsWindowsPhone
        {
            get
            {
                return this is WindowsPhone;
            }
        }

        /// <summary>
        /// <para>Gets this instance as a WindowsPhone, or <c>null</c>.</para>
        /// </summary>
        public WindowsPhone AsWindowsPhone
        {
            get
            {
                return this as WindowsPhone;
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether this instance is Blackberry</para>
        /// </summary>
        public bool IsBlackberry
        {
            get
            {
                return this is Blackberry;
            }
        }

        /// <summary>
        /// <para>Gets this instance as a Blackberry, or <c>null</c>.</para>
        /// </summary>
        public Blackberry AsBlackberry
        {
            get
            {
                return this as Blackberry;
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
        /// <para>Encoder for  <see cref="MobileClientPlatform" />.</para>
        /// </summary>
        private class MobileClientPlatformEncoder : enc.StructEncoder<MobileClientPlatform>
        {
            /// <summary>
            /// <para>Encode fields of given value.</para>
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="writer">The writer.</param>
            public override void EncodeFields(MobileClientPlatform value, enc.IJsonWriter writer)
            {
                if (value is Iphone)
                {
                    WriteProperty(".tag", "iphone", writer, enc.StringEncoder.Instance);
                    Iphone.Encoder.EncodeFields((Iphone)value, writer);
                    return;
                }
                if (value is Ipad)
                {
                    WriteProperty(".tag", "ipad", writer, enc.StringEncoder.Instance);
                    Ipad.Encoder.EncodeFields((Ipad)value, writer);
                    return;
                }
                if (value is Android)
                {
                    WriteProperty(".tag", "android", writer, enc.StringEncoder.Instance);
                    Android.Encoder.EncodeFields((Android)value, writer);
                    return;
                }
                if (value is WindowsPhone)
                {
                    WriteProperty(".tag", "windows_phone", writer, enc.StringEncoder.Instance);
                    WindowsPhone.Encoder.EncodeFields((WindowsPhone)value, writer);
                    return;
                }
                if (value is Blackberry)
                {
                    WriteProperty(".tag", "blackberry", writer, enc.StringEncoder.Instance);
                    Blackberry.Encoder.EncodeFields((Blackberry)value, writer);
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
        /// <para>Decoder for  <see cref="MobileClientPlatform" />.</para>
        /// </summary>
        private class MobileClientPlatformDecoder : enc.UnionDecoder<MobileClientPlatform>
        {
            /// <summary>
            /// <para>Create a new instance of type <see cref="MobileClientPlatform" />.</para>
            /// </summary>
            /// <returns>The struct instance.</returns>
            protected override MobileClientPlatform Create()
            {
                return new MobileClientPlatform();
            }

            /// <summary>
            /// <para>Decode based on given tag.</para>
            /// </summary>
            /// <param name="tag">The tag.</param>
            /// <param name="reader">The json reader.</param>
            /// <returns>The decoded object.</returns>
            protected override MobileClientPlatform Decode(string tag, enc.IJsonReader reader)
            {
                switch (tag)
                {
                    case "iphone":
                        return Iphone.Decoder.DecodeFields(reader);
                    case "ipad":
                        return Ipad.Decoder.DecodeFields(reader);
                    case "android":
                        return Android.Decoder.DecodeFields(reader);
                    case "windows_phone":
                        return WindowsPhone.Decoder.DecodeFields(reader);
                    case "blackberry":
                        return Blackberry.Decoder.DecodeFields(reader);
                    default:
                        return Other.Decoder.DecodeFields(reader);
                }
            }
        }

        #endregion

        /// <summary>
        /// <para>Official Dropbox iPhone client.</para>
        /// </summary>
        public sealed class Iphone : MobileClientPlatform
        {
            #pragma warning disable 108

            /// <summary>
            /// <para>The encoder instance.</para>
            /// </summary>
            internal static enc.StructEncoder<Iphone> Encoder = new IphoneEncoder();

            /// <summary>
            /// <para>The decoder instance.</para>
            /// </summary>
            internal static enc.StructDecoder<Iphone> Decoder = new IphoneDecoder();

            /// <summary>
            /// <para>Initializes a new instance of the <see cref="Iphone" /> class.</para>
            /// </summary>
            private Iphone()
            {
            }

            /// <summary>
            /// <para>A singleton instance of Iphone</para>
            /// </summary>
            public static readonly Iphone Instance = new Iphone();

            #region Encoder class

            /// <summary>
            /// <para>Encoder for  <see cref="Iphone" />.</para>
            /// </summary>
            private class IphoneEncoder : enc.StructEncoder<Iphone>
            {
                /// <summary>
                /// <para>Encode fields of given value.</para>
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="writer">The writer.</param>
                public override void EncodeFields(Iphone value, enc.IJsonWriter writer)
                {
                }
            }

            #endregion

            #region Decoder class

            /// <summary>
            /// <para>Decoder for  <see cref="Iphone" />.</para>
            /// </summary>
            private class IphoneDecoder : enc.StructDecoder<Iphone>
            {
                /// <summary>
                /// <para>Create a new instance of type <see cref="Iphone" />.</para>
                /// </summary>
                /// <returns>The struct instance.</returns>
                protected override Iphone Create()
                {
                    return Iphone.Instance;
                }

            }

            #endregion
        }

        /// <summary>
        /// <para>Official Dropbox iPad client.</para>
        /// </summary>
        public sealed class Ipad : MobileClientPlatform
        {
            #pragma warning disable 108

            /// <summary>
            /// <para>The encoder instance.</para>
            /// </summary>
            internal static enc.StructEncoder<Ipad> Encoder = new IpadEncoder();

            /// <summary>
            /// <para>The decoder instance.</para>
            /// </summary>
            internal static enc.StructDecoder<Ipad> Decoder = new IpadDecoder();

            /// <summary>
            /// <para>Initializes a new instance of the <see cref="Ipad" /> class.</para>
            /// </summary>
            private Ipad()
            {
            }

            /// <summary>
            /// <para>A singleton instance of Ipad</para>
            /// </summary>
            public static readonly Ipad Instance = new Ipad();

            #region Encoder class

            /// <summary>
            /// <para>Encoder for  <see cref="Ipad" />.</para>
            /// </summary>
            private class IpadEncoder : enc.StructEncoder<Ipad>
            {
                /// <summary>
                /// <para>Encode fields of given value.</para>
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="writer">The writer.</param>
                public override void EncodeFields(Ipad value, enc.IJsonWriter writer)
                {
                }
            }

            #endregion

            #region Decoder class

            /// <summary>
            /// <para>Decoder for  <see cref="Ipad" />.</para>
            /// </summary>
            private class IpadDecoder : enc.StructDecoder<Ipad>
            {
                /// <summary>
                /// <para>Create a new instance of type <see cref="Ipad" />.</para>
                /// </summary>
                /// <returns>The struct instance.</returns>
                protected override Ipad Create()
                {
                    return Ipad.Instance;
                }

            }

            #endregion
        }

        /// <summary>
        /// <para>Official Dropbox Android client.</para>
        /// </summary>
        public sealed class Android : MobileClientPlatform
        {
            #pragma warning disable 108

            /// <summary>
            /// <para>The encoder instance.</para>
            /// </summary>
            internal static enc.StructEncoder<Android> Encoder = new AndroidEncoder();

            /// <summary>
            /// <para>The decoder instance.</para>
            /// </summary>
            internal static enc.StructDecoder<Android> Decoder = new AndroidDecoder();

            /// <summary>
            /// <para>Initializes a new instance of the <see cref="Android" /> class.</para>
            /// </summary>
            private Android()
            {
            }

            /// <summary>
            /// <para>A singleton instance of Android</para>
            /// </summary>
            public static readonly Android Instance = new Android();

            #region Encoder class

            /// <summary>
            /// <para>Encoder for  <see cref="Android" />.</para>
            /// </summary>
            private class AndroidEncoder : enc.StructEncoder<Android>
            {
                /// <summary>
                /// <para>Encode fields of given value.</para>
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="writer">The writer.</param>
                public override void EncodeFields(Android value, enc.IJsonWriter writer)
                {
                }
            }

            #endregion

            #region Decoder class

            /// <summary>
            /// <para>Decoder for  <see cref="Android" />.</para>
            /// </summary>
            private class AndroidDecoder : enc.StructDecoder<Android>
            {
                /// <summary>
                /// <para>Create a new instance of type <see cref="Android" />.</para>
                /// </summary>
                /// <returns>The struct instance.</returns>
                protected override Android Create()
                {
                    return Android.Instance;
                }

            }

            #endregion
        }

        /// <summary>
        /// <para>Official Dropbox Windows phone client.</para>
        /// </summary>
        public sealed class WindowsPhone : MobileClientPlatform
        {
            #pragma warning disable 108

            /// <summary>
            /// <para>The encoder instance.</para>
            /// </summary>
            internal static enc.StructEncoder<WindowsPhone> Encoder = new WindowsPhoneEncoder();

            /// <summary>
            /// <para>The decoder instance.</para>
            /// </summary>
            internal static enc.StructDecoder<WindowsPhone> Decoder = new WindowsPhoneDecoder();

            /// <summary>
            /// <para>Initializes a new instance of the <see cref="WindowsPhone" />
            /// class.</para>
            /// </summary>
            private WindowsPhone()
            {
            }

            /// <summary>
            /// <para>A singleton instance of WindowsPhone</para>
            /// </summary>
            public static readonly WindowsPhone Instance = new WindowsPhone();

            #region Encoder class

            /// <summary>
            /// <para>Encoder for  <see cref="WindowsPhone" />.</para>
            /// </summary>
            private class WindowsPhoneEncoder : enc.StructEncoder<WindowsPhone>
            {
                /// <summary>
                /// <para>Encode fields of given value.</para>
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="writer">The writer.</param>
                public override void EncodeFields(WindowsPhone value, enc.IJsonWriter writer)
                {
                }
            }

            #endregion

            #region Decoder class

            /// <summary>
            /// <para>Decoder for  <see cref="WindowsPhone" />.</para>
            /// </summary>
            private class WindowsPhoneDecoder : enc.StructDecoder<WindowsPhone>
            {
                /// <summary>
                /// <para>Create a new instance of type <see cref="WindowsPhone" />.</para>
                /// </summary>
                /// <returns>The struct instance.</returns>
                protected override WindowsPhone Create()
                {
                    return WindowsPhone.Instance;
                }

            }

            #endregion
        }

        /// <summary>
        /// <para>Official Dropbox Blackberry client.</para>
        /// </summary>
        public sealed class Blackberry : MobileClientPlatform
        {
            #pragma warning disable 108

            /// <summary>
            /// <para>The encoder instance.</para>
            /// </summary>
            internal static enc.StructEncoder<Blackberry> Encoder = new BlackberryEncoder();

            /// <summary>
            /// <para>The decoder instance.</para>
            /// </summary>
            internal static enc.StructDecoder<Blackberry> Decoder = new BlackberryDecoder();

            /// <summary>
            /// <para>Initializes a new instance of the <see cref="Blackberry" /> class.</para>
            /// </summary>
            private Blackberry()
            {
            }

            /// <summary>
            /// <para>A singleton instance of Blackberry</para>
            /// </summary>
            public static readonly Blackberry Instance = new Blackberry();

            #region Encoder class

            /// <summary>
            /// <para>Encoder for  <see cref="Blackberry" />.</para>
            /// </summary>
            private class BlackberryEncoder : enc.StructEncoder<Blackberry>
            {
                /// <summary>
                /// <para>Encode fields of given value.</para>
                /// </summary>
                /// <param name="value">The value.</param>
                /// <param name="writer">The writer.</param>
                public override void EncodeFields(Blackberry value, enc.IJsonWriter writer)
                {
                }
            }

            #endregion

            #region Decoder class

            /// <summary>
            /// <para>Decoder for  <see cref="Blackberry" />.</para>
            /// </summary>
            private class BlackberryDecoder : enc.StructDecoder<Blackberry>
            {
                /// <summary>
                /// <para>Create a new instance of type <see cref="Blackberry" />.</para>
                /// </summary>
                /// <returns>The struct instance.</returns>
                protected override Blackberry Create()
                {
                    return Blackberry.Instance;
                }

            }

            #endregion
        }

        /// <summary>
        /// <para>The other object</para>
        /// </summary>
        public sealed class Other : MobileClientPlatform
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
