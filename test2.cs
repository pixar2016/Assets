using UnityEngine;
using System.Collections;

public class test2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Sprite sprite = this.GetComponent<SpriteRenderer>().sprite;
        for (int i = 0; i < sprite.uv.Length; i++)
        {
            Debug.Log(sprite.uv[i]);
        }
        for (int i = 0; i < sprite.vertices.Length; i++)
        {
            Debug.Log(sprite.vertices[i]);
        }
        sprite.vertices[0] = new Vector2(10, 0);
        Debug.Log(sprite.vertices[0]);
        Debug.Log(sprite.triangles);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
