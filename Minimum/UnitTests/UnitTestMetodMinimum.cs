using Minimum;
using System.IO;

namespace UnitTests
{
	[TestClass]
	public class UnitTestMetodMinimum
	{
		[TestMethod]
		public void IsFileExists_CheckFileExists_ReturnTrue()
		{
			string path = "file.csv";

			MetodMinimum metodMinimum = new MetodMinimum();
			bool result = metodMinimum.IsFileExists(path);

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void IsFileExists_CheckFileExists_ReturnFalse()
		{
			string path = "notFile.csv";

			MetodMinimum metodMinimum = new MetodMinimum();
			bool result = metodMinimum.IsFileExists(path);

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void Is—losed_TaskClose_ReturnTrue()
		{
			List<List<int>> testTariffs = new List<List<int>>
			{
				new List<int> { 0, 100, 50 },
				new List<int> { 80, 0, 0 },
				new List<int> { 70, 0, 0 }
			};
			MetodMinimum metodMinimum = new MetodMinimum();
			metodMinimum.tariffs = testTariffs;
			bool result = metodMinimum.Is—losed();

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void Is—losed_TaskOpen_ReturnFalse()
		{
			List<List<int>> testTariffs = new List<List<int>>
			{
				new List<int> { 0, 100, 50 },
				new List<int> { 60, 0, 0 },
				new List<int> { 50, 0, 0 }
			};

			MetodMinimum metodMinimum = new MetodMinimum();
			metodMinimum.tariffs = testTariffs;
			bool result = metodMinimum.Is—losed();

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void GetStartFreeCells_ListTariffs_ReturnCount()
		{
			List<List<int>> testTariffs = new List<List<int>> { { [0, 100, 50] }, { [60, 0, 0] }, { [50, 0, 0] } };
			int expected = 2;

			MetodMinimum metodMinimum = new MetodMinimum();
			metodMinimum.tariffs = testTariffs;
			int result = metodMinimum.GetStartFreeCells().Count;
			
			Assert.AreEqual(expected, result);
		}
	}
}