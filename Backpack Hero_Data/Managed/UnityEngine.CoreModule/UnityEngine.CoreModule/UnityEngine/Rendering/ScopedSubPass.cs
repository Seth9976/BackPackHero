using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200040B RID: 1035
	public struct ScopedSubPass : IDisposable
	{
		// Token: 0x06002364 RID: 9060 RVA: 0x0003BB90 File Offset: 0x00039D90
		internal ScopedSubPass(ScriptableRenderContext context)
		{
			this.m_Context = context;
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x0003BB9C File Offset: 0x00039D9C
		public void Dispose()
		{
			try
			{
				this.m_Context.EndSubPass();
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("The ScopedSubPass instance is not valid. This can happen if it was constructed using the default constructor.", ex);
			}
		}

		// Token: 0x04000D2D RID: 3373
		private ScriptableRenderContext m_Context;
	}
}
