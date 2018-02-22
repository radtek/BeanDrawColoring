using UnityEngine;
using System.Collections;
using System;
//using Vuforia;
public class NewBehaviourScript : MonoBehaviour {

    //这个就是我们先前建立的panel
    public GameObject panel;

    void Update()
    {
        //获取相机图像
        Matrix4x4 P = GL.GetGPUProjectionMatrix(Camera.main.projectionMatrix, false);
        //相机位置
        Matrix4x4 V = Camera.main.worldToCameraMatrix;
        //识别图位置
        Matrix4x4 M = panel.GetComponent<Renderer>().localToWorldMatrix;
        Matrix4x4 MVP = P * V * M;
        
        panel.GetComponent<Renderer>().material.SetMatrix("_MATRIX_MVP", MVP);//截图传入shader中处理
    }
}