using UnityEngine;

/// <summary>
/// This class is responsible for the player unit movement on the android/iSO devices and in the editor.
/// It is just veiew and is not connected to the game logic.
/// </summary>
public class PlayerUnitMove : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidbody2D;
    
    [SerializeField]
    private float jumpForce = 10f;

    private void Update()
    {
        #if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.Space))
            AddForce();
        #endif

        if(Input.touchCount== 0)
            return;

        if(Input.GetTouch(0).phase == TouchPhase.Began)
            AddForce();
    }

    private void AddForce()
    {
        rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
