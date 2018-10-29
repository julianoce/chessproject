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

    public void setEnemyColor(){
        enemy = true;
        foreach(SpriteRenderer c in children){
           c.color = enemyPiece;
        }
    }

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
           if(enemy){
                c.color = enemyPiece;
            }else
            {
                c.color = ini;
            } 
        }
    }
}
