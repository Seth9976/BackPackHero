using System;
using System.Collections.Generic;

namespace UnityEngine.Timeline
{
	// Token: 0x0200004C RID: 76
	public interface IPropertyCollector
	{
		// Token: 0x060002D5 RID: 725
		void PushActiveGameObject(GameObject gameObject);

		// Token: 0x060002D6 RID: 726
		void PopActiveGameObject();

		// Token: 0x060002D7 RID: 727
		void AddFromClip(AnimationClip clip);

		// Token: 0x060002D8 RID: 728
		void AddFromClips(IEnumerable<AnimationClip> clips);

		// Token: 0x060002D9 RID: 729
		void AddFromName<T>(string name) where T : Component;

		// Token: 0x060002DA RID: 730
		void AddFromName(string name);

		// Token: 0x060002DB RID: 731
		void AddFromClip(GameObject obj, AnimationClip clip);

		// Token: 0x060002DC RID: 732
		void AddFromClips(GameObject obj, IEnumerable<AnimationClip> clips);

		// Token: 0x060002DD RID: 733
		void AddFromName<T>(GameObject obj, string name) where T : Component;

		// Token: 0x060002DE RID: 734
		void AddFromName(GameObject obj, string name);

		// Token: 0x060002DF RID: 735
		void AddFromName(Component component, string name);

		// Token: 0x060002E0 RID: 736
		void AddFromComponent(GameObject obj, Component component);

		// Token: 0x060002E1 RID: 737
		void AddObjectProperties(Object obj, AnimationClip clip);
	}
}
