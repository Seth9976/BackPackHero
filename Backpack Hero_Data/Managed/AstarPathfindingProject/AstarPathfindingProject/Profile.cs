using System;
using System.Diagnostics;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200009C RID: 156
	public class Profile
	{
		// Token: 0x0600074D RID: 1869 RVA: 0x0002CBF3 File Offset: 0x0002ADF3
		public int ControlValue()
		{
			return this.control;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0002CBFB File Offset: 0x0002ADFB
		public Profile(string name)
		{
			this.name = name;
			this.watch = new Stopwatch();
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0002CC20 File Offset: 0x0002AE20
		public static void WriteCSV(string path, params Profile[] profiles)
		{
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0002CC22 File Offset: 0x0002AE22
		public void Run(Action action)
		{
			action();
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0002CC2A File Offset: 0x0002AE2A
		[Conditional("PROFILE")]
		public void Start()
		{
			this.watch.Start();
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0002CC37 File Offset: 0x0002AE37
		[Conditional("PROFILE")]
		public void Stop()
		{
			this.counter++;
			this.watch.Stop();
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0002CC52 File Offset: 0x0002AE52
		[Conditional("PROFILE")]
		public void Log()
		{
			Debug.Log(this.ToString());
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0002CC5F File Offset: 0x0002AE5F
		[Conditional("PROFILE")]
		public void ConsoleLog()
		{
			Console.WriteLine(this.ToString());
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0002CC6C File Offset: 0x0002AE6C
		[Conditional("PROFILE")]
		public void Stop(int control)
		{
			this.counter++;
			this.watch.Stop();
			if (this.control == 1073741824)
			{
				this.control = control;
				return;
			}
			if (this.control != control)
			{
				throw new Exception("Control numbers do not match " + this.control.ToString() + " != " + control.ToString());
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0002CCD8 File Offset: 0x0002AED8
		[Conditional("PROFILE")]
		public void Control(Profile other)
		{
			if (this.ControlValue() != other.ControlValue())
			{
				throw new Exception(string.Concat(new string[]
				{
					"Control numbers do not match (",
					this.name,
					" ",
					other.name,
					") ",
					this.ControlValue().ToString(),
					" != ",
					other.ControlValue().ToString()
				}));
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0002CD58 File Offset: 0x0002AF58
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.name,
				" #",
				this.counter.ToString(),
				" ",
				this.watch.Elapsed.TotalMilliseconds.ToString("0.0 ms"),
				" avg: ",
				(this.watch.Elapsed.TotalMilliseconds / (double)this.counter).ToString("0.00 ms")
			});
		}

		// Token: 0x04000427 RID: 1063
		private const bool PROFILE_MEM = false;

		// Token: 0x04000428 RID: 1064
		public readonly string name;

		// Token: 0x04000429 RID: 1065
		private readonly Stopwatch watch;

		// Token: 0x0400042A RID: 1066
		private int counter;

		// Token: 0x0400042B RID: 1067
		private long mem;

		// Token: 0x0400042C RID: 1068
		private long smem;

		// Token: 0x0400042D RID: 1069
		private int control = 1073741824;

		// Token: 0x0400042E RID: 1070
		private const bool dontCountFirst = false;
	}
}
