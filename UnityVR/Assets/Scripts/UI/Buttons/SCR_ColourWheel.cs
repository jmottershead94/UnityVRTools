using UnityEngine;
using System.Collections;
using Microsoft.Win32;

public class SCR_ColourWheel : SCR_3DButton 
{

	/* Attributes. */
	private Color colourSelected = Color.white;
	//private SpriteRenderer spriteRenderer = null;
	//private Texture2D texture = null;private Texture2D texture = null;
	private MeshRenderer meshRenderer = null;
	private Texture texture = null;
	private Texture2D tex = null;
	private Color[] colours = null;

	/* Methods. */
	/*
	*
	*	Overview
	*	--------
	*	This will be called before initialisation.
	*
	*/
	new protected void Awake()
	{

		base.Awake();

		//spriteRenderer = GetComponent<SpriteRenderer>();
		meshRenderer = GetComponent<MeshRenderer>();
		texture = meshRenderer.sharedMaterial.mainTexture;
		tex = (Texture2D)texture;
		colours = tex.GetPixels();

		Debug.Log("Texture Size = " + texture.width + " " + texture.height);

	}

	private Color SampleTexture(float xPos, float yPos)
	{
		Color color = tex.GetPixel((int)(xPos / texture.width), (int)(yPos / texture.height));
		Debug.Log("Click Pos on Wheel = " + (int)(xPos / texture.width) + " " + (int)(yPos / texture.height));
		return color;
	}

	void OnMouseDown()
	{

		ButtonPressResponse();

	}

	/*
	*
	*	Overview
	*	--------
	*	This will allow us to define a specific button response.
	*
	*/
	override public void ButtonPressResponse()
	{

		// This would need to be either the mouse position.
		// OR the end of the ray from the right hand controller.
		Vector2 clickPosition = new Vector2(Input.mousePosition.x - transform.position.x, Input.mousePosition.y - transform.position.y);

        Debug.Log("Click Pos = " + clickPosition.x + " " + clickPosition.y);

		//int x2 = (int)(x * (spriteRenderer.bounds.size.x / (spriteRenderer.bounds.size.x + 0.0f)));
		//int y2 = (int)((spriteRenderer.bounds.size.y - y) * (spriteRenderer.bounds.size.y / (spriteRenderer.bounds.size.y + 0.0f)));
		Vector2 samplePosition = new Vector2(clickPosition.x / 4.0f, clickPosition.y / 4.0f);

		Color col = SampleTexture((clickPosition.x / 4.0f), (clickPosition.y / 4.0f));
        colourSelected = col;

		Debug.Log("RGB: " + colourSelected.r.ToString("F2") + " " + colourSelected.g.ToString("F2") + " " + colourSelected.b.ToString("F2") + " ");

		for(int i = 0; i < sceneEditor.Objects.Count; i++)
		{

			if(sceneEditor.Objects[i].InFocus)
			{
				
				sceneEditor.Objects[i].gameObject.GetComponent<Renderer>().material.color = colourSelected;
				sceneEditor.Objects[i].DefaultMaterial.color = colourSelected;

			}

		}

	}
}