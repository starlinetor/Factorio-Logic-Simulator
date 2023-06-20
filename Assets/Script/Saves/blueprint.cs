using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SaveFile
{
    public Blueprint blueprint;
}

[Serializable]
public class Blueprint
{
    //this class defines the blueprint JSON file format
    public IconObject[] incons;
    public EntityObject[] entities;
}

[Serializable]
public class IconObject
{
    public SignalIDObject signal;
    public string index;
}

[Serializable]
public class SignalIDObject
{
    public string name;
    public string type;
}

[Serializable]
public class EntityObject
{
    public int entity_number;
    public string name;
    public PositionObject position;
    public int direction;
    public ConnectionObject connections;
}

[Serializable]
public class PositionObject
{
    public double x;
    public double y;
}

[Serializable]
public class ConnectionObject
{
    //Object containing information about the connections to other entities formed by red or green wires.

    //this naming will not work right when you generate the json file for the factorio blueprints, this because these two value should be named "1" and "2"
    public ConnectionPointObject first;
    public ConnectionPointObject second;

}

[Serializable]
public class ConnectionPointObject
{
    //The actual point where a wire is connected to. Contains information about where it is connected to.
    public ConnectionDataObject[] red;
    public ConnectionDataObject[] green;
}

[Serializable]
public class ConnectionDataObject
{
    //Information about a single connection between two connection points.
    public int entity_id;
    public int circuit_id;
}












