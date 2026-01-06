using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x02000288 RID: 648
	public static class PhraseRecognitionSystem
	{
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001C1E RID: 7198
		public static extern bool isSupported
		{
			[ThreadSafe]
			[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001C1F RID: 7199
		public static extern SpeechSystemStatus Status
		{
			[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001C20 RID: 7200
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[NativeThrows]
		[MethodImpl(4096)]
		public static extern void Restart();

		// Token: 0x06001C21 RID: 7201
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		public static extern void Shutdown();

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06001C22 RID: 7202 RVA: 0x0002D128 File Offset: 0x0002B328
		// (remove) Token: 0x06001C23 RID: 7203 RVA: 0x0002D15C File Offset: 0x0002B35C
		[field: DebuggerBrowsable(0)]
		public static event PhraseRecognitionSystem.ErrorDelegate OnError;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06001C24 RID: 7204 RVA: 0x0002D190 File Offset: 0x0002B390
		// (remove) Token: 0x06001C25 RID: 7205 RVA: 0x0002D1C4 File Offset: 0x0002B3C4
		[field: DebuggerBrowsable(0)]
		public static event PhraseRecognitionSystem.StatusDelegate OnStatusChanged;

		// Token: 0x06001C26 RID: 7206 RVA: 0x0002D1F8 File Offset: 0x0002B3F8
		[RequiredByNativeCode]
		private static void PhraseRecognitionSystem_InvokeErrorEvent(SpeechError errorCode)
		{
			PhraseRecognitionSystem.ErrorDelegate onError = PhraseRecognitionSystem.OnError;
			bool flag = onError != null;
			if (flag)
			{
				onError(errorCode);
			}
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x0002D21C File Offset: 0x0002B41C
		[RequiredByNativeCode]
		private static void PhraseRecognitionSystem_InvokeStatusChangedEvent(SpeechSystemStatus status)
		{
			PhraseRecognitionSystem.StatusDelegate onStatusChanged = PhraseRecognitionSystem.OnStatusChanged;
			bool flag = onStatusChanged != null;
			if (flag)
			{
				onStatusChanged(status);
			}
		}

		// Token: 0x02000289 RID: 649
		// (Invoke) Token: 0x06001C29 RID: 7209
		public delegate void ErrorDelegate(SpeechError errorCode);

		// Token: 0x0200028A RID: 650
		// (Invoke) Token: 0x06001C2D RID: 7213
		public delegate void StatusDelegate(SpeechSystemStatus status);
	}
}
