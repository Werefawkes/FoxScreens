using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Foxthorne.FoxScreens
{
	public class BarIndicator : MonoBehaviour
	{
		[Header("Settings")]
		public string elementName;
		public bool autoNameObject = true;

		[Header("References")]
		public Image fillImage;

		private void OnValidate()
		{
			if (autoNameObject) NameObject();
		}

		public void SetFillPercent(float percent)
		{
			fillImage.fillAmount = percent;
		}

		public void SetFillPercent(int percent)
		{
			fillImage.fillAmount = percent / 100f;
		}

		public void NameObject()
		{
			name = $"[Bar] {elementName}";
		}

	}
}
