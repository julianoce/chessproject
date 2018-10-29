using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public Text text_novo_jogo;
    public Text text_tutorial;
    public Text text_sair;

    public Text checkW, checkB, backToMenu, jogar, facil, medio, dificil, backToMenuFromTutorial,
                continuarJogo, sairJogo, tutorialJogo;
    public RawImage img_alto_falante;
    public RawImage img_sem_alto_falante;
    bool som, pause;
    Ray ray;

	//level pode ser "facil", "medio" ou "dificil", black e white podem ser "player" ou "cpu". 
    public static string level, black, white;
    Scene sc;
    RaycastHit hit;
    Camera cam;
    string menu;
    Animator anim;

	
    public GameObject canvasTutorial, canvasPrincipal, canvasNovoJogo, canvasPause;
    // Use this for initialization
    void Start()
    {
        sc = SceneManager.GetActiveScene();
        DontDestroyOnLoad(this.gameObject);
        som = true;
        pause = false;
        anim = GetComponent<Animator>();
        //anim.SetBool("menuEnter", true);
        anim.Play("MenuAnimation");
        //cam = GetComponent<Camera>();
        menu = "principal";
        anim.SetBool("boardReturn", true);
        canvasNovoJogo.SetActive(false);
        canvasTutorial.SetActive(false);
        canvasPause.SetActive(false);
        canvasPrincipal.SetActive(true);
        medio.fontStyle = FontStyle.Bold;
		medio.fontSize = 17;
        level = "medio";
        this.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        sc = SceneManager.GetActiveScene();
        

        if(sc.name == "Demo" && Input.GetKeyDown(KeyCode.Escape)){
                pause = true;
                this.gameObject.SetActive(true);
                anim.SetBool("boardReturn", false);
                anim.Play("MenuAnimation");
                menu = "pause";
                Debug.Log(Camera.allCameras);
            }

      
            
        

       
        


        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            switch (hit.collider.name)
            {
                case "novo jogo":
                    text_novo_jogo.fontStyle = FontStyle.Bold;
                    text_sair.fontStyle = FontStyle.Normal;
                    text_tutorial.fontStyle = FontStyle.Normal;
                    break;

                case "tutorial":
                    text_novo_jogo.fontStyle = FontStyle.Normal;
                    text_sair.fontStyle = FontStyle.Normal;
                    text_tutorial.fontStyle = FontStyle.Bold;
                    break;

                case "sair":
                    text_novo_jogo.fontStyle = FontStyle.Normal;
                    text_sair.fontStyle = FontStyle.Bold;
                    text_tutorial.fontStyle = FontStyle.Normal;
                    break;

                case "backToMenu":
                    backToMenu.fontStyle = FontStyle.Bold;
                    jogar.fontStyle = FontStyle.Normal;
                    break;


                case "jogar":
                    jogar.fontStyle = FontStyle.Bold;
                    backToMenu.fontStyle = FontStyle.Normal;
                    break;

                case "backToMenuTuto":
                backToMenuFromTutorial.fontStyle = FontStyle.Bold;
                jogar.fontStyle = FontStyle.Normal;
                break;

                case "continuarJogo":
                continuarJogo.fontStyle = FontStyle.Bold;
                tutorialJogo.fontStyle = FontStyle.Normal;
                sairJogo.fontStyle = FontStyle.Normal;
                break;

                case "tutorialJogo":
                continuarJogo.fontStyle = FontStyle.Normal;
                tutorialJogo.fontStyle = FontStyle.Bold;
                sairJogo.fontStyle = FontStyle.Normal;
                break;

                case "sairJogo":
                continuarJogo.fontStyle = FontStyle.Normal;
                tutorialJogo.fontStyle = FontStyle.Normal;
                sairJogo.fontStyle = FontStyle.Bold;
                break;


                default:

                    continuarJogo.fontStyle = FontStyle.Normal;
                    tutorialJogo.fontStyle = FontStyle.Normal;
                    sairJogo.fontStyle = FontStyle.Normal;
                    backToMenuFromTutorial.fontStyle = FontStyle.Normal;
                    checkB.fontStyle = FontStyle.Normal;
                    backToMenu.fontStyle = FontStyle.Normal;
                    text_novo_jogo.fontStyle = FontStyle.Normal;
                    text_sair.fontStyle = FontStyle.Normal;
                    text_tutorial.fontStyle = FontStyle.Normal;
                    jogar.fontStyle = FontStyle.Normal;

                    break;

            }

            if (som)
            {
                img_alto_falante.enabled = true;
                img_sem_alto_falante.enabled = false;
            }
            else
            {
                img_alto_falante.enabled = false;
                img_sem_alto_falante.enabled = true;
            }


            if (Input.GetMouseButtonUp(0))
            {

                
                switch (hit.collider.name)
                {


                    //------------- CANVAS PRINCIPAL----------------------
                    case "somCollider":
                        som = !som;
                        text_novo_jogo.fontStyle = FontStyle.Normal;
                        text_sair.fontStyle = FontStyle.Normal;
                        text_tutorial.fontStyle = FontStyle.Normal;
                        break;

                    case "novo jogo":
                        anim.SetBool("boardReturn", true);
                        anim.Play("MenuExitAnimation");
                        menu = "novo jogo";
                        //
                        break;

                    case "tutorial":
                        anim.SetBool("boardReturn", true);
                        anim.Play("MenuExitAnimation");
                        menu = "tutorial";
                        break;

                    case "sair":
                        Application.Quit();
                        break;

                    //------------------------- CANVAS NOVO JOGO-----------------

                    case "checkBoxW":
                        checkW.enabled = !checkW.enabled;
                        break;

                    case "checkBoxB":
                        checkB.enabled = !checkB.enabled;

                        break;

                    case "backToMenu":
                        anim.Play("MenuExitAnimation");
                        menu = "principal";
                        break;

                    case "backToMenuTuto":
                        anim.Play("MenuExitAnimation");
                        menu = "principal";
                        break;


                    case "jogar":
                        anim.SetBool("boardReturn", false);
                        anim.Play("MenuExitAnimation");
                        white = checkW.enabled ? "player" : "cpu";
                        black = checkB.enabled ? "player" : "cpu";
                        SceneManager.LoadScene("Demo");

                        break;

                    case "facil":
						facil.fontSize = 17;
                        facil.fontStyle = FontStyle.Bold;
						dificil.fontSize = 12;
                        dificil.fontStyle = FontStyle.Normal;
						medio.fontSize = 12;
                        medio.fontStyle = FontStyle.Normal;
                        level = "facil";
                        break;

                    case "medio":
						medio.fontSize = 17;
						facil.fontSize = 12;
						dificil.fontSize = 12;
                        medio.fontStyle = FontStyle.Bold;
                        facil.fontStyle = FontStyle.Normal;
                        dificil.fontStyle = FontStyle.Normal;
                        level = "medio";
                        break;

                    case "dificil":
						medio.fontSize = 12;
						facil.fontSize = 12;
						dificil.fontSize = 17;
                        facil.fontStyle = FontStyle.Normal;
                        dificil.fontStyle = FontStyle.Bold;
                        medio.fontStyle = FontStyle.Normal;
                        level = "dificil";
                        break;

                //------------------------------PAUSE-----------------------------------
                    case "continuarJogo":
                    anim.SetBool("boardReturn", false);
                    anim.Play("MenuExitAnimation");
                    menu = "continuarJogo";
                    pause =false;

                    break;

                    case "tutorialJogo":
                        anim.SetBool("boardReturn", true);
                        anim.Play("MenuExitAnimation");
                        menu = "tutorial";
                    break;

                    case "sairJogo":
                        menu = "principal";
                        SceneManager.LoadScene("Menu");
                    break;


                    default:

                        break;
                }
            }

        }

        //----------------------------------PAUSE-----------------------------------

       

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");

        switch (menu)
        {
            case "novo jogo":
                canvasNovoJogo.SetActive(true);
                canvasTutorial.SetActive(false);
                canvasPrincipal.SetActive(false);
                canvasPause.SetActive(false);
                break;

            case "principal":
                canvasPause.SetActive(false);
                canvasNovoJogo.SetActive(false);
                canvasTutorial.SetActive(false);
                canvasPrincipal.SetActive(true);

                break;
            case "tutorial":
                canvasPause.SetActive(false);
                canvasNovoJogo.SetActive(false);
                canvasTutorial.SetActive(true);
                canvasPrincipal.SetActive(false);

                break;

            case "pause":
                canvasPause.SetActive(true);
                canvasNovoJogo.SetActive(false);
                canvasTutorial.SetActive(false);
                canvasPrincipal.SetActive(false);
            break;

            case "continuarJogo":
            Time.timeScale = 1;
            this.gameObject.SetActive(false);

            break;



            default:


                break;
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>

    //----A FAZER!!!------------------------------canvas pause nao ta entrando!!!!!-canvas nao muda pq na outra cena nao tem o trigger changeMenu! -----
    //---------------------------------ver posiçao da camera para para o pause!!!!! LEMBRAR DA CAMERA E CUBO DO GUIZAN (DEPTH ONLY E MEXER CULLING MASK [CRIAR LAYER DO MENU])
    void OnTriggerExit(Collider other)
    {
         switch (menu)
        {
            case "novo jogo":
                canvasNovoJogo.SetActive(true);
                canvasTutorial.SetActive(false);
                canvasPrincipal.SetActive(false);
                canvasPause.SetActive(false);
                break;

            case "principal":
                canvasPause.SetActive(false);
                canvasNovoJogo.SetActive(false);
                canvasTutorial.SetActive(false);
                canvasPrincipal.SetActive(true);

                break;
            case "tutorial":
                canvasPause.SetActive(false);
                canvasNovoJogo.SetActive(false);
                canvasTutorial.SetActive(true);
                canvasPrincipal.SetActive(false);

                break;

            case "pause":
                canvasPause.SetActive(true);
                canvasNovoJogo.SetActive(false);
                canvasTutorial.SetActive(false);
                canvasPrincipal.SetActive(false);
            break;

            case "continuarJogo":
            Time.timeScale = 1;
            this.gameObject.SetActive(false);

            break;



            default:


                break;
        }
    }


}
