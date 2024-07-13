using System;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField] List<LineRenderer> lineRenderer;
    [SerializeField] float speed;
    [SerializeField] List<GameObject> peoplesObject = new();
    [SerializeField] List<PeoplePath> peoples = new();

    private void Awake()
    {
        for (int i = 0; i < peoplesObject.Count; i++)
        {
            AddPerson(peoplesObject[i], 0);
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < peoples.Count; i++)
        {
            peoples[i].people.transform.position = Vector3.MoveTowards(
                peoples[i].people.transform.position,
                lineRenderer[peoples[i].pathID].GetPosition(peoples[i].pathNode),
                Time.fixedDeltaTime * speed
                );

            if (Vector3.Distance(peoples[i].people.transform.position, lineRenderer[peoples[i].pathID].GetPosition(peoples[i].pathNode)) <= 0.1)
            {
                peoples[i].people.transform.position = lineRenderer[peoples[i].pathID].GetPosition(peoples[i].pathNode);

                if (peoples[i].isReverse)
                {
                    if(peoples[i].pathNode > 0)
                    {
                        peoples[i].pathNode -= 1;
                    }
                    else
                    {
                        //Action End Full Path
                        RemovePerson(peoples[i]);
                    }
                }
                else
                {
                    if (peoples[i].pathNode < lineRenderer[peoples[i].pathID].positionCount - 1)
                    {
                        peoples[i].pathNode += 1;
                    }
                    else
                    {
                        //Acrion End Reverse Path

                        peoples[i].isReverse = true;
                    }
                }
            }
        }
    }

    public void AddPerson(GameObject people, int pathID)
    {
        peoples.Add(new PeoplePath(people, pathID));
        people.transform.position = lineRenderer[pathID].GetPosition(0);
    }

    public void RemovePerson(PeoplePath peoplePath)
    {
        peoples.Remove(peoplePath);
    }
}

public class PeoplePath
{
    public GameObject people;
    public int pathID;
    public bool isReverse;
    public int pathNode;

    public PeoplePath(GameObject people, int pathID)
    {
        this.people = people;
        this.pathID = pathID;
        this.isReverse = false;
        this.pathNode = 1;
    }
}