using MahApps.Metro.Controls;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using WpfIoTSimulatorApp.ViewModels;

namespace WpfIoTSimulatorApp.Views
{
    /// <summary>
    /// MainView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainView : MetroWindow
    {
        public MainView()
        {
            InitializeComponent();
        }

        // 뷰상에 있는 이벤트 핸들러를 전부 제거
        // WPF 상의 객체 애니메이션 추가. 애니메이션은 디자이너 역할(View)
        public void StartHmiAni()
        {
            Product.Fill = new SolidColorBrush(Colors.Gray); // 제품을 회색으로 칠하기

            // 기어 애니메이션
            DoubleAnimation ga = new DoubleAnimation();
            ga.From = 0;
            ga.To = 360; // 360도 회전
            ga.Duration = TimeSpan.FromSeconds(5); // 계획 로드타임(Schedules의 LoadTime 값이 들어가야 함)

            RotateTransform rt = new RotateTransform();
            GearStart.RenderTransform = rt;
            GearStart.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            GearEnd.RenderTransform = rt;
            GearEnd.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);

            rt.BeginAnimation(RotateTransform.AngleProperty, ga);

            // 제품 애니메이션
            DoubleAnimation pa = new DoubleAnimation();
            pa.From = 127;
            pa.To = 417; // x축: 센서아래 위치
            pa.Duration = TimeSpan.FromSeconds(3);

            Product.BeginAnimation(Canvas.LeftProperty, pa);

        }

        public void StartSensorCheck()
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                DoubleAnimation sa = new DoubleAnimation
                {
                    From = 1,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(2),
                    AutoReverse = true
                };
                SortingSensor.BeginAnimation(OpacityProperty, sa);
            }));

            Thread.Sleep(1000); // 1초 딜레이

            // 랜덤으로 색상을 결정짓는 작업
            Random rand = new Random();
            int result = rand.Next(1, 4); // 1~3 중 하나 선별

            switch (result)
            {
                case 1:
                    Product.Fill = new SolidColorBrush(Colors.Green); // 양품
                    break;

                case 2:
                    Product.Fill = new SolidColorBrush(Colors.Crimson); // 불량
                    break;

                case 3:
                    Product.Fill = new SolidColorBrush(Colors.Gray); // 선별실패
                    break;
            }
        }
    }
}
