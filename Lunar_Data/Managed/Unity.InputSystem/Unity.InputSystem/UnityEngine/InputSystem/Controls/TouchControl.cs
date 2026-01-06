using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000118 RID: 280
	[InputControlLayout(stateType = typeof(TouchState))]
	public class TouchControl : InputControl<TouchState>
	{
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x0004C553 File Offset: 0x0004A753
		// (set) Token: 0x06000FD3 RID: 4051 RVA: 0x0004C55B File Offset: 0x0004A75B
		public TouchPressControl press { get; set; }

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x0004C564 File Offset: 0x0004A764
		// (set) Token: 0x06000FD5 RID: 4053 RVA: 0x0004C56C File Offset: 0x0004A76C
		public IntegerControl displayIndex { get; set; }

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x0004C575 File Offset: 0x0004A775
		// (set) Token: 0x06000FD7 RID: 4055 RVA: 0x0004C57D File Offset: 0x0004A77D
		public IntegerControl touchId { get; set; }

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x0004C586 File Offset: 0x0004A786
		// (set) Token: 0x06000FD9 RID: 4057 RVA: 0x0004C58E File Offset: 0x0004A78E
		public Vector2Control position { get; set; }

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x0004C597 File Offset: 0x0004A797
		// (set) Token: 0x06000FDB RID: 4059 RVA: 0x0004C59F File Offset: 0x0004A79F
		public DeltaControl delta { get; set; }

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x0004C5A8 File Offset: 0x0004A7A8
		// (set) Token: 0x06000FDD RID: 4061 RVA: 0x0004C5B0 File Offset: 0x0004A7B0
		public AxisControl pressure { get; set; }

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x0004C5B9 File Offset: 0x0004A7B9
		// (set) Token: 0x06000FDF RID: 4063 RVA: 0x0004C5C1 File Offset: 0x0004A7C1
		public Vector2Control radius { get; set; }

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x0004C5CA File Offset: 0x0004A7CA
		// (set) Token: 0x06000FE1 RID: 4065 RVA: 0x0004C5D2 File Offset: 0x0004A7D2
		public TouchPhaseControl phase { get; set; }

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x0004C5DB File Offset: 0x0004A7DB
		// (set) Token: 0x06000FE3 RID: 4067 RVA: 0x0004C5E3 File Offset: 0x0004A7E3
		public ButtonControl indirectTouch { get; set; }

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x0004C5EC File Offset: 0x0004A7EC
		// (set) Token: 0x06000FE5 RID: 4069 RVA: 0x0004C5F4 File Offset: 0x0004A7F4
		public ButtonControl tap { get; set; }

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x0004C5FD File Offset: 0x0004A7FD
		// (set) Token: 0x06000FE7 RID: 4071 RVA: 0x0004C605 File Offset: 0x0004A805
		public IntegerControl tapCount { get; set; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x0004C60E File Offset: 0x0004A80E
		// (set) Token: 0x06000FE9 RID: 4073 RVA: 0x0004C616 File Offset: 0x0004A816
		public DoubleControl startTime { get; set; }

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x0004C61F File Offset: 0x0004A81F
		// (set) Token: 0x06000FEB RID: 4075 RVA: 0x0004C627 File Offset: 0x0004A827
		public Vector2Control startPosition { get; set; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x0004C630 File Offset: 0x0004A830
		public unsafe bool isInProgress
		{
			get
			{
				TouchPhase touchPhase = (TouchPhase)(*this.phase.value);
				return touchPhase - TouchPhase.Began <= 1 || touchPhase == TouchPhase.Stationary;
			}
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0004C657 File Offset: 0x0004A857
		public TouchControl()
		{
			this.m_StateBlock.format = new FourCC('T', 'O', 'U', 'C');
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0004C678 File Offset: 0x0004A878
		protected override void FinishSetup()
		{
			this.press = base.GetChildControl<TouchPressControl>("press");
			this.displayIndex = base.GetChildControl<IntegerControl>("displayIndex");
			this.touchId = base.GetChildControl<IntegerControl>("touchId");
			this.position = base.GetChildControl<Vector2Control>("position");
			this.delta = base.GetChildControl<DeltaControl>("delta");
			this.pressure = base.GetChildControl<AxisControl>("pressure");
			this.radius = base.GetChildControl<Vector2Control>("radius");
			this.phase = base.GetChildControl<TouchPhaseControl>("phase");
			this.indirectTouch = base.GetChildControl<ButtonControl>("indirectTouch");
			this.tap = base.GetChildControl<ButtonControl>("tap");
			this.tapCount = base.GetChildControl<IntegerControl>("tapCount");
			this.startTime = base.GetChildControl<DoubleControl>("startTime");
			this.startPosition = base.GetChildControl<Vector2Control>("startPosition");
			base.FinishSetup();
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0004C768 File Offset: 0x0004A968
		public unsafe override TouchState ReadUnprocessedValueFromState(void* statePtr)
		{
			TouchState* ptr = (TouchState*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			return *ptr;
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0004C78C File Offset: 0x0004A98C
		public unsafe override void WriteValueIntoState(TouchState value, void* statePtr)
		{
			TouchState* ptr = (TouchState*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			UnsafeUtility.MemCpy((void*)ptr, UnsafeUtility.AddressOf<TouchState>(ref value), (long)UnsafeUtility.SizeOf<TouchState>());
		}
	}
}
