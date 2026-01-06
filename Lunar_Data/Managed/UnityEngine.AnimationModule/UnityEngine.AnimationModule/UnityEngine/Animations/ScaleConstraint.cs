using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000068 RID: 104
	[UsedByNativeCode]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Animation/Constraints/ScaleConstraint.h")]
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	public sealed class ScaleConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		// Token: 0x060005CA RID: 1482 RVA: 0x00007EB1 File Offset: 0x000060B1
		private ScaleConstraint()
		{
			ScaleConstraint.Internal_Create(this);
		}

		// Token: 0x060005CB RID: 1483
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] ScaleConstraint self);

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060005CC RID: 1484
		// (set) Token: 0x060005CD RID: 1485
		public extern float weight
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x00007EC4 File Offset: 0x000060C4
		// (set) Token: 0x060005CF RID: 1487 RVA: 0x00007EDA File Offset: 0x000060DA
		public Vector3 scaleAtRest
		{
			get
			{
				Vector3 vector;
				this.get_scaleAtRest_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_scaleAtRest_Injected(ref value);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x00007EE4 File Offset: 0x000060E4
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x00007EFA File Offset: 0x000060FA
		public Vector3 scaleOffset
		{
			get
			{
				Vector3 vector;
				this.get_scaleOffset_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_scaleOffset_Injected(ref value);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060005D2 RID: 1490
		// (set) Token: 0x060005D3 RID: 1491
		public extern Axis scalingAxis
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060005D4 RID: 1492
		// (set) Token: 0x060005D5 RID: 1493
		public extern bool constraintActive
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060005D6 RID: 1494
		// (set) Token: 0x060005D7 RID: 1495
		public extern bool locked
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00007F04 File Offset: 0x00006104
		public int sourceCount
		{
			get
			{
				return ScaleConstraint.GetSourceCountInternal(this);
			}
		}

		// Token: 0x060005D9 RID: 1497
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		[MethodImpl(4096)]
		private static extern int GetSourceCountInternal([NotNull("ArgumentNullException")] ScaleConstraint self);

		// Token: 0x060005DA RID: 1498
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void GetSources([NotNull("ArgumentNullException")] List<ConstraintSource> sources);

		// Token: 0x060005DB RID: 1499 RVA: 0x00007F1C File Offset: 0x0000611C
		public void SetSources(List<ConstraintSource> sources)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			ScaleConstraint.SetSourcesInternal(this, sources);
		}

		// Token: 0x060005DC RID: 1500
		[FreeFunction("ConstraintBindings::SetSources", ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void SetSourcesInternal([NotNull("ArgumentNullException")] ScaleConstraint self, List<ConstraintSource> sources);

		// Token: 0x060005DD RID: 1501 RVA: 0x00007F45 File Offset: 0x00006145
		public int AddSource(ConstraintSource source)
		{
			return this.AddSource_Injected(ref source);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00007F4F File Offset: 0x0000614F
		public void RemoveSource(int index)
		{
			this.ValidateSourceIndex(index);
			this.RemoveSourceInternal(index);
		}

		// Token: 0x060005DF RID: 1503
		[NativeName("RemoveSource")]
		[MethodImpl(4096)]
		private extern void RemoveSourceInternal(int index);

		// Token: 0x060005E0 RID: 1504 RVA: 0x00007F64 File Offset: 0x00006164
		public ConstraintSource GetSource(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetSourceInternal(index);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00007F88 File Offset: 0x00006188
		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			ConstraintSource constraintSource;
			this.GetSourceInternal_Injected(index, out constraintSource);
			return constraintSource;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00007F9F File Offset: 0x0000619F
		public void SetSource(int index, ConstraintSource source)
		{
			this.ValidateSourceIndex(index);
			this.SetSourceInternal(index, source);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00007FB3 File Offset: 0x000061B3
		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			this.SetSourceInternal_Injected(index, ref source);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00007FC0 File Offset: 0x000061C0
		private void ValidateSourceIndex(int index)
		{
			bool flag = this.sourceCount == 0;
			if (flag)
			{
				throw new InvalidOperationException("The ScaleConstraint component has no sources.");
			}
			bool flag2 = index < 0 || index >= this.sourceCount;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Constraint source index {0} is out of bounds (0-{1}).", index, this.sourceCount));
			}
		}

		// Token: 0x060005E5 RID: 1509
		[MethodImpl(4096)]
		private extern void get_scaleAtRest_Injected(out Vector3 ret);

		// Token: 0x060005E6 RID: 1510
		[MethodImpl(4096)]
		private extern void set_scaleAtRest_Injected(ref Vector3 value);

		// Token: 0x060005E7 RID: 1511
		[MethodImpl(4096)]
		private extern void get_scaleOffset_Injected(out Vector3 ret);

		// Token: 0x060005E8 RID: 1512
		[MethodImpl(4096)]
		private extern void set_scaleOffset_Injected(ref Vector3 value);

		// Token: 0x060005E9 RID: 1513
		[MethodImpl(4096)]
		private extern int AddSource_Injected(ref ConstraintSource source);

		// Token: 0x060005EA RID: 1514
		[MethodImpl(4096)]
		private extern void GetSourceInternal_Injected(int index, out ConstraintSource ret);

		// Token: 0x060005EB RID: 1515
		[MethodImpl(4096)]
		private extern void SetSourceInternal_Injected(int index, ref ConstraintSource source);
	}
}
