using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTranformFromQr
{
    public Vector3 position;
    public Quaternion rotation;
    public string qrName;
    public string prefabName;

    public string toString()
    {
        return "Prefab name:"+ prefabName+ "\n"+
               "Position : \n"+
               "X :"+ position.x + "\n"+
               "Y :"+ position.y + "\n"+
               "Z :"+ position.z + "\n"+
               "Rotation : \n"+
               "X :"+ rotation.eulerAngles.x + "\n"+
               "Y :"+ rotation.eulerAngles.y + "\n"+
               "Z :"+ rotation.eulerAngles.z + "\n"+
               "For qr: "+ qrName;
    }

}
