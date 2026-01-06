using System;
using System.Collections;

namespace System.Diagnostics
{
	/// <summary>Provides a strongly typed collection of <see cref="T:System.Diagnostics.InstanceData" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200026A RID: 618
	public class InstanceDataCollection : DictionaryBase
	{
		// Token: 0x0600136F RID: 4975 RVA: 0x0005175B File Offset: 0x0004F95B
		private static void CheckNull(object value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.InstanceDataCollection" /> class, using the specified performance counter (which defines a performance instance).</summary>
		/// <param name="counterName">The name of the counter, which often describes the quantity that is being counted. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="counterName" /> parameter is null. </exception>
		// Token: 0x06001370 RID: 4976 RVA: 0x00051767 File Offset: 0x0004F967
		[Obsolete("Use InstanceDataCollectionCollection indexer instead.")]
		public InstanceDataCollection(string counterName)
		{
			InstanceDataCollection.CheckNull(counterName, "counterName");
			this.counterName = counterName;
		}

		/// <summary>Gets the name of the performance counter whose instance data you want to get.</summary>
		/// <returns>The performance counter name.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x00051781 File Offset: 0x0004F981
		public string CounterName
		{
			get
			{
				return this.counterName;
			}
		}

		/// <summary>Gets the instance data associated with this counter. This is typically a set of raw counter values.</summary>
		/// <returns>An <see cref="T:System.Diagnostics.InstanceData" /> item, by which the <see cref="T:System.Diagnostics.InstanceDataCollection" /> object is indexed.</returns>
		/// <param name="instanceName">The name of the performance counter category instance, or an empty string ("") if the category contains a single instance. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instanceName" /> parameter is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003A3 RID: 931
		public InstanceData this[string instanceName]
		{
			get
			{
				InstanceDataCollection.CheckNull(instanceName, "instanceName");
				return (InstanceData)base.Dictionary[instanceName];
			}
		}

		/// <summary>Gets the object and counter registry keys for the objects associated with this instance data.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that represents a set of object-specific registry keys.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x000517A7 File Offset: 0x0004F9A7
		public ICollection Keys
		{
			get
			{
				return base.Dictionary.Keys;
			}
		}

		/// <summary>Gets the raw counter values that comprise the instance data for the counter.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that represents the counter's raw data values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x000517B4 File Offset: 0x0004F9B4
		public ICollection Values
		{
			get
			{
				return base.Dictionary.Values;
			}
		}

		/// <summary>Determines whether a performance instance with a specified name (identified by one of the indexed <see cref="T:System.Diagnostics.InstanceData" /> objects) exists in the collection.</summary>
		/// <returns>true if the instance exists in the collection; otherwise, false.</returns>
		/// <param name="instanceName">The name of the instance to find in this collection. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instanceName" /> parameter is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001375 RID: 4981 RVA: 0x000517C1 File Offset: 0x0004F9C1
		public bool Contains(string instanceName)
		{
			InstanceDataCollection.CheckNull(instanceName, "instanceName");
			return base.Dictionary.Contains(instanceName);
		}

		/// <summary>Copies the items in the collection to the specified one-dimensional array at the specified index.</summary>
		/// <param name="instances">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection. </param>
		/// <param name="index">The zero-based index value at which to add the new instances. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001376 RID: 4982 RVA: 0x000517DA File Offset: 0x0004F9DA
		public void CopyTo(InstanceData[] instances, int index)
		{
			base.Dictionary.CopyTo(instances, index);
		}

		// Token: 0x04000B01 RID: 2817
		private string counterName;
	}
}
