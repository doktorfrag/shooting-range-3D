using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;
    public float minumumVert = -60.0f;
    public float maximumVert = 15.0f;
    private float _rotationX = 0;

    void Update()
    {
        //capture horizontal and vertical mouse rotation
        Cursor.visible = false;
        _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
        _rotationX = Mathf.Clamp(_rotationX, minumumVert, maximumVert);
        float delta = Input.GetAxis("Mouse X") * sensitivityHor;
        float rotationY = transform.localEulerAngles.y + delta;
        transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
    }
}
