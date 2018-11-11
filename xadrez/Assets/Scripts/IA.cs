using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour {
	private BoardRules br;
	private GameManager gm;
	private GameObject tabuleiro;
	public GameObject pretas;
	public GameObject brancas;
	private string cor = "Black";
	private List<Vector2> jogadas;
	const int MAX_ITE = 3;

	void Start () {
		br = GameObject.FindObjectOfType(typeof(BoardRules)) as BoardRules;
		jogadas = new List<Vector2>();
		cor = "Black";
		Random r = new Random();
		//receber como parametro cor escolhida pelo jogador
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void olar(){
		GameObject[][] tab = br.GetTabuleiro();
		GameObject[][] tab_aux = copiar(tab);
		int r = Max(tab_aux, int.MinValue, int.MaxValue, 0);
		Debug.Log("Terminou");
		Debug.Log(r);
	}

	int Max(GameObject[][] tab, int alpha, int beta, int poda){
		if(poda == MAX_ITE)
			return Utility(tab);

		int v = int.MinValue;
		List<Vector2> jogadasPossiveis = br.JogadasPossiveis(tab,this.cor);
		foreach(Vector2 jogada in jogadasPossiveis) {
			GameObject[][] tabCopy = this.copia_ref(tab);
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

	int Min(GameObject[][] tab, int alpha, int beta, int poda){
		if(poda == MAX_ITE)
			return Utility(tab);
		
		int v = int.MaxValue;

		List<Vector2> jogadasPossiveis = br.JogadasPossiveis(tab,this.cor);
		foreach(Vector2 jogada in jogadasPossiveis) {
			GameObject[][] tabCopy = this.copia_ref(tab);
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
		

	private GameObject[][] criar(GameObject[][] tab){
		GameObject[][] tab_n = new GameObject[8][];
		for(int i = 0; i < tab_n.Length; i++) {
			tab_n[i] = new GameObject[8];
		}
		return tab_n;
	}

	private GameObject[][] copia_ref(GameObject[][] tab){
		GameObject[][] tab_aux = criar(tab);
		for(int i = 0; i < tab_aux.Length; i++) {
			tab_aux[i] = new GameObject[8];
			for(int j = 0; j < tab_aux[i].Length; j++) {
				tab_aux[i][j] = tab[i][j];
			}
		}
		return tab;
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
		if(string.Equals(this.cor,"White")){
			timeInimigo = "Black";
		}else{
			timeInimigo = "White";
		}
		int numPecasInimigo = br.NumPecasTime(tab, timeInimigo);
		int numPecas = br.NumPecasTime(tab, this.cor);
		return numPecas - numPecasInimigo;
	}
}