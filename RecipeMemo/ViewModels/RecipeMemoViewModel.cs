﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using System.Collections.ObjectModel;
using RecipeMemo.Models;

namespace RecipeMemo.ViewModels
{
    public class RecipeMemoViewModel : ViewModel
    {
        /* コマンド、プロパティの定義にはそれぞれ 
         * 
         *  lvcom   : ViewModelCommand
         *  lvcomn  : ViewModelCommand(CanExecute無)
         *  llcom   : ListenerCommand(パラメータ有のコマンド)
         *  llcomn  : ListenerCommand(パラメータ有のコマンド・CanExecute無)
         *  lprop   : 変更通知プロパティ(.NET4.5ではlpropn)
         *  
         * を使用してください。
         * 
         * Modelが十分にリッチであるならコマンドにこだわる必要はありません。
         * View側のコードビハインドを使用しないMVVMパターンの実装を行う場合でも、ViewModelにメソッドを定義し、
         * LivetCallMethodActionなどから直接メソッドを呼び出してください。
         * 
         * ViewModelのコマンドを呼び出せるLivetのすべてのビヘイビア・トリガー・アクションは
         * 同様に直接ViewModelのメソッドを呼び出し可能です。
         */

        /* ViewModelからViewを操作したい場合は、View側のコードビハインド無で処理を行いたい場合は
         * Messengerプロパティからメッセージ(各種InteractionMessage)を発信する事を検討してください。
         */

        /* Modelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedEventListenerや
         * CollectionChangedEventListenerを使うと便利です。各種ListenerはViewModelに定義されている
         * CompositeDisposableプロパティ(LivetCompositeDisposable型)に格納しておく事でイベント解放を容易に行えます。
         * 
         * ReactiveExtensionsなどを併用する場合は、ReactiveExtensionsのCompositeDisposableを
         * ViewModelのCompositeDisposableプロパティに格納しておくのを推奨します。
         * 
         * LivetのWindowテンプレートではViewのウィンドウが閉じる際にDataContextDisposeActionが動作するようになっており、
         * ViewModelのDisposeが呼ばれCompositeDisposableプロパティに格納されたすべてのIDisposable型のインスタンスが解放されます。
         * 
         * ViewModelを使いまわしたい時などは、ViewからDataContextDisposeActionを取り除くか、発動のタイミングをずらす事で対応可能です。
         */

        /* UIDispatcherを操作する場合は、DispatcherHelperのメソッドを操作してください。
         * UIDispatcher自体はApp.xaml.csでインスタンスを確保してあります。
         * 
         * LivetのViewModelではプロパティ変更通知(RaisePropertyChanged)やDispatcherCollectionを使ったコレクション変更通知は
         * 自動的にUIDispatcher上での通知に変換されます。変更通知に際してUIDispatcherを操作する必要はありません。
         */


        #region Recipes変更通知プロパティ
        private ObservableCollection<RecipeItems> _Recipes;

        public ObservableCollection<RecipeItems> Recipes
        {
            get
            { return _Recipes; }
            set
            {
                if (_Recipes == value)
                    return;
                _Recipes = value;
                RaisePropertyChanged();
            }
        }
        #endregion

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

        #region UpdateLabel変更通知プロパティ
        private string _UpdateLabel;

        public string UpdateLabel
        {
            get
            { return _UpdateLabel; }
            set
            {
                if (_UpdateLabel == value)
                    return;
                _UpdateLabel = value;
                RaisePropertyChanged(nameof(UpdateLabel));
            }
        }
        #endregion
        
        #region Header変更通知プロパティ
        private string _Header;

        public string Header
        {
            get
            { return _Header; }
            set
            {
                if (_Header == value)
                    return;
                _Header = value;
                RaisePropertyChanged(nameof(Header));
            }
        }
        #endregion

        #region modelプロパティ
        private RecipeModelBase _model;

        public RecipeModelBase model
        {
            get
            { return _model; }
            set
            {
                if (_model == value)
                    return;
                _model = value;

                InitProp();
            }
        }
        #endregion

        PropertyChangedEventListener listener;

        public RecipeMemoViewModel()
        {

        }

        public void InitProp()
        {
            this.DataSource = model.DataSource;
            Recipes = model.Init();
        }

        public void Initialize()
        {
            listener = new PropertyChangedEventListener(model) {
                () => model.DataSource, (_, __) => RaisePropertyChanged(() => DataSource)
            };
        }

        #region UpdateCommand
        private ViewModelCommand _UpdateCommand;

        public ViewModelCommand UpdateCommand
        {
            get
            {
                if (_UpdateCommand == null)
                {
                    _UpdateCommand = new ViewModelCommand(Update);
                }
                return _UpdateCommand;
            }
        }

        public void Update()
        {
            Recipes = model.Update();
        }

        #endregion

    }

    public class RecipeMemoViewModels : ViewModel
    {
        #region ViewModels変更通知プロパティ
        private ObservableCollection<RecipeMemoViewModel> _ViewModels;

        public ObservableCollection<RecipeMemoViewModel> ViewModels
        {
            get
            { return _ViewModels; }
            set
            {
                if (_ViewModels == value)
                    return;
                _ViewModels = value;
                RaisePropertyChanged(nameof(ViewModels));
            }
        }
        #endregion

        #region SelectedIndex変更通知プロパティ
        private int _SelectedIndex = -1;

        public int SelectedIndex
        {
            get
            { return _SelectedIndex; }
            set
            {
                if (_SelectedIndex == value)
                    return;
                _SelectedIndex = value;
                RaisePropertyChanged(nameof(SelectedIndex));
            }
        }
        #endregion

        public void Initialize()
        {
            foreach (var vm in this.ViewModels)
            {
                vm.Initialize();

                this.SelectedIndex = 0;
            }
        }
    }
}
