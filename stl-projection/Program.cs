using System;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace STLProjection
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			var dir = "C:\\Users\\PC1\\Desktop\\495\\term";

			var inputName = "Part1.stl";
			var outputName = "Out1.stl";
			var shapeDataName = "shapes.txt";

			var pathShapes = dir + "\\" + shapeDataName;

			var embedder = new Embedder(pathShapes);


			Stopwatch sw = new Stopwatch();
			var pathInput = dir + "\\" + inputName;
			var pathOutput = dir + "\\" + outputName;

			sw.Start();
			var model = Stlio.Read(pathInput);
			sw.Stop();

			Console.WriteLine($"Read: {sw.Elapsed.TotalMilliseconds} ms");

			sw.Reset();
			sw.Start();
			var mesh = MeshUtility.StlModelToMesh(model);
			sw.Stop();

			Console.WriteLine($"STL to mesh: {sw.Elapsed.TotalMilliseconds} ms");

			sw.Reset();
			sw.Start();
			embedder.Embed(165289711, mesh, 12, 0.6, 0.5, 1, new Vector(25, 50, 31), -Vector.UP, Vector.RIGHT, Vector.FORWARD);
			sw.Stop();

			Console.WriteLine($"Embed: {sw.Elapsed.TotalMilliseconds} ms");


			sw.Reset();
			sw.Start();
			model = MeshUtility.MeshToStlModel(mesh);
			sw.Stop();

			Console.WriteLine($"Mesh to STL: {sw.Elapsed.TotalMilliseconds} ms");

			sw.Reset();
			sw.Start();
			Stlio.Write(pathOutput, model);
			sw.Stop();


			Console.WriteLine($"Write: {sw.Elapsed.TotalMilliseconds} ms");
		}
	}
}
