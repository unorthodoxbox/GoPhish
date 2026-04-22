using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class NotEnoughCoins : MonoBehaviour
{
    [SerializeField] private float visibleTime = 1.5f;
    [SerializeField] private float fadeDuration = 0.75f;

    private CanvasGroup canvasGroup;
    private Coroutine currentRoutine;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void ShowMessage()
    {
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }

        currentRoutine = StartCoroutine(ShowAndFade());
    }

    private IEnumerator ShowAndFade()
    {
        canvasGroup.alpha = 1f;

        yield return new WaitForSeconds(visibleTime);

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        currentRoutine = null;
    }
}