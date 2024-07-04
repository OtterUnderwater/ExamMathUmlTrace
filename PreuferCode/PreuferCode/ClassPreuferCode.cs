using System.Diagnostics;

namespace PreuferCode
{
	public class ClassPreuferCode
	{
		public List<int[]> ribs;
		public List<int> code;
		public bool IsFileExists(string path) => File.Exists(path);
		public List<int> GetListLeaves() => ribs.SelectMany(it => it).GroupBy(it => it).Where(it => it.Count() == 1).Select(it => it.Key).ToList();
		public void GetPreuferCode()
		{
			ReadFile();
			code = new List<int>();
			while (ribs.Count != 1)
			{
				List<int> leaves = GetListLeaves(); //список листьев
				int minLeave = leaves.Min();
				for (int i = 0; i < ribs.Count; i++)
				{
					if (ribs[i][0] == minLeave || ribs[i][1] == minLeave)
					{
						code.Add(ribs[i][0] == minLeave ? ribs[i][1] : ribs[i][0]);
						ribs.RemoveAt(i);
						break;
					}
				}
			}
			WriteFile(code);
			Console.WriteLine("Код прюфера записан в файл result.csv");
		}
		public void ReadFile()
		{
			var listener = new TextWriterTraceListener("LogFile.txt");
			Trace.Listeners.Add(listener);
			Trace.AutoFlush = true;
			try
			{
				if (IsFileExists("file.csv"))
				{
					ribs = new List<int[]>();
					using (StreamReader read = new StreamReader("file.csv"))
					{
						while (!read.EndOfStream)
						{
							int[] row = read.ReadLine().Split(';').Select(int.Parse).ToArray();
							ribs.Add(row);
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
		public void WriteFile(List<int> preuferCode)
		{
			using (StreamWriter writer = new StreamWriter("result.csv", false))
			{
				writer.WriteLine(string.Join(";", preuferCode));
			}
		}
	}
}
