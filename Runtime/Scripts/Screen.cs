using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foxthorne.FoxScreens
{
	[AddComponentMenu("Foxthorne/FoxScreens/Screen")]
	public class Screen : MonoBehaviour
	{
		[Header("Settings")]
		public string screenName;
		public int screenID;
		public bool isHudElement = false;
		public bool autoNameObject = true;
		public bool openByDefault = false;

		readonly string hudPrefix = "HUD";
		readonly string screenPrefix = "GUI";

		public bool IsOpen { get; private set; }

		private void OnValidate()
		{
			if (autoNameObject)
			{
				NameObject();
			}
		}

		public void Open()
		{
			IsOpen = true;

			gameObject.SetActive(true);
		}

		public void Close()
		{
			IsOpen = false;

			gameObject.SetActive(false);
		}

		public void NameObject()
		{
			string prefix;
			if (isHudElement)
			{
				prefix = hudPrefix;
			}
			else
			{
				prefix = screenPrefix;
			}

			name = $"[{prefix}-{screenID}] {screenName}";
		}
	}
}
