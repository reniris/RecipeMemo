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
    public class UserRecipeModelTests
    {
        [TestMethod()]
        public void InitTest()
        {
            var m = new UserRecipeModel();
            var list = m.Init();

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