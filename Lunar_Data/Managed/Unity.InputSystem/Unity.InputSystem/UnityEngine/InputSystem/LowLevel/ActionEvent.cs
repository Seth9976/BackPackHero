using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000D7 RID: 215
	[StructLayout(LayoutKind.Explicit, Size = 37)]
	internal struct ActionEvent : IInputEventTypeInfo
	{
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00042286 File Offset: 0x00040486
		public static FourCC Type
		{
			get
			{
				return new FourCC('A', 'C', 'T', 'N');
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x00042295 File Offset: 0x00040495
		// (set) Token: 0x06000D08 RID: 3336 RVA: 0x0004229D File Offset: 0x0004049D
		public double startTime
		{
			get
			{
				return this.m_StartTime;
			}
			set
			{
				this.m_StartTime = value;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x000422A6 File Offset: 0x000404A6
		// (set) Token: 0x06000D0A RID: 3338 RVA: 0x000422AE File Offset: 0x000404AE
		public InputActionPhase phase
		{
			get
			{
				return (InputActionPhase)this.m_Phase;
			}
			set
			{
				this.m_Phase = (byte)value;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x000422B8 File Offset: 0x000404B8
		public unsafe byte* valueData
		{
			get
			{
				fixed (byte* ptr = &this.m_ValueData.FixedElementField)
				{
					return ptr;
				}
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x000422D3 File Offset: 0x000404D3
		public int valueSizeInBytes
		{
			get
			{
				return (int)(this.baseEvent.sizeInBytes - 20U - 16U);
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x000422E6 File Offset: 0x000404E6
		// (set) Token: 0x06000D0E RID: 3342 RVA: 0x000422EE File Offset: 0x000404EE
		public int stateIndex
		{
			get
			{
				return (int)this.m_StateIndex;
			}
			set
			{
				if (value < 0 || value > 255)
				{
					throw new NotSupportedException("State count cannot exceed byte.MaxValue");
				}
				this.m_StateIndex = (byte)value;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x0004230F File Offset: 0x0004050F
		// (set) Token: 0x06000D10 RID: 3344 RVA: 0x00042317 File Offset: 0x00040517
		public int controlIndex
		{
			get
			{
				return (int)this.m_ControlIndex;
			}
			set
			{
				if (value < 0 || value > 65535)
				{
					throw new NotSupportedException("Control count cannot exceed ushort.MaxValue");
				}
				this.m_ControlIndex = (ushort)value;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x00042338 File Offset: 0x00040538
		// (set) Token: 0x06000D12 RID: 3346 RVA: 0x00042340 File Offset: 0x00040540
		public int bindingIndex
		{
			get
			{
				return (int)this.m_BindingIndex;
			}
			set
			{
				if (value < 0 || value > 65535)
				{
					throw new NotSupportedException("Binding count cannot exceed ushort.MaxValue");
				}
				this.m_BindingIndex = (ushort)value;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x00042361 File Offset: 0x00040561
		// (set) Token: 0x06000D14 RID: 3348 RVA: 0x00042378 File Offset: 0x00040578
		public int interactionIndex
		{
			get
			{
				if (this.m_InteractionIndex == 65535)
				{
					return -1;
				}
				return (int)this.m_InteractionIndex;
			}
			set
			{
				if (value == -1)
				{
					this.m_InteractionIndex = ushort.MaxValue;
					return;
				}
				if (value < 0 || value >= 65535)
				{
					throw new NotSupportedException("Interaction count cannot exceed ushort.MaxValue-1");
				}
				this.m_InteractionIndex = (ushort)value;
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x000423AC File Offset: 0x000405AC
		public unsafe InputEventPtr ToEventPtr()
		{
			fixed (ActionEvent* ptr = &this)
			{
				return new InputEventPtr((InputEvent*)ptr);
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x000423C2 File Offset: 0x000405C2
		public FourCC typeStatic
		{
			get
			{
				return ActionEvent.Type;
			}
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x000423C9 File Offset: 0x000405C9
		public static int GetEventSizeWithValueSize(int valueSizeInBytes)
		{
			return 36 + valueSizeInBytes;
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x000423D0 File Offset: 0x000405D0
		public unsafe static ActionEvent* From(InputEventPtr ptr)
		{
			if (!ptr.valid)
			{
				throw new ArgumentNullException("ptr");
			}
			if (!ptr.IsA<ActionEvent>())
			{
				throw new InvalidCastException(string.Format("Cannot cast event with type '{0}' into ActionEvent", ptr.type));
			}
			return (ActionEvent*)ptr.data;
		}

		// Token: 0x04000548 RID: 1352
		[FieldOffset(0)]
		public InputEvent baseEvent;

		// Token: 0x04000549 RID: 1353
		[FieldOffset(20)]
		private ushort m_ControlIndex;

		// Token: 0x0400054A RID: 1354
		[FieldOffset(22)]
		private ushort m_BindingIndex;

		// Token: 0x0400054B RID: 1355
		[FieldOffset(24)]
		private ushort m_InteractionIndex;

		// Token: 0x0400054C RID: 1356
		[FieldOffset(26)]
		private byte m_StateIndex;

		// Token: 0x0400054D RID: 1357
		[FieldOffset(27)]
		private byte m_Phase;

		// Token: 0x0400054E RID: 1358
		[FieldOffset(28)]
		private double m_StartTime;

		// Token: 0x0400054F RID: 1359
		[FixedBuffer(typeof(byte), 1)]
		[FieldOffset(36)]
		public ActionEvent.<m_ValueData>e__FixedBuffer m_ValueData;

		// Token: 0x02000205 RID: 517
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct <m_ValueData>e__FixedBuffer
		{
			// Token: 0x04000B1E RID: 2846
			public byte FixedElementField;
		}
	}
}
