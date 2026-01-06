using System;
using Unity.Jobs;

namespace Pathfinding.Jobs
{
	// Token: 0x02000184 RID: 388
	public interface IJobTimeSliced : IJob
	{
		// Token: 0x06000AB5 RID: 2741
		bool Execute(TimeSlice timeSlice);
	}
}
