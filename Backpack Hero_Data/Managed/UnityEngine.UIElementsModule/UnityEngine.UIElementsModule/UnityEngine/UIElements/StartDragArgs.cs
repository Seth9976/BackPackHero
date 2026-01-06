using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001AC RID: 428
	internal class StartDragArgs
	{
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x000387E1 File Offset: 0x000369E1
		public string title { get; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x000387E9 File Offset: 0x000369E9
		public object userData { get; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x000387F1 File Offset: 0x000369F1
		internal Hashtable genericData
		{
			get
			{
				return this.m_GenericData;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x000387F9 File Offset: 0x000369F9
		// (set) Token: 0x06000DD6 RID: 3542 RVA: 0x00038801 File Offset: 0x00036A01
		internal IEnumerable<Object> unityObjectReferences { get; private set; } = null;

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0003880A File Offset: 0x00036A0A
		internal StartDragArgs()
		{
			this.title = string.Empty;
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x00038831 File Offset: 0x00036A31
		public StartDragArgs(string title, object userData)
		{
			this.title = title;
			this.userData = userData;
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0003885B File Offset: 0x00036A5B
		public void SetGenericData(string key, object data)
		{
			this.m_GenericData[key] = data;
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0003886C File Offset: 0x00036A6C
		public void SetUnityObjectReferences(IEnumerable<Object> references)
		{
			this.unityObjectReferences = references;
		}

		// Token: 0x0400065D RID: 1629
		private readonly Hashtable m_GenericData = new Hashtable();
	}
}
