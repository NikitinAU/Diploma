using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class QObjType
{
    public ObjType type;

    public string StringObjType()
    {
        switch (type)
        {
            case ObjType.Collectable: return "Collectable";
            case ObjType.Defeatable: return "Defeatable";
            case ObjType.Talkable: return "Talkable";
            default: return "None";
        }
    }
    public void SetObjTypeFromString(string Type)
    {
        switch (Type)
        {
            case "Collectable": { type = ObjType.Collectable; break; }
            case "Defeatable": { type = ObjType.Defeatable; break; }
            case "Talkable": { type = ObjType.Talkable; break; }
            default: { type = ObjType.None; break; }
        }
    }
}

public enum ObjType
{
    Talkable,
    Collectable,
    Defeatable,
    None
}
