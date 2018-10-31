using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour {
	private BoardRules br;
	private GameObject tabuleiro;

	void Start () {
		br = GameObject.FindObjectOfType(typeof(BoardRules)) as BoardRules;
		//receber tabuleiro como parametro
		//proxima_jogada = new List<>;
		Random r = new Random();
		max(tabuleiro, int.MinValue, int.MaxValue, 0)
		//receber como parametro cor escolhida pelo jogador
	}

	// Update is called once per frame
	void Update () {
		
	}

	int max(GameObject tab, int alpha, int beta, int poda){

	}

	int min(GameObject tab, int alpha, int beta, int poda){

	}

	List jogadas_possiveis(String cor){
		//para cada peça da minha cor
			//criar uma entrada na lista com a minha peça atual e a jogada possivel
	}
}