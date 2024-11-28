using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

//Experiental Cloud Script Using Batching
public class Clouds : MonoBehaviour {

    //Cloud Data
    private struct Cloud {
        public float Scale;
        public int x;
        public int y;
    }
    //Creating a Batch
    private struct Batch {
        public int Length;
        public Cloud[] Clouds;
        public Matrix4x4[] Objects;
    }

    private struct FrameParams {
        public float Time;
        public float DeltaTime;
        public int BatchIndex;
    }


    [SerializeField]
    private Mesh m_mesh;
    [SerializeField]
    private Material m_Material;
    [SerializeField]
    private float m_CloudSize = 1;
    [SerializeField]
    private float m_MaxScale = 1;
    [SerializeField]
    private float m_TimeScale = .05f;

    [SerializeField]
    private float m_TexScale = .1f;
    [SerializeField]
    private float m_minNoiseSize = .6f;
    [SerializeField]
    private float m_ScaleSize = 1.5f;
    [SerializeField]
    private int m_cloudsCount = 100;

    //Set the Max amount of matrices arrays that can be parsed over to Graphics.DrawMeshInstanced
    private const int BatchSize = 1023;

    private Batch[] m_Batches;
    private Task[] m_Tasks;

    void Start() {

        // total count of clouds
        int count = m_cloudsCount * m_cloudsCount;

        // total count of batches. each size limited by unity to 1023
        int batchCount = count / BatchSize + 1;
        
        // initialize data arrays
        m_Batches = new Batch[batchCount];
        m_Tasks = new Task[batchCount];

        for (int i = 0; i < batchCount; i++)
        {
            // last array lenght can be less than 1023
            int length = Mathf.Min(BatchSize, count - i * BatchSize);
            m_Batches[i].Length = length;
            m_Batches[i].Clouds = new Cloud[length];
            m_Batches[i].Objects = new Matrix4x4[length];
        }

        //pivot of clouds should be at center, so i'll be just shifting each cloud
        var offset = -m_cloudsCount * .5f;
        
        //init data for each cloud
        for (int cloudY = 0; cloudY < m_cloudsCount; cloudY++) {
             for (int cloudX = 0; cloudX < m_cloudsCount; cloudX++) {

                 var cloud = new Cloud {
                     Scale = 0,
                     x = cloudX,
                     y = cloudY,
                 };

                //set the position of the cloud in world 
                var position = new Vector3 {
                    x = offset + transform.position.x + cloudX * m_CloudSize,
                    y = transform.position.y,
                    z = offset + transform.position.z + cloudY * m_CloudSize,
                };

                // convert X and Y into batch indices
                int index = cloudY * m_cloudsCount + cloudX;
                int x = index / BatchSize;
                int y = index % BatchSize;

                m_Batches[x].Clouds[y] = cloud;
                m_Batches[x].Objects[y] = Matrix4x4.TRS(position, Quaternion.identity, Vector3.zero);
             }
        }

    }

    void Update() {
        
        // each batch will be updated on a seperate Thread;
        for(int BatchIndex = 0; BatchIndex < m_Batches.Length; BatchIndex++) {

            //in order to avoid allocations while creating delegations such as 
            //()=> UpdateBatch(frameparams) im going to send the frame params as 
            //an object and then cast it into FrameParams;

            FrameParams frameParams = new FrameParams {
                BatchIndex = BatchIndex,
                DeltaTime = Time.deltaTime,
                Time = Time.time,
            };

            //creation of new Task allocates some memory but im too stupid
            //to figure out what to do with it so im leaving it as is
            //-Haoting 2020
            m_Tasks[BatchIndex] = Task.Factory.StartNew(UpdateBatch, frameParams);
        }

        //this will wait till all tasks given are completed
        Task.WaitAll();

        //and then send them all into render
        for(int batchIndex = 0; batchIndex < m_Batches.Length; batchIndex++) {
            Graphics.DrawMeshInstanced(m_mesh, 0, m_Material, m_Batches[batchIndex].Objects);
        }
    }

    void UpdateBatch(object input) {
        FrameParams frameParams = (FrameParams)input;

        for( int cloudIndex = 0; cloudIndex < m_Batches[frameParams.BatchIndex].Length; cloudIndex ++) {
            
            //as i said im stupid so lemme just simplify this shit because 
            //i cant fucking remember it with long words
            int i = frameParams.BatchIndex; int j = cloudIndex;

            //calculate the amount of noise cased on coordinates of the cloud
            //and current time
            float x = m_Batches[i].Clouds[j].x * m_TexScale + frameParams.Time * m_TimeScale;
            float y = m_Batches[i].Clouds[j].y * m_TexScale + frameParams.Time * m_TimeScale;
            float noise = Mathf.PerlinNoise(x, y);

            //and with the noise we get the scale direction!
            int dir = noise > m_minNoiseSize ? 1 : -1;

            //now to calculate the scale and clamp the shit out of it
            float shift = m_ScaleSize * frameParams.DeltaTime * dir;
            float scale = m_Batches[i].Clouds[j].Scale + shift;
            scale = Mathf.Clamp(scale, 0, m_MaxScale);
            m_Batches[i].Clouds[j].Scale = scale;
            

            // and finally...  we set a scale to the object matrix
            // set new scale to object matrix
            m_Batches[i].Objects[j].m00 = scale;
            m_Batches[i].Objects[j].m11 = scale;
            m_Batches[i].Objects[j].m22 = scale;
        }
    }
    
}
