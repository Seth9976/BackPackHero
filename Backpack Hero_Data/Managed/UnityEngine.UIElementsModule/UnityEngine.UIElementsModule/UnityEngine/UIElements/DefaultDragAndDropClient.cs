using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A4 RID: 420
	internal class DefaultDragAndDropClient : IDragAndDrop, IDragAndDropData
	{
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x000382CB File Offset: 0x000364CB
		public object userData
		{
			get
			{
				StartDragArgs startDragArgs = this.m_StartDragArgs;
				return (startDragArgs != null) ? startDragArgs.userData : null;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x000382DF File Offset: 0x000364DF
		public IEnumerable<Object> unityObjectReferences
		{
			get
			{
				StartDragArgs startDragArgs = this.m_StartDragArgs;
				return (startDragArgs != null) ? startDragArgs.unityObjectReferences : null;
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x000382F3 File Offset: 0x000364F3
		public void StartDrag(StartDragArgs args)
		{
			this.m_StartDragArgs = args;
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x000382FD File Offset: 0x000364FD
		public void AcceptDrag()
		{
			this.m_StartDragArgs = null;
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x000020E6 File Offset: 0x000002E6
		public void SetVisualMode(DragVisualMode visualMode)
		{
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x00038308 File Offset: 0x00036508
		public IDragAndDropData data
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0003831C File Offset: 0x0003651C
		public object GetGenericData(string key)
		{
			bool flag = this.m_StartDragArgs == null;
			object obj;
			if (flag)
			{
				obj = null;
			}
			else
			{
				obj = (this.m_StartDragArgs.genericData.ContainsKey(key) ? this.m_StartDragArgs.genericData[key] : null);
			}
			return obj;
		}

		// Token: 0x0400064B RID: 1611
		private StartDragArgs m_StartDragArgs;
	}
}
