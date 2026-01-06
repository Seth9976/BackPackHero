using System;

namespace System.Diagnostics
{
	// Token: 0x02000219 RID: 537
	internal class FilterElement : TypedElement
	{
		// Token: 0x06000F9B RID: 3995 RVA: 0x000456D8 File Offset: 0x000438D8
		public FilterElement()
			: base(typeof(TraceFilter))
		{
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x000456EA File Offset: 0x000438EA
		public TraceFilter GetRuntimeObject()
		{
			TraceFilter traceFilter = (TraceFilter)base.BaseGetRuntimeObject();
			traceFilter.initializeData = base.InitData;
			return traceFilter;
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x00045703 File Offset: 0x00043903
		internal TraceFilter RefreshRuntimeObject(TraceFilter filter)
		{
			if (Type.GetType(this.TypeName) != filter.GetType() || base.InitData != filter.initializeData)
			{
				this._runtimeObject = null;
				return this.GetRuntimeObject();
			}
			return filter;
		}
	}
}
