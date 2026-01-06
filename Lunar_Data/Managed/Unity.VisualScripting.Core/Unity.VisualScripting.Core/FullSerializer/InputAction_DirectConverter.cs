using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using UnityEngine.InputSystem;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x0200018A RID: 394
	[UsedImplicitly]
	public class InputAction_DirectConverter : fsDirectConverter<InputAction>
	{
		// Token: 0x06000A72 RID: 2674 RVA: 0x0002C048 File Offset: 0x0002A248
		protected override fsResult DoSerialize(InputAction model, Dictionary<string, fsData> serialized)
		{
			return fsResult.Success + base.SerializeMember<string>(serialized, null, "id", model.id.ToString()) + base.SerializeMember<string>(serialized, null, "name", model.name.ToString()) + base.SerializeMember<string>(serialized, null, "expectedControlType", model.expectedControlType) + base.SerializeMember<InputActionType>(serialized, null, "type", model.type);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0002C0D0 File Offset: 0x0002A2D0
		protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref InputAction model)
		{
			string text;
			string text2;
			string text3;
			InputActionType inputActionType;
			fsResult fsResult = fsResult.Success + base.DeserializeMember<string>(data, null, "id", out text) + base.DeserializeMember<string>(data, null, "name", out text2) + base.DeserializeMember<string>(data, null, "expectedControlType", out text3) + base.DeserializeMember<InputActionType>(data, null, "type", out inputActionType);
			model = InputAction_DirectConverter.MakeInputActionWithId(text, text2, text3, inputActionType);
			return fsResult;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0002C140 File Offset: 0x0002A340
		public static InputAction MakeInputActionWithId(string actionId, string actionName, string expectedControlType, InputActionType type)
		{
			InputAction inputAction = new InputAction();
			typeof(InputAction).GetField("m_Id", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(inputAction, actionId);
			typeof(InputAction).GetField("m_Name", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(inputAction, actionName);
			typeof(InputAction).GetField("m_Type", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(inputAction, type);
			inputAction.expectedControlType = expectedControlType;
			return inputAction;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002C1B7 File Offset: 0x0002A3B7
		public override object CreateInstance(fsData data, Type storageType)
		{
			return new InputAction();
		}
	}
}
