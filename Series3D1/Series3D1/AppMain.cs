using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

namespace Series3D1
{
	public class AppMain
	{
		private static GraphicsContext graphics;
		
		private static ShaderProgram shader;
		
		private static float[] colors = {
				1.0f, 0.0f, 0.0f, 0.0f,
				0.0f, 1.0f, 0.0f, 0.0f,
				1.0f, 1.0f, 0.0f, 0.0f
		};
		
		private static VertexBuffer vBuffer;
		
		private static Vector3[] vArr;
		
		public static void Main (string[] args)
		{
			Initialize ();

			while (true) {
				SystemEvents.CheckEvents ();
				Update ();
				Render ();
			}
		}

		public static void Initialize ()
		{
			// Set up the graphics system
			graphics = new GraphicsContext ();
			shader = new ShaderProgram("/Application/shaders/Simple.cgx");
			SetUpVertices();
			graphics.SetVertexBuffer(0, vBuffer);
		}

		public static void Update ()
		{
		}

		public static void Render ()
		{
			// Clear the screen
			graphics.SetClearColor (0.28f, 0.24f, 0.55f, 0.0f);
			graphics.Clear ();
			
			graphics.SetShaderProgram(shader);
			graphics.DrawArrays(DrawMode.Triangles, 0, 3);
			
			// Present the screen
			graphics.SwapBuffers ();
		}
		
		private static void SetUpVertices()
		{
			Vector3 v1 = new Vector3(0.0f, 0.0f, 0.0f);
			
			Vector3 v2 = new Vector3(10.0f, 10.0f, 0.0f);
			
			Vector3 v3 = new Vector3(10.0f, 0.0f, -5.0f);
			
			vArr = new Vector3[3]; 
			vArr[0] = v1;
			vArr[1] = v2;
			vArr[2] = v3;
			
			vBuffer = new VertexBuffer(3, VertexFormat.Float3, VertexFormat.Float4);
			vBuffer.SetVertices(0, vArr);
			vBuffer.SetVertices(1, colors);
		}
	}
}
