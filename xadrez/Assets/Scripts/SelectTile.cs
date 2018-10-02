using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTile : MonoBehaviour {

    private MeshRenderer render;
    private Material mat;
    private Color cInit;

    private PlayerScript ps;

    public Color selected;
	// Use this for initialization
	void Start () {
        ps = GameObject.FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
        render = this.GetComponent<MeshRenderer>();
        mat = render.materials[0];
        cInit = mat.color;
    }
	
	// Update is called once per frame
	void Update () {
      
    }
    void OnMouseEnter()
    {
        mat.color = selected;
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
        mat.color = cInit;
    }
}
