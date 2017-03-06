using UnityEngine;
using System.Collections;

public class SCR_MathUtilities
{
	public static float EuclideanDistance3D(Vector3 pointOne, Vector3 pointTwo)
	{
		float result = 0.0f;
		float x = (pointTwo.x - pointOne.x) * (pointTwo.x - pointOne.x);
		float y = (pointTwo.y - pointOne.y) * (pointTwo.y - pointOne.y);
		result = Mathf.Sqrt (x + y);
		return result;
	}

	public static float EuclideanDistance(float pointOne, float pointTwo)
	{
		float result = 0.0f;
		float x = (pointTwo - pointOne) * (pointTwo - pointOne);
		result = Mathf.Sqrt(x);
		return result;
	}
}