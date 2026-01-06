using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000139 RID: 313
	public class SerializationOperation
	{
		// Token: 0x06000892 RID: 2194 RVA: 0x000261A8 File Offset: 0x000243A8
		public SerializationOperation()
		{
			this.objectReferences = new List<Object>();
			this.serializer = new fsSerializer();
			this.serializer.AddConverter(new UnityObjectConverter());
			this.serializer.AddConverter(new RayConverter());
			this.serializer.AddConverter(new Ray2DConverter());
			this.serializer.AddConverter(new NamespaceConverter());
			this.serializer.AddConverter(new LooseAssemblyNameConverter());
			this.serializer.Context.Set<List<Object>>(this.objectReferences);
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x00026237 File Offset: 0x00024437
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x0002623F File Offset: 0x0002443F
		public fsSerializer serializer { get; private set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x00026248 File Offset: 0x00024448
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x00026250 File Offset: 0x00024450
		public List<Object> objectReferences { get; private set; }

		// Token: 0x06000897 RID: 2199 RVA: 0x00026259 File Offset: 0x00024459
		public void Reset()
		{
			this.objectReferences.Clear();
		}
	}
}
