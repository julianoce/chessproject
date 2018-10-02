using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float speed;
    public float heigth;
    private GameObject piece;
    private GameObject tile;

    private GameManager gm;
    private BoardMapping bm;

    private float halfDist;
    private bool ctrlMove;
	// Use this for initialization
	void Start () {
        gm = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        bm = GameObject.FindObjectOfType(typeof(BoardMapping)) as BoardMapping;

        piece = null;
        tile = null;

        ctrlMove = false;
    }

    public void setSelected(GameObject p) {
        piece = p;
    }

    public void movePiece(GameObject p, GameObject t)
    {
        if (p != null) piece = p;
        if (t != null) tile = t;
        if(piece != null && tile != null)
        {
            halfDist = Vector3.Distance(piece.transform.position, tile.transform.position) / 2;
            ctrlMove = true;
        }
    }

    // Update is called once per frame
    void Update () {
        if (ctrlMove)
        {
            if (Vector3.Distance(piece.transform.position, tile.transform.position) > 0.01f)
            {
               piece.transform.position =  Vector3.Lerp(piece.transform.position, tile.transform.position, speed);
            }else
            {
                bm.clearTiles();
                ctrlMove = false;
                gm.mudaTurno();
            }
        } 
    }
}
