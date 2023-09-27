using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    public GameObject cellPrefab;
    float cellSize = 0.2f;
    cellScript[,] cells;
    int numberOfColums, numberOfRows;
    int spawnChancePrecentage = 5;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 4;

        numberOfColums = (int)Mathf.Floor((Camera.main.orthographicSize * Camera.main.aspect *2) / cellSize);
        numberOfRows = (int)Mathf.Floor(Camera.main.orthographicSize * 2 / cellSize);

        cells = new cellScript[numberOfColums, numberOfRows];

        //For each Row
        for (int y = 0; y < numberOfRows; ++y)
        {
            //For each colum in each row
            for(int x = 0; x < numberOfColums; ++x)
            {
                Vector2 newPos = new Vector2(x * cellSize - Camera.main.orthographicSize * Camera.main.aspect, 
                    y * cellSize - Camera.main.orthographicSize);
                var newCell = Instantiate(cellPrefab, newPos, Quaternion.identity);
                newCell.transform.localScale = Vector2.one * cellSize;
                cells[x, y] = newCell.GetComponent<cellScript>();

                if (Random.Range(0, 100) < spawnChancePrecentage)
                {
                    cells[x, y].alive = true;
                }
                cells[x, y].UpdateStatus();

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int y = 0; y < numberOfRows; y++)
        {
            for( int x = 0;x < numberOfColums; ++x)
            {
                cells[x, y ].UpdateStatus();
            }
        }
    }
    // Check if cell is alive, on all the rows and colums
    //If Alive,
    //check if cells[x -1, y] is Alive
    //if cells[x +1, y] is Alive
    //if cells[x, y -1] is Alive
    //if cells[x, y +1] is Alive
    //if cells[x -1, y -1] is Alive
    //if cells[x -1, y +1] is Alive
    //if cells[x +1, y - 1] is Alive
    //if cells[x + 1, y + 1] is Alive

    //Every cell should have the int neighbors. 

    // bool livesToNextGeneration = False

    //For each if above, if Alive = True, neighbors += 1; If Alive = False, neighbors += 0;
    // if neighbors == 3 or neighbors == 2, livesToNextGeneration = True.
    // if nighbors <= 1 or nighbors >= 4, livesToNextGeneration = False.






}
