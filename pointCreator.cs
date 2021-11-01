using System;
using System.Collections.Generic;

namespace VoronoiDiagram{
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
}
