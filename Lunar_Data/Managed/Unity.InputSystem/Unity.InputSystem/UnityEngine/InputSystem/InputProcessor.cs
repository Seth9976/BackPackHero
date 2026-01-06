using System;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000034 RID: 52
	public abstract class InputProcessor
	{
		// Token: 0x0600044A RID: 1098
		public abstract object ProcessAsObject(object value, InputControl control);

		// Token: 0x0600044B RID: 1099
		public unsafe abstract void Process(void* buffer, int bufferSize, InputControl control);

		// Token: 0x0600044C RID: 1100 RVA: 0x00012B38 File Offset: 0x00010D38
		internal static Type GetValueTypeFromType(Type processorType)
		{
			if (processorType == null)
			{
				throw new ArgumentNullException("processorType");
			}
			return TypeHelpers.GetGenericTypeArgumentFromHierarchy(processorType, typeof(InputProcessor<>), 0);
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00012B5F File Offset: 0x00010D5F
		public virtual InputProcessor.CachingPolicy cachingPolicy
		{
			get
			{
				return InputProcessor.CachingPolicy.CacheResult;
			}
		}

		// Token: 0x04000140 RID: 320
		internal static TypeTable s_Processors;

		// Token: 0x02000190 RID: 400
		public enum CachingPolicy
		{
			// Token: 0x04000881 RID: 2177
			CacheResult,
			// Token: 0x04000882 RID: 2178
			EvaluateOnEveryRead
		}
	}
}
