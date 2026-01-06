using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x02000662 RID: 1634
	internal sealed class ObjectHolder
	{
		// Token: 0x06003D10 RID: 15632 RVA: 0x000D3D32 File Offset: 0x000D1F32
		internal ObjectHolder(long objID)
			: this(null, objID, null, null, 0L, null, null)
		{
		}

		// Token: 0x06003D11 RID: 15633 RVA: 0x000D3D44 File Offset: 0x000D1F44
		internal ObjectHolder(object obj, long objID, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainingObj, FieldInfo field, int[] arrayIndex)
		{
			this.m_object = obj;
			this.m_id = objID;
			this.m_flags = 0;
			this.m_missingElementsRemaining = 0;
			this.m_missingDecendents = 0;
			this.m_dependentObjects = null;
			this.m_next = null;
			this.m_serInfo = info;
			this.m_surrogate = surrogate;
			this.m_markForFixupWhenAvailable = false;
			if (obj is TypeLoadExceptionHolder)
			{
				this.m_typeLoad = (TypeLoadExceptionHolder)obj;
			}
			if (idOfContainingObj != 0L && ((field != null && field.FieldType.IsValueType) || arrayIndex != null))
			{
				if (idOfContainingObj == objID)
				{
					throw new SerializationException(Environment.GetResourceString("The ID of the containing object cannot be the same as the object ID."));
				}
				this.m_valueFixup = new ValueTypeFixupInfo(idOfContainingObj, field, arrayIndex);
			}
			this.SetFlags();
		}

		// Token: 0x06003D12 RID: 15634 RVA: 0x000D3E00 File Offset: 0x000D2000
		internal ObjectHolder(string obj, long objID, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainingObj, FieldInfo field, int[] arrayIndex)
		{
			this.m_object = obj;
			this.m_id = objID;
			this.m_flags = 0;
			this.m_missingElementsRemaining = 0;
			this.m_missingDecendents = 0;
			this.m_dependentObjects = null;
			this.m_next = null;
			this.m_serInfo = info;
			this.m_surrogate = surrogate;
			this.m_markForFixupWhenAvailable = false;
			if (idOfContainingObj != 0L && arrayIndex != null)
			{
				this.m_valueFixup = new ValueTypeFixupInfo(idOfContainingObj, field, arrayIndex);
			}
			if (this.m_valueFixup != null)
			{
				this.m_flags |= 8;
			}
		}

		// Token: 0x06003D13 RID: 15635 RVA: 0x000D3E89 File Offset: 0x000D2089
		private void IncrementDescendentFixups(int amount)
		{
			this.m_missingDecendents += amount;
		}

		// Token: 0x06003D14 RID: 15636 RVA: 0x000D3E99 File Offset: 0x000D2099
		internal void DecrementFixupsRemaining(ObjectManager manager)
		{
			this.m_missingElementsRemaining--;
			if (this.RequiresValueTypeFixup)
			{
				this.UpdateDescendentDependencyChain(-1, manager);
			}
		}

		// Token: 0x06003D15 RID: 15637 RVA: 0x000D3EB9 File Offset: 0x000D20B9
		internal void RemoveDependency(long id)
		{
			this.m_dependentObjects.RemoveElement(id);
		}

		// Token: 0x06003D16 RID: 15638 RVA: 0x000D3EC8 File Offset: 0x000D20C8
		internal void AddFixup(FixupHolder fixup, ObjectManager manager)
		{
			if (this.m_missingElements == null)
			{
				this.m_missingElements = new FixupHolderList();
			}
			this.m_missingElements.Add(fixup);
			this.m_missingElementsRemaining++;
			if (this.RequiresValueTypeFixup)
			{
				this.UpdateDescendentDependencyChain(1, manager);
			}
		}

		// Token: 0x06003D17 RID: 15639 RVA: 0x000D3F08 File Offset: 0x000D2108
		private void UpdateDescendentDependencyChain(int amount, ObjectManager manager)
		{
			ObjectHolder objectHolder = this;
			do
			{
				objectHolder = manager.FindOrCreateObjectHolder(objectHolder.ContainerID);
				objectHolder.IncrementDescendentFixups(amount);
			}
			while (objectHolder.RequiresValueTypeFixup);
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x000D3F33 File Offset: 0x000D2133
		internal void AddDependency(long dependentObject)
		{
			if (this.m_dependentObjects == null)
			{
				this.m_dependentObjects = new LongList();
			}
			this.m_dependentObjects.Add(dependentObject);
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x000D3F54 File Offset: 0x000D2154
		[SecurityCritical]
		internal void UpdateData(object obj, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainer, FieldInfo field, int[] arrayIndex, ObjectManager manager)
		{
			this.SetObjectValue(obj, manager);
			this.m_serInfo = info;
			this.m_surrogate = surrogate;
			if (idOfContainer != 0L && ((field != null && field.FieldType.IsValueType) || arrayIndex != null))
			{
				if (idOfContainer == this.m_id)
				{
					throw new SerializationException(Environment.GetResourceString("The ID of the containing object cannot be the same as the object ID."));
				}
				this.m_valueFixup = new ValueTypeFixupInfo(idOfContainer, field, arrayIndex);
			}
			this.SetFlags();
			if (this.RequiresValueTypeFixup)
			{
				this.UpdateDescendentDependencyChain(this.m_missingElementsRemaining, manager);
			}
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x000D3FDF File Offset: 0x000D21DF
		internal void MarkForCompletionWhenAvailable()
		{
			this.m_markForFixupWhenAvailable = true;
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x000D3FE8 File Offset: 0x000D21E8
		internal void SetFlags()
		{
			if (this.m_object is IObjectReference)
			{
				this.m_flags |= 1;
			}
			this.m_flags &= -7;
			if (this.m_surrogate != null)
			{
				this.m_flags |= 4;
			}
			else if (this.m_object is ISerializable)
			{
				this.m_flags |= 2;
			}
			if (this.m_valueFixup != null)
			{
				this.m_flags |= 8;
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06003D1C RID: 15644 RVA: 0x000D4068 File Offset: 0x000D2268
		// (set) Token: 0x06003D1D RID: 15645 RVA: 0x000D4075 File Offset: 0x000D2275
		internal bool IsIncompleteObjectReference
		{
			get
			{
				return (this.m_flags & 1) != 0;
			}
			set
			{
				if (value)
				{
					this.m_flags |= 1;
					return;
				}
				this.m_flags &= -2;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06003D1E RID: 15646 RVA: 0x000D4098 File Offset: 0x000D2298
		internal bool RequiresDelayedFixup
		{
			get
			{
				return (this.m_flags & 7) != 0;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06003D1F RID: 15647 RVA: 0x000D40A5 File Offset: 0x000D22A5
		internal bool RequiresValueTypeFixup
		{
			get
			{
				return (this.m_flags & 8) != 0;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06003D20 RID: 15648 RVA: 0x000D40B2 File Offset: 0x000D22B2
		// (set) Token: 0x06003D21 RID: 15649 RVA: 0x000D40E6 File Offset: 0x000D22E6
		internal bool ValueTypeFixupPerformed
		{
			get
			{
				return (this.m_flags & 32768) != 0 || (this.m_object != null && (this.m_dependentObjects == null || this.m_dependentObjects.Count == 0));
			}
			set
			{
				if (value)
				{
					this.m_flags |= 32768;
				}
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06003D22 RID: 15650 RVA: 0x000D40FD File Offset: 0x000D22FD
		internal bool HasISerializable
		{
			get
			{
				return (this.m_flags & 2) != 0;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06003D23 RID: 15651 RVA: 0x000D410A File Offset: 0x000D230A
		internal bool HasSurrogate
		{
			get
			{
				return (this.m_flags & 4) != 0;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06003D24 RID: 15652 RVA: 0x000D4117 File Offset: 0x000D2317
		internal bool CanSurrogatedObjectValueChange
		{
			get
			{
				return this.m_surrogate == null || this.m_surrogate.GetType() != typeof(SurrogateForCyclicalReference);
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06003D25 RID: 15653 RVA: 0x000D413D File Offset: 0x000D233D
		internal bool CanObjectValueChange
		{
			get
			{
				return this.IsIncompleteObjectReference || (this.HasSurrogate && this.CanSurrogatedObjectValueChange);
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06003D26 RID: 15654 RVA: 0x000D4159 File Offset: 0x000D2359
		internal int DirectlyDependentObjects
		{
			get
			{
				return this.m_missingElementsRemaining;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06003D27 RID: 15655 RVA: 0x000D4161 File Offset: 0x000D2361
		internal int TotalDependentObjects
		{
			get
			{
				return this.m_missingElementsRemaining + this.m_missingDecendents;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06003D28 RID: 15656 RVA: 0x000D4170 File Offset: 0x000D2370
		// (set) Token: 0x06003D29 RID: 15657 RVA: 0x000D4178 File Offset: 0x000D2378
		internal bool Reachable
		{
			get
			{
				return this.m_reachable;
			}
			set
			{
				this.m_reachable = value;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06003D2A RID: 15658 RVA: 0x000D4181 File Offset: 0x000D2381
		internal bool TypeLoadExceptionReachable
		{
			get
			{
				return this.m_typeLoad != null;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06003D2B RID: 15659 RVA: 0x000D418C File Offset: 0x000D238C
		// (set) Token: 0x06003D2C RID: 15660 RVA: 0x000D4194 File Offset: 0x000D2394
		internal TypeLoadExceptionHolder TypeLoadException
		{
			get
			{
				return this.m_typeLoad;
			}
			set
			{
				this.m_typeLoad = value;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06003D2D RID: 15661 RVA: 0x000D419D File Offset: 0x000D239D
		internal object ObjectValue
		{
			get
			{
				return this.m_object;
			}
		}

		// Token: 0x06003D2E RID: 15662 RVA: 0x000D41A5 File Offset: 0x000D23A5
		[SecurityCritical]
		internal void SetObjectValue(object obj, ObjectManager manager)
		{
			this.m_object = obj;
			if (obj == manager.TopObject)
			{
				this.m_reachable = true;
			}
			if (obj is TypeLoadExceptionHolder)
			{
				this.m_typeLoad = (TypeLoadExceptionHolder)obj;
			}
			if (this.m_markForFixupWhenAvailable)
			{
				manager.CompleteObject(this, true);
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06003D2F RID: 15663 RVA: 0x000D41E2 File Offset: 0x000D23E2
		// (set) Token: 0x06003D30 RID: 15664 RVA: 0x000D41EA File Offset: 0x000D23EA
		internal SerializationInfo SerializationInfo
		{
			get
			{
				return this.m_serInfo;
			}
			set
			{
				this.m_serInfo = value;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06003D31 RID: 15665 RVA: 0x000D41F3 File Offset: 0x000D23F3
		internal ISerializationSurrogate Surrogate
		{
			get
			{
				return this.m_surrogate;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06003D32 RID: 15666 RVA: 0x000D41FB File Offset: 0x000D23FB
		// (set) Token: 0x06003D33 RID: 15667 RVA: 0x000D4203 File Offset: 0x000D2403
		internal LongList DependentObjects
		{
			get
			{
				return this.m_dependentObjects;
			}
			set
			{
				this.m_dependentObjects = value;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06003D34 RID: 15668 RVA: 0x000D420C File Offset: 0x000D240C
		// (set) Token: 0x06003D35 RID: 15669 RVA: 0x000D4233 File Offset: 0x000D2433
		internal bool RequiresSerInfoFixup
		{
			get
			{
				return ((this.m_flags & 4) != 0 || (this.m_flags & 2) != 0) && (this.m_flags & 16384) == 0;
			}
			set
			{
				if (!value)
				{
					this.m_flags |= 16384;
					return;
				}
				this.m_flags &= -16385;
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06003D36 RID: 15670 RVA: 0x000D425D File Offset: 0x000D245D
		internal ValueTypeFixupInfo ValueFixup
		{
			get
			{
				return this.m_valueFixup;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06003D37 RID: 15671 RVA: 0x000D4265 File Offset: 0x000D2465
		internal bool CompletelyFixed
		{
			get
			{
				return !this.RequiresSerInfoFixup && !this.IsIncompleteObjectReference;
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06003D38 RID: 15672 RVA: 0x000D427A File Offset: 0x000D247A
		internal long ContainerID
		{
			get
			{
				if (this.m_valueFixup != null)
				{
					return this.m_valueFixup.ContainerID;
				}
				return 0L;
			}
		}

		// Token: 0x0400274E RID: 10062
		internal const int INCOMPLETE_OBJECT_REFERENCE = 1;

		// Token: 0x0400274F RID: 10063
		internal const int HAS_ISERIALIZABLE = 2;

		// Token: 0x04002750 RID: 10064
		internal const int HAS_SURROGATE = 4;

		// Token: 0x04002751 RID: 10065
		internal const int REQUIRES_VALUETYPE_FIXUP = 8;

		// Token: 0x04002752 RID: 10066
		internal const int REQUIRES_DELAYED_FIXUP = 7;

		// Token: 0x04002753 RID: 10067
		internal const int SER_INFO_FIXED = 16384;

		// Token: 0x04002754 RID: 10068
		internal const int VALUETYPE_FIXUP_PERFORMED = 32768;

		// Token: 0x04002755 RID: 10069
		private object m_object;

		// Token: 0x04002756 RID: 10070
		internal long m_id;

		// Token: 0x04002757 RID: 10071
		private int m_missingElementsRemaining;

		// Token: 0x04002758 RID: 10072
		private int m_missingDecendents;

		// Token: 0x04002759 RID: 10073
		internal SerializationInfo m_serInfo;

		// Token: 0x0400275A RID: 10074
		internal ISerializationSurrogate m_surrogate;

		// Token: 0x0400275B RID: 10075
		internal FixupHolderList m_missingElements;

		// Token: 0x0400275C RID: 10076
		internal LongList m_dependentObjects;

		// Token: 0x0400275D RID: 10077
		internal ObjectHolder m_next;

		// Token: 0x0400275E RID: 10078
		internal int m_flags;

		// Token: 0x0400275F RID: 10079
		private bool m_markForFixupWhenAvailable;

		// Token: 0x04002760 RID: 10080
		private ValueTypeFixupInfo m_valueFixup;

		// Token: 0x04002761 RID: 10081
		private TypeLoadExceptionHolder m_typeLoad;

		// Token: 0x04002762 RID: 10082
		private bool m_reachable;
	}
}
