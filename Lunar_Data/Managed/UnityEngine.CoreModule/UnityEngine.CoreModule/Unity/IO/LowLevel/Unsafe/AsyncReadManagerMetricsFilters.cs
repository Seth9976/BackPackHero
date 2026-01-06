using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x02000084 RID: 132
	[NativeConditional("ENABLE_PROFILER")]
	[NativeAsStruct]
	[RequiredByNativeCode]
	[StructLayout(0)]
	public class AsyncReadManagerMetricsFilters
	{
		// Token: 0x06000231 RID: 561 RVA: 0x00004159 File Offset: 0x00002359
		public AsyncReadManagerMetricsFilters()
		{
			this.ClearFilters();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000416A File Offset: 0x0000236A
		public AsyncReadManagerMetricsFilters(ulong typeID)
		{
			this.ClearFilters();
			this.SetTypeIDFilter(typeID);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00004183 File Offset: 0x00002383
		public AsyncReadManagerMetricsFilters(ProcessingState state)
		{
			this.ClearFilters();
			this.SetStateFilter(state);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000419C File Offset: 0x0000239C
		public AsyncReadManagerMetricsFilters(FileReadType readType)
		{
			this.ClearFilters();
			this.SetReadTypeFilter(readType);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000041B5 File Offset: 0x000023B5
		public AsyncReadManagerMetricsFilters(Priority priorityLevel)
		{
			this.ClearFilters();
			this.SetPriorityFilter(priorityLevel);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000041CE File Offset: 0x000023CE
		public AsyncReadManagerMetricsFilters(AssetLoadingSubsystem subsystem)
		{
			this.ClearFilters();
			this.SetSubsystemFilter(subsystem);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000041E7 File Offset: 0x000023E7
		public AsyncReadManagerMetricsFilters(ulong[] typeIDs)
		{
			this.ClearFilters();
			this.SetTypeIDFilter(typeIDs);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00004200 File Offset: 0x00002400
		public AsyncReadManagerMetricsFilters(ProcessingState[] states)
		{
			this.ClearFilters();
			this.SetStateFilter(states);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00004219 File Offset: 0x00002419
		public AsyncReadManagerMetricsFilters(FileReadType[] readTypes)
		{
			this.ClearFilters();
			this.SetReadTypeFilter(readTypes);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00004232 File Offset: 0x00002432
		public AsyncReadManagerMetricsFilters(Priority[] priorityLevels)
		{
			this.ClearFilters();
			this.SetPriorityFilter(priorityLevels);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000424B File Offset: 0x0000244B
		public AsyncReadManagerMetricsFilters(AssetLoadingSubsystem[] subsystems)
		{
			this.ClearFilters();
			this.SetSubsystemFilter(subsystems);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00004264 File Offset: 0x00002464
		public AsyncReadManagerMetricsFilters(ulong[] typeIDs, ProcessingState[] states, FileReadType[] readTypes, Priority[] priorityLevels, AssetLoadingSubsystem[] subsystems)
		{
			this.ClearFilters();
			this.SetTypeIDFilter(typeIDs);
			this.SetStateFilter(states);
			this.SetReadTypeFilter(readTypes);
			this.SetPriorityFilter(priorityLevels);
			this.SetSubsystemFilter(subsystems);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000429F File Offset: 0x0000249F
		public void SetTypeIDFilter(ulong[] _typeIDs)
		{
			this.TypeIDs = _typeIDs;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000042A9 File Offset: 0x000024A9
		public void SetStateFilter(ProcessingState[] _states)
		{
			this.States = _states;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000042B3 File Offset: 0x000024B3
		public void SetReadTypeFilter(FileReadType[] _readTypes)
		{
			this.ReadTypes = _readTypes;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000042BD File Offset: 0x000024BD
		public void SetPriorityFilter(Priority[] _priorityLevels)
		{
			this.PriorityLevels = _priorityLevels;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000042C7 File Offset: 0x000024C7
		public void SetSubsystemFilter(AssetLoadingSubsystem[] _subsystems)
		{
			this.Subsystems = _subsystems;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000042D1 File Offset: 0x000024D1
		public void SetTypeIDFilter(ulong _typeID)
		{
			this.TypeIDs = new ulong[] { _typeID };
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000042E4 File Offset: 0x000024E4
		public void SetStateFilter(ProcessingState _state)
		{
			this.States = new ProcessingState[] { _state };
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000042F7 File Offset: 0x000024F7
		public void SetReadTypeFilter(FileReadType _readType)
		{
			this.ReadTypes = new FileReadType[] { _readType };
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000430A File Offset: 0x0000250A
		public void SetPriorityFilter(Priority _priorityLevel)
		{
			this.PriorityLevels = new Priority[] { _priorityLevel };
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000431D File Offset: 0x0000251D
		public void SetSubsystemFilter(AssetLoadingSubsystem _subsystem)
		{
			this.Subsystems = new AssetLoadingSubsystem[] { _subsystem };
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00004330 File Offset: 0x00002530
		public void RemoveTypeIDFilter()
		{
			this.TypeIDs = null;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000433A File Offset: 0x0000253A
		public void RemoveStateFilter()
		{
			this.States = null;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00004344 File Offset: 0x00002544
		public void RemoveReadTypeFilter()
		{
			this.ReadTypes = null;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000434E File Offset: 0x0000254E
		public void RemovePriorityFilter()
		{
			this.PriorityLevels = null;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00004358 File Offset: 0x00002558
		public void RemoveSubsystemFilter()
		{
			this.Subsystems = null;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00004362 File Offset: 0x00002562
		public void ClearFilters()
		{
			this.RemoveTypeIDFilter();
			this.RemoveStateFilter();
			this.RemoveReadTypeFilter();
			this.RemovePriorityFilter();
			this.RemoveSubsystemFilter();
		}

		// Token: 0x04000204 RID: 516
		[NativeName("typeIDs")]
		internal ulong[] TypeIDs;

		// Token: 0x04000205 RID: 517
		[NativeName("states")]
		internal ProcessingState[] States;

		// Token: 0x04000206 RID: 518
		[NativeName("readTypes")]
		internal FileReadType[] ReadTypes;

		// Token: 0x04000207 RID: 519
		[NativeName("priorityLevels")]
		internal Priority[] PriorityLevels;

		// Token: 0x04000208 RID: 520
		[NativeName("subsystems")]
		internal AssetLoadingSubsystem[] Subsystems;
	}
}
