using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Net
{
	// Token: 0x020003EE RID: 1006
	internal static class NclUtilities
	{
		// Token: 0x060020AE RID: 8366 RVA: 0x000778CC File Offset: 0x00075ACC
		internal static bool IsThreadPoolLow()
		{
			int num;
			int num2;
			ThreadPool.GetAvailableThreads(out num, out num2);
			return num < 2;
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060020AF RID: 8367 RVA: 0x000778E6 File Offset: 0x00075AE6
		internal static bool HasShutdownStarted
		{
			get
			{
				return Environment.HasShutdownStarted || AppDomain.CurrentDomain.IsFinalizingForUnload();
			}
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x000778FC File Offset: 0x00075AFC
		internal static bool IsCredentialFailure(SecurityStatus error)
		{
			return error == SecurityStatus.LogonDenied || error == SecurityStatus.UnknownCredentials || error == SecurityStatus.NoImpersonation || error == SecurityStatus.NoAuthenticatingAuthority || error == SecurityStatus.UntrustedRoot || error == SecurityStatus.CertExpired || error == SecurityStatus.SmartcardLogonRequired || error == SecurityStatus.BadBinding;
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x0007794C File Offset: 0x00075B4C
		internal static bool IsClientFault(SecurityStatus error)
		{
			return error == SecurityStatus.InvalidToken || error == SecurityStatus.CannotPack || error == SecurityStatus.QopNotSupported || error == SecurityStatus.NoCredentials || error == SecurityStatus.MessageAltered || error == SecurityStatus.OutOfSequence || error == SecurityStatus.IncompleteMessage || error == SecurityStatus.IncompleteCredentials || error == SecurityStatus.WrongPrincipal || error == SecurityStatus.TimeSkew || error == SecurityStatus.IllegalMessage || error == SecurityStatus.CertUnknown || error == SecurityStatus.AlgorithmMismatch || error == SecurityStatus.SecurityQosFailed || error == SecurityStatus.UnsupportedPreauth;
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060020B2 RID: 8370 RVA: 0x000779D3 File Offset: 0x00075BD3
		internal static ContextCallback ContextRelativeDemandCallback
		{
			get
			{
				if (NclUtilities.s_ContextRelativeDemandCallback == null)
				{
					NclUtilities.s_ContextRelativeDemandCallback = new ContextCallback(NclUtilities.DemandCallback);
				}
				return NclUtilities.s_ContextRelativeDemandCallback;
			}
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x00003917 File Offset: 0x00001B17
		private static void DemandCallback(object state)
		{
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x000779F8 File Offset: 0x00075BF8
		internal static bool GuessWhetherHostIsLoopback(string host)
		{
			string text = host.ToLowerInvariant();
			return text == "localhost" || text == "loopback";
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x00077A29 File Offset: 0x00075C29
		internal static bool IsFatal(Exception exception)
		{
			return exception != null && (exception is OutOfMemoryException || exception is StackOverflowException || exception is ThreadAbortException);
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x00077A4C File Offset: 0x00075C4C
		internal static bool IsAddressLocal(IPAddress ipAddress)
		{
			IPAddress[] localAddresses = NclUtilities.LocalAddresses;
			for (int i = 0; i < localAddresses.Length; i++)
			{
				if (ipAddress.Equals(localAddresses[i], false))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x00077A7C File Offset: 0x00075C7C
		private static IPHostEntry GetLocalHost()
		{
			return Dns.GetHostByName(Dns.GetHostName());
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060020B8 RID: 8376 RVA: 0x00077A88 File Offset: 0x00075C88
		internal static IPAddress[] LocalAddresses
		{
			get
			{
				IPAddress[] array = NclUtilities._LocalAddresses;
				if (array != null)
				{
					return array;
				}
				object localAddressesLock = NclUtilities.LocalAddressesLock;
				IPAddress[] array2;
				lock (localAddressesLock)
				{
					array = NclUtilities._LocalAddresses;
					if (array != null)
					{
						array2 = array;
					}
					else
					{
						List<IPAddress> list = new List<IPAddress>();
						try
						{
							IPHostEntry localHost = NclUtilities.GetLocalHost();
							if (localHost != null)
							{
								if (localHost.HostName != null)
								{
									int num = localHost.HostName.IndexOf('.');
									if (num != -1)
									{
										NclUtilities._LocalDomainName = localHost.HostName.Substring(num);
									}
								}
								IPAddress[] addressList = localHost.AddressList;
								if (addressList != null)
								{
									foreach (IPAddress ipaddress in addressList)
									{
										list.Add(ipaddress);
									}
								}
							}
						}
						catch
						{
						}
						array = new IPAddress[list.Count];
						int num2 = 0;
						foreach (IPAddress ipaddress2 in list)
						{
							array[num2] = ipaddress2;
							num2++;
						}
						NclUtilities._LocalAddresses = array;
						array2 = array;
					}
				}
				return array2;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x00077BC8 File Offset: 0x00075DC8
		private static object LocalAddressesLock
		{
			get
			{
				if (NclUtilities._LocalAddressesLock == null)
				{
					Interlocked.CompareExchange(ref NclUtilities._LocalAddressesLock, new object(), null);
				}
				return NclUtilities._LocalAddressesLock;
			}
		}

		// Token: 0x040011CC RID: 4556
		private static volatile ContextCallback s_ContextRelativeDemandCallback;

		// Token: 0x040011CD RID: 4557
		private static volatile IPAddress[] _LocalAddresses;

		// Token: 0x040011CE RID: 4558
		private static object _LocalAddressesLock;

		// Token: 0x040011CF RID: 4559
		private const int HostNameBufferLength = 256;

		// Token: 0x040011D0 RID: 4560
		internal static string _LocalDomainName;
	}
}
