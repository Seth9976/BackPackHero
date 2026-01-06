using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.SqlClient
{
	// Token: 0x02000128 RID: 296
	internal static class SNINativeMethodWrapper
	{
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x0004F588 File Offset: 0x0004D788
		internal static int SniMaxComposedSpnLength
		{
			get
			{
				if (SNINativeMethodWrapper.s_sniMaxComposedSpnLength == -1)
				{
					SNINativeMethodWrapper.s_sniMaxComposedSpnLength = checked((int)SNINativeMethodWrapper.GetSniMaxComposedSpnLength());
				}
				return SNINativeMethodWrapper.s_sniMaxComposedSpnLength;
			}
		}

		// Token: 0x06000FF4 RID: 4084
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIAddProviderWrapper")]
		internal static extern uint SNIAddProvider(SNIHandle pConn, SNINativeMethodWrapper.ProviderEnum ProvNum, [In] ref uint pInfo);

		// Token: 0x06000FF5 RID: 4085
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNICheckConnectionWrapper")]
		internal static extern uint SNICheckConnection([In] SNIHandle pConn);

		// Token: 0x06000FF6 RID: 4086
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNICloseWrapper")]
		internal static extern uint SNIClose(IntPtr pConn);

		// Token: 0x06000FF7 RID: 4087
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SNIGetLastError(out SNINativeMethodWrapper.SNI_Error pErrorStruct);

		// Token: 0x06000FF8 RID: 4088
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SNIPacketRelease(IntPtr pPacket);

		// Token: 0x06000FF9 RID: 4089
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIPacketResetWrapper")]
		internal static extern void SNIPacketReset([In] SNIHandle pConn, SNINativeMethodWrapper.IOType IOType, SNIPacket pPacket, SNINativeMethodWrapper.ConsumerNumber ConsNum);

		// Token: 0x06000FFA RID: 4090
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIQueryInfo(SNINativeMethodWrapper.QTypes QType, ref uint pbQInfo);

		// Token: 0x06000FFB RID: 4091
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIQueryInfo(SNINativeMethodWrapper.QTypes QType, ref IntPtr pbQInfo);

		// Token: 0x06000FFC RID: 4092
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIReadAsyncWrapper")]
		internal static extern uint SNIReadAsync(SNIHandle pConn, ref IntPtr ppNewPacket);

		// Token: 0x06000FFD RID: 4093
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIReadSyncOverAsync(SNIHandle pConn, ref IntPtr ppNewPacket, int timeout);

		// Token: 0x06000FFE RID: 4094
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIRemoveProviderWrapper")]
		internal static extern uint SNIRemoveProvider(SNIHandle pConn, SNINativeMethodWrapper.ProviderEnum ProvNum);

		// Token: 0x06000FFF RID: 4095
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNISecInitPackage(ref uint pcbMaxToken);

		// Token: 0x06001000 RID: 4096
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNISetInfoWrapper")]
		internal static extern uint SNISetInfo(SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, [In] ref uint pbQInfo);

		// Token: 0x06001001 RID: 4097
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNITerminate();

		// Token: 0x06001002 RID: 4098
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIWaitForSSLHandshakeToCompleteWrapper")]
		internal static extern uint SNIWaitForSSLHandshakeToComplete([In] SNIHandle pConn, int dwMilliseconds);

		// Token: 0x06001003 RID: 4099
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint UnmanagedIsTokenRestricted([In] IntPtr token, [MarshalAs(UnmanagedType.Bool)] out bool isRestricted);

		// Token: 0x06001004 RID: 4100
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint GetSniMaxComposedSpnLength();

		// Token: 0x06001005 RID: 4101
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint SNIGetInfoWrapper([In] SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, out Guid pbQInfo);

		// Token: 0x06001006 RID: 4102
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint SNIInitialize([In] IntPtr pmo);

		// Token: 0x06001007 RID: 4103
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint SNIOpenSyncExWrapper(ref SNINativeMethodWrapper.SNI_CLIENT_CONSUMER_INFO pClientConsumerInfo, out IntPtr ppConn);

		// Token: 0x06001008 RID: 4104
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint SNIOpenWrapper([In] ref SNINativeMethodWrapper.Sni_Consumer_Info pConsumerInfo, [MarshalAs(UnmanagedType.LPStr)] string szConnect, [In] SNIHandle pConn, out IntPtr ppConn, [MarshalAs(UnmanagedType.Bool)] bool fSync);

		// Token: 0x06001009 RID: 4105
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr SNIPacketAllocateWrapper([In] SafeHandle pConn, SNINativeMethodWrapper.IOType IOType);

		// Token: 0x0600100A RID: 4106
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint SNIPacketGetDataWrapper([In] IntPtr packet, [In] [Out] byte[] readBuffer, uint readBufferLength, out uint dataSize);

		// Token: 0x0600100B RID: 4107
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		private unsafe static extern void SNIPacketSetData(SNIPacket pPacket, [In] byte* pbBuf, uint cbBuf);

		// Token: 0x0600100C RID: 4108
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		private unsafe static extern uint SNISecGenClientContextWrapper([In] SNIHandle pConn, [In] [Out] byte[] pIn, uint cbIn, [In] [Out] byte[] pOut, [In] ref uint pcbOut, [MarshalAs(UnmanagedType.Bool)] out bool pfDone, byte* szServerInfo, uint cbServerInfo, [MarshalAs(UnmanagedType.LPWStr)] string pwszUserName, [MarshalAs(UnmanagedType.LPWStr)] string pwszPassword);

		// Token: 0x0600100D RID: 4109
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint SNIWriteAsyncWrapper(SNIHandle pConn, [In] SNIPacket pPacket);

		// Token: 0x0600100E RID: 4110
		[DllImport("sni.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern uint SNIWriteSyncOverAsync(SNIHandle pConn, [In] SNIPacket pPacket);

		// Token: 0x0600100F RID: 4111 RVA: 0x0004F5A2 File Offset: 0x0004D7A2
		internal static uint SniGetConnectionId(SNIHandle pConn, ref Guid connId)
		{
			return SNINativeMethodWrapper.SNIGetInfoWrapper(pConn, SNINativeMethodWrapper.QTypes.SNI_QUERY_CONN_CONNID, out connId);
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0004F5AD File Offset: 0x0004D7AD
		internal static uint SNIInitialize()
		{
			return SNINativeMethodWrapper.SNIInitialize(IntPtr.Zero);
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0004F5BC File Offset: 0x0004D7BC
		internal static uint SNIOpenMarsSession(SNINativeMethodWrapper.ConsumerInfo consumerInfo, SNIHandle parent, ref IntPtr pConn, bool fSync)
		{
			SNINativeMethodWrapper.Sni_Consumer_Info sni_Consumer_Info = default(SNINativeMethodWrapper.Sni_Consumer_Info);
			SNINativeMethodWrapper.MarshalConsumerInfo(consumerInfo, ref sni_Consumer_Info);
			return SNINativeMethodWrapper.SNIOpenWrapper(ref sni_Consumer_Info, "session:", parent, out pConn, fSync);
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0004F5E8 File Offset: 0x0004D7E8
		internal unsafe static uint SNIOpenSyncEx(SNINativeMethodWrapper.ConsumerInfo consumerInfo, string constring, ref IntPtr pConn, byte[] spnBuffer, byte[] instanceName, bool fOverrideCache, bool fSync, int timeout, bool fParallel)
		{
			fixed (byte* ptr = &instanceName[0])
			{
				byte* ptr2 = ptr;
				SNINativeMethodWrapper.SNI_CLIENT_CONSUMER_INFO sni_CLIENT_CONSUMER_INFO = default(SNINativeMethodWrapper.SNI_CLIENT_CONSUMER_INFO);
				SNINativeMethodWrapper.MarshalConsumerInfo(consumerInfo, ref sni_CLIENT_CONSUMER_INFO.ConsumerInfo);
				sni_CLIENT_CONSUMER_INFO.wszConnectionString = constring;
				sni_CLIENT_CONSUMER_INFO.networkLibrary = SNINativeMethodWrapper.PrefixEnum.UNKNOWN_PREFIX;
				sni_CLIENT_CONSUMER_INFO.szInstanceName = ptr2;
				sni_CLIENT_CONSUMER_INFO.cchInstanceName = (uint)instanceName.Length;
				sni_CLIENT_CONSUMER_INFO.fOverrideLastConnectCache = fOverrideCache;
				sni_CLIENT_CONSUMER_INFO.fSynchronousConnection = fSync;
				sni_CLIENT_CONSUMER_INFO.timeout = timeout;
				sni_CLIENT_CONSUMER_INFO.fParallel = fParallel;
				sni_CLIENT_CONSUMER_INFO.transparentNetworkResolution = SNINativeMethodWrapper.TransparentNetworkResolutionMode.DisabledMode;
				sni_CLIENT_CONSUMER_INFO.totalTimeout = -1;
				sni_CLIENT_CONSUMER_INFO.isAzureSqlServerEndpoint = ADP.IsAzureSqlServerEndpoint(constring);
				if (spnBuffer != null)
				{
					fixed (byte* ptr3 = &spnBuffer[0])
					{
						byte* ptr4 = ptr3;
						sni_CLIENT_CONSUMER_INFO.szSPN = ptr4;
						sni_CLIENT_CONSUMER_INFO.cchSPN = (uint)spnBuffer.Length;
						return SNINativeMethodWrapper.SNIOpenSyncExWrapper(ref sni_CLIENT_CONSUMER_INFO, out pConn);
					}
				}
				return SNINativeMethodWrapper.SNIOpenSyncExWrapper(ref sni_CLIENT_CONSUMER_INFO, out pConn);
			}
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0004F6AD File Offset: 0x0004D8AD
		internal static void SNIPacketAllocate(SafeHandle pConn, SNINativeMethodWrapper.IOType IOType, ref IntPtr pPacket)
		{
			pPacket = SNINativeMethodWrapper.SNIPacketAllocateWrapper(pConn, IOType);
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0004F6B8 File Offset: 0x0004D8B8
		internal static uint SNIPacketGetData(IntPtr packet, byte[] readBuffer, ref uint dataSize)
		{
			return SNINativeMethodWrapper.SNIPacketGetDataWrapper(packet, readBuffer, (uint)readBuffer.Length, out dataSize);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0004F6C8 File Offset: 0x0004D8C8
		internal unsafe static void SNIPacketSetData(SNIPacket packet, byte[] data, int length)
		{
			fixed (byte* ptr = &data[0])
			{
				byte* ptr2 = ptr;
				SNINativeMethodWrapper.SNIPacketSetData(packet, ptr2, (uint)length);
			}
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0004F6EC File Offset: 0x0004D8EC
		internal unsafe static uint SNISecGenClientContext(SNIHandle pConnectionObject, byte[] inBuff, uint receivedLength, byte[] OutBuff, ref uint sendLength, byte[] serverUserName)
		{
			fixed (byte* ptr = &serverUserName[0])
			{
				byte* ptr2 = ptr;
				bool flag;
				return SNINativeMethodWrapper.SNISecGenClientContextWrapper(pConnectionObject, inBuff, receivedLength, OutBuff, ref sendLength, out flag, ptr2, (uint)serverUserName.Length, null, null);
			}
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0004F719 File Offset: 0x0004D919
		internal static uint SNIWritePacket(SNIHandle pConn, SNIPacket packet, bool sync)
		{
			if (sync)
			{
				return SNINativeMethodWrapper.SNIWriteSyncOverAsync(pConn, packet);
			}
			return SNINativeMethodWrapper.SNIWriteAsyncWrapper(pConn, packet);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0004F730 File Offset: 0x0004D930
		private static void MarshalConsumerInfo(SNINativeMethodWrapper.ConsumerInfo consumerInfo, ref SNINativeMethodWrapper.Sni_Consumer_Info native_consumerInfo)
		{
			native_consumerInfo.DefaultUserDataLength = consumerInfo.defaultBufferSize;
			native_consumerInfo.fnReadComp = ((consumerInfo.readDelegate != null) ? Marshal.GetFunctionPointerForDelegate<SNINativeMethodWrapper.SqlAsyncCallbackDelegate>(consumerInfo.readDelegate) : IntPtr.Zero);
			native_consumerInfo.fnWriteComp = ((consumerInfo.writeDelegate != null) ? Marshal.GetFunctionPointerForDelegate<SNINativeMethodWrapper.SqlAsyncCallbackDelegate>(consumerInfo.writeDelegate) : IntPtr.Zero);
			native_consumerInfo.ConsumerKey = consumerInfo.key;
		}

		// Token: 0x04000A23 RID: 2595
		private const string SNI = "sni.dll";

		// Token: 0x04000A24 RID: 2596
		private static int s_sniMaxComposedSpnLength = -1;

		// Token: 0x04000A25 RID: 2597
		private const int SniOpenTimeOut = -1;

		// Token: 0x02000129 RID: 297
		internal enum SniSpecialErrors : uint
		{
			// Token: 0x04000A27 RID: 2599
			LocalDBErrorCode = 50U,
			// Token: 0x04000A28 RID: 2600
			MultiSubnetFailoverWithMoreThan64IPs = 47U,
			// Token: 0x04000A29 RID: 2601
			MultiSubnetFailoverWithInstanceSpecified,
			// Token: 0x04000A2A RID: 2602
			MultiSubnetFailoverWithNonTcpProtocol,
			// Token: 0x04000A2B RID: 2603
			MaxErrorValue = 50157U
		}

		// Token: 0x0200012A RID: 298
		// (Invoke) Token: 0x0600101B RID: 4123
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void SqlAsyncCallbackDelegate(IntPtr m_ConsKey, IntPtr pPacket, uint dwError);

		// Token: 0x0200012B RID: 299
		internal struct ConsumerInfo
		{
			// Token: 0x04000A2C RID: 2604
			internal int defaultBufferSize;

			// Token: 0x04000A2D RID: 2605
			internal SNINativeMethodWrapper.SqlAsyncCallbackDelegate readDelegate;

			// Token: 0x04000A2E RID: 2606
			internal SNINativeMethodWrapper.SqlAsyncCallbackDelegate writeDelegate;

			// Token: 0x04000A2F RID: 2607
			internal IntPtr key;
		}

		// Token: 0x0200012C RID: 300
		internal enum ConsumerNumber
		{
			// Token: 0x04000A31 RID: 2609
			SNI_Consumer_SNI,
			// Token: 0x04000A32 RID: 2610
			SNI_Consumer_SSB,
			// Token: 0x04000A33 RID: 2611
			SNI_Consumer_PacketIsReleased,
			// Token: 0x04000A34 RID: 2612
			SNI_Consumer_Invalid
		}

		// Token: 0x0200012D RID: 301
		internal enum IOType
		{
			// Token: 0x04000A36 RID: 2614
			READ,
			// Token: 0x04000A37 RID: 2615
			WRITE
		}

		// Token: 0x0200012E RID: 302
		internal enum PrefixEnum
		{
			// Token: 0x04000A39 RID: 2617
			UNKNOWN_PREFIX,
			// Token: 0x04000A3A RID: 2618
			SM_PREFIX,
			// Token: 0x04000A3B RID: 2619
			TCP_PREFIX,
			// Token: 0x04000A3C RID: 2620
			NP_PREFIX,
			// Token: 0x04000A3D RID: 2621
			VIA_PREFIX,
			// Token: 0x04000A3E RID: 2622
			INVALID_PREFIX
		}

		// Token: 0x0200012F RID: 303
		internal enum ProviderEnum
		{
			// Token: 0x04000A40 RID: 2624
			HTTP_PROV,
			// Token: 0x04000A41 RID: 2625
			NP_PROV,
			// Token: 0x04000A42 RID: 2626
			SESSION_PROV,
			// Token: 0x04000A43 RID: 2627
			SIGN_PROV,
			// Token: 0x04000A44 RID: 2628
			SM_PROV,
			// Token: 0x04000A45 RID: 2629
			SMUX_PROV,
			// Token: 0x04000A46 RID: 2630
			SSL_PROV,
			// Token: 0x04000A47 RID: 2631
			TCP_PROV,
			// Token: 0x04000A48 RID: 2632
			VIA_PROV,
			// Token: 0x04000A49 RID: 2633
			MAX_PROVS,
			// Token: 0x04000A4A RID: 2634
			INVALID_PROV
		}

		// Token: 0x02000130 RID: 304
		internal enum QTypes
		{
			// Token: 0x04000A4C RID: 2636
			SNI_QUERY_CONN_INFO,
			// Token: 0x04000A4D RID: 2637
			SNI_QUERY_CONN_BUFSIZE,
			// Token: 0x04000A4E RID: 2638
			SNI_QUERY_CONN_KEY,
			// Token: 0x04000A4F RID: 2639
			SNI_QUERY_CLIENT_ENCRYPT_POSSIBLE,
			// Token: 0x04000A50 RID: 2640
			SNI_QUERY_SERVER_ENCRYPT_POSSIBLE,
			// Token: 0x04000A51 RID: 2641
			SNI_QUERY_CERTIFICATE,
			// Token: 0x04000A52 RID: 2642
			SNI_QUERY_LOCALDB_HMODULE,
			// Token: 0x04000A53 RID: 2643
			SNI_QUERY_CONN_ENCRYPT,
			// Token: 0x04000A54 RID: 2644
			SNI_QUERY_CONN_PROVIDERNUM,
			// Token: 0x04000A55 RID: 2645
			SNI_QUERY_CONN_CONNID,
			// Token: 0x04000A56 RID: 2646
			SNI_QUERY_CONN_PARENTCONNID,
			// Token: 0x04000A57 RID: 2647
			SNI_QUERY_CONN_SECPKG,
			// Token: 0x04000A58 RID: 2648
			SNI_QUERY_CONN_NETPACKETSIZE,
			// Token: 0x04000A59 RID: 2649
			SNI_QUERY_CONN_NODENUM,
			// Token: 0x04000A5A RID: 2650
			SNI_QUERY_CONN_PACKETSRECD,
			// Token: 0x04000A5B RID: 2651
			SNI_QUERY_CONN_PACKETSSENT,
			// Token: 0x04000A5C RID: 2652
			SNI_QUERY_CONN_PEERADDR,
			// Token: 0x04000A5D RID: 2653
			SNI_QUERY_CONN_PEERPORT,
			// Token: 0x04000A5E RID: 2654
			SNI_QUERY_CONN_LASTREADTIME,
			// Token: 0x04000A5F RID: 2655
			SNI_QUERY_CONN_LASTWRITETIME,
			// Token: 0x04000A60 RID: 2656
			SNI_QUERY_CONN_CONSUMER_ID,
			// Token: 0x04000A61 RID: 2657
			SNI_QUERY_CONN_CONNECTTIME,
			// Token: 0x04000A62 RID: 2658
			SNI_QUERY_CONN_HTTPENDPOINT,
			// Token: 0x04000A63 RID: 2659
			SNI_QUERY_CONN_LOCALADDR,
			// Token: 0x04000A64 RID: 2660
			SNI_QUERY_CONN_LOCALPORT,
			// Token: 0x04000A65 RID: 2661
			SNI_QUERY_CONN_SSLHANDSHAKESTATE,
			// Token: 0x04000A66 RID: 2662
			SNI_QUERY_CONN_SOBUFAUTOTUNING,
			// Token: 0x04000A67 RID: 2663
			SNI_QUERY_CONN_SECPKGNAME,
			// Token: 0x04000A68 RID: 2664
			SNI_QUERY_CONN_SECPKGMUTUALAUTH,
			// Token: 0x04000A69 RID: 2665
			SNI_QUERY_CONN_CONSUMERCONNID,
			// Token: 0x04000A6A RID: 2666
			SNI_QUERY_CONN_SNIUCI,
			// Token: 0x04000A6B RID: 2667
			SNI_QUERY_CONN_SUPPORTS_EXTENDED_PROTECTION,
			// Token: 0x04000A6C RID: 2668
			SNI_QUERY_CONN_CHANNEL_PROVIDES_AUTHENTICATION_CONTEXT,
			// Token: 0x04000A6D RID: 2669
			SNI_QUERY_CONN_PEERID,
			// Token: 0x04000A6E RID: 2670
			SNI_QUERY_CONN_SUPPORTS_SYNC_OVER_ASYNC
		}

		// Token: 0x02000131 RID: 305
		internal enum TransparentNetworkResolutionMode : byte
		{
			// Token: 0x04000A70 RID: 2672
			DisabledMode,
			// Token: 0x04000A71 RID: 2673
			SequentialMode,
			// Token: 0x04000A72 RID: 2674
			ParallelMode
		}

		// Token: 0x02000132 RID: 306
		private struct Sni_Consumer_Info
		{
			// Token: 0x04000A73 RID: 2675
			public int DefaultUserDataLength;

			// Token: 0x04000A74 RID: 2676
			public IntPtr ConsumerKey;

			// Token: 0x04000A75 RID: 2677
			public IntPtr fnReadComp;

			// Token: 0x04000A76 RID: 2678
			public IntPtr fnWriteComp;

			// Token: 0x04000A77 RID: 2679
			public IntPtr fnTrace;

			// Token: 0x04000A78 RID: 2680
			public IntPtr fnAcceptComp;

			// Token: 0x04000A79 RID: 2681
			public uint dwNumProts;

			// Token: 0x04000A7A RID: 2682
			public IntPtr rgListenInfo;

			// Token: 0x04000A7B RID: 2683
			public IntPtr NodeAffinity;
		}

		// Token: 0x02000133 RID: 307
		private struct SNI_CLIENT_CONSUMER_INFO
		{
			// Token: 0x04000A7C RID: 2684
			public SNINativeMethodWrapper.Sni_Consumer_Info ConsumerInfo;

			// Token: 0x04000A7D RID: 2685
			[MarshalAs(UnmanagedType.LPWStr)]
			public string wszConnectionString;

			// Token: 0x04000A7E RID: 2686
			public SNINativeMethodWrapper.PrefixEnum networkLibrary;

			// Token: 0x04000A7F RID: 2687
			public unsafe byte* szSPN;

			// Token: 0x04000A80 RID: 2688
			public uint cchSPN;

			// Token: 0x04000A81 RID: 2689
			public unsafe byte* szInstanceName;

			// Token: 0x04000A82 RID: 2690
			public uint cchInstanceName;

			// Token: 0x04000A83 RID: 2691
			[MarshalAs(UnmanagedType.Bool)]
			public bool fOverrideLastConnectCache;

			// Token: 0x04000A84 RID: 2692
			[MarshalAs(UnmanagedType.Bool)]
			public bool fSynchronousConnection;

			// Token: 0x04000A85 RID: 2693
			public int timeout;

			// Token: 0x04000A86 RID: 2694
			[MarshalAs(UnmanagedType.Bool)]
			public bool fParallel;

			// Token: 0x04000A87 RID: 2695
			public SNINativeMethodWrapper.TransparentNetworkResolutionMode transparentNetworkResolution;

			// Token: 0x04000A88 RID: 2696
			public int totalTimeout;

			// Token: 0x04000A89 RID: 2697
			public bool isAzureSqlServerEndpoint;
		}

		// Token: 0x02000134 RID: 308
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct SNI_Error
		{
			// Token: 0x04000A8A RID: 2698
			internal SNINativeMethodWrapper.ProviderEnum provider;

			// Token: 0x04000A8B RID: 2699
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 261)]
			internal string errorMessage;

			// Token: 0x04000A8C RID: 2700
			internal uint nativeError;

			// Token: 0x04000A8D RID: 2701
			internal uint sniError;

			// Token: 0x04000A8E RID: 2702
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string fileName;

			// Token: 0x04000A8F RID: 2703
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string function;

			// Token: 0x04000A90 RID: 2704
			internal uint lineNumber;
		}
	}
}
