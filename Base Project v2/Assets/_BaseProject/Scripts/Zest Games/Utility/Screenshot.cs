using UnityEngine;

namespace ZestGames.Utility
{
    public static class Screenshot
    {
        public static int ScreenshotCount
        {
            get
            {
                return PlayerPrefs.GetInt("ScreenshotCount", 0);
            }
            set
            {
                PlayerPrefs.SetInt("ScreenshotCount", value);
            }
        }
        public static string ScreenshotName;

        /// <summary>
        /// Takes a screenshot sized by game resolution that you are currently in.
        /// </summary>
        public static void TakeAScreenshot()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ScreenshotCount++;
                ScreenshotName = "Screenshot_" + ScreenshotCount + ".png";
                ScreenCapture.CaptureScreenshot(ScreenshotName);
            }
        }
    }
}
