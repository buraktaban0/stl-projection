using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace STLProjection
{
	public static class Stlio
	{
		public static StlModel Read(string path)
		{
			var lines = File.ReadAllLines(path).Select(line => line.Trim()).ToArray();
			var name = lines[0].Replace("solid ", "").Trim();

			List<Vector> vertices = new List<Vector>(1024);
			List<Vector> normals = new List<Vector>(512);

			for (int i = 1; i < lines.Length; i += 7)
			{
				var line = lines[i].Replace("facet normal", "").Trim();

				if (line.Contains("endsolid"))
				{
					// end of file
					break;
				}

				var normal = Vector.Parse(line);
				normals.Add(normal);

				for (int j = i + 2; j < i + 5; j++)
				{
					line = lines[j].Replace("vertex ", "").Trim();
					var vertex = Vector.Parse(line);
					vertices.Add(vertex);
				}
			}

			var model = new StlModel(name, vertices, normals);

			return model;
		}

		public static void Write(string path, StlModel model)
		{
			List<string> lines = new List<string>(1024);

			var vertices = model.vertices;
			var normals = model.normals;
			int tris = model.normals.Count;
			
			lines.Add($"solid {model.name}");
			
			for (int i = 0; i < tris; i++)
			{
				var n = normals[i].ToStringFull();
				lines.Add($"   facet normal {n}");
				lines.Add("      outer loop");
				for (int j = 0; j < 3; j++)
				{
					lines.Add($"         vertex {vertices[i*3 + j].ToStringFull()}");
				}
				lines.Add("      endloop");
				lines.Add("   endfacet");
			}
			
			lines.Add("endsolid");
			
			File.WriteAllLines(path, lines);
			
		}

	}
}
