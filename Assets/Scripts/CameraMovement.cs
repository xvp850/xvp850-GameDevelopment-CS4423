using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //[SerializeField] private Camera cam;

    //[SerializeField] private float zoomStep;
    //[SerializeField] private float minCamSize;
    //[SerializeField] private float maxCamSize;

    //[SerializeField] private SpriteRenderer mapRenderer;

    [SerializeField] private float cameraSpeed = 5;

    private float xMax;
    private float yMax;

    //private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    //private Vector3 dragOrigin;

    private void Awake() {
        //mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        //mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;

        //mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        //mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
    }

    private void Update() {
        GetInput();
    }

    private void PanCamera() {

    }

    public void ZoomIn() {
        //float newSize = cam.orthographicSize - zoomStep;
        //cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
    }

    public void ZoomOut() {
        //float newSize = cam.orthographicSize + zoomStep;
        //cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
    }

    private void GetInput() {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            transform.Translate(Vector3.down * cameraSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
        }

        //transform position = new Vector3(Mathf.Clamp(transform.position.x,0,(0.5, 0.5),
        //Mathf.Clamp(transform.position.y, (-0.5, -0.5), 0)));
    }

    private void SetLimits(Vector3 maxTile) {
        Vector3 worldPoint = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));
        xMax = maxTile.x - worldPoint.x;
        yMax = maxTile.y - worldPoint.y;
    }
}
