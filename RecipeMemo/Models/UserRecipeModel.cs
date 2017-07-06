using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;

namespace RecipeMemo.Models
{
    public class UserRecipeModel : RecipeModelBase
    {
        public UserRecipeModel()
        {
            this.DataSource = filename;
        }

        protected override string filename => "UserRecipe.xaml";

        protected override IEnumerable<RecipeItems> Load(bool update)
        {
            if (File.Exists(GetFullPath()) == false)
            {
                return SetDefaltData();
            }
            else
            {
                using (var s = new FileStream(GetFullPath(), FileMode.Open))
                {
                    return XamlServices.Load(s) as List<RecipeItems>;
                }
            }
        }

        private List<RecipeItems> SetDefaltData()
        {
            var list = new List<RecipeItems>();
            var item = new RecipeItems();
            item.Title = "あきつ丸";
            item.RecipeList = new List<RecipeItem>();
            //定番レシピ
            item.RecipeList.Add(new RecipeItem() { fuel = "3000", ammo = "4500", steel = "4500", bauxite = "2000", comment = "定番レシピ" });
            //やや盛ったレシピ
            item.RecipeList.Add(new RecipeItem() { fuel = "3600", ammo = "4500", steel = "4500", bauxite = "2900", comment = "やや盛ったレシピ" });
            //節約レシピ
            item.RecipeList.Add(new RecipeItem() { fuel = "3000", ammo = "4200", steel = "4200", bauxite = "2000", comment = "節約レシピ" });

            list.Add(item);

            using (var w = new FileStream(GetFullPath(), FileMode.Create))
            {
                XamlServices.Save(w, list);
            }

            return list;
        }
    }
}