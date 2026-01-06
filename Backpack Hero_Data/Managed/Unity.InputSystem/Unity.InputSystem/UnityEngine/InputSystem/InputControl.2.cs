using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200002F RID: 47
	public abstract class InputControl<TValue> : InputControl where TValue : struct
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000FFA0 File Offset: 0x0000E1A0
		public override Type valueType
		{
			get
			{
				return typeof(TValue);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000FFAC File Offset: 0x0000E1AC
		public override int valueSizeInBytes
		{
			get
			{
				return UnsafeUtility.SizeOf<TValue>();
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000FFB3 File Offset: 0x0000E1B3
		public unsafe readonly ref TValue value
		{
			get
			{
				if (!InputSettings.readValueCachingFeatureEnabled || this.m_CachedValueIsStale || this.evaluateProcessorsEveryRead)
				{
					this.m_CachedValue = this.ProcessValue(*this.unprocessedValue);
					this.m_CachedValueIsStale = false;
				}
				return ref this.m_CachedValue;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000FFF0 File Offset: 0x0000E1F0
		internal readonly ref TValue unprocessedValue
		{
			get
			{
				if (!InputSettings.readValueCachingFeatureEnabled || this.m_UnprocessedCachedValueIsStale)
				{
					this.m_UnprocessedCachedValue = this.ReadUnprocessedValueFromState(base.currentStatePtr);
					this.m_UnprocessedCachedValueIsStale = false;
				}
				return ref this.m_UnprocessedCachedValue;
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00010020 File Offset: 0x0000E220
		public unsafe TValue ReadValue()
		{
			return *this.value;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0001002D File Offset: 0x0000E22D
		public TValue ReadValueFromPreviousFrame()
		{
			return this.ReadValueFromState(base.previousFrameStatePtr);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001003B File Offset: 0x0000E23B
		public TValue ReadDefaultValue()
		{
			return this.ReadValueFromState(base.defaultStatePtr);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00010049 File Offset: 0x0000E249
		public unsafe TValue ReadValueFromState(void* statePtr)
		{
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			return this.ProcessValue(this.ReadUnprocessedValueFromState(statePtr));
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00010068 File Offset: 0x0000E268
		public unsafe TValue ReadValueFromStateWithCaching(void* statePtr)
		{
			if (statePtr != base.currentStatePtr)
			{
				return this.ReadValueFromState(statePtr);
			}
			return *this.value;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00010086 File Offset: 0x0000E286
		public unsafe TValue ReadUnprocessedValueFromStateWithCaching(void* statePtr)
		{
			if (statePtr != base.currentStatePtr)
			{
				return this.ReadUnprocessedValueFromState(statePtr);
			}
			return *this.unprocessedValue;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000100A4 File Offset: 0x0000E2A4
		public unsafe TValue ReadUnprocessedValue()
		{
			return *this.unprocessedValue;
		}

		// Token: 0x060003D4 RID: 980
		public unsafe abstract TValue ReadUnprocessedValueFromState(void* statePtr);

		// Token: 0x060003D5 RID: 981 RVA: 0x000100B1 File Offset: 0x0000E2B1
		public unsafe override object ReadValueFromStateAsObject(void* statePtr)
		{
			return this.ReadValueFromState(statePtr);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x000100C0 File Offset: 0x0000E2C0
		public unsafe override void ReadValueFromStateIntoBuffer(void* statePtr, void* bufferPtr, int bufferSize)
		{
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			if (bufferPtr == null)
			{
				throw new ArgumentNullException("bufferPtr");
			}
			int num = UnsafeUtility.SizeOf<TValue>();
			if (bufferSize < num)
			{
				throw new ArgumentException(string.Format("bufferSize={0} < sizeof(TValue)={1}", bufferSize, num), "bufferSize");
			}
			TValue tvalue = this.ReadValueFromState(statePtr);
			void* ptr = UnsafeUtility.AddressOf<TValue>(ref tvalue);
			UnsafeUtility.MemCpy(bufferPtr, ptr, (long)num);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00010134 File Offset: 0x0000E334
		public unsafe override void WriteValueFromBufferIntoState(void* bufferPtr, int bufferSize, void* statePtr)
		{
			if (bufferPtr == null)
			{
				throw new ArgumentNullException("bufferPtr");
			}
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			int num = UnsafeUtility.SizeOf<TValue>();
			if (bufferSize < num)
			{
				throw new ArgumentException(string.Format("bufferSize={0} < sizeof(TValue)={1}", bufferSize, num), "bufferSize");
			}
			TValue tvalue = default(TValue);
			UnsafeUtility.MemCpy(UnsafeUtility.AddressOf<TValue>(ref tvalue), bufferPtr, (long)num);
			this.WriteValueIntoState(tvalue, statePtr);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x000101AC File Offset: 0x0000E3AC
		public unsafe override void WriteValueFromObjectIntoState(object value, void* statePtr)
		{
			if (statePtr == null)
			{
				throw new ArgumentNullException("statePtr");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is TValue))
			{
				value = Convert.ChangeType(value, typeof(TValue));
			}
			TValue tvalue = (TValue)((object)value);
			this.WriteValueIntoState(tvalue, statePtr);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00010200 File Offset: 0x0000E400
		public unsafe virtual void WriteValueIntoState(TValue value, void* statePtr)
		{
			throw new NotSupportedException(string.Format("Control '{0}' does not support writing", this));
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00010214 File Offset: 0x0000E414
		public unsafe override object ReadValueFromBufferAsObject(void* buffer, int bufferSize)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = UnsafeUtility.SizeOf<TValue>();
			if (bufferSize < num)
			{
				throw new ArgumentException(string.Format("Expecting buffer of at least {0} bytes for value of type {1} but got buffer of only {2} bytes instead", num, typeof(TValue).Name, bufferSize), "bufferSize");
			}
			TValue tvalue = default(TValue);
			UnsafeUtility.MemCpy(UnsafeUtility.AddressOf<TValue>(ref tvalue), buffer, (long)num);
			return tvalue;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00010288 File Offset: 0x0000E488
		private unsafe static bool CompareValue(ref TValue firstValue, ref TValue secondValue)
		{
			void* ptr = UnsafeUtility.AddressOf<TValue>(ref firstValue);
			void* ptr2 = UnsafeUtility.AddressOf<TValue>(ref secondValue);
			return UnsafeUtility.MemCmp(ptr, ptr2, (long)UnsafeUtility.SizeOf<TValue>()) != 0;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x000102B4 File Offset: 0x0000E4B4
		public unsafe override bool CompareValue(void* firstStatePtr, void* secondStatePtr)
		{
			TValue tvalue = this.ReadValueFromState(firstStatePtr);
			TValue tvalue2 = this.ReadValueFromState(secondStatePtr);
			return InputControl<TValue>.CompareValue(ref tvalue, ref tvalue2);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x000102DA File Offset: 0x0000E4DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TValue ProcessValue(TValue value)
		{
			this.ProcessValue(ref value);
			return value;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000102E8 File Offset: 0x0000E4E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ProcessValue(ref TValue value)
		{
			if (this.m_ProcessorStack.length <= 0)
			{
				return;
			}
			value = this.m_ProcessorStack.firstValue.Process(value, this);
			if (this.m_ProcessorStack.additionalValues == null)
			{
				return;
			}
			for (int i = 0; i < this.m_ProcessorStack.length - 1; i++)
			{
				value = this.m_ProcessorStack.additionalValues[i].Process(value, this);
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00010368 File Offset: 0x0000E568
		internal TProcessor TryGetProcessor<TProcessor>() where TProcessor : InputProcessor<TValue>
		{
			if (this.m_ProcessorStack.length > 0)
			{
				TProcessor tprocessor = this.m_ProcessorStack.firstValue as TProcessor;
				if (tprocessor != null)
				{
					return tprocessor;
				}
				if (this.m_ProcessorStack.additionalValues != null)
				{
					for (int i = 0; i < this.m_ProcessorStack.length - 1; i++)
					{
						TProcessor tprocessor2 = this.m_ProcessorStack.additionalValues[i] as TProcessor;
						if (tprocessor2 != null)
						{
							return tprocessor2;
						}
					}
				}
			}
			return default(TProcessor);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000103F4 File Offset: 0x0000E5F4
		internal override void AddProcessor(object processor)
		{
			InputProcessor<TValue> inputProcessor = processor as InputProcessor<TValue>;
			if (inputProcessor == null)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Cannot add processor of type '",
					processor.GetType().Name,
					"' to control of type '",
					base.GetType().Name,
					"'"
				}), "processor");
			}
			this.m_ProcessorStack.Append(inputProcessor);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00010464 File Offset: 0x0000E664
		protected override void FinishSetup()
		{
			using (IEnumerator<InputProcessor<TValue>> enumerator = this.m_ProcessorStack.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.cachingPolicy == InputProcessor.CachingPolicy.EvaluateOnEveryRead)
					{
						this.evaluateProcessorsEveryRead = true;
					}
				}
			}
			base.FinishSetup();
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x000104C0 File Offset: 0x0000E6C0
		internal InputProcessor<TValue>[] processors
		{
			get
			{
				return this.m_ProcessorStack.ToArray();
			}
		}

		// Token: 0x04000130 RID: 304
		internal InlinedArray<InputProcessor<TValue>> m_ProcessorStack;

		// Token: 0x04000131 RID: 305
		private TValue m_CachedValue;

		// Token: 0x04000132 RID: 306
		private TValue m_UnprocessedCachedValue;

		// Token: 0x04000133 RID: 307
		internal bool evaluateProcessorsEveryRead;
	}
}
