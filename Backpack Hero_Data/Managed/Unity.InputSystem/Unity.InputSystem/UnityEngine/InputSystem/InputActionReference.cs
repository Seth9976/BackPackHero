using System;
using System.Linq;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000021 RID: 33
	public class InputActionReference : ScriptableObject
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00006E90 File Offset: 0x00005090
		public InputActionAsset asset
		{
			get
			{
				return this.m_Asset;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00006E98 File Offset: 0x00005098
		public InputAction action
		{
			get
			{
				if (this.m_Action == null)
				{
					if (this.m_Asset == null)
					{
						return null;
					}
					this.m_Action = this.m_Asset.FindAction(new Guid(this.m_ActionId));
				}
				return this.m_Action;
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006ED4 File Offset: 0x000050D4
		public void Set(InputAction action)
		{
			if (action == null)
			{
				this.m_Asset = null;
				this.m_ActionId = null;
				return;
			}
			InputActionMap actionMap = action.actionMap;
			if (actionMap == null || actionMap.asset == null)
			{
				throw new InvalidOperationException(string.Format("Action '{0}' must be part of an InputActionAsset in order to be able to create an InputActionReference for it", action));
			}
			this.SetInternal(actionMap.asset, action);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006F2C File Offset: 0x0000512C
		public void Set(InputActionAsset asset, string mapName, string actionName)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(mapName))
			{
				throw new ArgumentNullException("mapName");
			}
			if (string.IsNullOrEmpty(actionName))
			{
				throw new ArgumentNullException("actionName");
			}
			InputActionMap inputActionMap = asset.FindActionMap(mapName, false);
			if (inputActionMap == null)
			{
				throw new ArgumentException(string.Format("No action map '{0}' in '{1}'", mapName, asset), "mapName");
			}
			InputAction inputAction = inputActionMap.FindAction(actionName, false);
			if (inputAction == null)
			{
				throw new ArgumentException(string.Format("No action '{0}' in map '{1}' of asset '{2}'", actionName, mapName, asset), "actionName");
			}
			this.SetInternal(asset, inputAction);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00006FC0 File Offset: 0x000051C0
		private void SetInternal(InputActionAsset asset, InputAction action)
		{
			InputActionMap actionMap = action.actionMap;
			if (!asset.actionMaps.Contains(actionMap))
			{
				throw new ArgumentException(string.Format("Action '{0}' is not contained in asset '{1}'", action, asset), "action");
			}
			this.m_Asset = asset;
			this.m_ActionId = action.id.ToString();
			base.name = InputActionReference.GetDisplayName(action);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000702C File Offset: 0x0000522C
		public override string ToString()
		{
			try
			{
				InputAction action = this.action;
				return string.Concat(new string[]
				{
					this.m_Asset.name,
					":",
					action.actionMap.name,
					"/",
					action.name
				});
			}
			catch
			{
				if (this.m_Asset != null)
				{
					return this.m_Asset.name + ":" + this.m_ActionId;
				}
			}
			return base.ToString();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000070CC File Offset: 0x000052CC
		private static string GetDisplayName(InputAction action)
		{
			string text;
			if (action == null)
			{
				text = null;
			}
			else
			{
				InputActionMap actionMap = action.actionMap;
				text = ((actionMap != null) ? actionMap.name : null);
			}
			if (!string.IsNullOrEmpty(text))
			{
				InputActionMap actionMap2 = action.actionMap;
				return ((actionMap2 != null) ? actionMap2.name : null) + "/" + action.name;
			}
			if (action == null)
			{
				return null;
			}
			return action.name;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00007126 File Offset: 0x00005326
		internal string ToDisplayName()
		{
			if (!string.IsNullOrEmpty(base.name))
			{
				return base.name;
			}
			return InputActionReference.GetDisplayName(this.action);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00007147 File Offset: 0x00005347
		public static implicit operator InputAction(InputActionReference reference)
		{
			if (reference == null)
			{
				return null;
			}
			return reference.action;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00007154 File Offset: 0x00005354
		public static InputActionReference Create(InputAction action)
		{
			if (action == null)
			{
				return null;
			}
			InputActionReference inputActionReference = ScriptableObject.CreateInstance<InputActionReference>();
			inputActionReference.Set(action);
			return inputActionReference;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00007167 File Offset: 0x00005367
		public InputAction ToInputAction()
		{
			return this.action;
		}

		// Token: 0x040000C1 RID: 193
		[SerializeField]
		internal InputActionAsset m_Asset;

		// Token: 0x040000C2 RID: 194
		[SerializeField]
		internal string m_ActionId;

		// Token: 0x040000C3 RID: 195
		[NonSerialized]
		private InputAction m_Action;
	}
}
