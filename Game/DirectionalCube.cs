using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Windows.Media.Animation;

namespace lab3.Game
{
    public class DirectionalCube : ModelVisual3D
    {
        private readonly RotateTransform3D rt3d = new RotateTransform3D(); //для поворота кубика по свойству Direction
        private readonly TranslateTransform3D tt3d = new TranslateTransform3D(); //для расположения кубика в трёхмерном пространсте по свойству Position
        public DirectionalCube()
        {
            DrawCube(_size);
            SetPosition(_pos);
            SetDirection(_dir);
            Transform = new Transform3DGroup
            {
                Children = { rt3d, tt3d }
            };
        }
        private double _size = 0.5;
        public double Size
        {
            get => _size;
            set
            {
                _size = value;

                DrawCube(_size);
                SetPosition(_pos);
            }
        }

        private Point3D _pos;
        public Point3D Position
        {
            get => _pos;
            set
            {
                _pos = value;
                _pos.X = (int)_pos.X;
                _pos.Y = (int)_pos.Y;
                _pos.Z = (int)_pos.Z;

                SetPosition(_pos);
            }
        }

        private string _dir = "Up";
        public string Direction
        {
            get => _dir;
            set
            {
                _dir = value;
                SetDirection(_dir);
            }
        }

        // Материалы граней
        public ImageBrush _top = new ImageBrush(new BitmapImage(new Uri("dot.jpg", UriKind.Relative)));
        public ImageBrush _side = new ImageBrush(new BitmapImage(new Uri("arrow.jpg", UriKind.Relative)));
        public ImageBrush _bottom = new ImageBrush(new BitmapImage(new Uri("X.jpg", UriKind.Relative)));

        private static GeometryModel3D AddFace(
            Point3D point1,
            Point3D point2,
            Point3D point3,
            Point3D point4,
            Material material)
        {
            GeometryModel3D geometryModel3D = new GeometryModel3D
            {
                Geometry = new MeshGeometry3D()
                {
                    Positions = new Point3DCollection
                    {
                        point1,
                        point2,
                        point3,
                        point3,
                        point4,
                        point1
                    },
                    TextureCoordinates = new PointCollection
                    {
                        new Point(0, 1),
                        new Point(0, 0),
                        new Point(1, 0),
                        new Point(1, 0),
                        new Point(1, 1),
                        new Point(0, 1)
                    }
                }
            };
            geometryModel3D.Material = material;
            return geometryModel3D;
        }


