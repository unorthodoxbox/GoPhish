using UnityEngine;

public class ProgressBar : MonoBehaviour
{

    public float progress = 0.0f; // Progress value between 0 and 1
    public float fillSpeed = 0.5f; // Speed at which the progress bar fills

    public Vector3 fullScale = new Vector3(4, 1, 1);
    public Vector3 emptyScale = new Vector3(0.1f, 1, 1);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateProgressBar();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateProgressBar();
    }

    private void UpdateProgressBar()
    {
        // Smoothly interpolate the scale of the progress bar based on the progress value
        Vector3 targetScale = Vector3.Lerp(emptyScale, fullScale, progress);
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, (fillSpeed * 10) * Time.deltaTime);
    }

    public void IncreaseProgress(float amount)
    {
        progress = Mathf.Clamp01(progress + amount);
    }

    public void DecreaseProgress(float amount)
    {
        progress = Mathf.Clamp01(progress - amount);
    }

    public void ResetProgress()
    {
        progress = 0.0f;
    }
}
