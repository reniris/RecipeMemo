using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using System.Net;
using System.IO;
using System.Net.Http;
using Sgml;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace RecipeMemo.Models
{
    public class RecipeModel : RecipeModelBase
    {
        private const string def_url = "http://wikiwiki.jp/kancolle/?%B3%AB%C8%AF%A5%EC%A5%B7%A5%D4";

        public RecipeModel()
        {
            this.DataSource = def_url;
        }

        protected override string filename => "Recipe.xml";

        /// <summary>
        /// Parses this instance.
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<RecipeItems> Load(bool update)
        {
            XDocument xml = HtmlToXml(update, this.DataSource);

            var ns = xml.Root.Name.Namespace;
            var titles = xml.Descendants(ns + "h3")
                .Where(e => e.HasElements)
                .Select(i => i.Value).ToList();

            //<div class="ie5">
            var tables = xml.Descendants(ns + "div")
                .Where(e => e.HasAttributes)
                .Where(e => e.FirstAttribute.Value == "ie5")
                .Descendants(ns + "table");

            // 
            var rgroup = new List<List<RecipeItem>>();
            foreach (var item in tables)
            {
                var list = item.Descendants(ns + "tr")
                .Select(e => e.Elements(ns + "td"))
                .Skip(1)
                .Select(es => new RecipeItem
                {
                    //燃料 弾薬 鋼材 ボーキ 備考 
                    fuel = es.ElementAt(0).Value,
                    ammo = es.ElementAt(1).Value,
                    steel = es.ElementAt(2).Value,
                    bauxite = es.ElementAt(3).Value,
                    comment = es.Count() > 4 ? es.Last().Value : "",
                    highlight = es.Attributes().Any(e => e.Value.Contains("background-color"))
                }).ToList();

                rgroup.Add(list);
            }

            //いらないのを飛ばす
            var data = rgroup.Take(titles.Count() - 1).ToList();
            data.Add(rgroup.Last());

            var ret = data.Select((item, i) => new RecipeItems
            {
                Title = titles[i],
                RecipeList = item
            });

            return ret;
        }

        /// <summary>
        /// HTMLs to XML.
        /// </summary>
        /// <param name="update">強制的に更新するか</param>
        /// <returns></returns>
        private XDocument HtmlToXml(bool update, string url)
        {
            //キャッシュのパス
            var fullpath = GetFullPath();
            XDocument xml = null;

            //キャッシュがある場合はそこから読む（強制更新しない場合）
            if (update == false && File.Exists(fullpath) == true)
            {
                xml = XDocument.Load(fullpath);
            }
            else
            {
                using (var sgml = new SgmlReader() { Href = url, IgnoreDtd = true })
                {
                    xml = XDocument.Load(sgml); // たった3行でHtml to Xml
                }
                xml.Save(fullpath);
            }

            return xml;
        }
    }
}
