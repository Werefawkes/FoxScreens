using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Foxthorne.FoxScreens
{
	[AddComponentMenu("Foxthorne/FoxScreens/UI Navigator")]
	public class UINavigator : MonoBehaviour
	{

		public void Back()
		{
			// Only try to access the breadcrumbs list if there are screens in it
			if (UIManager.Instance.breadcrumbs.Count >= 1)
			{
				UIManager.Instance.CloseScreen(UIManager.Instance.breadcrumbs[^1]);
				// Make sure there is a previous screen to go to
				if (UIManager.Instance.breadcrumbs.Count >= 1)
				{
					UIManager.Instance.OpenScreen(UIManager.Instance.breadcrumbs[^1]);
				}
			}
		}

		public void Confirm()
		{

		}

		public void Cancel()
		{

		}

		void OnSubmit()
		{
			Debug.Log("OnSubmit");
		}

		void OnCancel()
		{
			Debug.Log("OnCancel");
		}

		void OnPause()
		{
			if (UIManager.IsUIClear)
			{
				UIManager.Instance.OpenScreen("PauseMenu");
			}
			else
			{
				Back();
			}
		}
	}
}
