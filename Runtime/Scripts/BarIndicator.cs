using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Foxthorne.FoxScreens
{
	public class BarIndicator : MonoBehaviour
	{
		[Header("Settings")]
		public string exposedFloatKey;
		public bool autoNameObject = true;
		public float defaultValue = 1;

		[Header("References")]
		public Image fillImage;

		private void OnValidate()
		{
			if (autoNameObject) NameObject();
		}

		private void Update()
		{
			float val = UIManager.Instance.GetExposedFloat(exposedFloatKey);
			if (float.IsNaN(val))
			{
				val = defaultValue;
			}
			SetFillPercent(val);
		}

		private void Start()
		{
			SetFillPercent(defaultValue);
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
			name = $"[Bar] {exposedFloatKey}";
		}

	}
}
