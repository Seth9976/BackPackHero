using System;
using System.Buffers;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Util;
using System.Text;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class from which all implementations of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm inherit.</summary>
	// Token: 0x020004AA RID: 1194
	[ComVisible(true)]
	public abstract class RSA : AsymmetricAlgorithm
	{
		/// <summary>Creates an instance of the default implementation of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		/// <returns>A new instance of the default implementation of <see cref="T:System.Security.Cryptography.RSA" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002FB3 RID: 12211 RVA: 0x000AD44C File Offset: 0x000AB64C
		public new static RSA Create()
		{
			return RSA.Create("System.Security.Cryptography.RSA");
		}

		/// <summary>Creates an instance of the specified implementation of <see cref="T:System.Security.Cryptography.RSA" />.</summary>
		/// <returns>A new instance of the specified implementation of <see cref="T:System.Security.Cryptography.RSA" />.</returns>
		/// <param name="algName">The name of the implementation of <see cref="T:System.Security.Cryptography.RSA" /> to use. </param>
		// Token: 0x06002FB4 RID: 12212 RVA: 0x000AD458 File Offset: 0x000AB658
		public new static RSA Create(string algName)
		{
			return (RSA)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000AD465 File Offset: 0x000AB665
		public virtual byte[] Encrypt(byte[] data, RSAEncryptionPadding padding)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x000AD465 File Offset: 0x000AB665
		public virtual byte[] Decrypt(byte[] data, RSAEncryptionPadding padding)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x000AD465 File Offset: 0x000AB665
		public virtual byte[] SignHash(byte[] hash, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000AD465 File Offset: 0x000AB665
		public virtual bool VerifyHash(byte[] hash, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000AD465 File Offset: 0x000AB665
		protected virtual byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x000AD465 File Offset: 0x000AB665
		protected virtual byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x000AD46C File Offset: 0x000AB66C
		public byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return this.SignData(data, 0, data.Length, hashAlgorithm, padding);
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x000AD48C File Offset: 0x000AB68C
		public virtual byte[] SignData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (offset < 0 || offset > data.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > data.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			byte[] array = this.HashData(data, offset, count, hashAlgorithm);
			return this.SignHash(array, hashAlgorithm, padding);
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x000AD514 File Offset: 0x000AB714
		public virtual byte[] SignData(Stream data, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			byte[] array = this.HashData(data, hashAlgorithm);
			return this.SignHash(array, hashAlgorithm, padding);
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x000AD569 File Offset: 0x000AB769
		public bool VerifyData(byte[] data, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return this.VerifyData(data, 0, data.Length, signature, hashAlgorithm, padding);
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x000AD588 File Offset: 0x000AB788
		public virtual bool VerifyData(byte[] data, int offset, int count, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (offset < 0 || offset > data.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > data.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			byte[] array = this.HashData(data, offset, count, hashAlgorithm);
			return this.VerifyHash(array, signature, hashAlgorithm, padding);
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x000AD620 File Offset: 0x000AB820
		public bool VerifyData(Stream data, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			byte[] array = this.HashData(data, hashAlgorithm);
			return this.VerifyHash(array, signature, hashAlgorithm, padding);
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x000A7485 File Offset: 0x000A5685
		private static Exception DerivedClassMustOverride()
		{
			return new NotImplementedException(Environment.GetResourceString("Derived classes must provide an implementation."));
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x000A7496 File Offset: 0x000A5696
		internal static Exception HashAlgorithmNameNullOrEmpty()
		{
			return new ArgumentException(Environment.GetResourceString("The hash algorithm name cannot be null or empty."), "hashAlgorithm");
		}

		/// <summary>When overridden in a derived class, decrypts the input data using the private key.</summary>
		/// <returns>The resulting decryption of the <paramref name="rgb" /> parameter in plain text.</returns>
		/// <param name="rgb">The cipher text to be decrypted. </param>
		// Token: 0x06002FC3 RID: 12227 RVA: 0x000AD686 File Offset: 0x000AB886
		public virtual byte[] DecryptValue(byte[] rgb)
		{
			throw new NotSupportedException(Environment.GetResourceString("Method is not supported."));
		}

		/// <summary>When overridden in a derived class, encrypts the input data using the public key.</summary>
		/// <returns>The resulting encryption of the <paramref name="rgb" /> parameter as cipher text.</returns>
		/// <param name="rgb">The plain text to be encrypted. </param>
		// Token: 0x06002FC4 RID: 12228 RVA: 0x000AD686 File Offset: 0x000AB886
		public virtual byte[] EncryptValue(byte[] rgb)
		{
			throw new NotSupportedException(Environment.GetResourceString("Method is not supported."));
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06002FC5 RID: 12229 RVA: 0x000AD697 File Offset: 0x000AB897
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return "RSA";
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06002FC6 RID: 12230 RVA: 0x000AD697 File Offset: 0x000AB897
		public override string SignatureAlgorithm
		{
			get
			{
				return "RSA";
			}
		}

		/// <summary>Initializes an <see cref="T:System.Security.Cryptography.RSA" /> object from the key information from an XML string.</summary>
		/// <param name="xmlString">The XML string containing <see cref="T:System.Security.Cryptography.RSA" /> key information. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="xmlString" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The format of the <paramref name="xmlString" /> parameter is not valid. </exception>
		// Token: 0x06002FC7 RID: 12231 RVA: 0x000AD6A0 File Offset: 0x000AB8A0
		public override void FromXmlString(string xmlString)
		{
			if (xmlString == null)
			{
				throw new ArgumentNullException("xmlString");
			}
			RSAParameters rsaparameters = default(RSAParameters);
			SecurityElement topElement = new Parser(xmlString).GetTopElement();
			string text = topElement.SearchForTextOfLocalName("Modulus");
			if (text == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Input string does not contain a valid encoding of the '{0}' '{1}' parameter.", new object[] { "RSA", "Modulus" }));
			}
			rsaparameters.Modulus = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text));
			string text2 = topElement.SearchForTextOfLocalName("Exponent");
			if (text2 == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Input string does not contain a valid encoding of the '{0}' '{1}' parameter.", new object[] { "RSA", "Exponent" }));
			}
			rsaparameters.Exponent = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text2));
			string text3 = topElement.SearchForTextOfLocalName("P");
			if (text3 != null)
			{
				rsaparameters.P = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text3));
			}
			string text4 = topElement.SearchForTextOfLocalName("Q");
			if (text4 != null)
			{
				rsaparameters.Q = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text4));
			}
			string text5 = topElement.SearchForTextOfLocalName("DP");
			if (text5 != null)
			{
				rsaparameters.DP = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text5));
			}
			string text6 = topElement.SearchForTextOfLocalName("DQ");
			if (text6 != null)
			{
				rsaparameters.DQ = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text6));
			}
			string text7 = topElement.SearchForTextOfLocalName("InverseQ");
			if (text7 != null)
			{
				rsaparameters.InverseQ = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text7));
			}
			string text8 = topElement.SearchForTextOfLocalName("D");
			if (text8 != null)
			{
				rsaparameters.D = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text8));
			}
			this.ImportParameters(rsaparameters);
		}

		/// <summary>Creates and returns an XML string containing the key of the current <see cref="T:System.Security.Cryptography.RSA" /> object.</summary>
		/// <returns>An XML string containing the key of the current <see cref="T:System.Security.Cryptography.RSA" /> object.</returns>
		/// <param name="includePrivateParameters">true to include a public and private RSA key; false to include only the public key. </param>
		// Token: 0x06002FC8 RID: 12232 RVA: 0x000AD83C File Offset: 0x000ABA3C
		public override string ToXmlString(bool includePrivateParameters)
		{
			RSAParameters rsaparameters = this.ExportParameters(includePrivateParameters);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<RSAKeyValue>");
			stringBuilder.Append("<Modulus>" + Convert.ToBase64String(rsaparameters.Modulus) + "</Modulus>");
			stringBuilder.Append("<Exponent>" + Convert.ToBase64String(rsaparameters.Exponent) + "</Exponent>");
			if (includePrivateParameters)
			{
				stringBuilder.Append("<P>" + Convert.ToBase64String(rsaparameters.P) + "</P>");
				stringBuilder.Append("<Q>" + Convert.ToBase64String(rsaparameters.Q) + "</Q>");
				stringBuilder.Append("<DP>" + Convert.ToBase64String(rsaparameters.DP) + "</DP>");
				stringBuilder.Append("<DQ>" + Convert.ToBase64String(rsaparameters.DQ) + "</DQ>");
				stringBuilder.Append("<InverseQ>" + Convert.ToBase64String(rsaparameters.InverseQ) + "</InverseQ>");
				stringBuilder.Append("<D>" + Convert.ToBase64String(rsaparameters.D) + "</D>");
			}
			stringBuilder.Append("</RSAKeyValue>");
			return stringBuilder.ToString();
		}

		/// <summary>When overridden in a derived class, exports the <see cref="T:System.Security.Cryptography.RSAParameters" />.</summary>
		/// <returns>The parameters for <see cref="T:System.Security.Cryptography.DSA" />.</returns>
		/// <param name="includePrivateParameters">true to include private parameters; otherwise, false. </param>
		// Token: 0x06002FC9 RID: 12233
		public abstract RSAParameters ExportParameters(bool includePrivateParameters);

		/// <summary>When overridden in a derived class, imports the specified <see cref="T:System.Security.Cryptography.RSAParameters" />.</summary>
		/// <param name="parameters">The parameters for <see cref="T:System.Security.Cryptography.RSA" />. </param>
		// Token: 0x06002FCA RID: 12234
		public abstract void ImportParameters(RSAParameters parameters);

		// Token: 0x06002FCB RID: 12235 RVA: 0x000AD984 File Offset: 0x000ABB84
		public static RSA Create(int keySizeInBits)
		{
			RSA rsa = RSA.Create();
			RSA rsa2;
			try
			{
				rsa.KeySize = keySizeInBits;
				rsa2 = rsa;
			}
			catch
			{
				rsa.Dispose();
				throw;
			}
			return rsa2;
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x000AD9BC File Offset: 0x000ABBBC
		public static RSA Create(RSAParameters parameters)
		{
			RSA rsa = RSA.Create();
			RSA rsa2;
			try
			{
				rsa.ImportParameters(parameters);
				rsa2 = rsa;
			}
			catch
			{
				rsa.Dispose();
				throw;
			}
			return rsa2;
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x000AD9F4 File Offset: 0x000ABBF4
		public virtual bool TryDecrypt(ReadOnlySpan<byte> data, Span<byte> destination, RSAEncryptionPadding padding, out int bytesWritten)
		{
			byte[] array = this.Decrypt(data.ToArray(), padding);
			if (destination.Length >= array.Length)
			{
				new ReadOnlySpan<byte>(array).CopyTo(destination);
				bytesWritten = array.Length;
				return true;
			}
			bytesWritten = 0;
			return false;
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x000ADA38 File Offset: 0x000ABC38
		public virtual bool TryEncrypt(ReadOnlySpan<byte> data, Span<byte> destination, RSAEncryptionPadding padding, out int bytesWritten)
		{
			byte[] array = this.Encrypt(data.ToArray(), padding);
			if (destination.Length >= array.Length)
			{
				new ReadOnlySpan<byte>(array).CopyTo(destination);
				bytesWritten = array.Length;
				return true;
			}
			bytesWritten = 0;
			return false;
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x000ADA7C File Offset: 0x000ABC7C
		protected virtual bool TryHashData(ReadOnlySpan<byte> data, Span<byte> destination, HashAlgorithmName hashAlgorithm, out int bytesWritten)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(data.Length);
			byte[] array2;
			try
			{
				data.CopyTo(array);
				array2 = this.HashData(array, 0, data.Length, hashAlgorithm);
			}
			finally
			{
				Array.Clear(array, 0, data.Length);
				ArrayPool<byte>.Shared.Return(array, false);
			}
			if (destination.Length >= array2.Length)
			{
				new ReadOnlySpan<byte>(array2).CopyTo(destination);
				bytesWritten = array2.Length;
				return true;
			}
			bytesWritten = 0;
			return false;
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x000ADB10 File Offset: 0x000ABD10
		public virtual bool TrySignHash(ReadOnlySpan<byte> hash, Span<byte> destination, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding, out int bytesWritten)
		{
			byte[] array = this.SignHash(hash.ToArray(), hashAlgorithm, padding);
			if (destination.Length >= array.Length)
			{
				new ReadOnlySpan<byte>(array).CopyTo(destination);
				bytesWritten = array.Length;
				return true;
			}
			bytesWritten = 0;
			return false;
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x000ADB58 File Offset: 0x000ABD58
		public virtual bool TrySignData(ReadOnlySpan<byte> data, Span<byte> destination, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding, out int bytesWritten)
		{
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			int num;
			if (this.TryHashData(data, destination, hashAlgorithm, out num) && this.TrySignHash(destination.Slice(0, num), destination, hashAlgorithm, padding, out bytesWritten))
			{
				return true;
			}
			bytesWritten = 0;
			return false;
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x000ADBC0 File Offset: 0x000ABDC0
		public virtual bool VerifyData(ReadOnlySpan<byte> data, ReadOnlySpan<byte> signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			int num = 256;
			checked
			{
				bool flag;
				for (;;)
				{
					int num2 = 0;
					byte[] array = ArrayPool<byte>.Shared.Rent(num);
					try
					{
						if (this.TryHashData(data, array, hashAlgorithm, out num2))
						{
							flag = this.VerifyHash(new ReadOnlySpan<byte>(array, 0, num2), signature, hashAlgorithm, padding);
							break;
						}
					}
					finally
					{
						Array.Clear(array, 0, num2);
						ArrayPool<byte>.Shared.Return(array, false);
					}
					num *= 2;
				}
				return flag;
			}
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x000ADC60 File Offset: 0x000ABE60
		public virtual bool VerifyHash(ReadOnlySpan<byte> hash, ReadOnlySpan<byte> signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			return this.VerifyHash(hash.ToArray(), signature.ToArray(), hashAlgorithm, padding);
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x0001B98F File Offset: 0x00019B8F
		public virtual byte[] ExportRSAPrivateKey()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x0001B98F File Offset: 0x00019B8F
		public virtual byte[] ExportRSAPublicKey()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x0001B98F File Offset: 0x00019B8F
		public virtual void ImportRSAPrivateKey(ReadOnlySpan<byte> source, out int bytesRead)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x0001B98F File Offset: 0x00019B8F
		public virtual void ImportRSAPublicKey(ReadOnlySpan<byte> source, out int bytesRead)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x0001B98F File Offset: 0x00019B8F
		public virtual bool TryExportRSAPrivateKey(Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x0001B98F File Offset: 0x00019B8F
		public virtual bool TryExportRSAPublicKey(Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
