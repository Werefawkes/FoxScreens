using Foxthorne.FoxCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Foxthorne.FoxScreens
{
	[AddComponentMenu("Foxthorne/FoxScreens/UI Manager")]
	public class UIManager : Singleton<UIManager>
	{
		[Header("Settings")]
		public bool LockCursor = true;

		public static bool IsUIClear { get; private set; }

		[Header("Screens")]
		public List<Screen> allScreens;
		public List<Screen> openScreens;

		public List<Screen> breadcrumbs;


		private void Start()
		{
			openScreens = new List<Screen>();
			foreach (Screen s in allScreens)
			{
				if (s.openByDefault)
				{
					openScreens.Add(s);
				}
			}

			ScreenUpdate();
		}

		private void OnValidate()
		{
			allScreens = new();
			Screen[] newScreens = GetComponentsInChildren<Screen>(true);
			for (int i = 0; i < newScreens.Length; i++)
			{
				allScreens.Add(newScreens[i]);
				newScreens[i].screenID = i;
				newScreens[i].NameObject();
			}

			if (allScreens.Count == 0)
			{
				Debug.LogWarning("No screens found. Make sure this component is attached to the highest parent of the Canvas.");
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
			breadcrumbs.Add(screen);

			ScreenUpdate();
		}

		public void OpenScreen(string screenName, bool exclusive = true)
		{
			Screen s = GetScreen(screenName);
			if (s == null)
			{
				Debug.LogWarning("No screen of name " + screenName);
			}
			else
			{
				OpenScreen(s, exclusive);
			}
		}

		public void OpenScreen(int screenId, bool exclusive = true)
		{
			Screen s = GetScreen(screenId);
			if (s == null)
			{
				Debug.LogWarning("No screen of ID " + screenId);
			}
			else
			{
				OpenScreen(s, exclusive);
			}
		}

		public void CloseScreen(Screen screen)
		{
			openScreens.Remove(screen);
			breadcrumbs.Remove(screen);

			ScreenUpdate();
		}

		public void CloseAllScreens()
		{
			openScreens.Clear();
			breadcrumbs.Clear();

			ScreenUpdate();
		}

		public void ScreenUpdate()
		{
			bool uiclear = true;
			foreach (Screen s in allScreens)
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
			if (IsUIClear && LockCursor)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			else
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}

		public Screen GetScreen(string name)
		{
			foreach (Screen s in allScreens)
			{
				if (s.screenName == name)
				{
					return s;
				}
			}

			return null;
		}

		public Screen GetScreen(int id)
		{
			foreach (Screen s in allScreens)
			{
				if (s.screenID == id)
				{
					return s;
				}
			}

			return null;
		}
		#endregion

		#region Indicator Management
		readonly Dictionary<string, float> exposedFloats = new();

		public void SetExposedFloat(string key, float value)
		{
			// If it exists, update it
			if (exposedFloats.ContainsKey(key))
			{
				exposedFloats[key] = value;
			}
			else
			{
				exposedFloats.Add(key, value);
			}
		}

		public float GetExposedFloat(string key)
		{
			if (exposedFloats.ContainsKey(key))
			{
				return exposedFloats[key];
			}
			else
			{
				Debug.LogWarning($"Tried to get exposed float '{key}' but it did not exist", this);
				return float.NaN;
			}
		}

		public void DeleteExposedFloat(string key)
		{
			if (exposedFloats.ContainsKey(key))
			{
				exposedFloats.Remove(key);
			}
		}
		#endregion

		#region Scene Management
		public void LoadScene(string sceneName)
		{
			SceneManager.LoadScene(sceneName);
		}

		public void ExitGame()
		{
			Application.Quit();
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