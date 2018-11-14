using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject white, black;
    public string player1, player2;
    public int turno = 1;
    private GameObject promoteToChange;
    private SelectPiece sp;
    private Collider[] coll;
    private IA ia;
    private CameraMove cm;
    // Use this for initialization
    void Start()
    {
        sp = GameObject.FindObjectOfType(typeof(SelectPiece)) as SelectPiece;
        ia =  GameObject.FindObjectOfType(typeof(IA)) as IA;
        cm = GameObject.FindObjectOfType(typeof(CameraMove)) as CameraMove;


        if(player1.Equals("IA")){
            StartCoroutine(RunIA(1, "White"));
        }

        if(player1.Equals("Player")&& player2.Equals("Player")){
            cm.active = true;
        }else if(player1.Equals("IA")&& player2.Equals("Player")){
            cm.setPlayer2();
            StartCoroutine(RunIA(1, "White"));
        }  
     }
 
     IEnumerator RunIA(float waitTime, string color)
     {
         yield return new WaitForSeconds(waitTime);
         
        Debug.Log("ia rodando em coroutine");
        Debug.Log(color);
        ia.buscar(color);
        
     }
    
    // Update is called once per frame
    void Update()
    {

    }

    public void setPromoteToChange(GameObject o){
        promoteToChange = o;
    }
    public GameObject getPromoteToChange(){
        return promoteToChange;;
    }
    public int getTurno()
    {
        return turno;
    }

    public void disableColliders(){
        coll = white.GetComponentsInChildren<Collider>();
        foreach(Collider c in coll){
            c.enabled = false;
        }
        coll = black.GetComponentsInChildren<Collider>();
        foreach(Collider c in coll){
            c.enabled = false;
        }
    }

    public void mudaTurno()
    {
        if(turno == 2)
        {
            turno = 1;
            // Debug.Log("IA pensando");
            // ia.buscar();
            coll = white.GetComponentsInChildren<Collider>();
            foreach(Collider c in coll){
                c.enabled = true;
            }
            coll = black.GetComponentsInChildren<Collider>();
            foreach(Collider c in coll){
                c.enabled = false;
            }
            if(player1.Equals("IA")){
                StartCoroutine(RunIA(0, "White"));
            } 
        
        }else if (turno == 1)
        {
            turno = 2;
             coll = white.GetComponentsInChildren<Collider>();
            foreach(Collider c in coll){
                c.enabled = false;
            }
            coll = black.GetComponentsInChildren<Collider>();
            foreach(Collider c in coll){
                c.enabled = true;
            }
            if(player2.Equals("IA")){
                StartCoroutine(RunIA(0, "Black"));
            } 
        }
        sp.cleanSelection();
    }
}