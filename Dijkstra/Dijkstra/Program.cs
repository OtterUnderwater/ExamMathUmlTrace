namespace Dijkstra
{
	public class Program
	{
		static void Main()
		{
			MethodDijkstra methodDijkstra = new MethodDijkstra();
			Console.Write("Введите имя файла *.csv в котором находится список ребер с весом: ");
			string path = Console.ReadLine();
			Console.Write("Введите вершину, от которой найти кратчайшие пути: ");
			int startPoint = Convert.ToInt32(Console.ReadLine());
			methodDijkstra.Dijkstra(path, startPoint);
		}
	}
}
