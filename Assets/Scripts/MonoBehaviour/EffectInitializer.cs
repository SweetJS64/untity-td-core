using UnityEngine;

public class EffectInitializer : MonoBehaviour
{
    [Tooltip("Добавь сюда ScriptableObject-ы с эффектами.")]
    public AbstractEffectConfig[] configs;

    private void Awake()
    {
        foreach (var config in configs)
        {
            Debug.Log($"Загружен эффект: {config.name}, Id: {config.Id}");
        }
    }
}