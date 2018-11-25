using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float speed;
    public float heigth;
    private GameObject piece;
    private GameObject tile;

     private GameObject pieceToMove;
    private Vector3 posToGo;

    private GameManager gm;
    private BoardMapping bm;
    private BoardRules br;

    public bool podeJogar;

    public AudioSource toc;

    private bool ctrlMove;
	// Use this for initialization
	void Start () {
        gm = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        bm = GameObject.FindObjectOfType(typeof(BoardMapping)) as BoardMapping;
        br = GameObject.FindObjectOfType(typeof(BoardRules)) as BoardRules;

        piece = null;
        pieceToMove = null;
        tile = null;

        ctrlMove = false;
        podeJogar = false;
    }

    //guarda qual peça está selecionada no momento
    public void setSelected(GameObject p) {
        piece = p;
    }

    //movimenta a peça para a posição do tile desejado
    public void movePiece(GameObject p, GameObject t)
    {
        gm.disableColliders();
        if (p != null) piece = p;
        if (t != null) tile = t;
        string[] resp = null;
        Vector2 vec = new Vector2();
        if(piece != null && tile != null)
        {
            pieceToMove = piece;
            posToGo = tile.transform.position;
            bm.clearTiles();
            
            string respStr = tile.name;
            resp = respStr.Split(new char[] { ',' });
            float[] respf = new float[2];
            for (int i = 0; i < 2; i++)
            {
                respf[i] = float.Parse(resp[i]);
            }

            vec = new Vector2(respf[0], respf[1]);

            br.AtualizaPosicoes(piece, vec);
            
            ctrlMove = true;
            toc.Play();
        } 
    }

    // Update is called once per frame
    void Update () {
        // faz a animação de movimentação da peça
        if (ctrlMove)
        {
            if (Vector3.Distance(pieceToMove.transform.position, posToGo) > 0.01f)
            {
               pieceToMove.transform.position =  Vector3.Lerp(pieceToMove.transform.position, posToGo, speed);
            }else
            {
                ctrlMove = false;
                posToGo = pieceToMove.transform.position;
                pieceToMove = null;
                podeJogar = true;
                gm.mudaTurno();
            }
        } 
    }
}
