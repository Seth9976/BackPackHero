using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Mono.Security;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Represents Abstract Syntax Notation One (ASN.1)-encoded data.</summary>
	// Token: 0x020002BA RID: 698
	public class AsnEncodedData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AsnEncodedData" /> class.</summary>
		// Token: 0x060015DF RID: 5599 RVA: 0x0000219B File Offset: 0x0000039B
		protected AsnEncodedData()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AsnEncodedData" /> class using a byte array.</summary>
		/// <param name="oid">A string that represents <see cref="T:System.Security.Cryptography.Oid" /> information.</param>
		/// <param name="rawData">A byte array that contains Abstract Syntax Notation One (ASN.1)-encoded data.</param>
		// Token: 0x060015E0 RID: 5600 RVA: 0x000578AC File Offset: 0x00055AAC
		public AsnEncodedData(string oid, byte[] rawData)
		{
			this._oid = new Oid(oid);
			this.RawData = rawData;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AsnEncodedData" /> class using an <see cref="T:System.Security.Cryptography.Oid" /> object and a byte array.</summary>
		/// <param name="oid">An <see cref="T:System.Security.Cryptography.Oid" /> object.</param>
		/// <param name="rawData">A byte array that contains Abstract Syntax Notation One (ASN.1)-encoded data.</param>
		// Token: 0x060015E1 RID: 5601 RVA: 0x000578C7 File Offset: 0x00055AC7
		public AsnEncodedData(Oid oid, byte[] rawData)
		{
			this.Oid = oid;
			this.RawData = rawData;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AsnEncodedData" /> class using an instance of the <see cref="T:System.Security.Cryptography.AsnEncodedData" /> class.</summary>
		/// <param name="asnEncodedData">An instance of the <see cref="T:System.Security.Cryptography.AsnEncodedData" /> class.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is null.</exception>
		// Token: 0x060015E2 RID: 5602 RVA: 0x000578DD File Offset: 0x00055ADD
		public AsnEncodedData(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			if (asnEncodedData._oid != null)
			{
				this.Oid = new Oid(asnEncodedData._oid);
			}
			this.RawData = asnEncodedData._raw;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AsnEncodedData" /> class using a byte array.</summary>
		/// <param name="rawData">A byte array that contains Abstract Syntax Notation One (ASN.1)-encoded data.</param>
		// Token: 0x060015E3 RID: 5603 RVA: 0x00057918 File Offset: 0x00055B18
		public AsnEncodedData(byte[] rawData)
		{
			this.RawData = rawData;
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Oid" /> value for an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x00057927 File Offset: 0x00055B27
		// (set) Token: 0x060015E5 RID: 5605 RVA: 0x0005792F File Offset: 0x00055B2F
		public Oid Oid
		{
			get
			{
				return this._oid;
			}
			set
			{
				if (value == null)
				{
					this._oid = null;
					return;
				}
				this._oid = new Oid(value);
			}
		}

		/// <summary>Gets or sets the Abstract Syntax Notation One (ASN.1)-encoded data represented in a byte array.</summary>
		/// <returns>A byte array that represents the Abstract Syntax Notation One (ASN.1)-encoded data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value is null.</exception>
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x00057948 File Offset: 0x00055B48
		// (set) Token: 0x060015E7 RID: 5607 RVA: 0x00057950 File Offset: 0x00055B50
		public byte[] RawData
		{
			get
			{
				return this._raw;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("RawData");
				}
				this._raw = (byte[])value.Clone();
			}
		}

		/// <summary>Copies information from an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object to base the new object on.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData " />is null.</exception>
		// Token: 0x060015E8 RID: 5608 RVA: 0x00057971 File Offset: 0x00055B71
		public virtual void CopyFrom(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			if (asnEncodedData._oid == null)
			{
				this.Oid = null;
			}
			else
			{
				this.Oid = new Oid(asnEncodedData._oid);
			}
			this.RawData = asnEncodedData._raw;
		}

		/// <summary>Returns a formatted version of the Abstract Syntax Notation One (ASN.1)-encoded data as a string.</summary>
		/// <returns>A formatted string that represents the Abstract Syntax Notation One (ASN.1)-encoded data.</returns>
		/// <param name="multiLine">true if the return string should contain carriage returns; otherwise, false.</param>
		// Token: 0x060015E9 RID: 5609 RVA: 0x000579AF File Offset: 0x00055BAF
		public virtual string Format(bool multiLine)
		{
			if (this._raw == null)
			{
				return string.Empty;
			}
			if (this._oid == null)
			{
				return this.Default(multiLine);
			}
			return this.ToString(multiLine);
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x000579D8 File Offset: 0x00055BD8
		internal virtual string ToString(bool multiLine)
		{
			string value = this._oid.Value;
			if (value == "2.5.29.19")
			{
				return this.BasicConstraintsExtension(multiLine);
			}
			if (value == "2.5.29.37")
			{
				return this.EnhancedKeyUsageExtension(multiLine);
			}
			if (value == "2.5.29.15")
			{
				return this.KeyUsageExtension(multiLine);
			}
			if (value == "2.5.29.14")
			{
				return this.SubjectKeyIdentifierExtension(multiLine);
			}
			if (value == "2.5.29.17")
			{
				return this.SubjectAltName(multiLine);
			}
			if (!(value == "2.16.840.1.113730.1.1"))
			{
				return this.Default(multiLine);
			}
			return this.NetscapeCertType(multiLine);
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00057A78 File Offset: 0x00055C78
		internal string Default(bool multiLine)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this._raw.Length; i++)
			{
				stringBuilder.Append(this._raw[i].ToString("x2"));
				if (i != this._raw.Length - 1)
				{
					stringBuilder.Append(" ");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00057ADC File Offset: 0x00055CDC
		internal string BasicConstraintsExtension(bool multiLine)
		{
			string text;
			try
			{
				text = new X509BasicConstraintsExtension(this, false).ToString(multiLine);
			}
			catch
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00057B14 File Offset: 0x00055D14
		internal string EnhancedKeyUsageExtension(bool multiLine)
		{
			string text;
			try
			{
				text = new X509EnhancedKeyUsageExtension(this, false).ToString(multiLine);
			}
			catch
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00057B4C File Offset: 0x00055D4C
		internal string KeyUsageExtension(bool multiLine)
		{
			string text;
			try
			{
				text = new X509KeyUsageExtension(this, false).ToString(multiLine);
			}
			catch
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00057B84 File Offset: 0x00055D84
		internal string SubjectKeyIdentifierExtension(bool multiLine)
		{
			string text;
			try
			{
				text = new X509SubjectKeyIdentifierExtension(this, false).ToString(multiLine);
			}
			catch
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00057BBC File Offset: 0x00055DBC
		internal string SubjectAltName(bool multiLine)
		{
			if (this._raw.Length < 5)
			{
				return "Information Not Available";
			}
			string text3;
			try
			{
				Mono.Security.ASN1 asn = new Mono.Security.ASN1(this._raw);
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < asn.Count; i++)
				{
					Mono.Security.ASN1 asn2 = asn[i];
					byte tag = asn2.Tag;
					string text;
					string text2;
					if (tag != 129)
					{
						if (tag != 130)
						{
							text = string.Format("Unknown ({0})=", asn2.Tag);
							text2 = Mono.Security.Cryptography.CryptoConvert.ToHex(asn2.Value);
						}
						else
						{
							text = "DNS Name=";
							text2 = Encoding.ASCII.GetString(asn2.Value);
						}
					}
					else
					{
						text = "RFC822 Name=";
						text2 = Encoding.ASCII.GetString(asn2.Value);
					}
					stringBuilder.Append(text);
					stringBuilder.Append(text2);
					if (multiLine)
					{
						stringBuilder.Append(Environment.NewLine);
					}
					else if (i < asn.Count - 1)
					{
						stringBuilder.Append(", ");
					}
				}
				text3 = stringBuilder.ToString();
			}
			catch
			{
				text3 = string.Empty;
			}
			return text3;
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00057CE8 File Offset: 0x00055EE8
		internal string NetscapeCertType(bool multiLine)
		{
			if (this._raw.Length < 4 || this._raw[0] != 3 || this._raw[1] != 2)
			{
				return "Information Not Available";
			}
			int num = this._raw[3] >> (int)this._raw[2] << (int)this._raw[2];
			StringBuilder stringBuilder = new StringBuilder();
			if ((num & 128) == 128)
			{
				stringBuilder.Append("SSL Client Authentication");
			}
			if ((num & 64) == 64)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("SSL Server Authentication");
			}
			if ((num & 32) == 32)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("SMIME");
			}
			if ((num & 16) == 16)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("Signature");
			}
			if ((num & 8) == 8)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("Unknown cert type");
			}
			if ((num & 4) == 4)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("SSL CA");
			}
			if ((num & 2) == 2)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("SMIME CA");
			}
			if ((num & 1) == 1)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("Signature CA");
			}
			stringBuilder.AppendFormat(" ({0})", num.ToString("x2"));
			return stringBuilder.ToString();
		}

		// Token: 0x04000C4A RID: 3146
		internal Oid _oid;

		// Token: 0x04000C4B RID: 3147
		internal byte[] _raw;
	}
}
