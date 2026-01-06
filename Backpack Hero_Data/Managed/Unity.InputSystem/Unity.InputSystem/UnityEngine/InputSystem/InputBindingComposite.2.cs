using System;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000027 RID: 39
	public abstract class InputBindingComposite<TValue> : InputBindingComposite where TValue : struct
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000C595 File Offset: 0x0000A795
		public override Type valueType
		{
			get
			{
				return typeof(TValue);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000C5A1 File Offset: 0x0000A7A1
		public override int valueSizeInBytes
		{
			get
			{
				return UnsafeUtility.SizeOf<TValue>();
			}
		}

		// Token: 0x060002D5 RID: 725
		public abstract TValue ReadValue(ref InputBindingCompositeContext context);

		// Token: 0x060002D6 RID: 726 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
		public unsafe override void ReadValue(ref InputBindingCompositeContext context, void* buffer, int bufferSize)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = UnsafeUtility.SizeOf<TValue>();
			if (bufferSize < num)
			{
				throw new ArgumentException(string.Format("Expected buffer of at least {0} bytes but got buffer of only {1} bytes instead", UnsafeUtility.SizeOf<TValue>(), bufferSize), "bufferSize");
			}
			TValue tvalue = this.ReadValue(ref context);
			void* ptr = UnsafeUtility.AddressOf<TValue>(ref tvalue);
			UnsafeUtility.MemCpy(buffer, ptr, (long)num);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000C610 File Offset: 0x0000A810
		public unsafe override object ReadValueAsObject(ref InputBindingCompositeContext context)
		{
			TValue tvalue = default(TValue);
			void* ptr = UnsafeUtility.AddressOf<TValue>(ref tvalue);
			this.ReadValue(ref context, ptr, UnsafeUtility.SizeOf<TValue>());
			return tvalue;
		}
	}
}
