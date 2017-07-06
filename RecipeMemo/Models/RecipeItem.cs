using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeMemo.Models
{
    public class RecipeItems
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }
        /// <summary>
        /// Gets the recipe list.
        /// </summary>
        /// <value>
        /// The recipe list.
        /// </value>
        public List<RecipeItem> RecipeList { get; set; }
    }

    public class RecipeItem
    {
        /// <summary>
        /// 燃料
        /// </summary>
        public string fuel { get; set; }

        /// <summary>
        /// 弾薬
        /// </summary>
        public string ammo { get; set; }

        /// <summary>
        /// 鋼材
        /// </summary>
        public string steel { get; set; }

        /// <summary>
        /// ボーキサイト
        /// </summary>
        public string bauxite { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        public string comment { get; set; }

        /// <summary>
        /// ハイライト
        /// </summary>
        public bool highlight { get; set; }
    }
}
