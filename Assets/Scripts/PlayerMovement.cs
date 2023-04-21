using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    
    Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // WASD
        float horizontalMovement = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float verticalMovement = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        
        // https://answers.unity.com/questions/285476/maintain-direction-regardless-of-rotation.html
        transform.Translate(horizontalMovement, verticalMovement, 0, Space.World);

        // ROTATE ACCORDING TO MOUSE
        // thanks: https://answers.unity.com/questions/855976/make-a-player-model-rotate-towards-mouse-location.html
        Vector2 positionOnScreen = _camera.WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = _camera.ScreenToViewportPoint(Input.mousePosition);
        
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        transform.rotation = Quaternion.Euler(new Vector3(0f,0f,angle));
    }
    
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
