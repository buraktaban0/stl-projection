using System;
using System.Diagnostics;

namespace STLProjection
{
	class Program
	{
		static void Main(string[] args)
		{
			// Default debug paths
			var pathShapes = "C:\\Users\\PC1\\Desktop\\495\\term\\shapes.txt";
			var pathInput = "C:\\Users\\PC1\\Desktop\\495\\term\\Part1.stl";
			var pathOutput = "C:\\Users\\PC1\\Desktop\\495\\term\\Out1.stl";

			// Check command line arguments for paths
			CollectPathArguments(args, ref pathShapes, ref pathInput, ref pathOutput);

			// Create embedder with shapes
			var embedder = new Embedder(pathShapes);

			// For timing every step
			Stopwatch sw = new Stopwatch();

			sw.Start();
			// Read STL file into custom STL data structure
			var model = Stlio.Read(pathInput);
			sw.Stop();

			Console.WriteLine($"Read: {sw.Elapsed.TotalMilliseconds} ms");

			sw.Reset();
			sw.Start();
			model.Subdivide(2);
			sw.Stop();
			Console.WriteLine($"Subdivide: {sw.Elapsed.TotalMilliseconds} ms");
			
			sw.Reset();
			sw.Start();
			// Convert STL to a more convenient mesh structure with shared vertices
			var mesh = MeshUtility.StlModelToMesh(model);
			sw.Stop();

			Console.WriteLine($"STL to mesh: {sw.Elapsed.TotalMilliseconds} ms");

			sw.Reset();
			sw.Start();
			// Embed the code onto the mesh 
			embedder.Embed(165289711 /*Arbitrary number*/, mesh, 15, 0.5, 0.6, 1, new Vector(25, 50, 32), -Vector.UP,
			               Vector.RIGHT);
			sw.Stop();

			Console.WriteLine($"Embed: {sw.Elapsed.TotalMilliseconds} ms");


			sw.Reset();
			sw.Start();
			// Convert mesh data back to STL format
			model = MeshUtility.MeshToStlModel(mesh);
			sw.Stop();

			Console.WriteLine($"Mesh to STL: {sw.Elapsed.TotalMilliseconds} ms");

			sw.Reset();
			sw.Start();
			// Write modified STL data to output path
			Stlio.Write(pathOutput, model);
			sw.Stop();

			Console.WriteLine($"Write: {sw.Elapsed.TotalMilliseconds} ms");
		}

		private static void CollectPathArguments(string[] args, ref string pathShapes, ref string pathInput,
		                                         ref string pathOutput)
		{
			for (var i = 0; i < args.Length; i++)
			{
				var s = args[i];
				if (i < args.Length - 1)
				{
					if (s == "--input")
					{
						i++;
						pathInput = args[i];
					}
					else if (s == "--output")
					{
						i++;
						pathOutput = args[i];
					}
					else if (s == "--shapes")
					{
						i++;
						pathShapes = args[i];
					}
				}
			}
		}
	}
}
