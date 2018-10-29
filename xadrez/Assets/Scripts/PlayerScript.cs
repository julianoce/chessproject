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

    //guarda qual peça está selecionada no momento
    public void setSelected(GameObject p) {
        piece = p;
    }

    //movimenta a peça para a posição do tile desejado
    public void movePiece(GameObject p, GameObject t)
    {
        if (p != null) piece = p;
        if (t != null) tile = t;
        string[] resp = null;
        Vector2 vec = new Vector2();
        if(piece != null && tile != null)
        {
            ctrlMove = true;

            string respStr = tile.name;
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
        // faz a animação de movimentação da peça
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
