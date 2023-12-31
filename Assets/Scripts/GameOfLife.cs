using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOfLife : MonoBehaviour
{
    public GameObject cellPrefab;
    float cellSize = 0.5f;
    cellScript[,] cells;
    int numberOfColums, numberOfRows;
    int spawnChancePrecentage = 25;
    public bool livesToNextGeneration = false;
    private bool newGeneration = true;
    private int generationCount = 0;
    public int maximumGenerations = 1000;
    public float generationDelay = 2f;


    void Start()
    {

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 8;

        numberOfColums = (int)Mathf.Floor((Camera.main.orthographicSize * Camera.main.aspect * 2) / cellSize);
        numberOfRows = (int)Mathf.Floor(Camera.main.orthographicSize * 2 / cellSize);

        cells = new cellScript[numberOfColums, numberOfRows];


        for (int y = 0; y < numberOfRows; ++y)
        {
            for (int x = 0; x < numberOfColums; ++x)
            {
                Vector2 newPos = new Vector2(x * cellSize - Camera.main.orthographicSize * Camera.main.aspect,
                    y * cellSize - Camera.main.orthographicSize);
                var newCell = Instantiate(cellPrefab, newPos, Quaternion.identity);
                newCell.transform.localScale = Vector2.one * cellSize;
                cells[x, y] = newCell.GetComponent<cellScript>();

                
                if(menu.startPoint == 0)
                {
                    StartAsRandom(x, y);
                }
                if (menu.startPoint == 1)
                {
                    StartAsGP(x, y);
                }
                if (menu.startPoint == 2)
                {
                    StartAsCross(x, y);
                }

                cells[x, y].UpdateStatus();
            }
        }
    }

   internal void StartAsRandom(int x, int y)
    {
        if (Random.Range(0, 100) < spawnChancePrecentage)
         {
             cells[x, y].alive = true;
         }
    }
   internal void StartAsCross(int x, int y)
    {
        if (x == numberOfColums / 2)
        {
            cells[x, y].alive = true;
        }
        if (y == numberOfRows / 2)
        {
            cells[x, y].alive = true;
        }
    }

   internal void StartAsGP(int x, int y)
    {
        // Maximum cellsize 0.5f
        if (x == 10 && y == 2 || x == 11 && y == 2 || x == 12 && y == 2 || x == 13 && y == 2  || x == 14 && y == 2 || x == 15 && y == 2 ||
            x == 20 && y == 2 ||
            x == 10 && y == 15 || x == 11 && y == 15 || x == 12 && y == 15 || x == 13 && y == 15 || x == 14 && y == 15 || x == 15 && y == 15 ||
            x == 20 && y == 15 || x == 21 && y == 15 || x == 22 && y == 15 || x == 23 && y == 15 || x == 24 && y == 15 || x == 25 && y == 15 ||
            x == 10 && y == 8 || x == 12 && y == 8 || x == 13 && y == 8 || x == 14 && y == 8 || x == 15 && y == 8 || x == 20 && y == 8 || x == 21 && y == 8 ||
            x == 22 && y == 8 || x == 23 && y == 8 || x == 24 && y == 8 || x == 25 && y == 8 ||
            x == 10 && y == 3 || x == 10 && y == 4 || x == 10 && y == 5 || x == 10 && y == 6 || x == 10 && y == 7 || x == 10 && y == 9 || x == 10 && y == 10 ||
            x == 10 && y == 11 || x == 10 && y == 12 || x == 10 && y == 13 || x == 10 && y == 14 || x == 10 && y == 15 ||
            x == 15 && y == 3 || x == 15 && y == 4 || x == 15 && y == 5 || x == 15 && y == 6 || x == 15 && y == 7
            || x == 20 && y == 3 || x == 20 && y == 4 || x == 20 && y == 5 || x == 20 && y == 6 || x == 20 && y == 7 || x == 20 && y == 9 || x == 20 && y == 10
            || x == 20 && y == 11 || x == 20 && y == 12 || x == 20 && y == 13 || x == 20 && y == 14
            || x == 25 && y == 9 || x == 25 && y == 10 || x == 25 && y == 11 || x == 25 && y == 12 || x == 25 && y == 13 || x == 25 && y == 14 ||

            x == 27 && y == 15 || x == 28 && y == 15 || x == 29 && y == 15 || x == 27 && y == 12 || x == 28 && y == 12 || x == 29 && y == 12 ||
            x == 27 && y == 8 || x == 28 && y == 8 || x == 29 && y == 8 || x == 27 && y == 9|| x == 27 && y == 10 || x == 27 && y == 11 ||x == 29 && y == 14|| x == 29 && y == 13 ||
            x == 27 && 9 == 10 ||
            x == 31 && y == 15 || x == 32 && y == 15 || x == 33 && y == 15 ||
            x == 31 && y == 12 || x == 32 && y == 12 || x == 33 && y == 12 ||
            x == 31 && y == 8 || x == 32 && y == 8 || x == 33 && y == 8 ||
            x == 33 && y == 14 || x == 33 && y == 13 || x == 33 && y == 11|| x == 33 && y == 10 || x == 33 && y == 9 
            )
            
        {
            cells[x, y].alive = true;
        }
    }
    void Update()
    {
        if (newGeneration == true && generationCount < maximumGenerations)
        {
            NextGeneration();
            generationCount++;
            newGeneration = false;
            StartCoroutine(NextGenerationBuffer());
        }
    }
    
    IEnumerator NextGenerationBuffer()
    {
        yield return new WaitForSeconds(generationDelay);
        UpdateCellStatus();
        newGeneration = true;
    }

    public void NextGeneration()
    {
        for (int y = 0; y < numberOfRows; y++)
        {
            for (int x = 0; x < numberOfColums; ++x)
            {
                NeighborCheck(x, y);
            }
        }
    }

    public void UpdateCellStatus()
    {
        for (int y = 0; y < numberOfRows; y++)
        {
            for (int x = 0; x < numberOfColums; ++x)
            {
                cells[x, y].alive = cells[x, y].livesToNextGeneration;
                cells[x, y].UpdateStatus();
            }
        }
    }

    public void NeighborCheck(int x, int y)
    {
        int aliveNeighbors = 0;

        if (x > 0 && y > 0 && cells[x - 1, y -1].alive)
        { 
            aliveNeighbors++;
        }
        if ( y > 0 && cells[x, y -1].alive)
        {
            aliveNeighbors++;
        }
        if (x < numberOfColums -1 && y > 0 && cells[x +1, y -1].alive)
        {
            aliveNeighbors++;
        }
        if (x > 0 && cells[x -1, y].alive)
        {
            aliveNeighbors++;
        }
        if (x < numberOfColums -1 && cells[x +1, y].alive)
        {
            aliveNeighbors++;
        }
        if (x > 0 && y < numberOfRows -1 && cells[x -1, y +1].alive)
        {
            aliveNeighbors++;
        }
        if (x < numberOfColums -1 && y < numberOfRows -1 && cells[x +1, y +1].alive)
        {
            aliveNeighbors++;
        }
        if (y < numberOfRows -1 && cells[x, y +1].alive)
        {
            aliveNeighbors++;
        }

        if (cells[x, y].alive)
        {
            if(aliveNeighbors == 2 || aliveNeighbors == 3)
            {
                cells[x, y].livesToNextGeneration = true;
                cells[x, y].alive = true;
            }

            if (aliveNeighbors < 2 || aliveNeighbors > 3)
            {
                cells[x, y].livesToNextGeneration = false;
                cells[x, y].alive = true;
                cells[x, y].faded = true;
            }
        }

        if (!cells[x, y].alive) 
        {
            if (aliveNeighbors == 3)
            {
                cells[x, y].livesToNextGeneration = true;
            }
            if(aliveNeighbors < 3 || aliveNeighbors > 3)
            {
                cells[x, y].livesToNextGeneration = false;
            }
        }
    }
}

