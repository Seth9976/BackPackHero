using System;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x020002B5 RID: 693
	[Serializable]
	internal class ArgumentCache : ISerializationCallbackReceiver
	{
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001D0F RID: 7439 RVA: 0x0002E7DC File Offset: 0x0002C9DC
		// (set) Token: 0x06001D10 RID: 7440 RVA: 0x0002E7F4 File Offset: 0x0002C9F4
		public Object unityObjectArgument
		{
			get
			{
				return this.m_ObjectArgument;
			}
			set
			{
				this.m_ObjectArgument = value;
				this.m_ObjectArgumentAssemblyTypeName = ((value != null) ? value.GetType().AssemblyQualifiedName : string.Empty);
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001D11 RID: 7441 RVA: 0x0002E820 File Offset: 0x0002CA20
		public string unityObjectArgumentAssemblyTypeName
		{
			get
			{
				return this.m_ObjectArgumentAssemblyTypeName;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x0002E838 File Offset: 0x0002CA38
		// (set) Token: 0x06001D13 RID: 7443 RVA: 0x0002E850 File Offset: 0x0002CA50
		public int intArgument
		{
			get
			{
				return this.m_IntArgument;
			}
			set
			{
				this.m_IntArgument = value;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001D14 RID: 7444 RVA: 0x0002E85C File Offset: 0x0002CA5C
		// (set) Token: 0x06001D15 RID: 7445 RVA: 0x0002E874 File Offset: 0x0002CA74
		public float floatArgument
		{
			get
			{
				return this.m_FloatArgument;
			}
			set
			{
				this.m_FloatArgument = value;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x0002E880 File Offset: 0x0002CA80
		// (set) Token: 0x06001D17 RID: 7447 RVA: 0x0002E898 File Offset: 0x0002CA98
		public string stringArgument
		{
			get
			{
				return this.m_StringArgument;
			}
			set
			{
				this.m_StringArgument = value;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x0002E8A4 File Offset: 0x0002CAA4
		// (set) Token: 0x06001D19 RID: 7449 RVA: 0x0002E8BC File Offset: 0x0002CABC
		public bool boolArgument
		{
			get
			{
				return this.m_BoolArgument;
			}
			set
			{
				this.m_BoolArgument = value;
			}
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x0002E8C6 File Offset: 0x0002CAC6
		public void OnBeforeSerialize()
		{
			this.m_ObjectArgumentAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_ObjectArgumentAssemblyTypeName);
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x0002E8C6 File Offset: 0x0002CAC6
		public void OnAfterDeserialize()
		{
			this.m_ObjectArgumentAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_ObjectArgumentAssemblyTypeName);
		}

		// Token: 0x0400098B RID: 2443
		[SerializeField]
		[FormerlySerializedAs("objectArgument")]
		private Object m_ObjectArgument;

		// Token: 0x0400098C RID: 2444
		[FormerlySerializedAs("objectArgumentAssemblyTypeName")]
		[SerializeField]
		private string m_ObjectArgumentAssemblyTypeName;

		// Token: 0x0400098D RID: 2445
		[SerializeField]
		[FormerlySerializedAs("intArgument")]
		private int m_IntArgument;

		// Token: 0x0400098E RID: 2446
		[FormerlySerializedAs("floatArgument")]
		[SerializeField]
		private float m_FloatArgument;

		// Token: 0x0400098F RID: 2447
		[FormerlySerializedAs("stringArgument")]
		[SerializeField]
		private string m_StringArgument;

		// Token: 0x04000990 RID: 2448
		[SerializeField]
		private bool m_BoolArgument;
	}
}
