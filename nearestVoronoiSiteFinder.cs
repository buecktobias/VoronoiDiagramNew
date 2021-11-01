using System;
using System.Collections.Generic;

namespace VoronoiDiagram{

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
}