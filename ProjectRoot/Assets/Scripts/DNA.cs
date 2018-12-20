using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour {

    public enum MoveDirection { LEFT, RIGHT, JUMP }
    public MoveDirection currentDir;
    public List<MoveDirection> directions = new List<MoveDirection>();
    public int count = 0;
    public int lifespan = 200;
    private int currentIndex;
    private float timer = 0.1f;
    private Vector3 dir;


    void Start () {
        SetDirection();
    }
	
	void Update () {
        timer -= Time.deltaTime;
        count++;
        if (timer < 0f && count < lifespan)
        {
            SwitchDirection();
            timer = 0.1f;
        }
	}

    private void SetDirection()
    {
        directions.Add(MoveDirection.LEFT);
        directions.Add(MoveDirection.RIGHT);
        directions.Add(MoveDirection.JUMP);
    }

    private void SwitchDirection()
    {
        currentIndex++;
        if (currentIndex > directions.Count - 1)
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

    /*public void SetDirection(Vector3 dir, Genome.MoveDirection direction)
    {
        if (direction == Genome.MoveDirection.LEFT)
        {

        }
        else
        {

        }
    }*/
}
