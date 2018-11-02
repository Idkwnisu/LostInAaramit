using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSeasonalChange : SeasonalChange {
    enum Season { AUTUMN, WINTER, SPRING, SUMMER };

    public Mesh autumn_mesh;
    public Mesh spring_mesh;
    public Mesh winter_mesh;
    public Mesh summer_mesh;

    private MeshFilter meshFilter;

    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    public override void ChangeSeason(int newSeason)
    {
        switch (newSeason)
        {
            case (int)Season.AUTUMN:
                meshFilter.mesh = autumn_mesh;

                break;

            case (int)Season.SUMMER:
                meshFilter.mesh = summer_mesh;
                break;
            case (int)Season.WINTER:
                meshFilter.mesh = winter_mesh;
                break;
            case (int)Season.SPRING:
                meshFilter.mesh = spring_mesh;
                break;
        }
    }
}
