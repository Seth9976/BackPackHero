using System;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000034 RID: 52
	public abstract class InputProcessor
	{
		// Token: 0x0600044C RID: 1100
		public abstract object ProcessAsObject(object value, InputControl control);

		// Token: 0x0600044D RID: 1101
		public unsafe abstract void Process(void* buffer, int bufferSize, InputControl control);

		// Token: 0x0600044E RID: 1102 RVA: 0x00012B74 File Offset: 0x00010D74
		internal static Type GetValueTypeFromType(Type processorType)
		{
			if (processorType == null)
			{
				throw new ArgumentNullException("processorType");
			}
			return TypeHelpers.GetGenericTypeArgumentFromHierarchy(processorType, typeof(InputProcessor<>), 0);
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00012B9B File Offset: 0x00010D9B
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
			// Token: 0x04000882 RID: 2178
			CacheResult,
			// Token: 0x04000883 RID: 2179
			EvaluateOnEveryRead
		}
	}
}
