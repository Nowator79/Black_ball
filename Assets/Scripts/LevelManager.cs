using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform StartPoint; 
    [HideInInspector]
    public int Level;

    public void Init()
    {
        StartPoint = transform.Find("StartPoint");
    }

    public LevelModule GetDetailsList()
    {
        LevelModule LevelModule;
        int id = 0;
        List<DetailLevelModule> detailLevels = new List<DetailLevelModule>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform TransformCurent = transform.GetChild(i);
            DetailLevel DetailLevel = TransformCurent.GetComponent<DetailLevel>();
            if (DetailLevel != null)
            {
                VectorModule position = new VectorModule(DetailLevel.transform.position.x, DetailLevel.transform.position.y, DetailLevel.transform.position.z);
                QModule rotation = new QModule(DetailLevel.transform.rotation.x, DetailLevel.transform.rotation.y, DetailLevel.transform.rotation.z, DetailLevel.transform.rotation.w);
                VectorModule scale = new VectorModule(DetailLevel.transform.localScale.x, DetailLevel.transform.localScale.y, DetailLevel.transform.localScale.z);
                TransformModule transformModule = new TransformModule(position, rotation, scale);
                detailLevels.Add(new DetailLevelModule(DetailLevel.Id, transformModule));
            }
        }

        LevelModule = new LevelModule(id, "TestLevel", detailLevels);
        return LevelModule;
    }
}
