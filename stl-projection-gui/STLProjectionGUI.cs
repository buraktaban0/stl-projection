using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using STLProjection;

namespace stl_projection_gui
{
	static class STLProjectionGUI
	{
		private static MainForm form;

		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			embedder = new Embedder("symbols.txt");

			pngPath = embedder.SaveMatrixPng(id, size, margin);

			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(form = new MainForm());

			RefreshPlot();
		}

		public static string inputPath;
		public static string outputPath;
		public static string symbolsPath = "symbols.txt";

		public static string pngPath;

		public static Embedder embedder;

		private static int _id = new System.Random().Next(244140626);

		public static int id
		{
			get => _id;
			set
			{
				_id = value;
				RefreshPlot();
			}
		}

		private static double _size = 15;

		public static double size
		{
			get => _size;
			set
			{
				_size = value;
				RefreshPlot();
			}
		}

		private static double _margin = 1;

		public static double margin
		{
			get => _margin;
			set
			{
				_margin = value;
				RefreshPlot();
			}
		}

		private static int _subdivisions = 0;

		public static int subdivisions
		{
			get => _subdivisions;
			set { _subdivisions = value; }
		}


		private static void RefreshPlot()
		{
			pngPath = embedder.SaveMatrixPng(id, size, margin);
			form.RefreshPlot();
		}

		public static void Embed(ProgressBar progressBar, Action onCompleted)
		{
			Action<int> setProg = i =>
			{
				progressBar.Invoke((MethodInvoker) delegate
				{
					progressBar.Value = i;
				});
			};

			setProg(0);
			Task.Run(() =>
			{
				var model = Stlio.Read(inputPath);

				setProg(10);
				model.Subdivide(subdivisions);

				setProg(25);
				var mesh = MeshUtility.StlModelToMesh(model);

				setProg(35);
				embedder.Embed(id, mesh, size, margin, 0.6, 0.7, new Vector(25, 50, 32), -Vector.UP, Vector.RIGHT);

				setProg(70);
				model = MeshUtility.MeshToStlModel(mesh);
			
				setProg(80);
				
				Stlio.Write(outputPath, model);
				
				setProg(100);
				
				onCompleted.Invoke();
			}).ConfigureAwait(false);

		}
	}
}
