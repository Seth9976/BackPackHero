using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x0200006E RID: 110
	public class BoneControl : InputControl<Bone>
	{
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x00035FD0 File Offset: 0x000341D0
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x00035FD8 File Offset: 0x000341D8
		[InputControl(offset = 0U, displayName = "parentBoneIndex")]
		public IntegerControl parentBoneIndex { get; private set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x00035FE1 File Offset: 0x000341E1
		// (set) Token: 0x060009E5 RID: 2533 RVA: 0x00035FE9 File Offset: 0x000341E9
		[InputControl(offset = 4U, displayName = "Position")]
		public Vector3Control position { get; private set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00035FF2 File Offset: 0x000341F2
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x00035FFA File Offset: 0x000341FA
		[InputControl(offset = 16U, displayName = "Rotation")]
		public QuaternionControl rotation { get; private set; }

		// Token: 0x060009E8 RID: 2536 RVA: 0x00036003 File Offset: 0x00034203
		protected override void FinishSetup()
		{
			this.parentBoneIndex = base.GetChildControl<IntegerControl>("parentBoneIndex");
			this.position = base.GetChildControl<Vector3Control>("position");
			this.rotation = base.GetChildControl<QuaternionControl>("rotation");
			base.FinishSetup();
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00036040 File Offset: 0x00034240
		public unsafe override Bone ReadUnprocessedValueFromState(void* statePtr)
		{
			return new Bone
			{
				parentBoneIndex = (uint)this.parentBoneIndex.ReadUnprocessedValueFromStateWithCaching(statePtr),
				position = this.position.ReadUnprocessedValueFromStateWithCaching(statePtr),
				rotation = this.rotation.ReadUnprocessedValueFromStateWithCaching(statePtr)
			};
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0003608F File Offset: 0x0003428F
		public unsafe override void WriteValueIntoState(Bone value, void* statePtr)
		{
			this.parentBoneIndex.WriteValueIntoState((int)value.parentBoneIndex, statePtr);
			this.position.WriteValueIntoState(value.position, statePtr);
			this.rotation.WriteValueIntoState(value.rotation, statePtr);
		}
	}
}
