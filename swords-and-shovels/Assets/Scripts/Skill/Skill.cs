using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

public class Skill : MonoBehaviour, ISkill
{
    [SerializeField] private float defaultCooldown = 0f;
    [SerializeField] private Image cooldownFill;

    private DateTime readyAtUtc = DateTime.MinValue;
    private CancellationTokenSource cdCts;

    public bool IsReady => DateTime.UtcNow >= readyAtUtc;
    public float CooldownRemain => (float)Math.Max(0, (readyAtUtc - DateTime.UtcNow).TotalSeconds);

    private void Awake() => ResetState();

    public virtual bool TryCast(Mana mana, SkillData data)
    {
        if (mana == null || data == null) return false;
        if (!IsReady) return false;
        return true;
    }

    public virtual bool OnCast(Mana mana, Vector3 target) => true;

    protected void StartCooldown(float cd)
    {
        float useCd = cd > 0f ? cd : defaultCooldown;
        readyAtUtc = (useCd <= 0f) ? DateTime.UtcNow : DateTime.UtcNow.AddSeconds(useCd);

        cdCts?.Cancel();
        cdCts?.Dispose();
        cdCts = null;

        if (useCd > 0f && cooldownFill)
        {
            cdCts = new CancellationTokenSource();
            CooldownUIAsync(useCd, cdCts.Token).Forget();
        }
        else
        {
            if (cooldownFill) cooldownFill.fillAmount = 0f;
        }
    }

    private async UniTaskVoid CooldownUIAsync(float duration, CancellationToken ct)
    {
        if (cooldownFill) cooldownFill.fillAmount = 1f;
        try
        {
            while (!ct.IsCancellationRequested)
            {
                float remain = CooldownRemain;
                if (remain <= 0f) break;
                if (cooldownFill) cooldownFill.fillAmount = Mathf.Clamp01(remain / duration);
                await UniTask.Yield(PlayerLoopTiming.Update, ct);
            }
        }
        catch (OperationCanceledException) { }
        finally
        {
            if (cooldownFill) cooldownFill.fillAmount = 0f;
        }
    }

    private void OnDisable()
    {
        cdCts?.Cancel();
        cdCts?.Dispose();
        cdCts = null;
    }

    public void ResetState()
    {
        readyAtUtc = DateTime.UtcNow;
        if (cooldownFill) cooldownFill.fillAmount = 0f;
    }
}