using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLibrary
{
    public class MathVectors
    {
        public class Vec2D
        {
            float X = 0;
            float Y = 0;
            float Z = 0;
        }

        public class Vec3D
        {
            public float X { get => v[0]; set { v[0] = value; } }
            public float Y { get => v[1]; set { v[1] = value; } }
            public float Z { get => v[2]; set { v[2] = value; } }
            public float W { get => v[3]; set { v[3] = value; } } // needs a 4th term to work common 4x4 matrix multiplication

            float[] v = new float[4] { 0, 0, 0, 1 };
        }

        public class Mat4x4
        {
            public float[,] m = new float[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };

            /// <summary>
            /// First row
            /// </summary>
            public float M00 { get => m[0,0]; set { m[0, 0] = value; } }
            public float M01 { get => m[0,1]; set { m[0, 1] = value; } }
            public float M02 { get => m[0,2]; set { m[0, 2] = value; } }
            public float M03 { get => m[0,3]; set { m[0, 3] = value; } }

            /// <summary>
            /// Second row
            /// </summary>
            public float M10 { get => m[1, 0]; set { m[1, 0] = value; } }
            public float M11 { get => m[1, 1]; set { m[1, 1] = value; } }
            public float M12 { get => m[1, 2]; set { m[1, 2] = value; } }
            public float M13 { get => m[1, 3]; set { m[1, 3] = value; } }
            
            /// <summary>
            /// Third row
            /// </summary>
            public float M20 { get => m[2, 0]; set { m[2, 0] = value; } }
            public float M21 { get => m[2, 1]; set { m[2, 1] = value; } }
            public float M22 { get => m[2, 2]; set { m[2, 2] = value; } }
            public float M23 { get => m[2, 3]; set { m[2, 3] = value; } }

            /// <summary>
            /// First row
            /// </summary>
            public float M30 { get => m[3, 0]; set { m[3, 0] = value; } }
            public float M31 { get => m[3, 1]; set { m[3, 1] = value; } }
            public float M32 { get => m[3, 2]; set { m[3, 2] = value; } }
            public float M33 { get => m[3, 3]; set { m[3, 3] = value; } }
        }

        public static class MathOps
        {
            public static Vec3D Mat_MultiplayVector(Mat4x4 m, Vec3D i)
            {
                Vec3D v = new Vec3D();
                v.X = i.X * m.M00 + i.Y * m.M10 + i.Z * m.M20 + i.W * m.M30;
                v.Y = i.X * m.M01 + i.Y * m.M11 + i.Z * m.M21 + i.W * m.M31;
                v.Z = i.X * m.M02 + i.Y * m.M12 + i.Z * m.M22 + i.W * m.M32;
                v.W = i.X * m.M03 + i.Y * m.M13 + i.Z * m.M23 + i.W * m.M33;
                return v;
            }

            public static Mat4x4 Mat_MultiplyMatrix(Mat4x4 m1, Mat4x4 m2)
            {
                Mat4x4 matrix = new Mat4x4();
                for (int c = 0; c < 4; c++)
                    for (int r = 0; r < 4; r++)
                        matrix.m[r,c] = m1.m[r,0] * m2.m[0,c] + m1.m[r,1] * m2.m[1,c] + m1.m[r,2] * m2.m[2,c] + m1.m[r,3] * m2.m[3,c];
                return matrix;
            }

            public static Mat4x4 Mat_MakeIdentity()
            {
                Mat4x4 matrix = new Mat4x4();
                matrix.M00 = 1.0f;
                matrix.M11 = 1.0f;
                matrix.M22 = 1.0f;
                matrix.M33 = 1.0f;
                return matrix;
            }

            public static Mat4x4 Mat_MakeRotationX(float fAngleRad)
            {
                Mat4x4 matrix = new Mat4x4();
                matrix.M00 = 1.0f;
                matrix.M11 = (float)Math.Cos(fAngleRad);
                matrix.M12 = (float)Math.Sin(fAngleRad);
                matrix.M21 = (float)(-Math.Sin(fAngleRad));
                matrix.M22 = (float)Math.Cos(fAngleRad);
                matrix.M33 = 1.0f;
                return matrix;
            }

            public static Mat4x4 Mat_MakeRotationY(float fAngleRad)
            {
                Mat4x4 matrix = new Mat4x4();
                matrix.M00 = (float)Math.Cos(fAngleRad);
                matrix.M01 = (float)Math.Sin(fAngleRad);
                matrix.M10 = (float)(-Math.Sin(fAngleRad));
                matrix.M11 = 1.0f;
                matrix.M22 = (float)Math.Cos(fAngleRad);
                matrix.M33 = 1.0f;
                return matrix;
            }

            public static Mat4x4 Mat_MakeRotationZ(float fAngleRad)
            {
                Mat4x4 matrix = new Mat4x4();
                matrix.M00 = (float)Math.Cos(fAngleRad);
                matrix.M01 = (float)Math.Sin(fAngleRad);
                matrix.M10 = (float)(-Math.Sin(fAngleRad));
                matrix.M11 = (float)Math.Cos(fAngleRad);
                matrix.M22 = 1.0f;
                matrix.M33 = 1.0f;
                return matrix;
            }

            public static Mat4x4 Mat_MakeScale(float x, float y, float z)
            {
                Mat4x4 matrix = new Mat4x4();
                matrix.M00 = x;
                matrix.M11 = y;
                matrix.M22 = z;
                matrix.M33 = 1.0f;
                return matrix;
            }

            public static Mat4x4 Mat_MakeTranslation(float x, float y, float z)
            {
                Mat4x4 matrix = new Mat4x4();
                matrix.M00 = 1.0f;
                matrix.M11 = 1.0f;
                matrix.M33 = 1.0f;
                matrix.M33 = 1.0f;
                matrix.M30 = x;
                matrix.M31 = y;
                matrix.M32 = z;
                return matrix;
            }

            public static Mat4x4 Mat_MakeProjection(float fFovDegrees, float fAspectRatio, float fNear, float fFar)
            {
                float fFovRad = 1.0f / ((float)(Math.Tan(fFovDegrees * 0.5f / 180.0f * 3.14159f)));
                Mat4x4 matrix = new Mat4x4();
                matrix.M00 = fAspectRatio * fFovRad;
                matrix.M11 = fFovRad;
                matrix.M22 = fFar / (fFar - fNear);
                matrix.M33 = (-fFar * fNear) / (fFar - fNear);
                matrix.M23 = 1.0f;
                matrix.M33 = 0.0f;
                return matrix;
            }

            public static Mat4x4 Mat_PointAt(Vec3D pos, Vec3D target, Vec3D up)
            {
                // Calculate new forward direction
                Vec3D newForward = Vec_Sub(target, pos);
                newForward = Vec_Normalize(newForward);

                // Calculate new Up direction
                Vec3D a = Vec_Mul(newForward, Vec_DotProduct(up, newForward));
                Vec3D newUp = Vec_Sub(up, a);
                newUp = Vec_Normalize(newUp);

                // New Right direction is easy, its just cross product
                Vec3D newRight = Vec_CrossProduct(newUp, newForward);

                // Construct Dimensioning and Translation Matrix	
                Mat4x4 matrix = new Mat4x4();
                matrix.M00 = newRight.X; matrix.M01 = newRight.Y; matrix.M02 = newRight.Z; matrix.M03 = 0.0f;
                matrix.M10 = newUp.X; matrix.M11 = newUp.Y; matrix.M12 = newUp.Z; matrix.M13 = 0.0f;
                matrix.M20 = newForward.X; matrix.M21 = newForward.Y; matrix.M22 = newForward.Z; matrix.M23 = 0.0f;
                matrix.M30 = pos.X; matrix.M31 = pos.Y; matrix.M32 = pos.Z; matrix.M33 = 1.0f;
                return matrix;
            }

            public static Mat4x4 Mat_QuickInverse(Mat4x4 m)  // only for rotation / translation matrices
            {
                Mat4x4 matrix = new Mat4x4();
                matrix.M00 = m.M00; matrix.M01 = m.M10; matrix.M02 = m.M20; matrix.M03 = 0.0f;
                matrix.M10 = m.M01; matrix.M11 = m.M11; matrix.M12 = m.M21; matrix.M13 = 0.0f;
                matrix.M20 = m.M02; matrix.M21 = m.M12; matrix.M22 = m.M22; matrix.M23 = 0.0f;
                matrix.M30 = -(m.M30 * matrix.M00 + m.M31 * matrix.M10 + m.M32 * matrix.M20);
                matrix.M31 = -(m.M30 * matrix.M01 + m.M31 * matrix.M11 + m.M32 * matrix.M21);
                matrix.M32 = -(m.M30 * matrix.M02 + m.M31 * matrix.M12 + m.M32 * matrix.M22);
                matrix.M33 = 1.0f;
                return matrix;
            }

            public static Mat4x4 Mat_Inverse(Mat4x4 m)
            {
                double det;

                Mat4x4 matInv = new Mat4x4();

                matInv.m[0,0] =  m.m[1,1] * m.m[2,2] * m.m[3,3] - m.m[1,1] * m.m[2,3] * m.m[3,2] - m.m[2,1] * m.m[1,2] * m.m[3,3] + m.m[2,1] * m.m[1,3] * m.m[3,2] + m.m[3,1] * m.m[1,2] * m.m[2,3] - m.m[3,1] * m.m[1,[3] * m.m[2,2];
                matInv.m[1,0] = -m.m[1,0] * m.m[2,2] * m.m[3,3] + m.m[1,0] * m.m[2,3] * m.m[3,2] + m.m[2,0] * m.m[1,2] * m.m[3,3] - m.m[2,0] * m.m[1,3] * m.m[3,2] - m.m[3,0] * m.m[1,2] * m.m[2,3] + m.m[3,0] * m.m[1,[3] * m.m[2,2];
                matInv.m[2,0] =  m.m[1,0] * m.m[2,1] * m.m[3,3] - m.m[1,0] * m.m[2,3] * m.m[3,1] - m.m[2,0] * m.m[1,1] * m.m[3,3] + m.m[2,0] * m.m[1,3] * m.m[3,1] + m.m[3,0] * m.m[1,1] * m.m[2,3] - m.m[3,0] * m.m[1,[3] * m.m[2,1];
                matInv.m[3,0] = -m.m[1,0] * m.m[2,1] * m.m[3,2] + m.m[1,0] * m.m[2,2] * m.m[3,1] + m.m[2,0] * m.m[1,1] * m.m[3,2] - m.m[2,0] * m.m[1,2] * m.m[3,1] - m.m[3,0] * m.m[1,1] * m.m[2,2] + m.m[3,0] * m.m[1,[2] * m.m[2,1];
                matInv.m[0,1] = -m.m[0,1] * m.m[2,2] * m.m[3,3] + m.m[0,1] * m.m[2,3] * m.m[3,2] + m.m[2,1] * m.m[0,2] * m.m[3,3] - m.m[2,1] * m.m[0,3] * m.m[3,2] - m.m[3,1] * m.m[0,2] * m.m[2,3] + m.m[3,1] * m.m[0,[3] * m.m[2,2];
                matInv.m[1,1] =  m.m[0,0] * m.m[2,2] * m.m[3,3] - m.m[0,0] * m.m[2,3] * m.m[3,2] - m.m[2,0] * m.m[0,2] * m.m[3,3] + m.m[2,0] * m.m[0,3] * m.m[3,2] + m.m[3,0] * m.m[0,2] * m.m[2,3] - m.m[3,0] * m.m[0,[3] * m.m[2,2];
                matInv.m[2,1] = -m.m[0,0] * m.m[2,1] * m.m[3,3] + m.m[0,0] * m.m[2,3] * m.m[3,1] + m.m[2,0] * m.m[0,1] * m.m[3,3] - m.m[2,0] * m.m[0,3] * m.m[3,1] - m.m[3,0] * m.m[0,1] * m.m[2,3] + m.m[3,0] * m.m[0,[3] * m.m[2,1];
                matInv.m[3,1] =  m.m[0,0] * m.m[2,1] * m.m[3,2] - m.m[0,0] * m.m[2,2] * m.m[3,1] - m.m[2,0] * m.m[0,1] * m.m[3,2] + m.m[2,0] * m.m[0,2] * m.m[3,1] + m.m[3,0] * m.m[0,1] * m.m[2,2] - m.m[3,0] * m.m[0,[2] * m.m[2,1];
                matInv.m[0,2] =  m.m[0,1] * m.m[1,2] * m.m[3,3] - m.m[0,1] * m.m[1,3] * m.m[3,2] - m.m[1,1] * m.m[0,2] * m.m[3,3] + m.m[1,1] * m.m[0,3] * m.m[3,2] + m.m[3,1] * m.m[0,2] * m.m[1,3] - m.m[3,1] * m.m[0,[3] * m.m[1,2];
                matInv.m[1,2] = -m.m[0,0] * m.m[1,2] * m.m[3,3] + m.m[0,0] * m.m[1,3] * m.m[3,2] + m.m[1,0] * m.m[0,2] * m.m[3,3] - m.m[1,0] * m.m[0,3] * m.m[3,2] - m.m[3,0] * m.m[0,2] * m.m[1,3] + m.m[3,0] * m.m[0,[3] * m.m[1,2];
                matInv.m[2,2] =  m.m[0,0] * m.m[1,1] * m.m[3,3] - m.m[0,0] * m.m[1,3] * m.m[3,1] - m.m[1,0] * m.m[0,1] * m.m[3,3] + m.m[1,0] * m.m[0,3] * m.m[3,1] + m.m[3,0] * m.m[0,1] * m.m[1,3] - m.m[3,0] * m.m[0,[3] * m.m[1,1];
                matInv.m[3,2] = -m.m[0,0] * m.m[1,1] * m.m[3,2] + m.m[0,0] * m.m[1,2] * m.m[3,1] + m.m[1,0] * m.m[0,1] * m.m[3,2] - m.m[1,0] * m.m[0,2] * m.m[3,1] - m.m[3,0] * m.m[0,1] * m.m[1,2] + m.m[3,0] * m.m[0,[2] * m.m[1,1];
                matInv.m[0,3] = -m.m[0,1] * m.m[1,2] * m.m[2,3] + m.m[0,1] * m.m[1,3] * m.m[2,2] + m.m[1,1] * m.m[0,2] * m.m[2,3] - m.m[1,1] * m.m[0,3] * m.m[2,2] - m.m[2,1] * m.m[0,2] * m.m[1,3] + m.m[2,1] * m.m[0,[3] * m.m[1,2];
                matInv.m[1,3] =  m.m[0,0] * m.m[1,2] * m.m[2,3] - m.m[0,0] * m.m[1,3] * m.m[2,2] - m.m[1,0] * m.m[0,2] * m.m[2,3] + m.m[1,0] * m.m[0,3] * m.m[2,2] + m.m[2,0] * m.m[0,2] * m.m[1,3] - m.m[2,0] * m.m[0,[3] * m.m[1,2];
                matInv.m[2,3] = -m.m[0,0] * m.m[1,1] * m.m[2,3] + m.m[0,0] * m.m[1,3] * m.m[2,1] + m.m[1,0] * m.m[0,1] * m.m[2,3] - m.m[1,0] * m.m[0,3] * m.m[2,1] - m.m[2,0] * m.m[0,1] * m.m[1,3] + m.m[2,0] * m.m[0,[3] * m.m[1,1];
                matInv.m[3,3] =  m.m[0,0] * m.m[1,1] * m.m[2,2] - m.m[0,0] * m.m[1,2] * m.m[2,1] - m.m[1,0] * m.m[0,1] * m.m[2,2] + m.m[1,0] * m.m[0,2] * m.m[2,1] + m.m[2,0] * m.m[0,1] * m.m[1,2] - m.m[2,0] * m.m[0,[2] * m.m[1,1];

                det = m.m[0,0] * matInv.m[0,0] + m.m[0,1] * matInv.m[1,0] + m.m[0,2] * matInv.m[2,0] + m.m[0,3] * matInv.m[3,0];

                det = 1.0 / det;

                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        matInv.m[i,j] *= (float)det;

                return matInv;
            }

            public static Vec3D Vec_Add(Vec3D v1, Vec3D v2)
            {
                Vec3D temp = new Vec3D();
                temp.X = v1.X + v2.X;
                temp.Y = v1.Y + v2.Y;
                temp.Z = v1.Z + v2.Z;
                temp.W = v1.W + v2.W;
                return temp;
            }

            public static Vec3D Vec_Sub(Vec3D v1, Vec3D v2)
            {
                Vec3D temp = new Vec3D();
                temp.X = v1.X - v2.X;
                temp.Y = v1.Y - v2.Y;
                temp.Z = v1.Z - v2.Z;
                temp.W = v1.W - v2.W;
                return temp;
            }

            public static Vec3D Vec_Mul(Vec3D v1, float k)
            {
                Vec3D temp = new Vec3D();
                temp.X = v1.X * k;
                temp.Y = v1.Y * k;
                temp.Z = v1.Z * k;
                temp.W = v1.W * k;
                return temp;
            }

            public static Vec3D Vec_Div(Vec3D v1, float k)
            {
                Vec3D temp = new Vec3D();
                temp.X = v1.X / k;
                temp.Y = v1.Y / k;
                temp.Z = v1.Z / k;
                temp.W = v1.W / k;
                return temp;
            }

            public static float Vec_DotProduct(Vec3D v1, Vec3D v2)
            {
                return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
            }

            public static float Vec_Length(Vec3D v)
            {
                return (float)Math.Sqrt(Vec_DotProduct(v, v));
            }

            public static Vec3D Vec_Normalize(Vec3D v)
            {
                Vec3D temp = new Vec3D();
                float l = Vec_Length(v);
                temp.X = v.X / l;
                temp.Y = v.Y / l;
                temp.Z = v.Z / l;
                return temp;
            }

            public static Vec3D Vec_CrossProduct(Vec3D v1, Vec3D v2)
            {
                Vec3D v = new Vec3D();
                v.X = v1.Y * v2.Z - v1.Z * v2.Y;
                v.Y = v1.Z * v2.X - v1.X * v2.Z;
                v.Z = v1.X * v2.Y - v1.Y * v2.X;
                return v;
            }

            public static Vec3D Vec_IntersectPlane(Vec3D plane_p, Vec3D plane_n, Vec3D lineStart, Vec3D lineEnd, float t)
            {
                plane_n = Vec_Normalize(plane_n);
                float plane_d = -Vec_DotProduct(plane_n, plane_p);
                float ad = Vec_DotProduct(lineStart, plane_n);
                float bd = Vec_DotProduct(lineEnd, plane_n);
                t = (-plane_d - ad) / (bd - ad);
                Vec3D lineStartToEnd = Vec_Sub(lineEnd, lineStart);
                Vec3D lineToIntersect = Vec_Mul(lineStartToEnd, t);
                return Vec_Add(lineStart, lineToIntersect);
            }
        }
    }
}
