using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000069 RID: 105
	[UsedByNativeCode]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Animation/Constraints/LookAtConstraint.h")]
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	public sealed class LookAtConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		// Token: 0x060005EC RID: 1516 RVA: 0x00008025 File Offset: 0x00006225
		private LookAtConstraint()
		{
			LookAtConstraint.Internal_Create(this);
		}

		// Token: 0x060005ED RID: 1517
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] LookAtConstraint self);

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060005EE RID: 1518
		// (set) Token: 0x060005EF RID: 1519
		public extern float weight
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060005F0 RID: 1520
		// (set) Token: 0x060005F1 RID: 1521
		public extern float roll
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060005F2 RID: 1522
		// (set) Token: 0x060005F3 RID: 1523
		public extern bool constraintActive
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060005F4 RID: 1524
		// (set) Token: 0x060005F5 RID: 1525
		public extern bool locked
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00008038 File Offset: 0x00006238
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x0000804E File Offset: 0x0000624E
		public Vector3 rotationAtRest
		{
			get
			{
				Vector3 vector;
				this.get_rotationAtRest_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_rotationAtRest_Injected(ref value);
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00008058 File Offset: 0x00006258
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x0000806E File Offset: 0x0000626E
		public Vector3 rotationOffset
		{
			get
			{
				Vector3 vector;
				this.get_rotationOffset_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_rotationOffset_Injected(ref value);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060005FA RID: 1530
		// (set) Token: 0x060005FB RID: 1531
		public extern Transform worldUpObject
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060005FC RID: 1532
		// (set) Token: 0x060005FD RID: 1533
		public extern bool useUpObject
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x00008078 File Offset: 0x00006278
		public int sourceCount
		{
			get
			{
				return LookAtConstraint.GetSourceCountInternal(this);
			}
		}

		// Token: 0x060005FF RID: 1535
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		[MethodImpl(4096)]
		private static extern int GetSourceCountInternal([NotNull("ArgumentNullException")] LookAtConstraint self);

		// Token: 0x06000600 RID: 1536
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void GetSources([NotNull("ArgumentNullException")] List<ConstraintSource> sources);

		// Token: 0x06000601 RID: 1537 RVA: 0x00008090 File Offset: 0x00006290
		public void SetSources(List<ConstraintSource> sources)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			LookAtConstraint.SetSourcesInternal(this, sources);
		}

		// Token: 0x06000602 RID: 1538
		[FreeFunction("ConstraintBindings::SetSources", ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void SetSourcesInternal([NotNull("ArgumentNullException")] LookAtConstraint self, List<ConstraintSource> sources);

		// Token: 0x06000603 RID: 1539 RVA: 0x000080B9 File Offset: 0x000062B9
		public int AddSource(ConstraintSource source)
		{
			return this.AddSource_Injected(ref source);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000080C3 File Offset: 0x000062C3
		public void RemoveSource(int index)
		{
			this.ValidateSourceIndex(index);
			this.RemoveSourceInternal(index);
		}

		// Token: 0x06000605 RID: 1541
		[NativeName("RemoveSource")]
		[MethodImpl(4096)]
		private extern void RemoveSourceInternal(int index);

		// Token: 0x06000606 RID: 1542 RVA: 0x000080D8 File Offset: 0x000062D8
		public ConstraintSource GetSource(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetSourceInternal(index);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x000080FC File Offset: 0x000062FC
		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			ConstraintSource constraintSource;
			this.GetSourceInternal_Injected(index, out constraintSource);
			return constraintSource;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00008113 File Offset: 0x00006313
		public void SetSource(int index, ConstraintSource source)
		{
			this.ValidateSourceIndex(index);
			this.SetSourceInternal(index, source);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00008127 File Offset: 0x00006327
		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			this.SetSourceInternal_Injected(index, ref source);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00008134 File Offset: 0x00006334
		private void ValidateSourceIndex(int index)
		{
			bool flag = this.sourceCount == 0;
			if (flag)
			{
				throw new InvalidOperationException("The LookAtConstraint component has no sources.");
			}
			bool flag2 = index < 0 || index >= this.sourceCount;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Constraint source index {0} is out of bounds (0-{1}).", index, this.sourceCount));
			}
		}

		// Token: 0x0600060B RID: 1547
		[MethodImpl(4096)]
		private extern void get_rotationAtRest_Injected(out Vector3 ret);

		// Token: 0x0600060C RID: 1548
		[MethodImpl(4096)]
		private extern void set_rotationAtRest_Injected(ref Vector3 value);

		// Token: 0x0600060D RID: 1549
		[MethodImpl(4096)]
		private extern void get_rotationOffset_Injected(out Vector3 ret);

		// Token: 0x0600060E RID: 1550
		[MethodImpl(4096)]
		private extern void set_rotationOffset_Injected(ref Vector3 value);

		// Token: 0x0600060F RID: 1551
		[MethodImpl(4096)]
		private extern int AddSource_Injected(ref ConstraintSource source);

		// Token: 0x06000610 RID: 1552
		[MethodImpl(4096)]
		private extern void GetSourceInternal_Injected(int index, out ConstraintSource ret);

		// Token: 0x06000611 RID: 1553
		[MethodImpl(4096)]
		private extern void SetSourceInternal_Injected(int index, ref ConstraintSource source);
	}
}
