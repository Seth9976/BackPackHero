using System;
using System.Linq;
using CleverCrow.Fluid.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace CleverCrow.Fluid.Databases
{
	// Token: 0x0200000A RID: 10
	public class GlobalBoolMonitor : MonoBehaviour
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000021C0 File Offset: 0x000003C0
		private void Start()
		{
			IKeyValueDefinition<bool>[] array = this._booleans.Select(new Func<KeyValueDefinitionBool, KeyValueDefinitionBool>(Object.Instantiate<KeyValueDefinitionBool>)).ToArray<IKeyValueDefinition<bool>>();
			this._internal = new GlobalBoolMonitorInternal(Singleton<GlobalDatabaseManager>.Instance.Database, array);
			this._internal.EventTrue.AddListener(new UnityAction(this._eventTrue.Invoke));
			this._internal.EventFalse.AddListener(new UnityAction(this._eventFalse.Invoke));
			this._internal.UpdateEvent();
			this._internal.BindChangeMonitor();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002258 File Offset: 0x00000458
		private void OnDestroy()
		{
			if (!Singleton<GlobalDatabaseManager>.IsInstance)
			{
				return;
			}
			GlobalBoolMonitorInternal @internal = this._internal;
			if (@internal == null)
			{
				return;
			}
			@internal.UnbindChangeMonitor();
		}

		// Token: 0x04000008 RID: 8
		private GlobalBoolMonitorInternal _internal;

		// Token: 0x04000009 RID: 9
		[SerializeField]
		private KeyValueDefinitionBool[] _booleans = new KeyValueDefinitionBool[1];

		// Token: 0x0400000A RID: 10
		[SerializeField]
		private UnityEvent _eventTrue = new UnityEvent();

		// Token: 0x0400000B RID: 11
		[SerializeField]
		private UnityEvent _eventFalse = new UnityEvent();
	}
}
