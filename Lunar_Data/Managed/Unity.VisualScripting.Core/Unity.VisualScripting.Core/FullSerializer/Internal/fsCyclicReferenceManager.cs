using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Unity.VisualScripting.FullSerializer.Internal
{
	// Token: 0x020001AB RID: 427
	public class fsCyclicReferenceManager
	{
		// Token: 0x06000B64 RID: 2916 RVA: 0x00030AAA File Offset: 0x0002ECAA
		public void Enter()
		{
			this._depth++;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00030ABC File Offset: 0x0002ECBC
		public bool Exit()
		{
			this._depth--;
			if (this._depth == 0)
			{
				this._objectIds = new Dictionary<object, int>(fsCyclicReferenceManager.ObjectReferenceEqualityComparator.Instance);
				this._nextId = 0;
				this._marked = new Dictionary<int, object>();
			}
			if (this._depth < 0)
			{
				this._depth = 0;
				throw new InvalidOperationException("Internal Error - Mismatched Enter/Exit. Please report a bug at https://github.com/jacobdufault/fullserializer/issues with the serialization data.");
			}
			return this._depth == 0;
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00030B25 File Offset: 0x0002ED25
		public object GetReferenceObject(int id)
		{
			if (!this._marked.ContainsKey(id))
			{
				throw new InvalidOperationException("Internal Deserialization Error - Object definition has not been encountered for object with id=" + id.ToString() + "; have you reordered or modified the serialized data? If this is an issue with an unmodified Full Serializer implementation and unmodified serialization data, please report an issue with an included test case.");
			}
			return this._marked[id];
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00030B5D File Offset: 0x0002ED5D
		public void AddReferenceWithId(int id, object reference)
		{
			this._marked[id] = reference;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00030B6C File Offset: 0x0002ED6C
		public int GetReferenceId(object item)
		{
			int num;
			if (!this._objectIds.TryGetValue(item, out num))
			{
				int nextId = this._nextId;
				this._nextId = nextId + 1;
				num = nextId;
				this._objectIds[item] = num;
			}
			return num;
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00030BA9 File Offset: 0x0002EDA9
		public bool IsReference(object item)
		{
			return this._marked.ContainsKey(this.GetReferenceId(item));
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00030BC0 File Offset: 0x0002EDC0
		public void MarkSerialized(object item)
		{
			int referenceId = this.GetReferenceId(item);
			if (this._marked.ContainsKey(referenceId))
			{
				throw new InvalidOperationException("Internal Error - " + ((item != null) ? item.ToString() : null) + " has already been marked as serialized");
			}
			this._marked[referenceId] = item;
		}

		// Token: 0x040002C1 RID: 705
		private Dictionary<object, int> _objectIds = new Dictionary<object, int>(fsCyclicReferenceManager.ObjectReferenceEqualityComparator.Instance);

		// Token: 0x040002C2 RID: 706
		private int _nextId;

		// Token: 0x040002C3 RID: 707
		private Dictionary<int, object> _marked = new Dictionary<int, object>();

		// Token: 0x040002C4 RID: 708
		private int _depth;

		// Token: 0x02000220 RID: 544
		private class ObjectReferenceEqualityComparator : IEqualityComparer<object>
		{
			// Token: 0x06001321 RID: 4897 RVA: 0x0003920F File Offset: 0x0003740F
			bool IEqualityComparer<object>.Equals(object x, object y)
			{
				return x == y;
			}

			// Token: 0x06001322 RID: 4898 RVA: 0x00039215 File Offset: 0x00037415
			int IEqualityComparer<object>.GetHashCode(object obj)
			{
				return RuntimeHelpers.GetHashCode(obj);
			}

			// Token: 0x040009DD RID: 2525
			public static readonly IEqualityComparer<object> Instance = new fsCyclicReferenceManager.ObjectReferenceEqualityComparator();
		}
	}
}
