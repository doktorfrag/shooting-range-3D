﻿using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    //public variables

    //private variables
    private static GameController _gameControllerInstance;
    private List<List<string>> _writeList = new List<List<string>>();

    //private variable accessors
    public static GameController Instance
    {
        get { return _gameControllerInstance ?? (_gameControllerInstance = new GameObject("GameController").AddComponent<GameController>()); }
    }

    private int _shotsFired = 0;
    public int ShotsFired
    {
        get
        {
            return _shotsFired;
        }

        set
        {
            _shotsFired += value;
        }
    }

    static private int _targetsHit = 0;
    public int TargetsHit
    {
        get
        {
            return _targetsHit;
        }

        set
        {
            _targetsHit += value;
        }
    }

    private bool _targetDestroyed = false;
    public bool TargetDestroyed
    {
        get
        {
            return _targetDestroyed;
        }

        set
        {
            _targetDestroyed = value;
        }
    }

    private List<string> _writeData = new List<string>();
    public List<string> WriteData
    {
        set
        {
            _writeData = value;
            _writeList.Add(_writeData);
            _writeData = null;
        }
    }

    //methods
    private void Update()
    {
     if(_targetDestroyed)
        {
            _targetDestroyed = false;
            StartCoroutine(SpawnTarget());
        }
    }

    private void OnDestroy()
    {
        string path = "Assets/Resources/trajectoryData.txt";
        StreamWriter writer = new StreamWriter(path, true);
        if (_writeList != null)
        {
            foreach (List<string> listEntry in _writeList)
            {
                foreach (string stringEntry in listEntry)
                {
                    writer.WriteLine(stringEntry);
                }
            }
        }
        writer.Close();
    }

    private IEnumerator SpawnTarget()
    {
        yield return new WaitForSeconds(1);
        GameObject target = Instantiate(Resources.Load("Prefabs/Target", typeof(GameObject))) as GameObject;
        float randomZ = Random.Range(15.0f, 20.0f);
        target.transform.position = new Vector3(0f, 0.5f, randomZ);
    }
}
