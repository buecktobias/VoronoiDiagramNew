using System;

namespace VoronoiDiagram{

class EuclideanDistanceCalculator : IDistanceCalculator{
  public double calculateDistance(Point2D point1, Point2D point2){
      int XDiff = Math.Abs(point1.GetX() - point2.GetX());
      int YDiff = Math.Abs(point1.GetY() - point2.GetY());
      int XDiffSquared = XDiff * XDiff;
      int YDiffSquared = YDiff * YDiff;
      return Math.Sqrt(XDiffSquared + YDiffSquared);
  }
}
}