using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour {
	private BoardRules br;
	private GameObject tabuleiro;
	private string cor = "Black";

	private List<Vector2> jogadas = new List<Vector2>();

	const int MAX_ITE = 10;

	void Start () {
		br = GameObject.FindObjectOfType(typeof(BoardRules)) as BoardRules;
		//receber tabuleiro como parametro
		//proxima_jogada = new List<>;
		Random r = new Random();
		Max(tabuleiro, int.MinValue, int.MaxValue, 0);
		//receber como parametro cor escolhida pelo jogador
	}

	// Update is called once per frame
	void Update () {
		
	}

	int Max(GameObject tab, int alpha, int beta, int poda){
		if(poda == MAX_ITE)
			return Utility(tab);

		int v = int.MinValue;
		List<Vector2> jogadasPossiveis = br.JogadasPossiveis(this.cor);
		foreach(Vector2 jogada in jogadasPossiveis) {
			GameObject tabCopy = this.copia(tab);
			// br.AtualizaPosicoes(peca, posicao);
			int vLinha =  Min(tabCopy, alpha, beta, poda+1);

			if(poda == 0 && vLinha == v)
				this.jogadas.Add(jogada);
			if(vLinha > v) {
				v = vLinha;
				if(poda == 0) {
					this.jogadas.Clear();
					this.jogadas.Add(jogada);
				}
			}
			if(vLinha >= beta)
				return v;
			
			if(vLinha>alpha)
				alpha = vLinha;

		}
		return v;
	}

	int Min(GameObject tab, int alpha, int beta, int poda){
		if(poda == MAX_ITE)
			return Utility(tab);
		
		int v = int.MaxValue;

		List<Vector2> jogadasPossiveis = br.JogadasPossiveis(this.cor);
		foreach(Vector2 jogada in jogadasPossiveis) {
			GameObject tabCopy = this.copia(tab);
			// br.AtualizaPosicoes(peca, posicao);
			int vLinha = Max(tabCopy, alpha, beta, poda+1);
			if(vLinha < v)
				v = vLinha;
			if(vLinha <= alpha)
				return v;
			if(vLinha < beta)
				beta = vLinha;

		}

		return v;
	}


	int Utility(GameObject tab) {
		return 0;
	}

	GameObject copia(GameObject tab) {
		return tab;
	}
}