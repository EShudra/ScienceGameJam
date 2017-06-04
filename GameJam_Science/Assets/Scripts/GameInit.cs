using UnityEngine;
using System.Collections;

public class GameInit : MonoBehaviour {
	public Texture2D cursorTex;
	public Vector2 hotSpot = Vector2.zero;

	// Use this for initialization
	void Start () {
		Cursor.SetCursor (cursorTex, hotSpot, CursorMode.Auto);
	}

}
