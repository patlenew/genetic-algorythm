using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DNA {

    public enum MoveDirection { LEFT, RIGHT, JUMP }
    public MoveDirection currentDir;
    public MoveDirection[] directions;
    public int lifespan;
    private int currentIndex;
    private float timer = 0.1f;
    private Vector3 dir;


    public DNA(MoveDirection[] directions)
    {
        if (directions != null)
        {
            this.directions = directions;
        }
        lifespan = 300;
        //SetDirection();
    }

    void Start() {
        /*lifespan = 500;
        speed = 10f;*/
        //SetDirection();
    }

    void Update() {
        /* timer -= Time.deltaTime;
         count++;
         if (timer < 0f && count < lifespan)
         {
             SwitchDirection();
             timer = 0.1f;
         }*/
    }

    public void SetDirection()
    {
        directions = new MoveDirection[lifespan];
        for (int i = 0; i < directions.Length; i++)
        {
            System.Array A = System.Enum.GetValues(typeof(MoveDirection));
            MoveDirection V = (MoveDirection)A.GetValue(UnityEngine.Random.Range(0, A.Length));
            directions[i] = V;
        }
    }

    public void SwitchDirection()
    {
        currentIndex++;

        if (currentIndex > directions.Length)
        {
            currentIndex = 0;
        }
        currentDir = directions[currentIndex];
        SetDirectionVector();
    }

    private void SetDirectionVector()
    {
        if (currentDir == MoveDirection.LEFT)
        {
            dir = new Vector3(-1f, 0f, 0f);
        }
        else if (currentDir == MoveDirection.RIGHT)
        {
            dir = new Vector3(1f, 0f, 0f);
        }
    }

    public Vector3 GetDirectionValue()
    {
        return dir;
    }

    public DNA Crossover(DNA partner)
    {
        MoveDirection[] newDna = new MoveDirection[lifespan];
        float mid = Mathf.Floor(UnityEngine.Random.Range(0, directions.Length));

        for (int i = 0; i<directions.Length; i++)
        {
            if (i > mid)
            {
                newDna[i] = directions[i];
            }
            else
            {
                newDna[i] = partner.directions[i];
            }
        }

        return new DNA(newDna);
    }

    public void Mutate()
    {
        for (int i = 0; i < directions.Length; i++)
        {
            int range = UnityEngine.Random.Range(0,2);
            if (range == 0)
            {
                System.Array A = System.Enum.GetValues(typeof(MoveDirection));
                MoveDirection V = (MoveDirection)A.GetValue(UnityEngine.Random.Range(0, A.Length));
                directions[i] = V;
            }
        }
    }
}
