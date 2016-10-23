using UnityEngine;
using System.Collections;

// Persistent object IS A game object, therefore inherits from it.
public class SCR_PersistentObject : MonoBehaviour 
{

	public SCR_PersistentObject(PrimitiveType primitiveType)
	{

		GameObject.CreatePrimitive(primitiveType);//.transform.parent;

	}

}
