using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeMemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeMemo.Models.Tests
{
	[TestClass()]
	public class RecipeModelTests
	{
		[TestMethod()]
		public void ParseTest()
		{
			var m = new RecipeModel();
			var list = m.Parse(false);

			foreach (var item in list)
			{
				Console.WriteLine(item.Title);
				foreach (var r in item.RecipeList)
				{
					Console.WriteLine(r.comment);
				}
			}
		}
	}
}