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
		public List<Screen> openScreens;

		private void Start()
		{
			openScreens = new List<Screen>();
			ScreenUpdate();
		}

		private void OnValidate()
		{
			screens = new();
			Screen[] newScreens = GetComponentsInChildren<Screen>(true);
			for (int i = 0; i < newScreens.Length; i++)
			{
				screens.Add(newScreens[i]);
				newScreens[i].screenID = i;
				newScreens[i].NameObject();
			}
		}

		#region Screen Management
		public void OpenScreen(Screen screen, bool exclusive = true)
		{
			if (exclusive)
			{
				openScreens.Clear();
			}

			openScreens.Add(screen);

			ScreenUpdate();
		}

		public void OpenScreen(string screenName, bool exclusive = true)
		{
			OpenScreen(GetScreenByName(screenName), exclusive);
		}

		public void OpenScreen(int screenId, bool exclusive = true)
		{
			OpenScreen(GetScreenByID(screenId), exclusive);
		}

		public void CloseScreen(Screen screen)
		{
			openScreens.Remove(screen);

			ScreenUpdate();
		}

		public void CloseAllScreens()
		{
			openScreens.Clear();

			ScreenUpdate();
		}

		public void ScreenUpdate()
		{
			bool uiclear = true;
			foreach (Screen s in screens)
			{
				if (openScreens.Contains(s))
				{
					s.Open();
					if (!s.isHudElement)
					{
						uiclear = false;
					}
				}
				else
				{
					s.Close();
				}
			}

			IsUIClear = uiclear;
		}

		public Screen GetScreenByName(string name)
		{
			foreach (Screen s in screens)
			{
				if (s.screenName == name)
				{
					return s;
				}
			}

			return null;
		}

		public Screen GetScreenByID(int id)
		{
			foreach (Screen s in screens)
			{
				if (s.screenID == id)
				{
					return s;
				}
			}

			return null;
		}
		#endregion

		#region Editor Tools
		public void NewScreen()
		{
			Debug.Log("NewScreen()");
		}

		public void SetUp()
		{
			Canvas canvas = new GameObject().AddComponent<Canvas>();
			transform.parent = canvas.transform;
		}
		#endregion
	}
}