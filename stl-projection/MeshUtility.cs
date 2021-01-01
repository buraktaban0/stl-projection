﻿using System;
using System.Collections.Generic;

namespace STLProjection
{
	public class MeshUtility
	{
		// private class SharedVertexData
		// {
		// 	public int index = -1;
		// 	public Vector3 cumNorm = new Vector3(0, 0, 0);
		// 	public int normCount = 0;
		// }

		public static Mesh StlModelToMesh(StlModel model)
		{
			var verts = model.vertices;
			var norms = model.normals;

			int triCount = model.TriangleCount;

			Dictionary<Vector, int> hash = new Dictionary<Vector, int>();
			List<Vector> vertices = new List<Vector>();
			List<Vector> normals = new List<Vector>();

			List<int> indices = new List<int>();

			for (int i = 0; i < triCount; i++)
			{
				var norm = norms[i].Normalized;
				for (int j = 0; j < 3; j++)
				{
					var v = verts[i * 3 + j];
					if (hash.TryGetValue(v, out var sharedIndex))
					{
						indices.Add(sharedIndex);
						normals[sharedIndex] += norm;
					}
					else
					{
						int index = vertices.Count;
						vertices.Add(v);
						normals.Add(norm);
						indices.Add(index);
						hash.Add(v, index);
					}
				}
			}

			for (int i = 0; i < normals.Count; i++)
			{
				normals[i] = normals[i].Normalized;
			}

			// for (int i = 0; i < vertices.Count; i++)
			// {
			// 	Console.WriteLine(vertices[i].ToString("0.000") + "  " + normals[i].ToString("0.000"));
			// }

			var mesh = new Mesh(model.name, vertices, normals, indices);
			return mesh;
		}

		public static StlModel MeshToStlModel(Mesh mesh)
		{
			var triCount = mesh.TriangleCount;

			List<Vector> vertices = new List<Vector>();
			List<Vector> normals = new List<Vector>();

			for (int i = 0; i < triCount; i++)
			{
				var i0 = mesh.indices[i * 3 + 0];
				var i1 = mesh.indices[i * 3 + 1];
				var i2 = mesh.indices[i * 3 + 2];

				var v0 = mesh.vertices[i0];
				var v1 = mesh.vertices[i1];
				var v2 = mesh.vertices[i2];

				var n0 = mesh.normals[i0];
				var n1 = mesh.normals[i1];
				var n2 = mesh.normals[i2];

				vertices.Add(v0);
				vertices.Add(v1);
				vertices.Add(v2);

				var nMean = (n0 + n1 + n2).Normalized;

				var r1 = (v1 - v0).Normalized;
				var r2 = (v2 - v0).Normalized;
				
				var nCross = Vector.Cross(r1, r2);

				var dot = Vector.Dot(nCross, nMean);
				
				var n = dot > 0 ? nCross : -nCross;

				//Console.WriteLine($"{v0} ; {v1} ; {v2} :: {nMean} ; {nCross} ; {dot} ; {n}");
				
				normals.Add(n.Normalized);
			}

			var stlModel = new StlModel(mesh.name, vertices, normals);
			return stlModel;
		}


		public static void ApplyNoise(Mesh mesh, double strength)
		{
			var rand = new Random();
			for (var i = 0; i < mesh.vertices.Count; i++)
			{
				var v = mesh.vertices[i];
				var n = mesh.normals[i];
				mesh.vertices[i] = v + n * ((rand.NextDouble() * 2 - 1) * strength);
			}
		}
	}
}