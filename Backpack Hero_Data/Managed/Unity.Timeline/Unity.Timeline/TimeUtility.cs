using System;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000053 RID: 83
	internal static class TimeUtility
	{
		// Token: 0x060002F3 RID: 755 RVA: 0x0000A85D File Offset: 0x00008A5D
		private static void ValidateFrameRate(double frameRate)
		{
			if (frameRate <= TimeUtility.kTimeEpsilon)
			{
				throw new ArgumentException("frame rate cannot be 0 or negative");
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000A874 File Offset: 0x00008A74
		public static int ToFrames(double time, double frameRate)
		{
			TimeUtility.ValidateFrameRate(frameRate);
			time = Math.Min(Math.Max(time, -TimeUtility.k_MaxTimelineDurationInSeconds), TimeUtility.k_MaxTimelineDurationInSeconds);
			double epsilon = TimeUtility.GetEpsilon(time, frameRate);
			if (time < 0.0)
			{
				return (int)Math.Ceiling(time * frameRate - epsilon);
			}
			return (int)Math.Floor(time * frameRate + epsilon);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000A8CA File Offset: 0x00008ACA
		public static double ToExactFrames(double time, double frameRate)
		{
			TimeUtility.ValidateFrameRate(frameRate);
			return time * frameRate;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000A8D5 File Offset: 0x00008AD5
		public static double FromFrames(int frames, double frameRate)
		{
			TimeUtility.ValidateFrameRate(frameRate);
			return (double)frames / frameRate;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000A8E1 File Offset: 0x00008AE1
		public static double FromFrames(double frames, double frameRate)
		{
			TimeUtility.ValidateFrameRate(frameRate);
			return frames / frameRate;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000A8EC File Offset: 0x00008AEC
		public static bool OnFrameBoundary(double time, double frameRate)
		{
			return TimeUtility.OnFrameBoundary(time, frameRate, TimeUtility.GetEpsilon(time, frameRate));
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000A8FC File Offset: 0x00008AFC
		public static double GetEpsilon(double time, double frameRate)
		{
			return Math.Max(Math.Abs(time), 1.0) * frameRate * TimeUtility.kTimeEpsilon;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000A91C File Offset: 0x00008B1C
		public static bool OnFrameBoundary(double time, double frameRate, double epsilon)
		{
			TimeUtility.ValidateFrameRate(frameRate);
			double num = TimeUtility.ToExactFrames(time, frameRate);
			double num2 = Math.Round(num);
			return Math.Abs(num - num2) < epsilon;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000A948 File Offset: 0x00008B48
		public static double RoundToFrame(double time, double frameRate)
		{
			TimeUtility.ValidateFrameRate(frameRate);
			double num = (double)((int)Math.Floor(time * frameRate)) / frameRate;
			double num2 = (double)((int)Math.Ceiling(time * frameRate)) / frameRate;
			if (Math.Abs(time - num) >= Math.Abs(time - num2))
			{
				return num2;
			}
			return num;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000A98C File Offset: 0x00008B8C
		public static string TimeAsFrames(double timeValue, double frameRate, string format = "F2")
		{
			if (TimeUtility.OnFrameBoundary(timeValue, frameRate))
			{
				return TimeUtility.ToFrames(timeValue, frameRate).ToString();
			}
			return TimeUtility.ToExactFrames(timeValue, frameRate).ToString(format);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000A9C4 File Offset: 0x00008BC4
		public static string TimeAsTimeCode(double timeValue, double frameRate, string format = "F2")
		{
			TimeUtility.ValidateFrameRate(frameRate);
			int num = (int)Math.Abs(timeValue);
			int num2 = num / 3600;
			int num3 = num % 3600 / 60;
			int num4 = num % 60;
			string text = ((timeValue < 0.0) ? "-" : string.Empty);
			string text2;
			if (num2 > 0)
			{
				text2 = string.Concat(new string[]
				{
					num2.ToString(),
					":",
					num3.ToString("D2"),
					":",
					num4.ToString("D2")
				});
			}
			else if (num3 > 0)
			{
				text2 = num3.ToString() + ":" + num4.ToString("D2");
			}
			else
			{
				text2 = num4.ToString();
			}
			int num5 = (int)Math.Floor(Math.Log10(frameRate) + 1.0);
			string text3 = (TimeUtility.ToFrames(timeValue, frameRate) - TimeUtility.ToFrames((double)num, frameRate)).ToString().PadLeft(num5, '0');
			if (!TimeUtility.OnFrameBoundary(timeValue, frameRate))
			{
				string text4 = TimeUtility.ToExactFrames(timeValue, frameRate).ToString(format);
				int num6 = text4.IndexOf('.');
				if (num6 >= 0)
				{
					text3 = text3 + " [" + text4.Substring(num6) + "]";
				}
			}
			return text + text2 + ":" + text3;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000AB20 File Offset: 0x00008D20
		public static double ParseTimeCode(string timeCode, double frameRate, double defaultValue)
		{
			timeCode = TimeUtility.RemoveChar(timeCode, (char c) => char.IsWhiteSpace(c));
			string[] array = timeCode.Split(':', StringSplitOptions.None);
			if (array.Length == 0 || array.Length > 4)
			{
				return defaultValue;
			}
			int num = 0;
			int num2 = 0;
			double num3 = 0.0;
			double num4 = 0.0;
			try
			{
				string text = array[array.Length - 1];
				if (Regex.Match(text, "^\\d+\\.\\d+$").Success)
				{
					num3 = double.Parse(text);
					if (array.Length > 3)
					{
						return defaultValue;
					}
					if (array.Length > 1)
					{
						num2 = int.Parse(array[array.Length - 2]);
					}
					if (array.Length > 2)
					{
						num = int.Parse(array[array.Length - 3]);
					}
				}
				else
				{
					if (Regex.Match(text, "^\\d+\\[\\.\\d+\\]$").Success)
					{
						num4 = double.Parse(TimeUtility.RemoveChar(text, (char c) => c == '[' || c == ']'));
					}
					else
					{
						if (!Regex.Match(text, "^\\d*$").Success)
						{
							return defaultValue;
						}
						num4 = (double)int.Parse(text);
					}
					if (array.Length > 1)
					{
						num3 = (double)int.Parse(array[array.Length - 2]);
					}
					if (array.Length > 2)
					{
						num2 = int.Parse(array[array.Length - 3]);
					}
					if (array.Length > 3)
					{
						num = int.Parse(array[array.Length - 4]);
					}
				}
			}
			catch (FormatException)
			{
				return defaultValue;
			}
			return num4 / frameRate + num3 + (double)(num2 * 60) + (double)(num * 3600);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000ACC0 File Offset: 0x00008EC0
		public static double ParseTimeSeconds(string timeCode, double frameRate, double defaultValue)
		{
			timeCode = TimeUtility.RemoveChar(timeCode, (char c) => char.IsWhiteSpace(c));
			string[] array = timeCode.Split(':', StringSplitOptions.None);
			if (array.Length == 0 || array.Length > 4)
			{
				return defaultValue;
			}
			int num = 0;
			int num2 = 0;
			double num3 = 0.0;
			try
			{
				string text = array[array.Length - 1];
				if (!double.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out num3))
				{
					if (!Regex.Match(text, "^\\d+\\.\\d+$").Success)
					{
						return defaultValue;
					}
					num3 = double.Parse(text);
				}
				if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out num3))
				{
					return defaultValue;
				}
				if (array.Length > 3)
				{
					return defaultValue;
				}
				if (array.Length > 1)
				{
					num2 = int.Parse(array[array.Length - 2]);
				}
				if (array.Length > 2)
				{
					num = int.Parse(array[array.Length - 3]);
				}
			}
			catch (FormatException)
			{
				return defaultValue;
			}
			return num3 + (double)(num2 * 60) + (double)(num * 3600);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000ADCC File Offset: 0x00008FCC
		public static double GetAnimationClipLength(AnimationClip clip)
		{
			if (clip == null || clip.empty)
			{
				return 0.0;
			}
			double num = (double)clip.length;
			if (clip.frameRate > 0f)
			{
				num = (double)Mathf.Round(clip.length * clip.frameRate) / (double)clip.frameRate;
			}
			return num;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000AE28 File Offset: 0x00009028
		private static string RemoveChar(string str, Func<char, bool> charToRemoveFunc)
		{
			int length = str.Length;
			char[] array = str.ToCharArray();
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				if (!charToRemoveFunc(array[i]))
				{
					array[num++] = array[i];
				}
			}
			return new string(array, 0, num);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000AE70 File Offset: 0x00009070
		public static FrameRate GetClosestFrameRate(double frameRate)
		{
			TimeUtility.ValidateFrameRate(frameRate);
			FrameRate frameRate2 = FrameRate.DoubleToFrameRate(frameRate);
			if (Math.Abs(frameRate - frameRate2.rate) >= TimeUtility.kFrameRateRounding)
			{
				return default(FrameRate);
			}
			return frameRate2;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000AEAC File Offset: 0x000090AC
		public static FrameRate ToFrameRate(StandardFrameRates enumValue)
		{
			switch (enumValue)
			{
			case StandardFrameRates.Fps24:
				return FrameRate.k_24Fps;
			case StandardFrameRates.Fps23_97:
				return FrameRate.k_23_976Fps;
			case StandardFrameRates.Fps25:
				return FrameRate.k_25Fps;
			case StandardFrameRates.Fps30:
				return FrameRate.k_30Fps;
			case StandardFrameRates.Fps29_97:
				return FrameRate.k_29_97Fps;
			case StandardFrameRates.Fps50:
				return FrameRate.k_50Fps;
			case StandardFrameRates.Fps60:
				return FrameRate.k_60Fps;
			case StandardFrameRates.Fps59_94:
				return FrameRate.k_59_94Fps;
			default:
				return default(FrameRate);
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000AF1C File Offset: 0x0000911C
		internal static bool ToStandardFrameRate(FrameRate rate, out StandardFrameRates standard)
		{
			if (rate == FrameRate.k_23_976Fps)
			{
				standard = StandardFrameRates.Fps23_97;
			}
			else if (rate == FrameRate.k_24Fps)
			{
				standard = StandardFrameRates.Fps24;
			}
			else if (rate == FrameRate.k_25Fps)
			{
				standard = StandardFrameRates.Fps25;
			}
			else if (rate == FrameRate.k_30Fps)
			{
				standard = StandardFrameRates.Fps30;
			}
			else if (rate == FrameRate.k_29_97Fps)
			{
				standard = StandardFrameRates.Fps29_97;
			}
			else if (rate == FrameRate.k_50Fps)
			{
				standard = StandardFrameRates.Fps50;
			}
			else if (rate == FrameRate.k_59_94Fps)
			{
				standard = StandardFrameRates.Fps59_94;
			}
			else
			{
				if (!(rate == FrameRate.k_60Fps))
				{
					standard = (StandardFrameRates)Enum.GetValues(typeof(StandardFrameRates)).Length;
					return false;
				}
				standard = StandardFrameRates.Fps60;
			}
			return true;
		}

		// Token: 0x04000107 RID: 263
		public static readonly double kTimeEpsilon = 1E-14;

		// Token: 0x04000108 RID: 264
		public static readonly double kFrameRateEpsilon = 1E-06;

		// Token: 0x04000109 RID: 265
		public static readonly double k_MaxTimelineDurationInSeconds = 9000000.0;

		// Token: 0x0400010A RID: 266
		public static readonly double kFrameRateRounding = 0.01;
	}
}
