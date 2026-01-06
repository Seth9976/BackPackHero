using System;
using System.Buffers;
using System.IO;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Provides an abstract base class that encapsulates the Elliptic Curve Digital Signature Algorithm (ECDSA).</summary>
	// Token: 0x0200004A RID: 74
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class ECDsa : AsymmetricAlgorithm
	{
		/// <summary>Gets the name of the key exchange algorithm.</summary>
		/// <returns>Always null.</returns>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000392D File Offset: 0x00001B2D
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the name of the signature algorithm.</summary>
		/// <returns>The string "ECDsa".</returns>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00003A7E File Offset: 0x00001C7E
		public override string SignatureAlgorithm
		{
			get
			{
				return "ECDsa";
			}
		}

		/// <summary>Creates a new instance of the default implementation of the Elliptic Curve Digital Signature Algorithm (ECDSA).</summary>
		/// <returns>A new instance of the default implementation (<see cref="T:System.Security.Cryptography.ECDsaCng" />) of this class.</returns>
		// Token: 0x0600017F RID: 383 RVA: 0x000023CA File Offset: 0x000005CA
		public new static ECDsa Create()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a new instance of the specified implementation of the Elliptic Curve Digital Signature Algorithm (ECDSA).</summary>
		/// <returns>A new instance of the specified implementation of this class. If the specified algorithm name does not map to an ECDSA implementation, this method returns null. </returns>
		/// <param name="algorithm">The name of an ECDSA implementation. The following strings all refer to the same implementation, which is the only implementation currently supported in the .NET Framework:- "ECDsa"- "ECDsaCng"- "System.Security.Cryptography.ECDsaCng"You can also provide the name of a custom ECDSA implementation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="algorithm" /> parameter is null.</exception>
		// Token: 0x06000180 RID: 384 RVA: 0x00003A85 File Offset: 0x00001C85
		public new static ECDsa Create(string algorithm)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			return CryptoConfig.CreateFromName(algorithm) as ECDsa;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00003AA0 File Offset: 0x00001CA0
		public static ECDsa Create(ECCurve curve)
		{
			ECDsa ecdsa = ECDsa.Create();
			if (ecdsa != null)
			{
				try
				{
					ecdsa.GenerateKey(curve);
				}
				catch
				{
					ecdsa.Dispose();
					throw;
				}
			}
			return ecdsa;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00003ADC File Offset: 0x00001CDC
		public static ECDsa Create(ECParameters parameters)
		{
			ECDsa ecdsa = ECDsa.Create();
			if (ecdsa != null)
			{
				try
				{
					ecdsa.ImportParameters(parameters);
				}
				catch
				{
					ecdsa.Dispose();
					throw;
				}
			}
			return ecdsa;
		}

		/// <summary>Generates a digital signature for the specified hash value. </summary>
		/// <returns>A digital signature that consists of the given hash value encrypted with the private key.</returns>
		/// <param name="hash">The hash value of the data that is being signed.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hash" /> parameter is null.</exception>
		// Token: 0x06000183 RID: 387
		public abstract byte[] SignHash(byte[] hash);

		/// <summary>Verifies a digital signature against the specified hash value. </summary>
		/// <returns>true if the hash value equals the decrypted signature; otherwise, false.</returns>
		/// <param name="hash">The hash value of a block of data.</param>
		/// <param name="signature">The digital signature to be verified.</param>
		// Token: 0x06000184 RID: 388
		public abstract bool VerifyHash(byte[] hash, byte[] signature);

		// Token: 0x06000185 RID: 389 RVA: 0x00003B18 File Offset: 0x00001D18
		protected virtual byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
		{
			throw ECDsa.DerivedClassMustOverride();
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00003B18 File Offset: 0x00001D18
		protected virtual byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
		{
			throw ECDsa.DerivedClassMustOverride();
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00003B1F File Offset: 0x00001D1F
		public virtual byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithm)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return this.SignData(data, 0, data.Length, hashAlgorithm);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00003B3C File Offset: 0x00001D3C
		public virtual byte[] SignData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
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
				throw ECDsa.HashAlgorithmNameNullOrEmpty();
			}
			byte[] array = this.HashData(data, offset, count, hashAlgorithm);
			return this.SignHash(array);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00003BAC File Offset: 0x00001DAC
		public virtual byte[] SignData(Stream data, HashAlgorithmName hashAlgorithm)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw ECDsa.HashAlgorithmNameNullOrEmpty();
			}
			byte[] array = this.HashData(data, hashAlgorithm);
			return this.SignHash(array);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00003BEB File Offset: 0x00001DEB
		public bool VerifyData(byte[] data, byte[] signature, HashAlgorithmName hashAlgorithm)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return this.VerifyData(data, 0, data.Length, signature, hashAlgorithm);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00003C08 File Offset: 0x00001E08
		public virtual bool VerifyData(byte[] data, int offset, int count, byte[] signature, HashAlgorithmName hashAlgorithm)
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
				throw ECDsa.HashAlgorithmNameNullOrEmpty();
			}
			byte[] array = this.HashData(data, offset, count, hashAlgorithm);
			return this.VerifyHash(array, signature);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00003C88 File Offset: 0x00001E88
		public bool VerifyData(Stream data, byte[] signature, HashAlgorithmName hashAlgorithm)
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
				throw ECDsa.HashAlgorithmNameNullOrEmpty();
			}
			byte[] array = this.HashData(data, hashAlgorithm);
			return this.VerifyHash(array, signature);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000039F5 File Offset: 0x00001BF5
		public virtual ECParameters ExportParameters(bool includePrivateParameters)
		{
			throw new NotSupportedException(global::SR.GetString("Method not supported. Derived class must override."));
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000039F5 File Offset: 0x00001BF5
		public virtual ECParameters ExportExplicitParameters(bool includePrivateParameters)
		{
			throw new NotSupportedException(global::SR.GetString("Method not supported. Derived class must override."));
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000039F5 File Offset: 0x00001BF5
		public virtual void ImportParameters(ECParameters parameters)
		{
			throw new NotSupportedException(global::SR.GetString("Method not supported. Derived class must override."));
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000039F5 File Offset: 0x00001BF5
		public virtual void GenerateKey(ECCurve curve)
		{
			throw new NotSupportedException(global::SR.GetString("Method not supported. Derived class must override."));
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000039E4 File Offset: 0x00001BE4
		private static Exception DerivedClassMustOverride()
		{
			return new NotImplementedException(global::SR.GetString("Method not supported. Derived class must override."));
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00003CD6 File Offset: 0x00001ED6
		internal static Exception HashAlgorithmNameNullOrEmpty()
		{
			return new ArgumentException(global::SR.GetString("The hash algorithm name cannot be null or empty."), "hashAlgorithm");
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00003CEC File Offset: 0x00001EEC
		protected virtual bool TryHashData(ReadOnlySpan<byte> data, Span<byte> destination, HashAlgorithmName hashAlgorithm, out int bytesWritten)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(data.Length);
			bool flag;
			try
			{
				data.CopyTo(array);
				byte[] array2 = this.HashData(array, 0, data.Length, hashAlgorithm);
				if (array2.Length <= destination.Length)
				{
					new ReadOnlySpan<byte>(array2).CopyTo(destination);
					bytesWritten = array2.Length;
					flag = true;
				}
				else
				{
					bytesWritten = 0;
					flag = false;
				}
			}
			finally
			{
				Array.Clear(array, 0, data.Length);
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return flag;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00003D84 File Offset: 0x00001F84
		public virtual bool TrySignHash(ReadOnlySpan<byte> hash, Span<byte> destination, out int bytesWritten)
		{
			byte[] array = this.SignHash(hash.ToArray());
			if (array.Length <= destination.Length)
			{
				new ReadOnlySpan<byte>(array).CopyTo(destination);
				bytesWritten = array.Length;
				return true;
			}
			bytesWritten = 0;
			return false;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00003DC5 File Offset: 0x00001FC5
		public virtual bool VerifyHash(ReadOnlySpan<byte> hash, ReadOnlySpan<byte> signature)
		{
			return this.VerifyHash(hash.ToArray(), signature.ToArray());
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00003DDC File Offset: 0x00001FDC
		public virtual bool TrySignData(ReadOnlySpan<byte> data, Span<byte> destination, HashAlgorithmName hashAlgorithm, out int bytesWritten)
		{
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException("The hash algorithm name cannot be null or empty.", "hashAlgorithm");
			}
			int num;
			if (this.TryHashData(data, destination, hashAlgorithm, out num) && this.TrySignHash(destination.Slice(0, num), destination, out bytesWritten))
			{
				return true;
			}
			bytesWritten = 0;
			return false;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00003E34 File Offset: 0x00002034
		public virtual bool VerifyData(ReadOnlySpan<byte> data, ReadOnlySpan<byte> signature, HashAlgorithmName hashAlgorithm)
		{
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException("The hash algorithm name cannot be null or empty.", "hashAlgorithm");
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
							flag = this.VerifyHash(new ReadOnlySpan<byte>(array, 0, num2), signature);
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

		// Token: 0x06000198 RID: 408 RVA: 0x00003A06 File Offset: 0x00001C06
		public virtual byte[] ExportECPrivateKey()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00003A06 File Offset: 0x00001C06
		public virtual bool TryExportECPrivateKey(Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00003A06 File Offset: 0x00001C06
		public virtual void ImportECPrivateKey(ReadOnlySpan<byte> source, out int bytesRead)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
