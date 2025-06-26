using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace WpfMrpSimulatorApp.Views
{
    /// <summary>
    /// MonitoringView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MonitoringView : UserControl
    {
        public MonitoringView()
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
            ga.Duration = TimeSpan.FromSeconds(3); // 계획 로드타임(Schedules의 LoadTime 값이 들어가야 함)

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
            DoubleAnimation sa = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(2),
                AutoReverse = true
            };

            SortingSensor.BeginAnimation(OpacityProperty, sa);
        }
    }
}
