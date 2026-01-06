using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides a basic designer loader interface that can be used to implement a custom designer loader.</summary>
	// Token: 0x02000798 RID: 1944
	public abstract class DesignerLoader
	{
		/// <summary>Gets a value indicating whether the loader is currently loading a document.</summary>
		/// <returns>true if the loader is currently loading a document; otherwise, false.</returns>
		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06003D8B RID: 15755 RVA: 0x00003062 File Offset: 0x00001262
		public virtual bool Loading
		{
			get
			{
				return false;
			}
		}

		/// <summary>Begins loading a designer.</summary>
		/// <param name="host">The loader host through which this loader loads components. </param>
		// Token: 0x06003D8C RID: 15756
		public abstract void BeginLoad(IDesignerLoaderHost host);

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.Serialization.DesignerLoader" />.</summary>
		// Token: 0x06003D8D RID: 15757
		public abstract void Dispose();

		/// <summary>Writes cached changes to the location that the designer was loaded from.</summary>
		// Token: 0x06003D8E RID: 15758 RVA: 0x00003917 File Offset: 0x00001B17
		public virtual void Flush()
		{
		}
	}
}
