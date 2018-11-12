using UnityEngine;
using UnityEditor;

namespace MinMaxSliderAttribute {
	[CustomPropertyDrawer(typeof(MinMaxRangeAttribute))]
	public class MinMaxSliderDrawer : PropertyDrawer {
		protected enum MinMaxType {
			Int,
			Float
		}
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			MinMaxRangeAttribute minMaxAttr = attribute as MinMaxRangeAttribute;

			var minProperty = property.FindPropertyRelative("min");
			var maxProperty = property.FindPropertyRelative("max");

			if (property.type == typeof(MinMaxInt).Name) {
				DrawMinMaxSlider(position, label, minMaxAttr, minProperty, maxProperty, MinMaxType.Int);
			} else if (property.type == typeof(MinMaxFloat).Name) {
				DrawMinMaxSlider(position, label, minMaxAttr, minProperty, maxProperty, MinMaxType.Float);
			} else if (property.type == typeof(MinMaxVector2).Name) {
				string labelName = label.text;
				var xMinProperty = minProperty.FindPropertyRelative("x");
				var xMaxProperty = maxProperty.FindPropertyRelative("x");
				DrawMinMaxSlider(position, new GUIContent(labelName + " - X"), minMaxAttr, xMinProperty, xMaxProperty, MinMaxType.Float);
				position.y += EditorGUIUtility.singleLineHeight * 2;
				var yMaxProperty = maxProperty.FindPropertyRelative("y");
				var yMinProperty = minProperty.FindPropertyRelative("y");
				DrawMinMaxSlider(position, new GUIContent(labelName + " - Y"), minMaxAttr, yMinProperty, yMaxProperty, MinMaxType.Float);
			} else if (property.type == typeof(MinMaxVector3).Name) {
				string labelName = label.text;
				var xMinProperty = minProperty.FindPropertyRelative("x");
				var xMaxProperty = maxProperty.FindPropertyRelative("x");
				DrawMinMaxSlider(position, new GUIContent(labelName + " - X"), minMaxAttr, xMinProperty, xMaxProperty, MinMaxType.Float);
				position.y += EditorGUIUtility.singleLineHeight * 2;
				var yMinProperty = minProperty.FindPropertyRelative("y");
				var yMaxProperty = maxProperty.FindPropertyRelative("y");
				DrawMinMaxSlider(position, new GUIContent(labelName + " - Y"), minMaxAttr, yMinProperty, yMaxProperty, MinMaxType.Float);
				position.y += EditorGUIUtility.singleLineHeight * 2;
				var zMinProperty = minProperty.FindPropertyRelative("z");
				var zMaxProperty = maxProperty.FindPropertyRelative("z");
				DrawMinMaxSlider(position, new GUIContent(labelName + " - Z"), minMaxAttr, zMinProperty, zMaxProperty, MinMaxType.Float);
			} else if (property.type == typeof(MinMaxVector4).Name) {
				string labelName = label.text;
				var xMinProperty = minProperty.FindPropertyRelative("x");
				var xMaxProperty = maxProperty.FindPropertyRelative("x");
				DrawMinMaxSlider(position, new GUIContent(labelName + " - X"), minMaxAttr, xMinProperty, xMaxProperty, MinMaxType.Float);
				position.y += EditorGUIUtility.singleLineHeight * 2;
				var yMinProperty = minProperty.FindPropertyRelative("y");
				var yMaxProperty = maxProperty.FindPropertyRelative("y");
				DrawMinMaxSlider(position, new GUIContent(labelName + " - Y"), minMaxAttr, yMinProperty, yMaxProperty, MinMaxType.Float);
				position.y += EditorGUIUtility.singleLineHeight * 2;
				var zMinProperty = minProperty.FindPropertyRelative("z");
				var zMaxProperty = maxProperty.FindPropertyRelative("z");
				DrawMinMaxSlider(position, new GUIContent(labelName + " - Z"), minMaxAttr, zMinProperty, zMaxProperty, MinMaxType.Float);
				position.y += EditorGUIUtility.singleLineHeight * 2;
				var wMinProperty = minProperty.FindPropertyRelative("w");
				var wMaxProperty = maxProperty.FindPropertyRelative("w");
				DrawMinMaxSlider(position, new GUIContent(labelName + " - W"), minMaxAttr, wMinProperty, wMaxProperty, MinMaxType.Float);
			}
		}
		// Main method of drawing MinMaxSlider
		protected void DrawMinMaxSlider(Rect position, GUIContent label, MinMaxRangeAttribute minMaxAttr, SerializedProperty minProperty, SerializedProperty maxProperty, MinMaxType minMaxType) {
			float textWidth = 30f;
			float paddingWidth = 10f;
			float fieldWidth = (position.width - EditorGUIUtility.labelWidth) / 2 - textWidth - paddingWidth / 2;
			position.height = EditorGUIUtility.singleLineHeight;
			float minValue = 0;
			float maxValue = 0;

			switch (minMaxType) {
				case MinMaxType.Int:
					minValue = minProperty.intValue;
					maxValue = maxProperty.intValue;
					break;
				case MinMaxType.Float:
					minValue = minProperty.floatValue;
					maxValue = maxProperty.floatValue;
					break;
			}
			EditorGUI.BeginChangeCheck();
			// Slider.
			EditorGUI.MinMaxSlider(position, label, ref minValue, ref maxValue, minMaxAttr.MinLimit, minMaxAttr.MaxLimit);
			// Next line.
			position.y += EditorGUIUtility.singleLineHeight;
			// Min text.
			position.x += EditorGUIUtility.labelWidth;
			position.width = textWidth;
			GUI.Label(position, new GUIContent("Min"));
			// Min field.
			position.x += position.width;
			position.width = fieldWidth;
			switch (minMaxType) {
				case MinMaxType.Int:
					minValue = Mathf.Clamp(EditorGUI.IntField(position, Mathf.RoundToInt(minValue)), minMaxAttr.MinLimit, maxValue);
					break;
				case MinMaxType.Float:
					minValue = Mathf.Clamp(EditorGUI.FloatField(position, minValue), minMaxAttr.MinLimit, maxValue);
					break;
			}
			// Padding width.
			position.x += paddingWidth;
			// Max text.
			position.x += position.width;
			position.width = textWidth;
			GUI.Label(position, new GUIContent("Max"));
			// Max field.
			position.x += position.width;
			position.width = fieldWidth;
			switch (minMaxType) {
				case MinMaxType.Int:
					maxValue = Mathf.Clamp(EditorGUI.IntField(position, Mathf.RoundToInt(maxValue)), minValue, minMaxAttr.MaxLimit);
					break;
				case MinMaxType.Float:
					maxValue = Mathf.Clamp(EditorGUI.FloatField(position, maxValue), minValue, minMaxAttr.MaxLimit);
					break;
			}

			// Setter.
			if (EditorGUI.EndChangeCheck()) {
				switch (minMaxType) {
					case MinMaxType.Int:
						minProperty.intValue = Mathf.RoundToInt(minValue);
						maxProperty.intValue = Mathf.RoundToInt(maxValue);
						break;
					case MinMaxType.Float:
						minProperty.floatValue = minValue;
						maxProperty.floatValue = maxValue;
						break;
				}
			}
		}
		// this method lets unity know how big to draw the property. We need to override this because it could end up meing more than one line big
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			MinMaxRangeAttribute minMax = attribute as MinMaxRangeAttribute;

			// by default just return the standard line height * 2
			float size = EditorGUIUtility.singleLineHeight * 2;
			// Some special type need more line.
			if (property.type == typeof(MinMaxVector2).Name) {
				size *= 2;
			} else if (property.type == typeof(MinMaxVector3).Name) {
				size *= 3;
			} else if (property.type == typeof(MinMaxVector4).Name) {
				size *= 4;
			}

			return size;
		}
	}
}