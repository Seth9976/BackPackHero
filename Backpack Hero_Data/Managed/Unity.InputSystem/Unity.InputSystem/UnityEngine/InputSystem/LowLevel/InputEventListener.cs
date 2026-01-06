using System;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000E1 RID: 225
	public struct InputEventListener : IObservable<InputEventPtr>
	{
		// Token: 0x06000D58 RID: 3416 RVA: 0x00042F48 File Offset: 0x00041148
		public static InputEventListener operator +(InputEventListener _, Action<InputEventPtr, InputDevice> callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			InputManager s_Manager = InputSystem.s_Manager;
			lock (s_Manager)
			{
				InputSystem.s_Manager.onEvent += callback;
			}
			return default(InputEventListener);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00042FA4 File Offset: 0x000411A4
		public static InputEventListener operator -(InputEventListener _, Action<InputEventPtr, InputDevice> callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			InputManager s_Manager = InputSystem.s_Manager;
			lock (s_Manager)
			{
				InputSystem.s_Manager.onEvent -= callback;
			}
			return default(InputEventListener);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00043000 File Offset: 0x00041200
		public IDisposable Subscribe(IObserver<InputEventPtr> observer)
		{
			if (InputEventListener.s_ObserverState == null)
			{
				InputEventListener.s_ObserverState = new InputEventListener.ObserverState();
			}
			if (InputEventListener.s_ObserverState.observers.length == 0)
			{
				InputSystem.s_Manager.onEvent += InputEventListener.s_ObserverState.onEventDelegate;
			}
			InputEventListener.s_ObserverState.observers.AppendWithCapacity(observer, 10);
			return new InputEventListener.DisposableObserver
			{
				observer = observer
			};
		}

		// Token: 0x0400056D RID: 1389
		internal static InputEventListener.ObserverState s_ObserverState;

		// Token: 0x0200020A RID: 522
		internal class ObserverState
		{
			// Token: 0x0600147C RID: 5244 RVA: 0x0005F47A File Offset: 0x0005D67A
			public ObserverState()
			{
				this.onEventDelegate = delegate(InputEventPtr eventPtr, InputDevice device)
				{
					for (int i = this.observers.length - 1; i >= 0; i--)
					{
						this.observers[i].OnNext(eventPtr);
					}
				};
			}

			// Token: 0x04000B29 RID: 2857
			public InlinedArray<IObserver<InputEventPtr>> observers;

			// Token: 0x04000B2A RID: 2858
			public Action<InputEventPtr, InputDevice> onEventDelegate;
		}

		// Token: 0x0200020B RID: 523
		private class DisposableObserver : IDisposable
		{
			// Token: 0x0600147E RID: 5246 RVA: 0x0005F4CC File Offset: 0x0005D6CC
			public void Dispose()
			{
				int num = InputEventListener.s_ObserverState.observers.IndexOfReference(this.observer);
				if (num >= 0)
				{
					InputEventListener.s_ObserverState.observers.RemoveAtWithCapacity(num);
				}
				if (InputEventListener.s_ObserverState.observers.length == 0)
				{
					InputSystem.s_Manager.onEvent -= InputEventListener.s_ObserverState.onEventDelegate;
				}
			}

			// Token: 0x04000B2B RID: 2859
			public IObserver<InputEventPtr> observer;
		}
	}
}
