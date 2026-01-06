using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x0200006B RID: 107
	[RequireComponent(typeof(Transform))]
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/Constraints/ParentConstraint.h")]
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	public sealed class ParentConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x000082CF File Offset: 0x000064CF
		private ParentConstraint()
		{
			ParentConstraint.Internal_Create(this);
		}

		// Token: 0x06000622 RID: 1570
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] ParentConstraint self);

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000623 RID: 1571
		// (set) Token: 0x06000624 RID: 1572
		public extern float weight
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000625 RID: 1573
		// (set) Token: 0x06000626 RID: 1574
		public extern bool constraintActive
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000627 RID: 1575
		// (set) Token: 0x06000628 RID: 1576
		public extern bool locked
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x000082E0 File Offset: 0x000064E0
		public int sourceCount
		{
			get
			{
				return ParentConstraint.GetSourceCountInternal(this);
			}
		}

		// Token: 0x0600062A RID: 1578
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		[MethodImpl(4096)]
		private static extern int GetSourceCountInternal([NotNull("ArgumentNullException")] ParentConstraint self);

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x000082F8 File Offset: 0x000064F8
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x0000830E File Offset: 0x0000650E
		public Vector3 translationAtRest
		{
			get
			{
				Vector3 vector;
				this.get_translationAtRest_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_translationAtRest_Injected(ref value);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00008318 File Offset: 0x00006518
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x0000832E File Offset: 0x0000652E
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

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600062F RID: 1583
		// (set) Token: 0x06000630 RID: 1584
		public extern Vector3[] translationOffsets
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000631 RID: 1585
		// (set) Token: 0x06000632 RID: 1586
		public extern Vector3[] rotationOffsets
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000633 RID: 1587
		// (set) Token: 0x06000634 RID: 1588
		public extern Axis translationAxis
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000635 RID: 1589
		// (set) Token: 0x06000636 RID: 1590
		public extern Axis rotationAxis
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00008338 File Offset: 0x00006538
		public Vector3 GetTranslationOffset(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetTranslationOffsetInternal(index);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00008359 File Offset: 0x00006559
		public void SetTranslationOffset(int index, Vector3 value)
		{
			this.ValidateSourceIndex(index);
			this.SetTranslationOffsetInternal(index, value);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00008370 File Offset: 0x00006570
		[NativeName("GetTranslationOffset")]
		private Vector3 GetTranslationOffsetInternal(int index)
		{
			Vector3 vector;
			this.GetTranslationOffsetInternal_Injected(index, out vector);
			return vector;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00008387 File Offset: 0x00006587
		[NativeName("SetTranslationOffset")]
		private void SetTranslationOffsetInternal(int index, Vector3 value)
		{
			this.SetTranslationOffsetInternal_Injected(index, ref value);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00008394 File Offset: 0x00006594
		public Vector3 GetRotationOffset(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetRotationOffsetInternal(index);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x000083B5 File Offset: 0x000065B5
		public void SetRotationOffset(int index, Vector3 value)
		{
			this.ValidateSourceIndex(index);
			this.SetRotationOffsetInternal(index, value);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x000083CC File Offset: 0x000065CC
		[NativeName("GetRotationOffset")]
		private Vector3 GetRotationOffsetInternal(int index)
		{
			Vector3 vector;
			this.GetRotationOffsetInternal_Injected(index, out vector);
			return vector;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x000083E3 File Offset: 0x000065E3
		[NativeName("SetRotationOffset")]
		private void SetRotationOffsetInternal(int index, Vector3 value)
		{
			this.SetRotationOffsetInternal_Injected(index, ref value);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x000083F0 File Offset: 0x000065F0
		private void ValidateSourceIndex(int index)
		{
			bool flag = this.sourceCount == 0;
			if (flag)
			{
				throw new InvalidOperationException("The ParentConstraint component has no sources.");
			}
			bool flag2 = index < 0 || index >= this.sourceCount;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Constraint source index {0} is out of bounds (0-{1}).", index, this.sourceCount));
			}
		}

		// Token: 0x06000640 RID: 1600
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void GetSources([NotNull("ArgumentNullException")] List<ConstraintSource> sources);

		// Token: 0x06000641 RID: 1601 RVA: 0x00008458 File Offset: 0x00006658
		public void SetSources(List<ConstraintSource> sources)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			ParentConstraint.SetSourcesInternal(this, sources);
		}

		// Token: 0x06000642 RID: 1602
		[FreeFunction("ConstraintBindings::SetSources", ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void SetSourcesInternal([NotNull("ArgumentNullException")] ParentConstraint self, List<ConstraintSource> sources);

		// Token: 0x06000643 RID: 1603 RVA: 0x00008481 File Offset: 0x00006681
		public int AddSource(ConstraintSource source)
		{
			return this.AddSource_Injected(ref source);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0000848B File Offset: 0x0000668B
		public void RemoveSource(int index)
		{
			this.ValidateSourceIndex(index);
			this.RemoveSourceInternal(index);
		}

		// Token: 0x06000645 RID: 1605
		[NativeName("RemoveSource")]
		[MethodImpl(4096)]
		private extern void RemoveSourceInternal(int index);

		// Token: 0x06000646 RID: 1606 RVA: 0x000084A0 File Offset: 0x000066A0
		public ConstraintSource GetSource(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetSourceInternal(index);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x000084C4 File Offset: 0x000066C4
		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			ConstraintSource constraintSource;
			this.GetSourceInternal_Injected(index, out constraintSource);
			return constraintSource;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x000084DB File Offset: 0x000066DB
		public void SetSource(int index, ConstraintSource source)
		{
			this.ValidateSourceIndex(index);
			this.SetSourceInternal(index, source);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000084EF File Offset: 0x000066EF
		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			this.SetSourceInternal_Injected(index, ref source);
		}

		// Token: 0x0600064A RID: 1610
		[MethodImpl(4096)]
		private extern void get_translationAtRest_Injected(out Vector3 ret);

		// Token: 0x0600064B RID: 1611
		[MethodImpl(4096)]
		private extern void set_translationAtRest_Injected(ref Vector3 value);

		// Token: 0x0600064C RID: 1612
		[MethodImpl(4096)]
		private extern void get_rotationAtRest_Injected(out Vector3 ret);

		// Token: 0x0600064D RID: 1613
		[MethodImpl(4096)]
		private extern void set_rotationAtRest_Injected(ref Vector3 value);

		// Token: 0x0600064E RID: 1614
		[MethodImpl(4096)]
		private extern void GetTranslationOffsetInternal_Injected(int index, out Vector3 ret);

		// Token: 0x0600064F RID: 1615
		[MethodImpl(4096)]
		private extern void SetTranslationOffsetInternal_Injected(int index, ref Vector3 value);

		// Token: 0x06000650 RID: 1616
		[MethodImpl(4096)]
		private extern void GetRotationOffsetInternal_Injected(int index, out Vector3 ret);

		// Token: 0x06000651 RID: 1617
		[MethodImpl(4096)]
		private extern void SetRotationOffsetInternal_Injected(int index, ref Vector3 value);

		// Token: 0x06000652 RID: 1618
		[MethodImpl(4096)]
		private extern int AddSource_Injected(ref ConstraintSource source);

		// Token: 0x06000653 RID: 1619
		[MethodImpl(4096)]
		private extern void GetSourceInternal_Injected(int index, out ConstraintSource ret);

		// Token: 0x06000654 RID: 1620
		[MethodImpl(4096)]
		private extern void SetSourceInternal_Injected(int index, ref ConstraintSource source);
	}
}
