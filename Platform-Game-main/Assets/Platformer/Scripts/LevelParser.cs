﻿using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParser : MonoBehaviour
{
    public string filename;
    public Transform environmentRoot;

    [Header("Block Prefab")]
    public GameObject rockPrefab;
    public GameObject brickPrefab;
    public GameObject questionBoxPrefab;
    public GameObject stonePrefab;
    public GameObject WaterPrefab;
    public GameObject FlagPolePrefab;
     public GameObject MarioPrefab;

    // --------------------------------------------------------------------------
    void Start()
    {
        LoadLevel();
         MarioPrefab = GameObject.Find("Mario");
    }

    // --------------------------------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }
    void FixedUpdate()
    {
        // if (Input.GetKey(KeyCode.LeftArrow))
        // {
        //     Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - 1, Camera.main.transform.position.y, Camera.main.transform.position.z);
        // }

        // if (Input.GetKey(KeyCode.RightArrow))
        // {
        //     Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + 1, Camera.main.transform.position.y, Camera.main.transform.position.z);
        // }
        Camera.main.transform.position = new Vector3(MarioPrefab.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
    }
    // --------------------------------------------------------------------------
    private void LoadLevel()
    {
        string fileToParse = $"{Application.dataPath}{"/Resources/"}{filename}.txt";
        Debug.Log($"Loading level file: {fileToParse}");

        Stack<string> levelRows = new Stack<string>();

        // Get each line of text representing blocks in our level
        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                levelRows.Push(line);
            }

            sr.Close();
        }
            int row = 0;
        // Go through the rows from bottom to top
        while (levelRows.Count > 0)
        {
            string currentLine = levelRows.Pop();

            char[] letters = currentLine.ToCharArray();
            for (int col = 0; col < letters.Length; col++)
            {
                var letter  = letters[col];
                // Todo - Instantiate a new GameObject that matches the type specified by letter
                // Todo - Position the new GameObject at the appropriate location by using row and column
                // Todo - Parent the new GameObject under levelRoot
                if (letter == '?')
                {
                    Vector3 newPos = new Vector3(col, row, 0f);
                    Instantiate(questionBoxPrefab, newPos, Quaternion.identity, environmentRoot);
                    questionBoxPrefab.transform.Rotate(0, 90, 0);
                }
                else if (letter == 's')
                {
                    Vector3 newPos = new Vector3(col, row, 0f);
                    Instantiate(stonePrefab, newPos, Quaternion.identity, environmentRoot);
                }
                else if (letter == 'b')
                {
                    Vector3 newPos = new Vector3(col, row, 0f);
                    Instantiate(brickPrefab, newPos, Quaternion.identity, environmentRoot);
                }
                else if (letter == 'x')
                {
                    Vector3 newPos = new Vector3(col, row, 0f);
                    Instantiate(rockPrefab, newPos, Quaternion.identity, environmentRoot);
                }
                
            }

            row++;
        }
    }

    // --------------------------------------------------------------------------
    private void ReloadLevel()
    {
        foreach (Transform child in environmentRoot)
        {
           Destroy(child.gameObject);
        }
        LoadLevel();
    }
}
