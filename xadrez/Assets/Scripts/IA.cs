using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour {
	private BoardRules br;
	private GameManager gm;
	private GameObject tabuleiro;
	public GameObject pretas;
	public GameObject brancas;
	private string cor;
	private string cor_adv;
	private List<Vector2> jogadas;
	private List<GameObject> quem;
	const int MAX_ITE = 3;

	void Start () {
		br = GameObject.FindObjectOfType(typeof(BoardRules)) as BoardRules;
		cor = "Black";
		cor_adv = "White";
		
		//receber como parametro cor escolhida pelo jogador
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void olar(){
		GameObject[][] tab = br.GetTabuleiro();
		GameObject[][] tab_aux = copiar(tab);
		this.jogadas = new List<Vector2>();
		this.quem = new List<GameObject>();
	 	Max(tab_aux, int.MinValue, int.MaxValue, 0);
		System.Random r = new System.Random();
		int x = r.Next(0,jogadas.Count);
		Debug.Log("Respostas");
		Debug.Log(quem[x]);
		Debug.Log(jogadas[x]);
		Debug.Log("Terminou");
	}

	int Max(GameObject[][] tab, int alpha, int beta, int poda){
		if(poda == MAX_ITE)
			return Utility(tab);

		int v = int.MinValue;

		for(int i = 0; i < tab.Length; i++) {
			for(int j = 0; j < tab[i].Length; j++) {
				if(tab[i][j] && tab[i][j].name.StartsWith(this.cor)) {
					List<Vector2> jogadasPossiveis = br.MovimentosPossiveis(tab,tab[i][j]);
					foreach(Vector2 jogada in jogadasPossiveis) {
						GameObject[][] tabCopy = this.copia_ref(tab);
						br.AtualizaPosicoesRelativas(tabCopy, tab[i][j], jogada);
						int vLinha =  Min(tabCopy, alpha, beta, poda+1);
						if(poda == 0 && vLinha == v){
							this.jogadas.Add(jogada);
							this.quem.Add(tab[i][j]);
						}
						if(vLinha > v) {
							v = vLinha;
							if(poda == 0) {
								this.jogadas.Clear();
								this.quem.Clear();
								this.jogadas.Add(jogada);
								this.quem.Add(tab[i][j]);
							}
						}
						if(vLinha >= beta){
							return v;
						}
						if(vLinha>alpha){
							alpha = vLinha;
						}
					}
				}
			}
		}
		return v;
	}

	int Min(GameObject[][] tab, int alpha, int beta, int poda){
		if(poda == MAX_ITE)
			return Utility(tab);
		
		int v = int.MaxValue;
		for(int i = 0; i < tab.Length; i++) {
			for(int j = 0; j < tab[i].Length; j++) {
				if(tab[i][j] && tab[i][j].name.StartsWith(this.cor_adv)) {
					List<Vector2> jogadasPossiveis = br.MovimentosPossiveis(tab,tab[i][j]);
					foreach(Vector2 jogada in jogadasPossiveis) {
						GameObject[][] tabCopy = this.copia_ref(tab);
						br.AtualizaPosicoesRelativas(tabCopy, tab[i][j], jogada);
						int vLinha = Max(tabCopy, alpha, beta, poda+1);
						if(vLinha < v)
							v = vLinha;

						if(vLinha <= alpha)
							return v;

						if(vLinha < beta)
							beta = vLinha;
					}
				}
			}
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
			for(int j = 0; j < tab_aux[i].Length; j++) {
				tab_aux[i][j] = tab[i][j];
			}
		}
		return tab_aux;
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
		int numPecasInimigo = 0;
		int numPecas = 0;
		for(int i=0; i<tab.Length;i++){
			for(int j=0; j<tab.Length;j++){
				if(tab[i][j]){
					if(tab[i][j].name.StartsWith(this.cor_adv) && tab[i][j].name.Contains("Queen")){
						numPecasInimigo += 9;
					}
					if(tab[i][j].name.StartsWith(this.cor) && tab[i][j].name.Contains("Queen")){
						numPecas += 9;
					}
					if(tab[i][j].name.StartsWith(this.cor_adv) && tab[i][j].name.Contains("Rook")){
						numPecasInimigo += 5;
					}
					if(tab[i][j].name.StartsWith(this.cor) && tab[i][j].name.Contains("Rook")){
						numPecas += 5;
					}

					if(tab[i][j].name.StartsWith(this.cor_adv) && tab[i][j].name.Contains("Knight") || tab[i][j].name.Contains("Bishop")){
						numPecasInimigo += 3;
					}
					if(tab[i][j].name.StartsWith(this.cor) && (tab[i][j].name.Contains("Knight") || tab[i][j].name.Contains("Bishop"))){
						numPecas += 3;
					}

					if(tab[i][j].name.StartsWith(this.cor_adv) && tab[i][j].name.Contains("Pawn")){
						numPecasInimigo += 1;
					}
					if(tab[i][j].name.StartsWith(this.cor) && tab[i][j].name.Contains("Pawn")){
						numPecas += 1;
					}

					if(tab[i][j].name.StartsWith(this.cor_adv) && tab[i][j].name.Contains("King")){
						numPecasInimigo += 10000000;
					}
					if(tab[i][j].name.StartsWith(this.cor) && tab[i][j].name.Contains("King")){
						numPecas += 10000000;
					}
				}
			}
		}
		return numPecas - numPecasInimigo;
	}
}