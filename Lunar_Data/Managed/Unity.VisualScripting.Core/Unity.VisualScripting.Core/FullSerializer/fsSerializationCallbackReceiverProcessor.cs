using System;
using UnityEngine;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x0200019E RID: 414
	public class fsSerializationCallbackReceiverProcessor : fsObjectProcessor
	{
		// Token: 0x06000ADA RID: 2778 RVA: 0x0002D57C File Offset: 0x0002B77C
		public override bool CanProcess(Type type)
		{
			return typeof(ISerializationCallbackReceiver).IsAssignableFrom(type);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0002D58E File Offset: 0x0002B78E
		public override void OnBeforeSerialize(Type storageType, object instance)
		{
			if (instance == null || instance is Object)
			{
				return;
			}
			((ISerializationCallbackReceiver)instance).OnBeforeSerialize();
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0002D5A7 File Offset: 0x0002B7A7
		public override void OnAfterDeserialize(Type storageType, object instance)
		{
			if (instance == null || instance is Object)
			{
				return;
			}
			((ISerializationCallbackReceiver)instance).OnAfterDeserialize();
		}
	}
}
