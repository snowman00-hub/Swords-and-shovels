using Cysharp.Threading.Tasks;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float moveTime = 1f;
    public float moveDistance = 2f;

    public void Open()
    {
        MoveDownAsync().Forget();
    }

    private async UniTask MoveDownAsync()
    {
        var originalPos = transform.position;
        var targetPos = transform.position + Vector3.down * moveDistance;

        float elapsed = 0f;
        while (elapsed < moveTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveTime;
            transform.position = Vector3.Lerp(originalPos, targetPos, t);
            await UniTask.Yield();
        }

        transform.position = targetPos;
    }
}