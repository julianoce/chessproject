using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour {
	private BoardRules br;
	private GameObject tabuleiro;
	public GameObject pretas;
	public GameObject brancas;

	void Start () {
		br = GameObject.FindObjectOfType(typeof(BoardRules)) as BoardRules;
		// //receber tabuleiro como parametro
		// //proxima_jogada = new List<>;
		// Random r = new Random();
		// max(tabuleiro, int.MinValue, int.MaxValue, 0);	
		// //receber como parametro cor escolhida pelo jogador
	}
		int MAX_PODAS = 6;
	// Update is called once per frame
	void Update () {
		
	}
	public void olar(){
		GameObject[][] tab = br.GetTabuleiro();
		GameObject[][] tab_aux = criar(tab);
		for(int i = 0; i < tab_aux.Length; i++) {
			tab_aux[i] = new GameObject[8];
			for(int j = 0; j < tab_aux[i].Length; j++) {
				if (tab[i][j] && tab[i][j].name.StartsWith("Black")){
					tab_aux[i][j] = pretas.transform.Find(tab[i][j].name).gameObject;
				}
			}
		}
		List<Vector2> l1 = br.MovimentosPossiveis(tab_aux,tab_aux[0][0]);
		GameObject aux = tab_aux[0][0];
		tab_aux[0][0]= tab_aux[0][1];
		tab_aux[0][0] = aux;
		List<Vector2> l2 = br.MovimentosPossiveis(tab_aux,tab_aux[0][0]);
		if (!l1.Equals(l2)){
			Debug.Log("FOI");
		}
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

	// void jogadas_possiveis(String cor){
	// 	//para cada peça da minha cor
	// 		//criar uma entrada na lista com a minha peça atual e a jogada possivel
	// }

	private GameObject[][] criar(GameObject[][] tab){
		GameObject[][] tab_n = new GameObject[8][];
		for(int i = 0; i < tab_n.Length; i++) {
			tab_n[i] = new GameObject[8];
		}
		return tab_n;
	}

	// private void destruir(GameObject[][] tab_n){
	// 	for(int i = 0; i < tab_n.Length; i++) {
	// 		tab_n[i] = new GameObject[8];
	// 		for(int j = 0; j < tab_n[i].Length; j++) {
	// 			Debug.Log("entrou aqui?");
	// 			if (tab_n[i][j]){
	// 				Destroy(tab_n[i][j]);
	// 			}
	// 		}
	// 	}
	// }
}