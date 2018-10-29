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
    private BoardRules br;

    private float halfDist;
    private bool ctrlMove;
	// Use this for initialization
	void Start () {
        gm = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        bm = GameObject.FindObjectOfType(typeof(BoardMapping)) as BoardMapping;
        br = GameObject.FindObjectOfType(typeof(BoardRules)) as BoardRules;

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
        string[] resp = null;
        Vector2 vec = new Vector2();
        if(piece != null && tile != null)
        {
            halfDist = Vector3.Distance(piece.transform.position, tile.transform.position) / 2;
            ctrlMove = true;
            string respStr = tile.name;
            Debug.Log(respStr);
            resp = respStr.Split(new char[] { ',' });
            float[] respf = new float[2];
            for (int i = 0; i < 2; i++)
            {
                respf[i] = float.Parse(resp[i]);
            }
            vec = new Vector2(respf[0], respf[1]);
        }
        
        br.AtualizaPosicoes(piece, vec);
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
