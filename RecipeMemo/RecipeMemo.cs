using Grabacr07.KanColleViewer.Composition;
using Livet;
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
    [ExportMetadata("Version", "2.1")]
    [ExportMetadata("Author", "reniris")]
    public class RecipeMemo : IPlugin, ITool
    {
        //private ViewModel viewModel;

        public string Name => "Recipe";

        object ITool.View => new RecipeMemoView
        {
            DataContext = new RecipeMemoViewModels()
            {
                ViewModels = new ObservableCollection<RecipeMemoViewModel>
                {
                    new RecipeMemoViewModel
                    {
                        model = new RecipeModel(),
                        UpdateLabel = "Wiki URL ",
                        Header = "装備レシピ"
                    },
                    new RecipeMemoViewModel
                    {
                        model = new UserRecipeModel(),
                        UpdateLabel = "ファイル ",
                        Header = "ユーザー定義レシピ"

                    },
                }
            }
        };
        
        public void Initialize()
        {
        }
    }
}
