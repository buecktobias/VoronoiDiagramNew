using System;
using System.Collections.Generic;

namespace VoronoiDiagram{

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
}