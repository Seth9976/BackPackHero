using System;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x0200019D RID: 413
	public class fsSerializationCallbackProcessor : fsObjectProcessor
	{
		// Token: 0x06000AD4 RID: 2772 RVA: 0x0002D4D2 File Offset: 0x0002B6D2
		public override bool CanProcess(Type type)
		{
			return typeof(fsISerializationCallbacks).IsAssignableFrom(type);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0002D4E4 File Offset: 0x0002B6E4
		public override void OnBeforeSerialize(Type storageType, object instance)
		{
			if (instance == null)
			{
				return;
			}
			((fsISerializationCallbacks)instance).OnBeforeSerialize(storageType);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0002D4F6 File Offset: 0x0002B6F6
		public override void OnAfterSerialize(Type storageType, object instance, ref fsData data)
		{
			if (instance == null)
			{
				return;
			}
			((fsISerializationCallbacks)instance).OnAfterSerialize(storageType, ref data);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002D50C File Offset: 0x0002B70C
		public override void OnBeforeDeserializeAfterInstanceCreation(Type storageType, object instance, ref fsData data)
		{
			if (!(instance is fsISerializationCallbacks))
			{
				string text = "Please ensure the converter for ";
				string text2 = ((storageType != null) ? storageType.ToString() : null);
				string text3 = " actually returns an instance of it, not an instance of ";
				Type type = instance.GetType();
				throw new InvalidCastException(text + text2 + text3 + ((type != null) ? type.ToString() : null));
			}
			((fsISerializationCallbacks)instance).OnBeforeDeserialize(storageType, ref data);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0002D562 File Offset: 0x0002B762
		public override void OnAfterDeserialize(Type storageType, object instance)
		{
			if (instance == null)
			{
				return;
			}
			((fsISerializationCallbacks)instance).OnAfterDeserialize(storageType);
		}
	}
}
