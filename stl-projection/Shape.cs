using System.Collections.Generic;

namespace STLProjection
{
	public class Shape
	{
		public List<Line> lines = new List<Line>();

		public override string ToString()
		{
			var s = "";
			foreach (var line in lines)
			{
				s += line + "\r\n";
			}

			return s.Trim();
		}
	}
}
