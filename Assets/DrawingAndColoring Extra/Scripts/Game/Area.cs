using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace IndieStudio.DrawingAndColoring.Logic
{
	[DisallowMultipleComponent]
	public class Area : MonoBehaviour
	{

		/// <summary>
		/// The shapes drawing contents.
		/// </summary>
		public static List<DrawingContents> shapesDrawingContents = new List<DrawingContents> ();
	}
}
