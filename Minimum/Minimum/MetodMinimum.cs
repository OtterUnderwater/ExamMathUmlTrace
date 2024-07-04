using System.Diagnostics;

namespace Minimum
{
	public class MetodMinimum
	{
		public List<List<int>> tariffs;
		public List<List<int>> GetStartPlan()
		{
			List<List<int>> referencePlan = new List<List<int>>();
			for (int i = 0; i < tariffs.Count; i++)
			{
				referencePlan.Add(new List<int>());
				for (int j = 0; j < tariffs[0].Count; j++)
				{
					if (i == 0 || j == 0)
					{
						referencePlan[i].Add(tariffs[i][j]);
					}
					else
					{
						referencePlan[i].Add(0);
					}
				}
			}
			return referencePlan;
		}
		public List<List<bool>> GetStartFreeCells()
		{
			List<List<bool>> freeCells = new List<List<bool>>();
			for (int i = 0; i < tariffs.Count - 1; i++)
			{
				freeCells.Add(new List<bool>());
				for (int j = 0; j < tariffs[0].Count - 1; j++)
				{
					freeCells[i].Add(true);
				}
			}
			return freeCells;
		}
		public bool IsСlosed() => tariffs[0].Sum() == tariffs.Sum(it => it[0]);
		public bool IsFileExists(string path) => File.Exists(path);
		public void GetPlan()
		{
			var listener = new TextWriterTraceListener("LogFile.txt");
			Trace.Listeners.Add(listener);
			Trace.AutoFlush = true;
			ReadFile(); //тарифы
			List<List<int>> referencePlan = GetStartPlan(); //опорный план
			List<List<bool>> freeCells = GetStartFreeCells(); //занята ли ячейка	
			int L = 0;
			if (IsСlosed()) // проверка, что задача закрытая
			{
				while (freeCells.Any(row => row.Contains(true)))
				{
					int minEl = int.MaxValue;
					int minI = 0;
					int minJ = 0;
					for (int i = 1; i < tariffs.Count; i++)
					{
						for (int j = 1; j < tariffs[0].Count; j++)
						{
							if (tariffs[i][j] < minEl && freeCells[i - 1][j - 1] == true)
							{
								minEl = tariffs[i][j];
								minI = i;
								minJ = j;
							}
						}
					}
					int min = Math.Min(tariffs[minI][0], tariffs[0][minJ]);
					referencePlan[minI][minJ] = min;
					tariffs[minI][0] -= min;
					tariffs[0][minJ] -= min;
					freeCells[minI - 1][minJ - 1] = false;
					L += referencePlan[minI][minJ] * tariffs[minI][minJ];
				}
				WriteFile(referencePlan, L);
				Console.WriteLine("Опорный план записан в файл result.csv");
			}
			else
			{
				Console.WriteLine("Задача не должна быть открытой.");
				Trace.WriteLine("Задача не должна быть открытой.");
			}
		}
		public void ReadFile()
		{
			try
			{
				if (IsFileExists("file.csv"))
				{
					tariffs = new List<List<int>>();
					using (StreamReader read = new StreamReader("file.csv"))
					{
						while (!read.EndOfStream)
						{
							List<int> row = read.ReadLine().Split(';').Select(int.Parse).ToList();
							tariffs.Add(row);
						}
					}
				}
				else
				{
					Console.WriteLine("Файл не существует");
					Trace.WriteLine("Файл не существует");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Данные в файле некорректные");
				Trace.WriteLine("Данные в файле некорректные: " + ex.GetType().Name);
			}
		}
		public void WriteFile(List<List<int>> plan, int L)
		{
			using (StreamWriter writer = new StreamWriter("result.csv", false))
			{
				for (int i = 0; i < plan.Count; i++)
				{
					string row = string.Join(";", plan[i]);
					writer.WriteLine(row);
				}
				writer.WriteLine($"L = {L}");
			}
		}
	}
}
