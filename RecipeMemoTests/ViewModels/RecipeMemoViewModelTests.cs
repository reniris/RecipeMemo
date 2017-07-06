using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeMemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeMemo.ViewModels.Tests
{
    [TestClass()]
    public class RecipeMemoViewModelTests
    {
        [TestMethod()]
        public void InitializeTest()
        {
            var vm = new RecipeMemoViewModel();
            Console.WriteLine(vm.DataSource);
        }
    }
}