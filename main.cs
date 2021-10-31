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
  private char _siteLetter;
  private char _cellLetter;
  public VoronoiSite(int X, int Y, char siteLetter, char cellLetter) : base(X, Y) { 
    this._siteLetter = siteLetter;
    this._cellLetter = cellLetter;
  }

  public override string ToString(){
    return base.ToString() + " - " + this._siteLetter;
  }

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

class Rectangle{
  private Point2D _bottomLeftPoint;
  private Point2D _topRightPoint;

  public Rectangle(Point2D BottomLeftPoint, Point2D TopRightPoint){
    this._bottomLeftPoint = BottomLeftPoint;
    this._topRightPoint = TopRightPoint;
  }


  public Point2D getBottomLeftPoint(){
    return this._bottomLeftPoint;
  }

  public Point2D getTopRightPoint(){
    return this._topRightPoint;
  }
  
}

class PointCreator{

  public static List<List<Point2D>> createAllPointsInsideRectangle(Rectangle rectangle){

    List<List<Point2D>> points = new List<List<Point2D>>();
    for(int x = rectangle.getBottomLeftPoint().GetX(); x <= rectangle.getTopRightPoint().GetX(); x++){
      List<Point2D> column = new List<Point2D>();
      for(int y = rectangle.getBottomLeftPoint().GetY(); y <= rectangle.getTopRightPoint().GetY();y++){
          column.Add(new Point2D(x, y));
      }
      points.Add(column);

    }
    return points;
  }

}

class VoronoiDiagramCreator{

}
 


class Program {
  public static void Main (string[] args) {

    VoronoiSite vs1 = new VoronoiSite(2, 2, 'A', 'a');
    VoronoiSite vs2 = new VoronoiSite(2, 8, 'B', 'b');
    VoronoiSite vs3 = new VoronoiSite(3, 5, 'C', 'c');
    List<VoronoiSite> allVoronoiSites = new List<VoronoiSite>();
    allVoronoiSites.Add(vs1);
    allVoronoiSites.Add(vs2);
    allVoronoiSites.Add(vs3);
    Console.WriteLine(allVoronoiSites);
    var point1 = new Point2D(3, 5);
    NearestVoronoiSiteFinder nearestVoronoiSiteFinder = new NearestVoronoiSiteFinder(new EuclideanDistanceCalculator());

    Console.WriteLine(nearestVoronoiSiteFinder.findNearestVoronoiSite(point1, allVoronoiSites));
    List<List<Point2D>> points = PointCreator.createAllPointsInsideRectangle(new Rectangle(new Point2D(0,0), new Point2D(3,3)));
    foreach(var column in points){
      foreach(var y in column){
        Console.WriteLine(y);
      }
    }


  }
}