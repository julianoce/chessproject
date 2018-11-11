﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRules : MonoBehaviour {

	public GameObject brancas;
	public GameObject pretas;
	private GameObject[][] tabuleiro;
	private Vector2 reiBranco;
	private Vector2 reiPreto;
	Vector2 posPeca = new Vector2();
	
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
	public List<Vector2> MovimentosPossiveis(GameObject[][] tabuleiro, GameObject peca) {
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
			if((int)posPeca.x + 1 < tabuleiro.Length) {
				if(tabuleiro[(int)posPeca.x + 1][(int)posPeca.y] == null)
					// Só pode andar pra frente se estiver vazio
					resultado.Add(new Vector2(posPeca.x + 1, posPeca.y));
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
			if((int)posPeca.x - 1 >= 0) {
				if(tabuleiro[(int)posPeca.x - 1][(int)posPeca.y] == null)
					// Só pode andar pra frente se estiver vazio
					resultado.Add(new Vector2(posPeca.x - 1, posPeca.y));
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
					// Se o lugar ta vazio pode mover
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

		// Movimento dos bispos
		else if(peca.name.StartsWith("White Bishop")) {
			parouEsq = false; parouDir = false;
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
						} else if(tabuleiro[i][(int)posPeca.y + j].name.StartsWith("Black")) {
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
						} else if(tabuleiro[i][(int)posPeca.y - j].name.StartsWith("Black")) {
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
						} else if(tabuleiro[i][(int)posPeca.y + j].name.StartsWith("Black")) {
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
						} else if(tabuleiro[i][(int)posPeca.y - j].name.StartsWith("Black")) {
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
		} else if(peca.name.StartsWith("Black Bishop")) {
			parouEsq = false; parouDir = false;
			// Primeiro 'for' é para os bispos pretos subirem no tabuleiro
			// "Subir" segundo a logica da matriz e não visualmente
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
						} else if(tabuleiro[i][(int)posPeca.y + j].name.StartsWith("White")) {
							// Se o lugar possui uma peça branca pode mover, mas se sabe as posições seguintes são invalidas
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
						} else if(tabuleiro[i][(int)posPeca.y - j].name.StartsWith("White")) {
							// Se o lugar possui uma peça branca pode mover, mas se sabe as posições seguintes são invalidas
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
			//Segundo for é para os bispos pretos descerem no tabuleiro
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
						} else if(tabuleiro[i][(int)posPeca.y + j].name.StartsWith("White")) {
							// Se o lugar possui uma peça branca pode mover, mas se sabe as posições seguintes são invalidas
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
						} else if(tabuleiro[i][(int)posPeca.y - j].name.StartsWith("White")) {
							// Se o lugar possui uma peça branca pode mover, mas se sabe as posições seguintes são invalidas
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
		}

		// Movimento dos cavalos
		else if(peca.name.Contains("Knight")) {
			int xPeca = (int)posPeca.x, yPeca = (int)posPeca.y;
			Debug.Log(peca.name + ": (" + xPeca + "," + yPeca + ")");
			for(int i = 1; i < 3; i++) {
				for(int j = 1; j < 3; j++) {
					if(i == j) continue;
					if(xPeca + i < tabuleiro.Length) {
						// Pode subir
						if(yPeca + j < tabuleiro.Length) {
							// Pode ir para a esquerda da matriz
							if(peca.name.StartsWith("White") && 
								((tabuleiro[xPeca + i][yPeca + j] == null) || 
								tabuleiro[xPeca + i][yPeca + j].name.StartsWith("Black"))) {
								// Se a peça clicada é branca e a posição desejada contem uma peça preta ou nenhuma peça
								resultado.Add(new Vector2(xPeca + i, yPeca + j));
							}
							else if(peca.name.StartsWith("Black") && 
								((tabuleiro[xPeca + i][yPeca + j] == null) || 
								tabuleiro[xPeca + i][yPeca + j].name.StartsWith("White"))) {
								// Se a peça clicada é preta e a posição desejada contem uma peça branca ou nenhuma peça
								resultado.Add(new Vector2(xPeca + i, yPeca + j));
							}
						}
						if(yPeca - j >= 0) {
							// Pode ir para a direita da matriz
							if(peca.name.StartsWith("White") && 
								((tabuleiro[xPeca + i][yPeca - j] == null) || 
								tabuleiro[xPeca + i][yPeca - j].name.StartsWith("Black"))) {
								// Se a peça clicada é branca e a posição desejada contem uma peça preta ou nenhuma peça
								resultado.Add(new Vector2(xPeca + i, yPeca - j));
							}
							else if(peca.name.StartsWith("Black") && 
								((tabuleiro[xPeca + i][yPeca - j] == null) || 
								tabuleiro[xPeca + i][yPeca - j].name.StartsWith("White"))) {
								// Se a peça clicada é preta e a posição desejada contem uma peça branca ou nenhuma peça
								resultado.Add(new Vector2(xPeca + i, yPeca - j));
							}
						}
					}
					if(xPeca - i >= 0) {
						// Pode descer!! LEMBRANDO: SEMPRE EM RELAÇÃO A MATRIZ
						if(yPeca + j < tabuleiro.Length) {
							// Pode ir para a esquerda da matriz
							if(peca.name.StartsWith("White") && 
								((tabuleiro[xPeca - i][yPeca + j] == null) || 
								tabuleiro[xPeca - i][yPeca + j].name.StartsWith("Black"))) {
								// Se a peça clicada é branca e a posição desejada contem uma peça preta ou nenhuma peça
								resultado.Add(new Vector2(xPeca - i, yPeca + j));
							}
							else if(peca.name.StartsWith("Black") && 
								((tabuleiro[xPeca - i][yPeca + j] == null) || 
								tabuleiro[xPeca - i][yPeca + j].name.StartsWith("White"))) {
								// Se a peça clicada é preta e a posição desejada contem uma peça branca ou nenhuma peça
								resultado.Add(new Vector2(xPeca - i, yPeca + j));
							}
						}
						if(yPeca - j >= 0) {
							// Pode ir para a direita da matriz
							if(peca.tag == "whitePiece" && 
								((tabuleiro[xPeca - i][yPeca - j] == null) || 
								tabuleiro[xPeca - i][yPeca - j].name.StartsWith("Black"))) {
								// Se a peça clicada é branca e a posição desejada contem uma peça preta ou nenhuma peça
								resultado.Add(new Vector2(xPeca - i, yPeca - j));
							}
							else if(peca.name.StartsWith("Black") && 
								((tabuleiro[xPeca - i][yPeca - j] == null) || 
								tabuleiro[xPeca - i][yPeca - j].name.StartsWith("White"))) {
								// Se a peça clicada é preta e a posição desejada contem uma peça branca ou nenhuma peça
								resultado.Add(new Vector2(xPeca - i, yPeca - j));
							}
						}
					}
				}
			}
		}

		// Movimento do Rei
		else if(peca.name.Contains("King")) {
			int xPeca = (int)posPeca.x, yPeca = (int)posPeca.y;
			if(xPeca + 1 < tabuleiro.Length) {
				// Pode subir
				if(tabuleiro[xPeca + 1][yPeca] == null || 
				(peca.name.StartsWith("White") && tabuleiro[xPeca + 1][yPeca].name.StartsWith("Black")) ||
				(peca.name.StartsWith("Black") && tabuleiro[xPeca + 1][yPeca].name.StartsWith("White"))) {
					// Para cima na matriz está vazio ou contem peça inimiga
					resultado.Add(new Vector2(xPeca + 1, yPeca));
				}
				if(yPeca + 1 < tabuleiro.Length && 
				(tabuleiro[xPeca + 1][yPeca + 1] == null || 
				(peca.name.StartsWith("White") && tabuleiro[xPeca + 1][yPeca + 1].name.StartsWith("Black")) ||
				(peca.name.StartsWith("Black") && tabuleiro[xPeca + 1][yPeca + 1].name.StartsWith("White")))) {
					// Para cima e para esquerda em relação a matriz está vazio ou contem peça inimiga
					resultado.Add(new Vector2(xPeca + 1, yPeca + 1));
				}
				if(yPeca - 1 >= 0 && 
				(tabuleiro[xPeca + 1][yPeca - 1] == null || 
				(peca.name.StartsWith("White") && tabuleiro[xPeca + 1][yPeca - 1].name.StartsWith("Black")) ||
				(peca.name.StartsWith("Black") && tabuleiro[xPeca + 1][yPeca - 1].name.StartsWith("White")))) {
					// Para cima e para direita em relação a matriz está vazio ou contem peça inimiga
					resultado.Add(new Vector2(xPeca + 1, yPeca - 1));
				}
			}
			if(xPeca - 1 >= 0) {
				// Pode descer
				if(tabuleiro[xPeca - 1][yPeca] == null || 
				(peca.name.StartsWith("White") && tabuleiro[xPeca - 1][yPeca].name.StartsWith("Black")) ||
				(peca.name.StartsWith("Black") && tabuleiro[xPeca - 1][yPeca].name.StartsWith("White"))) {
					// Para baixo na matriz está vazio ou contem peça inimiga
					resultado.Add(new Vector2(xPeca - 1, yPeca));
				}
				if(yPeca + 1 < tabuleiro.Length && 
				(tabuleiro[xPeca - 1][yPeca + 1] == null || 
				(peca.name.StartsWith("White") && tabuleiro[xPeca - 1][yPeca + 1].name.StartsWith("Black")) ||
				(peca.name.StartsWith("Black") && tabuleiro[xPeca - 1][yPeca + 1].name.StartsWith("White")))) {
					// Para baixo e para esquerda em relação a matriz está vazio ou contem peça inimiga
					resultado.Add(new Vector2(xPeca - 1, yPeca + 1));
				}
				if(yPeca - 1 >= 0 && 
				(tabuleiro[xPeca - 1][yPeca - 1] == null || 
				(peca.name.StartsWith("White") && tabuleiro[xPeca - 1][yPeca - 1].name.StartsWith("Black")) ||
				(peca.name.StartsWith("Black") && tabuleiro[xPeca - 1][yPeca - 1].name.StartsWith("White")))) {
					// Para baixo e para direita em relação a matriz está vazio ou contem peça inimiga
					resultado.Add(new Vector2(xPeca - 1, yPeca - 1));
				}
			}
			if(yPeca + 1 < tabuleiro.Length &&
				(tabuleiro[xPeca][yPeca + 1] == null|| 
				(peca.name.StartsWith("White") && tabuleiro[xPeca][yPeca + 1].name.StartsWith("Black")) ||
				(peca.name.StartsWith("Black") && tabuleiro[xPeca][yPeca + 1].name.StartsWith("White")))) {
					// Para esquerda em relação a matriz está vazio ou contem peça inimiga
					resultado.Add(new Vector2(xPeca, yPeca + 1));
			}
			if(yPeca - 1 >= 0 &&
				(tabuleiro[xPeca][yPeca - 1] == null || 
				(peca.name.StartsWith("White") && tabuleiro[xPeca][yPeca - 1].name.StartsWith("Black")) ||
				(peca.name.StartsWith("Black") && tabuleiro[xPeca][yPeca - 1].name.StartsWith("White")))) {
					// Para direita em relação a matriz está vazio ou contem peça inimiga
					resultado.Add(new Vector2(xPeca, yPeca - 1));
			}
		}

		// Movimento da Rainha
		else if(peca.name.Contains("Queen")) {
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
					} else if(peca.name.StartsWith("White") && 
						tabuleiro[xPeca + i][yPeca].name.StartsWith("Black")) {
						// Peça selecionada é branca, lugar checado tem peça preta
						resultado.Add(new Vector2(xPeca + i, yPeca));
						parouCima = true;
					} else if(peca.name.StartsWith("Black") && 
						tabuleiro[xPeca + i][yPeca].name.StartsWith("White")) {
						// Peça selecionada é preta, lugar checado tem peça branca
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
					} else if(peca.name.StartsWith("White") && 
						tabuleiro[xPeca - i][yPeca].name.StartsWith("Black")) {
						// Peça selecionada é branca, lugar checado tem peça preta
						resultado.Add(new Vector2(xPeca - i, yPeca));
						parouBaixo = true;
					} else if(peca.name.StartsWith("Black") && 
						tabuleiro[xPeca - i][yPeca].name.StartsWith("White")) {
						// Peça selecionada é preta, lugar checado tem peça branca
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
					} else if(peca.name.StartsWith("White") && 
						tabuleiro[xPeca][yPeca + i].name.StartsWith("Black")) {
						// Peça selecionada é branca, lugar checado tem peça preta
						resultado.Add(new Vector2(xPeca, yPeca + i));
						parouEsq = true;
					} else if(peca.name.StartsWith("Black") && 
						tabuleiro[xPeca][yPeca + i].name.StartsWith("White")) {
						// Peça selecionada é preta, lugar checado tem peça branca
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
					} else if(peca.name.StartsWith("White") && 
						tabuleiro[xPeca][yPeca - i].name.StartsWith("Black")) {
						// Peça selecionada é branca, lugar checado tem peça preta
						resultado.Add(new Vector2(xPeca, yPeca - i));
						parouDir = true;
					} else if(peca.name.StartsWith("Black") && 
						tabuleiro[xPeca][yPeca - i].name.StartsWith("White")) {
						// Peça selecionada é preta, lugar checado tem peça branca
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
					} else if(peca.name.StartsWith("White") && 
						tabuleiro[xPeca + i][yPeca + i].name.StartsWith("Black")) {
						// Peça selecionada é branca, lugar checado tem peça preta
						resultado.Add(new Vector2(xPeca + i, yPeca + i));
						parouDSE = true;
					} else if(peca.name.StartsWith("Black") && 
						tabuleiro[xPeca + i][yPeca + i].name.StartsWith("White")) {
						// Peça selecionada é preta, lugar checado tem peça branca
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
					} else if(peca.name.StartsWith("White") && 
						tabuleiro[xPeca + i][yPeca - i].name.StartsWith("Black")) {
						// Peça selecionada é branca, lugar checado tem peça preta
						resultado.Add(new Vector2(xPeca + i, yPeca - i));
						parouDSD = true;
					} else if(peca.name.StartsWith("Black") && 
						tabuleiro[xPeca + i][yPeca - i].name.StartsWith("White")) {
						// Peça selecionada é preta, lugar checado tem peça branca
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
					} else if(peca.name.StartsWith("White") && 
						tabuleiro[xPeca - i][yPeca + i].name.StartsWith("Black")) {
						// Peça selecionada é branca, lugar checado tem peça preta
						resultado.Add(new Vector2(xPeca - i, yPeca + i));
						parouDIE = true;
					} else if(peca.name.StartsWith("Black") && 
						tabuleiro[xPeca - i][yPeca + i].name.StartsWith("White")) {
						// Peça selecionada é preta, lugar checado tem peça branca
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
					} else if(peca.name.StartsWith("White") && 
						tabuleiro[xPeca - i][yPeca - i].name.StartsWith("Black")) {
						// Peça selecionada é branca, lugar checado tem peça preta
						resultado.Add(new Vector2(xPeca - i, yPeca - i));
						parouDID = true;
					} else if(peca.name.StartsWith("Black") && 
						tabuleiro[xPeca - i][yPeca - i].name.StartsWith("White")) {
						// Peça selecionada é preta, lugar checado tem peça branca
						resultado.Add(new Vector2(xPeca - i, yPeca - i));
						parouDID = true;
					} else {
						parouDID = true;
					}
				}
			}
		}
		

		return resultado;
	}

	private bool VaiFicarEmCheck(Vector2 posRei) {
		return false;
	}

	public void AtualizaPosicoes(GameObject peca, Vector2 pos) {
		if(tabuleiro[(int)pos.x][(int)pos.y]) { 
			Destroy(tabuleiro[(int)pos.x][(int)pos.y]);
		}
		if(peca.tag == "whitePiece") {
			tabuleiro[(int)pos.x][(int)pos.y] = brancas.transform.Find(peca.name).gameObject;
		} else {
			tabuleiro[(int)pos.x][(int)pos.y] = pretas.transform.Find(peca.name).gameObject;
		}
		
		tabuleiro[(int)posPeca.x][(int)posPeca.y] = null;
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
	
	public List<Vector2> JogadasPossiveis(GameObject[][] tab, string cor){
		List<Vector2> resultado = new List<Vector2>();
		return resultado;
	}
}
