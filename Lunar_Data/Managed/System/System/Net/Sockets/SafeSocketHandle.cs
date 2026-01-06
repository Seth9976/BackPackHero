using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Sockets
{
	// Token: 0x020005CD RID: 1485
	internal sealed class SafeSocketHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002FC7 RID: 12231 RVA: 0x000A9A47 File Offset: 0x000A7C47
		public SafeSocketHandle(IntPtr preexistingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(preexistingHandle);
			if (SafeSocketHandle.THROW_ON_ABORT_RETRIES)
			{
				this.threads_stacktraces = new Dictionary<Thread, StackTrace>();
			}
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x00013AC8 File Offset: 0x00011CC8
		internal SafeSocketHandle()
			: base(true)
		{
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x000A9A6C File Offset: 0x000A7C6C
		protected override bool ReleaseHandle()
		{
			int num = 0;
			Socket.Blocking_icall(this.handle, false, out num);
			if (this.blocking_threads != null)
			{
				List<Thread> list = this.blocking_threads;
				lock (list)
				{
					int num2 = 0;
					while (this.blocking_threads.Count > 0)
					{
						if (num2++ >= 10)
						{
							if (SafeSocketHandle.THROW_ON_ABORT_RETRIES)
							{
								StringBuilder stringBuilder = new StringBuilder();
								stringBuilder.AppendLine("Could not abort registered blocking threads before closing socket.");
								foreach (Thread thread in this.blocking_threads)
								{
									stringBuilder.AppendLine("Thread StackTrace:");
									stringBuilder.AppendLine(this.threads_stacktraces[thread].ToString());
								}
								stringBuilder.AppendLine();
								throw new Exception(stringBuilder.ToString());
							}
							break;
						}
						else
						{
							if (this.blocking_threads.Count == 1 && this.blocking_threads[0] == Thread.CurrentThread)
							{
								break;
							}
							foreach (Thread thread2 in this.blocking_threads)
							{
								Socket.cancel_blocking_socket_operation(thread2);
							}
							this.in_cleanup = true;
							Monitor.Wait(this.blocking_threads, 100);
						}
					}
				}
			}
			Socket.Close_icall(this.handle, out num);
			return num == 0;
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x000A9C2C File Offset: 0x000A7E2C
		public void RegisterForBlockingSyscall()
		{
			if (this.blocking_threads == null)
			{
				Interlocked.CompareExchange<List<Thread>>(ref this.blocking_threads, new List<Thread>(), null);
			}
			bool flag = false;
			try
			{
				base.DangerousAddRef(ref flag);
			}
			finally
			{
				List<Thread> list = this.blocking_threads;
				lock (list)
				{
					this.blocking_threads.Add(Thread.CurrentThread);
					if (SafeSocketHandle.THROW_ON_ABORT_RETRIES)
					{
						this.threads_stacktraces.Add(Thread.CurrentThread, new StackTrace(true));
					}
				}
				if (flag)
				{
					base.DangerousRelease();
				}
				if (base.IsClosed)
				{
					throw new SocketException(10004);
				}
			}
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x000A9CE4 File Offset: 0x000A7EE4
		public void UnRegisterForBlockingSyscall()
		{
			List<Thread> list = this.blocking_threads;
			lock (list)
			{
				Thread currentThread = Thread.CurrentThread;
				this.blocking_threads.Remove(currentThread);
				if (SafeSocketHandle.THROW_ON_ABORT_RETRIES && this.blocking_threads.IndexOf(currentThread) == -1)
				{
					this.threads_stacktraces.Remove(currentThread);
				}
				if (this.in_cleanup && this.blocking_threads.Count == 0)
				{
					Monitor.Pulse(this.blocking_threads);
				}
			}
		}

		// Token: 0x04001C97 RID: 7319
		private List<Thread> blocking_threads;

		// Token: 0x04001C98 RID: 7320
		private Dictionary<Thread, StackTrace> threads_stacktraces;

		// Token: 0x04001C99 RID: 7321
		private bool in_cleanup;

		// Token: 0x04001C9A RID: 7322
		private const int SOCKET_CLOSED = 10004;

		// Token: 0x04001C9B RID: 7323
		private const int ABORT_RETRIES = 10;

		// Token: 0x04001C9C RID: 7324
		private static bool THROW_ON_ABORT_RETRIES = Environment.GetEnvironmentVariable("MONO_TESTS_IN_PROGRESS") == "yes";
	}
}
