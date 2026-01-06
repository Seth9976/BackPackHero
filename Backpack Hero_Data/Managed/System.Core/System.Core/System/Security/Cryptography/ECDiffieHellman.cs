using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Provides an abstract base class that Elliptic Curve Diffie-Hellman (ECDH) algorithm implementations can derive from. This class provides the basic set of operations that all ECDH implementations must support.</summary>
	// Token: 0x02000048 RID: 72
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class ECDiffieHellman : AsymmetricAlgorithm
	{
		/// <summary>Gets the name of the key exchange algorithm.</summary>
		/// <returns>The name of the key exchange algorithm. </returns>
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00003926 File Offset: 0x00001B26
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return "ECDiffieHellman";
			}
		}

		/// <summary>Gets the name of the signature algorithm.</summary>
		/// <returns>Always null.</returns>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000392D File Offset: 0x00001B2D
		public override string SignatureAlgorithm
		{
			get
			{
				return null;
			}
		}

		/// <summary>Creates a new instance of the default implementation of the Elliptic Curve Diffie-Hellman (ECDH) algorithm.</summary>
		/// <returns>A new instance of the default implementation of this class.</returns>
		// Token: 0x06000161 RID: 353 RVA: 0x000023CA File Offset: 0x000005CA
		public new static ECDiffieHellman Create()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a new instance of the specified implementation of the Elliptic Curve Diffie-Hellman (ECDH) algorithm.</summary>
		/// <returns>A new instance of the specified implementation of this class. If the specified algorithm name does not map to an ECDH implementation, this method returns null.</returns>
		/// <param name="algorithm">The name of an implementation of the ECDH algorithm. The following strings all refer to the same implementation, which is the only implementation currently supported in the .NET Framework:- "ECDH"- "ECDiffieHellman"- "ECDiffieHellmanCng"- "System.Security.Cryptography.ECDiffieHellmanCng"You can also provide the name of a custom ECDH implementation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="algorithm" /> parameter is null. </exception>
		// Token: 0x06000162 RID: 354 RVA: 0x00003930 File Offset: 0x00001B30
		public new static ECDiffieHellman Create(string algorithm)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			return CryptoConfig.CreateFromName(algorithm) as ECDiffieHellman;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000394C File Offset: 0x00001B4C
		public static ECDiffieHellman Create(ECCurve curve)
		{
			ECDiffieHellman ecdiffieHellman = ECDiffieHellman.Create();
			if (ecdiffieHellman != null)
			{
				try
				{
					ecdiffieHellman.GenerateKey(curve);
				}
				catch
				{
					ecdiffieHellman.Dispose();
					throw;
				}
			}
			return ecdiffieHellman;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00003988 File Offset: 0x00001B88
		public static ECDiffieHellman Create(ECParameters parameters)
		{
			ECDiffieHellman ecdiffieHellman = ECDiffieHellman.Create();
			if (ecdiffieHellman != null)
			{
				try
				{
					ecdiffieHellman.ImportParameters(parameters);
				}
				catch
				{
					ecdiffieHellman.Dispose();
					throw;
				}
			}
			return ecdiffieHellman;
		}

		/// <summary>Gets the public key that is being used by the current Elliptic Curve Diffie-Hellman (ECDH) instance.</summary>
		/// <returns>The public part of the ECDH key pair that is being used by this <see cref="T:System.Security.Cryptography.ECDiffieHellman" /> instance.</returns>
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000165 RID: 357
		public abstract ECDiffieHellmanPublicKey PublicKey { get; }

		/// <summary>Derives bytes that can be used as a key, given another party's public key.</summary>
		/// <returns>The key material from the key exchange with the other party’s public key.</returns>
		/// <param name="otherPartyPublicKey">The other party's public key.</param>
		// Token: 0x06000166 RID: 358 RVA: 0x000039C4 File Offset: 0x00001BC4
		public virtual byte[] DeriveKeyMaterial(ECDiffieHellmanPublicKey otherPartyPublicKey)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000039CB File Offset: 0x00001BCB
		public byte[] DeriveKeyFromHash(ECDiffieHellmanPublicKey otherPartyPublicKey, HashAlgorithmName hashAlgorithm)
		{
			return this.DeriveKeyFromHash(otherPartyPublicKey, hashAlgorithm, null, null);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000039C4 File Offset: 0x00001BC4
		public virtual byte[] DeriveKeyFromHash(ECDiffieHellmanPublicKey otherPartyPublicKey, HashAlgorithmName hashAlgorithm, byte[] secretPrepend, byte[] secretAppend)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000039D7 File Offset: 0x00001BD7
		public byte[] DeriveKeyFromHmac(ECDiffieHellmanPublicKey otherPartyPublicKey, HashAlgorithmName hashAlgorithm, byte[] hmacKey)
		{
			return this.DeriveKeyFromHmac(otherPartyPublicKey, hashAlgorithm, hmacKey, null, null);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000039C4 File Offset: 0x00001BC4
		public virtual byte[] DeriveKeyFromHmac(ECDiffieHellmanPublicKey otherPartyPublicKey, HashAlgorithmName hashAlgorithm, byte[] hmacKey, byte[] secretPrepend, byte[] secretAppend)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000039C4 File Offset: 0x00001BC4
		public virtual byte[] DeriveKeyTls(ECDiffieHellmanPublicKey otherPartyPublicKey, byte[] prfLabel, byte[] prfSeed)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000039E4 File Offset: 0x00001BE4
		private static Exception DerivedClassMustOverride()
		{
			return new NotImplementedException(global::SR.GetString("Method not supported. Derived class must override."));
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000039C4 File Offset: 0x00001BC4
		public virtual ECParameters ExportParameters(bool includePrivateParameters)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000039C4 File Offset: 0x00001BC4
		public virtual ECParameters ExportExplicitParameters(bool includePrivateParameters)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000039C4 File Offset: 0x00001BC4
		public virtual void ImportParameters(ECParameters parameters)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000039F5 File Offset: 0x00001BF5
		public virtual void GenerateKey(ECCurve curve)
		{
			throw new NotSupportedException(global::SR.GetString("Method not supported. Derived class must override."));
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00003A06 File Offset: 0x00001C06
		public virtual byte[] ExportECPrivateKey()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00003A06 File Offset: 0x00001C06
		public virtual bool TryExportECPrivateKey(Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00003A06 File Offset: 0x00001C06
		public virtual void ImportECPrivateKey(ReadOnlySpan<byte> source, out int bytesRead)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
