using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinMaxSliderAttribute {
	/// <summary>
	/// The Attribute only work on MinMaxInt, MinMaxFloat, MinMaxVector2, MinMaxVector3 and MinMaxVector4.
	/// </summary>
	[System.AttributeUsage(System.AttributeTargets.Field)]
	public class MinMaxRangeAttribute : PropertyAttribute {
		public readonly float MinLimit = 0;
		public readonly float MaxLimit = 1;

		public MinMaxRangeAttribute(float min, float max) {
			MinLimit = min;
			MaxLimit = max;
		}

		public MinMaxRangeAttribute(int min, int max) {
			MinLimit = min;
			MaxLimit = max;
		}
	}
}