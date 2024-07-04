using Northwest;

namespace UnitTests
{
	[TestClass]
	public class UnitTestMetodNorthwest
	{
		[TestMethod]
		public void IsFileExists_CheckFileExists_ReturnTrue()
		{
			string path = "file.csv";

			MetodNorthwest metodNorthwest = new MetodNorthwest();
			bool result = metodNorthwest.IsFileExists(path);

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void IsFileExists_CheckFileExists_ReturnFalse()
		{
			string path = "notFile.csv";

			MetodNorthwest metodNorthwest = new MetodNorthwest();
			bool result = metodNorthwest.IsFileExists(path);

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void IsClose_TaskClose_ReturnTrue()
		{
			List<List<int>> testTariffs = new List<List<int>>
{
	new List<int> { 0, 100, 50 },
	new List<int> { 80, 0, 0 },
	new List<int> { 70, 0, 0 }
};
			MetodNorthwest metodNorthwest = new MetodNorthwest();
			metodNorthwest.tariffs = testTariffs;
			bool result = metodNorthwest.IsClose();

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void IsClose_TaskOpen_ReturnFalse()
		{
			List<List<int>> testTariffs = new List<List<int>>
{
	new List<int> { 0, 100, 50 },
	new List<int> { 60, 0, 0 },
	new List<int> { 50, 0, 0 }
};

			MetodNorthwest metodNorthwest = new MetodNorthwest();
			metodNorthwest.tariffs = testTariffs;
			bool result = metodNorthwest.IsClose();

			Assert.IsFalse(result);
		}

		[TestMethod]
		public void GetStartFreeCells_ListTariffs_ReturnCount()
		{
			List<List<int>> testTariffs = new List<List<int>> { { [0, 100, 50] }, { [60, 0, 0] }, { [50, 0, 0] } };
			int expected = 2;

			MetodNorthwest metodNorthwest = new MetodNorthwest();
			metodNorthwest.tariffs = testTariffs;
			int result = metodNorthwest.GetStartFreeCells().Count;

			Assert.AreEqual(expected, result);
		}

	}
}