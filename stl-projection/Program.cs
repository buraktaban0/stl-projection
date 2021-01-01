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

			return;


			Stopwatch sw = new Stopwatch();
			var pathInput = dir + "\\" + inputName;
			var pathOutput = dir + "\\" + outputName;

			sw.Start();
			var model = Stlio.Read(pathInput);
			sw.Stop();

			Console.WriteLine($"Read: {sw.Elapsed.TotalMilliseconds} ms");

			var mesh = MeshUtility.StlModelToMesh(model);

			MeshUtility.ApplyNoise(mesh, 0.25);


			model = MeshUtility.MeshToStlModel(mesh);

			sw.Reset();
			sw.Start();
			Stlio.Write(pathOutput, model);
			sw.Stop();


			Console.WriteLine($"Write: {sw.Elapsed.TotalMilliseconds} ms");
		}
	}
}
