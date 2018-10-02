using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPiece : MonoBehaviour {

    public GameObject board;

    private Component[] comp;

    private GameManager gm;
    private PlayerScript ps;

    // Use this for initialization
    void Start () {
        gm = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        ps = GameObject.FindObjectOfType(typeof(PlayerScript)) as PlayerScript;

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void cleanSelection()
    {
        comp = board.GetComponentsInChildren<Outline>();

        foreach (Outline c in comp)
        {
            c.enabled = false;
            ps.setSelected(null);
        }
    }

    void OnMouseDown()
    {
        if (gm.getTurno() == 1)
        {
            if (this.tag == "whitePiece")
            {
                cleanSelection();
                this.GetComponent<Outline>().enabled = true;
                ps.setSelected(this.gameObject);
            } 
        }
        else
        {
            if (this.tag == "blackPiece")
            {
                cleanSelection();
                this.GetComponent<Outline>().enabled = true;
                ps.setSelected(this.gameObject);
            }
        }
        
    }
}
