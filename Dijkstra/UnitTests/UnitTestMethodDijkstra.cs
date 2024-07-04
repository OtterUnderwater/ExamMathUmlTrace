using Dijkstra;

namespace UnitTests
{
	[TestClass]
	public class UnitTestMethodDijkstra
	{
		[TestMethod]
		public void IsFileExists_RealPath_ReturnTrue()
		{
			string path = "file.csv";

			MethodDijkstra methodDijkstra = new MethodDijkstra(); 
			bool result = methodDijkstra.IsFileExists(path);

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void IsFileExists_FakePath_ReturnFalse()
		{
			string path = "notFile.csv";

			MethodDijkstra methodDijkstra = new MethodDijkstra();
			bool result = methodDijkstra.IsFileExists(path);

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void GetListNeedRibs_StartPoint1_ReturnList()
		{
			List<(int start, int end, int count)> testRibs = new List<(int, int, int)>
			{
				{(1, 2, 0)},
				{(2, 4, 0)},
				{(3, 1, 0)},
				{(5, 4, 0)}
			};
			List<(int start, int end, int count)> expected = new List<(int, int, int)>
			{
				{(1, 2, 0)},
				{(3, 1, 0)}
			};

			MethodDijkstra methodDijkstra = new MethodDijkstra();
			methodDijkstra.ribs = testRibs;
			List<(int start, int end, int count)> result = methodDijkstra.GetListNeedRibs(1);

			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GetListNeedRibs_StartPoint2_ReturnList()
		{
			List<(int start, int end, int count)> testRibs = new List<(int, int, int)>
			{
				{(1, 2, 0)},
				{(2, 4, 0)},
				{(3, 1, 0)},
				{(5, 4, 0)}
			};
			List<(int start, int end, int count)> expected = new List<(int, int, int)>
			{
				{(1, 2, 0)},
				{(2, 4, 0)}
			};

			MethodDijkstra methodDijkstra = new MethodDijkstra();
			methodDijkstra.ribs = testRibs;
			List<(int start, int end, int count)> result = methodDijkstra.GetListNeedRibs(2);

			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GetListNeedRibs_StartPoint3_ReturnList()
		{
			List<(int start, int end, int count)> testRibs = new List<(int, int, int)>
			{
				{(1, 2, 0)},
				{(2, 4, 0)},
				{(3, 1, 0)},
				{(5, 4, 0)}
			};
			List<(int start, int end, int count)> expected = new List<(int, int, int)>
			{
				{(3, 1, 0)}
			};

			MethodDijkstra methodDijkstra = new MethodDijkstra();
			methodDijkstra.ribs = testRibs;
			List<(int start, int end, int count)> result = methodDijkstra.GetListNeedRibs(3);

			CollectionAssert.AreEqual(expected, result);
		}

	}
}