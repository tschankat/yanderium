using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RadarChart : MonoBehaviour
{
    [Header("Assets")]
    [SerializeField]
    private Material backgroundMaterial;
    [SerializeField]
    private Material circleMaterial;
    [SerializeField]
    private Material circleFillMaterial;
    [SerializeField]
    private Material radarScoreMaterial;
    [SerializeField]
    private Material radarScoreFillMaterial;
    [Space(5)]
    [Header("Line render settings")]
    [SerializeField]
    private LineRenderMode lineRenderMode = LineRenderMode.Quads;
    public enum LineRenderMode { Simple, Quads }
    [SerializeField]
    private float rotationOffset;
    [SerializeField]
    [Range(0.01f, 15)]
    private float thicknessSpacing = 0.01f;
    [SerializeField]
    [Range(0, 1)]
    private float radarLinesThickness = 0.02f;
    [SerializeField]
    [Range(0, 1)]
    private float radarCirclesThickness = 0.01f;
    [SerializeField]
    [Range(0, 1)]
    private float radarScoreLineThickness = 0.02f;
    [SerializeField]
    [Range(8, 360)]
    private int circleVertexLimit = 40;
    [SerializeField]
    private float radarSize;
    [SerializeField]
    private Vector2 radarRange = new Vector2(-100, 100);
    [Space(5)]
    [Header("Score font settings")]
    [SerializeField]
    private Font scoreFont;
    [SerializeField]
    private float scoreFontSize;
    [SerializeField]
    private Color scoreFontColor = Color.white;
    [SerializeField]
    private Vector3 scoreFontOffset;
    [SerializeField]
    private float scoreFontLength;
    [SerializeField]
    private float minFromRingDist = 0.2f;
    [SerializeField]
    private Color scoreFontShadow = Color.white;
    [SerializeField]
    private Vector2 scoreFontShadowOffset;
    [Space(5)]
    [Header("Field font settings")]
    [SerializeField]
    private Font fieldFont;
    [SerializeField]
    private float fieldFontSize;
    [SerializeField]
    private Color fieldFontColor = Color.white;
    [SerializeField]
    private float fieldFontOffset;
    [SerializeField]
    private Color fieldFontShadow = Color.white;
    [SerializeField]
    private Vector2 fieldFontShadowOffset;
    [Space(5)]
    [Header("Data")]
    [SerializeField]
    public RadarField[] fields;

    [ExecuteInEditMode]
    public void OnRenderObject()
    {
        thicknessSpacing = Mathf.Clamp(thicknessSpacing, 0.01f, 15);

        GL.MultMatrix(transform.localToWorldMatrix);

        //Draw background
        if (backgroundMaterial != null)
        {
            backgroundMaterial.SetPass(0);
            drawQuad(4);
        }

        float smallRadius = radarSize / 3;
        for (int i = 0; i < 4; i++)
        {
            circleMaterial.SetPass(0);
            drawCircle(circleVertexLimit, smallRadius * i, radarCirclesThickness, new Vector3(0, 0, -.002f), i == 3 ? circleFillMaterial : null);
        }

        //Draw fields
        circleMaterial.SetPass(0);
        List<Vector3> fieldPositions = new List<Vector3>();
        List<Vector3> namePoints = new List<Vector3>();
        List<Vector3> valuePoints = new List<Vector3>();
        List<string> fieldNames = new List<string>();
        List<float> fieldScores = new List<float>();
        for (int i = 0; i < fields.Length; i++)
        {
            float a = i / (float)fields.Length + rotationOffset/360;
            float angle = a * Mathf.PI * 2;
            Vector3 peakPoint = new Vector3(Mathf.Cos(angle) * radarSize, Mathf.Sin(angle) * radarSize, 0);
            drawLine(Vector3.zero, peakPoint, radarLinesThickness, -0.003f);
            fields[i].ValuePeakPosition = peakPoint;

            float radius = remap(fields[i].Value, radarRange.x, radarRange.y, 0, radarSize);
            Vector3 position = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            Vector3 namePoint = new Vector3(Mathf.Cos(angle) * (radarSize + fieldFontOffset), Mathf.Sin(angle) * (radarSize+fieldFontOffset), 0);
            float valueClamp = Mathf.Clamp((radius + scoreFontLength), 0, radarSize - minFromRingDist);
            Vector3 valuePoint = new Vector3(Mathf.Cos(angle) * valueClamp, Mathf.Sin(angle) * valueClamp, 0); ;
            fieldPositions.Add(position);
            fieldNames.Add(fields[i].Name);
            fieldScores.Add(fields[i].Value);
            namePoints.Add(namePoint);
            valuePoints.Add(valuePoint);
        }

        if (radarScoreFillMaterial != null)
        {
            radarScoreFillMaterial.SetPass(0);
            GL.PushMatrix();
            GL.Begin(GL.TRIANGLES);
            for (int i = 0; i < fieldPositions.Count; i++)
            {
                int index = i < fieldPositions.Count - 1 ? i + 1 : 0;
                GL.Vertex(Vector3.zero - new Vector3(0, 0, 0.0015f));
                GL.Vertex(fieldPositions[index] - new Vector3(0, 0, 0.0015f));
                GL.Vertex(fieldPositions[i] - new Vector3(0, 0, 0.0015f));
            }
            GL.End();
            GL.PopMatrix();
        }

        //Draw score line
        for (int i = 0; i < fieldPositions.Count; i++)
        {
            int index = i < fieldPositions.Count - 1 ? i + 1 : 0;

            radarScoreMaterial.SetPass(0);
            drawLine(fieldPositions[i], fieldPositions[index], radarScoreLineThickness, -0.006f);
            drawCircle(16, 0.03f, .01f, fieldPositions[i] + new Vector3(0, 0, -0.006f), radarScoreMaterial);

            //SCORE FONTS
            drawText(fieldScores[i].ToString(), //Shadow
                     scoreFont,
                     valuePoints[i] + new Vector3(scoreFontShadowOffset.x, scoreFontShadowOffset.y, 0) + scoreFontOffset,
                     scoreFontSize / 100, scoreFontShadow);
            drawText(fieldScores[i].ToString(), 
                     scoreFont, 
                     valuePoints[i] + scoreFontOffset, 
                     scoreFontSize/100, scoreFontColor);
        }
        for(int i = 0; i<namePoints.Count; i++)
        {
            drawText(fieldNames[i].ToString(), //Shadow
                     fieldFont,
                     namePoints[i] + new Vector3(0, 0, -0.02f) + new Vector3(fieldFontShadowOffset.x, fieldFontShadowOffset.y, 0),
                     fieldFontSize / 100, fieldFontShadow);
            drawText(fieldNames[i].ToString(),
                     fieldFont,
                     namePoints[i] + new Vector3(0, 0, -0.03f),
                     fieldFontSize / 100, fieldFontColor);
        }
    }
    private void drawCircle(int verts, float radius, float thickness = 1, Vector3 position = new Vector3(), Material fillMaterial = null) //Thickness will be implemented later
    {
        List<Vector3> verticies = new List<Vector3>();
        for (int i = 0; i < verts; i++)
        {
            float a = i / (float)verts;
            float angle = a * Mathf.PI * 2;
            verticies.Add(new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0));
        }
        for (int i = 0; i < verticies.Count; i++)
        {
            Vector3 targetEnd;
            if (i < verticies.Count - 1)
            {
                targetEnd = position + verticies[i + 1];
            }
            else
            {
                targetEnd = position + verticies[0];
            }
            drawLine(position + verticies[i], targetEnd, thickness, position.z);
        }
        GL.PushMatrix();
        GL.Begin(GL.TRIANGLES);
        if (fillMaterial != null)
        {
            position += new Vector3(0, 0, 0.001f);
            fillMaterial.SetPass(0);
            for (int i = 0; i < verticies.Count; i++)
            {
                if (i < verticies.Count - 1)
                {
                    GL.Vertex(position + verticies[i + 1]);
                }
                else
                {
                    GL.Vertex(position + verticies[0]);
                }
                GL.Vertex(position + verticies[i]);
                GL.Vertex(position);
            }
        }
        GL.End();
        GL.PopMatrix();
    }
    private void drawQuad(float size, float z = 0, Vector3 position = new Vector3())
    {
        GL.PushMatrix();
        GL.Begin(GL.QUADS);
        GL.Color(Color.white);
        GL.TexCoord2(0, 0); GL.Vertex(position + new Vector3(-size / 2, -size / 2, z));
        GL.TexCoord2(0, 1); GL.Vertex(position + new Vector3(-size / 2, size / 2, z));
        GL.TexCoord2(1, 1); GL.Vertex(position + new Vector3(size / 2, size / 2, z));
        GL.TexCoord2(1, 0); GL.Vertex(position + new Vector3(size / 2, -size / 2, z));
        GL.End();
        GL.PopMatrix();
    }
    private void drawLine(Vector3 start, Vector3 end, float thickness = 1, float z = 0)
    {
        switch (lineRenderMode)
        {
            case LineRenderMode.Simple:
                float thck = 0.01f;
                thickness *= 10;
                thickness = Mathf.Clamp(thickness, 0, 1);
                GL.PushMatrix();
                GL.Begin(GL.LINES);
                for (float x = -thickness / 2; x < thickness / 2; x += thck * 5)
                {
                    for (float y = -thickness / 2; y < thickness / 2; y += thck * 5)
                    {
                        Vector3 offset = new Vector3(thck * 15 * x, thck * 15 * y, z);
                        GL.Vertex(start + offset);
                        GL.Vertex(end + offset);
                    }
                }
                GL.End();
                GL.PopMatrix();
                break;
            case LineRenderMode.Quads:
                float length = Vector3.Distance(start, end);
                for (float i = 0; i < length; i += thicknessSpacing)
                {
                    float progress = i / length;
                    drawQuad(thickness, z, Vector3.Lerp(start, end, progress));
                }
                break;
        }
    }
    void drawText(string text, Font font, Vector3 position, float size, Color color = new Color())
    {
        Material m = new Material(font.material);
        m.color = color;
        m.SetPass(0);
        // Generate a mesh for the characters we want to print.
        var vertices = new Vector3[text.Length * 4];
        //var triangles = new int[text.Length * 6];
        var uv = new Vector2[text.Length * 4];
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < text.Length; i++)
        {
            // Get character rendering information from the font
            CharacterInfo ch;
            font.GetCharacterInfo(text[i], out ch);

            vertices[4 * i + 0] = (pos + new Vector3(ch.minX, ch.maxY, 0)) * size;
            vertices[4 * i + 1] = (pos + new Vector3(ch.maxX, ch.maxY, 0)) * size;
            vertices[4 * i + 2] = (pos + new Vector3(ch.maxX, ch.minY, 0)) * size;
            vertices[4 * i + 3] = (pos + new Vector3(ch.minX, ch.minY, 0)) * size;

            uv[4 * i + 0] = ch.uvTopLeft;
            uv[4 * i + 1] = ch.uvTopRight;
            uv[4 * i + 2] = ch.uvBottomRight;
            uv[4 * i + 3] = ch.uvBottomLeft;

            // Advance character position
            pos += new Vector3(ch.advance, 0, 0);
        }
        float width = (vertices[0].x - vertices[vertices.Length-3].x);
        float height = (vertices[0].y - vertices[vertices.Length-1].y);
        Vector3 offset = new Vector3(width/2, -height/2, 0);
        GL.PushMatrix();
        GL.Begin(GL.QUADS);
        for(int i = 0; i<vertices.Length; i++)
        {
            GL.TexCoord(uv[i]);
            GL.Vertex(offset + position + vertices[i]);
        }
        GL.End();
        GL.PopMatrix();
    }
    private float remap(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}

[System.Serializable]
public struct RadarField
{
    public string Name;
    public float Value;
    public Vector3 ValuePeakPosition;
}