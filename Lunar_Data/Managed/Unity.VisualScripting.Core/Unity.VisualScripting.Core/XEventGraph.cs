using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000054 RID: 84
	public static class XEventGraph
	{
		// Token: 0x0600027C RID: 636 RVA: 0x000063B8 File Offset: 0x000045B8
		public static void TriggerEventHandler<TArgs>(this GraphReference reference, Func<EventHook, bool> predicate, TArgs args, Func<IGraphParentElement, bool> recurse, bool force)
		{
			Ensure.That("reference").IsNotNull<GraphReference>(reference);
			foreach (IGraphElement graphElement in reference.graph.elements)
			{
				IGraphEventHandler<TArgs> graphEventHandler = graphElement as IGraphEventHandler<TArgs>;
				if (graphEventHandler != null && (predicate == null || predicate(graphEventHandler.GetHook(reference))) && (force || graphEventHandler.IsListening(reference)))
				{
					graphEventHandler.Trigger(reference, args);
				}
				IGraphParentElement graphParentElement = graphElement as IGraphParentElement;
				if (graphParentElement != null && recurse(graphParentElement))
				{
					GraphReference graphReference = reference.ChildReference(graphParentElement, false, new int?(0));
					if (graphReference != null)
					{
						graphReference.TriggerEventHandler(predicate, args, recurse, force);
					}
				}
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000647C File Offset: 0x0000467C
		public static void TriggerEventHandler<TArgs>(this GraphStack stack, Func<EventHook, bool> predicate, TArgs args, Func<IGraphParentElement, bool> recurse, bool force)
		{
			Ensure.That("stack").IsNotNull<GraphStack>(stack);
			GraphReference graphReference = null;
			foreach (IGraphElement graphElement in stack.graph.elements)
			{
				IGraphEventHandler<TArgs> graphEventHandler = graphElement as IGraphEventHandler<TArgs>;
				if (graphEventHandler != null)
				{
					if (graphReference == null)
					{
						graphReference = stack.ToReference();
					}
					if ((predicate == null || predicate(graphEventHandler.GetHook(graphReference))) && (force || graphEventHandler.IsListening(graphReference)))
					{
						graphEventHandler.Trigger(graphReference, args);
					}
				}
				IGraphParentElement graphParentElement = graphElement as IGraphParentElement;
				if (graphParentElement != null && recurse(graphParentElement) && stack.TryEnterParentElementUnsafe(graphParentElement))
				{
					stack.TriggerEventHandler(predicate, args, recurse, force);
					stack.ExitParentElement();
				}
			}
		}
	}
}
