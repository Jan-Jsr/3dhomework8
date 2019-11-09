using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Halo : MonoBehaviour
{
    public ParticleSystem particleSystem;
    private float time = 0;
    private ParticleSystem.Particle[] particlesArray;
    private Color[] changeColor = { new Color(255, 255, 255), new Color(255, 0, 0), new Color(255, 255, 0), new Color(0, 255, 0), new Color(0, 0, 255) };
    private float colorTimeOut = 0;
    public int particleNumber = 5000;       //最大粒子数
    public float pingPong = 0.05f;
    public float size = 0.05f;             //大小
    public float maxRadius = 10f;          //最大旋转半径
    public float minRadius = 4.0f;            //最小旋转半径     
    public float speed = 0.05f;             //旋转速度
    private float[] particleAngle;         //旋转角度数组
    private float[] particleRadius;        //旋转半径数组
   
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();

        particlesArray = new ParticleSystem.Particle[particleNumber];
        particleSystem.maxParticles = particleNumber;//最大粒子数
        particleAngle = new float[particleNumber];
        particleRadius = new float[particleNumber];
        particleSystem.Emit(particleNumber);//发射粒子
        particleSystem.GetParticles(particlesArray);//将粒子存入数组

        init();

        particleSystem.SetParticles(particlesArray, particlesArray.Length);
    }

    void Update()//粒子运动设置
    {
        colorTimeOut += Time.deltaTime;
        for (int i = 0; i < particleNumber; i++)
        {
            time += Time.deltaTime;
            particlesArray[i].color = changeColor[(int)(colorTimeOut % 5)];
            particleRadius[i] += (Mathf.PingPong(time / minRadius / maxRadius, pingPong) - pingPong / 2.0f);
            if (i % 2 == 0)//放大
            {
                particleAngle[i] += speed * (i % 10 + 1);
            }
            else//缩小
            {
                particleAngle[i] -= speed * (i % 10 + 1);
            }
            particleAngle[i] = (particleAngle[i] + 360) % 360;
            float rad = particleAngle[i] / 180 * Mathf.PI;
            //设置粒子位置
            particlesArray[i].position = new Vector3(particleRadius[i] * Mathf.Cos(rad), particleRadius[i] * Mathf.Sin(rad), 0f);
        }
        particleSystem.SetParticles(particlesArray, particleNumber);
    }

    void init()//粒子初始化
    {
 
        for (int i = 0; i < particleNumber; i++)
        {
         
            float angle = Random.Range(0.0f, 360.0f);
        
            float rad = angle / 180 * Mathf.PI;
            //设置速率
            float midRadius = (maxRadius + minRadius) / 2;
            float rate1 = Random.Range(1.0f, midRadius / minRadius);
            float rate2 = Random.Range(midRadius / maxRadius, 1.0f);
            float r = Random.Range(minRadius * rate1, maxRadius * rate2);
            //设置半径、角度等
            particlesArray[i].size = size;
            particleAngle[i] = angle;
            particleRadius[i] = r;
            particlesArray[i].position = new Vector3(r * Mathf.Cos(rad), r * Mathf.Sin(rad), 0.0f);
        }
    }
}
