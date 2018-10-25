using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRules : MonoBehaviour {

	public GameObject brancas;
	public GameObject pretas;
	private GameObject[][] tabuleiro;
	private Vector2 reiBranco;
	private Vector2 reiPreto;
	
	// Use this for initialization
	void Start () {
		tabuleiro = new GameObject[8][];
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

	public List<Vector2> MovimentosPossiveis(GameObject peca) {
		Vector2 posPeca = new Vector2();
		List<Vector2> resultado = new List<Vector2>();
		for(int i = 0; i < tabuleiro.Length; i++) {
			for(int j = 0; j < tabuleiro[i].Length; j++) {
				//acha a peça
				if(peca.Equals(tabuleiro[i][j])) {
					posPeca = new Vector2(i, j);
				}	
			}
		} 

		// Movimento do peões
		if(peca.name.StartsWith("White Pawn")) {
			if(posPeca.x + 1 <= 7) {
				resultado.Add(new Vector2(posPeca.x + 1, posPeca.y));
			} 
		} else if(peca.name.StartsWith("Black Pawn")) {
			if(posPeca.x - 1 >= 0) {
				resultado.Add(new Vector2(posPeca.x - 1, posPeca.y));
			} 
		}

		// Movimento das torres
		else if(peca.name.StartsWith("White Rook")) {
			// Os 2 'for' seguintes são para subir e descer no tabuleiro
			for(int i = (int)posPeca.x + 1; i < tabuleiro.Length; i++) {
				if(tabuleiro[i][(int)posPeca.y] == null ) {
					//Se o lugar ta vazio pode mover
					resultado.Add(new Vector2(i, (int)posPeca.y));
				} else if (tabuleiro[i][(int)posPeca.y].name.StartsWith("Black")) {
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
				} else if (tabuleiro[i][(int)posPeca.y].name.StartsWith("Black")) {
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
				} else if(tabuleiro[(int)posPeca.x][i].name.StartsWith("Black")) {
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
				} else if(tabuleiro[(int)posPeca.x][i].name.StartsWith("Black")) {
					// Se o lugar possui uma peça preta pode mover, mas se sabe que as posições seguintes são invalidas
					resultado.Add(new Vector2((int)posPeca.x, i));
					break;
				} else {
					// Se o lugar não ta vazio e nem é uma peça preta significa que é uma peça branca, logo não pode mover
					break;
				}
			}

		} else if(peca.name.StartsWith("Black Rook")) {
			// Os 2 'for' seguintes são para subir e descer no tabuleiro
			for(int i = (int)posPeca.x + 1; i < tabuleiro.Length; i++) {
				if(tabuleiro[i][(int)posPeca.y] == null ) {
					//Se o lugar ta vazio pode mover
					resultado.Add(new Vector2(i, (int)posPeca.y));
				} else if (tabuleiro[i][(int)posPeca.y].name.StartsWith("White")) {
					// Se o lugar possui uma peça branca pode mover, mas se sabe que a posições seguintes são invalidas
					resultado.Add(new Vector2(i, (int)posPeca.y));
					break;
				} else {
					// Se o lugar não ta vazio e nem é uma peça branca significa que é uma peça preta, logo não pode mover
					break;
				}
			}
			for(int i = (int)posPeca.x - 1; i >= 0; i--) {
				if(tabuleiro[i][(int)posPeca.y] == (null)) {
					// Se o lugar ta vazio pode mover
					resultado.Add(new Vector2(i, (int)posPeca.y));
				} else if (tabuleiro[i][(int)posPeca.y].name.StartsWith("White")) {
					// Se o lugar possui uma peça branca pode mover, mas se sabe que as posições seguintes são invalidas
					resultado.Add(new Vector2(i, (int)posPeca.y));
					break;
				} else {
					// Se o lugar não ta vazio e nem é uma peça branca significa que é uma peça preta, logo não pode mover
					break;
				}
			}

			// Os proximos dois 'for' são para os movimentos laterais
			for(int i = (int)posPeca.y + 1; i < tabuleiro[(int)posPeca.x].Length; i++) {
				if(tabuleiro[(int)posPeca.x][i] == (null)) {
					//Se o lugar ta vazio pode mover
					resultado.Add(new Vector2((int)posPeca.x, i));
				} else if(tabuleiro[(int)posPeca.x][i].name.StartsWith("White")) {
					// Se o lugar possui uma peça branca pode mover, mas se sabe que as posições seguintes são invalidas
					resultado.Add(new Vector2((int)posPeca.x, i));
					break;
				} else {
					// Se o lugar não ta vazio e nem é uma peça branca significa que é uma peça preta, logo não pode mover
					break;
				}
			}
			for(int i = (int)posPeca.y - 1; i >= 0; i--) {
				if(tabuleiro[(int)posPeca.x][i] == (null)) {
					//Se o lugar ta vazio pode mover
					resultado.Add(new Vector2((int)posPeca.x, i));
				} else if(tabuleiro[(int)posPeca.x][i].name.StartsWith("White")) {
					// Se o lugar possui uma peça branca pode mover, mas se sabe que as posições seguintes são invalidas
					resultado.Add(new Vector2((int)posPeca.x, i));
					break;
				} else {
					// Se o lugar não ta vazio e nem é uma peça branca significa que é uma peça preta, logo não pode mover
					break;
				}
			}
		}

		return resultado;
	}

	private bool VaiFicarEmCheck(Vector2 posRei) {
		return false;
	}

	public void AtualizaPosicoes(GameObject peca, Vector2 pos) {
		// QUEBRADO POR CAUSA DAS REFERENCIAS AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
		// Fazer funcionar para qualquer peça
		
		tabuleiro[2][0] = brancas.transform.Find(tabuleiro[1][0].name).gameObject;
		tabuleiro[1][0] = null;
	}
}
