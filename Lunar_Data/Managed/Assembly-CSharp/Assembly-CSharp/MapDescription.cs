using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000053 RID: 83
public class MapDescription : MonoBehaviour
{
	// Token: 0x040001CD RID: 461
	[SerializeField]
	public Sprite mapIcon;

	// Token: 0x040001CE RID: 462
	[SerializeField]
	public string descriptorKey;

	// Token: 0x040001CF RID: 463
	[SerializeField]
	public string[] description;

	// Token: 0x040001D0 RID: 464
	[SerializeField]
	public List<Sprite> images;
}
