using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformModule 
{
    public VectorModule Position;
    public QModule Rotation;
    public VectorModule Size;

    public TransformModule()
    {
        Position = new VectorModule(0, 0, 0);
        Rotation = new QModule();
        Size = new VectorModule(0, 0, 0);
    }
    public TransformModule(VectorModule position, QModule rotation, VectorModule size)
    {
        Position = position;
        Rotation = rotation;
        Size = size;
    }
}
