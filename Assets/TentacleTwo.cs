using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleTwo : MonoBehaviour
{
    public int length;
    public LineRenderer lineRendl;
    public Vector3[] segmentPoses;
    private Vector3[] segmentV;
    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;



    public float wiggleSpeed;
    public float wiggleMagnitude;
    public Transform wiggleDir;
    // Start is called before the first frame update
    void Start()
    {
        lineRendl.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length];
    }

    // Update is called once per frame
    void Update()
    {
        wiggleDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);

        segmentPoses[0] = targetDir.position;
        for (int i = 1; i < segmentPoses.Length; i++)
        {
            Vector3 targetPos = segmentPoses[i - 1] + (segmentPoses[i] - segmentPoses[i - 1]).normalized * targetDist;
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], targetPos, ref segmentV[i], smoothSpeed);

            //bodyParts[i-1].transform.postition = segmentPos[i];
        }
        lineRendl.SetPositions(segmentPoses);
    }
}
