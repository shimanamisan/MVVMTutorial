using System;
using System.Collections.Generic;
using System.ComponentModel; // 追加
using System.Linq;
using System.Runtime.CompilerServices; // 追加
using System.Text;
using System.Threading.Tasks;

namespace MVVMTutorial
{
    // モデルはパブリックであり継承する必要がある
    // INotifyPropertyChangedはプロパティの値が変更されたことを通知する
    public class Model : INotifyPropertyChanged
    {

        // イベントハンドラーを追加
        public event PropertyChangedEventHandler PropertyChanged;

        // リアルタイムで表示するコンテンツを追加
        public int Content { get; set; }
        // タイマーを追加
        private static System.Timers.Timer aTimer;

        public Model()
        {
            Content = 1;
            SetTimer();
        }

        // モデルがUIを更新するために必要な処理
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //
        public void SetTimer()
        {
            // タイマーが1秒を超えるとイベントが発生するように設定
            aTimer = new System.Timers.Timer(1000);
            // タイマーがイベントを通知するたびにプロパティの値を変更するようにする
            aTimer.Elapsed += Atimer_Elapsed;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void Atimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Content++;
            // OnPropertyChangedメソッドでコンテンツが変更されたことを通知する
            // そしてUIに反映させる処理をMainWindowに記述する
            OnPropertyChanged("Content");
        }
        

    }
}
