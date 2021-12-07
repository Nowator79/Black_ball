using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailLevelModule
{
    public int Id;
    public TransformModule Transform;

    public DetailLevelModule()
    {
        Id = 0;
        Transform = new TransformModule();
    }

    public DetailLevelModule(int id, TransformModule transform)
    {
        Id = id;
        Transform = transform;
    }
}
