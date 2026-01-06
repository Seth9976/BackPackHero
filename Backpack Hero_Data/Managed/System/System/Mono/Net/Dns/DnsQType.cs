using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000B9 RID: 185
	internal enum DnsQType : ushort
	{
		// Token: 0x040002BB RID: 699
		A = 1,
		// Token: 0x040002BC RID: 700
		NS,
		// Token: 0x040002BD RID: 701
		[Obsolete]
		MD,
		// Token: 0x040002BE RID: 702
		[Obsolete]
		MF,
		// Token: 0x040002BF RID: 703
		CNAME,
		// Token: 0x040002C0 RID: 704
		SOA,
		// Token: 0x040002C1 RID: 705
		[Obsolete]
		MB,
		// Token: 0x040002C2 RID: 706
		[Obsolete]
		MG,
		// Token: 0x040002C3 RID: 707
		[Obsolete]
		MR,
		// Token: 0x040002C4 RID: 708
		[Obsolete]
		NULL,
		// Token: 0x040002C5 RID: 709
		[Obsolete]
		WKS,
		// Token: 0x040002C6 RID: 710
		PTR,
		// Token: 0x040002C7 RID: 711
		[Obsolete]
		HINFO,
		// Token: 0x040002C8 RID: 712
		[Obsolete]
		MINFO,
		// Token: 0x040002C9 RID: 713
		MX,
		// Token: 0x040002CA RID: 714
		TXT,
		// Token: 0x040002CB RID: 715
		[Obsolete]
		RP,
		// Token: 0x040002CC RID: 716
		AFSDB,
		// Token: 0x040002CD RID: 717
		[Obsolete]
		X25,
		// Token: 0x040002CE RID: 718
		[Obsolete]
		ISDN,
		// Token: 0x040002CF RID: 719
		[Obsolete]
		RT,
		// Token: 0x040002D0 RID: 720
		[Obsolete]
		NSAP,
		// Token: 0x040002D1 RID: 721
		[Obsolete]
		NSAPPTR,
		// Token: 0x040002D2 RID: 722
		SIG,
		// Token: 0x040002D3 RID: 723
		KEY,
		// Token: 0x040002D4 RID: 724
		[Obsolete]
		PX,
		// Token: 0x040002D5 RID: 725
		[Obsolete]
		GPOS,
		// Token: 0x040002D6 RID: 726
		AAAA,
		// Token: 0x040002D7 RID: 727
		LOC,
		// Token: 0x040002D8 RID: 728
		[Obsolete]
		NXT,
		// Token: 0x040002D9 RID: 729
		[Obsolete]
		EID,
		// Token: 0x040002DA RID: 730
		[Obsolete]
		NIMLOC,
		// Token: 0x040002DB RID: 731
		SRV,
		// Token: 0x040002DC RID: 732
		[Obsolete]
		ATMA,
		// Token: 0x040002DD RID: 733
		NAPTR,
		// Token: 0x040002DE RID: 734
		KX,
		// Token: 0x040002DF RID: 735
		CERT,
		// Token: 0x040002E0 RID: 736
		[Obsolete]
		A6,
		// Token: 0x040002E1 RID: 737
		DNAME,
		// Token: 0x040002E2 RID: 738
		[Obsolete]
		SINK,
		// Token: 0x040002E3 RID: 739
		OPT,
		// Token: 0x040002E4 RID: 740
		[Obsolete]
		APL,
		// Token: 0x040002E5 RID: 741
		DS,
		// Token: 0x040002E6 RID: 742
		SSHFP,
		// Token: 0x040002E7 RID: 743
		IPSECKEY,
		// Token: 0x040002E8 RID: 744
		RRSIG,
		// Token: 0x040002E9 RID: 745
		NSEC,
		// Token: 0x040002EA RID: 746
		DNSKEY,
		// Token: 0x040002EB RID: 747
		DHCID,
		// Token: 0x040002EC RID: 748
		NSEC3,
		// Token: 0x040002ED RID: 749
		NSEC3PARAM,
		// Token: 0x040002EE RID: 750
		HIP = 55,
		// Token: 0x040002EF RID: 751
		NINFO,
		// Token: 0x040002F0 RID: 752
		RKEY,
		// Token: 0x040002F1 RID: 753
		TALINK,
		// Token: 0x040002F2 RID: 754
		SPF = 99,
		// Token: 0x040002F3 RID: 755
		[Obsolete]
		UINFO,
		// Token: 0x040002F4 RID: 756
		[Obsolete]
		UID,
		// Token: 0x040002F5 RID: 757
		[Obsolete]
		GID,
		// Token: 0x040002F6 RID: 758
		[Obsolete]
		UNSPEC,
		// Token: 0x040002F7 RID: 759
		TKEY = 249,
		// Token: 0x040002F8 RID: 760
		TSIG,
		// Token: 0x040002F9 RID: 761
		IXFR,
		// Token: 0x040002FA RID: 762
		AXFR,
		// Token: 0x040002FB RID: 763
		[Obsolete]
		MAILB,
		// Token: 0x040002FC RID: 764
		[Obsolete]
		MAILA,
		// Token: 0x040002FD RID: 765
		ALL,
		// Token: 0x040002FE RID: 766
		URI,
		// Token: 0x040002FF RID: 767
		TA = 32768,
		// Token: 0x04000300 RID: 768
		DLV
	}
}
