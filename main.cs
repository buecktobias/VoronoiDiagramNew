using System;
using System.Collections.Generic;

namespace VoronoiDiagram{
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
}