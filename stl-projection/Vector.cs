namespace STLProjection
{
	
	// Custom 3D Vector and vector math implementation 
	public struct Vector
	{
		public static readonly Vector RIGHT = new Vector(1, 0, 0);
		public static readonly Vector UP = new Vector(0, 1, 0);
		public static readonly Vector FORWARD = new Vector(0, 0, 1);
		public static readonly Vector ONE = new Vector(1, 1, 1);
		public static readonly Vector ZERO = new Vector(0, 0, 0);

		public double x;
		public double y;
		public double z;

		public Vector(double x, double y)
		{
			this.x = x;
			this.y = y;
			this.z = 0f;
		}

		public Vector(double x, double y, double z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public double SqrMagnitude => x * x + y * y + z * z;
		public double Magnitude    => System.Math.Sqrt(SqrMagnitude);

		public Vector Normalized => this / Magnitude;

		public static double Distance(Vector a, Vector b)
		{
			return (a - b).Magnitude;
		}

		public static double Dot(Vector a, Vector b) => a.x * b.x + a.y * b.y + a.z * b.z;

		// Left handed
		public static Vector Cross(Vector a, Vector b) =>
			new Vector(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);

		public static double Angle(Vector a, Vector b)
		{
			return System.Math.Acos(Vector.Dot(a, b) / a.Magnitude / b.Magnitude) * 180.0 / System.Math.PI;
		}

		public string ToString(string format)
		{
			return $"{x.ToString(format)} {y.ToString(format)} {z.ToString(format)}";
		}

		public override string ToString()
		{
			return ToString("0.000");
		}

		public string ToStringFull()
		{
			return $"{x} {y} {z}";
		}

		// Common hash code generation method for 3D vectors, needed for fast lookups in hashmap used for shared vertices
		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode() >> 2;
		}

		public override bool Equals(object obj)
		{
			if (obj is Vector other)
			{
				return Approximately(this, other);
			}

			return false;
		}

		public static Vector Parse(string s)
		{
			//s = s.Replace("vertex ", "").Trim();
			var seg = s.Split(' ');
			var x = double.Parse(seg[0]);
			var y = double.Parse(seg[1]);
			var z = double.Parse(seg[2]);

			return new Vector(x, y, z);
		}

		// Cannot rely on equality of floating point numbers, check if difference is within a threshold
		public static bool Approximately(Vector a, Vector b, double threshold = 1e-4)
		{
			return System.Math.Abs(a.x - b.x) < threshold && System.Math.Abs(a.y - b.y) < threshold &&
			       System.Math.Abs(a.z - b.z) < threshold;
		}


		// Common vector operations
		public static Vector operator +(Vector a, Vector b)
		{
			return new Vector(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public static Vector operator -(Vector a, Vector b)
		{
			return new Vector(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		public static Vector operator +(Vector a)
		{
			return a;
		}

		public static Vector operator -(Vector a)
		{
			return new Vector(-a.x, -a.y, -a.z);
		}

		public static Vector operator *(Vector a, double m)
		{
			return new Vector(a.x * m, a.y * m, a.z * m);
		}

		public static Vector operator /(Vector a, double m)
		{
			return new Vector(a.x / m, a.y / m, a.z / m);
		}
	}
}
