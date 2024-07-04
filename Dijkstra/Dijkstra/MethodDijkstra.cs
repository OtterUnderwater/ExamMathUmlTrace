using System.Diagnostics;

namespace Dijkstra
{
	public class MethodDijkstra
	{
		public List<(int start, int end, int count)> ribs = new List<(int, int, int)>();
		public List<(int start, int end, int count)> GetListNeedRibs(int startEl) => ribs.Where(it => it.start == startEl || it.end == startEl).ToList();
		public bool IsFileExists(string path) => File.Exists(path);
		public void Dijkstra(string path, int startPoint)
		{
			ReadFile(path);
			Dictionary<int, int> pointWeight = new Dictionary<int, int>();
			Dictionary<int, bool> pointCheck = new Dictionary<int, bool>();
			int countPoint = Math.Max(ribs.Max(it => it.start), ribs.Max(it => it.end));
			for (int i = 1; i <= countPoint; i++)
			{
				pointWeight.Add(i, i == startPoint ? 0 : int.MaxValue);
				pointCheck.Add(i, false);
			}
			while (pointCheck.Values.Contains(false)) //пока есть непройденные
			{
				int minWeight = int.MaxValue;
				int startEl = 0;
				//Непосещенная вершина с минимальным весом
				for (int i = 1; i <= pointWeight.Count; i++)
				{
					if (pointWeight[i] < minWeight && pointCheck[i] == false)
					{
						minWeight = pointWeight[i];
						startEl = i;
					}
				}
				var filterRibs = GetListNeedRibs(startEl);
				foreach (var rib in filterRibs)
				{
					int endEl = (rib.start == startEl) ? rib.end : rib.start;
					int weight = pointWeight[startEl] + rib.count;
					if (weight < pointWeight[endEl])
					{
						pointWeight[endEl] = weight;
					}
					ribs.Remove(rib); //удаляем ребро
				}
				pointCheck[startEl] = true; //вершина посещена
			}
			WriteFile(startPoint, pointWeight);
			Console.WriteLine("Кратчайшие пути записаны в файл");
		}
		public void ReadFile(string path)
		{
			var listener = new TextWriterTraceListener("LogFile.txt");
			Trace.Listeners.Add(listener);
			Trace.AutoFlush = true;
			try
			{
				if (IsFileExists(path))
				{
					using (StreamReader reader = new StreamReader(path))
					{
						while (!reader.EndOfStream)
						{
							List<int> row = reader.ReadLine().Split(";").Select(int.Parse).ToList();
							ribs.Add((row[0], row[1], row[2]));
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
				Console.WriteLine("Некорректные данные в файле");
				Trace.WriteLine($"Некорректные данные в файле: {ex.GetType().Name}");
			}
		}
		public void WriteFile(int startPoint, Dictionary<int, int> weightPoint)
		{
			using (StreamWriter writer = new StreamWriter("result.csv", false))
			{
				for (int i = 2; i <= weightPoint.Count; i++)
				{
					writer.WriteLine($"{startPoint} -> {i};{weightPoint[i]}");
				}
			}
		}
	}
}