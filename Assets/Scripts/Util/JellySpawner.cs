using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellySpawner : MonoBehaviour
{
    public LineRenderer _lineRenderer;
    public GameObject jellyPrefab;
    [Range(0.1f, 2f)] public float spawnInterval = 0.5f;

    [ContextMenu("Spawn Jellies")]
    void SpawnJellies()
    {
        ClearJellies();

        int vertexCount = _lineRenderer.positionCount;
        Vector3[] pos = new Vector3[vertexCount];//선분 개수
        _lineRenderer.GetPositions(pos);
        
        float totalLength = 0f;//총 길이
        for(int i = 0; i < vertexCount - 1; i++)
        {
            totalLength += Vector3.Distance(pos[i], pos[i + 1]);
        }

        float distancefromSpawn = 0f;
        int currentSegment = 0;
        float segmentStartDistance = 0f;

        while (distancefromSpawn <= totalLength)
        {
            float segmentLength = Vector3.Distance(pos[currentSegment], pos[currentSegment + 1]);//현재 선분의 길이
            float t = (distancefromSpawn - segmentStartDistance) / segmentLength;//선분에서 젤리가 생성될 위치의 비율
            t = Mathf.Clamp01(t);//오차, 오류 방지
            Vector3 spawnPosition = Vector3.Lerp(pos[currentSegment], pos[currentSegment + 1], t);//선분에서 젤리가 생성될 위치

            Instantiate(jellyPrefab, spawnPosition, Quaternion.identity, transform);

            distancefromSpawn += spawnInterval;//생성한 거리만큼 총 거리 증가

            //선분을 처음부터 다시 돌면서 현재 생성한 총 거리가 어느 선분에 있는지 체크
            while (currentSegment < vertexCount - 1 && (distancefromSpawn - segmentStartDistance) > segmentLength)
            {
                segmentStartDistance += segmentLength;
                currentSegment++;
                if (currentSegment >= vertexCount - 1) break;
                segmentLength = Vector3.Distance(pos[currentSegment], pos[currentSegment + 1]);
            }
        }
    }

    [ContextMenu("Clear Jellies")]
    void ClearJellies()
    {
#if UNITY_EDITOR
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
#endif
    }
}
