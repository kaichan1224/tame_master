using System;

/// <summary>
///　緯度経度変換用のStaticクラス
/// </summary>
public static class NaviMath
{
    // 地球の半径
    private const double EARTH_RADIUS = 6378.137d;
    public static double Deg2Rad { get { return Math.PI / 180.0d; } }

    /// <summary>
    /// ２地点の緯度経度間の距離をkmで計算する.Haversine formulaのアルゴリズムを使用
    /// </summary>
    /// <param name="a">地点aの緯度経度</param>
    /// <param name="b">地点bの緯度経度</param>
    /// <returns>地点aと地点bの間での距離km</returns>
    public static double LatlngDistance(Location a, Location b)
    {
        double dlat1 = a.Latitude * Deg2Rad;
        double dlng1 = a.Longitude * Deg2Rad;
        double dlat2 = b.Latitude * Deg2Rad;
        double dlng2 = b.Longitude * Deg2Rad;

        double d1 = Math.Sin(dlat1) * Math.Sin(dlat2);
        double d2 = Math.Cos(dlat1) * Math.Cos(dlat2) * Math.Cos(dlng2 - dlng1);
        double distance = EARTH_RADIUS * Math.Acos(d1 + d2);
        return distance;
    }
}
