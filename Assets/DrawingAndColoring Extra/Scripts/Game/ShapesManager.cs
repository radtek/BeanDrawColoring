using UnityEngine;
using System.Collections;
using System.Collections.Generic;



namespace IndieStudio.DrawingAndColoring.Logic
{
	public class ShapesManager : MonoBehaviour
	{
        /// <summary>
        /// 形状列表。
        /// </summary>
        public List<Shape> shapes = new List<Shape> ();

        /// <summary>
        /// 最后选定的形状。
        /// </summary>
        [HideInInspector]
			public int lastSelectedShape;

        /// <summary>
        /// 这个类的实例。
        /// </summary>
        public static ShapesManager instance;

			void Awake ()
			{
				if (instance == null) {
					instance = this;
					DontDestroyOnLoad (gameObject);
				lastSelectedShape = 0;
				} else {
					Destroy (gameObject);
				}
			}

			[System.Serializable]
			public class Shape
			{
					public bool showContents = true;
					public GameObject gamePrefab;
			}
	}
}
