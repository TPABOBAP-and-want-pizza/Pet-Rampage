using UnityEngine;
public interface IPursue 
{
    Transform PursueTransform { get; set; }
    bool isInPursueZone { get; set; }
    void StartTimer();
}
