using UnityEngine;
using System.Collections;

public class SCR_ColourWheel : SCR_3DButton 
{

	/* Attributes. */
	private Color colourSelected = Color.white;
	private SpriteRenderer spriteRenderer = null;
	private Texture2D texture = null;
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

		spriteRenderer = GetComponent<SpriteRenderer>();
		texture = spriteRenderer.sprite.texture;
		colours = texture.GetPixels();

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
        float x = Input.mousePosition.x - transform.position.x;
        float y = Input.mousePosition.y - transform.position.y;

		int x2 = (int)(x * (spriteRenderer.bounds.size.x / (spriteRenderer.bounds.size.x + 0.0f)));
		int y2 = (int)((spriteRenderer.bounds.size.y - y) * (spriteRenderer.bounds.size.y / (spriteRenderer.bounds.size.y + 0.0f)));

        Color col = texture.GetPixel(x2, y2);
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