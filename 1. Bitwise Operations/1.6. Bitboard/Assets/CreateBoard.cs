using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateBoard : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject housePrefab;
    public GameObject treePrefab;
    public Text score;
    GameObject[] tiles;
    long dirtBB = 0;
    long desertBB = 0;
    long playerBB = 0;
    long treeBB = 0;
    const int LENGTH = 8;
    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[64];
        for (int r = 0; r < LENGTH; r++)
        {
            for (int c = 0; c < LENGTH; c++)
            {
                int randomTile = UnityEngine.Random.Range(0, tilePrefabs.Length);
                Vector3 pos = new(c, 0, r);
                GameObject tile = Instantiate(tilePrefabs[randomTile], pos, Quaternion.identity);
                tile.name = tile.tag + "_" + r + " " + c;
                tiles[r * LENGTH + c] = tile;
                if (tile.CompareTag("Dirt"))
                {
                    dirtBB = SetCellState(dirtBB, r, c);
                }
                else if (tile.CompareTag("Desert"))
                {
                    desertBB = SetCellState(desertBB, r, c);
                }
            }
            PrintBB("Dirt", dirtBB);
        }
        PrintBB("Dirt", dirtBB);
        Debug.Log("Dirt cells = " + CellCount(dirtBB));
        InvokeRepeating(nameof(PlantTree), 1, 1);
    }

    void PlantTree()
    {
        int rr = UnityEngine.Random.Range(0, LENGTH);
        int rc = UnityEngine.Random.Range(0, LENGTH);
        // Debug.Log("PlantTree: " + rr + " " + rc);

        if (GetCellState(dirtBB & ~playerBB, rr, rc))
        {
            GameObject tree = Instantiate(treePrefab);
            tree.transform.parent = tiles[rr * LENGTH + rc].transform;
            tree.transform.localPosition = Vector3.zero;
            treeBB = SetCellState(treeBB, rr, rc);
        }
    }

    void PrintBB(string name, long BB)
    {
        Debug.Log(name + ": " + Convert.ToString(BB, 2).PadLeft(64, '0'));
    }

    long SetCellState(long bitboard, int row, int col)
    {
        long newBit = 1L << (row * LENGTH + col);
        return (bitboard |= newBit);
    }

    bool GetCellState(long bitboard, int row, int col)
    {
        long mask = 1L << (row * LENGTH + col);
        return ((bitboard & mask) != 0);
    }

    int CellCount(long bitboard)
    {
        int count = 0;
        long bb = bitboard;
        while (bb != 0)
        {
            bb &= bb - 1;
            count++;
        }
        return count;
    }

    void CalculateScore()
    {
        score.text = "Score: " + (CellCount(dirtBB & playerBB) * 10 + CellCount(desertBB & playerBB) * 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                int r = (int)hit.collider.gameObject.transform.position.z;
                int c = (int)hit.collider.gameObject.transform.position.x;
                if (GetCellState((dirtBB & ~treeBB) | desertBB, r, c))
                {
                    GameObject house = Instantiate(housePrefab);
                    house.transform.parent = hit.collider.gameObject.transform;
                    house.transform.localPosition = Vector3.zero;
                    playerBB = SetCellState(playerBB, r, c);
                    CalculateScore();
                }

            }
        }
    }
}
