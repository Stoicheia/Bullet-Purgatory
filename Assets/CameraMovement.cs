using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    private const float FULL_ZOOM = 5;
    
    private Bounds stageBounds;
    private Player player;
    private Camera cam;

    [Range(0.01f,1)]
    public float defaultZoom;
    private float zoom;
    [Range(0, 0.99f)] 
    public float smoothing;

    private Vector3 targetVector;
    private Vector3 moveToVector;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        zoom = defaultZoom;
        
        SetStandardValues();
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();

        if (player == null)
        {
            Debug.LogError("There is no player in this scene!");
        }

        stageBounds = GameObject.FindGameObjectWithTag("Stage").GetComponent<MeshCollider>().bounds;

        if (stageBounds == null)
        {
            Debug.LogError("There is no stage in this scene!");
        }

        moveToVector = transform.position;
    }

    private void Update()
    {
        cam.orthographicSize = FULL_ZOOM * zoom;
        float minX = stageBounds.center.x - stageBounds.size.x * (1 - zoom) / 2;
        float maxX = stageBounds.center.x + stageBounds.size.x * (1 - zoom) / 2;
        float minY = stageBounds.center.y - stageBounds.size.y * (1 - zoom) / 2;
        float maxY = stageBounds.center.y + stageBounds.size.y * (1 - zoom) / 2;
        Vector3 playerPos = player.transform.position;
        targetVector 
            = new Vector3(Mathf.Lerp(minX,maxX,(stageBounds.max.x - playerPos.x)/stageBounds.size.x),
                            Mathf.Lerp(minY,maxY,(stageBounds.max.y - playerPos.y)/stageBounds.size.y),-10);
        moveToVector = Vector3.Lerp(moveToVector, targetVector, Time.deltaTime*(1-smoothing));
        transform.position = moveToVector;
    }

    void SetStandardValues()
    {
        defaultZoom = 0.96f;
        smoothing = 0.28f;
    }
}
