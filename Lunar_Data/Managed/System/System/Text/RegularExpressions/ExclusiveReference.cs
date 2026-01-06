using System;
using System.Threading;

namespace System.Text.RegularExpressions
{
	// Token: 0x020001F4 RID: 500
	internal sealed class ExclusiveReference
	{
		// Token: 0x06000D5B RID: 3419 RVA: 0x00036474 File Offset: 0x00034674
		public RegexRunner Get()
		{
			if (Interlocked.Exchange(ref this._locked, 1) != 0)
			{
				return null;
			}
			RegexRunner @ref = this._ref;
			if (@ref == null)
			{
				this._locked = 0;
				return null;
			}
			this._obj = @ref;
			return @ref;
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x000364B0 File Offset: 0x000346B0
		public void Release(RegexRunner obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (this._obj == obj)
			{
				this._obj = null;
				this._locked = 0;
				return;
			}
			if (this._obj == null && Interlocked.Exchange(ref this._locked, 1) == 0)
			{
				if (this._ref == null)
				{
					this._ref = obj;
				}
				this._locked = 0;
				return;
			}
		}

		// Token: 0x040007FF RID: 2047
		private RegexRunner _ref;

		// Token: 0x04000800 RID: 2048
		private RegexRunner _obj;

		// Token: 0x04000801 RID: 2049
		private volatile int _locked;
	}
}
