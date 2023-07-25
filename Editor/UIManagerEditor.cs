using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace Foxthorne.FoxScreens
{
	[CustomEditor(typeof(UIManager))]
	public class UIManagerEditor : Editor
	{
		public VisualTreeAsset inspectorXML;

		public override VisualElement CreateInspectorGUI()
		{
			VisualElement inspector = new();

			inspectorXML.CloneTree(inspector);

			VisualElement newScreenButton = inspector.Q("newScreenButton");
			VisualElement setUpButton = inspector.Q("setUpButton");

			newScreenButton.RegisterCallback<PointerUpEvent>(NewScreenEvent);
			setUpButton.RegisterCallback<PointerUpEvent>(SetUpEvent);

			return inspector;
		}

		void NewScreenEvent(PointerUpEvent e)
		{
			Debug.Log("Click!");
			UIManager.Instance.NewScreen();
		}

		void SetUpEvent(PointerUpEvent e)
		{
			UIManager.Instance.SetUp();
		}
	}
}
