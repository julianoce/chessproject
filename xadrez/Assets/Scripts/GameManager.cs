using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject white, black;
    public int turno = 1;
    private SelectPiece sp;
    private Collider[] coll;
    private IA ia;
    // Use this for initialization
    void Start()
    {
        sp = GameObject.FindObjectOfType(typeof(SelectPiece)) as SelectPiece;
        ia =  GameObject.FindObjectOfType(typeof(IA)) as IA;
    }

    // Update is called once per frame
    void Update()
    {

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
            ia.buscar("White");
        
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
            Debug.Log("IA pensando");
            ia.buscar("Black");
        }
        sp.cleanSelection();
    }
}