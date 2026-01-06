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
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x0004C59F File Offset: 0x0004A79F
		// (set) Token: 0x06000FD8 RID: 4056 RVA: 0x0004C5A7 File Offset: 0x0004A7A7
		public TouchPressControl press { get; set; }

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0004C5B0 File Offset: 0x0004A7B0
		// (set) Token: 0x06000FDA RID: 4058 RVA: 0x0004C5B8 File Offset: 0x0004A7B8
		public IntegerControl displayIndex { get; set; }

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x0004C5C1 File Offset: 0x0004A7C1
		// (set) Token: 0x06000FDC RID: 4060 RVA: 0x0004C5C9 File Offset: 0x0004A7C9
		public IntegerControl touchId { get; set; }

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x0004C5D2 File Offset: 0x0004A7D2
		// (set) Token: 0x06000FDE RID: 4062 RVA: 0x0004C5DA File Offset: 0x0004A7DA
		public Vector2Control position { get; set; }

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x0004C5E3 File Offset: 0x0004A7E3
		// (set) Token: 0x06000FE0 RID: 4064 RVA: 0x0004C5EB File Offset: 0x0004A7EB
		public DeltaControl delta { get; set; }

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x0004C5F4 File Offset: 0x0004A7F4
		// (set) Token: 0x06000FE2 RID: 4066 RVA: 0x0004C5FC File Offset: 0x0004A7FC
		public AxisControl pressure { get; set; }

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x0004C605 File Offset: 0x0004A805
		// (set) Token: 0x06000FE4 RID: 4068 RVA: 0x0004C60D File Offset: 0x0004A80D
		public Vector2Control radius { get; set; }

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x0004C616 File Offset: 0x0004A816
		// (set) Token: 0x06000FE6 RID: 4070 RVA: 0x0004C61E File Offset: 0x0004A81E
		public TouchPhaseControl phase { get; set; }

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x0004C627 File Offset: 0x0004A827
		// (set) Token: 0x06000FE8 RID: 4072 RVA: 0x0004C62F File Offset: 0x0004A82F
		public ButtonControl indirectTouch { get; set; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x0004C638 File Offset: 0x0004A838
		// (set) Token: 0x06000FEA RID: 4074 RVA: 0x0004C640 File Offset: 0x0004A840
		public ButtonControl tap { get; set; }

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x0004C649 File Offset: 0x0004A849
		// (set) Token: 0x06000FEC RID: 4076 RVA: 0x0004C651 File Offset: 0x0004A851
		public IntegerControl tapCount { get; set; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x0004C65A File Offset: 0x0004A85A
		// (set) Token: 0x06000FEE RID: 4078 RVA: 0x0004C662 File Offset: 0x0004A862
		public DoubleControl startTime { get; set; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x0004C66B File Offset: 0x0004A86B
		// (set) Token: 0x06000FF0 RID: 4080 RVA: 0x0004C673 File Offset: 0x0004A873
		public Vector2Control startPosition { get; set; }

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x0004C67C File Offset: 0x0004A87C
		public unsafe bool isInProgress
		{
			get
			{
				TouchPhase touchPhase = (TouchPhase)(*this.phase.value);
				return touchPhase - TouchPhase.Began <= 1 || touchPhase == TouchPhase.Stationary;
			}
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0004C6A3 File Offset: 0x0004A8A3
		public TouchControl()
		{
			this.m_StateBlock.format = new FourCC('T', 'O', 'U', 'C');
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0004C6C4 File Offset: 0x0004A8C4
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

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0004C7B4 File Offset: 0x0004A9B4
		public unsafe override TouchState ReadUnprocessedValueFromState(void* statePtr)
		{
			TouchState* ptr = (TouchState*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			return *ptr;
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0004C7D8 File Offset: 0x0004A9D8
		public unsafe override void WriteValueIntoState(TouchState value, void* statePtr)
		{
			TouchState* ptr = (TouchState*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			UnsafeUtility.MemCpy((void*)ptr, UnsafeUtility.AddressOf<TouchState>(ref value), (long)UnsafeUtility.SizeOf<TouchState>());
		}
	}
}
