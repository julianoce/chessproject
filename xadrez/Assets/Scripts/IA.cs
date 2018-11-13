using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour {
	private BoardRules br;
	private GameManager gm;
	private PlayerScript ps;
	private BoardMapping bm;
	private GameObject tabuleiro;
	public GameObject pretas;
	public GameObject brancas;
	private string cor;
	private string cor_adv;
	private List<Vector2> jogadas;
	private List<GameObject> quem;
	const int MAX_ITE = 4;

	void Start () {
		br = GameObject.FindObjectOfType(typeof(BoardRules)) as BoardRules;
		ps = GameObject.FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
		bm = GameObject.FindObjectOfType(typeof(BoardMapping)) as BoardMapping;
		cor = "Black";
		cor_adv = "White";
		
		//receber como parametro cor escolhida pelo jogador
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void buscar(){
		//receber como parametro cor escolhida pelo jogador e a dificuldade do jogo
		GameObject[][] tab = br.GetTabuleiro();
		GameObject[][] tab_aux = copiar(tab);
		this.jogadas = new List<Vector2>();
		this.quem = new List<GameObject>();
	 	Max(tab_aux, int.MinValue, int.MaxValue, 0);
		System.Random r = new System.Random();
		int x = r.Next(0,jogadas.Count);
		GameObject piece = quem[x];
		Debug.Log("Respostas");
		Debug.Log(piece);
		Debug.Log(jogadas[x]);

		bm.makeTiles(piece);
		ps.movePiece(quem[x], findTile(jogadas[x]));
		ps.podeJogar = false;
		
		Debug.Log("Terminou");
	}

	GameObject findTile(Vector2 v){
		return GameObject.Find(v.x+","+v.y);
	}

	int Max(GameObject[][] tab, int alpha, int beta, int poda){
		if(poda == MAX_ITE)
			return UtilityDificil(tab);

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

//Essa Utility eh a modo facil.
	public int Utility(GameObject[][]tab){
		int numPecasInimigo = br.NumPecasTime(tab, this.cor_adv);
		int numPecas = br.NumPecasTime(tab, this.cor);
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
						numPecasInimigo += 1000000;
					}
					if(tab[i][j].name.StartsWith(this.cor) && tab[i][j].name.Contains("King")){
						numPecas += 1000000;
					}
				}
			}
		}
		return numPecas - numPecasInimigo;
	}



	public int UtilityDificil(GameObject[][]tab){
	int value = 0;
	
	int[,] black_pawn_matrix = new int[8,8]{
		{ 0,  0,  0,  0,  0,  0,  0,  0},
     {4,  8,  8,-17,-17,  8,  8,  4},
     {4, -4, -8,  4,  4, -8, -4,  4},
     {0,  4,  8, 17, 17,  8,  4,  0},
     {4,  8, 12, 21, 21, 12,  8,  4},
    {25, 25, 29, 29, 29, 29, 25, 25},
    	{75, 75, 75, 75, 75, 75, 75, 75},
     {0,  0,  0,  0,  0,  0,  0,  0}
	};
	
	int [,] white_pawn_matrix = new int[8,8] {
    { 0,  0,  0,  0,  0,  0,  0,  0},
    {75, 75, 75, 75, 75, 75, 75, 75},
    {25, 25, 29, 29, 29, 29, 25, 25},
     {4,  8, 12, 21, 21, 12,  8,  4},
     {0,  4,  8, 17, 17,  8,  4,  0},
     {4, -4, -8,  4,  4, -8, -4,  4},
     {4,  8,  8,-17,-17,  8,  8,  4},
     {0,  0,  0,  0,  0,  0,  0,  0}};  
	 	 
	int [,] white_knight_matrix = new int[8,8] {
    {-50,-40,-30,-30,-30,-30,-40,-50},
    {-40,-20,  0,  0,  0,  0,-20,-40},
    {-30,  0, 10, 15, 15, 10,  0,-30},
     {-30,  5, 15, 20, 20, 15,  5,-30},
     {-30,  0, 15, 20, 20, 15,  0,-30},
     {-30,  5, 10, 15, 15, 10,  5,-30},
     {-40,-20,  0,  5,  5,  0,-20,-40},
     {-50,-40,-30,-30,-30,-30,-40,-50}}; 

	 int [,] black_knight_matrix = new int[8,8] {
    {-50,-40,-30,-30,-30,-30,-40,-50},
     {-40,-20,  0,  5,  5,  0,-20,-40},
     {-30,  5, 10, 15, 15, 10,  5,-30},
     {-30,  0, 15, 20, 20, 15,  0,-30},
     {-30,  5, 15, 20, 20, 15,  5,-30},
    {-30,  0, 10, 15, 15, 10,  0,-30},
	{-40,-20,  0,  0,  0,  0,-20,-40},
     {-50,-40,-30,-30,-30,-30,-40,-50}}; 
	 
int [,] black_bishop_matrix = new int[8,8] {
    {-20,-10,-10,-10,-10,-10,-10,-20},
	{-10,  5,  0,  0,  0,  0,  5,-10},
	{-10, 10, 10, 10, 10, 10, 10,-10},
	{-10,  0, 10, 10, 10, 10,  0,-10},
	{-10,  5,  5, 10, 10,  5,  5,-10},
	{-10,  0,  5, 10, 10,  5,  0,-10},
	{-10,  0,  0,  0,  0,  0,  0,-10},
	{-20,-10,-10,-10,-10,-10,-10,-20}}; 

	int [,] white_bishop_matrix = new int[8,8] {
    {-20,-10,-10,-10,-10,-10,-10,-20},
	{-10,  0,  0,  0,  0,  0,  0,-10},
	{-10,  0,  5, 10, 10,  5,  0,-10},
	{-10,  5,  5, 10, 10,  5,  5,-10},
	{-10,  0, 10, 10, 10, 10,  0,-10},
	{-10, 10, 10, 10, 10, 10, 10,-10},
	{-10,  5,  0,  0,  0,  0,  5,-10},
	{-20,-10,-10,-10,-10,-10,-10,-20}};  

	int [,] black_rook_matrix = new int[8,8] {
	{0,  0,  0,  5,  5,  0,  0,  0},
	{-5,  0,  0,  0,  0,  0,  0, -5},
	{-5,  0,  0,  0,  0,  0,  0, -5},
	{-5,  0,  0,  0,  0,  0,  0, -5},
	{-5,  0,  0,  0,  0,  0,  0, -5},
	{-5,  0,  0,  0,  0,  0,  0, -5},
	{5, 10, 10, 10, 10, 10, 10,  5},
	{0,  0,  0,  0,  0,  0,  0,  0},
	};  
	

	int [,] white_rook_matrix = new int[8,8] {
	{0,  0,  0,  0,  0,  0,  0,  0},
	{5, 10, 10, 10, 10, 10, 10,  5},
	{-5,  0,  0,  0,  0,  0,  0, -5},
	{-5,  0,  0,  0,  0,  0,  0, -5},
	{-5,  0,  0,  0,  0,  0,  0, -5},
	{-5,  0,  0,  0,  0,  0,  0, -5},
	{-5,  0,  0,  0,  0,  0,  0, -5},
	{0,  0,  0,  5,  5,  0,  0,  0}};  
	
	int [,] black_queen_matrix = new int[8,8] {
		{-20,-10,-10, -5, -5,-10,-10,-20},
		{-10,  0,  5,  0,  0,  0,  0,-10},
		{-10,  5,  5,  5,  5,  5,  0,-10},
		{0,  0,  5,  5,  5,  5,  0, -5},
		{-5,  0,  5,  5,  5,  5,  0, -5},
		{-10,  0,  5,  5,  5,  5,  0,-10},
		{-10,  0,  0,  0,  0,  0,  0,-10},
		{-20,-10,-10, -5, -5,-10,-10,-20}
		};
	
	int [,] white_queen_matrix = new int[8,8] {
		{-20,-10,-10, -5, -5,-10,-10,-20},
		{-10,  0,  0,  0,  0,  0,  0,-10},
		{-10,  0,  5,  5,  5,  5,  0,-10},
		{-5,  0,  5,  5,  5,  5,  0, -5},
		{0,  0,  5,  5,  5,  5,  0, -5},
		{-10,  5,  5,  5,  5,  5,  0,-10},
		{-10,  0,  5,  0,  0,  0,  0,-10},
		{-20,-10,-10, -5, -5,-10,-10,-20}};

	int [,] black_king_matrix_middle = new int[8,8] {
			{20, 30, 10,  0,  0, 10, 30, 20},
			{20, 20,  0,  0,  0,  0, 20, 20},
			{-10,-20,-20,-20,-20,-20,-20,-10},
			{-20,-30,-30,-40,-40,-30,-30,-20},
			{-30,-40,-40,-50,-50,-40,-40,-30},
			{-30,-40,-40,-50,-50,-40,-40,-30},
			{-30,-40,-40,-50,-50,-40,-40,-30},
			{-30,-40,-40,-50,-50,-40,-40,-30},
			};

	int [,] white_king_matrix_middle = new int[8,8] {
			{-30,-40,-40,-50,-50,-40,-40,-30},
			{-30,-40,-40,-50,-50,-40,-40,-30},
			{-30,-40,-40,-50,-50,-40,-40,-30},
			{-30,-40,-40,-50,-50,-40,-40,-30},
			{-20,-30,-30,-40,-40,-30,-30,-20},
			{-10,-20,-20,-20,-20,-20,-20,-10},
			{20, 20,  0,  0,  0,  0, 20, 20},
			{20, 30, 10,  0,  0, 10, 30, 20}};
	int value_white = 0;
	int value_black = 0;

	for(int i=0; i<tab.Length;i++){
			for(int j=0; j<tab.Length;j++){
				if(tab[i][j]){
					if(tab[i][j].name.StartsWith(this.cor_adv) && tab[i][j].name.Contains("Queen")){
						value_white += white_queen_matrix[i,j];
					}
					if(tab[i][j].name.StartsWith(this.cor) && tab[i][j].name.Contains("Queen")){
						value_black+= black_queen_matrix[i,j];
					}
					if(tab[i][j].name.StartsWith(this.cor_adv) && tab[i][j].name.Contains("Rook")){
						value_white += white_rook_matrix[i,j];
					}
					if(tab[i][j].name.StartsWith(this.cor) && tab[i][j].name.Contains("Rook")){
						value_black += black_rook_matrix[i,j];
						
					}

					if(tab[i][j].name.StartsWith(this.cor_adv) && tab[i][j].name.Contains("Knight") || tab[i][j].name.Contains("Bishop")){
						value_white += white_knight_matrix[i,j];
					}
					if(tab[i][j].name.StartsWith(this.cor) && (tab[i][j].name.Contains("Knight") || tab[i][j].name.Contains("Bishop"))){
						value_black += black_knight_matrix[i,j];						
					}

					if(tab[i][j].name.StartsWith(this.cor_adv) && tab[i][j].name.Contains("Pawn")){
						value_white += white_pawn_matrix[i,j];
					}
					if(tab[i][j].name.StartsWith(this.cor) && tab[i][j].name.Contains("Pawn")){
						value_black += black_pawn_matrix[i,j];
					}

					if(tab[i][j].name.StartsWith(this.cor_adv) && tab[i][j].name.Contains("King")){
						value_white += white_king_matrix_middle[i,j];
					}
					if(tab[i][j].name.StartsWith(this.cor) && tab[i][j].name.Contains("King")){
						value_black += black_king_matrix_middle[i,j];
					}
				}
			}
	}
			return value_white - value_black;

}
}
