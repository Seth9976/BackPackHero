using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200001B RID: 27
	public class InputActionAsset : ScriptableObject, IInputActionCollection2, IInputActionCollection, IEnumerable<InputAction>, IEnumerable
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00003E44 File Offset: 0x00002044
		public bool enabled
		{
			get
			{
				using (ReadOnlyArray<InputActionMap>.Enumerator enumerator = this.actionMaps.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.enabled)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00003EA0 File Offset: 0x000020A0
		public ReadOnlyArray<InputActionMap> actionMaps
		{
			get
			{
				return new ReadOnlyArray<InputActionMap>(this.m_ActionMaps);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00003EAD File Offset: 0x000020AD
		public ReadOnlyArray<InputControlScheme> controlSchemes
		{
			get
			{
				return new ReadOnlyArray<InputControlScheme>(this.m_ControlSchemes);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00003EBA File Offset: 0x000020BA
		public IEnumerable<InputBinding> bindings
		{
			get
			{
				int numActionMaps = this.m_ActionMaps.LengthSafe<InputActionMap>();
				if (numActionMaps == 0)
				{
					yield break;
				}
				int num;
				for (int i = 0; i < numActionMaps; i = num)
				{
					InputActionMap inputActionMap = this.m_ActionMaps[i];
					InputBinding[] bindings = inputActionMap.m_Bindings;
					int numBindings = bindings.LengthSafe<InputBinding>();
					for (int j = 0; j < numBindings; j = num)
					{
						yield return bindings[j];
						num = j + 1;
					}
					bindings = null;
					num = i + 1;
				}
				yield break;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00003ECA File Offset: 0x000020CA
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00003ED4 File Offset: 0x000020D4
		public InputBinding? bindingMask
		{
			get
			{
				return this.m_BindingMask;
			}
			set
			{
				if (this.m_BindingMask == value)
				{
					return;
				}
				this.m_BindingMask = value;
				this.ReResolveIfNecessary(true);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00003F2D File Offset: 0x0000212D
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00003F3A File Offset: 0x0000213A
		public ReadOnlyArray<InputDevice>? devices
		{
			get
			{
				return this.m_Devices.Get();
			}
			set
			{
				if (this.m_Devices.Set(value))
				{
					this.ReResolveIfNecessary(false);
				}
			}
		}

		// Token: 0x1700009C RID: 156
		public InputAction this[string actionNameOrId]
		{
			get
			{
				InputAction inputAction = this.FindAction(actionNameOrId, false);
				if (inputAction == null)
				{
					throw new KeyNotFoundException(string.Format("Cannot find action '{0}' in '{1}'", actionNameOrId, this));
				}
				return inputAction;
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00003F70 File Offset: 0x00002170
		public string ToJson()
		{
			return JsonUtility.ToJson(new InputActionAsset.WriteFileJson
			{
				name = base.name,
				maps = InputActionMap.WriteFileJson.FromMaps(this.m_ActionMaps).maps,
				controlSchemes = InputControlScheme.SchemeJson.ToJson(this.m_ControlSchemes)
			}, true);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00003FC8 File Offset: 0x000021C8
		public void LoadFromJson(string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				throw new ArgumentNullException("json");
			}
			JsonUtility.FromJson<InputActionAsset.ReadFileJson>(json).ToAsset(this);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00003FF7 File Offset: 0x000021F7
		public static InputActionAsset FromJson(string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				throw new ArgumentNullException("json");
			}
			InputActionAsset inputActionAsset = ScriptableObject.CreateInstance<InputActionAsset>();
			inputActionAsset.LoadFromJson(json);
			return inputActionAsset;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00004018 File Offset: 0x00002218
		public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
		{
			if (actionNameOrId == null)
			{
				throw new ArgumentNullException("actionNameOrId");
			}
			if (this.m_ActionMaps != null)
			{
				int num = actionNameOrId.IndexOf('/');
				if (num == -1)
				{
					InputAction inputAction = null;
					for (int i = 0; i < this.m_ActionMaps.Length; i++)
					{
						InputAction inputAction2 = this.m_ActionMaps[i].FindAction(actionNameOrId, false);
						if (inputAction2 != null)
						{
							if (inputAction2.enabled || inputAction2.m_Id == actionNameOrId)
							{
								return inputAction2;
							}
							if (inputAction == null)
							{
								inputAction = inputAction2;
							}
						}
					}
					if (inputAction != null)
					{
						return inputAction;
					}
				}
				else
				{
					Substring substring = new Substring(actionNameOrId, 0, num);
					Substring substring2 = new Substring(actionNameOrId, num + 1);
					if (substring.isEmpty || substring2.isEmpty)
					{
						throw new ArgumentException("Malformed action path: " + actionNameOrId, "actionNameOrId");
					}
					for (int j = 0; j < this.m_ActionMaps.Length; j++)
					{
						InputActionMap inputActionMap = this.m_ActionMaps[j];
						if (Substring.Compare(inputActionMap.name, substring, StringComparison.InvariantCultureIgnoreCase) == 0)
						{
							foreach (InputAction inputAction3 in inputActionMap.m_Actions)
							{
								if (Substring.Compare(inputAction3.name, substring2, StringComparison.InvariantCultureIgnoreCase) == 0)
								{
									return inputAction3;
								}
							}
							break;
						}
					}
				}
			}
			if (throwIfNotFound)
			{
				throw new ArgumentException(string.Format("No action '{0}' in '{1}'", actionNameOrId, this));
			}
			return null;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00004164 File Offset: 0x00002364
		public int FindBinding(InputBinding mask, out InputAction action)
		{
			int num = this.m_ActionMaps.LengthSafe<InputActionMap>();
			for (int i = 0; i < num; i++)
			{
				int num2 = this.m_ActionMaps[i].FindBinding(mask, out action);
				if (num2 >= 0)
				{
					return num2;
				}
			}
			action = null;
			return -1;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000041A4 File Offset: 0x000023A4
		public InputActionMap FindActionMap(string nameOrId, bool throwIfNotFound = false)
		{
			if (nameOrId == null)
			{
				throw new ArgumentNullException("nameOrId");
			}
			if (this.m_ActionMaps == null)
			{
				return null;
			}
			Guid guid;
			if (nameOrId.Contains('-') && Guid.TryParse(nameOrId, out guid))
			{
				for (int i = 0; i < this.m_ActionMaps.Length; i++)
				{
					InputActionMap inputActionMap = this.m_ActionMaps[i];
					if (inputActionMap.idDontGenerate == guid)
					{
						return inputActionMap;
					}
				}
			}
			for (int j = 0; j < this.m_ActionMaps.Length; j++)
			{
				InputActionMap inputActionMap2 = this.m_ActionMaps[j];
				if (string.Compare(nameOrId, inputActionMap2.name, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return inputActionMap2;
				}
			}
			if (throwIfNotFound)
			{
				throw new ArgumentException(string.Format("Cannot find action map '{0}' in '{1}'", nameOrId, this));
			}
			return null;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00004250 File Offset: 0x00002450
		public InputActionMap FindActionMap(Guid id)
		{
			if (this.m_ActionMaps == null)
			{
				return null;
			}
			for (int i = 0; i < this.m_ActionMaps.Length; i++)
			{
				InputActionMap inputActionMap = this.m_ActionMaps[i];
				if (inputActionMap.idDontGenerate == id)
				{
					return inputActionMap;
				}
			}
			return null;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00004294 File Offset: 0x00002494
		public InputAction FindAction(Guid guid)
		{
			if (this.m_ActionMaps == null)
			{
				return null;
			}
			for (int i = 0; i < this.m_ActionMaps.Length; i++)
			{
				InputAction inputAction = this.m_ActionMaps[i].FindAction(guid);
				if (inputAction != null)
				{
					return inputAction;
				}
			}
			return null;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000042D4 File Offset: 0x000024D4
		public int FindControlSchemeIndex(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (this.m_ControlSchemes == null)
			{
				return -1;
			}
			for (int i = 0; i < this.m_ControlSchemes.Length; i++)
			{
				if (string.Compare(name, this.m_ControlSchemes[i].name, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00004330 File Offset: 0x00002530
		public InputControlScheme? FindControlScheme(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			int num = this.FindControlSchemeIndex(name);
			if (num == -1)
			{
				return null;
			}
			return new InputControlScheme?(this.m_ControlSchemes[num]);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00004378 File Offset: 0x00002578
		public bool IsUsableWithDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			int num = this.m_ControlSchemes.LengthSafe<InputControlScheme>();
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					if (this.m_ControlSchemes[i].SupportsDevice(device))
					{
						return true;
					}
				}
			}
			else
			{
				int num2 = this.m_ActionMaps.LengthSafe<InputActionMap>();
				for (int j = 0; j < num2; j++)
				{
					if (this.m_ActionMaps[j].IsUsableWithDevice(device))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000043F4 File Offset: 0x000025F4
		public void Enable()
		{
			foreach (InputActionMap inputActionMap in this.actionMaps)
			{
				inputActionMap.Enable();
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00004448 File Offset: 0x00002648
		public void Disable()
		{
			foreach (InputActionMap inputActionMap in this.actionMaps)
			{
				inputActionMap.Disable();
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000449C File Offset: 0x0000269C
		public bool Contains(InputAction action)
		{
			InputActionMap inputActionMap = ((action != null) ? action.actionMap : null);
			return inputActionMap != null && inputActionMap.asset == this;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000044C7 File Offset: 0x000026C7
		public IEnumerator<InputAction> GetEnumerator()
		{
			if (this.m_ActionMaps == null)
			{
				yield break;
			}
			int num;
			for (int i = 0; i < this.m_ActionMaps.Length; i = num)
			{
				ReadOnlyArray<InputAction> actions = this.m_ActionMaps[i].actions;
				int actionCount = actions.Count;
				for (int j = 0; j < actionCount; j = num)
				{
					yield return actions[j];
					num = j + 1;
				}
				actions = default(ReadOnlyArray<InputAction>);
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000044D6 File Offset: 0x000026D6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000044DE File Offset: 0x000026DE
		internal void MarkAsDirty()
		{
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000044E0 File Offset: 0x000026E0
		internal void OnWantToChangeSetup()
		{
			if (this.m_ActionMaps.LengthSafe<InputActionMap>() > 0)
			{
				this.m_ActionMaps[0].OnWantToChangeSetup();
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000044FD File Offset: 0x000026FD
		internal void OnSetupChanged()
		{
			this.MarkAsDirty();
			if (this.m_ActionMaps.LengthSafe<InputActionMap>() > 0)
			{
				this.m_ActionMaps[0].OnSetupChanged();
				return;
			}
			this.m_SharedStateForAllMaps = null;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00004528 File Offset: 0x00002728
		private void ReResolveIfNecessary(bool fullResolve)
		{
			if (this.m_SharedStateForAllMaps == null)
			{
				return;
			}
			this.m_ActionMaps[0].LazyResolveBindings(fullResolve);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00004544 File Offset: 0x00002744
		internal void ResolveBindingsIfNecessary()
		{
			if (this.m_ActionMaps.LengthSafe<InputActionMap>() > 0)
			{
				InputActionMap[] actionMaps = this.m_ActionMaps;
				int num = 0;
				while (num < actionMaps.Length && !actionMaps[num].ResolveBindingsIfNecessary())
				{
					num++;
				}
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000457E File Offset: 0x0000277E
		private void OnDestroy()
		{
			this.Disable();
			if (this.m_SharedStateForAllMaps != null)
			{
				this.m_SharedStateForAllMaps.Dispose();
				this.m_SharedStateForAllMaps = null;
			}
		}

		// Token: 0x04000092 RID: 146
		public const string Extension = "inputactions";

		// Token: 0x04000093 RID: 147
		[SerializeField]
		internal InputActionMap[] m_ActionMaps;

		// Token: 0x04000094 RID: 148
		[SerializeField]
		internal InputControlScheme[] m_ControlSchemes;

		// Token: 0x04000095 RID: 149
		[NonSerialized]
		internal InputActionState m_SharedStateForAllMaps;

		// Token: 0x04000096 RID: 150
		[NonSerialized]
		internal InputBinding? m_BindingMask;

		// Token: 0x04000097 RID: 151
		[NonSerialized]
		internal int m_ParameterOverridesCount;

		// Token: 0x04000098 RID: 152
		[NonSerialized]
		internal InputActionRebindingExtensions.ParameterOverride[] m_ParameterOverrides;

		// Token: 0x04000099 RID: 153
		[NonSerialized]
		internal InputActionMap.DeviceArray m_Devices;

		// Token: 0x02000152 RID: 338
		[Serializable]
		internal struct WriteFileJson
		{
			// Token: 0x04000722 RID: 1826
			public string name;

			// Token: 0x04000723 RID: 1827
			public InputActionMap.WriteMapJson[] maps;

			// Token: 0x04000724 RID: 1828
			public InputControlScheme.SchemeJson[] controlSchemes;
		}

		// Token: 0x02000153 RID: 339
		[Serializable]
		internal struct ReadFileJson
		{
			// Token: 0x06001203 RID: 4611 RVA: 0x00054AF0 File Offset: 0x00052CF0
			public void ToAsset(InputActionAsset asset)
			{
				asset.name = this.name;
				InputActionMap.ReadFileJson readFileJson = default(InputActionMap.ReadFileJson);
				readFileJson.maps = this.maps;
				asset.m_ActionMaps = readFileJson.ToMaps();
				asset.m_ControlSchemes = InputControlScheme.SchemeJson.ToSchemes(this.controlSchemes);
				if (asset.m_ActionMaps != null)
				{
					InputActionMap[] actionMaps = asset.m_ActionMaps;
					for (int i = 0; i < actionMaps.Length; i++)
					{
						actionMaps[i].m_Asset = asset;
					}
				}
			}

			// Token: 0x04000725 RID: 1829
			public string name;

			// Token: 0x04000726 RID: 1830
			public InputActionMap.ReadMapJson[] maps;

			// Token: 0x04000727 RID: 1831
			public InputControlScheme.SchemeJson[] controlSchemes;
		}
	}
}
