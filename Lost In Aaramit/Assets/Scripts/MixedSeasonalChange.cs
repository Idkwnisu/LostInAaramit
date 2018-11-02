using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixedSeasonalChange : SeasonalChange {
    enum Season { AUTUMN, WINTER, SPRING, SUMMER };

    public Mesh autumn_mesh;
    public Mesh spring_mesh;
    public Mesh winter_mesh;
    public Mesh summer_mesh;


    public Material summer_mat;
    public Material spring_mat;
    public Material winter_mat;
    public Material autumn_mat;


    private MeshFilter meshFilter;


    private Renderer renderer;

    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        renderer = GetComponent<Renderer>();
        renderer.material = autumn_mat;
    }

    public override void ChangeSeason(int newSeason)
    {
        switch (newSeason)
        {
            case (int)Season.AUTUMN:
                meshFilter.mesh = autumn_mesh;
                renderer.material = autumn_mat;
                break;

            case (int)Season.SUMMER:
                meshFilter.mesh = summer_mesh;
                renderer.material = summer_mat;
                break;
            case (int)Season.WINTER:
                meshFilter.mesh = winter_mesh;
                renderer.material = winter_mat;
                break;
            case (int)Season.SPRING:
                meshFilter.mesh = spring_mesh;
                renderer.material = spring_mat;
                break;
        }
    }
}
