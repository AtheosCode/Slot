    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCameraView : MonoBehaviour {
    public bool isCustom = false;
    public List<Camera> cameraList = new List<Camera>();
	public float upperDistance = 1f;
	public float lowerDistance = 10f;

	// Update is called once per frame
	void Update () {
        if (isCustom)
        {
            for (int i = 0; i < cameraList.Count; i++)
            {
                if (cameraList[i]!= null)
                {
                    DrawCameraView(cameraList[i], Color.red, lowerDistance);
                    DrawCameraView(cameraList[i], Color.yellow, upperDistance);
                }
            }
        }
        else
        {
            for (int i = 0; i < Camera.allCamerasCount; i++)
            {
                DrawCameraView(Camera.allCameras[i], Color.red, lowerDistance);
                DrawCameraView(Camera.allCameras[i], Color.yellow, upperDistance);
            }
        }
	}

	void DrawCameraView(Camera targetCamera,Color color, float distance){
		Vector3[] corners = GetConers (targetCamera,distance);
		Debug.DrawLine (corners [0], corners [1], color);
		Debug.DrawLine (corners [1], corners [3], color);
		Debug.DrawLine (corners [3], corners [2], color);
		Debug.DrawLine (corners [2], corners [0], color);
	}
	Vector3[] GetConers(Camera targetCamera ,float distance){
		Transform tx = targetCamera.transform;
		Vector3[] corners = new Vector3[4];
		float halfFOV = (targetCamera.fieldOfView * 0.5f) * Mathf.Deg2Rad;
		float aspect = targetCamera.aspect;

		float height = distance * Mathf.Tan (halfFOV);
		float width = height * aspect;

		//UpperLeft
		corners[0] = tx.position - (tx.right * width );
		corners [0] += tx.up * height;
		corners [0] += tx.forward * distance;
		//UpperRight
		corners[1] = tx.position + (tx.right * width );
		corners [1] += tx.up * height;
		corners [1] += tx.forward * distance;
		//LowerLeft
		corners[2] = tx.position - (tx.right * width );
		corners [2] -= tx.up * height;
		corners [2] += tx.forward * distance;
		//LowerRight
		corners[3] = tx.position + (tx.right * width );
		corners [3] -= tx.up * height;
		corners [3] += tx.forward * distance;
		return corners;
	}
}
