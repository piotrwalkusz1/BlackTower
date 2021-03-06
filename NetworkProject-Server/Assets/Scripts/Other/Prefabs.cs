﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;
using NetworkProject.Monsters;

public class Prefabs : MonoBehaviour
{
    public GameObject _player;
    public GameObject _item;
    public GameObject _visualObject;
    public List<GameObject> _objects;
    public List<GameObject> _monsters;
    

	void Awake() 
    {
        StaticRepository.Prefabs = this;
	}

    public GameObject GetMonster(int id)
    {
        return _monsters[id];
    }
}
