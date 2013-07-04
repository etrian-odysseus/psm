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
		
		private static Matrix4 viewMatrix;
        private static Matrix4 projectionMatrix;
		private static Matrix4 idMatrix;
		
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
			shader.SetUniformBinding(0, "u_viewMatrix");
			shader.SetUniformBinding(1, "u_projMatrix");
			shader.SetUniformBinding(2, "u_worldMatrix");
			SetUpCamera();
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
			shader.SetUniformValue(0, ref viewMatrix);
			shader.SetUniformValue(1, ref projectionMatrix);
			shader.SetUniformValue(2, ref idMatrix);
			graphics.DrawArrays(DrawMode.Triangles, 0, 3);
			
			// Present the screen
			graphics.SwapBuffers ();
		}
		
		private static void SetUpCamera()
         {
            viewMatrix = Matrix4.LookAt(new Vector3(0, 0, 50), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            projectionMatrix = Matrix4.Perspective(FMath.PI/4, (float)graphics.GetViewport().Width/(float)graphics.GetViewport().Height, 1.0f, 300.0f);
        	idMatrix = Matrix4.Identity; 
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
