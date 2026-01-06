using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200002D RID: 45
	public class FocusChangeDirection : IDisposable
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00005DB7 File Offset: 0x00003FB7
		public static FocusChangeDirection unspecified { get; } = new FocusChangeDirection(-1);

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00005DBE File Offset: 0x00003FBE
		public static FocusChangeDirection none { get; } = new FocusChangeDirection(0);

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005DC5 File Offset: 0x00003FC5
		protected static FocusChangeDirection lastValue { get; } = FocusChangeDirection.none;

		// Token: 0x0600011F RID: 287 RVA: 0x00005DCC File Offset: 0x00003FCC
		protected FocusChangeDirection(int value)
		{
			this.m_Value = value;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005DE0 File Offset: 0x00003FE0
		public static implicit operator int(FocusChangeDirection fcd)
		{
			return (fcd != null) ? fcd.m_Value : 0;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005DFE File Offset: 0x00003FFE
		void IDisposable.Dispose()
		{
			this.Dispose();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000020E6 File Offset: 0x000002E6
		protected virtual void Dispose()
		{
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005E07 File Offset: 0x00004007
		internal virtual void ApplyTo(FocusController focusController, Focusable f)
		{
			focusController.SwitchFocus(f, this, false, DispatchMode.Default);
		}

		// Token: 0x04000084 RID: 132
		private readonly int m_Value;
	}
}
