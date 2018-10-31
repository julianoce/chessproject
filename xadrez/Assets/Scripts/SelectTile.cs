using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTile : MonoBehaviour {

    public Color change, enemyPiece, enemyPieceChanged;
	private Color ini;
	private SpriteRenderer[] children;
    private bool enemy;

    private PlayerScript ps;
    private BoardRules br;

	// Use this for initialization
	void Start () {
        enemy = false;
        ps = GameObject.FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
        br = GameObject.FindObjectOfType(typeof(BoardRules)) as BoardRules;
        ini = this.GetComponentInChildren<SpriteRenderer>().color;
        children = GetComponentsInChildren<SpriteRenderer>();
        verificaInimigo();
    }
	
	// Update is called once per frame
	void Update () {
      
    }

    void verificaInimigo (){
        string respStr = this.name;
        string[] resp = respStr.Split(new char[] { ',' });
        float[] respf = new float[2];
        for (int i = 0; i < 2; i++)
        {
            respf[i] = float.Parse(resp[i]);
        }

        Vector2 vec = new Vector2(respf[0], respf[1]);
        Debug.Log(vec);
        Debug.Log(this);
        if(br.verifyPosition(vec)){
            setEnemyColor();
        }
    }

    //coloca a cor de peça inimiga
    public void setEnemyColor(){
        enemy = true;
        Debug.Log(children);
        foreach(SpriteRenderer c in children){
           c.color = enemyPiece;
        }
    }

    //muda a cor quando mouse entra
    void OnMouseEnter()
    {
        foreach(SpriteRenderer c in children){
            if(enemy){
                c.color = enemyPieceChanged;
            }else
            {
                c.color = change;
            } 
        }
    }
    //enquanto o mouse tiver em cima pode-se mover a peça para essa posição
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ps.movePiece(null, this.gameObject);
        }
    }
    //volta a cor original quando o mouse sai
    void OnMouseExit()
    {
         foreach(SpriteRenderer c in children){
           if(enemy){
                c.color = enemyPiece;
            }else
            {
                c.color = ini;
            } 
        }
    }
}
