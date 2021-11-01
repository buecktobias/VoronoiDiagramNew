namespace VoronoiDiagram{

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
}