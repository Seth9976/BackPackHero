using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000022 RID: 34
	public static class InputActionSetupExtensions
	{
		// Token: 0x06000206 RID: 518 RVA: 0x00007178 File Offset: 0x00005378
		public static InputActionMap AddActionMap(this InputActionAsset asset, string name)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (asset.FindActionMap(name, false) != null)
			{
				throw new InvalidOperationException("An action map called '" + name + "' already exists in the asset");
			}
			InputActionMap inputActionMap = new InputActionMap(name);
			inputActionMap.GenerateId();
			asset.AddActionMap(inputActionMap);
			return inputActionMap;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000071E4 File Offset: 0x000053E4
		public static void AddActionMap(this InputActionAsset asset, InputActionMap map)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (map == null)
			{
				throw new ArgumentNullException("map");
			}
			if (string.IsNullOrEmpty(map.name))
			{
				throw new InvalidOperationException("Maps added to an input action asset must be named");
			}
			if (map.asset != null)
			{
				throw new InvalidOperationException(string.Format("Cannot add map '{0}' to asset '{1}' as it has already been added to asset '{2}'", map, asset, map.asset));
			}
			if (asset.FindActionMap(map.name, false) != null)
			{
				throw new InvalidOperationException("An action map called '" + map.name + "' already exists in the asset");
			}
			map.OnWantToChangeSetup();
			asset.OnWantToChangeSetup();
			ArrayHelpers.Append<InputActionMap>(ref asset.m_ActionMaps, map);
			map.m_Asset = asset;
			asset.OnSetupChanged();
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000072A4 File Offset: 0x000054A4
		public static void RemoveActionMap(this InputActionAsset asset, InputActionMap map)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (map == null)
			{
				throw new ArgumentNullException("map");
			}
			map.OnWantToChangeSetup();
			asset.OnWantToChangeSetup();
			if (map.m_Asset != asset)
			{
				return;
			}
			ArrayHelpers.Erase<InputActionMap>(ref asset.m_ActionMaps, map);
			map.m_Asset = null;
			asset.OnSetupChanged();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00007308 File Offset: 0x00005508
		public static void RemoveActionMap(this InputActionAsset asset, string nameOrId)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (nameOrId == null)
			{
				throw new ArgumentNullException("nameOrId");
			}
			InputActionMap inputActionMap = asset.FindActionMap(nameOrId, false);
			if (inputActionMap != null)
			{
				asset.RemoveActionMap(inputActionMap);
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000734C File Offset: 0x0000554C
		public static InputAction AddAction(this InputActionMap map, string name, InputActionType type = InputActionType.Value, string binding = null, string interactions = null, string processors = null, string groups = null, string expectedControlLayout = null)
		{
			if (map == null)
			{
				throw new ArgumentNullException("map");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("Action must have name", "name");
			}
			map.OnWantToChangeSetup();
			if (map.FindAction(name, false) != null)
			{
				throw new InvalidOperationException(string.Concat(new string[] { "Cannot add action with duplicate name '", name, "' to set '", map.name, "'" }));
			}
			InputAction inputAction = new InputAction(name, type, null, null, null, null)
			{
				expectedControlType = expectedControlLayout
			};
			inputAction.GenerateId();
			ArrayHelpers.Append<InputAction>(ref map.m_Actions, inputAction);
			inputAction.m_ActionMap = map;
			if (!string.IsNullOrEmpty(binding))
			{
				inputAction.AddBinding(binding, interactions, processors, groups);
			}
			else
			{
				if (!string.IsNullOrEmpty(groups))
				{
					throw new ArgumentException(string.Format("No binding path was specified for action '{0}' but groups was specified ('{1}'); cannot apply groups without binding", inputAction, groups), "groups");
				}
				inputAction.m_Interactions = interactions;
				inputAction.m_Processors = processors;
				map.OnSetupChanged();
			}
			return inputAction;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007444 File Offset: 0x00005644
		public static void RemoveAction(this InputAction action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			InputActionMap actionMap = action.actionMap;
			if (actionMap == null)
			{
				throw new ArgumentException(string.Format("Action '{0}' does not belong to an action map; nowhere to remove from", action), "action");
			}
			actionMap.OnWantToChangeSetup();
			InputBinding[] array = action.bindings.ToArray();
			int num = actionMap.m_Actions.IndexOfReference(action, -1);
			ArrayHelpers.EraseAt<InputAction>(ref actionMap.m_Actions, num);
			action.m_ActionMap = null;
			action.m_SingletonActionBindings = array;
			int num2 = actionMap.m_Bindings.Length - array.Length;
			if (num2 == 0)
			{
				actionMap.m_Bindings = null;
			}
			else
			{
				InputBinding[] array2 = new InputBinding[num2];
				InputBinding[] bindings = actionMap.m_Bindings;
				int num3 = 0;
				for (int i = 0; i < bindings.Length; i++)
				{
					InputBinding binding = bindings[i];
					if (array.IndexOf((InputBinding b) => b == binding) == -1)
					{
						array2[num3++] = binding;
					}
				}
				actionMap.m_Bindings = array2;
			}
			actionMap.OnSetupChanged();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000754C File Offset: 0x0000574C
		public static void RemoveAction(this InputActionAsset asset, string nameOrId)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (nameOrId == null)
			{
				throw new ArgumentNullException("nameOrId");
			}
			InputAction inputAction = asset.FindAction(nameOrId, false);
			if (inputAction == null)
			{
				return;
			}
			inputAction.RemoveAction();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00007584 File Offset: 0x00005784
		public static InputActionSetupExtensions.BindingSyntax AddBinding(this InputAction action, string path, string interactions = null, string processors = null, string groups = null)
		{
			return action.AddBinding(new InputBinding
			{
				path = path,
				interactions = interactions,
				processors = processors,
				groups = groups
			});
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000075C1 File Offset: 0x000057C1
		public static InputActionSetupExtensions.BindingSyntax AddBinding(this InputAction action, InputControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			return action.AddBinding(control.path, null, null, null);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000075E0 File Offset: 0x000057E0
		public static InputActionSetupExtensions.BindingSyntax AddBinding(this InputAction action, InputBinding binding = default(InputBinding))
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			binding.action = action.name;
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			int num = InputActionSetupExtensions.AddBindingInternal(orCreateActionMap, binding, -1);
			return new InputActionSetupExtensions.BindingSyntax(orCreateActionMap, num, null);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00007620 File Offset: 0x00005820
		public static InputActionSetupExtensions.BindingSyntax AddBinding(this InputActionMap actionMap, string path, string interactions = null, string groups = null, string action = null, string processors = null)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path", "Binding path cannot be null");
			}
			return actionMap.AddBinding(new InputBinding
			{
				path = path,
				interactions = interactions,
				groups = groups,
				action = action,
				processors = processors
			});
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000767C File Offset: 0x0000587C
		public static InputActionSetupExtensions.BindingSyntax AddBinding(this InputActionMap actionMap, string path, InputAction action, string interactions = null, string groups = null)
		{
			if (action != null && action.actionMap != actionMap)
			{
				throw new ArgumentException(string.Format("Action '{0}' is not part of action map '{1}'", action, actionMap), "action");
			}
			if (action == null)
			{
				return actionMap.AddBinding(path, interactions, groups, null, null);
			}
			return actionMap.AddBinding(path, action.id, interactions, groups);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000076D0 File Offset: 0x000058D0
		public static InputActionSetupExtensions.BindingSyntax AddBinding(this InputActionMap actionMap, string path, Guid action, string interactions = null, string groups = null)
		{
			if (action == Guid.Empty)
			{
				return actionMap.AddBinding(path, interactions, groups, null, null);
			}
			return actionMap.AddBinding(path, interactions, groups, action.ToString(), null);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00007704 File Offset: 0x00005904
		public static InputActionSetupExtensions.BindingSyntax AddBinding(this InputActionMap actionMap, InputBinding binding)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (binding.path == null)
			{
				throw new ArgumentException("Binding path cannot be null", "binding");
			}
			int num = InputActionSetupExtensions.AddBindingInternal(actionMap, binding, -1);
			return new InputActionSetupExtensions.BindingSyntax(actionMap, num, null);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000774C File Offset: 0x0000594C
		public static InputActionSetupExtensions.CompositeSyntax AddCompositeBinding(this InputAction action, string composite, string interactions = null, string processors = null)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(composite))
			{
				throw new ArgumentException("Composite name cannot be null or empty", "composite");
			}
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			InputBinding inputBinding = new InputBinding
			{
				name = NameAndParameters.ParseName(composite),
				path = composite,
				interactions = interactions,
				processors = processors,
				isComposite = true,
				action = action.name
			};
			int num = InputActionSetupExtensions.AddBindingInternal(orCreateActionMap, inputBinding, -1);
			return new InputActionSetupExtensions.CompositeSyntax(orCreateActionMap, action, num);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000077DC File Offset: 0x000059DC
		private static int AddBindingInternal(InputActionMap map, InputBinding binding, int bindingIndex = -1)
		{
			if (string.IsNullOrEmpty(binding.m_Id))
			{
				binding.GenerateId();
			}
			if (bindingIndex < 0)
			{
				bindingIndex = ArrayHelpers.Append<InputBinding>(ref map.m_Bindings, binding);
			}
			else
			{
				ArrayHelpers.InsertAt<InputBinding>(ref map.m_Bindings, bindingIndex, binding);
			}
			if (map.asset != null)
			{
				map.asset.MarkAsDirty();
			}
			if (map.m_SingletonAction != null)
			{
				map.m_SingletonAction.m_SingletonActionBindings = map.m_Bindings;
			}
			map.OnBindingModified();
			return bindingIndex;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007858 File Offset: 0x00005A58
		public static InputActionSetupExtensions.BindingSyntax ChangeBinding(this InputAction action, int index)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			int num = action.BindingIndexOnActionToBindingIndexOnMap(index);
			return new InputActionSetupExtensions.BindingSyntax(action.GetOrCreateActionMap(), num, action);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00007888 File Offset: 0x00005A88
		public static InputActionSetupExtensions.BindingSyntax ChangeBinding(this InputAction action, string name)
		{
			return action.ChangeBinding(new InputBinding
			{
				name = name
			});
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000078AC File Offset: 0x00005AAC
		public static InputActionSetupExtensions.BindingSyntax ChangeBinding(this InputActionMap actionMap, int index)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (index < 0 || index >= actionMap.m_Bindings.LengthSafe<InputBinding>())
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return new InputActionSetupExtensions.BindingSyntax(actionMap, index, null);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000078E4 File Offset: 0x00005AE4
		public static InputActionSetupExtensions.BindingSyntax ChangeBindingWithId(this InputAction action, string id)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			return action.ChangeBinding(new InputBinding
			{
				m_Id = id
			});
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007918 File Offset: 0x00005B18
		public static InputActionSetupExtensions.BindingSyntax ChangeBindingWithId(this InputAction action, Guid id)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			return action.ChangeBinding(new InputBinding
			{
				id = id
			});
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000794C File Offset: 0x00005B4C
		public static InputActionSetupExtensions.BindingSyntax ChangeBindingWithGroup(this InputAction action, string group)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			return action.ChangeBinding(new InputBinding
			{
				groups = group
			});
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00007980 File Offset: 0x00005B80
		public static InputActionSetupExtensions.BindingSyntax ChangeBindingWithPath(this InputAction action, string path)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			return action.ChangeBinding(new InputBinding
			{
				path = path
			});
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000079B4 File Offset: 0x00005BB4
		public static InputActionSetupExtensions.BindingSyntax ChangeBinding(this InputAction action, InputBinding match)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			Guid idDontGenerate = action.idDontGenerate;
			match.action = action.id.ToString();
			int num = orCreateActionMap.FindBindingRelativeToMap(match);
			if (num == -1)
			{
				match.action = action.name;
				num = orCreateActionMap.FindBindingRelativeToMap(match);
			}
			if (num == -1)
			{
				return default(InputActionSetupExtensions.BindingSyntax);
			}
			return new InputActionSetupExtensions.BindingSyntax(orCreateActionMap, num, action);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00007A34 File Offset: 0x00005C34
		public static InputActionSetupExtensions.BindingSyntax ChangeCompositeBinding(this InputAction action, string compositeName)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(compositeName))
			{
				throw new ArgumentNullException("compositeName");
			}
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			InputBinding[] bindings = orCreateActionMap.m_Bindings;
			int num = bindings.LengthSafe<InputBinding>();
			for (int i = 0; i < num; i++)
			{
				ref InputBinding ptr = ref bindings[i];
				if (ptr.isComposite && ptr.TriggersAction(action) && (compositeName.Equals(ptr.name, StringComparison.InvariantCultureIgnoreCase) || compositeName.Equals(NameAndParameters.ParseName(ptr.path), StringComparison.InvariantCultureIgnoreCase)))
				{
					return new InputActionSetupExtensions.BindingSyntax(orCreateActionMap, i, action);
				}
			}
			return default(InputActionSetupExtensions.BindingSyntax);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00007AD8 File Offset: 0x00005CD8
		public static void Rename(this InputAction action, string newName)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(newName))
			{
				throw new ArgumentNullException("newName");
			}
			if (action.name == newName)
			{
				return;
			}
			InputActionMap actionMap = action.actionMap;
			if (((actionMap != null) ? actionMap.FindAction(newName, false) : null) != null)
			{
				throw new InvalidOperationException(string.Format("Cannot rename '{0}' to '{1}' in map '{2}' as the map already contains an action with that name", action, newName, actionMap));
			}
			string name = action.m_Name;
			action.m_Name = newName;
			if (actionMap != null)
			{
				actionMap.ClearActionLookupTable();
			}
			if (((actionMap != null) ? actionMap.asset : null) != null && actionMap != null)
			{
				actionMap.asset.MarkAsDirty();
			}
			InputBinding[] bindings = action.GetOrCreateActionMap().m_Bindings;
			int num = bindings.LengthSafe<InputBinding>();
			for (int i = 0; i < num; i++)
			{
				if (string.Compare(bindings[i].action, name, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					bindings[i].action = newName;
				}
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007BC0 File Offset: 0x00005DC0
		public static void AddControlScheme(this InputActionAsset asset, InputControlScheme controlScheme)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(controlScheme.name))
			{
				throw new ArgumentException("Cannot add control scheme without name to asset " + asset.name, "controlScheme");
			}
			if (asset.FindControlScheme(controlScheme.name) != null)
			{
				throw new InvalidOperationException(string.Concat(new string[] { "Asset '", asset.name, "' already contains a control scheme called '", controlScheme.name, "'" }));
			}
			ArrayHelpers.Append<InputControlScheme>(ref asset.m_ControlSchemes, controlScheme);
			asset.MarkAsDirty();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00007C70 File Offset: 0x00005E70
		public static InputActionSetupExtensions.ControlSchemeSyntax AddControlScheme(this InputActionAsset asset, string name)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			int count = asset.controlSchemes.Count;
			asset.AddControlScheme(new InputControlScheme(name, null, null));
			return new InputActionSetupExtensions.ControlSchemeSyntax(asset, count);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00007CC8 File Offset: 0x00005EC8
		public static void RemoveControlScheme(this InputActionAsset asset, string name)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			int num = asset.FindControlSchemeIndex(name);
			if (num != -1)
			{
				ArrayHelpers.EraseAt<InputControlScheme>(ref asset.m_ControlSchemes, num);
			}
			asset.MarkAsDirty();
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00007D1C File Offset: 0x00005F1C
		public static InputControlScheme WithBindingGroup(this InputControlScheme scheme, string bindingGroup)
		{
			return new InputActionSetupExtensions.ControlSchemeSyntax(scheme).WithBindingGroup(bindingGroup).Done();
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00007D40 File Offset: 0x00005F40
		public static InputControlScheme WithDevice(this InputControlScheme scheme, string controlPath, bool required)
		{
			if (required)
			{
				return new InputActionSetupExtensions.ControlSchemeSyntax(scheme).WithRequiredDevice(controlPath).Done();
			}
			return new InputActionSetupExtensions.ControlSchemeSyntax(scheme).WithOptionalDevice(controlPath).Done();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007D80 File Offset: 0x00005F80
		public static InputControlScheme WithRequiredDevice(this InputControlScheme scheme, string controlPath)
		{
			return new InputActionSetupExtensions.ControlSchemeSyntax(scheme).WithRequiredDevice(controlPath).Done();
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007DA4 File Offset: 0x00005FA4
		public static InputControlScheme WithOptionalDevice(this InputControlScheme scheme, string controlPath)
		{
			return new InputActionSetupExtensions.ControlSchemeSyntax(scheme).WithOptionalDevice(controlPath).Done();
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00007DC8 File Offset: 0x00005FC8
		public static InputControlScheme OrWithRequiredDevice(this InputControlScheme scheme, string controlPath)
		{
			return new InputActionSetupExtensions.ControlSchemeSyntax(scheme).OrWithRequiredDevice(controlPath).Done();
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00007DEC File Offset: 0x00005FEC
		public static InputControlScheme OrWithOptionalDevice(this InputControlScheme scheme, string controlPath)
		{
			return new InputActionSetupExtensions.ControlSchemeSyntax(scheme).OrWithOptionalDevice(controlPath).Done();
		}

		// Token: 0x02000168 RID: 360
		public struct BindingSyntax
		{
			// Token: 0x170004EE RID: 1262
			// (get) Token: 0x0600126D RID: 4717 RVA: 0x00056FD6 File Offset: 0x000551D6
			public bool valid
			{
				get
				{
					return this.m_ActionMap != null && this.m_BindingIndexInMap >= 0 && this.m_BindingIndexInMap < this.m_ActionMap.m_Bindings.LengthSafe<InputBinding>();
				}
			}

			// Token: 0x170004EF RID: 1263
			// (get) Token: 0x0600126E RID: 4718 RVA: 0x00057003 File Offset: 0x00055203
			public int bindingIndex
			{
				get
				{
					if (!this.valid)
					{
						return -1;
					}
					if (this.m_Action != null)
					{
						return this.m_Action.BindingIndexOnMapToBindingIndexOnAction(this.m_BindingIndexInMap);
					}
					return this.m_BindingIndexInMap;
				}
			}

			// Token: 0x170004F0 RID: 1264
			// (get) Token: 0x0600126F RID: 4719 RVA: 0x0005702F File Offset: 0x0005522F
			public InputBinding binding
			{
				get
				{
					if (!this.valid)
					{
						throw new InvalidOperationException("BindingSyntax accessor is not valid");
					}
					return this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap];
				}
			}

			// Token: 0x06001270 RID: 4720 RVA: 0x0005705A File Offset: 0x0005525A
			internal BindingSyntax(InputActionMap map, int bindingIndexInMap, InputAction action = null)
			{
				this.m_ActionMap = map;
				this.m_BindingIndexInMap = bindingIndexInMap;
				this.m_Action = action;
			}

			// Token: 0x06001271 RID: 4721 RVA: 0x00057074 File Offset: 0x00055274
			public InputActionSetupExtensions.BindingSyntax WithName(string name)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].name = name;
				this.m_ActionMap.OnBindingModified();
				return this;
			}

			// Token: 0x06001272 RID: 4722 RVA: 0x000570C4 File Offset: 0x000552C4
			public InputActionSetupExtensions.BindingSyntax WithPath(string path)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].path = path;
				this.m_ActionMap.OnBindingModified();
				return this;
			}

			// Token: 0x06001273 RID: 4723 RVA: 0x00057114 File Offset: 0x00055314
			public InputActionSetupExtensions.BindingSyntax WithGroup(string group)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(group))
				{
					throw new ArgumentException("Group name cannot be null or empty", "group");
				}
				if (group.IndexOf(';') != -1)
				{
					throw new ArgumentException(string.Format("Group name cannot contain separator character '{0}'", ';'), "group");
				}
				return this.WithGroups(group);
			}

			// Token: 0x06001274 RID: 4724 RVA: 0x0005717C File Offset: 0x0005537C
			public InputActionSetupExtensions.BindingSyntax WithGroups(string groups)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(groups))
				{
					return this;
				}
				string groups2 = this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].groups;
				if (!string.IsNullOrEmpty(groups2))
				{
					groups = string.Join(";", new string[] { groups2, groups });
				}
				this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].groups = groups;
				this.m_ActionMap.OnBindingModified();
				return this;
			}

			// Token: 0x06001275 RID: 4725 RVA: 0x00057218 File Offset: 0x00055418
			public InputActionSetupExtensions.BindingSyntax WithInteraction(string interaction)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(interaction))
				{
					throw new ArgumentException("Interaction cannot be null or empty", "interaction");
				}
				if (interaction.IndexOf(';') != -1)
				{
					throw new ArgumentException(string.Format("Interaction string cannot contain separator character '{0}'", ';'), "interaction");
				}
				return this.WithInteractions(interaction);
			}

			// Token: 0x06001276 RID: 4726 RVA: 0x00057280 File Offset: 0x00055480
			public InputActionSetupExtensions.BindingSyntax WithInteractions(string interactions)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(interactions))
				{
					return this;
				}
				string interactions2 = this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].interactions;
				if (!string.IsNullOrEmpty(interactions2))
				{
					interactions = string.Join(";", new string[] { interactions2, interactions });
				}
				this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].interactions = interactions;
				this.m_ActionMap.OnBindingModified();
				return this;
			}

			// Token: 0x06001277 RID: 4727 RVA: 0x0005731C File Offset: 0x0005551C
			public InputActionSetupExtensions.BindingSyntax WithInteraction<TInteraction>() where TInteraction : IInputInteraction
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				InternedString internedString = InputProcessor.s_Processors.FindNameForType(typeof(TInteraction));
				if (internedString.IsEmpty())
				{
					throw new NotSupportedException(string.Format("Type '{0}' has not been registered as a processor", typeof(TInteraction)));
				}
				return this.WithInteraction(internedString);
			}

			// Token: 0x06001278 RID: 4728 RVA: 0x00057380 File Offset: 0x00055580
			public InputActionSetupExtensions.BindingSyntax WithProcessor(string processor)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(processor))
				{
					throw new ArgumentException("Processor cannot be null or empty", "processor");
				}
				if (processor.IndexOf(';') != -1)
				{
					throw new ArgumentException(string.Format("Processor string cannot contain separator character '{0}'", ';'), "processor");
				}
				return this.WithProcessors(processor);
			}

			// Token: 0x06001279 RID: 4729 RVA: 0x000573E8 File Offset: 0x000555E8
			public InputActionSetupExtensions.BindingSyntax WithProcessors(string processors)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				if (string.IsNullOrEmpty(processors))
				{
					return this;
				}
				string processors2 = this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].processors;
				if (!string.IsNullOrEmpty(processors2))
				{
					processors = string.Join(";", new string[] { processors2, processors });
				}
				this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].processors = processors;
				this.m_ActionMap.OnBindingModified();
				return this;
			}

			// Token: 0x0600127A RID: 4730 RVA: 0x00057484 File Offset: 0x00055684
			public InputActionSetupExtensions.BindingSyntax WithProcessor<TProcessor>()
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				InternedString internedString = InputProcessor.s_Processors.FindNameForType(typeof(TProcessor));
				if (internedString.IsEmpty())
				{
					throw new NotSupportedException(string.Format("Type '{0}' has not been registered as a processor", typeof(TProcessor)));
				}
				return this.WithProcessor(internedString);
			}

			// Token: 0x0600127B RID: 4731 RVA: 0x000574E8 File Offset: 0x000556E8
			public InputActionSetupExtensions.BindingSyntax Triggering(InputAction action)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				if (action == null)
				{
					throw new ArgumentNullException("action");
				}
				if (action.isSingletonAction)
				{
					throw new ArgumentException(string.Format("Cannot change the action a binding triggers on singleton action '{0}'", action), "action");
				}
				this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].action = action.name;
				this.m_ActionMap.OnBindingModified();
				return this;
			}

			// Token: 0x0600127C RID: 4732 RVA: 0x00057568 File Offset: 0x00055768
			public InputActionSetupExtensions.BindingSyntax To(InputBinding binding)
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Accessor is not valid");
				}
				this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap] = binding;
				if (this.m_ActionMap.m_SingletonAction != null)
				{
					this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].action = this.m_ActionMap.m_SingletonAction.name;
				}
				this.m_ActionMap.OnBindingModified();
				return this;
			}

			// Token: 0x0600127D RID: 4733 RVA: 0x000575E8 File Offset: 0x000557E8
			public InputActionSetupExtensions.BindingSyntax NextBinding()
			{
				return this.Iterate(true);
			}

			// Token: 0x0600127E RID: 4734 RVA: 0x000575F1 File Offset: 0x000557F1
			public InputActionSetupExtensions.BindingSyntax PreviousBinding()
			{
				return this.Iterate(false);
			}

			// Token: 0x0600127F RID: 4735 RVA: 0x000575FA File Offset: 0x000557FA
			public InputActionSetupExtensions.BindingSyntax NextPartBinding(string partName)
			{
				if (string.IsNullOrEmpty(partName))
				{
					throw new ArgumentNullException("partName");
				}
				return this.IteratePartBinding(true, partName);
			}

			// Token: 0x06001280 RID: 4736 RVA: 0x00057617 File Offset: 0x00055817
			public InputActionSetupExtensions.BindingSyntax PreviousPartBinding(string partName)
			{
				if (string.IsNullOrEmpty(partName))
				{
					throw new ArgumentNullException("partName");
				}
				return this.IteratePartBinding(false, partName);
			}

			// Token: 0x06001281 RID: 4737 RVA: 0x00057634 File Offset: 0x00055834
			public InputActionSetupExtensions.BindingSyntax NextCompositeBinding(string compositeName = null)
			{
				return this.IterateCompositeBinding(true, compositeName);
			}

			// Token: 0x06001282 RID: 4738 RVA: 0x0005763E File Offset: 0x0005583E
			public InputActionSetupExtensions.BindingSyntax PreviousCompositeBinding(string compositeName = null)
			{
				return this.IterateCompositeBinding(false, compositeName);
			}

			// Token: 0x06001283 RID: 4739 RVA: 0x00057648 File Offset: 0x00055848
			private InputActionSetupExtensions.BindingSyntax Iterate(bool next)
			{
				if (this.m_ActionMap == null)
				{
					return default(InputActionSetupExtensions.BindingSyntax);
				}
				InputBinding[] bindings = this.m_ActionMap.m_Bindings;
				if (bindings == null)
				{
					return default(InputActionSetupExtensions.BindingSyntax);
				}
				int num = this.m_BindingIndexInMap;
				for (;;)
				{
					num += (next ? 1 : (-1));
					if (num < 0 || num >= bindings.Length)
					{
						break;
					}
					if (this.m_Action == null || bindings[num].TriggersAction(this.m_Action))
					{
						goto IL_006C;
					}
				}
				return default(InputActionSetupExtensions.BindingSyntax);
				IL_006C:
				return new InputActionSetupExtensions.BindingSyntax(this.m_ActionMap, num, this.m_Action);
			}

			// Token: 0x06001284 RID: 4740 RVA: 0x000576D4 File Offset: 0x000558D4
			private InputActionSetupExtensions.BindingSyntax IterateCompositeBinding(bool next, string compositeName)
			{
				InputActionSetupExtensions.BindingSyntax bindingSyntax = this.Iterate(next);
				while (bindingSyntax.valid)
				{
					if (bindingSyntax.binding.isComposite)
					{
						if (compositeName == null)
						{
							return bindingSyntax;
						}
						if (compositeName.Equals(bindingSyntax.binding.name, StringComparison.InvariantCultureIgnoreCase))
						{
							return bindingSyntax;
						}
						string text = NameAndParameters.ParseName(bindingSyntax.binding.path);
						if (compositeName.Equals(text, StringComparison.InvariantCultureIgnoreCase))
						{
							return bindingSyntax;
						}
					}
					bindingSyntax = bindingSyntax.Iterate(next);
				}
				return default(InputActionSetupExtensions.BindingSyntax);
			}

			// Token: 0x06001285 RID: 4741 RVA: 0x00057758 File Offset: 0x00055958
			private InputActionSetupExtensions.BindingSyntax IteratePartBinding(bool next, string partName)
			{
				if (!this.valid)
				{
					return default(InputActionSetupExtensions.BindingSyntax);
				}
				if (this.binding.isComposite)
				{
					if (!next)
					{
						return default(InputActionSetupExtensions.BindingSyntax);
					}
				}
				else if (!this.binding.isPartOfComposite)
				{
					return default(InputActionSetupExtensions.BindingSyntax);
				}
				InputActionSetupExtensions.BindingSyntax bindingSyntax = this.Iterate(next);
				while (bindingSyntax.valid)
				{
					if (!bindingSyntax.binding.isPartOfComposite)
					{
						return default(InputActionSetupExtensions.BindingSyntax);
					}
					if (partName.Equals(bindingSyntax.binding.name, StringComparison.InvariantCultureIgnoreCase))
					{
						return bindingSyntax;
					}
					bindingSyntax = bindingSyntax.Iterate(next);
				}
				return default(InputActionSetupExtensions.BindingSyntax);
			}

			// Token: 0x06001286 RID: 4742 RVA: 0x00057808 File Offset: 0x00055A08
			public void Erase()
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Instance not valid");
				}
				bool isComposite = this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].isComposite;
				ArrayHelpers.EraseAt<InputBinding>(ref this.m_ActionMap.m_Bindings, this.m_BindingIndexInMap);
				if (isComposite)
				{
					while (this.m_BindingIndexInMap < this.m_ActionMap.m_Bindings.LengthSafe<InputBinding>() && this.m_ActionMap.m_Bindings[this.m_BindingIndexInMap].isPartOfComposite)
					{
						ArrayHelpers.EraseAt<InputBinding>(ref this.m_ActionMap.m_Bindings, this.m_BindingIndexInMap);
					}
				}
				this.m_Action.m_BindingsCount = this.m_ActionMap.m_Bindings.LengthSafe<InputBinding>();
				this.m_ActionMap.OnBindingModified();
				if (this.m_ActionMap.m_SingletonAction != null)
				{
					this.m_ActionMap.m_SingletonAction.m_SingletonActionBindings = this.m_ActionMap.m_Bindings;
				}
			}

			// Token: 0x06001287 RID: 4743 RVA: 0x000578F8 File Offset: 0x00055AF8
			public InputActionSetupExtensions.BindingSyntax InsertPartBinding(string partName, string path)
			{
				if (string.IsNullOrEmpty(partName))
				{
					throw new ArgumentNullException("partName");
				}
				if (!this.valid)
				{
					throw new InvalidOperationException("Binding accessor is not valid");
				}
				InputBinding binding = this.binding;
				if (!binding.isPartOfComposite && !binding.isComposite)
				{
					throw new InvalidOperationException("Binding accessor must point to composite or part binding");
				}
				InputActionSetupExtensions.AddBindingInternal(this.m_ActionMap, new InputBinding
				{
					path = path,
					isPartOfComposite = true,
					name = partName
				}, this.m_BindingIndexInMap + 1);
				return new InputActionSetupExtensions.BindingSyntax(this.m_ActionMap, this.m_BindingIndexInMap + 1, this.m_Action);
			}

			// Token: 0x040007A9 RID: 1961
			private readonly InputActionMap m_ActionMap;

			// Token: 0x040007AA RID: 1962
			private readonly InputAction m_Action;

			// Token: 0x040007AB RID: 1963
			internal readonly int m_BindingIndexInMap;
		}

		// Token: 0x02000169 RID: 361
		public struct CompositeSyntax
		{
			// Token: 0x170004F1 RID: 1265
			// (get) Token: 0x06001288 RID: 4744 RVA: 0x0005799D File Offset: 0x00055B9D
			public int bindingIndex
			{
				get
				{
					if (this.m_ActionMap == null)
					{
						return -1;
					}
					if (this.m_Action != null)
					{
						return this.m_Action.BindingIndexOnMapToBindingIndexOnAction(this.m_BindingIndexInMap);
					}
					return this.m_BindingIndexInMap;
				}
			}

			// Token: 0x06001289 RID: 4745 RVA: 0x000579C9 File Offset: 0x00055BC9
			internal CompositeSyntax(InputActionMap map, InputAction action, int compositeIndex)
			{
				this.m_Action = action;
				this.m_ActionMap = map;
				this.m_BindingIndexInMap = compositeIndex;
			}

			// Token: 0x0600128A RID: 4746 RVA: 0x000579E0 File Offset: 0x00055BE0
			public InputActionSetupExtensions.CompositeSyntax With(string name, string binding, string groups = null, string processors = null)
			{
				using (InputActionRebindingExtensions.DeferBindingResolution())
				{
					int num;
					if (this.m_Action != null)
					{
						num = this.m_Action.AddBinding(binding, null, processors, groups).m_BindingIndexInMap;
					}
					else
					{
						num = this.m_ActionMap.AddBinding(binding, null, groups, null, processors).m_BindingIndexInMap;
					}
					this.m_ActionMap.m_Bindings[num].name = name;
					this.m_ActionMap.m_Bindings[num].isPartOfComposite = true;
				}
				return this;
			}

			// Token: 0x040007AC RID: 1964
			private readonly InputAction m_Action;

			// Token: 0x040007AD RID: 1965
			private readonly InputActionMap m_ActionMap;

			// Token: 0x040007AE RID: 1966
			private int m_BindingIndexInMap;
		}

		// Token: 0x0200016A RID: 362
		public struct ControlSchemeSyntax
		{
			// Token: 0x0600128B RID: 4747 RVA: 0x00057A7C File Offset: 0x00055C7C
			internal ControlSchemeSyntax(InputActionAsset asset, int index)
			{
				this.m_Asset = asset;
				this.m_ControlSchemeIndex = index;
				this.m_ControlScheme = default(InputControlScheme);
			}

			// Token: 0x0600128C RID: 4748 RVA: 0x00057A98 File Offset: 0x00055C98
			internal ControlSchemeSyntax(InputControlScheme controlScheme)
			{
				this.m_Asset = null;
				this.m_ControlSchemeIndex = -1;
				this.m_ControlScheme = controlScheme;
			}

			// Token: 0x0600128D RID: 4749 RVA: 0x00057AB0 File Offset: 0x00055CB0
			public InputActionSetupExtensions.ControlSchemeSyntax WithBindingGroup(string bindingGroup)
			{
				if (string.IsNullOrEmpty(bindingGroup))
				{
					throw new ArgumentNullException("bindingGroup");
				}
				if (this.m_Asset == null)
				{
					this.m_ControlScheme.m_BindingGroup = bindingGroup;
				}
				else
				{
					this.m_Asset.m_ControlSchemes[this.m_ControlSchemeIndex].bindingGroup = bindingGroup;
				}
				return this;
			}

			// Token: 0x0600128E RID: 4750 RVA: 0x00057B0E File Offset: 0x00055D0E
			public InputActionSetupExtensions.ControlSchemeSyntax WithRequiredDevice<TDevice>() where TDevice : InputDevice
			{
				return this.WithRequiredDevice(this.DeviceTypeToControlPath<TDevice>());
			}

			// Token: 0x0600128F RID: 4751 RVA: 0x00057B1C File Offset: 0x00055D1C
			public InputActionSetupExtensions.ControlSchemeSyntax WithOptionalDevice<TDevice>() where TDevice : InputDevice
			{
				return this.WithOptionalDevice(this.DeviceTypeToControlPath<TDevice>());
			}

			// Token: 0x06001290 RID: 4752 RVA: 0x00057B2A File Offset: 0x00055D2A
			public InputActionSetupExtensions.ControlSchemeSyntax OrWithRequiredDevice<TDevice>() where TDevice : InputDevice
			{
				return this.OrWithRequiredDevice(this.DeviceTypeToControlPath<TDevice>());
			}

			// Token: 0x06001291 RID: 4753 RVA: 0x00057B38 File Offset: 0x00055D38
			public InputActionSetupExtensions.ControlSchemeSyntax OrWithOptionalDevice<TDevice>() where TDevice : InputDevice
			{
				return this.OrWithOptionalDevice(this.DeviceTypeToControlPath<TDevice>());
			}

			// Token: 0x06001292 RID: 4754 RVA: 0x00057B46 File Offset: 0x00055D46
			public InputActionSetupExtensions.ControlSchemeSyntax WithRequiredDevice(string controlPath)
			{
				this.AddDeviceEntry(controlPath, InputControlScheme.DeviceRequirement.Flags.None);
				return this;
			}

			// Token: 0x06001293 RID: 4755 RVA: 0x00057B56 File Offset: 0x00055D56
			public InputActionSetupExtensions.ControlSchemeSyntax WithOptionalDevice(string controlPath)
			{
				this.AddDeviceEntry(controlPath, InputControlScheme.DeviceRequirement.Flags.Optional);
				return this;
			}

			// Token: 0x06001294 RID: 4756 RVA: 0x00057B66 File Offset: 0x00055D66
			public InputActionSetupExtensions.ControlSchemeSyntax OrWithRequiredDevice(string controlPath)
			{
				this.AddDeviceEntry(controlPath, InputControlScheme.DeviceRequirement.Flags.Or);
				return this;
			}

			// Token: 0x06001295 RID: 4757 RVA: 0x00057B76 File Offset: 0x00055D76
			public InputActionSetupExtensions.ControlSchemeSyntax OrWithOptionalDevice(string controlPath)
			{
				this.AddDeviceEntry(controlPath, InputControlScheme.DeviceRequirement.Flags.Optional | InputControlScheme.DeviceRequirement.Flags.Or);
				return this;
			}

			// Token: 0x06001296 RID: 4758 RVA: 0x00057B88 File Offset: 0x00055D88
			private string DeviceTypeToControlPath<TDevice>() where TDevice : InputDevice
			{
				string text = InputControlLayout.s_Layouts.TryFindLayoutForType(typeof(TDevice)).ToString();
				if (string.IsNullOrEmpty(text))
				{
					text = typeof(TDevice).Name;
				}
				return "<" + text + ">";
			}

			// Token: 0x06001297 RID: 4759 RVA: 0x00057BE0 File Offset: 0x00055DE0
			public InputControlScheme Done()
			{
				if (this.m_Asset != null)
				{
					return this.m_Asset.m_ControlSchemes[this.m_ControlSchemeIndex];
				}
				return this.m_ControlScheme;
			}

			// Token: 0x06001298 RID: 4760 RVA: 0x00057C10 File Offset: 0x00055E10
			private void AddDeviceEntry(string controlPath, InputControlScheme.DeviceRequirement.Flags flags)
			{
				if (string.IsNullOrEmpty(controlPath))
				{
					throw new ArgumentNullException("controlPath");
				}
				InputControlScheme inputControlScheme = ((this.m_Asset != null) ? this.m_Asset.m_ControlSchemes[this.m_ControlSchemeIndex] : this.m_ControlScheme);
				ArrayHelpers.Append<InputControlScheme.DeviceRequirement>(ref inputControlScheme.m_DeviceRequirements, new InputControlScheme.DeviceRequirement
				{
					m_ControlPath = controlPath,
					m_Flags = flags
				});
				if (this.m_Asset == null)
				{
					this.m_ControlScheme = inputControlScheme;
					return;
				}
				this.m_Asset.m_ControlSchemes[this.m_ControlSchemeIndex] = inputControlScheme;
			}

			// Token: 0x040007AF RID: 1967
			private readonly InputActionAsset m_Asset;

			// Token: 0x040007B0 RID: 1968
			private readonly int m_ControlSchemeIndex;

			// Token: 0x040007B1 RID: 1969
			private InputControlScheme m_ControlScheme;
		}
	}
}
