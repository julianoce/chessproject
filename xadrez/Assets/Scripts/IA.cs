using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour {
	private BoardRules br;
	private GameObject tabuleiro;
	public GameObject pretas;
	public GameObject brancas;
	private GameManager gm;
	private string time;

	void Start () {
		br = GameObject.FindObjectOfType(typeof(BoardRules)) as BoardRules;
		// //receber tabuleiro como parametro
		// //proxima_jogada = new List<>;
		// Random r = new Random();
		// max(tabuleiro, int.MinValue, int.MaxValue, 0);	
		// //receber como parametro cor escolhida pelo jogador
	}
		int MAX_PODAS = 3;
	// Update is called once per frame
	void Update () {
		
	}
	public void olar(){
		GameObject[][] tab = br.GetTabuleiro();
		GameObject[][] tab_aux = copiar(tab);

		Debug.Log("pqqqqqqqqqqqqqq");
	}
		
	// int max(GameObject tab, int alpha, int beta, int poda){
	// 	if(poda == MAX_PODAS){
	// 		// aqui vamos chamar a funcao de avaliacao
	// 	}
	// 	int v = int.MinValue;
		
	// }

	// int min(GameObject tab, int alpha, int beta, int poda){

	// }


	private GameObject[][] criar(GameObject[][] tab){
		GameObject[][] tab_n = new GameObject[8][];
		for(int i = 0; i < tab_n.Length; i++) {
			tab_n[i] = new GameObject[8];
		}
		return tab_n;
	}

	private GameObject[][] copiar(GameObject[][] tab){
		GameObject[][] tab_aux = criar(tab);
		for(int i = 0; i < tab_aux.Length; i++) {
			tab_aux[i] = new GameObject[8];
			for(int j = 0; j < tab_aux[i].Length; j++) {
				if (tab[i][j] && tab[i][j].name.StartsWith("Black")){
					tab_aux[i][j] = pretas.transform.Find(tab[i][j].name).gameObject;
				}
				else if (tab[i][j] && tab[i][j].name.StartsWith("White")){
					tab_aux[i][j] = brancas.transform.Find(tab[i][j].name).gameObject;
				}
			}
		}
		return tab_aux;
	}

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