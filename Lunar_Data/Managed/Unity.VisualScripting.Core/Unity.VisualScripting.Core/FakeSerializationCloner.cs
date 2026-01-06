using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000007 RID: 7
	public sealed class FakeSerializationCloner : ReflectedCloner
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000234D File Offset: 0x0000054D
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002355 File Offset: 0x00000555
		public fsConfig config { get; set; } = new fsConfig();

		// Token: 0x0600001C RID: 28 RVA: 0x0000235E File Offset: 0x0000055E
		public override void BeforeClone(Type type, object original)
		{
			ISerializationCallbackReceiver serializationCallbackReceiver = original as ISerializationCallbackReceiver;
			if (serializationCallbackReceiver == null)
			{
				return;
			}
			serializationCallbackReceiver.OnBeforeSerialize();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002370 File Offset: 0x00000570
		public override void AfterClone(Type type, object clone)
		{
			ISerializationCallbackReceiver serializationCallbackReceiver = clone as ISerializationCallbackReceiver;
			if (serializationCallbackReceiver == null)
			{
				return;
			}
			serializationCallbackReceiver.OnAfterDeserialize();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002382 File Offset: 0x00000582
		protected override IEnumerable<MemberInfo> GetMembers(Type type)
		{
			return fsMetaType.Get(this.config, type).Properties.Select((fsMetaProperty p) => p._memberInfo);
		}
	}
}
