using Foxthorne.FoxCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foxthorne.FoxScreens
{
    public class UISceneSettings : Singleton<UISceneSettings>
    {
        public string defaultScreenName;

		private void Start()
		{
			if (defaultScreenName.Trim() != "")
			{
				UIManager.Instance.OpenScreen(defaultScreenName);
			}
		}
	}
}
