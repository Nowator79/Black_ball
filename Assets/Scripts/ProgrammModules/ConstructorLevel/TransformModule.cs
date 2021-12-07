using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformModule 
{
    public VectorModule Position;
    public VectorModule Rotation;
    public VectorModule Size;

    public TransformModule()
    {
        Position = new VectorModule(0, 0, 0);
        Rotation = new VectorModule(0, 0, 0);
        Size = new VectorModule(0, 0, 0);
    }
    public TransformModule(VectorModule position, VectorModule rotation, VectorModule size)
    {
        Position = position;
        Rotation = rotation;
        Size = size;
    }
}
