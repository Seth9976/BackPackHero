using System;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000035 RID: 53
	public abstract class InputProcessor<TValue> : InputProcessor where TValue : struct
	{
		// Token: 0x06000451 RID: 1105
		public abstract TValue Process(TValue value, InputControl control);

		// Token: 0x06000452 RID: 1106 RVA: 0x00012BA8 File Offset: 0x00010DA8
		public override object ProcessAsObject(object value, InputControl control)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is TValue))
			{
				throw new ArgumentException(string.Format("Expecting value of type '{0}' but got value '{1}' of type '{2}'", typeof(TValue).Name, value, value.GetType().Name), "value");
			}
			TValue tvalue = (TValue)((object)value);
			return this.Process(tvalue, control);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00012C10 File Offset: 0x00010E10
		public unsafe override void Process(void* buffer, int bufferSize, InputControl control)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = UnsafeUtility.SizeOf<TValue>();
			if (bufferSize < num)
			{
				throw new ArgumentException(string.Format("Expected buffer of at least {0} bytes but got buffer with just {1} bytes", num, bufferSize), "bufferSize");
			}
			TValue tvalue = default(TValue);
			void* ptr = UnsafeUtility.AddressOf<TValue>(ref tvalue);
			UnsafeUtility.MemCpy(ptr, buffer, (long)num);
			tvalue = this.Process(tvalue, control);
			ptr = UnsafeUtility.AddressOf<TValue>(ref tvalue);
			UnsafeUtility.MemCpy(buffer, ptr, (long)num);
		}
	}
}
