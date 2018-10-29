using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTile : MonoBehaviour {

    public Color change, enemyPiece, enemyPieceChanged;
	private Color ini;
	private SpriteRenderer[] children;
    private bool enemy;

    private PlayerScript ps;

	// Use this for initialization
	void Start () {
        enemy = false;
        ps = GameObject.FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
        ini = this.GetComponentInChildren<SpriteRenderer>().color;
        children = GetComponentsInChildren<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
      
    }

    //coloca a cor de peça inimiga
    public void setEnemyColor(){
        enemy = true;
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
