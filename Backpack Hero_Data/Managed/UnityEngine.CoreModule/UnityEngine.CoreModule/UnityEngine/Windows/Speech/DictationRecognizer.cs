using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x0200028D RID: 653
	public sealed class DictationRecognizer : IDisposable
	{
		// Token: 0x06001C45 RID: 7237
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern IntPtr Create(object self, ConfidenceLevel minimumConfidence, DictationTopicConstraint topicConstraint);

		// Token: 0x06001C46 RID: 7238
		[NativeThrows]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern void Start(IntPtr self);

		// Token: 0x06001C47 RID: 7239
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern void Stop(IntPtr self);

		// Token: 0x06001C48 RID: 7240
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern void Destroy(IntPtr self);

		// Token: 0x06001C49 RID: 7241
		[ThreadSafe]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern void DestroyThreaded(IntPtr self);

		// Token: 0x06001C4A RID: 7242
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern SpeechSystemStatus GetStatus(IntPtr self);

		// Token: 0x06001C4B RID: 7243
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern float GetAutoSilenceTimeoutSeconds(IntPtr self);

		// Token: 0x06001C4C RID: 7244
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern void SetAutoSilenceTimeoutSeconds(IntPtr self, float value);

		// Token: 0x06001C4D RID: 7245
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern float GetInitialSilenceTimeoutSeconds(IntPtr self);

		// Token: 0x06001C4E RID: 7246
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		[MethodImpl(4096)]
		private static extern void SetInitialSilenceTimeoutSeconds(IntPtr self, float value);

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06001C4F RID: 7247 RVA: 0x0002D4E8 File Offset: 0x0002B6E8
		// (remove) Token: 0x06001C50 RID: 7248 RVA: 0x0002D520 File Offset: 0x0002B720
		[field: DebuggerBrowsable(0)]
		public event DictationRecognizer.DictationHypothesisDelegate DictationHypothesis;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06001C51 RID: 7249 RVA: 0x0002D558 File Offset: 0x0002B758
		// (remove) Token: 0x06001C52 RID: 7250 RVA: 0x0002D590 File Offset: 0x0002B790
		[field: DebuggerBrowsable(0)]
		public event DictationRecognizer.DictationResultDelegate DictationResult;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06001C53 RID: 7251 RVA: 0x0002D5C8 File Offset: 0x0002B7C8
		// (remove) Token: 0x06001C54 RID: 7252 RVA: 0x0002D600 File Offset: 0x0002B800
		[field: DebuggerBrowsable(0)]
		public event DictationRecognizer.DictationCompletedDelegate DictationComplete;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06001C55 RID: 7253 RVA: 0x0002D638 File Offset: 0x0002B838
		// (remove) Token: 0x06001C56 RID: 7254 RVA: 0x0002D670 File Offset: 0x0002B870
		[field: DebuggerBrowsable(0)]
		public event DictationRecognizer.DictationErrorHandler DictationError;

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001C57 RID: 7255 RVA: 0x0002D6A8 File Offset: 0x0002B8A8
		public SpeechSystemStatus Status
		{
			get
			{
				return (this.m_Recognizer != IntPtr.Zero) ? DictationRecognizer.GetStatus(this.m_Recognizer) : SpeechSystemStatus.Stopped;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001C58 RID: 7256 RVA: 0x0002D6DC File Offset: 0x0002B8DC
		// (set) Token: 0x06001C59 RID: 7257 RVA: 0x0002D718 File Offset: 0x0002B918
		public float AutoSilenceTimeoutSeconds
		{
			get
			{
				bool flag = this.m_Recognizer == IntPtr.Zero;
				float num;
				if (flag)
				{
					num = 0f;
				}
				else
				{
					num = DictationRecognizer.GetAutoSilenceTimeoutSeconds(this.m_Recognizer);
				}
				return num;
			}
			set
			{
				bool flag = this.m_Recognizer == IntPtr.Zero;
				if (!flag)
				{
					DictationRecognizer.SetAutoSilenceTimeoutSeconds(this.m_Recognizer, value);
				}
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x0002D74C File Offset: 0x0002B94C
		// (set) Token: 0x06001C5B RID: 7259 RVA: 0x0002D788 File Offset: 0x0002B988
		public float InitialSilenceTimeoutSeconds
		{
			get
			{
				bool flag = this.m_Recognizer == IntPtr.Zero;
				float num;
				if (flag)
				{
					num = 0f;
				}
				else
				{
					num = DictationRecognizer.GetInitialSilenceTimeoutSeconds(this.m_Recognizer);
				}
				return num;
			}
			set
			{
				bool flag = this.m_Recognizer == IntPtr.Zero;
				if (!flag)
				{
					DictationRecognizer.SetInitialSilenceTimeoutSeconds(this.m_Recognizer, value);
				}
			}
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x0002D7B9 File Offset: 0x0002B9B9
		public DictationRecognizer()
			: this(ConfidenceLevel.Medium, DictationTopicConstraint.Dictation)
		{
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x0002D7C5 File Offset: 0x0002B9C5
		public DictationRecognizer(ConfidenceLevel confidenceLevel)
			: this(confidenceLevel, DictationTopicConstraint.Dictation)
		{
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x0002D7D1 File Offset: 0x0002B9D1
		public DictationRecognizer(DictationTopicConstraint topic)
			: this(ConfidenceLevel.Medium, topic)
		{
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x0002D7DD File Offset: 0x0002B9DD
		public DictationRecognizer(ConfidenceLevel minimumConfidence, DictationTopicConstraint topic)
		{
			this.m_Recognizer = DictationRecognizer.Create(this, minimumConfidence, topic);
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x0002D7F8 File Offset: 0x0002B9F8
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Recognizer != IntPtr.Zero;
				if (flag)
				{
					DictationRecognizer.DestroyThreaded(this.m_Recognizer);
					this.m_Recognizer = IntPtr.Zero;
					GC.SuppressFinalize(this);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x0002D858 File Offset: 0x0002BA58
		public void Start()
		{
			bool flag = this.m_Recognizer == IntPtr.Zero;
			if (!flag)
			{
				DictationRecognizer.Start(this.m_Recognizer);
			}
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x0002D888 File Offset: 0x0002BA88
		public void Stop()
		{
			bool flag = this.m_Recognizer == IntPtr.Zero;
			if (!flag)
			{
				DictationRecognizer.Stop(this.m_Recognizer);
			}
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x0002D8B8 File Offset: 0x0002BAB8
		public void Dispose()
		{
			bool flag = this.m_Recognizer != IntPtr.Zero;
			if (flag)
			{
				DictationRecognizer.Destroy(this.m_Recognizer);
				this.m_Recognizer = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x0002D8FC File Offset: 0x0002BAFC
		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeHypothesisGeneratedEvent(string keyword)
		{
			DictationRecognizer.DictationHypothesisDelegate dictationHypothesis = this.DictationHypothesis;
			bool flag = dictationHypothesis != null;
			if (flag)
			{
				dictationHypothesis(keyword);
			}
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x0002D924 File Offset: 0x0002BB24
		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeResultGeneratedEvent(string keyword, ConfidenceLevel minimumConfidence)
		{
			DictationRecognizer.DictationResultDelegate dictationResult = this.DictationResult;
			bool flag = dictationResult != null;
			if (flag)
			{
				dictationResult(keyword, minimumConfidence);
			}
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x0002D94C File Offset: 0x0002BB4C
		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeCompletedEvent(DictationCompletionCause cause)
		{
			DictationRecognizer.DictationCompletedDelegate dictationComplete = this.DictationComplete;
			bool flag = dictationComplete != null;
			if (flag)
			{
				dictationComplete(cause);
			}
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x0002D974 File Offset: 0x0002BB74
		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeErrorEvent(string error, int hresult)
		{
			DictationRecognizer.DictationErrorHandler dictationError = this.DictationError;
			bool flag = dictationError != null;
			if (flag)
			{
				dictationError(error, hresult);
			}
		}

		// Token: 0x04000926 RID: 2342
		private IntPtr m_Recognizer;

		// Token: 0x0200028E RID: 654
		// (Invoke) Token: 0x06001C69 RID: 7273
		public delegate void DictationHypothesisDelegate(string text);

		// Token: 0x0200028F RID: 655
		// (Invoke) Token: 0x06001C6D RID: 7277
		public delegate void DictationResultDelegate(string text, ConfidenceLevel confidence);

		// Token: 0x02000290 RID: 656
		// (Invoke) Token: 0x06001C71 RID: 7281
		public delegate void DictationCompletedDelegate(DictationCompletionCause cause);

		// Token: 0x02000291 RID: 657
		// (Invoke) Token: 0x06001C75 RID: 7285
		public delegate void DictationErrorHandler(string error, int hresult);
	}
}
