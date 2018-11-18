using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public Text level;	//, rulesMain, quitMain,
				//wOption, bOption, 
				//backRules, 
				//continuePause, rulesPause, quitGamePause;

	public GameObject mainMenu, pauseMenu, rulesMenu, newGameMenu;

	Animator animMain, animPause, animRules, animNewGame;
	List<GameObject> menus;
	public enum MenuType{none, main, newGame, pause, rules};
	public MenuType menuType;
	int menu;
	

	public GameObject player1;
    public GameObject player2;

	public RawImage player1w, player2w, player1b, player2b, cpu1, cpu2 ;
	public Text arrow1, arrow2, arrow3, arrow4;
	public Button bt1, bt2, bt3, bt4; 

	public RawImage img_alto_falante;
    public RawImage img_sem_alto_falante;
    bool sound = true;

	public static string whitePieces, blackPieces, mode, p1, p2; 

	Scene sc;
	// Use this for initialization
	void Start () {
		sc = SceneManager.GetActiveScene();
		menu =(int) MenuType.none;
		animMain = mainMenu.GetComponent<Animator>();
		animPause = pauseMenu.GetComponent<Animator>();
		animRules = rulesMenu.GetComponent<Animator>();
		animNewGame = newGameMenu.GetComponent<Animator>();
		if(sc.name == "Menu"){
			animMain.Play("slideIn");
			mode = "facil";
		}
		else if(sc.name == "Demo")
		{
			pauseMenu.SetActive(false);
			rulesMenu.SetActive(false);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		 if(sc.name == "Demo"){
			 if(Input.GetKeyDown(KeyCode.Escape)){
				pauseMenu.SetActive(true);
				Time.timeScale = 0;
			 }
		 }
	}

	public void continueGame()
	{
		pauseMenu.SetActive(false);
		Time.timeScale = 1;
	}

	public void toRules()
	{
		pauseMenu.SetActive(false);
		rulesMenu.SetActive(true);
	}

	public void backToPause()
	{
		pauseMenu.SetActive(true);
		rulesMenu.SetActive(false);
	}

	public void quitGame()
	{
		SceneManager.LoadScene("Menu");
	}

	
	

	public void onClick(int menuOut)//}, int menuIn)
	{
	
		switch(menuOut)
		{
			case (int)MenuType.main:
			animMain.Play("slideOut");
			break;

			case (int)MenuType.rules:
			animRules.Play("slideOut");
			break;

			case (int)MenuType.newGame:
			animNewGame.Play("slideOut");
			break;

			case (int)MenuType.pause:
			animPause.Play("slideOut");
			break;
		}
	}

	public void toMenu(int menuIn){

		this.menu = menuIn;
	}

	public void Quit(){
		Application.Quit();
	}

	public void soundOnOff()
	{
		sound = !sound;
		img_alto_falante.enabled = sound;
		img_sem_alto_falante.enabled = !sound;
	}

	public void choosePlayer(int arrow)
	{
		switch (arrow)
		{
			case 1:
			if(arrow1.text == "<"){
				cpu1.enabled = false;
				player1w.enabled = true;
				arrow1.text = ">";
				arrow2.text = "";
				bt2.interactable = false;
				arrow3.text = "";
				bt3.interactable = false;
			}
			else
			{
				cpu1.enabled = true;
				player1w.enabled = false;
				arrow1.text = "<";
				arrow2.text = ">";
				bt2.interactable = true;
				if( cpu2.enabled)
				{
				arrow3.text = "<";
				bt3.interactable = true;
				}
			}
			break;
			case 2:
			if(arrow2.text == ">"){
				cpu1.enabled = false;
				player1b.enabled = true;
				arrow1.text = "";
				bt1.interactable = false;
				arrow2.text = "<";
				arrow4.text = "";
				bt4.interactable = false;
			}
			else
			{
				cpu1.enabled = true;
				player1b.enabled = false;
				arrow1.text = "<";
				bt1.interactable = true;
				arrow2.text = ">";
				if( cpu2.enabled)
				{
				arrow4.text = ">";
				bt4.interactable = true;
				}
			}
			break;
			case 3:
			if(arrow3.text == "<"){
			cpu2.enabled = false;
			player2w.enabled = true;
			arrow3.text = ">";
			arrow1.text = "";
			bt1.interactable = false;
			arrow4.text = "";
			bt4.interactable = false;
			}
			else
			{
				cpu2.enabled = true;
				player2w.enabled = false;
				arrow3.text = "<";
				arrow4.text = ">";
				bt4.interactable = true;
				if( cpu1.enabled)
				{
				arrow1.text = "<";
				bt1.interactable = true;
				}
			}
			break;
			case 4:
			if(arrow4.text == ">"){
				cpu2.enabled = false;
				player2b.enabled = true;
				arrow3.text = "";
				bt3.interactable = false;
				arrow4.text = "<";
				arrow2.text = "";
				bt2.interactable = false;
			}
			else
			{
				cpu2.enabled = true;
				player2b.enabled = false;
				arrow3.text = "<";
				bt3.interactable = true;
				arrow4.text = ">";
				if( cpu1.enabled)
				{
					arrow2.text = ">";
					bt2.interactable = true;
				}
			}
			break;
			default:
			break;
		}
	}

	public void changeLevel(){
		if(level.text == "FÁCIL")
		{
			level.text = "DIFÍCIL";
			mode = "dificil";
		}
		else
		{
			level.text = "FÁCIL";
			mode = "facil";
		}
	}

	public void mouseOverButton(Text button){

		button.fontStyle = FontStyle.Bold;
	}

	public void mouseExitButton(Text button){

		
		button.fontStyle = FontStyle.Normal;
	}

/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{
		switch (menu)
		{
			case (int)MenuType.none:
			
			break;
			case (int)MenuType.main:
			
				animMain.Play("slideIn");
			
			break;
			case (int)MenuType.rules:
			
				animRules.Play("slideIn");
			
			
			break;
			case (int)MenuType.newGame:
			
				animNewGame.Play("slideIn");
			
			break;
			case (int)MenuType.pause:
		
				animPause.Play("slideIn");
				
			
			break;

			default:
			if(player1b.enabled || player2b.enabled){
				blackPieces = "player";
			}else
				blackPieces = "cpu";

			if(player1w.enabled || player2w.enabled)
				whitePieces = "player";
			else
				whitePieces = "cpu";

			p1 = cpu1.enabled? "IA" : "Player";
			p2 = cpu2.enabled? "IA" : "Player";
			SceneManager.LoadScene("Demo");
			break;
		}

		
	}

	
}
