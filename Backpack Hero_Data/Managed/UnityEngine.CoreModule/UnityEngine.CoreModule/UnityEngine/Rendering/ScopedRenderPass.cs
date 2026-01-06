using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200040A RID: 1034
	public struct ScopedRenderPass : IDisposable
	{
		// Token: 0x06002362 RID: 9058 RVA: 0x0003BB49 File Offset: 0x00039D49
		internal ScopedRenderPass(ScriptableRenderContext context)
		{
			this.m_Context = context;
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x0003BB54 File Offset: 0x00039D54
		public void Dispose()
		{
			try
			{
				this.m_Context.EndRenderPass();
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("The ScopedRenderPass instance is not valid. This can happen if it was constructed using the default constructor.", ex);
			}
		}

		// Token: 0x04000D2C RID: 3372
		private ScriptableRenderContext m_Context;
	}
}
