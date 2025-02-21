﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class OnPatchDownloadProgressEvent : AEvent<Scene, OnPatchDownloadProgress>
    {
        protected override async ETTask Run(Scene scene, OnPatchDownloadProgress a)
        {

            scene.GetComponent<UIComponent>()?.GetDlgLogic<DlgHotUpdate>()?.
                    OnPatchDownloadProgress(a.TotalDownloadCount, a.CurrentDownloadCount, a.TotalDownloadSizeBytes, a.CurrentDownloadSizeBytes);
            await ETTask.CompletedTask;
        }

    }

    [Event(SceneType.Demo)]
    public class OnPatchDownlodFailedEvent : AEvent<Scene, OnPatchDownlodFailed>
    {
        protected override async ETTask Run(Scene scene, OnPatchDownlodFailed a)
        {
            scene.Fiber.Log.Error($"下载资源失败: {a.FileName} {a.Error}");
            await ETTask.CompletedTask;
        }
    }

    [FriendOf(typeof(DlgHotUpdate))]
    [EntitySystemOf(typeof(DlgHotUpdate))]
	public static partial class DlgHotUpdateSystem
	{

		public static void Awake(this DlgHotUpdate self)
		{
		 
		}
		public static void RegisterUIEvent(this DlgHotUpdate self)
		{
		 
		}

		public static void ShowWindow(this DlgHotUpdate self, ShowWindowDataBase contextData = null)
		{
		}


        public static void OnPatchDownloadProgress(this DlgHotUpdate self, int totalDownloadCount, int currentDownloadCount, long totalDownloadBytes, long currentDownloadBytes)
        {
            self.View.E_TxtLoadingText.text = $"{totalDownloadBytes}/{currentDownloadBytes}";
            self.View.E_ImgLoadingImage.fillAmount = 100.0f * currentDownloadBytes / totalDownloadBytes;
        }

    }
}
