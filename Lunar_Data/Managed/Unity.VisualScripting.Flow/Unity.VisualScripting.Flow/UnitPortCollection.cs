using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Unity.VisualScripting
{
	// Token: 0x02000173 RID: 371
	public sealed class UnitPortCollection<TPort> : KeyedCollection<string, TPort>, IUnitPortCollection<TPort>, IKeyedCollection<string, TPort>, ICollection<TPort>, IEnumerable<TPort>, IEnumerable where TPort : IUnitPort
	{
		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x0001111A File Offset: 0x0000F31A
		public IUnit unit { get; }

		// Token: 0x06000996 RID: 2454 RVA: 0x00011122 File Offset: 0x0000F322
		public UnitPortCollection(IUnit unit)
		{
			this.unit = unit;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00011134 File Offset: 0x0000F334
		private void BeforeAdd(TPort port)
		{
			if (port.unit == null)
			{
				port.unit = this.unit;
				return;
			}
			if (port.unit == this.unit)
			{
				throw new InvalidOperationException("Node ports cannot be added multiple time to the same unit.");
			}
			throw new InvalidOperationException("Node ports cannot be shared across nodes.");
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0001118E File Offset: 0x0000F38E
		private void AfterAdd(TPort port)
		{
			this.unit.PortsChanged();
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0001119B File Offset: 0x0000F39B
		private void BeforeRemove(TPort port)
		{
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0001119D File Offset: 0x0000F39D
		private void AfterRemove(TPort port)
		{
			port.unit = null;
			this.unit.PortsChanged();
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x000111B8 File Offset: 0x0000F3B8
		public TPort Single()
		{
			if (base.Count != 0)
			{
				throw new InvalidOperationException("Port collection does not have a single port.");
			}
			return base[0];
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x000111D4 File Offset: 0x0000F3D4
		protected override string GetKeyForItem(TPort item)
		{
			return item.key;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x000111E3 File Offset: 0x0000F3E3
		public new bool TryGetValue(string key, out TPort value)
		{
			if (base.Dictionary == null)
			{
				value = default(TPort);
				return false;
			}
			return base.Dictionary.TryGetValue(key, out value);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00011203 File Offset: 0x0000F403
		protected override void InsertItem(int index, TPort item)
		{
			this.BeforeAdd(item);
			base.InsertItem(index, item);
			this.AfterAdd(item);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0001121C File Offset: 0x0000F41C
		protected override void RemoveItem(int index)
		{
			TPort tport = base[index];
			this.BeforeRemove(tport);
			base.RemoveItem(index);
			this.AfterRemove(tport);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00011246 File Offset: 0x0000F446
		protected override void SetItem(int index, TPort item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0001124D File Offset: 0x0000F44D
		protected override void ClearItems()
		{
			while (base.Count > 0)
			{
				this.RemoveItem(0);
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00011261 File Offset: 0x0000F461
		TPort IKeyedCollection<string, TPort>.get_Item(string key)
		{
			return base[key];
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0001126A File Offset: 0x0000F46A
		bool IKeyedCollection<string, TPort>.Contains(string key)
		{
			return base.Contains(key);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00011273 File Offset: 0x0000F473
		bool IKeyedCollection<string, TPort>.Remove(string key)
		{
			return base.Remove(key);
		}
	}
}
