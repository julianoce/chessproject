using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRules : MonoBehaviour {

	public GameObject brancas;
	public GameObject pretas;

	// false se nunca andou, true se já
	// peão n posição 0 = peão 1. peão na posição 1 = peão 2 e por ai vai
	public bool[] peoesPretos;
	public bool[] peoesBrancos;

	// posição 0 é o rei branco, posição 1 é o rei preto
	// false se nunca andou, true se já
	public bool[] reiAndou;
	// posição 0 é a torre mais perto, posilão 1 é torre mais longa
	public bool[] torresBrancas;
	public bool[] torresPretas;
	private bool roque;

	private GameObject[][] tabuleiro;
	private Vector2 reiBranco;
	private Vector2 reiPreto;
	private bool reiBrancoEmCheck;
	private bool reiPretoEmCheck;
	Vector2 posPeca = new Vector2();

	bool parouDir, parouEsq;
	private GameManager gm;
	
	// Use this for initialization
	void Start () {
		gm = FindObjectOfType(typeof(GameManager)) as GameManager;
		roque = false;
		tabuleiro = new GameObject[8][];
		reiBrancoEmCheck = false;
		reiPretoEmCheck = false;
		peoesBrancos = new bool[8];
		peoesPretos = new bool[8];
		reiAndou = new bool[2];
		torresBrancas = new bool[2];
		torresPretas = new bool[2];
		for(int i = 0; i < tabuleiro.Length; i++) {
			tabuleiro[i] = new GameObject[8];
			for(int j = 0; j < tabuleiro[i].Length; j++) {
				
				if(i == 1) {
					string nome = "White Pawn ";
					nome += (j+1).ToString();
					tabuleiro[i][j] = brancas.transform.Find(nome).gameObject;
				} else if(i == 6) {
					string nome = "Black Pawn ";
					nome += (j+1).ToString();
					tabuleiro[i][j] = pretas.transform.Find(nome).gameObject;
				} else if(i == 0) {
					switch(j) {
						case 0:
							tabuleiro[i][j] = brancas.transform.Find("White Rook 2").gameObject;
							break;
						case 1:
							tabuleiro[i][j] = brancas.transform.Find("White Knight 2").gameObject;
							break;
						case 2:
							tabuleiro[i][j] = brancas.transform.Find("White Bishop 2").gameObject;
							break;
						case 3:
							tabuleiro[i][j] = brancas.transform.Find("White King").gameObject;
							reiBranco = new Vector2(i, j);
							break;
						case 4:
							tabuleiro[i][j] = brancas.transform.Find("White Queen").gameObject;
							break;
						case 5:
							tabuleiro[i][j] = brancas.transform.Find("White Bishop 1").gameObject;
							break;
						case 6:
							tabuleiro[i][j] = brancas.transform.Find("White Knight 1").gameObject;
							break;
						case 7: 
							tabuleiro[i][j] = brancas.transform.Find("White Rook 1").gameObject;
							break;
					}
				} else if(i == 7) {
					switch(j) {
						case 0:
							tabuleiro[i][j] = pretas.transform.Find("Black Rook 2").gameObject;
							break;
						case 1:
							tabuleiro[i][j] = pretas.transform.Find("Black Knight 2").gameObject;
							break;
						case 2:
							tabuleiro[i][j] = pretas.transform.Find("Black Bishop 2").gameObject;
							break;
						case 3:
							tabuleiro[i][j] = pretas.transform.Find("Black King").gameObject;
							reiPreto = new Vector2(i, j);
							break;
						case 4:
							tabuleiro[i][j] = pretas.transform.Find("Black Queen").gameObject;
							break;
						case 5:
							tabuleiro[i][j] = pretas.transform.Find("Black Bishop 1").gameObject;
							break;
						case 6:
							tabuleiro[i][j] = pretas.transform.Find("Black Knight 1").gameObject;
							break;
						case 7: 
							tabuleiro[i][j] = pretas.transform.Find("Black Rook 1").gameObject;
							break;
					}
				} else {
					tabuleiro[i][j] = null;

				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public int NumPecasTime(GameObject[][]tab, string time){
		int countPeca = 0;
		for(int i=0; i< tab.Length;i++){
			for(int j=0;j<tab.Length;j++){
				if(tab[i][j]!= null){
					GameObject peca = tab[i][j];
					if(peca.name.StartsWith(time)){
						countPeca+=1;
					}
				}
			}
		}
		return countPeca;
	}
	public List<Vector2> MovimentosPossiveis(GameObject peca) {
		return MovimentosPossiveis(this.tabuleiro, peca);
	}

	private List<Vector2> rook_moves(string adv_color, Vector2 posPeca, List<Vector2> resultado){
		// Os 2 'for' seguintes são para subir e descer no tabuleiro
			for(int i = (int)posPeca.x + 1; i < tabuleiro.Length; i++) {
				if(tabuleiro[i][(int)posPeca.y] == null ) {
					//Se o lugar ta vazio pode mover
					resultado.Add(new Vector2(i, (int)posPeca.y));
				} else if (tabuleiro[i][(int)posPeca.y].name.StartsWith(adv_color)) {
					// Se o lugar possui uma peça preta pode mover, mas se sabe que a posições seguintes são invalidas
					resultado.Add(new Vector2(i, (int)posPeca.y));
					break;
				} else {
					// Se o lugar não ta vazio e nem é uma peça preta significa que é uma peça branca, logo não pode mover
					break;
				}
			}
			for(int i = (int)posPeca.x - 1; i >= 0; i--) {
				if(tabuleiro[i][(int)posPeca.y] == (null)) {
					// Se o lugar ta vazio pode mover
					resultado.Add(new Vector2(i, (int)posPeca.y));
				} else if (tabuleiro[i][(int)posPeca.y].name.StartsWith(adv_color)) {
					// Se o lugar possui uma peça preta pode mover, mas se sabe que as posições seguintes são invalidas
					resultado.Add(new Vector2(i, (int)posPeca.y));
					break;
				} else {
					// Se o lugar não ta vazio e nem é uma peça preta significa que é uma peça branca, logo não pode mover
					break;
				}
			}

			// Os proximos dois 'for' são para os movimentos laterais
			for(int i = (int)posPeca.y + 1; i < tabuleiro[(int)posPeca.x].Length; i++) {
				if(tabuleiro[(int)posPeca.x][i] == (null)) {
					//Se o lugar ta vazio pode mover
					resultado.Add(new Vector2((int)posPeca.x, i));
				} else if(tabuleiro[(int)posPeca.x][i].name.StartsWith(adv_color)) {
					// Se o lugar possui uma peça preta pode mover, mas se sabe que as posições seguintes são invalidas
					resultado.Add(new Vector2((int)posPeca.x, i));
					break;
				} else {
					// Se o lugar não ta vazio e nem é uma peça preta significa que é uma peça branca, logo não pode mover
					break;
				}
			}
			for(int i = (int)posPeca.y - 1; i >= 0; i--) {
				if(tabuleiro[(int)posPeca.x][i] == (null)) {
					//Se o lugar ta vazio pode mover
					resultado.Add(new Vector2((int)posPeca.x, i));
				} else if(tabuleiro[(int)posPeca.x][i].name.StartsWith(adv_color)) {
					// Se o lugar possui uma peça preta pode mover, mas se sabe que as posições seguintes são invalidas
					resultado.Add(new Vector2((int)posPeca.x, i));
					break;
				} else {
					// Se o lugar não ta vazio e nem é uma peça preta significa que é uma peça branca, logo não pode mover
					break;
				}
			}
		return resultado;
	}

	private List<Vector2> bishop_moves(string adv_color, Vector2 posPeca, List<Vector2> resultado){
		bool parouDir = false, parouEsq = false;
		// Primeiro 'for' é para os bispos brancos subirem no tabuleiro
		int j = 1;
		for(int i = (int)posPeca.x + 1; i < tabuleiro.Length; i++) {
			if(!parouEsq || !parouDir) {
				// Se entrou aqui ainda da pra subir pra algum lado

				// Os 'ifs' abaixo são pra saber se saiu do tabuleiro
				if((int)posPeca.y + j >= tabuleiro.Length) {
					parouEsq = true;
				}
				if((int)posPeca.y - j < 0) {
					parouDir = true;
				}

				if(!parouEsq) {
					// Se entrou aqui ainda da pra subir pra esquerda
					if(tabuleiro[i][(int)posPeca.y + j] == null) {
						// Se o lugar ta vazio
						resultado.Add(new Vector2(i, (int)posPeca.y + j));
					} else if(tabuleiro[i][(int)posPeca.y + j].name.StartsWith(adv_color)) {
						// Se o lugar possui uma peça preta pode mover, mas se sabe as posições seguintes são invalidas
						resultado.Add(new Vector2(i, (int)posPeca.y + j));
						parouEsq = true;
					} else {
						parouEsq = true;
					} 
				}
				if(!parouDir) {
					// Se entrou aqui ainda da pra subir pra direita
					if(tabuleiro[i][(int)posPeca.y - j] == null) {
						// Se o lugar ta vazio
						resultado.Add(new Vector2(i, (int)posPeca.y - j));
					} else if(tabuleiro[i][(int)posPeca.y - j].name.StartsWith(adv_color)) {
						// Se o lugar possui uma peça preta pode mover, mas se sabe as posições seguintes são invalidas
						resultado.Add(new Vector2(i, (int)posPeca.y - j));
						parouDir = true;
					} else {
						parouDir = true;
					} 
				}
				j++;
			} else {
				// parouEsq && parouDir
				break;
			}
		}
		parouDir = false; parouEsq = false;
		//Segundo for é para os bispos brancos descerem no tabuleiro
		j = 1;
		for(int i = (int)posPeca.x - 1; i >= 0; i--) {
			if(!parouDir || !parouEsq) {
				// Se entrou aqui ainda da para descer para algum lado

				// Os 'ifs' abaixo são pra saber se saiu do tabuleiro
				if((int)posPeca.y + j >= tabuleiro.Length) {
					parouEsq = true;
				}
				if((int)posPeca.y - j < 0) {
					parouDir = true;
				}
				if(!parouEsq) {
					// Se entrou aqui ainda da pra descer pra esquerda
					if(tabuleiro[i][(int)posPeca.y + j] == null) {
						// Se o lugar ta vazio
						resultado.Add(new Vector2(i, (int)posPeca.y + j));
					} else if(tabuleiro[i][(int)posPeca.y + j].name.StartsWith(adv_color)) {
						// Se o lugar possui uma peça preta pode mover, mas se sabe as posições seguintes são invalidas
						resultado.Add(new Vector2(i, (int)posPeca.y + j));
						parouEsq = true;
					} else {
						parouEsq = true;
					} 
				}
				if(!parouDir) {
					// Se entrou aqui ainda da pra descer pra direita
					if(tabuleiro[i][(int)posPeca.y - j] == null) {
						// Se o lugar ta vazio
						resultado.Add(new Vector2(i, (int)posPeca.y - j));
					} else if(tabuleiro[i][(int)posPeca.y - j].name.StartsWith(adv_color)) {
						// Se o lugar possui uma peça preta pode mover, mas se sabe as posições seguintes são invalidas
						resultado.Add(new Vector2(i, (int)posPeca.y - j));
						parouDir = true;
					} else {
						parouDir = true;
					} 
				}
				j++;
			} else {
				// parouEsq && parouDir
				break;
			}
		}
		return resultado;
	}

	private List<Vector2> knight_moves(string color, string adv_color, Vector2 posPeca, List<Vector2> resultado){
		int xPeca = (int)posPeca.x, yPeca = (int)posPeca.y;
			//Debug.Log(peca.name + ": (" + xPeca + "," + yPeca + ")");
			for(int i = 1; i < 3; i++) {
				for(int j = 1; j < 3; j++) {
					if(i == j) continue;
					if(xPeca + i < tabuleiro.Length) {
						// Pode subir
						if(yPeca + j < tabuleiro.Length) {
							// Pode ir para a esquerda da matriz
							if((tabuleiro[xPeca + i][yPeca + j] == null) || 
								tabuleiro[xPeca + i][yPeca + j].name.StartsWith(adv_color)) {
								// Se a peça clicada é branca e a posição desejada contem uma peça preta ou nenhuma peça
								resultado.Add(new Vector2(xPeca + i, yPeca + j));
							}
						}
						if(yPeca - j >= 0) {
							// Pode ir para a direita da matriz
							if((tabuleiro[xPeca + i][yPeca - j] == null) || 
								tabuleiro[xPeca + i][yPeca - j].name.StartsWith(adv_color)) {
								// Se a peça clicada é branca e a posição desejada contem uma peça preta ou nenhuma peça
								resultado.Add(new Vector2(xPeca + i, yPeca - j));
							}
						}
					}
					if(xPeca - i >= 0) {
						// Pode descer!! LEMBRANDO: SEMPRE EM RELAÇÃO A MATRIZ
						if(yPeca + j < tabuleiro.Length) {
							// Pode ir para a esquerda da matriz
							if((tabuleiro[xPeca - i][yPeca + j] == null) || 
								tabuleiro[xPeca - i][yPeca + j].name.StartsWith(adv_color)) {
								// Se a peça clicada é branca e a posição desejada contem uma peça preta ou nenhuma peça
								resultado.Add(new Vector2(xPeca - i, yPeca + j));
							}
						}
						if(yPeca - j >= 0) {
							// Pode ir para a direita da matriz
							if((tabuleiro[xPeca - i][yPeca - j] == null) || 
								tabuleiro[xPeca - i][yPeca - j].name.StartsWith(adv_color)) {
								// Se a peça clicada é branca e a posição desejada contem uma peça preta ou nenhuma peça
								resultado.Add(new Vector2(xPeca - i, yPeca - j));
							}
						}
					}
				}
			}
		return resultado;
	}

	private List<Vector2> king_moves(string color, string adv_color, Vector2 posPeca, List<Vector2> resultado){
		int xPeca = (int)posPeca.x, yPeca = (int)posPeca.y;
			if(xPeca + 1 < tabuleiro.Length) {
				// Pode subir
				if(tabuleiro[xPeca + 1][yPeca] == null || 
				(tabuleiro[xPeca + 1][yPeca].name.StartsWith(adv_color))) {
					// Para cima na matriz está vazio ou contem peça inimiga
					resultado.Add(new Vector2(xPeca + 1, yPeca));
				}
				if(yPeca + 1 < tabuleiro.Length && 
				(tabuleiro[xPeca + 1][yPeca + 1] == null || 
				(tabuleiro[xPeca + 1][yPeca + 1].name.StartsWith(adv_color)))) {
					// Para cima e para esquerda em relação a matriz está vazio ou contem peça inimiga
					resultado.Add(new Vector2(xPeca + 1, yPeca + 1));
				}
				if(yPeca - 1 >= 0 && 
				(tabuleiro[xPeca + 1][yPeca - 1] == null || 
				(tabuleiro[xPeca + 1][yPeca - 1].name.StartsWith(adv_color)))) {
					// Para cima e para direita em relação a matriz está vazio ou contem peça inimiga
					resultado.Add(new Vector2(xPeca + 1, yPeca - 1));
				}
			}
			if(xPeca - 1 >= 0) {
				// Pode descer
				if(tabuleiro[xPeca - 1][yPeca] == null || 
				(tabuleiro[xPeca - 1][yPeca].name.StartsWith(adv_color))) {
					// Para baixo na matriz está vazio ou contem peça inimiga
					resultado.Add(new Vector2(xPeca - 1, yPeca));
				}
				if(yPeca + 1 < tabuleiro.Length && 
				(tabuleiro[xPeca - 1][yPeca + 1] == null || 
				(tabuleiro[xPeca - 1][yPeca + 1].name.StartsWith(adv_color)))) {
					// Para baixo e para esquerda em relação a matriz está vazio ou contem peça inimiga
					resultado.Add(new Vector2(xPeca - 1, yPeca + 1));
				}
				if(yPeca - 1 >= 0 && 
				(tabuleiro[xPeca - 1][yPeca - 1] == null || 
				(tabuleiro[xPeca - 1][yPeca - 1].name.StartsWith(adv_color)))) {
					// Para baixo e para direita em relação a matriz está vazio ou contem peça inimiga
					resultado.Add(new Vector2(xPeca - 1, yPeca - 1));
				}
			}
			if(yPeca + 1 < tabuleiro.Length &&
				(tabuleiro[xPeca][yPeca + 1] == null|| 
				(tabuleiro[xPeca][yPeca + 1].name.StartsWith(adv_color)))) {
					// Para esquerda em relação a matriz está vazio ou contem peça inimiga
					resultado.Add(new Vector2(xPeca, yPeca + 1));
			}
			if(yPeca - 1 >= 0 &&
				(tabuleiro[xPeca][yPeca - 1] == null || 
				(tabuleiro[xPeca][yPeca - 1].name.StartsWith(adv_color)))) {
					// Para direita em relação a matriz está vazio ou contem peça inimiga
					resultado.Add(new Vector2(xPeca, yPeca - 1));
			}
			// condições para o roque
			if((color.Equals("White") && reiAndou[0] == false) || (color.Equals("Black") && reiAndou[1] == false))  {
				// rei não andou
				if((torresBrancas[0] == false || torresPretas[0] == false) && tabuleiro[xPeca][yPeca - 1] == null && tabuleiro[xPeca][yPeca - 2] == null) {
					// rei pode fazer o roque com a torre mais proxima
					resultado.Add(new Vector2(xPeca, yPeca - 2));
					roque = true;
				} else if((torresBrancas[1] == false || torresPretas[1] == false) && tabuleiro[xPeca][yPeca + 1] == null && tabuleiro[xPeca][yPeca + 2] == null && tabuleiro[xPeca][yPeca + 3] == null) {
					// rei pode fazer o roque com a torre mais longe
					resultado.Add(new Vector2(xPeca, yPeca + 2));
					roque = true;
				}
			}
		return resultado;
	}

	private List<Vector2> queen_moves(string color, string adv_color, Vector2 posPeca, List<Vector2> resultado){
		parouDir = false; parouEsq = false;
		bool parouCima = false, parouBaixo = false;
		bool parouDSD = false, parouDSE = false, parouDID = false, parouDIE = false;
		int xPeca = (int)posPeca.x, yPeca = (int)posPeca.y;
		for(int i = 1; i < tabuleiro.Length; i++) {
			if(!parouCima && xPeca + i < tabuleiro.Length) {
				// 'if' para subir no tabuleiro em relação a matriz
				// Ainda não achou peça inimiga e nem chegou no fim do tabuleiro
				if(tabuleiro[xPeca + i][yPeca] == null) {
					// Lugar vazio, só adicionar
					resultado.Add(new Vector2(xPeca + i, yPeca));
				} else if(tabuleiro[xPeca + i][yPeca].name.StartsWith(adv_color)) {
					// Peça selecionada é branca, lugar checado tem peça preta
					resultado.Add(new Vector2(xPeca + i, yPeca));
					parouCima = true;
				} else {
					parouCima = true;
				}
			}
			if(!parouBaixo && xPeca - i >= 0) {
				// 'if' para descer no tabuleiro em relação a matriz
				// Ainda não achou peça inimiga e nem chegou no fim do tabuleiro
				if(tabuleiro[xPeca - i][yPeca] == null) {
					// Lugar vazio, só adicionar
					resultado.Add(new Vector2(xPeca - i, yPeca));
				} else if(tabuleiro[xPeca - i][yPeca].name.StartsWith(adv_color)) {
					// Peça selecionada é branca, lugar checado tem peça preta
					resultado.Add(new Vector2(xPeca - i, yPeca));
					parouBaixo = true;
				} else {
					parouBaixo = true;
				}
			}
			if(!parouEsq && yPeca + i < tabuleiro.Length) {
				// 'if' para andar pra esquerda no tabuleiro em relação a matriz
				// Ainda não achou peça inimiga e nem chegou no fim do tabuleiro
				if(tabuleiro[xPeca][yPeca + i] == null) {
					// Lugar vazio, só adicionar
					resultado.Add(new Vector2(xPeca, yPeca + i));
				} else if(tabuleiro[xPeca][yPeca + i].name.StartsWith(adv_color)) {
					// Peça selecionada é branca, lugar checado tem peça preta
					resultado.Add(new Vector2(xPeca, yPeca + i));
					parouEsq = true;
				} else {
					parouEsq = true;
				}
			} 
			if(!parouDir && yPeca - i >= 0) {
				// 'if' para andar pra direita no tabuleiro em relação a matriz
				// Ainda não achou peça inimiga e nem chegou no fim do tabuleiro
				if(tabuleiro[xPeca][yPeca - i] == null) {
					// Lugar vazio, só adicionar
					resultado.Add(new Vector2(xPeca, yPeca - i));
				} else if(tabuleiro[xPeca][yPeca - i].name.StartsWith(adv_color)) {
					// Peça selecionada é branca, lugar checado tem peça preta
					resultado.Add(new Vector2(xPeca, yPeca - i));
					parouDir = true;
				} else {
					parouDir = true;
				}
			}
			if(!parouDSE && xPeca + i < tabuleiro.Length && yPeca + i < tabuleiro.Length) {
				// 'if' para andar para diagonal superior esquerda no tabuleiro em relação a matriz
				// Ainda não achou peça inimiga e nem chegou no fim do tabuleiro
				if(tabuleiro[xPeca + i][yPeca + i] == null) {
					// Lugar vazio, só adicionar
					resultado.Add(new Vector2(xPeca + i, yPeca + i));
				} else if(tabuleiro[xPeca + i][yPeca + i].name.StartsWith(adv_color)) {
					// Peça selecionada é branca, lugar checado tem peça preta
					resultado.Add(new Vector2(xPeca + i, yPeca + i));
					parouDSE = true;
				} else {
					parouDSE = true;
				}
			}
			if(!parouDSD && xPeca + i < tabuleiro.Length && yPeca - i >= 0) {
				// 'if' para andar para diagonal superior direita no tabuleiro em relação a matriz
				// Ainda não achou peça inimiga e nem chegou no fim do tabuleiro
				if(tabuleiro[xPeca + i][yPeca - i] == null) {
					// Lugar vazio, só adicionar
					resultado.Add(new Vector2(xPeca + i, yPeca - i));
				} else if(tabuleiro[xPeca + i][yPeca - i].name.StartsWith(adv_color)) {
					// Peça selecionada é branca, lugar checado tem peça preta
					resultado.Add(new Vector2(xPeca + i, yPeca - i));
					parouDSD = true;
				} else {
					parouDSD = true;
				}
			}
			if(!parouDIE && xPeca - i >= 0 && yPeca + i < tabuleiro.Length) {
				// 'if' para andar para diagonal inferior esquerda no tabuleiro em relação a matriz
				// Ainda não achou peça inimiga e nem chegou no fim do tabuleiro
				if(tabuleiro[xPeca - i][yPeca + i] == null) {
					// Lugar vazio, só adicionar
					resultado.Add(new Vector2(xPeca - i, yPeca + i));
				} else if(tabuleiro[xPeca - i][yPeca + i].name.StartsWith(adv_color)) {
					// Peça selecionada é branca, lugar checado tem peça preta
					resultado.Add(new Vector2(xPeca - i, yPeca + i));
					parouDIE = true;
				} else {
					parouDIE = true;
				}
			}
			if(!parouDID && xPeca - i >= 0 && yPeca - i >= 0) {
				// 'if' para andar para diagonal inferior direita no tabuleiro em relação a matriz
				// Ainda não achou peça inimiga e nem chegou no fim do tabuleiro
				if(tabuleiro[xPeca - i][yPeca - i] == null) {
					// Lugar vazio, só adicionar
					resultado.Add(new Vector2(xPeca - i, yPeca - i));
				} else if(tabuleiro[xPeca - i][yPeca - i].name.StartsWith(adv_color)) {
					// Peça selecionada é branca, lugar checado tem peça preta
					resultado.Add(new Vector2(xPeca - i, yPeca - i));
					parouDID = true;
				} else {
					parouDID = true;
				}
			}
		}
		return resultado;
	}

	public List<Vector2> MovimentosPossiveis(GameObject[][] tabuleiro, GameObject peca) {
		roque = false;
		posPeca = new Vector2();
		List<Vector2> resultado = new List<Vector2>();
		if(tabuleiro == null) {
			Debug.Log("oiiiiii");
		}
		for(int i = 0; i < tabuleiro.Length; i++) {
			for(int j = 0; j < tabuleiro[i].Length; j++) {
				//acha a peça
				if(peca.Equals(tabuleiro[i][j])) {
					posPeca = new Vector2(i, j);
				}
			}
		} 

		bool parouDir, parouEsq;

		// Movimento do peões
		if(peca.name.StartsWith("White Pawn")) {
			int numeroPeao = (int)peca.name.ToCharArray()[11] - 49;
			if((int)posPeca.x + 1 < tabuleiro.Length) {
				if(tabuleiro[(int)posPeca.x + 1][(int)posPeca.y] == null) {
					// Só pode andar pra frente se estiver vazio
					resultado.Add(new Vector2(posPeca.x + 1, posPeca.y));
					if(peca.name.StartsWith("White") && peoesBrancos[numeroPeao] == false && tabuleiro[(int)posPeca.x + 2][(int)posPeca.y] == null) {
						resultado.Add(new Vector2(posPeca.x + 2, posPeca.y));
					} 
				}
				if((int)posPeca.y + 1 < tabuleiro.Length && 
					tabuleiro[(int)posPeca.x + 1][(int)posPeca.y + 1] != null && 
					tabuleiro[(int)posPeca.x + 1][(int)posPeca.y + 1].name.StartsWith("Black")) {
					// Comer para diagonal esquerda
					resultado.Add(new Vector2(posPeca.x + 1, posPeca.y + 1));
				}
				if((int)posPeca.y - 1 >= 0 && 
					tabuleiro[(int)posPeca.x + 1][(int)posPeca.y - 1] != null && 
					tabuleiro[(int)posPeca.x + 1][(int)posPeca.y - 1].name.StartsWith("Black")) {
					// Comer para diagonal direita
					resultado.Add(new Vector2(posPeca.x + 1, posPeca.y - 1));
				}
			}
			
		} else if(peca.name.StartsWith("Black Pawn")) {
			int numeroPeao = (int)peca.name.ToCharArray()[11] - 49;
			if((int)posPeca.x - 1 >= 0) {
				if(tabuleiro[(int)posPeca.x - 1][(int)posPeca.y] == null) {
					// Só pode andar pra frente se estiver vazio
					resultado.Add(new Vector2(posPeca.x - 1, posPeca.y));
					if(peca.name.StartsWith("Black") && peoesPretos[numeroPeao] == false && tabuleiro[(int)posPeca.x - 2][(int)posPeca.y] == null) {
						resultado.Add(new Vector2(posPeca.x - 2, posPeca.y));
						
					} 
				}
				if((int)posPeca.y + 1 < tabuleiro.Length && 
					tabuleiro[(int)posPeca.x - 1][(int)posPeca.y + 1] != null && 
					tabuleiro[(int)posPeca.x - 1][(int)posPeca.y + 1].name.StartsWith("White")) {
					// Comer para diagonal direita em relação as peças pretas
					resultado.Add(new Vector2(posPeca.x - 1, posPeca.y + 1));
				}
				if((int)posPeca.y - 1 >= 0 && 
					tabuleiro[(int)posPeca.x - 1][(int)posPeca.y - 1] != null && 
					tabuleiro[(int)posPeca.x - 1][(int)posPeca.y - 1].name.StartsWith("White")) {
					// Comer para diagonal esquerda em relação as peças pretas
					resultado.Add(new Vector2(posPeca.x - 1, posPeca.y - 1));
				} 
			}
		}

		// Movimento das torres
		else if(peca.name.StartsWith("White Rook")) {
			resultado = rook_moves("Black", posPeca, resultado);
		} else if(peca.name.StartsWith("Black Rook")) {
			resultado = rook_moves("White", posPeca, resultado);
		}

		// Movimento dos bispos
		else if(peca.name.StartsWith("White Bishop")) {
			resultado = bishop_moves("Black", posPeca, resultado);
		} else if(peca.name.StartsWith("Black Bishop")) {
			resultado = bishop_moves("White", posPeca, resultado);
		}

		// Movimento dos cavalos
		else if(peca.name.Contains("Knight")) {
			if (peca.name.StartsWith("White")){
				resultado = knight_moves("White","Black", posPeca, resultado);
			}
			else{
				resultado = knight_moves("Black", "White", posPeca, resultado);
			}
		}

		// Movimento do Rei
		else if(peca.name.Contains("King")) {
			if (peca.name.StartsWith("White")){
				resultado = king_moves("White","Black", posPeca, resultado);
			}
			else{
				resultado = king_moves("Black", "White", posPeca, resultado);
			}
		}

		// Movimento da Rainha
		else if(peca.name.Contains("Queen")) {
			if (peca.name.StartsWith("White")){
				resultado = queen_moves("White","Black", posPeca, resultado);
			}
			else{
				resultado = queen_moves("Black", "White", posPeca, resultado);
			}
		}
		//resultado = FiltrarMovimentos(tabuleiro, peca, resultado);
		return resultado;
	}
	private GameObject[][] Copiar(GameObject[][] tab){
		GameObject[][] tab_aux = new GameObject[8][];
		for(int i = 0; i < tab_aux.Length; i++) {
			tab_aux[i] = new GameObject[8];
		}

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

	public List<Vector2> FiltrarMovimentos(GameObject[][] tab, GameObject peca, List<Vector2> mov) {
		// Se o rei que você ta defendendo ta em check, só pode movimento pra tirar ele do check
		if(peca.name.StartsWith("White") && reiBrancoEmCheck) {
			for(int i = 0; i < tab.Length; i++) {
				for(int j = 0; j < tab.Length; j++) {
					if(tab[i][j] != null && tab[i][j].name.StartsWith("Black")) {
						
					}
				}
			}

		} else if(peca.name.StartsWith("Black") && reiPretoEmCheck) {

		}
		return mov;
	}
	private bool ReiEmCheck(GameObject[][] tab, Vector2 posRei, string corDoRei) {
		string corInimiga;
		if(corDoRei.Equals("White")) {
			corInimiga = "Black";
		} else {
			corInimiga = "White";
		}
		return false;
	}

	// recebe um tabuleiro, a posRei do rei que quer checar se ta em check e a ultima peça movida
	// retorna se o rei passado ficou em check ou não 
	public bool EntrouEmCheck(GameObject[][] t, Vector2 posReiInimigo, GameObject ultimaPeca) {
		
		int x = (int)posReiInimigo.x;
		int y = (int)posReiInimigo.y;
		List<Vector2> mov = MovimentosPossiveis(t, ultimaPeca);
		foreach(Vector2 v in mov) {
			if((int)v.x == x && (int)v.y == y) {
				Debug.Log("Entrou em check!");
				return true;
			}
		}
		return false;
	}

	public void AtualizaPosicoes(GameObject peca, Vector2 pos) {
		
		if(tabuleiro[(int)pos.x][(int)pos.y]) { 
			if(tabuleiro[(int)pos.x][(int)pos.y].name.Contains("King")){
				gm.endGame();
			}
			Destroy(tabuleiro[(int)pos.x][(int)pos.y]);
		}

			
		if(peca.tag == "whitePiece") {
			tabuleiro[(int)pos.x][(int)pos.y] = brancas.transform.Find(peca.name).gameObject;
			if(peca.name.Contains("Pawn")) {
				int numeroPeao = (int)peca.name.ToCharArray()[11] - 49;
				peoesBrancos[numeroPeao] = true;
			} else if(peca.name.Equals("White Rook 2")) {
				// torre mais proxima do rei branco
				torresBrancas[0] = true;
			} else if(peca.name.Equals("White Rook 1")) {
				// torre mais longe do rei branco
				torresBrancas[1] = true;
			} else if(peca.name.Contains("King")) {
				// rei branco andou
				if(roque && Mathf.Abs((int)pos.y - (int)posPeca.y) >= 2) {
					if((int)pos.y > (int)posPeca.y) {
						// significa que está fazendo o roque longo
						tabuleiro[(int)posPeca.x][(int)posPeca.y + 1] = brancas.transform.Find(tabuleiro[(int)posPeca.x][tabuleiro.Length - 1].name).gameObject;
						tabuleiro[(int)posPeca.x][tabuleiro.Length - 1] = null;
						// torre mais longe do rei branco
						torresBrancas[1] = true;	
					} else {
						// significa que está fazendo o roque curto
						tabuleiro[(int)posPeca.x][(int)posPeca.y - 1] = brancas.transform.Find(tabuleiro[(int)posPeca.x][0].name).gameObject;
						tabuleiro[(int)posPeca.x][0] = null;
						// torre mais perto do rei branco
						torresBrancas[0] = true;	
					}
				}
				reiBranco.x = pos.x;
				reiBranco.y = pos.y;
				reiAndou[0] = true;
			}
		} else {
			tabuleiro[(int)pos.x][(int)pos.y] = pretas.transform.Find(peca.name).gameObject;
			if(peca.name.Contains("Pawn")) {
				int numeroPeao = (int)peca.name.ToCharArray()[11] - 49;
				peoesPretos[numeroPeao] = true;
			} else if(peca.name.Equals("Black Rook 2")) {
				// torre mais proxima do rei preto
				torresPretas[0] = true;
			} else if(peca.name.Equals("Black Rook 1")) {
				// torre mais longe do rei pretos
				torresPretas[1] = true;
			} else if(peca.name.Contains("King")) {
				// rei preto andou
				if(roque && Mathf.Abs((int)pos.y - (int)posPeca.y) >= 2) {
					if((int)pos.y > (int)posPeca.y) {
						// significa que está fazendo o roque longo
						tabuleiro[(int)posPeca.x][(int)posPeca.y + 1] = pretas.transform.Find(tabuleiro[(int)posPeca.x][tabuleiro.Length - 1].name).gameObject;
						tabuleiro[(int)posPeca.x][tabuleiro.Length - 1] = null;
						// torre mais longe do rei preto
						torresPretas[1] = true;	
					} else {
						// significa que está fazendo o roque curto
						tabuleiro[(int)posPeca.x][(int)posPeca.y - 1] = pretas.transform.Find(tabuleiro[(int)posPeca.x][0].name).gameObject;
						tabuleiro[(int)posPeca.x][0] = null;
						// torre mais perto do rei preto
						torresPretas[0] = true;	
					}
				}
				reiPreto.x = pos.x;
				reiPreto.y = pos.y;
				reiAndou[1] = true;
			}
		}
		
		tabuleiro[(int)posPeca.x][(int)posPeca.y] = null;
		posPeca.x = pos.x;
		posPeca.y = pos.y;
		if(peca.name.StartsWith("White")) {
			if(!reiPretoEmCheck) {
				reiPretoEmCheck = EntrouEmCheck(tabuleiro, reiPreto, peca);
			}
		} else {
			if(!reiBrancoEmCheck) {
				reiBrancoEmCheck = EntrouEmCheck(tabuleiro, reiBranco, peca);
			}
		}
	}

	public void AtualizaPosicoesRelativas(GameObject[][] tab, GameObject peca, Vector2 pos){
		for(int i = 0; i < tab.Length; i++) {
			for(int j = 0; j < tab[i].Length; j++) {
				if(tab[i][j] && tab[i][j].name.Equals(peca.name)) {
					tab[(int)pos.x][(int)pos.y] = tab[i][j];
					tab[i][j] = null;
					return;
				}
			}
		}
	}
	public GameObject verifyPosition (Vector2 pos){
		return tabuleiro[(int)pos.x][(int)pos.y];
	}

	public List<GameObject> PecasDisponiveis(string cor) {
		List<GameObject> pecas = new List<GameObject>();
		for(int i = 0; i < tabuleiro.Length; i++) {
			for(int j = 0; j < tabuleiro[i].Length; j++) {
				if(tabuleiro[i][j].name.StartsWith(cor)) {
					pecas.Add(tabuleiro[i][j]);
				}
			}
		}
		return pecas.Count > 0 ? pecas : null;
	}

	public GameObject[][] GetTabuleiro() {
		return tabuleiro;
	}

	// promove a ultima peça selecionada para o objeto passado como parametro
	public void Promover(GameObject promo) {
		if(promo.name.StartsWith("White")) {
			tabuleiro[(int)posPeca.x][(int)posPeca.y] = brancas.transform.Find(promo.name).gameObject;
		} else if(promo.name.StartsWith("Black")) {
			tabuleiro[(int)posPeca.x][(int)posPeca.y] = pretas.transform.Find(promo.name).gameObject;
		}
	}
	
	public List<Vector2> JogadasPossiveis(GameObject[][] tab, string cor){
		List<Vector2> resultado = new List<Vector2>();
		for(int i = 0; i < tab.Length; i++) {
			tab[i] = new GameObject[8];
			for(int j = 0; j < tab[i].Length; j++) {
				if(tab[i][j] && tab[i][j].name.StartsWith(cor)){
					resultado.AddRange(MovimentosPossiveis(tab,tab[i][j]));
				}
			}
		}
		return resultado;
	}
}