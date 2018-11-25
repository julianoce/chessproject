using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Promotion : MonoBehaviour {

	public List<GameObject> white;
	public List<GameObject> black;
	public GameObject whitePiece, blackPiece;

	private GameObject toChange, piece;
	private GameManager gm;
	// Use this for initialization
	void Start () {
		gm = FindObjectOfType(typeof(GameManager)) as GameManager;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	// coloca a borda quando o mouse colide
	 void OnMouseEnter()
    {
        this.GetComponent<Outline>().enabled = true;
    }
    //enquanto o mouse tiver em cima pode-se escolher a peça para trocar
    void OnMouseOver()
    {
		Debug.Log(this.gameObject.name);
        if (Input.GetMouseButtonDown(0))
        {
			 switch (this.gameObject.name){
				case "W Queen":
					piece = white[0];
					break;
				case "W Knight":
					piece = white[1];
					break;
				case "W Bishop":
					piece = white[2];
					break;
				case "W Rook":
					piece = white[3];
					break;
				case "B Queen":
					piece = black[0];
					break;
				case "B Knight":
					piece = black[1];
					break;
				case "B Bishop":
					piece = black[2];
					break;
				case "B Rook":
					piece = black[3];
					break;
			}
			this.GetComponent<Outline>().enabled = false;
			instantiatePiece();
        }
    }
    //tira a borda quando o mouse sai do collider
    void OnMouseExit()
    {
        this.GetComponent<Outline>().enabled = false;
    }

	//função que instancia a peça no tabuleiro --------- falta ajustar a posição na matriz do código
	void instantiatePiece(){
		toChange = gm.getPromoteToChange();
		GameObject temp = Instantiate(piece, toChange.transform.position, toChange.transform.localRotation);
		temp.name = piece.name;
		if(this.gameObject.name.Contains("B ")){
			temp.tag = "blackPiece";
			temp.transform.parent = blackPiece.transform;
		}else if(this.gameObject.name.Contains("W ")){
			temp.tag = "whitePiece";
			temp.transform.parent = whitePiece.transform;
		}
		Destroy(toChange);
		gm.cleanPromotePlat();
	}
}
