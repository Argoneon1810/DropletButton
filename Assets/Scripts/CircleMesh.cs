using System.Collections.Generic;
using UnityEngine;
using DropletButton.EaseOfUse;

namespace DropletButton.Shape
{
    [ExecuteInEditMode]
    public class CircleMesh : Circle
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        Mesh mesh;

        CanvasRenderer canvasRenderer;
        RectTransform rectTransform;
        [SerializeField] Material material;

        [SerializeField, Range(3, 128)] int division;

        bool validated;
        Rect lastRect;
        float lastRadius;

        public override void OnValidate()
        {
            validated = true;
        }

        private void Start()
        {
            if(!TryGetComponent(out canvasRenderer))
                canvasRenderer = gameObject.AddComponent<CanvasRenderer>();
            rectTransform = transform as RectTransform;
            CreateCircle();
        }

        private void Update()
        {
            if(lastRect != rectTransform.rect)
            {
                radius = rectTransform.sizeDelta.x < rectTransform.sizeDelta.y
                    ? rectTransform.sizeDelta.x / 2f
                    : rectTransform.sizeDelta.y / 2f;
                CreateCircle();
                lastRect = rectTransform.rect;
            }
            if(validated.AsTrigger())
            {
                if (canvasRenderer)
                    CreateCircle();
                if (rectTransform && lastRadius != radius)
                {
                    rectTransform.sizeDelta = new Vector2(radius * 2, radius * 2);
                    lastRadius = radius;
                }
            }
        }

        private void CreateCircle()
        {
            ResetCircleMesh();
            CreateCircleMesh();
            UpdateCanvasRenderer();
        }

        private void ResetCircleMesh()
        {
            vertices.Clear();
            triangles.Clear();
        }

        private void CreateCircleMesh()
        {
            Vector2 center = Vector2.zero;
            vertices.Add(center);
            for (int i = 0; i < division; ++i)
            {
                vertices.Add(center + (Quaternion.Euler(0, 0, -360 / (division * 1f) * i) * Vector2.up).ToVector2() * radius);
                triangles.Add(0);
                triangles.Add(i + 1);
                triangles.Add(i + 2 > division ? 1 : i + 2);
            }
        }

        private void UpdateCanvasRenderer()
        {
            mesh = new Mesh();

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();

            canvasRenderer.SetMaterial(material, null);
            canvasRenderer.SetMesh(mesh);
        }
        public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, sp, eventCamera, out var localPoint))
                return false;

            if (localPoint.sqrMagnitude <= radius * radius)
                return true;

            return false;
        }
    }
}