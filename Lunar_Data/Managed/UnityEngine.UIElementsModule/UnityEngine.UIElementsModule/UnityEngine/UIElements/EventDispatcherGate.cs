using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000028 RID: 40
	public struct EventDispatcherGate : IDisposable, IEquatable<EventDispatcherGate>
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x000053F4 File Offset: 0x000035F4
		public EventDispatcherGate(EventDispatcher d)
		{
			bool flag = d == null;
			if (flag)
			{
				throw new ArgumentNullException("d");
			}
			this.m_Dispatcher = d;
			this.m_Dispatcher.CloseGate();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005429 File Offset: 0x00003629
		public void Dispose()
		{
			this.m_Dispatcher.OpenGate();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005438 File Offset: 0x00003638
		public bool Equals(EventDispatcherGate other)
		{
			return object.Equals(this.m_Dispatcher, other.m_Dispatcher);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000545C File Offset: 0x0000365C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is EventDispatcherGate && this.Equals((EventDispatcherGate)obj);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005494 File Offset: 0x00003694
		public override int GetHashCode()
		{
			return (this.m_Dispatcher != null) ? this.m_Dispatcher.GetHashCode() : 0;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000054BC File Offset: 0x000036BC
		public static bool operator ==(EventDispatcherGate left, EventDispatcherGate right)
		{
			return left.Equals(right);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000054D8 File Offset: 0x000036D8
		public static bool operator !=(EventDispatcherGate left, EventDispatcherGate right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400006D RID: 109
		private readonly EventDispatcher m_Dispatcher;
	}
}
