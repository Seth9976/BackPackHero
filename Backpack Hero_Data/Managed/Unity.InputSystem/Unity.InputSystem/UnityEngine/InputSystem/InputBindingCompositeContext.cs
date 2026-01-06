using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000028 RID: 40
	public struct InputBindingCompositeContext
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000C648 File Offset: 0x0000A848
		public unsafe IEnumerable<InputBindingCompositeContext.PartBinding> controls
		{
			get
			{
				if (this.m_State == null)
				{
					yield break;
				}
				int totalBindingCount = this.m_State.totalBindingCount;
				int num;
				for (int bindingIndex = this.m_BindingIndex + 1; bindingIndex < totalBindingCount; bindingIndex = num)
				{
					InputActionState.BindingState bindingState = *this.m_State.GetBindingState(bindingIndex);
					if (!bindingState.isPartOfComposite)
					{
						break;
					}
					int controlStartIndex = bindingState.controlStartIndex;
					for (int i = 0; i < bindingState.controlCount; i = num)
					{
						InputControl inputControl = this.m_State.controls[controlStartIndex + i];
						yield return new InputBindingCompositeContext.PartBinding
						{
							part = bindingState.partIndex,
							control = inputControl
						};
						num = i + 1;
					}
					num = bindingIndex + 1;
				}
				yield break;
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000C65D File Offset: 0x0000A85D
		public float EvaluateMagnitude(int partNumber)
		{
			return this.m_State.EvaluateCompositePartMagnitude(this.m_BindingIndex, partNumber);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000C674 File Offset: 0x0000A874
		public TValue ReadValue<TValue>(int partNumber) where TValue : struct, IComparable<TValue>
		{
			if (this.m_State == null)
			{
				return default(TValue);
			}
			int num;
			return this.m_State.ReadCompositePartValue<TValue, InputBindingCompositeContext.DefaultComparer<TValue>>(this.m_BindingIndex, partNumber, null, out num, default(InputBindingCompositeContext.DefaultComparer<TValue>));
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000C6B4 File Offset: 0x0000A8B4
		public TValue ReadValue<TValue>(int partNumber, out InputControl sourceControl) where TValue : struct, IComparable<TValue>
		{
			if (this.m_State == null)
			{
				sourceControl = null;
				return default(TValue);
			}
			int num;
			TValue tvalue = this.m_State.ReadCompositePartValue<TValue, InputBindingCompositeContext.DefaultComparer<TValue>>(this.m_BindingIndex, partNumber, null, out num, default(InputBindingCompositeContext.DefaultComparer<TValue>));
			if (num != -1)
			{
				sourceControl = this.m_State.controls[num];
				return tvalue;
			}
			sourceControl = null;
			return tvalue;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000C70C File Offset: 0x0000A90C
		public TValue ReadValue<TValue, TComparer>(int partNumber, TComparer comparer = default(TComparer)) where TValue : struct where TComparer : IComparer<TValue>
		{
			if (this.m_State == null)
			{
				return default(TValue);
			}
			int num;
			return this.m_State.ReadCompositePartValue<TValue, TComparer>(this.m_BindingIndex, partNumber, null, out num, comparer);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000C744 File Offset: 0x0000A944
		public TValue ReadValue<TValue, TComparer>(int partNumber, out InputControl sourceControl, TComparer comparer = default(TComparer)) where TValue : struct where TComparer : IComparer<TValue>
		{
			if (this.m_State == null)
			{
				sourceControl = null;
				return default(TValue);
			}
			int num;
			TValue tvalue = this.m_State.ReadCompositePartValue<TValue, TComparer>(this.m_BindingIndex, partNumber, null, out num, comparer);
			if (num != -1)
			{
				sourceControl = this.m_State.controls[num];
				return tvalue;
			}
			sourceControl = null;
			return tvalue;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000C794 File Offset: 0x0000A994
		public unsafe bool ReadValueAsButton(int partNumber)
		{
			if (this.m_State == null)
			{
				return false;
			}
			bool flag = false;
			int num;
			this.m_State.ReadCompositePartValue<float, InputBindingCompositeContext.DefaultComparer<float>>(this.m_BindingIndex, partNumber, &flag, out num, default(InputBindingCompositeContext.DefaultComparer<float>));
			return flag;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000C7CF File Offset: 0x0000A9CF
		public unsafe void ReadValue(int partNumber, void* buffer, int bufferSize)
		{
			InputActionState state = this.m_State;
			if (state == null)
			{
				return;
			}
			state.ReadCompositePartValue(this.m_BindingIndex, partNumber, buffer, bufferSize);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000C7EB File Offset: 0x0000A9EB
		public object ReadValueAsObject(int partNumber)
		{
			return this.m_State.ReadCompositePartValueAsObject(this.m_BindingIndex, partNumber);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000C7FF File Offset: 0x0000A9FF
		public double GetPressTime(int partNumber)
		{
			return this.m_State.GetCompositePartPressTime(this.m_BindingIndex, partNumber);
		}

		// Token: 0x040000E5 RID: 229
		internal InputActionState m_State;

		// Token: 0x040000E6 RID: 230
		internal int m_BindingIndex;

		// Token: 0x02000178 RID: 376
		public struct PartBinding
		{
			// Token: 0x1700052F RID: 1327
			// (get) Token: 0x0600131E RID: 4894 RVA: 0x00058D27 File Offset: 0x00056F27
			// (set) Token: 0x0600131F RID: 4895 RVA: 0x00058D2F File Offset: 0x00056F2F
			public int part { readonly get; set; }

			// Token: 0x17000530 RID: 1328
			// (get) Token: 0x06001320 RID: 4896 RVA: 0x00058D38 File Offset: 0x00056F38
			// (set) Token: 0x06001321 RID: 4897 RVA: 0x00058D40 File Offset: 0x00056F40
			public InputControl control { readonly get; set; }
		}

		// Token: 0x02000179 RID: 377
		private struct DefaultComparer<TValue> : IComparer<TValue> where TValue : IComparable<TValue>
		{
			// Token: 0x06001322 RID: 4898 RVA: 0x00058D49 File Offset: 0x00056F49
			public int Compare(TValue x, TValue y)
			{
				return x.CompareTo(y);
			}
		}
	}
}
