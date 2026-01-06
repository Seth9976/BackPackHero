using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000ED RID: 237
	internal static class InputUpdate
	{
		// Token: 0x06000DFB RID: 3579 RVA: 0x00044AC5 File Offset: 0x00042CC5
		internal static void OnBeforeUpdate(InputUpdateType type)
		{
			InputUpdate.s_LatestUpdateType = type;
			if (type - InputUpdateType.Dynamic <= 1 || type == InputUpdateType.Manual)
			{
				InputUpdate.s_PlayerUpdateStepCount.OnBeforeUpdate();
				InputUpdate.s_UpdateStepCount = InputUpdate.s_PlayerUpdateStepCount.value;
			}
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00044AF1 File Offset: 0x00042CF1
		internal static void OnUpdate(InputUpdateType type)
		{
			InputUpdate.s_LatestUpdateType = type;
			if (type - InputUpdateType.Dynamic <= 1 || type == InputUpdateType.Manual)
			{
				InputUpdate.s_PlayerUpdateStepCount.OnUpdate();
				InputUpdate.s_UpdateStepCount = InputUpdate.s_PlayerUpdateStepCount.value;
			}
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00044B20 File Offset: 0x00042D20
		public static InputUpdate.SerializedState Save()
		{
			return new InputUpdate.SerializedState
			{
				lastUpdateType = InputUpdate.s_LatestUpdateType,
				playerUpdateStepCount = InputUpdate.s_PlayerUpdateStepCount
			};
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00044B50 File Offset: 0x00042D50
		public static void Restore(InputUpdate.SerializedState state)
		{
			InputUpdate.s_LatestUpdateType = state.lastUpdateType;
			InputUpdate.s_PlayerUpdateStepCount = state.playerUpdateStepCount;
			InputUpdateType inputUpdateType = InputUpdate.s_LatestUpdateType;
			if (inputUpdateType - InputUpdateType.Dynamic <= 1 || inputUpdateType == InputUpdateType.Manual)
			{
				InputUpdate.s_UpdateStepCount = InputUpdate.s_PlayerUpdateStepCount.value;
				return;
			}
			InputUpdate.s_UpdateStepCount = 0U;
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00044B9A File Offset: 0x00042D9A
		public static InputUpdateType GetUpdateTypeForPlayer(this InputUpdateType mask)
		{
			if ((mask & InputUpdateType.Manual) != InputUpdateType.None)
			{
				return InputUpdateType.Manual;
			}
			if ((mask & InputUpdateType.Dynamic) != InputUpdateType.None)
			{
				return InputUpdateType.Dynamic;
			}
			if ((mask & InputUpdateType.Fixed) != InputUpdateType.None)
			{
				return InputUpdateType.Fixed;
			}
			return InputUpdateType.None;
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00044BB4 File Offset: 0x00042DB4
		public static bool IsPlayerUpdate(this InputUpdateType updateType)
		{
			return updateType != InputUpdateType.Editor && updateType > InputUpdateType.None;
		}

		// Token: 0x040005A9 RID: 1449
		public static uint s_UpdateStepCount;

		// Token: 0x040005AA RID: 1450
		public static InputUpdateType s_LatestUpdateType;

		// Token: 0x040005AB RID: 1451
		public static InputUpdate.UpdateStepCount s_PlayerUpdateStepCount;

		// Token: 0x02000211 RID: 529
		[Serializable]
		public struct UpdateStepCount
		{
			// Token: 0x1700058F RID: 1423
			// (get) Token: 0x060014A7 RID: 5287 RVA: 0x0005FDAC File Offset: 0x0005DFAC
			// (set) Token: 0x060014A8 RID: 5288 RVA: 0x0005FDB4 File Offset: 0x0005DFB4
			public uint value { readonly get; private set; }

			// Token: 0x060014A9 RID: 5289 RVA: 0x0005FDC0 File Offset: 0x0005DFC0
			public void OnBeforeUpdate()
			{
				this.m_WasUpdated = true;
				uint value = this.value;
				this.value = value + 1U;
			}

			// Token: 0x060014AA RID: 5290 RVA: 0x0005FDE4 File Offset: 0x0005DFE4
			public void OnUpdate()
			{
				if (!this.m_WasUpdated)
				{
					uint value = this.value;
					this.value = value + 1U;
				}
				this.m_WasUpdated = false;
			}

			// Token: 0x04000B45 RID: 2885
			private bool m_WasUpdated;
		}

		// Token: 0x02000212 RID: 530
		[Serializable]
		public struct SerializedState
		{
			// Token: 0x04000B47 RID: 2887
			public InputUpdateType lastUpdateType;

			// Token: 0x04000B48 RID: 2888
			public InputUpdate.UpdateStepCount playerUpdateStepCount;
		}
	}
}
