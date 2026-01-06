using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000C4 RID: 196
	internal enum DnsType : ushort
	{
		// Token: 0x04000329 RID: 809
		A = 1,
		// Token: 0x0400032A RID: 810
		NS,
		// Token: 0x0400032B RID: 811
		[Obsolete]
		MD,
		// Token: 0x0400032C RID: 812
		[Obsolete]
		MF,
		// Token: 0x0400032D RID: 813
		CNAME,
		// Token: 0x0400032E RID: 814
		SOA,
		// Token: 0x0400032F RID: 815
		[Obsolete]
		MB,
		// Token: 0x04000330 RID: 816
		[Obsolete]
		MG,
		// Token: 0x04000331 RID: 817
		[Obsolete]
		MR,
		// Token: 0x04000332 RID: 818
		[Obsolete]
		NULL,
		// Token: 0x04000333 RID: 819
		[Obsolete]
		WKS,
		// Token: 0x04000334 RID: 820
		PTR,
		// Token: 0x04000335 RID: 821
		[Obsolete]
		HINFO,
		// Token: 0x04000336 RID: 822
		[Obsolete]
		MINFO,
		// Token: 0x04000337 RID: 823
		MX,
		// Token: 0x04000338 RID: 824
		TXT,
		// Token: 0x04000339 RID: 825
		[Obsolete]
		RP,
		// Token: 0x0400033A RID: 826
		AFSDB,
		// Token: 0x0400033B RID: 827
		[Obsolete]
		X25,
		// Token: 0x0400033C RID: 828
		[Obsolete]
		ISDN,
		// Token: 0x0400033D RID: 829
		[Obsolete]
		RT,
		// Token: 0x0400033E RID: 830
		[Obsolete]
		NSAP,
		// Token: 0x0400033F RID: 831
		[Obsolete]
		NSAPPTR,
		// Token: 0x04000340 RID: 832
		SIG,
		// Token: 0x04000341 RID: 833
		KEY,
		// Token: 0x04000342 RID: 834
		[Obsolete]
		PX,
		// Token: 0x04000343 RID: 835
		[Obsolete]
		GPOS,
		// Token: 0x04000344 RID: 836
		AAAA,
		// Token: 0x04000345 RID: 837
		LOC,
		// Token: 0x04000346 RID: 838
		[Obsolete]
		NXT,
		// Token: 0x04000347 RID: 839
		[Obsolete]
		EID,
		// Token: 0x04000348 RID: 840
		[Obsolete]
		NIMLOC,
		// Token: 0x04000349 RID: 841
		SRV,
		// Token: 0x0400034A RID: 842
		[Obsolete]
		ATMA,
		// Token: 0x0400034B RID: 843
		NAPTR,
		// Token: 0x0400034C RID: 844
		KX,
		// Token: 0x0400034D RID: 845
		CERT,
		// Token: 0x0400034E RID: 846
		[Obsolete]
		A6,
		// Token: 0x0400034F RID: 847
		DNAME,
		// Token: 0x04000350 RID: 848
		[Obsolete]
		SINK,
		// Token: 0x04000351 RID: 849
		OPT,
		// Token: 0x04000352 RID: 850
		[Obsolete]
		APL,
		// Token: 0x04000353 RID: 851
		DS,
		// Token: 0x04000354 RID: 852
		SSHFP,
		// Token: 0x04000355 RID: 853
		IPSECKEY,
		// Token: 0x04000356 RID: 854
		RRSIG,
		// Token: 0x04000357 RID: 855
		NSEC,
		// Token: 0x04000358 RID: 856
		DNSKEY,
		// Token: 0x04000359 RID: 857
		DHCID,
		// Token: 0x0400035A RID: 858
		NSEC3,
		// Token: 0x0400035B RID: 859
		NSEC3PARAM,
		// Token: 0x0400035C RID: 860
		HIP = 55,
		// Token: 0x0400035D RID: 861
		NINFO,
		// Token: 0x0400035E RID: 862
		RKEY,
		// Token: 0x0400035F RID: 863
		TALINK,
		// Token: 0x04000360 RID: 864
		SPF = 99,
		// Token: 0x04000361 RID: 865
		[Obsolete]
		UINFO,
		// Token: 0x04000362 RID: 866
		[Obsolete]
		UID,
		// Token: 0x04000363 RID: 867
		[Obsolete]
		GID,
		// Token: 0x04000364 RID: 868
		[Obsolete]
		UNSPEC,
		// Token: 0x04000365 RID: 869
		TKEY = 249,
		// Token: 0x04000366 RID: 870
		TSIG,
		// Token: 0x04000367 RID: 871
		IXFR,
		// Token: 0x04000368 RID: 872
		AXFR,
		// Token: 0x04000369 RID: 873
		[Obsolete]
		MAILB,
		// Token: 0x0400036A RID: 874
		[Obsolete]
		MAILA,
		// Token: 0x0400036B RID: 875
		URI = 256,
		// Token: 0x0400036C RID: 876
		TA = 32768,
		// Token: 0x0400036D RID: 877
		DLV
	}
}
