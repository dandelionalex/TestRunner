using UnityEngine;

public class GameCameraView : MonoBehaviour
{
    [SerializeField]
    private Camera curentCamera;

    public Vector2 GameViewSize {get; private set;}

    void OnEnable()
    {
        GameViewSize = new Vector2(curentCamera.orthographicSize * curentCamera.aspect * 2f, curentCamera.orthographicSize * 2f);
    }
}