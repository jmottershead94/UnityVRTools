using UnityEngine;
using System.Collections;

// Persistent object IS A game object, therefore inherits from it.
public class SCR_PersistentObject : MonoBehaviour
{

	// Attributes.
	private PrimitiveType primitiveType;	// Accessing what primitive type we are using.

	// Getters/Setters.
	// This will allow us to get/set the primitive type of the game object.
	public PrimitiveType ObjectType
	{
		get { return primitiveType; }
		set { primitiveType = value; }
	}

}
