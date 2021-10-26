using MathLibrary;
using System.Collections.Generic;
using System.Windows;
using static MathLibrary.DrawingPipeline;
using static MathLibrary.MathVectors;

namespace MathLibraryDriver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Vec3D vec1 = new Vec3D(0.0f, 10.0f, 10.0f);
            Vec3D vec2 = new Vec3D(100.0f, 0.0f, -5.0f);
            Vec3D vec3 = new Vec3D(50.0f, 40.0f, 20.0f);

            Vec2D t1 = new Vec2D(0.0f, 0.0f, 0.0f);
            Vec2D t2 = new Vec2D(400.0f, 0.0f, 0.0f);
            Vec2D t3 = new Vec2D(200.0f, 200.0f, 0.0f);

            Pixel pixel1 = new Pixel(120, 120, 120);
            Pixel pixel2 = new Pixel(60, 60, 60);
            Pixel pixel3 = new Pixel(180, 180, 180);


            Triangle tri1 = new Triangle();
            tri1.p[0] = vec1;
            tri1.p[1] = vec2;
            tri1.p[2] = vec3;
            tri1.t[0] = t1;
            tri1.t[1] = t2;
            tri1.t[2] = t3;
            tri1.col[0] = pixel1;
            tri1.col[1] = pixel2;
            tri1.col[2] = pixel3;

            List<Triangle> list = new List<Triangle>();
            list.Add(tri1);

            DrawingPipeline pipeline = new DrawingPipeline(MainCanvas);
            Mat4x4 transform = new Mat4x4();
            transform = MathOps.Mat_MakeIdentity();

            pipeline.SetTransform(transform);

            Vec3D camera_pos = new Vec3D(0.0f, 0.0f, 1000.0f);
            Vec3D target = vec1;
            Vec3D up = new Vec3D(0.0f, 1.0f, 0.0f);
            pipeline.SetCamera(camera_pos, target, up);

            pipeline.SetProjection(45, 1.0f, 0.1f, 1000, 0.0f, 0.0f, (float) MainCanvas.Width, (float) MainCanvas.Height);

            pipeline.Render(list, RENDERFLAGS.RENDER_CULL_CCW | RENDERFLAGS.RENDER_WIRE, 0, 1);

        }
    }
}
