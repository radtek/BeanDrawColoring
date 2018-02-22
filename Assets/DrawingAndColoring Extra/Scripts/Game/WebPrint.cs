using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using cn.sharesdk.unity3d;
using System;

namespace IndieStudio.DrawingAndColoring.Logic
{
    public class WebPrint : MonoBehaviour
    {
        /// <summary>
        /// Whether process is running or not.
        /// </summary>
        public static bool isRunning;

        /// <summary>
        /// The flash effect fade.
        /// </summary>
        public Animator flashEffect;

        /// <summary>
        /// The flash sound effect.
        /// </summary>
        public AudioClip flashSFX;

        /// <summary>
        /// The objects bet hide/show on screen capturing.
        /// 下注的物品藏起来/在屏幕上显示。
        /// </summary>
        public Transform[] objects;

        /// <summary>
        /// The logo on the bottom of the page.
        /// </summary>
        public Transform bottomLogo;

    
        void Start()
        {
            isRunning = false;
        }

        /// <summary>
        /// Print the screen.
        /// </summary>
        public void PrintScreen()
        {
            //#if(UNITY_WEBPLAYER || UNITY_WEBGL || UNITY_EDITOR)
            //	Debug.LogWarning("Print feature works only in the Web platform, check out the Manual.pdf to implement print feature...");
            StartCoroutine("PrintScreenCoroutine");
            //#endif
        }
        GameObject ShareImgae;
        GameObject BackImage;
        private string photoPath;
        private bool photoSaved;

        public IEnumerator PrintScreenCoroutine()
        {
            string Path_save = "";
            isRunning = true;



            ShareImgae = GameObject.Find("ShareClick");
            BackImage = GameObject.Find("Back");
            ShareImgae.SetActive(false);
            BackImage.SetActive(false);

            HideObjects();
            if (bottomLogo != null)
                bottomLogo.gameObject.SetActive(true);
            string imageName = "BeanGod-" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";


            //Capture screen shot
            yield return new WaitForEndOfFrame();
            Texture2D texture = new Texture2D(Screen.width, Screen.height);
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            texture.Apply();


            //转为字节数组  
            byte[] bytes = texture.EncodeToPNG();
            //string destination = "/sdcard/DCIM/Camera";
            string destination = "/storage/emulated/0/DCIM/BeanGodPhoto";
            //判断目录是否存在，不存在则会创建目录  
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }
            Path_save = destination + "/" + imageName;
            //存图片  
            System.IO.File.WriteAllBytes(Path_save, bytes); 

            Debug.Log("保存图片了啊========" + Path_save);


                Debug.Log("=============进入try抛异常");
                AndroidJavaClass testActivityClass = new AndroidJavaClass("com.ryanwebb.androidscreenshot.MainActivity");
                Debug.Log("==========android 屏幕截图");
                if (testActivityClass != null)
                {
                    while (!photoSaved)
                    {
                        photoSaved = testActivityClass.CallStatic<bool>("scanMedia", Path_save);
                        yield return new WaitForSeconds(.1f);
                    }
                }



            flashEffect.SetTrigger("Run");
            if (flashSFX != null)
                AudioSource.PlayClipAtPoint(flashSFX, Vector3.zero, 1);
            yield return new WaitForSeconds(1);
            ShareClick(Path_save);

        }

        private string GetDefaultFilePath()
        {
            throw new NotImplementedException();
        }

        public void ShareClick(string imageName)
        {
            ShareImgae.SetActive(true);
            BackImage.SetActive(true);
            ShareImgae.GetComponent<Button>().onClick.AddListener(delegate() { FindObjectOfType<Demo>().MyShareClick(imageName); });
            //ShareImgae.GetComponent<Button>().onClick.AddListener(FindObjectOfType<Demo>().MyShareClick);

            //ShowObjects();
            //if (bottomLogo != null)
            //    bottomLogo.gameObject.SetActive(false);

            //Application.ExternalCall("PrintImage", System.Convert.ToBase64String(tex.EncodeToPNG()), imageName);
            //isRunning = false;
        }

        /// <summary>
        /// Hide the objects.
        /// </summary>
        private void HideObjects()
        {
            if (objects == null)
            {
                return;
            }

            foreach (Transform obj in objects)
            {
                if (obj != null)
                    obj.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Show the objects.
        /// </summary>
        private void ShowObjects()
        {
            if (objects == null)
            {
                return;
            }

            foreach (Transform obj in objects)
            {
                obj.gameObject.SetActive(true);
            }
        }
    }
}