using Grabacr07.KanColleViewer.Composition;
using RecipeMemo.Models;
using RecipeMemo.View;
using RecipeMemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeMemo
{
    [Export(typeof(IPlugin))]
    [Export(typeof(ITool))]
    [ExportMetadata("Guid", "42ED0309-9AE0-48C5-846A-1AE125678737")]
    [ExportMetadata("Title", "RecipeMemo")]
    [ExportMetadata("Description", "開発レシピを表示します")]
    [ExportMetadata("Version", "1.0")]
    [ExportMetadata("Author", "reniris")]
    class RecipeMemo : IPlugin, ITool
    {
        private RecipeMemoViewModel viewModel;

        public string Name => "Recipe";

        object ITool.View => new RecipeMemoView { DataContext = this.viewModel, };

        public void Initialize()
        {
            this.viewModel = new RecipeMemoViewModel();
        }
    }
}
