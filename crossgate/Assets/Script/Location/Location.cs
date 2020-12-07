using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    //world cords
    private Vector3 worldCord3D;
    private Vector2 worldCord2D;

    //Zone
    private ZoneTypes zone;
    public enum ZoneTypes{
        A,
        B,  
        C,  
        None
    }

    public Vector3 WorlCord3D {
        get { return worldCord3D; }
    }
    public Vector2 WorlCord2D {
        get { return worldCord2D; }
    }
    public ZoneTypes Zone {
        get { return zone; }
    }

    public Location(Vector3 worldCord3D){
        this.worldCord3D = worldCord3D;
        worldCord2D = Vector2.zero;
        zone = ZoneTypes.None;
    }
    public Location(Vector2 worldCord2D){
        worldCord3D = Vector3.zero;
        this.worldCord2D = Vector2.zero;
        zone = ZoneTypes.None;
    }
    public Location(ZoneTypes zone){
        worldCord3D = Vector3.zero;
        worldCord2D = Vector2.zero;
        this.zone = zone;
    }

    public bool Compare(Location location){
        if(worldCord3D != Vector3.zero && location.worldCord3D == worldCord3D){
            return true;
        }else if(worldCord2D != Vector2.zero && location.worldCord2D == worldCord2D){
            return true;
        }else if(zone != ZoneTypes.None && location.zone == zone){
            return true;
        }else{
            return false;
        }
    }
}
