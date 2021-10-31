using System;
using System.Collections.Generic;

class Point2D{
  private int _x;
  private int _y;

  public Point2D(int X, int Y){
    this._x = X;
    this._y = Y;
  }

  public int GetX(){
    return this._x;
  }

  public int GetY(){
    return this._y;
  }

  public override string ToString(){
    return "[" + this.GetX() + ", " + this.GetY() + "]";
  }

}

class VoronoiSite : Point2D {
  public VoronoiSite(int X, int Y) : base(X, Y) { }

}


interface IDistanceCalculator{
  public double calculateDistance(Point2D point1, Point2D point2);
}

class EuclideanDistanceCalculator : IDistanceCalculator{
  public double calculateDistance(Point2D point1, Point2D point2){
      int XDiff = Math.Abs(point1.GetX() - point2.GetX());
      int YDiff = Math.Abs(point1.GetY() - point2.GetY());
      int XDiffSquared = XDiff * XDiff;
      int YDiffSquared = YDiff * YDiff;
      return Math.Sqrt(XDiffSquared + YDiffSquared);
  }
}

class NearestVoronoiSiteFinder{
  private IDistanceCalculator _distanceCalculator;

  public NearestVoronoiSiteFinder(IDistanceCalculator DistanceCalculator){
      this._distanceCalculator = DistanceCalculator;
  }

  public VoronoiSite findNearestVoronoiSite(Point2D fromPoint, List<VoronoiSite> allVoronoiSites){
    if(allVoronoiSites.Count == 0){
      throw new Exception("voronoi Sites length must be at least one");
    }else{
      VoronoiSite NearestVoronoiSiteSoFar = allVoronoiSites[0];
      double DistanceNearestVoronoiSiteSoFar = 999_999_999;
      foreach(var voronoiSite in allVoronoiSites){
          double distanceToVoronoiSite = this._distanceCalculator.calculateDistance(fromPoint, voronoiSite);

          if(distanceToVoronoiSite < DistanceNearestVoronoiSiteSoFar){
            NearestVoronoiSiteSoFar = voronoiSite;
            DistanceNearestVoronoiSiteSoFar = distanceToVoronoiSite;
          }

      }
      return NearestVoronoiSiteSoFar;
    }
  }
}


class Program {
  public static void Main (string[] args) {

    VoronoiSite vs1 = new VoronoiSite(2,2);
    VoronoiSite vs2 = new VoronoiSite(2,8);
    VoronoiSite vs3 = new VoronoiSite(3,5);
    List<VoronoiSite> allVoronoiSites = new List<VoronoiSite>();
    allVoronoiSites.Add(vs1);
    allVoronoiSites.Add(vs2);
    allVoronoiSites.Add(vs3);

    var point1 = new Point2D(3, 5);
    NearestVoronoiSiteFinder nearestVoronoiSiteFinder = new NearestVoronoiSiteFinder(new EuclideanDistanceCalculator());

    Console.WriteLine(nearestVoronoiSiteFinder.findNearestVoronoiSite(point1, allVoronoiSites));
    
  }
}