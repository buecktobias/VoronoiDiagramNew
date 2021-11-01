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

  public char getSiteLetter(){
    return this._siteLetter;
  }

  public char getCellLetter(){
    return this._cellLetter;
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
    private List<VoronoiSite> _voronoiSites;
    private List<List<Point2D>> _points;
    private Point2D _size;
    private NearestVoronoiSiteFinder _nearestVoronoiSiteFinder;

    public VoronoiDiagramCreator(List<VoronoiSite> sites, Point2D rectangleSize, IDistanceCalculator distanceCalculator){
      this._voronoiSites = sites;
      this._size = rectangleSize;
      this._nearestVoronoiSiteFinder = new NearestVoronoiSiteFinder(distanceCalculator);
      this.generatePoints(rectangleSize);
    }

    private void generatePoints(Point2D rectangleSize){
      Rectangle rectangle = new Rectangle(new Point2D(0, 0), rectangleSize);
      this._points = PointCreator.createAllPointsInsideRectangle(rectangle);
    }

    private List<List<char>> generateEmptyDiagram(){
      List<List<char>> diagram = new List<List<char>>();
      for(int x=0; x <= this._size.GetX(); x++){
        List<char> column = new List<char>();
        for(int y=0; y <= this._size.GetY(); y++){
          column.Add('#');
        }
        diagram.Add(column);
      }
      return diagram;
    }

    public List<List<char>> createDiagram(){
        List<List<char>> diagram = this.generateEmptyDiagram();

        foreach(var column in this._points){
          foreach(var point in column){
            VoronoiSite vs = this._nearestVoronoiSiteFinder.findNearestVoronoiSite(point, this._voronoiSites);
            diagram[point.GetX()][point.GetY()] = vs.getCellLetter();
          }
        }
        foreach(var vs in this._voronoiSites){
          diagram[vs.GetX()][vs.GetY()] = vs.getSiteLetter();
        }
        return diagram;
    }

}
 


class Program {
  public static void Main (string[] args) {

    VoronoiSite vs1 = new VoronoiSite(2, 2, 'A', '#');
    VoronoiSite vs2 = new VoronoiSite(2, 8, 'B', '0');
    VoronoiSite vs3 = new VoronoiSite(7, 5, 'C', '_');
    VoronoiSite vs4 = new VoronoiSite(15, 15, 'D', '*');
    VoronoiSite vs5 = new VoronoiSite(18, 5, 'E', '/');
    VoronoiSite vs6 = new VoronoiSite(3, 14, 'F', '=');
    List<VoronoiSite> allVoronoiSites = new List<VoronoiSite>();
    allVoronoiSites.Add(vs1);
    allVoronoiSites.Add(vs2);
    allVoronoiSites.Add(vs3);
    allVoronoiSites.Add(vs4);
    allVoronoiSites.Add(vs5);
    allVoronoiSites.Add(vs6);
    Point2D diagramSize = new Point2D(20, 20);
    VoronoiDiagramCreator vDC = new VoronoiDiagramCreator(allVoronoiSites, diagramSize, new EuclideanDistanceCalculator());
    List<List<char>> diagram = vDC.createDiagram();
    for(int y = diagramSize.GetY(); y >= 0; y--){
      for(int x = 0; x <= diagramSize.GetX(); x++){
        Console.Write(" " + diagram[x][y] + " ");
      }
      Console.WriteLine();
    }
  }
}