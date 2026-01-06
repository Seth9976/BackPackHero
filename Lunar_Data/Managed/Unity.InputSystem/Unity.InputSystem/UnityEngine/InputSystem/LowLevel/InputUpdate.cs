using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000ED RID: 237
	internal static class InputUpdate
	{
		// Token: 0x06000DF7 RID: 3575 RVA: 0x00044A7D File Offset: 0x00042C7D
		internal static void OnBeforeUpdate(InputUpdateType type)
		{
			InputUpdate.s_LatestUpdateType = type;
			if (type - InputUpdateType.Dynamic <= 1 || type == InputUpdateType.Manual)
			{
				InputUpdate.s_PlayerUpdateStepCount.OnBeforeUpdate();
				InputUpdate.s_UpdateStepCount = InputUpdate.s_PlayerUpdateStepCount.value;
			}
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00044AA9 File Offset: 0x00042CA9
		internal static void OnUpdate(InputUpdateType type)
		{
			InputUpdate.s_LatestUpdateType = type;
			if (type - InputUpdateType.Dynamic <= 1 || type == InputUpdateType.Manual)
			{
				InputUpdate.s_PlayerUpdateStepCount.OnUpdate();
				InputUpdate.s_UpdateStepCount = InputUpdate.s_PlayerUpdateStepCount.value;
			}
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00044AD8 File Offset: 0x00042CD8
		public static InputUpdate.SerializedState Save()
		{
			return new InputUpdate.SerializedState
			{
				lastUpdateType = InputUpdate.s_LatestUpdateType,
				playerUpdateStepCount = InputUpdate.s_PlayerUpdateStepCount
			};
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00044B08 File Offset: 0x00042D08
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

		// Token: 0x06000DFB RID: 3579 RVA: 0x00044B52 File Offset: 0x00042D52
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

		// Token: 0x06000DFC RID: 3580 RVA: 0x00044B6C File Offset: 0x00042D6C
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
			// Token: 0x1700058D RID: 1421
			// (get) Token: 0x060014A0 RID: 5280 RVA: 0x0005FB98 File Offset: 0x0005DD98
			// (set) Token: 0x060014A1 RID: 5281 RVA: 0x0005FBA0 File Offset: 0x0005DDA0
			public uint value { readonly get; private set; }

			// Token: 0x060014A2 RID: 5282 RVA: 0x0005FBAC File Offset: 0x0005DDAC
			public void OnBeforeUpdate()
			{
				this.m_WasUpdated = true;
				uint value = this.value;
				this.value = value + 1U;
			}

			// Token: 0x060014A3 RID: 5283 RVA: 0x0005FBD0 File Offset: 0x0005DDD0
			public void OnUpdate()
			{
				if (!this.m_WasUpdated)
				{
					uint value = this.value;
					this.value = value + 1U;
				}
				this.m_WasUpdated = false;
			}

			// Token: 0x04000B44 RID: 2884
			private bool m_WasUpdated;
		}

		// Token: 0x02000212 RID: 530
		[Serializable]
		public struct SerializedState
		{
			// Token: 0x04000B46 RID: 2886
			public InputUpdateType lastUpdateType;

			// Token: 0x04000B47 RID: 2887
			public InputUpdate.UpdateStepCount playerUpdateStepCount;
		}
	}
}