        private void DrawCube(
            double size
            )
        {
            // Отсчёт точек от левого нижнего угла грани.


            // Размерности граней симметричны в обе стороны в абсолютных величинах.
            double absX = size / 2;
            double absY = size / 2;
            double absZ = size / 2;


            Point3D front_left_bottom = new Point3D(-absX, -absY, absZ);
            Point3D front_right_bottom = new Point3D(absX, -absY, absZ);
            Point3D front_right_top = new Point3D(absX, absY, absZ);
            Point3D front_left_top = new Point3D(-absX, absY, absZ);
            Point3D backside_right_top = new Point3D(absX, absY, -absZ);
            Point3D backside_left_top = new Point3D(-absX, absY, -absZ);
            Point3D backside_left_bottom = new Point3D(-absX, -absY, -absZ);
            Point3D backside_right_bottom = new Point3D(absX, -absY, -absZ);


            Model3DGroup m3dg = new Model3DGroup();

            // 1 Передняя
            DiffuseMaterial material = new DiffuseMaterial();
            material.Brush = _side;
            GeometryModel3D faceFront = AddFace(
                    front_left_bottom,
                    front_right_bottom,
                    front_right_top,
                    front_left_top,
                    material);

            m3dg.Children.Add(faceFront);

            // 2 Верхняя

            material = new DiffuseMaterial();
            material.Brush = _top;
            GeometryModel3D faceTop =
                AddFace(
                    front_left_top,
                    front_right_top,
                    backside_right_top,
                    backside_left_top,
                    material);
            m3dg.Children.Add(faceTop);

            // 3 Левая
            material = new DiffuseMaterial();
            material.Brush = _side;
            GeometryModel3D faceLeft =
                AddFace(
                    backside_left_bottom,
                    front_left_bottom,
                    front_left_top,
                    backside_left_top,
                    material);
            m3dg.Children.Add(faceLeft);

            // 4 Правая
            material = new DiffuseMaterial();
            material.Brush = _side;
            GeometryModel3D faceRight =
                AddFace(
                    front_right_bottom,
                    backside_right_bottom,
                    backside_right_top,
                    front_right_top,
                    material);
            m3dg.Children.Add(faceRight);

            // 5 Нижняя
            material = new DiffuseMaterial();
            material.Brush = _bottom;
            GeometryModel3D faceBottom =
                AddFace(
                    backside_left_bottom,
                    backside_right_bottom,
                    front_right_bottom,
                    front_left_bottom,
                    material);
            m3dg.Children.Add(faceBottom);

            // 6 Задняя
            material = new DiffuseMaterial();
            material.Brush = _side;
            GeometryModel3D faceBack =
                AddFace(
                    backside_right_bottom,
                    backside_left_bottom,
                    backside_left_top,
                    backside_right_top,
                    material);
            m3dg.Children.Add(faceBack);
            ModelUIElement3D muie3d = new ModelUIElement3D
            {
                Model = m3dg
            };
            muie3d.MouseDown += Cube_MouseDown;
            Children.Clear();
            Children.Add(muie3d);
        }
        private void SetPosition(Point3D pos)
        {
            tt3d.OffsetX = (pos.X - 0.5) * _size;
            tt3d.OffsetY = (pos.Y - 0.5) * _size;
            tt3d.OffsetZ = (pos.Z - 0.5) * _size;
        }
        private void SetDirection(string dir)
        {
            if (dir == "Down")
                rt3d.Rotation = new AxisAngleRotation3D(new Vector3D(1, 0, 0), 180);
            else if (dir == "Left")
                rt3d.Rotation = new AxisAngleRotation3D(new Vector3D(0, 0, 1), 90);
            else if (dir == "Right")
                rt3d.Rotation = new AxisAngleRotation3D(new Vector3D(0, 0, 1), -90);
            else if (dir == "Forward")
                rt3d.Rotation = new AxisAngleRotation3D(new Vector3D(1, 0, 0), -90);
            else if (dir == "Backward")
                rt3d.Rotation = new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90);
            else
                rt3d.Rotation = new AxisAngleRotation3D(new Vector3D(0, 0, 0), 0);
        }

        private bool moved = false;
        private static bool[,,] freeCells = new bool[2, 2, 2];
        private void Cube_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (moved) return;
            DoubleAnimation animation = new DoubleAnimation();
            animation.Duration = TimeSpan.FromSeconds(3);
            if (_dir == "Down")
            {
                if (_pos.Y > 0 && !freeCells[(int)_pos.X, 0, (int)_pos.Z])
                    return;
                animation.To = -10;
                tt3d.BeginAnimation(TranslateTransform3D.OffsetYProperty, animation);
            }
            else if (_dir == "Left")
            {
                if (_pos.X > 0 && !freeCells[0, (int)_pos.Y, (int)_pos.Z])
                    return;
                animation.To = -10;
                tt3d.BeginAnimation(TranslateTransform3D.OffsetXProperty, animation);
            }
            else if (_dir == "Right")
            {
                if (_pos.X < 1 && !freeCells[1, (int)_pos.Y, (int)_pos.Z])
                    return;
                animation.To = 10;
                tt3d.BeginAnimation(TranslateTransform3D.OffsetXProperty, animation);
            }
            else if (_dir == "Forward")
            {
                if (_pos.Z > 0 && !freeCells[(int)_pos.X, (int)_pos.Y, 0])
                    return;
                animation.To = -10;
                tt3d.BeginAnimation(TranslateTransform3D.OffsetZProperty, animation);
            }
            else if (_dir == "Backward")
            {
                if (_pos.Z < 1 && !freeCells[(int)_pos.X, (int)_pos.Y, 1])
                    return;
                animation.To = 10;
                tt3d.BeginAnimation(TranslateTransform3D.OffsetZProperty, animation);
            }
            else
            {
                if (_pos.Y < 1 && !freeCells[(int)_pos.X, 1, (int)_pos.Z])
                    return;
                animation.To = 10;
                tt3d.BeginAnimation(TranslateTransform3D.OffsetYProperty, animation);
            }
            moved = true;
            freeCells[(int)_pos.X, (int)_pos.Y, (int)_pos.Z] = true;
        }
    }
}
