using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour {
	private BoardRules br;
	private GameObject tabuleiro;
	private GameManager gm;
	private string time;

	void Start () {
		br = GameObject.FindObjectOfType(typeof(BoardRules)) as BoardRules;
		gm = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
		// //receber tabuleiro como parametro
		// //proxima_jogada = new List<>;
		// Random r = new Random();
		// max(tabuleiro, int.MinValue, int.MaxValue, 0);	
		// //receber como parametro cor escolhida pelo jogador
	}

	// Update is called once per frame
	void Update () {
		
	}
	public void olar(){
		GameObject game;
		Debug.Log("pqqqqqqqqqqqqqq");
	}
		
	// int max(GameObject tab, int alpha, int beta, int poda){

	// }

	// int min(GameObject tab, int alpha, int beta, int poda){

	// }

	// void jogadas_possiveis(String cor){
	// 	//para cada peça da minha cor
	// 		//criar uma entrada na lista com a minha peça atual e a jogada possivel
	// }

	public int Utility(GameObject[][]tab){
		string timeInimigo;
		if(string.Equals(this.time,"White")){
			timeInimigo = "Black";
		}else{
			timeInimigo = "White";
		}
		int numPecasInimigo = br.NumPecasTime(tab, timeInimigo);
		int numPecas = br.NumPecasTime(tab, this.time);
		return numPecas - numPecasInimigo;
	}
}