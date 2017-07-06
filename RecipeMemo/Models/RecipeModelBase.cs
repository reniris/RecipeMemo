using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using System.Collections.ObjectModel;
using System.IO;

namespace RecipeMemo.Models
{
    public abstract class RecipeModelBase : NotificationObject
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */

        #region DataSource変更通知プロパティ
        private string _DataSource;

        public string DataSource
        {
            get
            { return _DataSource; }
            set
            {
                if (_DataSource == value)
                    return;
                _DataSource = value;
                RaisePropertyChanged(nameof(DataSource));
            }
        }
        #endregion

        protected abstract string filename { get; }

        public virtual ObservableCollection<RecipeItems> Update()
        {
            return new ObservableCollection<RecipeItems>(this.Load(true));
        }

        public virtual ObservableCollection<RecipeItems> Init()
        {
            return new ObservableCollection<RecipeItems>(this.Load(false));
        }

        protected abstract IEnumerable<RecipeItems> Load(bool update);

        protected virtual string GetFullPath()
        {
            var fullpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
            return fullpath;
        }
    }
}
