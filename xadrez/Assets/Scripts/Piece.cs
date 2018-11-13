using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

	public bool jogou;
	// Use this for initialization
	void Start () {
		this.jogou = false;
	}
	
	public void jogar() {
		this.jogou = true;
	}

	public bool jahJogou() {
		return this.jogou;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
