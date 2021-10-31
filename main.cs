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


class Program {
  public static void Main (string[] args) {
    var point = new Point2D(10, 5);
    Console.WriteLine(point);
  }
}