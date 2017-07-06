using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeMemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeMemo.Tests
{
    [TestClass()]
    public class RecipeMemoTests
    {
        [TestMethod()]
        public void InitializeTest()
        {
            var r = new RecipeMemo();
            r.Initialize();

            Console.WriteLine();
        }
    }
}