using System.Collections.Generic;
using UnityEngine;

namespace DropletButton.Shape
{
    public class DropletMask : MonoBehaviour
    {
        [SerializeField] Circle theAutoMovingOneTop, theAutoMovingOneBottom;
        [SerializeField] Circle theSmallOne, theBigOne;

        List<Vector3> vertices = new List<Vector3>();
        Mesh mesh;

        CanvasRenderer canvasRenderer;
        [SerializeField] Material material;

        Vector2 smallToBottomContact => theSmallOne.position + (theAutoMovingOneBottom.position - theSmallOne.position).normalized * theSmallOne.radius - (transform as RectTransform).anchoredPosition;
        Vector2 smallToTopContact => theSmallOne.position + (theAutoMovingOneTop.position - theSmallOne.position).normalized * theSmallOne.radius - (transform as RectTransform).anchoredPosition;
        Vector2 bigToBottomContact => theBigOne.position + (theAutoMovingOneBottom.position - theBigOne.position).normalized * theBigOne.radius - (transform as RectTransform).anchoredPosition;
        Vector2 bigToTopContact => theBigOne.position + (theAutoMovingOneTop.position - theBigOne.position).normalized * theBigOne.radius - (transform as RectTransform).anchoredPosition;

        private void Start()
        {
            canvasRenderer = GetComponent<CanvasRenderer>();

            mesh = new Mesh();

            vertices.Add(smallToBottomContact);
            vertices.Add(smallToTopContact);
            vertices.Add(bigToBottomContact);
            vertices.Add(bigToTopContact);

            mesh.vertices = vertices.ToArray();
            mesh.triangles = new int[6] { 0, 1, 2, 2, 1, 3 };

            canvasRenderer.SetMaterial(material, null);
        }

        private void Update()
        {
            vertices[0] = smallToBottomContact;
            vertices[1] = smallToTopContact;
            vertices[2] = bigToBottomContact;
            vertices[3] = bigToTopContact;

            mesh.vertices = vertices.ToArray();
            canvasRenderer.SetMesh(mesh);
        }
    }

}