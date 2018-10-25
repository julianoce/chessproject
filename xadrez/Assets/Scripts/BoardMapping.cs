using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardMapping : MonoBehaviour {

    public GameObject greenTile;

    public List<Vector2> movimentos;

    public List<GameObject> tiles;

    private int pos;
    private List<GameObject> auxList;
    private BoardRules br;


    // Use this for initialization
    void Start() {
        auxList = new List<GameObject>();
        br = GameObject.FindObjectOfType(typeof(BoardRules)) as BoardRules;
    }

    // Update is called once per frame
    void Update() {
        //tiles[pos];
        if (Input.GetKeyDown(KeyCode.I))
        {
            // makeTiles();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            clearTiles();
        }
    }

    public void makeTiles(GameObject peca){
        movimentos = br.MovimentosPossiveis(peca);
        foreach (Vector2 v in movimentos)
        {
            pos = findInList(int.Parse(v.x + ""), int.Parse(v.y + ""));
            auxList.Add(Instantiate(greenTile, tiles[pos].transform.position, tiles[pos].transform.localRotation));
        }
    }

    public void clearTiles()
    {
        foreach (GameObject i in auxList)
        {
            Destroy(i);
        }
        auxList.Clear();
    }

    int findInList(int i, int j)
    {
        return i * 8 + j;
    }
}
