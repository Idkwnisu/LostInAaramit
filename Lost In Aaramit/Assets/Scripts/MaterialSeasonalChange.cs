using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSeasonalChange : SeasonalChange {
    enum Season { AUTUMN, WINTER, SPRING, SUMMER };

    public Material summer_mat;
    public Material spring_mat;
    public Material winter_mat;
    public Material autumn_mat;

    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material = autumn_mat;
    }

    public override void ChangeSeason(int newSeason)
    {
        switch (newSeason)
        {
            case (int)Season.AUTUMN:
                renderer.material = autumn_mat;

                break;

            case (int)Season.SUMMER:
                renderer.material = summer_mat;
                break;
            case (int)Season.WINTER:
                renderer.material = winter_mat;
                break;
            case (int)Season.SPRING:
                renderer.material = spring_mat;
                break;
        }
    }
}
