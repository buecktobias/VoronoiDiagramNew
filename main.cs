using System;

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


class EuclideanDistanceCalculator{
  public static double calculateDistance(Point2D point1, Point2D point2){
      int XDiff = Math.Abs(point1.GetX() - point2.GetX());
      int YDiff = Math.Abs(point1.GetY() - point2.GetY());
      int XDiffSquared = XDiff * XDiff;
      int YDiffSquared = YDiff * YDiff;
      return Math.Sqrt(XDiffSquared + YDiffSquared);
  }
}


class Program {
  public static void Main (string[] args) {
    var point1 = new Point2D(10, 5);
    var point2 = new Point2D(0, 0);
    Console.WriteLine(EuclideanDistanceCalculator.calculateDistance(point1, point2));
  }
}