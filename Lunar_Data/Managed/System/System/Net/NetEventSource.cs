using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000377 RID: 887
	internal sealed class NetEventSource : EventSource
	{
		// Token: 0x06001D19 RID: 7449 RVA: 0x00069D2A File Offset: 0x00067F2A
		[NonEvent]
		public static void Enter(object thisOrContextObject, FormattableString formattableString = null, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Enter(NetEventSource.IdOf(thisOrContextObject), memberName, (formattableString != null) ? NetEventSource.Format(formattableString) : "");
			}
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x00069D54 File Offset: 0x00067F54
		[NonEvent]
		public static void Enter(object thisOrContextObject, object arg0, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Enter(NetEventSource.IdOf(thisOrContextObject), memberName, string.Format("({0})", NetEventSource.Format(arg0)));
			}
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x00069D7E File Offset: 0x00067F7E
		[NonEvent]
		public static void Enter(object thisOrContextObject, object arg0, object arg1, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Enter(NetEventSource.IdOf(thisOrContextObject), memberName, string.Format("({0}, {1})", NetEventSource.Format(arg0), NetEventSource.Format(arg1)));
			}
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x00069DAE File Offset: 0x00067FAE
		[NonEvent]
		public static void Enter(object thisOrContextObject, object arg0, object arg1, object arg2, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Enter(NetEventSource.IdOf(thisOrContextObject), memberName, string.Format("({0}, {1}, {2})", NetEventSource.Format(arg0), NetEventSource.Format(arg1), NetEventSource.Format(arg2)));
			}
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x00069DE5 File Offset: 0x00067FE5
		[Event(1, Level = EventLevel.Informational, Keywords = (EventKeywords)4L)]
		private void Enter(string thisOrContextObject, string memberName, string parameters)
		{
			base.WriteEvent(1, thisOrContextObject, memberName ?? "(?)", parameters);
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x00069DFA File Offset: 0x00067FFA
		[NonEvent]
		public static void Exit(object thisOrContextObject, FormattableString formattableString = null, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Exit(NetEventSource.IdOf(thisOrContextObject), memberName, (formattableString != null) ? NetEventSource.Format(formattableString) : "");
			}
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x00069E24 File Offset: 0x00068024
		[NonEvent]
		public static void Exit(object thisOrContextObject, object arg0, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Exit(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(arg0).ToString());
			}
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x00069E49 File Offset: 0x00068049
		[NonEvent]
		public static void Exit(object thisOrContextObject, object arg0, object arg1, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Exit(NetEventSource.IdOf(thisOrContextObject), memberName, string.Format("{0}, {1}", NetEventSource.Format(arg0), NetEventSource.Format(arg1)));
			}
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x00069E79 File Offset: 0x00068079
		[Event(2, Level = EventLevel.Informational, Keywords = (EventKeywords)4L)]
		private void Exit(string thisOrContextObject, string memberName, string result)
		{
			base.WriteEvent(2, thisOrContextObject, memberName ?? "(?)", result);
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x00069E8E File Offset: 0x0006808E
		[NonEvent]
		public static void Info(object thisOrContextObject, FormattableString formattableString = null, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Info(NetEventSource.IdOf(thisOrContextObject), memberName, (formattableString != null) ? NetEventSource.Format(formattableString) : "");
			}
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x00069EB8 File Offset: 0x000680B8
		[NonEvent]
		public static void Info(object thisOrContextObject, object message, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Info(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(message).ToString());
			}
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x00069EDD File Offset: 0x000680DD
		[Event(4, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void Info(string thisOrContextObject, string memberName, string message)
		{
			base.WriteEvent(4, thisOrContextObject, memberName ?? "(?)", message);
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x00069EF2 File Offset: 0x000680F2
		[NonEvent]
		public static void Error(object thisOrContextObject, FormattableString formattableString, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.ErrorMessage(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(formattableString));
			}
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x00069F12 File Offset: 0x00068112
		[NonEvent]
		public static void Error(object thisOrContextObject, object message, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.ErrorMessage(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(message).ToString());
			}
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x00069F37 File Offset: 0x00068137
		[Event(5, Level = EventLevel.Warning, Keywords = (EventKeywords)1L)]
		private void ErrorMessage(string thisOrContextObject, string memberName, string message)
		{
			base.WriteEvent(5, thisOrContextObject, memberName ?? "(?)", message);
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x00069F4C File Offset: 0x0006814C
		[NonEvent]
		public static void Fail(object thisOrContextObject, FormattableString formattableString, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.CriticalFailure(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(formattableString));
			}
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x00069F6C File Offset: 0x0006816C
		[NonEvent]
		public static void Fail(object thisOrContextObject, object message, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.CriticalFailure(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(message).ToString());
			}
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x00069F91 File Offset: 0x00068191
		[Event(6, Level = EventLevel.Critical, Keywords = (EventKeywords)2L)]
		private void CriticalFailure(string thisOrContextObject, string memberName, string message)
		{
			base.WriteEvent(6, thisOrContextObject, memberName ?? "(?)", message);
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x00069FA6 File Offset: 0x000681A6
		[NonEvent]
		public static void DumpBuffer(object thisOrContextObject, byte[] buffer, [CallerMemberName] string memberName = null)
		{
			NetEventSource.DumpBuffer(thisOrContextObject, buffer, 0, buffer.Length, memberName);
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x00069FB4 File Offset: 0x000681B4
		[NonEvent]
		public static void DumpBuffer(object thisOrContextObject, byte[] buffer, int offset, int count, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				if (offset < 0 || offset > buffer.Length - count)
				{
					NetEventSource.Fail(thisOrContextObject, FormattableStringFactory.Create("Invalid {0} Args. Length={1}, Offset={2}, Count={3}", new object[] { "DumpBuffer", buffer.Length, offset, count }), memberName);
					return;
				}
				count = Math.Min(count, 1024);
				byte[] array = buffer;
				if (offset != 0 || count != buffer.Length)
				{
					array = new byte[count];
					Buffer.BlockCopy(buffer, offset, array, 0, count);
				}
				NetEventSource.Log.DumpBuffer(NetEventSource.IdOf(thisOrContextObject), memberName, array);
			}
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x0006A054 File Offset: 0x00068254
		[NonEvent]
		public unsafe static void DumpBuffer(object thisOrContextObject, IntPtr bufferPtr, int count, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				byte[] array = new byte[Math.Min(count, 1024)];
				byte[] array2;
				byte* ptr;
				if ((array2 = array) == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				Buffer.MemoryCopy((void*)bufferPtr, (void*)ptr, (long)array.Length, (long)array.Length);
				array2 = null;
				NetEventSource.Log.DumpBuffer(NetEventSource.IdOf(thisOrContextObject), memberName, array);
			}
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x0006A0B9 File Offset: 0x000682B9
		[Event(7, Level = EventLevel.Verbose, Keywords = (EventKeywords)2L)]
		private void DumpBuffer(string thisOrContextObject, string memberName, byte[] buffer)
		{
			this.WriteEvent(7, thisOrContextObject, memberName ?? "(?)", buffer);
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x0006A0CE File Offset: 0x000682CE
		[NonEvent]
		public static void Associate(object first, object second, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Associate(NetEventSource.IdOf(first), memberName, NetEventSource.IdOf(first), NetEventSource.IdOf(second));
			}
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x0006A0F4 File Offset: 0x000682F4
		[NonEvent]
		public static void Associate(object thisOrContextObject, object first, object second, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Associate(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.IdOf(first), NetEventSource.IdOf(second));
			}
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x0006A11A File Offset: 0x0006831A
		[Event(3, Level = EventLevel.Informational, Keywords = (EventKeywords)1L, Message = "[{2}]<-->[{3}]")]
		private void Associate(string thisOrContextObject, string memberName, string first, string second)
		{
			this.WriteEvent(3, thisOrContextObject, memberName ?? "(?)", first, second);
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x0006A131 File Offset: 0x00068331
		[Conditional("DEBUG_NETEVENTSOURCE_MISUSE")]
		private static void DebugValidateArg(object arg)
		{
			bool isEnabled = NetEventSource.IsEnabled;
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("DEBUG_NETEVENTSOURCE_MISUSE")]
		private static void DebugValidateArg(FormattableString arg)
		{
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x0006A139 File Offset: 0x00068339
		public new static bool IsEnabled
		{
			get
			{
				return NetEventSource.Log.IsEnabled();
			}
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x0006A148 File Offset: 0x00068348
		[NonEvent]
		public static string IdOf(object value)
		{
			if (value == null)
			{
				return "(null)";
			}
			return value.GetType().Name + "#" + NetEventSource.GetHashCode(value).ToString();
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x0006A181 File Offset: 0x00068381
		[NonEvent]
		public static int GetHashCode(object value)
		{
			if (value == null)
			{
				return 0;
			}
			return value.GetHashCode();
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x0006A190 File Offset: 0x00068390
		[NonEvent]
		public static object Format(object value)
		{
			if (value == null)
			{
				return "(null)";
			}
			string text = null;
			if (text != null)
			{
				return text;
			}
			Array array = value as Array;
			if (array != null)
			{
				return string.Format("{0}[{1}]", array.GetType().GetElementType(), ((Array)value).Length);
			}
			ICollection collection = value as ICollection;
			if (collection != null)
			{
				return string.Format("{0}({1})", collection.GetType().Name, collection.Count);
			}
			SafeHandle safeHandle = value as SafeHandle;
			if (safeHandle != null)
			{
				return string.Format("{0}:{1}(0x{2:X})", safeHandle.GetType().Name, safeHandle.GetHashCode(), safeHandle.DangerousGetHandle());
			}
			if (value is IntPtr)
			{
				return string.Format("0x{0:X}", value);
			}
			string text2 = value.ToString();
			if (text2 == null || text2 == value.GetType().FullName)
			{
				return NetEventSource.IdOf(value);
			}
			return value;
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x0006A27C File Offset: 0x0006847C
		[NonEvent]
		private static string Format(FormattableString s)
		{
			switch (s.ArgumentCount)
			{
			case 0:
				return s.Format;
			case 1:
				return string.Format(s.Format, NetEventSource.Format(s.GetArgument(0)));
			case 2:
				return string.Format(s.Format, NetEventSource.Format(s.GetArgument(0)), NetEventSource.Format(s.GetArgument(1)));
			case 3:
				return string.Format(s.Format, NetEventSource.Format(s.GetArgument(0)), NetEventSource.Format(s.GetArgument(1)), NetEventSource.Format(s.GetArgument(2)));
			default:
			{
				object[] arguments = s.GetArguments();
				object[] array = new object[arguments.Length];
				for (int i = 0; i < arguments.Length; i++)
				{
					array[i] = NetEventSource.Format(arguments[i]);
				}
				return string.Format(s.Format, array);
			}
			}
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x0006A350 File Offset: 0x00068550
		[NonEvent]
		private unsafe void WriteEvent(int eventId, string arg1, string arg2, string arg3, string arg4)
		{
			if (base.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				if (arg2 == null)
				{
					arg2 = "";
				}
				if (arg3 == null)
				{
					arg3 = "";
				}
				if (arg4 == null)
				{
					arg4 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (string text2 = arg2)
					{
						char* ptr2 = text2;
						if (ptr2 != null)
						{
							ptr2 += RuntimeHelpers.OffsetToStringData / 2;
						}
						fixed (string text3 = arg3)
						{
							char* ptr3 = text3;
							if (ptr3 != null)
							{
								ptr3 += RuntimeHelpers.OffsetToStringData / 2;
							}
							fixed (string text4 = arg4)
							{
								char* ptr4 = text4;
								if (ptr4 != null)
								{
									ptr4 += RuntimeHelpers.OffsetToStringData / 2;
								}
								EventSource.EventData* ptr5;
								checked
								{
									ptr5 = stackalloc EventSource.EventData[unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData)];
								}
								*ptr5 = new EventSource.EventData
								{
									DataPointer = (IntPtr)((void*)ptr),
									Size = (arg1.Length + 1) * 2
								};
								ptr5[1] = new EventSource.EventData
								{
									DataPointer = (IntPtr)((void*)ptr2),
									Size = (arg2.Length + 1) * 2
								};
								ptr5[2] = new EventSource.EventData
								{
									DataPointer = (IntPtr)((void*)ptr3),
									Size = (arg3.Length + 1) * 2
								};
								ptr5[3] = new EventSource.EventData
								{
									DataPointer = (IntPtr)((void*)ptr4),
									Size = (arg4.Length + 1) * 2
								};
								base.WriteEventCore(eventId, 4, ptr5);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x0006A4DC File Offset: 0x000686DC
		[NonEvent]
		private unsafe void WriteEvent(int eventId, string arg1, string arg2, byte[] arg3)
		{
			if (base.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				if (arg2 == null)
				{
					arg2 = "";
				}
				if (arg3 == null)
				{
					arg3 = Array.Empty<byte>();
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (string text2 = arg2)
					{
						char* ptr2 = text2;
						if (ptr2 != null)
						{
							ptr2 += RuntimeHelpers.OffsetToStringData / 2;
						}
						byte[] array;
						byte* ptr3;
						if ((array = arg3) == null || array.Length == 0)
						{
							ptr3 = null;
						}
						else
						{
							ptr3 = &array[0];
						}
						int num = arg3.Length;
						EventSource.EventData* ptr4;
						checked
						{
							ptr4 = stackalloc EventSource.EventData[unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData)];
						}
						*ptr4 = new EventSource.EventData
						{
							DataPointer = (IntPtr)((void*)ptr),
							Size = (arg1.Length + 1) * 2
						};
						ptr4[1] = new EventSource.EventData
						{
							DataPointer = (IntPtr)((void*)ptr2),
							Size = (arg2.Length + 1) * 2
						};
						ptr4[2] = new EventSource.EventData
						{
							DataPointer = (IntPtr)((void*)(&num)),
							Size = 4
						};
						ptr4[3] = new EventSource.EventData
						{
							DataPointer = (IntPtr)((void*)ptr3),
							Size = num
						};
						base.WriteEventCore(eventId, 4, ptr4);
						array = null;
					}
				}
			}
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x0006A640 File Offset: 0x00068840
		[NonEvent]
		private unsafe void WriteEvent(int eventId, string arg1, int arg2, int arg3, int arg4)
		{
			if (base.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2;
					checked
					{
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData)];
					}
					*ptr2 = new EventSource.EventData
					{
						DataPointer = (IntPtr)((void*)ptr),
						Size = (arg1.Length + 1) * 2
					};
					ptr2[1] = new EventSource.EventData
					{
						DataPointer = (IntPtr)((void*)(&arg2)),
						Size = 4
					};
					ptr2[2] = new EventSource.EventData
					{
						DataPointer = (IntPtr)((void*)(&arg3)),
						Size = 4
					};
					ptr2[3] = new EventSource.EventData
					{
						DataPointer = (IntPtr)((void*)(&arg4)),
						Size = 4
					};
					base.WriteEventCore(eventId, 4, ptr2);
				}
			}
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x0006A744 File Offset: 0x00068944
		[NonEvent]
		private unsafe void WriteEvent(int eventId, string arg1, int arg2, string arg3)
		{
			if (base.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				if (arg3 == null)
				{
					arg3 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (string text2 = arg3)
					{
						char* ptr2 = text2;
						if (ptr2 != null)
						{
							ptr2 += RuntimeHelpers.OffsetToStringData / 2;
						}
						EventSource.EventData* ptr3;
						checked
						{
							ptr3 = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
						}
						*ptr3 = new EventSource.EventData
						{
							DataPointer = (IntPtr)((void*)ptr),
							Size = (arg1.Length + 1) * 2
						};
						ptr3[1] = new EventSource.EventData
						{
							DataPointer = (IntPtr)((void*)(&arg2)),
							Size = 4
						};
						ptr3[2] = new EventSource.EventData
						{
							DataPointer = (IntPtr)((void*)ptr2),
							Size = (arg3.Length + 1) * 2
						};
						base.WriteEventCore(eventId, 3, ptr3);
					}
				}
			}
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x0006A848 File Offset: 0x00068A48
		[NonEvent]
		private unsafe void WriteEvent(int eventId, string arg1, string arg2, int arg3)
		{
			if (base.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				if (arg2 == null)
				{
					arg2 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (string text2 = arg2)
					{
						char* ptr2 = text2;
						if (ptr2 != null)
						{
							ptr2 += RuntimeHelpers.OffsetToStringData / 2;
						}
						EventSource.EventData* ptr3;
						checked
						{
							ptr3 = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
						}
						*ptr3 = new EventSource.EventData
						{
							DataPointer = (IntPtr)((void*)ptr),
							Size = (arg1.Length + 1) * 2
						};
						ptr3[1] = new EventSource.EventData
						{
							DataPointer = (IntPtr)((void*)ptr2),
							Size = (arg2.Length + 1) * 2
						};
						ptr3[2] = new EventSource.EventData
						{
							DataPointer = (IntPtr)((void*)(&arg3)),
							Size = 4
						};
						base.WriteEventCore(eventId, 3, ptr3);
					}
				}
			}
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x0006A948 File Offset: 0x00068B48
		[NonEvent]
		private unsafe void WriteEvent(int eventId, string arg1, string arg2, string arg3, int arg4)
		{
			if (base.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				if (arg2 == null)
				{
					arg2 = "";
				}
				if (arg3 == null)
				{
					arg3 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (string text2 = arg2)
					{
						char* ptr2 = text2;
						if (ptr2 != null)
						{
							ptr2 += RuntimeHelpers.OffsetToStringData / 2;
						}
						fixed (string text3 = arg3)
						{
							char* ptr3 = text3;
							if (ptr3 != null)
							{
								ptr3 += RuntimeHelpers.OffsetToStringData / 2;
							}
							EventSource.EventData* ptr4;
							checked
							{
								ptr4 = stackalloc EventSource.EventData[unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData)];
							}
							*ptr4 = new EventSource.EventData
							{
								DataPointer = (IntPtr)((void*)ptr),
								Size = (arg1.Length + 1) * 2
							};
							ptr4[1] = new EventSource.EventData
							{
								DataPointer = (IntPtr)((void*)ptr2),
								Size = (arg2.Length + 1) * 2
							};
							ptr4[2] = new EventSource.EventData
							{
								DataPointer = (IntPtr)((void*)ptr3),
								Size = (arg3.Length + 1) * 2
							};
							ptr4[3] = new EventSource.EventData
							{
								DataPointer = (IntPtr)((void*)(&arg4)),
								Size = 4
							};
							base.WriteEventCore(eventId, 4, ptr4);
						}
					}
				}
			}
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x0006AAA6 File Offset: 0x00068CA6
		[Event(10, Keywords = (EventKeywords)1L, Level = EventLevel.Informational)]
		public void AcquireDefaultCredential(string packageName, global::Interop.SspiCli.CredentialUse intent)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(10, new object[] { packageName, intent });
			}
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x0006AACB File Offset: 0x00068CCB
		[NonEvent]
		public void AcquireCredentialsHandle(string packageName, global::Interop.SspiCli.CredentialUse intent, object authdata)
		{
			if (base.IsEnabled())
			{
				this.AcquireCredentialsHandle(packageName, intent, NetEventSource.IdOf(authdata));
			}
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x0006AAE3 File Offset: 0x00068CE3
		[Event(11, Keywords = (EventKeywords)1L, Level = EventLevel.Informational)]
		public void AcquireCredentialsHandle(string packageName, global::Interop.SspiCli.CredentialUse intent, string authdata)
		{
			if (base.IsEnabled())
			{
				this.WriteEvent(11, packageName, (int)intent, authdata);
			}
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x0006AAF8 File Offset: 0x00068CF8
		[NonEvent]
		public void InitializeSecurityContext(SafeFreeCredentials credential, SafeDeleteContext context, string targetName, global::Interop.SspiCli.ContextFlags inFlags)
		{
			if (base.IsEnabled())
			{
				this.InitializeSecurityContext(NetEventSource.IdOf(credential), NetEventSource.IdOf(context), targetName, inFlags);
			}
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x0006AB17 File Offset: 0x00068D17
		[Event(12, Keywords = (EventKeywords)1L, Level = EventLevel.Informational)]
		private void InitializeSecurityContext(string credential, string context, string targetName, global::Interop.SspiCli.ContextFlags inFlags)
		{
			this.WriteEvent(12, credential, context, targetName, (int)inFlags);
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x0006AB26 File Offset: 0x00068D26
		[NonEvent]
		public void AcceptSecurityContext(SafeFreeCredentials credential, SafeDeleteContext context, global::Interop.SspiCli.ContextFlags inFlags)
		{
			if (base.IsEnabled())
			{
				this.AcceptSecurityContext(NetEventSource.IdOf(credential), NetEventSource.IdOf(context), inFlags);
			}
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x0006AB43 File Offset: 0x00068D43
		[Event(15, Keywords = (EventKeywords)1L, Level = EventLevel.Informational)]
		private void AcceptSecurityContext(string credential, string context, global::Interop.SspiCli.ContextFlags inFlags)
		{
			this.WriteEvent(15, credential, context, (int)inFlags);
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x0006AB50 File Offset: 0x00068D50
		[Event(16, Keywords = (EventKeywords)1L, Level = EventLevel.Informational)]
		public void OperationReturnedSomething(string operation, global::Interop.SECURITY_STATUS errorCode)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(16, new object[] { operation, errorCode });
			}
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x0006AB75 File Offset: 0x00068D75
		[Event(13, Keywords = (EventKeywords)1L, Level = EventLevel.Informational)]
		public void SecurityContextInputBuffer(string context, int inputBufferSize, int outputBufferSize, global::Interop.SECURITY_STATUS errorCode)
		{
			if (base.IsEnabled())
			{
				this.WriteEvent(13, context, inputBufferSize, outputBufferSize, (int)errorCode);
			}
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x0006AB8C File Offset: 0x00068D8C
		[Event(14, Keywords = (EventKeywords)1L, Level = EventLevel.Informational)]
		public void SecurityContextInputBuffers(string context, int inputBuffersSize, int outputBufferSize, global::Interop.SECURITY_STATUS errorCode)
		{
			if (base.IsEnabled())
			{
				this.WriteEvent(14, context, inputBuffersSize, outputBufferSize, (int)errorCode);
			}
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x0006ABA3 File Offset: 0x00068DA3
		[Event(8, Keywords = (EventKeywords)1L, Level = EventLevel.Informational)]
		public void EnumerateSecurityPackages(string securityPackage)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(8, securityPackage ?? "");
			}
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x0006ABBE File Offset: 0x00068DBE
		[Event(9, Keywords = (EventKeywords)1L, Level = EventLevel.Informational)]
		public void SspiPackageNotFound(string packageName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(9, packageName ?? "");
			}
		}

		// Token: 0x04000EE6 RID: 3814
		public static readonly NetEventSource Log = new NetEventSource();

		// Token: 0x04000EE7 RID: 3815
		private const string MissingMember = "(?)";

		// Token: 0x04000EE8 RID: 3816
		private const string NullInstance = "(null)";

		// Token: 0x04000EE9 RID: 3817
		private const string StaticMethodObject = "(static)";

		// Token: 0x04000EEA RID: 3818
		private const string NoParameters = "";

		// Token: 0x04000EEB RID: 3819
		private const int MaxDumpSize = 1024;

		// Token: 0x04000EEC RID: 3820
		private const int EnterEventId = 1;

		// Token: 0x04000EED RID: 3821
		private const int ExitEventId = 2;

		// Token: 0x04000EEE RID: 3822
		private const int AssociateEventId = 3;

		// Token: 0x04000EEF RID: 3823
		private const int InfoEventId = 4;

		// Token: 0x04000EF0 RID: 3824
		private const int ErrorEventId = 5;

		// Token: 0x04000EF1 RID: 3825
		private const int CriticalFailureEventId = 6;

		// Token: 0x04000EF2 RID: 3826
		private const int DumpArrayEventId = 7;

		// Token: 0x04000EF3 RID: 3827
		private const int EnumerateSecurityPackagesId = 8;

		// Token: 0x04000EF4 RID: 3828
		private const int SspiPackageNotFoundId = 9;

		// Token: 0x04000EF5 RID: 3829
		private const int AcquireDefaultCredentialId = 10;

		// Token: 0x04000EF6 RID: 3830
		private const int AcquireCredentialsHandleId = 11;

		// Token: 0x04000EF7 RID: 3831
		private const int InitializeSecurityContextId = 12;

		// Token: 0x04000EF8 RID: 3832
		private const int SecurityContextInputBufferId = 13;

		// Token: 0x04000EF9 RID: 3833
		private const int SecurityContextInputBuffersId = 14;

		// Token: 0x04000EFA RID: 3834
		private const int AcceptSecuritContextId = 15;

		// Token: 0x04000EFB RID: 3835
		private const int OperationReturnedSomethingId = 16;

		// Token: 0x04000EFC RID: 3836
		private const int NextAvailableEventId = 17;

		// Token: 0x02000378 RID: 888
		public class Keywords
		{
			// Token: 0x04000EFD RID: 3837
			public const EventKeywords Default = (EventKeywords)1L;

			// Token: 0x04000EFE RID: 3838
			public const EventKeywords Debug = (EventKeywords)2L;

			// Token: 0x04000EFF RID: 3839
			public const EventKeywords EnterExit = (EventKeywords)4L;
		}
	}
}
