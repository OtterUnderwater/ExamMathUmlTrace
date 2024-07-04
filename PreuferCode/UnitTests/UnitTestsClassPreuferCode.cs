using PreuferCode;

namespace UnitTests
{
	[TestClass]
	public class UnitTestsClassPreuferCode
	{
		[TestMethod]
		public void IsFileExists_CheckFileExists_ReturnTrue()
		{
			string path = "file.csv";

			ClassPreuferCode classPreuferCode = new ClassPreuferCode();
			bool result = classPreuferCode.IsFileExists(path);

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void IsFileExists_CheckFileExists_ReturnFalse()
		{
			string path = "notFile.csv";

			ClassPreuferCode classPreuferCode = new ClassPreuferCode();
			bool result = classPreuferCode.IsFileExists(path);

			Assert.IsFalse(result);		
		}

		[TestMethod]
		public void GetListLeaves_ListRibs_ReturnList245()
		{
			List<int[]> testRibs = new List<int[]> { { [1, 2] }, { [1, 3] }, { [3, 4] }, { [5, 3] }, };
			List<int> expected = new List<int> { { 2 }, { 4 }, { 5 } };

			ClassPreuferCode classPreuferCode = new ClassPreuferCode();
			classPreuferCode.ribs = testRibs;
			List<int> result = classPreuferCode.GetListLeaves();

			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GetListLeaves_ListRibs_ReturnList45()
		{
			List<int[]> testRibs = new List<int[]> { { [1, 2] }, { [2, 3] }, { [1, 4] }, { [4, 5] } };
			List<int> expected = new List<int> { { 3 }, { 5 } };

			ClassPreuferCode classPreuferCode = new ClassPreuferCode();
			classPreuferCode.ribs = testRibs;
			List<int> result = classPreuferCode.GetListLeaves();

			CollectionAssert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GetPreuferCode_ListRibs_ReturnList45()
		{
			List<int> expected = new List<int> { { 1 }, { 5 }, { 2 }, { 6 }, { 6 }, { 2 }, { 1 }, { 3 } };

			ClassPreuferCode classPreuferCode = new ClassPreuferCode();
			classPreuferCode.GetPreuferCode();
			List<int> result = classPreuferCode.code;

			CollectionAssert.AreEqual(expected, result);
		}
	}
}