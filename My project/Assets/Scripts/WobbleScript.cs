using UnityEngine;

public class WobbleScript : MonoBehaviour
{
    public float wobbleIntensity = 0.1f;
    public float wobbleSpeed = 2f;

    public float maxWobble = 1;
    
    private Rigidbody rb;
    private Vector3 lastVelocity;
    private float wobbleFactor;
    private float wobbleTime;
    
    private MeshFilter meshFilter;
    private Vector3[] originalVertices;
    private Vector3[] modifiedVertices;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        originalVertices = meshFilter.mesh.vertices;
        modifiedVertices = new Vector3[originalVertices.Length];
        rb = GetComponent <Rigidbody>();
        lastVelocity = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate velocity change (acceleration)
        Vector3 velocityChange = rb.linearVelocity - lastVelocity;
        wobbleFactor = velocityChange.magnitude * wobbleIntensity;
        Mathf.Clamp(wobbleFactor, 0, maxWobble);
        lastVelocity = rb.linearVelocity;
        
        
        wobbleTime += Time.deltaTime * wobbleSpeed;

        wobbleFactor = Mathf.Lerp(wobbleFactor, velocityChange.magnitude * wobbleIntensity, Time.deltaTime * 5f);

        // Apply wobble based on velocity change
        float wobbleEffect = Mathf.Sin(wobbleTime) * wobbleFactor;

        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 vertex = originalVertices[i];
            float offset = Mathf.PerlinNoise(vertex.x + wobbleTime, vertex.y + wobbleTime) * wobbleEffect;
            modifiedVertices[i] = vertex + vertex.normalized * Mathf.Clamp(offset, 0, maxWobble);
        }

        // Update the mesh
        Mesh mesh = meshFilter.mesh;
        mesh.vertices = modifiedVertices;
        mesh.RecalculateNormals();
    }
}
