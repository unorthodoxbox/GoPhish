using UnityEngine;

public static class UICursorManager
{
    private static GameObject currentWindow;
    private static bool pauseLocked = false;
    public static bool IsPauseLocked => pauseLocked;

    public static void OpenPauseWindow(GameObject window)
    {
        if (currentWindow != null && currentWindow != window)
        {
            currentWindow.SetActive(false);
        }

        currentWindow = window;
        pauseLocked = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public static void ClosePauseWindow(GameObject window)
    {
        if (currentWindow == window)
        {
            currentWindow = null;
        }

        pauseLocked = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public static bool TryOpenWindow(GameObject window)
    {
        if (pauseLocked)
        {
            return false;
        }

        if (currentWindow != null && currentWindow != window)
        {
            currentWindow.SetActive(false);
        }

        currentWindow = window;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        return true;
    }

    public static void CloseWindow(GameObject window)
    {
        if (pauseLocked)
        {
            return;
        }

        if (currentWindow == window)
        {
            currentWindow = null;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}