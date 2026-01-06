using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000065 RID: 101
	public static class GraphsExceptionUtility
	{
		// Token: 0x0600035C RID: 860 RVA: 0x00008D8C File Offset: 0x00006F8C
		public static Exception GetException(this IGraphElementWithDebugData element, GraphPointer pointer)
		{
			if (!pointer.hasDebugData)
			{
				return null;
			}
			return pointer.GetElementDebugData<IGraphElementDebugData>(element).runtimeException;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00008DA4 File Offset: 0x00006FA4
		public static void SetException(this IGraphElementWithDebugData element, GraphPointer pointer, Exception ex)
		{
			if (!pointer.hasDebugData)
			{
				return;
			}
			pointer.GetElementDebugData<IGraphElementDebugData>(element).runtimeException = ex;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00008DBC File Offset: 0x00006FBC
		public static void HandleException(this IGraphElementWithDebugData element, GraphPointer pointer, Exception ex)
		{
			Ensure.That("ex").IsNotNull<Exception>(ex);
			if (pointer == null)
			{
				Debug.LogError("Caught exception with null graph pointer (flow was likely disposed):\n" + ((ex != null) ? ex.ToString() : null));
				return;
			}
			GraphReference graphReference = pointer.AsReference();
			if (!ex.HandledIn(graphReference))
			{
				element.SetException(pointer, ex);
			}
			while (graphReference.isChild)
			{
				IGraphParentElement parentElement = graphReference.parentElement;
				graphReference = graphReference.ParentReference(true);
				IGraphElementWithDebugData graphElementWithDebugData = parentElement as IGraphElementWithDebugData;
				if (graphElementWithDebugData != null && !ex.HandledIn(graphReference))
				{
					graphElementWithDebugData.SetException(graphReference, ex);
				}
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00008E44 File Offset: 0x00007044
		private static bool HandledIn(this Exception ex, GraphReference reference)
		{
			Ensure.That("ex").IsNotNull<Exception>(ex);
			if (!ex.Data.Contains("Bolt.Core.Handled"))
			{
				ex.Data.Add("Bolt.Core.Handled", new HashSet<GraphReference>());
			}
			HashSet<GraphReference> hashSet = (HashSet<GraphReference>)ex.Data["Bolt.Core.Handled"];
			if (hashSet.Contains(reference))
			{
				return true;
			}
			hashSet.Add(reference);
			return false;
		}

		// Token: 0x040000E2 RID: 226
		private const string handledKey = "Bolt.Core.Handled";
	}
}
