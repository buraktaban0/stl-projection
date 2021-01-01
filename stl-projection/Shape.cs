using System.Collections.Generic;
using System.Linq;

namespace STLProjection
{
	public class Shape
	{
		public List<Line> lines = new List<Line>();

		public int index;

		public Shape(int index)
		{
			this.index = index;
		}

		public override string ToString()
		{
			var s = $"Shape {index}\r\n";
			foreach (var line in lines)
			{
				s += line + "\r\n";
			}

			s += $"Shape {index} end";

			return s.Trim();
		}


		public Shape Scale(double m)
		{
			return new Shape(index) {lines = lines.Select(line => new Line(line.p1 * m, line.p2 * m)).ToList()};
		}

		public Shape Translate(Vector offset)
		{
			return new Shape(index){lines = lines.Select(line => new Line(line.p1 + offset, line.p2 + offset)).ToList()};
		}

	}
}
