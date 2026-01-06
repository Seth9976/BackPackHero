using System;
using System.Collections;

namespace System.Diagnostics
{
	/// <summary>Provides a strongly typed collection of <see cref="T:System.Diagnostics.ProcessModule" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000244 RID: 580
	public class ProcessModuleCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessModuleCollection" /> class, with no associated <see cref="T:System.Diagnostics.ProcessModule" /> instances.</summary>
		// Token: 0x060011CC RID: 4556 RVA: 0x0004DCAE File Offset: 0x0004BEAE
		protected ProcessModuleCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessModuleCollection" /> class, using the specified array of <see cref="T:System.Diagnostics.ProcessModule" /> instances.</summary>
		/// <param name="processModules">An array of <see cref="T:System.Diagnostics.ProcessModule" /> instances with which to initialize this <see cref="T:System.Diagnostics.ProcessModuleCollection" /> instance. </param>
		// Token: 0x060011CD RID: 4557 RVA: 0x0004DCB6 File Offset: 0x0004BEB6
		public ProcessModuleCollection(ProcessModule[] processModules)
		{
			base.InnerList.AddRange(processModules);
		}

		/// <summary>Gets an index for iterating over the set of process modules.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.ProcessModule" /> that indexes the modules in the collection </returns>
		/// <param name="index">The zero-based index value of the module in the collection. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000320 RID: 800
		public ProcessModule this[int index]
		{
			get
			{
				return (ProcessModule)base.InnerList[index];
			}
		}

		/// <summary>Provides the location of a specified module within the collection.</summary>
		/// <returns>The zero-based index that defines the location of the module within the <see cref="T:System.Diagnostics.ProcessModuleCollection" />.</returns>
		/// <param name="module">The <see cref="T:System.Diagnostics.ProcessModule" /> whose index is retrieved. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060011CF RID: 4559 RVA: 0x0004DCDD File Offset: 0x0004BEDD
		public int IndexOf(ProcessModule module)
		{
			return base.InnerList.IndexOf(module);
		}

		/// <summary>Determines whether the specified process module exists in the collection.</summary>
		/// <returns>true if the module exists in the collection; otherwise, false.</returns>
		/// <param name="module">A <see cref="T:System.Diagnostics.ProcessModule" /> instance that indicates the module to find in this collection. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060011D0 RID: 4560 RVA: 0x0004DCEB File Offset: 0x0004BEEB
		public bool Contains(ProcessModule module)
		{
			return base.InnerList.Contains(module);
		}

		/// <summary>Copies an array of <see cref="T:System.Diagnostics.ProcessModule" /> instances to the collection, at the specified index.</summary>
		/// <param name="array">An array of <see cref="T:System.Diagnostics.ProcessModule" /> instances to add to the collection. </param>
		/// <param name="index">The location at which to add the new instances. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060011D1 RID: 4561 RVA: 0x0004DCF9 File Offset: 0x0004BEF9
		public void CopyTo(ProcessModule[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}
	}
}
