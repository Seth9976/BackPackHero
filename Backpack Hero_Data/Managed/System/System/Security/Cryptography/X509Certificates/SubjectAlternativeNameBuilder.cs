using System;
using System.Collections.Generic;
using System.Net;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002C0 RID: 704
	public sealed class SubjectAlternativeNameBuilder
	{
		// Token: 0x060015F9 RID: 5625 RVA: 0x00057F65 File Offset: 0x00056165
		public void AddEmailAddress(string emailAddress)
		{
			if (string.IsNullOrEmpty(emailAddress))
			{
				throw new ArgumentOutOfRangeException("emailAddress", "String cannot be empty or null.");
			}
			this._encodedTlvs.Add(this._generalNameEncoder.EncodeEmailAddress(emailAddress));
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x00057F96 File Offset: 0x00056196
		public void AddDnsName(string dnsName)
		{
			if (string.IsNullOrEmpty(dnsName))
			{
				throw new ArgumentOutOfRangeException("dnsName", "String cannot be empty or null.");
			}
			this._encodedTlvs.Add(this._generalNameEncoder.EncodeDnsName(dnsName));
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x00057FC7 File Offset: 0x000561C7
		public void AddUri(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			this._encodedTlvs.Add(this._generalNameEncoder.EncodeUri(uri));
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00057FF4 File Offset: 0x000561F4
		public void AddIpAddress(IPAddress ipAddress)
		{
			if (ipAddress == null)
			{
				throw new ArgumentNullException("ipAddress");
			}
			this._encodedTlvs.Add(this._generalNameEncoder.EncodeIpAddress(ipAddress));
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x0005801B File Offset: 0x0005621B
		public void AddUserPrincipalName(string upn)
		{
			if (string.IsNullOrEmpty(upn))
			{
				throw new ArgumentOutOfRangeException("upn", "String cannot be empty or null.");
			}
			this._encodedTlvs.Add(this._generalNameEncoder.EncodeUserPrincipalName(upn));
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x0005804C File Offset: 0x0005624C
		public X509Extension Build(bool critical = false)
		{
			return new X509Extension("2.5.29.17", DerEncoder.ConstructSequence(this._encodedTlvs), critical);
		}

		// Token: 0x04000C69 RID: 3177
		private readonly List<byte[][]> _encodedTlvs = new List<byte[][]>();

		// Token: 0x04000C6A RID: 3178
		private readonly GeneralNameEncoder _generalNameEncoder = new GeneralNameEncoder();
	}
}
