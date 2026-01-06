using System;
using System.Runtime.Serialization;

namespace System.Text
{
	/// <summary>The exception that is thrown when a decoder fallback operation fails. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000395 RID: 917
	[Serializable]
	public sealed class DecoderFallbackException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackException" /> class. </summary>
		// Token: 0x060025B2 RID: 9650 RVA: 0x00085F97 File Offset: 0x00084197
		public DecoderFallbackException()
			: base("Value does not fall within the expected range.")
		{
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackException" /> class. A parameter specifies the error message.</summary>
		/// <param name="message">An error message.</param>
		// Token: 0x060025B3 RID: 9651 RVA: 0x00085FAF File Offset: 0x000841AF
		public DecoderFallbackException(string message)
			: base(message)
		{
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackException" /> class. Parameters specify the error message and the inner exception that is the cause of this exception.</summary>
		/// <param name="message">An error message.</param>
		/// <param name="innerException">The exception that caused this exception.</param>
		// Token: 0x060025B4 RID: 9652 RVA: 0x00085FC3 File Offset: 0x000841C3
		public DecoderFallbackException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147024809;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderFallbackException" /> class. Parameters specify the error message, the array of bytes being decoded, and the index of the byte that cannot be decoded.</summary>
		/// <param name="message">An error message.</param>
		/// <param name="bytesUnknown">The input byte array.</param>
		/// <param name="index">The index position in <paramref name="bytesUnknown" /> of the byte that cannot be decoded.</param>
		// Token: 0x060025B5 RID: 9653 RVA: 0x00085FD8 File Offset: 0x000841D8
		public DecoderFallbackException(string message, byte[] bytesUnknown, int index)
			: base(message)
		{
			this._bytesUnknown = bytesUnknown;
			this._index = index;
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x00021113 File Offset: 0x0001F313
		private DecoderFallbackException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Gets the input byte sequence that caused the exception.</summary>
		/// <returns>The input byte array that cannot be decoded. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060025B7 RID: 9655 RVA: 0x00085FEF File Offset: 0x000841EF
		public byte[] BytesUnknown
		{
			get
			{
				return this._bytesUnknown;
			}
		}

		/// <summary>Gets the index position in the input byte sequence of the byte that caused the exception.</summary>
		/// <returns>The index position in the input byte array of the byte that cannot be decoded. The index position is zero-based. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060025B8 RID: 9656 RVA: 0x00085FF7 File Offset: 0x000841F7
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x04001D96 RID: 7574
		private byte[] _bytesUnknown;

		// Token: 0x04001D97 RID: 7575
		private int _index;
	}
}
