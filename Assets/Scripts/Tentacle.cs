using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public int length;
    public LineRenderer lineRendl;
    public Vector3[] segmentPoses;
    private Vector3[] segmentV;
    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;
    public float trailSpeed;


    public float wiggleSpeed;
    public float wiggleMagnitude;
  public Transform wiggleDir;
    //Start is called before the first frame update
    void Start()
    {
        lineRendl.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length];
    }

    // Update is called once per frame
    void Update()
    {
        wiggleDir.localRotation  =Quaternion.Euler(0,0,Mathf.Sin(Time.time*wiggleSpeed)*wiggleMagnitude);

        segmentPoses[0] = targetDir.position;
        for(int i = 1; i < segmentPoses.Length; i++)
        {
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i-1]+targetDir.right*targetDist,ref segmentV[i],smoothSpeed + i / trailSpeed);
        }
        lineRendl.SetPositions(segmentPoses);
    }
}
