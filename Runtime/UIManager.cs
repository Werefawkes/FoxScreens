using Foxthorne.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foxthorne.FoxScreens
{
	[AddComponentMenu("Foxthorne/FoxScreens/UI Manager")]
	public class UIManager : Singleton<UIManager>
	{
		public bool IsUIClear { get; private set; }

		public List<Screen> screens;

		public void OpenScreen()
		{

		}

		public void CloseScreen()
		{

		}

		public void CloseAllScreens()
		{

		}

		public void NewScreen()
		{
			Debug.Log("NewScreen()");
		}

		public void SetUp()
		{
			Canvas canvas = new GameObject().AddComponent<Canvas>();
			transform.parent = canvas.transform;
		}
	}
}