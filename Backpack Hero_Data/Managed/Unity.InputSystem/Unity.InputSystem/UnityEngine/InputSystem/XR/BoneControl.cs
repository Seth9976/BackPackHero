using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x0200006E RID: 110
	public class BoneControl : InputControl<Bone>
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0003600C File Offset: 0x0003420C
		// (set) Token: 0x060009E5 RID: 2533 RVA: 0x00036014 File Offset: 0x00034214
		[InputControl(offset = 0U, displayName = "parentBoneIndex")]
		public IntegerControl parentBoneIndex { get; private set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0003601D File Offset: 0x0003421D
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x00036025 File Offset: 0x00034225
		[InputControl(offset = 4U, displayName = "Position")]
		public Vector3Control position { get; private set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0003602E File Offset: 0x0003422E
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x00036036 File Offset: 0x00034236
		[InputControl(offset = 16U, displayName = "Rotation")]
		public QuaternionControl rotation { get; private set; }

		// Token: 0x060009EA RID: 2538 RVA: 0x0003603F File Offset: 0x0003423F
		protected override void FinishSetup()
		{
			this.parentBoneIndex = base.GetChildControl<IntegerControl>("parentBoneIndex");
			this.position = base.GetChildControl<Vector3Control>("position");
			this.rotation = base.GetChildControl<QuaternionControl>("rotation");
			base.FinishSetup();
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0003607C File Offset: 0x0003427C
		public unsafe override Bone ReadUnprocessedValueFromState(void* statePtr)
		{
			return new Bone
			{
				parentBoneIndex = (uint)this.parentBoneIndex.ReadUnprocessedValueFromStateWithCaching(statePtr),
				position = this.position.ReadUnprocessedValueFromStateWithCaching(statePtr),
				rotation = this.rotation.ReadUnprocessedValueFromStateWithCaching(statePtr)
			};
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x000360CB File Offset: 0x000342CB
		public unsafe override void WriteValueIntoState(Bone value, void* statePtr)
		{
			this.parentBoneIndex.WriteValueIntoState((int)value.parentBoneIndex, statePtr);
			this.position.WriteValueIntoState(value.position, statePtr);
			this.rotation.WriteValueIntoState(value.rotation, statePtr);
		}
	}
}
