using System.Diagnostics;
using System.Text;

namespace Northwest
{
	public class MetodNorthwest
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
					referencePlan[i].Add((i == 0 || j == 0) ? tariffs[i][j]: 0);
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
		public bool IsClose() => tariffs[0].Sum() == tariffs.Sum(it => it[0]);
		public bool IsFileExists(string path) => File.Exists(path);
		public void GetPlan()
		{
			var listener = new TextWriterTraceListener("LogFile.txt");
			Trace.Listeners.Add(listener);
			Trace.AutoFlush = true;
			ReadFile(); //тарифы
			List<List<int>> referencePlan = GetStartPlan(); //опорный план
			List<List<bool>> freeCells = GetStartFreeCells(); //свободные ячейки
			int L = 0;
			if (IsClose())
			{
				while (freeCells.Any(row => row.Contains(true)))
				{
					for (int i = 1; i < tariffs.Count; i++)
					{
						for (int j = 1; j < tariffs[0].Count; j++)
						{
							int min = Math.Min(tariffs[i][0], tariffs[0][j]);
							referencePlan[i][j] = min;
							tariffs[i][0] -= min;
							tariffs[0][j] -= min;
							freeCells[i - 1][j - 1] = false;
							L += referencePlan[i][j] * tariffs[i][j];
						}
					}
				}
				WriteFile(referencePlan, L);
				Console.WriteLine("Опорный план записан в файл result.csv");
			}
			else
			{
				Console.WriteLine("Задача должна быть закрытой.");
				Trace.WriteLine("Задача должна быть закрытой.");
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
				Debug.WriteLine(ex.Message);
			}
		}
		public void WriteFile(List<List<int>> plan, int L)
		{
			using (StreamWriter writer = new StreamWriter("result.csv", false, Encoding.UTF8))
			{
				for (int i = 0; i < plan.Count; i++)
				{
					writer.WriteLine(string.Join(";", plan[i]));
				}
				writer.WriteLine($"L = {L}");
			}
		}
	}
}
