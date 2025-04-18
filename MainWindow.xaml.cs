using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //private void ringVisual_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    Point location = e.GetPosition(viewport);

        //    RayMeshGeometry3DHitTestResult meshHitResult =
        //      (RayMeshGeometry3DHitTestResult)VisualTreeHelper.HitTest(viewport, location);

        //    axisRotation.Axis = new Vector3D(-meshHitResult.PointHit.Y, meshHitResult.PointHit.X, 0);
        //    DoubleAnimation animation = new DoubleAnimation();
        //    animation.From = 0;
        //    animation.To = 720;
        //    animation.DecelerationRatio = 1;
        //    animation.Duration = TimeSpan.FromSeconds(3);
        //    animation.AutoReverse = true;
        //    axisRotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, animation);
        //    DoubleAnimation animationZ = new DoubleAnimation();
        //    animationZ.To = -10;
        //    animationZ.DecelerationRatio = 1;
        //    animationZ.Duration = TimeSpan.FromSeconds(3);
        //    animationZ.AutoReverse = true;
        //    translate.BeginAnimation(TranslateTransform3D.OffsetZProperty, animationZ);
        //    DoubleAnimation animationX = new DoubleAnimation();
        //    animationX.To = 3.5;
        //    animationX.DecelerationRatio = 1;
        //    animationX.Duration = TimeSpan.FromSeconds(3);
        //    animationX.AutoReverse = true;
        //    translate.BeginAnimation(TranslateTransform3D.OffsetXProperty, animationX);
        //    DoubleAnimation animationY = new DoubleAnimation();
        //    animationY.To = 0.5;
        //    animationY.DecelerationRatio = 1;
        //    animationY.Duration = TimeSpan.FromSeconds(3);
        //    animationY.AutoReverse = true;
        //    translate.BeginAnimation(TranslateTransform3D.OffsetYProperty, animationX);
        //}
    }
}
