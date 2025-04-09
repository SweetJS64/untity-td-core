using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTowerControl : MonoBehaviour
{
    [SerializeField] private int _lengthGrid = 2;
    [SerializeField] private int _widthGrid = 3;
    [SerializeField] private int _spacingGrid = 2;
    [SerializeField] private Vector2 _posGrid = new(-1, -3);

    public GameObject GreenSquare;
    public GameObject RedSquare;

    private Vector2[,] _arrayGridPosElements;
    private bool[,] _arrayGridBool;

    void Start()
    {
        _arrayGridPosElements = new Vector2[_lengthGrid, _widthGrid];
        _arrayGridBool = new bool[_lengthGrid, _widthGrid];
        for (int i = 0; i < _arrayGridPosElements.GetLength(0); i++)
        {
            for (int j = 0; j < _arrayGridPosElements.GetLength(1); j++)
            {
                _arrayGridPosElements[i, j] = new Vector2(_posGrid.x + i * _spacingGrid, _posGrid.y + j * _spacingGrid);
                _arrayGridBool[i, j] = true;
            }
        }
    }

    public bool[,] GetBoolValueInGrid()
    {
        return _arrayGridBool;
    }

    public Vector2[,] GetPosValueInGrid()
    {
        return _arrayGridPosElements;
    }

    public bool[,] SetBoolValueInGrid(int indexBoolI, int indexBoolJ, bool status)
    {
        _arrayGridBool[indexBoolI, indexBoolJ] = status;
        return _arrayGridBool;
    }
}