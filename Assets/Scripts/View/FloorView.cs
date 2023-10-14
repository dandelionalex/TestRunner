using UnityEngine;
using Zenject;

/// <summary>
/// this is a buttom block in which player generally runs
/// </summary>
public class FloorView : MonoBehaviour
{
    [SerializeField]
    private Renderer renderer;

    private ISpeedManager speedManager;

    private GameCameraView gameViewSize;

    [Inject]
    private void Init(ISpeedManager playerUnitModel, GameCameraView gameViewSize)
    {
        this.speedManager = playerUnitModel;
        this.gameViewSize = gameViewSize;
    }

    void Start()
    {
        var meshScale = transform.localScale;
        transform.localScale = new Vector3(gameViewSize.GameViewSize.x, meshScale.y, meshScale.z);

        var textureScale = renderer.material.mainTextureScale;
        renderer.material.mainTextureScale = new Vector2(gameViewSize.GameViewSize.x, textureScale.y);
    }

    private void Update()
    {
        renderer.material.mainTextureOffset += new Vector2(speedManager.Speed * Time.deltaTime, 0);
    }
}
