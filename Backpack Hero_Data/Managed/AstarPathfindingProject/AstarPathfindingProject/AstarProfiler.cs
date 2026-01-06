using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000096 RID: 150
	public class AstarProfiler
	{
		// Token: 0x06000718 RID: 1816 RVA: 0x0002AB3F File Offset: 0x00028D3F
		private AstarProfiler()
		{
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0002AB48 File Offset: 0x00028D48
		[Conditional("ProfileAstar")]
		public static void InitializeFastProfile(string[] profileNames)
		{
			AstarProfiler.fastProfileNames = new string[profileNames.Length + 2];
			Array.Copy(profileNames, AstarProfiler.fastProfileNames, profileNames.Length);
			AstarProfiler.fastProfileNames[AstarProfiler.fastProfileNames.Length - 2] = "__Control1__";
			AstarProfiler.fastProfileNames[AstarProfiler.fastProfileNames.Length - 1] = "__Control2__";
			AstarProfiler.fastProfiles = new AstarProfiler.ProfilePoint[AstarProfiler.fastProfileNames.Length];
			for (int i = 0; i < AstarProfiler.fastProfiles.Length; i++)
			{
				AstarProfiler.fastProfiles[i] = new AstarProfiler.ProfilePoint();
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0002ABC9 File Offset: 0x00028DC9
		[Conditional("ProfileAstar")]
		public static void StartFastProfile(int tag)
		{
			AstarProfiler.fastProfiles[tag].watch.Start();
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0002ABDC File Offset: 0x00028DDC
		[Conditional("ProfileAstar")]
		public static void EndFastProfile(int tag)
		{
			AstarProfiler.ProfilePoint profilePoint = AstarProfiler.fastProfiles[tag];
			profilePoint.totalCalls++;
			profilePoint.watch.Stop();
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0002ABFD File Offset: 0x00028DFD
		[Conditional("ASTAR_UNITY_PRO_PROFILER")]
		public static void EndProfile()
		{
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0002AC00 File Offset: 0x00028E00
		[Conditional("ProfileAstar")]
		public static void StartProfile(string tag)
		{
			AstarProfiler.ProfilePoint profilePoint;
			AstarProfiler.profiles.TryGetValue(tag, out profilePoint);
			if (profilePoint == null)
			{
				profilePoint = new AstarProfiler.ProfilePoint();
				AstarProfiler.profiles[tag] = profilePoint;
			}
			profilePoint.tmpBytes = GC.GetTotalMemory(false);
			profilePoint.watch.Start();
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0002AC48 File Offset: 0x00028E48
		[Conditional("ProfileAstar")]
		public static void EndProfile(string tag)
		{
			if (!AstarProfiler.profiles.ContainsKey(tag))
			{
				Debug.LogError("Can only end profiling for a tag which has already been started (tag was " + tag + ")");
				return;
			}
			AstarProfiler.ProfilePoint profilePoint = AstarProfiler.profiles[tag];
			profilePoint.totalCalls++;
			profilePoint.watch.Stop();
			profilePoint.totalBytes += GC.GetTotalMemory(false) - profilePoint.tmpBytes;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0002ACB8 File Offset: 0x00028EB8
		[Conditional("ProfileAstar")]
		public static void Reset()
		{
			AstarProfiler.profiles.Clear();
			AstarProfiler.startTime = DateTime.UtcNow;
			if (AstarProfiler.fastProfiles != null)
			{
				for (int i = 0; i < AstarProfiler.fastProfiles.Length; i++)
				{
					AstarProfiler.fastProfiles[i] = new AstarProfiler.ProfilePoint();
				}
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0002AD00 File Offset: 0x00028F00
		[Conditional("ProfileAstar")]
		public static void PrintFastResults()
		{
			if (AstarProfiler.fastProfiles == null)
			{
				return;
			}
			for (int i = 0; i < 1000; i++)
			{
			}
			double num = AstarProfiler.fastProfiles[AstarProfiler.fastProfiles.Length - 2].watch.Elapsed.TotalMilliseconds / 1000.0;
			TimeSpan timeSpan = DateTime.UtcNow - AstarProfiler.startTime;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("============================\n\t\t\t\tProfile results:\n============================\n");
			stringBuilder.Append("Name\t\t|\tTotal Time\t|\tTotal Calls\t|\tAvg/Call\t|\tBytes");
			for (int j = 0; j < AstarProfiler.fastProfiles.Length; j++)
			{
				string text = AstarProfiler.fastProfileNames[j];
				AstarProfiler.ProfilePoint profilePoint = AstarProfiler.fastProfiles[j];
				int totalCalls = profilePoint.totalCalls;
				double num2 = profilePoint.watch.Elapsed.TotalMilliseconds - num * (double)totalCalls;
				if (totalCalls >= 1)
				{
					stringBuilder.Append("\n").Append(text.PadLeft(10)).Append("|   ");
					stringBuilder.Append(num2.ToString("0.0 ").PadLeft(10)).Append(profilePoint.watch.Elapsed.TotalMilliseconds.ToString("(0.0)").PadLeft(10)).Append("|   ");
					stringBuilder.Append(totalCalls.ToString().PadLeft(10)).Append("|   ");
					stringBuilder.Append((num2 / (double)totalCalls).ToString("0.000").PadLeft(10));
				}
			}
			stringBuilder.Append("\n\n============================\n\t\tTotal runtime: ");
			stringBuilder.Append(timeSpan.TotalSeconds.ToString("F3"));
			stringBuilder.Append(" seconds\n============================");
			Debug.Log(stringBuilder.ToString());
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0002AED8 File Offset: 0x000290D8
		[Conditional("ProfileAstar")]
		public static void PrintResults()
		{
			TimeSpan timeSpan = DateTime.UtcNow - AstarProfiler.startTime;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("============================\n\t\t\t\tProfile results:\n============================\n");
			int num = 5;
			foreach (KeyValuePair<string, AstarProfiler.ProfilePoint> keyValuePair in AstarProfiler.profiles)
			{
				num = Math.Max(keyValuePair.Key.Length, num);
			}
			stringBuilder.Append(" Name ".PadRight(num)).Append("|").Append(" Total Time\t".PadRight(20))
				.Append("|")
				.Append(" Total Calls ".PadRight(20))
				.Append("|")
				.Append(" Avg/Call ".PadRight(20));
			foreach (KeyValuePair<string, AstarProfiler.ProfilePoint> keyValuePair2 in AstarProfiler.profiles)
			{
				double totalMilliseconds = keyValuePair2.Value.watch.Elapsed.TotalMilliseconds;
				int totalCalls = keyValuePair2.Value.totalCalls;
				if (totalCalls >= 1)
				{
					string key = keyValuePair2.Key;
					stringBuilder.Append("\n").Append(key.PadRight(num)).Append("| ");
					stringBuilder.Append(totalMilliseconds.ToString("0.0").PadRight(20)).Append("| ");
					stringBuilder.Append(totalCalls.ToString().PadRight(20)).Append("| ");
					stringBuilder.Append((totalMilliseconds / (double)totalCalls).ToString("0.000").PadRight(20));
					stringBuilder.Append(AstarMath.FormatBytesBinary((int)keyValuePair2.Value.totalBytes).PadLeft(10));
				}
			}
			stringBuilder.Append("\n\n============================\n\t\tTotal runtime: ");
			stringBuilder.Append(timeSpan.TotalSeconds.ToString("F3"));
			stringBuilder.Append(" seconds\n============================");
			Debug.Log(stringBuilder.ToString());
		}

		// Token: 0x04000411 RID: 1041
		private static readonly Dictionary<string, AstarProfiler.ProfilePoint> profiles = new Dictionary<string, AstarProfiler.ProfilePoint>();

		// Token: 0x04000412 RID: 1042
		private static DateTime startTime = DateTime.UtcNow;

		// Token: 0x04000413 RID: 1043
		public static AstarProfiler.ProfilePoint[] fastProfiles;

		// Token: 0x04000414 RID: 1044
		public static string[] fastProfileNames;

		// Token: 0x02000144 RID: 324
		public class ProfilePoint
		{
			// Token: 0x04000760 RID: 1888
			public Stopwatch watch = new Stopwatch();

			// Token: 0x04000761 RID: 1889
			public int totalCalls;

			// Token: 0x04000762 RID: 1890
			public long tmpBytes;

			// Token: 0x04000763 RID: 1891
			public long totalBytes;
		}
	}
}
