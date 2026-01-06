using System;
using System.Globalization;
using System.Net;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002BB RID: 699
	internal sealed class GeneralNameEncoder
	{
		// Token: 0x060015F2 RID: 5618 RVA: 0x00057E92 File Offset: 0x00056092
		internal byte[][] EncodeEmailAddress(string emailAddress)
		{
			byte[][] array = DerEncoder.SegmentedEncodeIA5String(emailAddress.ToCharArray());
			array[0][0] = 129;
			return array;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00057EA9 File Offset: 0x000560A9
		internal byte[][] EncodeDnsName(string dnsName)
		{
			byte[][] array = DerEncoder.SegmentedEncodeIA5String(GeneralNameEncoder.s_idnMapping.GetAscii(dnsName).ToCharArray());
			array[0][0] = 130;
			return array;
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00057ECA File Offset: 0x000560CA
		internal byte[][] EncodeUri(Uri uri)
		{
			byte[][] array = DerEncoder.SegmentedEncodeIA5String(uri.AbsoluteUri.ToCharArray());
			array[0][0] = 134;
			return array;
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00057EE6 File Offset: 0x000560E6
		internal byte[][] EncodeIpAddress(IPAddress address)
		{
			byte[][] array = DerEncoder.SegmentedEncodeOctetString(address.GetAddressBytes());
			array[0][0] = 135;
			return array;
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00057F00 File Offset: 0x00056100
		internal byte[][] EncodeUserPrincipalName(string upn)
		{
			byte[][] array = DerEncoder.SegmentedEncodeUtf8String(upn.ToCharArray());
			byte[][] array2 = DerEncoder.ConstructSegmentedSequence(new byte[][][] { array });
			array2[0][0] = 160;
			byte[][] array3 = DerEncoder.ConstructSegmentedSequence(new byte[][][]
			{
				DerEncoder.SegmentedEncodeOid("1.3.6.1.4.1.311.20.2.3"),
				array2
			});
			array3[0][0] = 160;
			return array3;
		}

		// Token: 0x04000C4C RID: 3148
		private static readonly IdnMapping s_idnMapping = new IdnMapping();

		// Token: 0x020002BC RID: 700
		private enum GeneralNameTag : byte
		{
			// Token: 0x04000C4E RID: 3150
			OtherName = 160,
			// Token: 0x04000C4F RID: 3151
			Rfc822Name = 129,
			// Token: 0x04000C50 RID: 3152
			DnsName,
			// Token: 0x04000C51 RID: 3153
			X400Address,
			// Token: 0x04000C52 RID: 3154
			DirectoryName,
			// Token: 0x04000C53 RID: 3155
			EdiPartyName,
			// Token: 0x04000C54 RID: 3156
			Uri,
			// Token: 0x04000C55 RID: 3157
			IpAddress,
			// Token: 0x04000C56 RID: 3158
			RegisteredId
		}
	}
}
