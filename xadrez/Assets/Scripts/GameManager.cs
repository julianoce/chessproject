﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int turno = 1;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int getTurno()
    {
        return turno;
    }

    public void mudaTurno()
    {
        if(turno == 2)
        {
            turno = 1;
        }else if (turno == 1)
        {
            turno = 2;
        }
    }
}