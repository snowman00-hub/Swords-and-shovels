using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    public static DamagePopupManager Instance;
    public DamagePopup prefab;
    public Transform worldCanvas;

    private Queue<DamagePopup> pool = new Queue<DamagePopup>();

    void Awake()
    {
        Instance = this;

        for (int i = 0; i < 50; i++)
        {
            var popup = Instantiate(prefab, worldCanvas);
            popup.gameObject.SetActive(false);
            pool.Enqueue(popup);
        }
    }

    public void ShowDamage(Vector3 position, int damage)
    {
        DamagePopup popup;
        if (pool.Count > 0)
        {
            popup = pool.Dequeue();
        }
        else
        {
            popup = Instantiate(prefab, worldCanvas);
        }

        popup.Setup(damage);
        popup.transform.position = position + Vector3.up * 2f;
        HideAfterTimeAsync(popup, 1f).Forget();
    }

    private async UniTask HideAfterTimeAsync(DamagePopup popup, float time)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(time));
        popup.Hide();
        pool.Enqueue(popup);
    }
}