using System;
using System.IO;

namespace Unity.VisualScripting.Antlr3.Runtime.Misc
{
	// Token: 0x0200004C RID: 76
	public class Stats
	{
		// Token: 0x060002CC RID: 716 RVA: 0x00008868 File Offset: 0x00007868
		public static double Stddev(int[] X)
		{
			int num = X.Length;
			if (num <= 1)
			{
				return 0.0;
			}
			double num2 = Stats.Avg(X);
			double num3 = 0.0;
			for (int i = 0; i < num; i++)
			{
				num3 += ((double)X[i] - num2) * ((double)X[i] - num2);
			}
			num3 /= (double)(num - 1);
			return Math.Sqrt(num3);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000088C4 File Offset: 0x000078C4
		public static double Avg(int[] X)
		{
			double num = 0.0;
			int num2 = X.Length;
			if (num2 == 0)
			{
				return 0.0;
			}
			for (int i = 0; i < num2; i++)
			{
				num += (double)X[i];
			}
			if (num >= 0.0)
			{
				return num / (double)num2;
			}
			return 0.0;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000891C File Offset: 0x0000791C
		public static int Min(int[] X)
		{
			int num = int.MaxValue;
			int num2 = X.Length;
			if (num2 == 0)
			{
				return 0;
			}
			for (int i = 0; i < num2; i++)
			{
				if (X[i] < num)
				{
					num = X[i];
				}
			}
			return num;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00008950 File Offset: 0x00007950
		public static int Max(int[] X)
		{
			int num = int.MinValue;
			int num2 = X.Length;
			if (num2 == 0)
			{
				return 0;
			}
			for (int i = 0; i < num2; i++)
			{
				if (X[i] > num)
				{
					num = X[i];
				}
			}
			return num;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00008984 File Offset: 0x00007984
		public static int Sum(int[] X)
		{
			int num = 0;
			int num2 = X.Length;
			if (num2 == 0)
			{
				return 0;
			}
			for (int i = 0; i < num2; i++)
			{
				num += X[i];
			}
			return num;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000089B0 File Offset: 0x000079B0
		public static void WriteReport(string filename, string data)
		{
			string absoluteFileName = Stats.GetAbsoluteFileName(filename);
			FileInfo fileInfo = new FileInfo(absoluteFileName);
			fileInfo.Directory.Create();
			try
			{
				StreamWriter streamWriter = new StreamWriter(fileInfo.FullName, true);
				streamWriter.WriteLine(data);
				streamWriter.Close();
			}
			catch (IOException ex)
			{
				ErrorManager.InternalError("can't write stats to " + absoluteFileName, ex);
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00008A18 File Offset: 0x00007A18
		public static string GetAbsoluteFileName(string filename)
		{
			return Path.Combine(Path.Combine(Environment.CurrentDirectory, Constants.ANTLRWORKS_DIR), filename);
		}
	}
}
