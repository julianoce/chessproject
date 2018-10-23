using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTile : MonoBehaviour {

    public Color change;
	private Color ini;
	private SpriteRenderer[] children;

    private PlayerScript ps;

	// Use this for initialization
	void Start () {
        ps = GameObject.FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
        ini = this.GetComponentInChildren<SpriteRenderer>().color;
        children = GetComponentsInChildren<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
      
    }
    void OnMouseEnter()
    {
        foreach(SpriteRenderer c in children){
            c.color = change;
        }
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ps.movePiece(null, this.gameObject);
        }
    }
    void OnMouseExit()
    {
         foreach(SpriteRenderer c in children){
            c.color = ini;
        }
    }
}
