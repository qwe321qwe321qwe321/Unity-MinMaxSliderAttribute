using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinMaxSliderAttribute {
	[System.Serializable]
	public class MinMaxType<T> {
		public T min;
		public T max;
	}
	[System.Serializable]
	public class MinMaxInt : MinMaxType<int> { }
	[System.Serializable]
	public class MinMaxFloat : MinMaxType<float> { }
	[System.Serializable]
	public class MinMaxVector2 : MinMaxType<Vector2> { }
	[System.Serializable]
	public class MinMaxVector3 : MinMaxType<Vector3> { }
	[System.Serializable]
	public class MinMaxVector4 : MinMaxType<Vector4> { }
}
