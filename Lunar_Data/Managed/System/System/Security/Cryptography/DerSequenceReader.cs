using System;
using System.Globalization;
using System.Numerics;
using System.Text;
using System.Threading;

namespace System.Security.Cryptography
{
	// Token: 0x020002B0 RID: 688
	internal class DerSequenceReader
	{
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x00056961 File Offset: 0x00054B61
		// (set) Token: 0x06001586 RID: 5510 RVA: 0x00056969 File Offset: 0x00054B69
		internal int ContentLength { get; private set; }

		// Token: 0x06001587 RID: 5511 RVA: 0x00056972 File Offset: 0x00054B72
		private DerSequenceReader(bool startAtPayload, byte[] data, int offset, int length)
		{
			this._data = data;
			this._position = offset;
			this._end = offset + length;
			this.ContentLength = length;
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x0005699A File Offset: 0x00054B9A
		internal DerSequenceReader(byte[] data)
			: this(data, 0, data.Length)
		{
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x000569A7 File Offset: 0x00054BA7
		internal DerSequenceReader(byte[] data, int offset, int length)
			: this(DerSequenceReader.DerTag.Sequence, data, offset, length)
		{
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x000569B4 File Offset: 0x00054BB4
		private DerSequenceReader(DerSequenceReader.DerTag tagToEat, byte[] data, int offset, int length)
		{
			if (offset < 0 || length < 2 || length > data.Length - offset)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			this._data = data;
			this._end = offset + length;
			this._position = offset;
			this.EatTag(tagToEat);
			int num = this.EatLength();
			this.ContentLength = num;
			this._end = this._position + num;
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x00056A1F File Offset: 0x00054C1F
		internal static DerSequenceReader CreateForPayload(byte[] payload)
		{
			return new DerSequenceReader(true, payload, 0, payload.Length);
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x00056A2C File Offset: 0x00054C2C
		internal bool HasData
		{
			get
			{
				return this._position < this._end;
			}
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00056A3C File Offset: 0x00054C3C
		internal byte PeekTag()
		{
			if (!this.HasData)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			byte b = this._data[this._position];
			if ((b & 31) == 31)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			return b;
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x00056A71 File Offset: 0x00054C71
		internal bool HasTag(DerSequenceReader.DerTag expectedTag)
		{
			return this.HasTag((byte)expectedTag);
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x00056A7A File Offset: 0x00054C7A
		internal bool HasTag(byte expectedTag)
		{
			return this.HasData && this._data[this._position] == expectedTag;
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x00056A98 File Offset: 0x00054C98
		internal void SkipValue()
		{
			this.EatTag((DerSequenceReader.DerTag)this.PeekTag());
			int num = this.EatLength();
			this._position += num;
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00056AC8 File Offset: 0x00054CC8
		internal void ValidateAndSkipDerValue()
		{
			byte b = this.PeekTag();
			if ((b & 192) == 0)
			{
				if (b == 0 || b == 15)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				bool flag = false;
				int num = (int)(b & 31);
				if (num <= 11)
				{
					if (num != 8 && num != 11)
					{
						goto IL_004E;
					}
				}
				else if (num - 16 > 1 && num != 29)
				{
					goto IL_004E;
				}
				flag = true;
				IL_004E:
				bool flag2 = (b & 32) == 32;
				if (flag != flag2)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
			}
			this.EatTag((DerSequenceReader.DerTag)b);
			int num2 = this.EatLength();
			if (num2 > 0 && (b & 32) == 32)
			{
				DerSequenceReader derSequenceReader = new DerSequenceReader(true, this._data, this._position, this._end - this._position);
				while (derSequenceReader.HasData)
				{
					derSequenceReader.ValidateAndSkipDerValue();
				}
			}
			this._position += num2;
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x00056B98 File Offset: 0x00054D98
		internal byte[] ReadNextEncodedValue()
		{
			this.PeekTag();
			int num2;
			int num = DerSequenceReader.ScanContentLength(this._data, this._position + 1, this._end, out num2);
			int num3 = 1 + num2 + num;
			byte[] array = new byte[num3];
			Buffer.BlockCopy(this._data, this._position, array, 0, num3);
			this._position += num3;
			return array;
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00056BF8 File Offset: 0x00054DF8
		internal bool ReadBoolean()
		{
			this.EatTag(DerSequenceReader.DerTag.Boolean);
			int num = this.EatLength();
			if (num != 1)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			bool flag = this._data[this._position] > 0;
			this._position += num;
			return flag;
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00056C40 File Offset: 0x00054E40
		internal int ReadInteger()
		{
			byte[] array = this.ReadIntegerBytes();
			Array.Reverse<byte>(array);
			return (int)new BigInteger(array);
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x00056C65 File Offset: 0x00054E65
		internal byte[] ReadIntegerBytes()
		{
			this.EatTag(DerSequenceReader.DerTag.Integer);
			return this.ReadContentAsBytes();
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00056C74 File Offset: 0x00054E74
		internal byte[] ReadBitString()
		{
			this.EatTag(DerSequenceReader.DerTag.BitString);
			int num = this.EatLength();
			if (num < 1)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (this._data[this._position] > 7)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			num--;
			this._position++;
			byte[] array = new byte[num];
			Buffer.BlockCopy(this._data, this._position, array, 0, num);
			this._position += num;
			return array;
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x00056CF5 File Offset: 0x00054EF5
		internal byte[] ReadOctetString()
		{
			this.EatTag(DerSequenceReader.DerTag.OctetString);
			return this.ReadContentAsBytes();
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x00056D04 File Offset: 0x00054F04
		internal string ReadOidAsString()
		{
			this.EatTag(DerSequenceReader.DerTag.ObjectIdentifier);
			int num = this.EatLength();
			if (num < 1)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			StringBuilder stringBuilder = new StringBuilder(num * 4);
			byte b = this._data[this._position];
			byte b2 = b / 40;
			byte b3 = b % 40;
			stringBuilder.Append(b2);
			stringBuilder.Append('.');
			stringBuilder.Append(b3);
			bool flag = true;
			BigInteger bigInteger = new BigInteger(0);
			for (int i = 1; i < num; i++)
			{
				byte b4 = this._data[this._position + i];
				byte b5 = b4 & 127;
				if (flag)
				{
					stringBuilder.Append('.');
					flag = false;
				}
				bigInteger <<= 7;
				bigInteger += b5;
				if (b4 == b5)
				{
					stringBuilder.Append(bigInteger);
					bigInteger = 0;
					flag = true;
				}
			}
			this._position += num;
			return stringBuilder.ToString();
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x00056DF5 File Offset: 0x00054FF5
		internal Oid ReadOid()
		{
			return new Oid(this.ReadOidAsString());
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x00056E04 File Offset: 0x00055004
		internal string ReadUtf8String()
		{
			this.EatTag(DerSequenceReader.DerTag.UTF8String);
			int num = this.EatLength();
			string @string = Encoding.UTF8.GetString(this._data, this._position, num);
			this._position += num;
			return DerSequenceReader.TrimTrailingNulls(@string);
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x00056E4C File Offset: 0x0005504C
		private DerSequenceReader ReadCollectionWithTag(DerSequenceReader.DerTag expected)
		{
			DerSequenceReader.CheckTag(expected, this._data, this._position);
			int num2;
			int num = DerSequenceReader.ScanContentLength(this._data, this._position + 1, this._end, out num2);
			int num3 = 1 + num2 + num;
			DerSequenceReader derSequenceReader = new DerSequenceReader(expected, this._data, this._position, num3);
			this._position += num3;
			return derSequenceReader;
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x00056EAE File Offset: 0x000550AE
		internal DerSequenceReader ReadSequence()
		{
			return this.ReadCollectionWithTag(DerSequenceReader.DerTag.Sequence);
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x00056EB8 File Offset: 0x000550B8
		internal DerSequenceReader ReadSet()
		{
			return this.ReadCollectionWithTag(DerSequenceReader.DerTag.Set);
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x00056EC4 File Offset: 0x000550C4
		internal string ReadPrintableString()
		{
			this.EatTag(DerSequenceReader.DerTag.PrintableString);
			int num = this.EatLength();
			string @string = Encoding.ASCII.GetString(this._data, this._position, num);
			this._position += num;
			return DerSequenceReader.TrimTrailingNulls(@string);
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x00056F0C File Offset: 0x0005510C
		internal string ReadIA5String()
		{
			this.EatTag(DerSequenceReader.DerTag.IA5String);
			int num = this.EatLength();
			string @string = Encoding.ASCII.GetString(this._data, this._position, num);
			this._position += num;
			return DerSequenceReader.TrimTrailingNulls(@string);
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x00056F54 File Offset: 0x00055154
		internal string ReadT61String()
		{
			this.EatTag(DerSequenceReader.DerTag.T61String);
			int num = this.EatLength();
			Encoding encoding = LazyInitializer.EnsureInitialized<Encoding>(ref DerSequenceReader.s_utf8EncodingWithExceptionFallback, () => new UTF8Encoding(false, true));
			Encoding encoding2 = LazyInitializer.EnsureInitialized<Encoding>(ref DerSequenceReader.s_latin1Encoding, () => Encoding.GetEncoding("iso-8859-1"));
			string text;
			try
			{
				text = encoding.GetString(this._data, this._position, num);
			}
			catch (DecoderFallbackException)
			{
				text = encoding2.GetString(this._data, this._position, num);
			}
			this._position += num;
			return DerSequenceReader.TrimTrailingNulls(text);
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00057018 File Offset: 0x00055218
		internal DateTime ReadX509Date()
		{
			DerSequenceReader.DerTag derTag = (DerSequenceReader.DerTag)this.PeekTag();
			if (derTag == DerSequenceReader.DerTag.UTCTime)
			{
				return this.ReadUtcTime();
			}
			if (derTag != DerSequenceReader.DerTag.GeneralizedTime)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			return this.ReadGeneralizedTime();
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00057050 File Offset: 0x00055250
		internal DateTime ReadUtcTime()
		{
			return this.ReadTime(DerSequenceReader.DerTag.UTCTime, "yyMMddHHmmss'Z'");
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0005705F File Offset: 0x0005525F
		internal DateTime ReadGeneralizedTime()
		{
			return this.ReadTime(DerSequenceReader.DerTag.GeneralizedTime, "yyyyMMddHHmmss'Z'");
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x00057070 File Offset: 0x00055270
		internal string ReadBMPString()
		{
			this.EatTag(DerSequenceReader.DerTag.BMPString);
			int num = this.EatLength();
			string @string = Encoding.BigEndianUnicode.GetString(this._data, this._position, num);
			this._position += num;
			return DerSequenceReader.TrimTrailingNulls(@string);
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x000570B8 File Offset: 0x000552B8
		private static string TrimTrailingNulls(string value)
		{
			if (value != null && value.Length > 0)
			{
				int num = value.Length;
				while (num > 0 && value[num - 1] == '\0')
				{
					num--;
				}
				if (num != value.Length)
				{
					return value.Substring(0, num);
				}
			}
			return value;
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x00057100 File Offset: 0x00055300
		private DateTime ReadTime(DerSequenceReader.DerTag timeTag, string formatString)
		{
			this.EatTag(timeTag);
			int num = this.EatLength();
			string @string = Encoding.ASCII.GetString(this._data, this._position, num);
			this._position += num;
			DateTimeFormatInfo dateTimeFormatInfo = LazyInitializer.EnsureInitialized<DateTimeFormatInfo>(ref DerSequenceReader.s_validityDateTimeFormatInfo, delegate
			{
				DateTimeFormatInfo dateTimeFormatInfo2 = (DateTimeFormatInfo)CultureInfo.InvariantCulture.DateTimeFormat.Clone();
				dateTimeFormatInfo2.Calendar.TwoDigitYearMax = 2049;
				return dateTimeFormatInfo2;
			});
			DateTime dateTime;
			if (!DateTime.TryParseExact(@string, formatString, dateTimeFormatInfo, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out dateTime))
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			return dateTime;
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x00057184 File Offset: 0x00055384
		private byte[] ReadContentAsBytes()
		{
			int num = this.EatLength();
			byte[] array = new byte[num];
			Buffer.BlockCopy(this._data, this._position, array, 0, num);
			this._position += num;
			return array;
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x000571C2 File Offset: 0x000553C2
		private void EatTag(DerSequenceReader.DerTag expected)
		{
			if (!this.HasData)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			DerSequenceReader.CheckTag(expected, this._data, this._position);
			this._position++;
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x000571F8 File Offset: 0x000553F8
		private static void CheckTag(DerSequenceReader.DerTag expected, byte[] data, int position)
		{
			if (position >= data.Length)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			byte b = data[position];
			byte b2 = b & 31;
			if (b2 == 31)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if ((b & 128) != 0)
			{
				return;
			}
			if ((byte)(expected & (DerSequenceReader.DerTag)31) != b2)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x0005724C File Offset: 0x0005544C
		private int EatLength()
		{
			int num2;
			int num = DerSequenceReader.ScanContentLength(this._data, this._position, this._end, out num2);
			this._position += num2;
			return num;
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x00057280 File Offset: 0x00055480
		private static int ScanContentLength(byte[] data, int offset, int end, out int bytesConsumed)
		{
			if (offset >= end)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			byte b = data[offset];
			if (b < 128)
			{
				bytesConsumed = 1;
				if ((int)b > end - offset - bytesConsumed)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				return (int)b;
			}
			else
			{
				int num = (int)(b & 127);
				if (num > 4)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				bytesConsumed = 1 + num;
				if (bytesConsumed > end - offset)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				if (bytesConsumed == 1)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				int num2 = offset + bytesConsumed;
				int num3 = 0;
				for (int i = offset + 1; i < num2; i++)
				{
					num3 <<= 8;
					num3 |= (int)data[i];
				}
				if (num3 < 0)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				if (num3 > end - offset - bytesConsumed)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				return num3;
			}
		}

		// Token: 0x04000C0A RID: 3082
		internal const byte ContextSpecificTagFlag = 128;

		// Token: 0x04000C0B RID: 3083
		internal const byte ConstructedFlag = 32;

		// Token: 0x04000C0C RID: 3084
		internal const byte ContextSpecificConstructedTag0 = 160;

		// Token: 0x04000C0D RID: 3085
		internal const byte ContextSpecificConstructedTag1 = 161;

		// Token: 0x04000C0E RID: 3086
		internal const byte ContextSpecificConstructedTag2 = 162;

		// Token: 0x04000C0F RID: 3087
		internal const byte ContextSpecificConstructedTag3 = 163;

		// Token: 0x04000C10 RID: 3088
		internal const byte ConstructedSequence = 48;

		// Token: 0x04000C11 RID: 3089
		internal const byte TagClassMask = 192;

		// Token: 0x04000C12 RID: 3090
		internal const byte TagNumberMask = 31;

		// Token: 0x04000C13 RID: 3091
		internal static DateTimeFormatInfo s_validityDateTimeFormatInfo;

		// Token: 0x04000C14 RID: 3092
		private static Encoding s_utf8EncodingWithExceptionFallback;

		// Token: 0x04000C15 RID: 3093
		private static Encoding s_latin1Encoding;

		// Token: 0x04000C16 RID: 3094
		private readonly byte[] _data;

		// Token: 0x04000C17 RID: 3095
		private readonly int _end;

		// Token: 0x04000C18 RID: 3096
		private int _position;

		// Token: 0x020002B1 RID: 689
		internal enum DerTag : byte
		{
			// Token: 0x04000C1B RID: 3099
			Boolean = 1,
			// Token: 0x04000C1C RID: 3100
			Integer,
			// Token: 0x04000C1D RID: 3101
			BitString,
			// Token: 0x04000C1E RID: 3102
			OctetString,
			// Token: 0x04000C1F RID: 3103
			Null,
			// Token: 0x04000C20 RID: 3104
			ObjectIdentifier,
			// Token: 0x04000C21 RID: 3105
			UTF8String = 12,
			// Token: 0x04000C22 RID: 3106
			Sequence = 16,
			// Token: 0x04000C23 RID: 3107
			Set,
			// Token: 0x04000C24 RID: 3108
			PrintableString = 19,
			// Token: 0x04000C25 RID: 3109
			T61String,
			// Token: 0x04000C26 RID: 3110
			IA5String = 22,
			// Token: 0x04000C27 RID: 3111
			UTCTime,
			// Token: 0x04000C28 RID: 3112
			GeneralizedTime,
			// Token: 0x04000C29 RID: 3113
			BMPString = 30
		}
	}
}
